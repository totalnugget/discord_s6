using System;

namespace UserService.Data.utils
{
    public static class ConnectionStringUtil
    {
        public static string GetConnectionString()
        {
            string host = Environment.GetEnvironmentVariable("DB_HOST");
            string port = Environment.GetEnvironmentVariable("DB_PORT");
            string name = Environment.GetEnvironmentVariable("DB_NAME");
            string username = Environment.GetEnvironmentVariable("DB_USERNAME");
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            Console.WriteLine("data for connection:" + username + " : " + password);

            return $"Host={host};Username={username};Password={password};Database={name}";
        }
    }
}