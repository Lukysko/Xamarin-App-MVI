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

    public class SoftverTwo : ContentPage
    {

        StackLayout parent = null;
        List<Button> but = new List<Button>();
        private string mainTitle;
        private string mainContent;
        private List<GetSoftver> mainTitleFinal;
        private List<string> labelText = new List<string>();


        public class GetSoftver
        {
            public int id { get; set; }
            public List<int> categories { get; set; }
            public Content content { get; set; }
            public Title title { get; set; }
        }

        public SoftverTwo()
        {
            parent = new StackLayout();

            var scrollView = new ScrollView { Content = parent };
            Content = scrollView;
        }

        protected override async void OnAppearing()
        {
            var clientMainTitle = new HttpClient(new NativeMessageHandler());
            var contentMainTitle = await clientMainTitle.GetStringAsync("http://mechatronika.cool/noviny/wp-json/wp/v2/pages");

            // Spraviť obsah osftveru do content page  noveho
            var myDataMainTitle = JsonConvert.DeserializeObject<List<GetSoftver>>(contentMainTitle).Where(GetSoftver => GetSoftver.id == 83);
            mainTitleFinal = new List<GetSoftver>(myDataMainTitle);
            mainTitle = mainTitleFinal[0].title.rendered.ToString();
            Title = mainTitle;
            mainContent = mainTitleFinal[0].content.rendered.ToString();


            //Zobrazenie html formatu v content page
            string htmlText = mainContent.Replace(@"\", string.Empty);
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


