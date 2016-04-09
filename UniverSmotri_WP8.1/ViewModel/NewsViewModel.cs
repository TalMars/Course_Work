using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UniverSmotri_WP8._1.Model;
using UniverSmotri_WP8._1.Parser;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace UniverSmotri_WP8._1.ViewModel
{
    public class NewsViewModel : IncrementalLoadingBase, INotifyPropertyChanged
    {

        private bool hasMoreItems;
        private int nextPage = 2;
        private uint _count = 0;
        private string mainUrl;
        private bool isJustNews;
        public NewsViewModel(string mainUrl)
        {
            hasMoreItems = true;
            isJustNews = mainUrl.Substring(mainUrl.Length - 1) == "1";
            this.mainUrl = mainUrl.Substring(0, mainUrl.Length - 1);
        }

        private Visibility _visible;

        public Visibility Visible
        {
            get { return _visible; }
            set { _visible = value; OnPropertyChanged(); }
        }

        protected async override Task<IList<object>> LoadMoreItemsOverrideAsync(System.Threading.CancellationToken c, uint count)
        {
            await CoreWindow.GetForCurrentThread().Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                Visible = Visibility.Visible;
            });
            string nextPageUrl = "";
            if (isJustNews)
                nextPageUrl = mainUrl.Substring(0, mainUrl.Length - 5) + "/Page-" + nextPage.ToString() + "-10.html";
            else
                nextPageUrl = mainUrl.Substring(0, mainUrl.Length - 5) + "/Page-" + nextPage.ToString() + "-30.html";

            List<NewsModel> list;

            //await Task.Delay(100);

            if (_count == 0)
                list = await HtmlParser.Parse(mainUrl);
            else
            {
                list = await HtmlParser.Parse(nextPageUrl);
                nextPage++;
            }

            //foreach (NewsModel i in list)
            //    val.Add((object)i);
            var values = from j in list
                         select (object)j;

            _count += UInt32.Parse(list.Count.ToString());
            await CoreWindow.GetForCurrentThread().Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                Visible = Visibility.Collapsed;
            });
            return values.ToArray();
        }

        protected override bool HasMoreItemsOverride()
        {
            return hasMoreItems;
        }

        public void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
