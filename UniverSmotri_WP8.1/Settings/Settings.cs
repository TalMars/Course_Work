using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UniverSmotri_WP8._1.Settings
{
    public static class Settings
    {
        private static string _qualityStreamVideo;

        public static string QualityStreamVideo
        {
            get { return Settings._qualityStreamVideo; }
            set { Settings._qualityStreamVideo = value; }
        }

        private static string _qualityYouTubeVideo;

        public static string QualityYouTubeVideo
        {
            get { return Settings._qualityYouTubeVideo; }
            set { Settings._qualityYouTubeVideo = value; }
        }

    }
}
