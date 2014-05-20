using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modesty2.Models
{
    public class Server
    {
        /// <summary>
        /// The hosts IP
        /// </summary>
        public String HostIp;

        /// <summary>
        /// The version of Minecraft that the server is running
        /// </summary>
        public String Version;

        /// <summary>
        /// The gametype that the server is currently running, ie, SMP
        /// </summary>
        public String GameType;

        /// <summary>
        /// List of plugins, as strings
        /// </summary>
        public List<String> Plugins;

        /// <summary>
        /// Software version of the Minecraft server
        /// </summary>
        public String Software;

        /// <summary>
        /// Hostname of the Minecraft server
        /// </summary>
        public String HostName;

        /// <summary>
        /// Max number of players that the Minecraft server allows
        /// </summary>
        public Int16 MaxPlayers;

        /// <summary>
        /// Type of map used on Minecraft server
        /// </summary>
        public String Map;

        /// <summary>
        /// The current number of players on the Minecraft server
        /// </summary>
        public Int16 Players;

        /// <summary>
        /// The port that the Minecraft server uses
        /// </summary>
        public Int32 HostPort;
    }
}
