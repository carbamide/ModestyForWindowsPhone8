using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modesty2.Models
{
    public class ModestyInfo
    {
        /// <summary>
        /// Reference to the Server object
        /// </summary>
        public Server ServerInformation;

        /// <summary>
        /// Reference to a List of Player objects
        /// </summary>
        public List<Player> Players;
    }
}
