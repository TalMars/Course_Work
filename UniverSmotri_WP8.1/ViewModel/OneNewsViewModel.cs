using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UniverSmotri_WP8._1.Model;
using UniverSmotri_WP8._1.Parser;

namespace UniverSmotri_WP8._1.ViewModel
{
    public class OneNewsViewModel : INotifyPropertyChanged
    {
        private OneNews _oneNews;

        public OneNews OneNews
        {
            get { return _oneNews; }
            set { _oneNews = value; OnPropertyChanged(); }
        }

        public OneNewsViewModel()
        {

        }

        public OneNewsViewModel(string htmlUrl)
        {
            LoadNews(htmlUrl);
        }
        public async void LoadNews(string htmlUrl)
        {
            OneNews = await NewsElementParser.Parse(htmlUrl);
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
