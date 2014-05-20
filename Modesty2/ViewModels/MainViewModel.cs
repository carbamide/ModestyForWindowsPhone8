using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Modesty2.Resources;
using System.Net;
using Microsoft.Phone.Shell;
using Modesty2.Models;
namespace Modesty2.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Information Items
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }

        /// <summary>
        /// Players
        /// </summary>
        public ObservableCollection<ItemViewModel> Players { get; private set; }

        /// <summary>
        /// Social Links
        /// </summary>
        public ObservableCollection<ItemViewModel> Social { get; private set; }

        /// <summary>
        /// Staff Members
        /// </summary>
        public ObservableCollection<ItemViewModel> Staff { get; private set; }

        /// <summary>
        /// Plugin list
        /// </summary>
        public ObservableCollection<ItemViewModel> Plugins { get; private set; }

        /// <summary>
        /// Instantiates the ObservableCollections and loads the information
        /// </summary>
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
            this.Players = new ObservableCollection<ItemViewModel>();
            this.Social = new ObservableCollection<ItemViewModel>();
            this.Staff = new ObservableCollection<ItemViewModel>();
            this.Plugins = new ObservableCollection<ItemViewModel>();

            DataMapper.GetInstance().MainViewModel = this;
            DataMapper.GetInstance().RefreshInformation();
            DataMapper.GetInstance().StaffListing();
        }

        /// <summary>
        /// Refreshes the main info list and stops the progress indicator
        /// </summary>
        public void RefreshInfoListView()
        {
            Server server = DataMapper.GetInstance().ModestyInfo.ServerInformation;

            this.Items.Add(new ItemViewModel() { LineOne = "host", LineTwo = server.HostIp, LineThree = "" });
            this.Items.Add(new ItemViewModel() { LineOne = "port", LineTwo = server.HostPort.ToString(), LineThree = "" });
            this.Items.Add(new ItemViewModel() { LineOne = "version", LineTwo = server.Version, LineThree = "" });
            this.Items.Add(new ItemViewModel() { LineOne = "staff", LineTwo = "staff listing, along with ranks", LineThree = "" });
            this.Items.Add(new ItemViewModel() { LineOne = "players", LineTwo = server.Players.ToString() + " of " + server.MaxPlayers.ToString() + " max players", LineThree = "" });
            this.Items.Add(new ItemViewModel() { LineOne = "plugins", LineTwo = "plugin information, and more!", LineThree = "" });

            var progressIndicator = SystemTray.ProgressIndicator;

            if (progressIndicator != null)
            {
                progressIndicator.IsVisible = false;
            }
        }

        /// <summary>
        /// Refreshes the players list
        /// </summary>
        public void RefreshPlayersListView()
        {
            ModestyInfo modestyInfo = DataMapper.GetInstance().ModestyInfo;

            foreach (Player player in modestyInfo.Players)
            {
                this.Players.Add(new ItemViewModel() { LineOne = player.Username, AvatarUrl =  "https://minotar.net/helm/" + player.Username + "/30.png"});
            }
        }

        /// <summary>
        /// Refreshes the staff list and stops the progress indicator
        /// </summary>
        public void RefreshStaffListView()
        {
            DataMapper dataMapper = DataMapper.GetInstance();

            foreach (Staff tempStaff in dataMapper.Staff)
            {
                this.Staff.Add(new ItemViewModel() { LineOne = tempStaff.Username, LineTwo = tempStaff.Rank, AvatarUrl = "https://minotar.net/helm/" + tempStaff.Username + "/30.png" });
            }

            var progressIndicator = SystemTray.ProgressIndicator;

            if (progressIndicator != null)
            {
                progressIndicator.IsVisible = false;
            }
        }

        /// <summary>
        /// Refreshes the plugin list
        /// </summary>
        public void RefreshPluginListView()
        {
            Server server = DataMapper.GetInstance().ModestyInfo.ServerInformation;

            foreach (String plugin in server.Plugins)
            {
                this.Plugins.Add(new ItemViewModel() { LineOne = plugin });
            }
        }

        /// <summary>
        /// Refreshes the social list
        /// </summary>
        public void RefreshSocialListView()
        {
            this.Social.Add(new ItemViewModel() { LineOne = "Twitter" });
            this.Social.Add(new ItemViewModel() { LineOne = "Instagram" });
            this.Social.Add(new ItemViewModel() { LineOne = "Facebook" });
            this.Social.Add(new ItemViewModel() { LineOne = "Modesty Forums" });
            this.Social.Add(new ItemViewModel() { LineOne = "PlanetMinecraft" });
            this.Social.Add(new ItemViewModel() { LineOne = "Minecraftservers.org" });
            this.Social.Add(new ItemViewModel() { LineOne = "Minecraft Servers List" });
        }

        /// <summary>
        /// Property changed event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notification handler when a property has changed
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}