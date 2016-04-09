using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента пустой страницы см. по адресу http://go.microsoft.com/fwlink/?LinkID=390556

namespace UniverSmotri_WP8._1
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Settings.Settings.QualityYouTubeVideo != null)
            {
                int index = 2;
                switch (Settings.Settings.QualityYouTubeVideo)
                {
                    case "144p":
                        index = 0; break;
                    case "240p":
                        index = 1; break;
                    case "360p":
                        index = 2; break;
                    case "480p":
                        index = 3; break;
                    case "720p":
                        index = 4; break;
                    case "1080p":
                        index = 5; break;
                }
                CB_QuailtyYT.SelectedIndex = index;
            }
            if (Settings.Settings.QualityStreamVideo != null)
            {
                int index = 0;
                switch (Settings.Settings.QualityStreamVideo)
                {
                    case "Низкое": index = 0; break;
                    case "Среднее": index = 1; break;
                    case "Высокое": index = 2; break;
                }
                CB_QualityStream.SelectedIndex = index;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void CB_QualityStream_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem == null)
                return;

            string quality = ((sender as ComboBox).SelectedItem as TextBlock).Text;
            Settings.Settings.QualityStreamVideo = quality;
        }

        private void CB_QuailtyYT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem == null)
                return;

            string quality = ((sender as ComboBox).SelectedItem as TextBlock).Text;
            Settings.Settings.QualityYouTubeVideo = quality;
            //(sender as ComboBox).SelectedItem = null;

        }
    }
}
