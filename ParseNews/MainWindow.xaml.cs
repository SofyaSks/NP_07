using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ParseNews
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Regex numberRegex = new Regex(@"\d+");
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using HttpClient client = new();
            string html = await client.GetStringAsync("https://panorama.pub/science?page=1");

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            document.OptionOutputAsXml = true;

            XElement xml;
            using (StringWriter writer = new StringWriter())
            {
                document.Save(writer); // взять документ и записать в райтер
                string serialized = writer.ToString();

                using (StringReader reader = new StringReader(serialized))
                {
                    xml = XElement.Load(reader); //загрузить xml
                }
            }

            IEnumerable<XElement> tiles = xml.XPathSelectElements("//div[contains(@class,'grid')]/a");

            ICollection<News> news = new List<News>();

            foreach (XElement tile in tiles)
            {
                //textBox.Text += tile.ToString() + "\r\n\r\n";

                string title = ((string)tile.XPathSelectElement("div[3]")!).Trim();
                string href = ((string)tile.Attribute("href")!).Trim();
                string image = ((string)tile.XPathSelectElement("div[1]/img")!.Attribute("src")!).Trim();
                string ratingStr = ((string)tile.XPathSelectElement("div[2]/div")!).Trim();
                string commentsStr = ((string)tile.XPathSelectElement("div[2]/div[2]")!).Trim();

                int rating = int.Parse(numberRegex.Match(ratingStr).Value);
                int comments = int.Parse(numberRegex.Match(commentsStr).Value);

                news.Add(new News(title, new Uri("https://panorama.pub" + href), new BitmapImage(new Uri(image)),
                    rating, comments));

                /*textBox.Text += $@"{title} 
{href}
{image}
{ratingStr}
{commentsStr}

";*/

            }

            this.DataContext = news;
        }
    }
}
