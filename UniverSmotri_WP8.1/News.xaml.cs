using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UniverSmotri_WP8._1.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
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
    public sealed partial class News : Page
    {
        //private ViewModel.ViewModel vm;
        //private int nextPage;
        private ViewModel.NewsViewModel newsVM;
        public News()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }


        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
            
            if (e.NavigationMode == NavigationMode.Back)
                e = null;

            if (e != null && e.Parameter != null)
            {
                string param = e.Parameter.ToString();

                newsVM = new ViewModel.NewsViewModel(param);
                DataContext = newsVM;
                ListView_News.ItemsSource = newsVM;
                e = null;
            }
        }

        

        private void ListView_News_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListView_News.SelectedItem == null)
                return;
            NewsModel nm = (NewsModel)ListView_News.SelectedItem;
            string[] arr = new string[3];
            arr[0] = nm.NewsHeader;
            arr[1] = nm.DateNews;
            arr[2] = nm.Href;

            ListView_News.SelectedItem = null;
            Frame.Navigate(typeof(NewsElement_V2), arr);

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
