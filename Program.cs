using Cassandra;
using System;
using System.Linq;

namespace CassandraCli
{
    class Program
    {
        static void Main(string[] args)
        {
            Cluster cluster = Cluster.Builder()
                         .AddContactPoint("127.0.0.1")
                         .Build();

            ISession session = cluster.Connect("demo");

            SetUser(session, "user2", 25, "country2", "user2@country2.com", "first2");
            GetUsers(session);

            Console.ReadKey();
        }

        private static void SetUser(ISession session, string lastname, int age, string city, string email, string firstname)
        {
            var statement = new SimpleStatement("INSERT INTO users (lastname, age, city, email, firstname) VALUES (?,?,?,?,?)", lastname, age, city, email, firstname);
            session.Execute(statement);
        }

        private static void GetUsers(ISession session)
        {
            var statement = new SimpleStatement("SELECT * FROM users");
            var results = session.Execute(statement);

            foreach (var result in results)
            {
                Console.WriteLine("firstname:{0} lastname:{1} age:{2} city:{3} email:{4}",
                    result["firstname"],
                    result["lastname"],
                    result["age"],
                    result["city"],
                    result["email"]);
            }
        }
    }
}
