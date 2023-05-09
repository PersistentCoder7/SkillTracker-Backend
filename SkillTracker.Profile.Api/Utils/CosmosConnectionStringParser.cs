namespace SkillTracker.Profile.Api.Utils
{
    public class STCosmosConnectionStringParser
    {
        public string AccountEndpoint { get; private set; }
        public string AccountKey { get; private set; }
        public string Database { get; private set; }
        public string Container { get; private set; }

        public void ParseConnectionString(string connectionString)
        {
            var parts = connectionString.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                var kvp = part.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                if (kvp.Length == 2)
                {
                    var key = kvp[0].Trim().ToLowerInvariant();
                    var value = kvp[1].Trim();

                    switch (key)
                    {
                        case "accountendpoint":
                            AccountEndpoint = value;
                            break;
                        case "accountkey":
                            AccountKey = value;
                            break;
                        case "database":
                            Database = value;
                            break;
                        case "container":
                            Container = value;
                            break;
                    }
                }
            }
        }
    }

}
