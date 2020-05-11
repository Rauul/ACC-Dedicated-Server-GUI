namespace ACC_Dedicated_Server_GUI
{
    class Configuration
    {

        public class ConfigurationObject
        {
            public int udpPort { get; set; }
            public int tcpPort { get; set; }
            public int maxConnections { get; set; }
            public int lanDiscovery { get; set; }
            public int registerToLobby { get; set; }
            public int configVersion { get; set; }
        }

    }
}
