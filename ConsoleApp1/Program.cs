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
using System.Xml.Linq;
using System.Xml.XPath;



using HttpClient client = new HttpClient();

string html = await client.GetStringAsync("https://ru.wikipedia.org/wiki/%D0%97%D0%B0%D0%B3%D0%BB%D0%B0%D0%B2%D0%BD%D0%B0%D1%8F_%D1%81%D1%82%D1%80%D0%B0%D0%BD%D0%B8%D1%86%D0%B0");

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

IEnumerable<XElement> tiles = xml.XPathSelectElements("//ul/li");


foreach (XElement tile in tiles)
{
    //Console.WriteLine(tile);

    //textBox.Text += tile.ToString() + "\r\n\r\n";

    string title = ((string)tile.XPathSelectElement("b/a")!).Trim();

}

