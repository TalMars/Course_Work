using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UniverSmotri_WP8._1.Parser;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UniverSmotri_WP8._1.ViewModel;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Core;
using UniverSmotri_WP8._1.Model;
using Windows.Media.Playback;
// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=391641

namespace UniverSmotri_WP8._1
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string urlRadio;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

        }


        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (urlRadio == null)
            {
                try
                {
                    urlRadio = await RadioUrlParser.Parse();
                } catch(AggregateException){
                    RadioPlayer.IsEnabled = false;
                }
            }
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (BackgroundMediaPlayer.Current.CurrentState == MediaPlayerState.Paused | BackgroundMediaPlayer.Current.CurrentState == MediaPlayerState.Closed)
                {
                    RadioPlayer.IsOn = false;
                }
            });
            BackgroundMediaPlayer.Current.CurrentStateChanged += radioPlayer_CurrentStateChanged;
        }

        async void radioPlayer_CurrentStateChanged(MediaPlayer sender, object args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    if (BackgroundMediaPlayer.Current.CurrentState == MediaPlayerState.Playing)
                    {
                        RadioPlayer.IsOn = true;
                    }
                    else if (BackgroundMediaPlayer.Current.CurrentState == MediaPlayerState.Paused)
                    {
                        RadioPlayer.IsOn = false;
                    }
                });

        }

        private void News_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewsSelect));
        }

        private async void USTV_Click(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                RadioPlayer.IsOn = false;
            });
            Frame.Navigate(typeof(UniverSmotri_TV), true);
        }

        private async void KFUTV_Click(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                RadioPlayer.IsOn = false;
            });
            Frame.Navigate(typeof(UniverSmotri_TV), false);
        }

        private async void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    if (toggleSwitch.IsOn)
                    {
                        if (BackgroundMediaPlayer.Current.CurrentState != MediaPlayerState.Playing)
                        {
                            var message = new ValueSet
                          {
                              {
                                  "Play",
                                  urlRadio
                              }
                          };
                            BackgroundMediaPlayer.SendMessageToBackground(message);
                        }
                    }
                    else
                    {
                        BackgroundMediaPlayer.Current.Pause();
                    }

                });
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

    }
}
