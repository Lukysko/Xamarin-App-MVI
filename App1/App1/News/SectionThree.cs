using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ModernHttpClient;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;

namespace App1
{
    public class SectionThree : ContentPage
    {
        StackLayout parent = null;
        List<Button> but = new List<Button>();
        List<Label> lab = new List<Label>();
        List<int> categories;
        private string finalCategories;
        private string mainTitle;

        /// <summary>
        /// Texty z JSON
        /// </summary>
        private List<GetNews> textFinal;
        private List<GetMainTitle> mainTitleFinal;
        private string textReplace;
        private string textonLabel;
        private string textReplaceTitle;
        private String uri;

        /// <summary>
        /// Kategoria kontentu
        /// </summary>
        private int category = 13;

        /// <summary>
        /// Parametre z noviniek
        /// </summary>
        public class GetNews
        {
            public int id { get; set; }
            public List<int> categories { get; set; }
            public Content content { get; set; }
            public Title title { get; set; }
        }

        public class GetMainTitle
        {
            public string name { get; set; }
            public int id { get; set; }
        }

        public SectionThree()
        {
            //Vytvorenie dizajnu
            parent = new StackLayout();
            Padding = new Thickness(10, 10, 0, 10);

            var scrollView = new ScrollView { Content = parent };
            Content = scrollView;
        }

        protected override async void OnAppearing()
        {
            //Stiahnutie JSON
            var client = new HttpClient(new NativeMessageHandler());
            var content = await client.GetStringAsync("http://mechatronika.cool/noviny/wp-json/wp/v2/posts");
            var myData = JsonConvert.DeserializeObject<List<GetNews>>(content);

            var clientMainTitle = new HttpClient(new NativeMessageHandler());
            var contentMainTitle = await clientMainTitle.GetStringAsync("http://mechatronika.cool/noviny/wp-json/wp/v2/categories");
            var myDataMainTitle = JsonConvert.DeserializeObject<List<GetMainTitle>>(contentMainTitle).Where(GetMainTitle => GetMainTitle.id == category);

            mainTitleFinal = new List<GetMainTitle>(myDataMainTitle);
            mainTitle = mainTitleFinal[0].name.ToString();
            Title = mainTitle;

            textFinal = new List<GetNews>(myData);
            int lengthOfNews = textFinal.Count;

            //Vypis clankov
            for (int i = 0; i < lengthOfNews; i++)
            {
                categories = textFinal[i].categories;
                finalCategories = categories[0].ToString();
                if (Int32.Parse(finalCategories) == category)
                {
                    textReplace = textFinal[i].content.rendered.ToString();
                    textReplaceTitle = textFinal[i].title.rendered.ToString();
                    // Ziskanie url obrazka z JSON
                    List<Uri> link = FetchLinksFromSource(textReplace);
                    if (link.Count > 0)
                    {
                        uri = link[0].AbsoluteUri;
                    }
                    else
                    {
                        uri = "";
                    }
                    textonLabel = Regex.Replace(textReplace, "<.*?>", String.Empty);
                    textReplaceTitle = Regex.Replace(textReplaceTitle, "&#8211", String.Empty);
                    // Zlepenie komponentov a vytvorenie button-ov
                    but.Add(CreateView(i, textonLabel, textReplaceTitle, uri));
                }
            }
            base.OnAppearing();
        }

        /// <summary>
        /// Získanie url obrázku
        /// </summary>
        public List<Uri> FetchLinksFromSource(string htmlSource)
        {
            List<Uri> links = new List<Uri>();
            string regexImgSrc = @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";
            MatchCollection matchesImgSrc = Regex.Matches(htmlSource, regexImgSrc, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            foreach (Match m in matchesImgSrc)
            {
                string href = m.Groups[1].Value;
                links.Add(new Uri(href));
            }
            return links;
        }

        /// <summary>
        /// Vytvorenie content-u stranky
        /// </summary>
        /// <returns></returns>
        public Button CreateView(int i, string textOfReview, string textLabel, string uri)
        {

            int length = textOfReview.Length;
            string finalText = "";

            if (length > 150)
            {
                finalText = textOfReview.Substring(0, 150);
            }
            else
            {
                finalText = textOfReview;
            }


            Label title = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Text = textLabel,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Green,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };

            Label text = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black,
                Text = finalText,
            };

            parent.Children.Add(title);
            parent.Children.Add(text);

            if (uri != "")
            {

                var imageSource = new UriImageSource
                {
                    Uri = new Uri(uri)
                };

                Image picture = new Image
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Source = imageSource,
                };

                parent.Children.Add(picture);

            }

            Button button = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.White,
                Text = "Čítať ďalej ...",
            };

            button.IsVisible = false;

            if (length > 150)
            {
                button.IsVisible = true;
                parent.Children.Add(button);
            }

            button.Clicked += (sender, args) =>
            {
                button.IsVisible = false;
                text.Text = textOfReview;

            };

            return button;
        }


    }
}

