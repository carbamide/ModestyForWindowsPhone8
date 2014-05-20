using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Modesty2.ViewModels
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Private holder for LineOne
        /// </summary>
        private string _lineOne;

        /// <summary>
        /// LineOne setter and getter
        /// </summary>
        public string LineOne
        {
            get
            {
                return _lineOne;
            }
            set
            {
                if (value != _lineOne)
                {
                    _lineOne = value;
                    NotifyPropertyChanged("LineOne");
                }
            }
        }

        /// <summary>
        /// Private holder for LineTwo
        /// </summary>
        private string _lineTwo;

        /// <summary>
        /// LineTwo setter and getter
        /// </summary>
        public string LineTwo
        {
            get
            {
                return _lineTwo;
            }
            set
            {
                if (value != _lineTwo)
                {
                    _lineTwo = value;
                    NotifyPropertyChanged("LineTwo");
                }
            }
        }

        /// <summary>
        /// Private holder for LineThree
        /// </summary>
        private string _lineThree;

        /// <summary>
        /// LineThree setter and getter
        /// </summary>
        public string LineThree
        {
            get
            {
                return _lineThree;
            }
            set
            {
                if (value != _lineThree)
                {
                    _lineThree = value;
                    NotifyPropertyChanged("LineThree");
                }
            }
        }

        /// <summary>
        /// Private holder for AvatarUrl
        /// </summary>
        private string _avatarUrl;

        /// <summary>
        /// AvatarUrl setter and getter
        /// </summary>
        public string AvatarUrl
        {
            get
            {
                return _avatarUrl;
            }
            set
            {
                if (value != _avatarUrl)
                {
                    _avatarUrl = value;
                    NotifyPropertyChanged("AvatarUrl");
                }
            }
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