using MyToolkit.Multimedia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UniverSmotri_WP8._1.Model;
using UniverSmotri_WP8._1.Parser;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Popups;
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
    public sealed partial class NewsElement_V2 : Page
    {

        public NewsElement_V2()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait | DisplayOrientations.Landscape | DisplayOrientations.LandscapeFlipped;
            if (e.Parameter != null)
            {
                DisplayInformation.DisplayContentsInvalidated += DisplayInformation_DisplayContentsInvalidated;
                string[] param = (string[])e.Parameter;
                HeaderNews.Text = param[0];
                DateNews.Text = param[1];
                OneNews oneNews = await NewsElementParser.Parse(param[2]);
                DescrNews.Text = oneNews.Description;

                YouTubeQuality quality = YouTubeQuality.Quality360P;
                if (Settings.Settings.QualityYouTubeVideo != null)
                {
                    switch (Settings.Settings.QualityYouTubeVideo)
                    {
                        case "144p":
                            quality = YouTubeQuality.Quality144P;
                            break;
                        case "240p":
                            quality = YouTubeQuality.Quality240P;
                            break;
                        case "360p":
                            quality = YouTubeQuality.Quality360P;
                            break;
                        case "480p":
                            quality = YouTubeQuality.Quality480P;
                            break;
                        case "720p":
                            quality = YouTubeQuality.Quality720P;
                            break;
                        case "1080p":
                            quality = YouTubeQuality.Quality1080P;
                            break;
                    }
                }
                YouTubeUri videoUri = null;
                bool error = false;
                try
                {
                    videoUri = await YouTube.GetVideoUriAsync(oneNews.YouTubeID, quality);
                }
                catch (YouTubeUriNotFoundException)
                {
                    error = true;
                }

                if (!error && videoUri != null)
                    playerYouTube.Source = videoUri.Uri;
                else
                {
                    var dialog = new MessageDialog("Не удалось загрузить видео. \nПопробуйте поменять качество в настройках.");
                    await dialog.ShowAsync();
                }
            }
            else
            {
                if (Frame.CanGoBack)
                    Frame.GoBack();
            }
        }

        private void DisplayInformation_DisplayContentsInvalidated(DisplayInformation sender, object args)
        {
            if (DisplayInformation.GetForCurrentView().CurrentOrientation == DisplayOrientations.Landscape || DisplayInformation.GetForCurrentView().CurrentOrientation == DisplayOrientations.LandscapeFlipped)
            {
                DateNews.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                DescrNews.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                GridContent.RowDefinitions[1].Height = GridLength.Auto;
                HeaderNews.Width = 500;
            }
            if (DisplayInformation.GetForCurrentView().CurrentOrientation == DisplayOrientations.Portrait)
            {
                HeaderNews.Width = 260;
                GridContent.RowDefinitions[1].Height = new GridLength(320);
                DateNews.Visibility = Windows.UI.Xaml.Visibility.Visible;
                DescrNews.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void playerYouTube_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            playerYouTube.IsFullWindow = !playerYouTube.IsFullWindow;
        }
    }
}
