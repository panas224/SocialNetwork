using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using 

namespace DAL.Apache_Cassandra
{
    public class Posts:IPosts
    {
        private readonly IUserThread userThread;
        public void CreatePost(ISession session)
        {
            Guid PostOwnerId = Guid.NewGuid();
            TimeUuid updateAt = TimeUuid.NewId(DateTimeOffset.Now);
            Console.WriteLine("Enter post:");
            string context = Console.ReadLine();
            var insertPost = session.Prepare(
                "INSERT INTO posts (post_id, author_id,content, updated_at) VALUES(?,?,?, ?)");
            session.Execute(insertPost.Bind(PostOwnerId, updateAt));

            var insertPostFollowers = session.Prepare(
                "INSERT INTO post_followers (PostOwnerId, followers) VALUES(?,?)");

            userThread.SyncUserStream(session,  updatedAt);


        }
        public void UpdatePost(ISession session)
        {


            TimeUuid updatedAt = TimeUuid.NewId(DateTimeOffset.Now);


            Console.Write("Enter PostOwnerId : ");
            string strPostId = Console.ReadLine();
            Guid postId = new Guid(strPostOwnerId);


            Console.Write("Edit post : ");
            string content = Console.ReadLine();

            TimeUuid updatedAtPrev = new TimeUuid();
            var getLastUpdate = session.Prepare("Select updated_at from posts where post_id = ? ");
            var ex = session.Execute(getLastUpdate.Bind(postId));
            foreach (var lastUpdateAt in ex)
            {
                updatedAtPrev = lastUpdateAt.GetValue<TimeUuid>("updateAt");
            }


            var updatedPost = session.Prepare(
                "Update posts set content = ?, updateAt=?  where postOwnerId = ?");
            session.Execute(updatedPost.Bind(content, updatedAt, postId));


            userThread.SyncUserStream(session, postId, updatedAtPrev);

        }
    }
    
}
