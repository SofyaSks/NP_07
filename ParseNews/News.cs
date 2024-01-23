using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ParseNews
{
    internal class News
    {
        public string Title { get; }
        public Uri Href { get; }
        public ImageSource ImageSource { get; }
        public int Rating { get; }
        public int Comments { get; }

        public News(string title, Uri href, ImageSource imageSource, int rating, int comments)
        {
            Title = title;
            Href = href;
            ImageSource = imageSource;
            Rating = rating;
            Comments = comments;
        }
    }
}
