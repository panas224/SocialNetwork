﻿using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Apache_Cassandra
{
    public class UserThread:IUserThread
    {
        public void SyncUserStream(ISession session, Guid postId, TimeUuid updatedAtPrev)
        {
            // Get follower list
            var getFollowerList = session.Prepare("Select followers from post_followers where post_id = ? ");
            var listFollowers = session.Execute(getFollowerList.Bind(postId));

            List<Guid> list = new List<Guid>();
            foreach (var follower in listFollowers)
            {
                list = follower.GetValue<List<Guid>>("followers");

            }

            //Delete post from user stream
            var deletePost = session.Prepare("DELETE FROM user_stream where user_id =? and last_updated_at = ?");
            foreach (var follower in list)
            {
                session.Execute(deletePost.Bind(follower, updatedAtPrev));
            }


            //Get data from posts
            Guid authorId = new Guid();
            TimeUuid updatedAt = new TimeUuid();
            string content = "";

            var getPosts = session.Prepare("Select * from posts where post_id = ? ");
            var postsList = session.Execute(getPosts.Bind(postId));

            foreach (var post in postsList)
            {
                authorId = post.GetValue<Guid>("author_id");
                updatedAt = post.GetValue<TimeUuid>("updated_at");
                content = post.GetValue<string>("content");
            }

            //Get data from comments
            List<Posts> comments = new List<Posts>();
            var getComments = session.Prepare("Select * from comments where post_id = ? ");
            var listComments = session.Execute(getComments.Bind(postId));

            foreach (var commentRow in listComments)
            {
                Posts comment = new Posts()
                {
                    PostAuthor = commentRow.GetValue<TimeUuid>("comment_id"),
                    Comment = commentRow.GetValue<string>("comment"),
                    Poster = commentRow.GetValue<Guid>("user_id")
                };

                comments.Add(comment);
            }

            //Insert post in user stream
            var insertUserStream =
                session.Prepare("INSERT INTO user_stream (user_id, last_updated_at , post_id , author_id , content, comments) VALUES(?,?,?,?,?,?)");

            foreach (var follower in list)
            {
                session.Execute(insertUserStream.Bind(follower, updatedAt, postId, authorId, content, comments));
            }

        }
    }
}
