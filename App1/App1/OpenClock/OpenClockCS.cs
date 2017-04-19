using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ModernHttpClient;
using System.Text.RegularExpressions;

namespace App1
{
    public class Title
    {
        public string rendered { get; set; }
    }

    public class Content
    {
        public string rendered { get; set; }
        public bool @protected { get; set; }
    }

    public class Post
    {
        public int id { get; set; }
        public Title title { get; set; }
        public Content content { get; set; }
    }

    

    public partial class OpenClockCS : ContentPage
    {
        private List<Post> textFinal;
        private string textReplace;
        private string textReplaceTitle;

        StackLayout parent = null;

        public OpenClockCS()
        {
            parent = new StackLayout();
            Title = "Otváracie hodiny";
            var scrollView = new ScrollView { Content = parent };
            Content = scrollView;
        }

        protected override async void OnAppearing()
        {
            var client = new HttpClient(new NativeMessageHandler());
            var content = await client.GetStringAsync("http://mechatronika.cool/noviny/wp-json/wp/v2/pages");
            var myData = JsonConvert.DeserializeObject<List<Post>>(content).Where(Post => Post.id == 61);  
            textFinal = new List<Post>(myData);
            textReplace = textFinal[0].content.rendered.ToString();
            textReplaceTitle = textFinal[0].title.rendered.ToString();

            //Zobrazenie html formatu v content page
            string htmlText = textReplace.Replace(@"\", string.Empty);
            var browser = new WebView();
            Content = browser;
            var html = new HtmlWebViewSource
            {
                Html = htmlText
            };
            browser.Source = html;
            base.OnAppearing();
        }





    }
}
