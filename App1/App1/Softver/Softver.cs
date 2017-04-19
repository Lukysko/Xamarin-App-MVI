using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernHttpClient;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;

namespace App1
{
    public class Softver : ContentPage
    {
        StackLayout parent = null;
        List<Button> but = new List<Button>();
        private string mainTitle;
        private List<GetSoftver> mainTitleFinal;
        private List<string> headerText = new List<string>();
        private List<string> labelText = new List<string>();


        public class GetSoftver
        {
            public int id { get; set; }
            public List<int> categories { get; set; }
            public Content content { get; set; }
            public Title title { get; set; }
        }

        public Softver()
        {
            parent = new StackLayout();
            Padding = new Thickness(10, 10, 0, 10);
            var scrollView = new ScrollView { Content = parent };
            Content = scrollView;
            createButton();
        }

        /// <summary>
        /// Ziskanie textu na vypis
        /// </summary>
        public void setText(string contentMainTitle, int id) {
            var myDataMainTitle = JsonConvert.DeserializeObject<List<GetSoftver>>(contentMainTitle).Where(GetSoftver => GetSoftver.id == id);
            mainTitleFinal = new List<GetSoftver>(myDataMainTitle);
            mainTitle = mainTitleFinal[0].title.rendered.ToString();
            headerText.Add(mainTitle);
        } 

        /// <summary>
        /// Vytvorenie button-ov
        /// </summary>
        private async void createButton()
        {
            var clientMainTitle = new HttpClient(new NativeMessageHandler());
            var contentMainTitle = await clientMainTitle.GetStringAsync("http://mechatronika.cool/noviny/wp-json/wp/v2/pages");

            setText(contentMainTitle,85);
            setText(contentMainTitle, 83);
            setText(contentMainTitle, 79);
            setText(contentMainTitle, 77);

            Title = headerText[3];
            but.Add(setButton(1, headerText[0]));
            but.Add(setButton(2, headerText[1]));
            but.Add(setButton(3, headerText[2]));

        }

        /// <summary>
        /// Nastavenie obsahu button-u
        /// </summary>
        public Button setButton(int param, string header)
        {

            string image = "";

            if (param == 1)
            {
                image = "fyz.png";
            }
            else if (param == 2)
            {
                image = "har.png";
            }
            else if (param == 3)
            {
                image = "inf.png";
            }

            Button button = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.White,
                Text = header,
                Image = image,
                WidthRequest = 350,
                HeightRequest = 150,

            };
            
           
            button.Clicked += (sender, args) =>
            {
                if (param == 1)
                {
                    Navigation.PushAsync(new SoftverOne());
                }
                else if (param == 2) {

                    Navigation.PushAsync(new SoftverTwo());
                }
                else if (param == 3)
                {
                    Navigation.PushAsync(new SoftverThree());
                }
            };

            parent.Children.Add(button);
            
            return button;
        }
    }
}
