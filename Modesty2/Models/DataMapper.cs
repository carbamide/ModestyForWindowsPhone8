using Modesty2.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Modesty2.Models
{
    public sealed class DataMapper
    {
        /// <summary>
        /// Reference to a ModestyInfo object
        /// </summary>
        public ModestyInfo ModestyInfo;

        /// <summary>
        /// Reference to the MainViewModel, used in Begin.Invoke methods to call the main thread
        /// </summary>
        public MainViewModel MainViewModel;

        /// <summary>
        /// Reference to a List of staff members
        /// </summary>
        public List<Staff> Staff;

        /// <summary>
        /// Singleton instance
        /// </summary>
        private static readonly DataMapper uniqueInstance = new DataMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        private DataMapper()
        {
            Console.WriteLine("Singleton Instance Created.");
        }

        /// <summary>
        /// Method that returns the singleton instance
        /// </summary>
        /// <returns></returns>
        public static DataMapper GetInstance()
        {
            return uniqueInstance;
        }

        /// <summary>
        /// Ping modesty.  Simply returns and up or down string as a result.
        /// </summary>
        public void PingModesty()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://aqueous-lowlands-3303.herokuapp.com/ping.php");
            request.Method = "GET";
            request.Accept = "application/json";

            request.BeginGetResponse(PingModestyCallback, request);
        }

        /// <summary>
        /// The callback for the PingModesty method
        /// </summary>
        /// <param name="result">The async result, as returned from HttpWebRequest</param>
        private void PingModestyCallback(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Begins getting the staff listing from the api
        /// </summary>
        public void StaffListing()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://safe-retreat-6833.herokuapp.com/users.json");
            request.Method = "GET";
            request.Accept = "application/json";

            request.BeginGetResponse(StaffListingCallback, request);
        }

        /// <summary>
        /// The callback for StaffListing()
        /// </summary>
        /// <param name="result">The async result, as returned from HttpWebRequest</param>
        private void StaffListingCallback(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;

            if (request != null)
            {
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

                    StreamReader streamReader = new StreamReader(response.GetResponseStream());

                    String responseText = streamReader.ReadToEnd();

                    MapToStaff(responseText);
                }
                catch (WebException ignored)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Refreshes the main information
        /// </summary>
        public void RefreshInformation()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://aqueous-lowlands-3303.herokuapp.com");
            request.Method = "GET";
            request.Accept = "application/json";

            request.BeginGetResponse(RefreshInformationCallback, request);
        }

        /// <summary>
        /// The callback for RefreshInformation()
        /// </summary>
        /// <param name="result">The async result, as returned from HttpWebRequest</param>
        private void RefreshInformationCallback(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;

            if (request != null)
            {
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

                    StreamReader streamReader = new StreamReader(response.GetResponseStream());

                    String responseText = streamReader.ReadToEnd();

                    MapToModestyInfo(responseText);
                }
                catch (WebException ignored)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Maps the information from RefreshInformationCallback() to objects
        /// </summary>
        /// <param name="jsonObject">The json string as returned from RefreshInformationCallback()</param>
        public void MapToModestyInfo(String jsonObject)
        {
            JObject tempJsonObject = JObject.Parse(jsonObject);
            JObject tempInfoObject = (JObject)tempJsonObject["info"];

            ModestyInfo tempModestyInfo = new ModestyInfo();

            Server server = new Server();

            server.GameType = tempInfoObject.Value<string>("GameType");
            server.HostIp = tempInfoObject.Value<string>("HostIp");
            server.Version = tempInfoObject.Value<string>("Version");
            server.Software = tempInfoObject.Value<string>("Software");
            server.MaxPlayers = tempInfoObject.Value<Int16>("MaxPlayers");
            server.Map = tempInfoObject.Value<string>("Map");
            server.Players = tempInfoObject.Value<Int16>("Players");
            server.HostPort = tempInfoObject.Value<Int32>("HostPort");

            JArray jsonArray = (JArray)tempJsonObject["players"];

            tempModestyInfo.Players = new List<Player>();

            foreach (var playerString in jsonArray.Children())
            {
                Player player = new Player();

                player.Username = playerString.ToString();

                tempModestyInfo.Players.Add(player);
            }

            JArray tempPluginArray = (JArray)tempInfoObject["Plugins"];
            List<String> pluginArray = new List<String>();

            foreach (String plugin in tempPluginArray)
            {
                if (plugin.ToLower().Contains("capturecraft") ||
                    plugin.ToLower().Contains("disguisecraft") ||
                    plugin.ToLower().Contains("mcmmo") ||
                    plugin.ToLower().Contains("xpbanker"))
                {
                    pluginArray.Add(plugin);
                }
            }

            pluginArray.Add("And Many More!");

            server.Plugins = pluginArray;

            tempModestyInfo.ServerInformation = server;
            this.ModestyInfo = tempModestyInfo;

            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => {
                this.MainViewModel.RefreshInfoListView();
                this.MainViewModel.RefreshPlayersListView();
                this.MainViewModel.RefreshPluginListView();
            });
        }

        /// <summary>
        /// Maps the staff json string to objects
        /// </summary>
        /// <param name="jsonObject"><The staff json string as returned from the api/param>
        public void MapToStaff(String jsonObject)
        {
            JArray tempJsonObject = JArray.Parse(jsonObject);
            List<Staff> staff = new List<Staff>();

            foreach (JArray tempArray in tempJsonObject)
            {
                foreach (JObject tempStaffObject in tempArray)
                {
                    Staff tempStaff = new Staff();

                    tempStaff.Username = tempStaffObject.Value<string>("username");
                    tempStaff.Rank = tempStaffObject.Value<string>("rank");

                    staff.Add(tempStaff);
                }
            }

            this.Staff = staff;

            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                this.MainViewModel.RefreshStaffListView();
            });
        }
    }
}
