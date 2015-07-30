using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Modesty2
{
    public partial class StaffWindow : PhoneApplicationPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public StaffWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Since we load these on demand, we need to instantiate a ProgressIndictor and show it when
        /// this view is loaded.  This is the OnLoad handler
        /// </summary>
        /// <param name="sender">The caller of this method</param>
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
            progressIndicator.Text = "Accessing Staff Information...";
            progressIndicator.IsIndeterminate = true;
            progressIndicator.IsVisible = true;
        }
    }
}