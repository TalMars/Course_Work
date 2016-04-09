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
using Windows.Media;
using UniverSmotri_WP8._1.Model;
using UniverSmotri_WP8._1.Parser;
using UniverSmotri_WP8._1.ViewModel;
using Windows.Graphics.Display;

// Документацию по шаблону элемента пустой страницы см. по адресу http://go.microsoft.com/fwlink/?LinkID=390556

namespace UniverSmotri_WP8._1
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class UniverSmotri_TV : Page
    {
        private TV_ViewModel vm;
        private string kfuTvUrl = "http://cdn.universmotri.ru/live/univer_kfu2/playlist.m3u8";
        private string univerSmotriUrl_Low = "http://cdn.universmotri.ru/live/lq.sdp/playlist.m3u8";
        private string univerSmotriUrl_Medium = "http://cdn.universmotri.ru/live/mq.sdp/playlist.m3u8";
        private string univerSmotriUrl_High = "http://cdn.universmotri.ru/live/hq.sdp/playlist.m3u8";
        private string epgKFU = "http://tv.kpfu.ru/epg_kpfu";
        private string epgUniverSmotri = "http://tv.kpfu.ru/epg_universmotri";
        private string tvUrl;
        private string epg;
        public UniverSmotri_TV()
        {
            this.InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                if ((bool)e.Parameter)
                {
                    epg = epgUniverSmotri;
                    string qualityStream = "";
                    if (Settings.Settings.QualityStreamVideo != null)
                    {
                        switch (Settings.Settings.QualityStreamVideo)
                        {
                            case "Низкое": qualityStream = univerSmotriUrl_Low; break;
                            case "Среднее": qualityStream = univerSmotriUrl_Medium; break;
                            case "Высокое": qualityStream = univerSmotriUrl_High; break;
                        }
                    }
                    else
                    {
                        qualityStream = univerSmotriUrl_Low;
                    }
                    tvUrl = qualityStream;
                }
                else
                {
                    epg = epgKFU;
                    tvUrl = kfuTvUrl;
                }
                DisplayInformation.DisplayContentsInvalidated += DisplayInformation_DisplayContentsInvalidated;
                vm = new TV_ViewModel(epg);
                mPlayer.Source = new Uri(tvUrl);
                DataContext = vm;
            }
            else
            {
                Frame.GoBack();
            }
        }

        private void DisplayInformation_DisplayContentsInvalidated(DisplayInformation sender, object args)
        {
            if (DisplayInformation.GetForCurrentView().CurrentOrientation == DisplayOrientations.Landscape || DisplayInformation.GetForCurrentView().CurrentOrientation == DisplayOrientations.LandscapeFlipped)
            {
                ProgramList.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                GridContent.RowDefinitions[1].Height = GridLength.Auto;
            }
            if (DisplayInformation.GetForCurrentView().CurrentOrientation == DisplayOrientations.Portrait)
            {
                ProgramList.Visibility = Windows.UI.Xaml.Visibility.Visible;
                GridContent.RowDefinitions[1].Height = new GridLength(320);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void mPlayer_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            mPlayer.IsFullWindow = !mPlayer.IsFullWindow;

        }


    }
}
