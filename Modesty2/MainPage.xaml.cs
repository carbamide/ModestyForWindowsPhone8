using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Modesty2.ViewModels;
using Modesty2.Models;

namespace Modesty2
{
    public partial class MainPage : PhoneApplicationPage
    {
        /// <summary>
        /// Private static variables that hold constants for all the URLs for the Social list
        /// </summary>
        private static String kTwitterURL = "https://twitter.com/modesty_mc";
        private static String kInstagramURL = "http://instagram.com/degumaster";
        private static String kPlanetMinecraft = "http://www.planetminecraft.com/server/modesty/";
        private static String kMinecraftServersOrg = "http://minecraftservers.org/server/6465";
        private static String kMinecraftServerList = "http://minecraft-server-list.com/server/128633/vote/";
        private static String kFacebookURL = "https://www.facebook.com/minecraftmodesty";
        private static String kForumURL = "http://www.minecraftmodesty.enjin.com/forum";

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }

        /// <summary>
        /// On Load event handler
        /// </summary>
        /// <param name="sender">The caller of this action</param>
        /// <param name="e">The event that took place</param>
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            var progressIndicator = SystemTray.ProgressIndicator;

            if (progressIndicator != null)
            {
                return;
            }

            progressIndicator = new ProgressIndicator();

            SystemTray.SetProgressIndicator(this, progressIndicator);
            progressIndicator.Text = "Accessing Modesty Information...";
            progressIndicator.IsIndeterminate = true;
            progressIndicator.IsVisible = true;
        }

        /// <summary>
        /// On navigation refresh the Social List View
        /// </summary>
        /// <param name="e">The event that took place</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.ViewModel.RefreshSocialListView();
        }

        /// <summary>
        /// Selection handler for the Social list
        /// </summary>
        /// <param name="sender">The caller of this method</param>
        /// <param name="e">The event that took place</param>
        private void SocialSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector listSelector = sender as LongListSelector;

            if (listSelector.SelectedItem == null)
            {
                return;
            }

            ItemViewModel viewModel = (ItemViewModel)listSelector.SelectedItem;
            String viewModelLineOne = viewModel.LineOne;

            WebBrowserTask webBrowserTask = new WebBrowserTask();

            if (viewModelLineOne.Equals("Twitter"))
            {
                webBrowserTask.Uri = new Uri(kTwitterURL);
                webBrowserTask.Show();
            }
            else if (viewModelLineOne.Equals("Instagram"))
            {
                webBrowserTask.Uri = new Uri(kInstagramURL);
                webBrowserTask.Show();
            }
            else if (viewModelLineOne.Equals("Facebook"))
            {
                webBrowserTask.Uri = new Uri(kFacebookURL);
                webBrowserTask.Show();
            }
            else if (viewModelLineOne.Equals("Modesty Forums"))
            {
                webBrowserTask.Uri = new Uri(kForumURL);
                webBrowserTask.Show();
            }
            else if (viewModelLineOne.Equals("PlanetMinecraft"))
            {
                webBrowserTask.Uri = new Uri(kPlanetMinecraft);
                webBrowserTask.Show();
            }
            else if (viewModelLineOne.Equals("Minecraftservers.org"))
            {
                webBrowserTask.Uri = new Uri(kMinecraftServersOrg);
                webBrowserTask.Show();
            }
            else if (viewModelLineOne.Equals("Minecraft Servers List"))
            {
                webBrowserTask.Uri = new Uri(kMinecraftServerList);
                webBrowserTask.Show();
            }

            listSelector.SelectedItem = null;
        }

        /// <summary>
        /// Selection handler for the Info list 
        /// </summary>
        /// <param name="sender">The caller of this method</param>
        /// <param name="e">The event that took place</param>
        private void InfoSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector listSelector = sender as LongListSelector;

            if (listSelector.SelectedItem == null)
            {
                return;
            }

            ItemViewModel viewModel = (ItemViewModel)listSelector.SelectedItem;
            String viewModelLineOne = viewModel.LineOne;

            if (viewModelLineOne.Equals("players"))
            {
                PanoramaItem panItem = (PanoramaItem)panoramaControl.Items[1];

                panoramaControl.SetValue(Panorama.SelectedItemProperty, panItem);

                Panorama tempPanorama = panoramaControl;

                LayoutRoot.Children.Remove(panoramaControl);
                LayoutRoot.Children.Add(tempPanorama);

                LayoutRoot.UpdateLayout();
            }
            else if (viewModelLineOne.Equals("staff"))
            {
                NavigationService.Navigate(new Uri("/StaffWindow.xaml", UriKind.Relative));
            }
            else if (viewModelLineOne.Equals("plugins"))
            {
                NavigationService.Navigate(new Uri("/PluginWindow.xaml", UriKind.Relative));
            }
            else
            {
                Clipboard.SetText("108.174.48.200:25665");

                MessageBox.Show("The server address and port have been copied to your clipboard.  You can now paste the address anywhere you like.", "Copied", MessageBoxButton.OK);
            }

            listSelector.SelectedItem = null;
        }
    }
}