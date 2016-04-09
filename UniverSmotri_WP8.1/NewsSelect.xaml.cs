using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class NewsSelect : Page
    {
        public NewsSelect()
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
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
        }

        private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListSelectNews.SelectedItem == null)
                return;
            TextBlock tb = (TextBlock)ListSelectNews.SelectedItem; /*(ListSelectNews.SelectedItem as Border).Child as TextBlock;*/
            switch (tb.Text)
            {
                case "Все новости":
                    ListSelectNews.SelectedItem = null;
                    Frame.Navigate(typeof(News), "http://tv.kpfu.ru/index.php/novosti.html1");
                    break;
                case "Обзоры новостей":
                    ListSelectNews.SelectedItem = null;
                    Frame.Navigate(typeof(News), "http://tv.kpfu.ru/index.php/novosti/obzory-novostey.html0"); 
                    break;
                case "Новости ректората":
                    ListSelectNews.SelectedItem = null;
                    Frame.Navigate(typeof(News), "http://tv.kpfu.ru/index.php/novosti/novosti-rektorata.html0"); 
                    break;
                case "Общество и культура":
                    ListSelectNews.SelectedItem = null;
                    Frame.Navigate(typeof(News), "http://tv.kpfu.ru/index.php/novosti/obschestvo-i-kultura.html0"); 
                    break;
                case "Образование и наука":
                    ListSelectNews.SelectedItem = null;
                    Frame.Navigate(typeof(News), "http://tv.kpfu.ru/index.php/novosti/obrazovanie-i-nauka.html0"); 
                    break;
                case "Спорт":
                    ListSelectNews.SelectedItem = null;
                    Frame.Navigate(typeof(News), "http://tv.kpfu.ru/index.php/novosti/sport.html0"); 
                    break;
                default:
                    var dialog = new MessageDialog("error");
                    await dialog.ShowAsync();
                    break;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
