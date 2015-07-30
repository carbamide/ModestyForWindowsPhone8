using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Modesty2.ViewModels;
using Microsoft.Phone.Tasks;

namespace Modesty2
{
    public partial class PluginWindow : PhoneApplicationPage
    {
        /// <summary>
        /// Static private strings that hold urls for help for each of the user-visable plugins
        /// </summary>
        private static String kXpBanker = "http://dev.bukkit.org/bukkit-plugins/xpbanker/";
        private static String kCaptureCraft = "http://dev.bukkit.org/bukkit-plugins/capture-craft/";
        private static String kDisguiseCraft = "http://dev.bukkit.org/bukkit-plugins/disguisecraft/";
        private static String kMcmmo = "http://dev.bukkit.org/bukkit-plugins/mcmmo/";
        private static String kModestyHomepage = "http://www.minecraftmodesty.enjin.com";

        /// <summary>
        /// Constructor
        /// </summary>
        public PluginWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Selection handler for the Plugins list
        /// </summary>
        /// <param name="sender">The caller of this method</param>
        /// <param name="e">The event that took place</param>
        private void PluginsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector listSelector = sender as LongListSelector;

            if (listSelector.SelectedItem == null)
            {
                return;
            }

            ItemViewModel viewModel = (ItemViewModel)listSelector.SelectedItem;
            String viewModelLineOne = viewModel.LineOne;
            WebBrowserTask webBrowserTask = new WebBrowserTask();

            if (viewModelLineOne.ToLower().Contains("capturecraft"))
            {
                webBrowserTask.Uri = new Uri(kCaptureCraft);
                webBrowserTask.Show();
            }
            else if (viewModelLineOne.ToLower().Contains("disguisecraft"))
            {
                webBrowserTask.Uri = new Uri(kDisguiseCraft);
                webBrowserTask.Show();
            }
            else if (viewModelLineOne.ToLower().Contains("mcmmo"))
            {
                webBrowserTask.Uri = new Uri(kMcmmo);
                webBrowserTask.Show();
            }
            else if (viewModelLineOne.ToLower().Contains("xpbanker"))
            {
                webBrowserTask.Uri = new Uri(kXpBanker);
                webBrowserTask.Show();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Part of what makes Modesty so great is the secret sauce of plugins that have created such a great environment for us to enjoy!  Come check it out!", "Secret Sauce!", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    webBrowserTask.Uri = new Uri(kModestyHomepage);
                    webBrowserTask.Show();
                }
            }

            listSelector.SelectedItem = null;
        }
    }
}