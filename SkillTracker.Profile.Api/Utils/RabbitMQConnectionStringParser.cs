namespace SkillTracker.Profile.Api.Utils
{
    public class RabbitMqConnectionString
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public class RabbitMqConnectionStringParser
    {
        public RabbitMqConnectionString ParseConnectionString(string connectionString)
        {
            var uri = new Uri(connectionString);
            var credentials=uri.UserInfo.Split(':');
            return new RabbitMqConnectionString
            {
                Username = credentials[0],
                Password = credentials[1],
                Host = uri.Host,
                Port = uri.Port
            };
        }
    }
}
