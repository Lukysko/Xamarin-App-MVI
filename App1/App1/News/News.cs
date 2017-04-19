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
    // Hlavna trieda - novinky
    public class News : ContentPage
    {
        StackLayout parent = null;
        List<Button> but = new List<Button>();
        private string mainTitle;
        private List<GetMainTitle> mainTitleFinal;
        private List<string> headerText = new List<string>();

        public class GetMainTitle
        {
            public string name { get; set; }
            public int id { get; set; }
        }

        public News()
        {            
            parent = new StackLayout();
            Padding = new Thickness(10, 10, 0, 10);
            Title = "Novinky";
            var scrollView = new ScrollView { Content = parent };
            Content = scrollView;
            createButton();
        }

        // Ziskanie nadpisov kategorii 
        public void setText(string contentMainTitle,int id) {
            var myDataMainTitle = JsonConvert.DeserializeObject<List<GetMainTitle>>(contentMainTitle).Where(GetMainTitle => GetMainTitle.id == id);
            mainTitleFinal = new List<GetMainTitle>(myDataMainTitle);
            mainTitle = mainTitleFinal[0].name.ToString();
            headerText.Add(mainTitle);
        }
       
        //Vytvorneie button-ov s obsahom ziskanim zo stranky
        private async void createButton() {
            var clientMainTitle = new HttpClient(new NativeMessageHandler());
            var contentMainTitle = await clientMainTitle.GetStringAsync("http://mechatronika.cool/noviny/wp-json/wp/v2/categories");

            setText(contentMainTitle,1);
            setText(contentMainTitle, 8);
            setText(contentMainTitle, 13);
            setText(contentMainTitle, 14);

            but.Add(setButton(1, headerText[0]));
            but.Add(setButton(2, headerText[1]));
            but.Add(setButton(3, headerText[2]));
            but.Add(setButton(4, headerText[3]));
        }

        //Nastavi parametre button-ov
        public Button setButton(int param, string header) {

            string image = "";
            if (param == 1)
            {
                image = "famous.png";
            }
            else if (param == 2)
            {

                image = "message.png";
            }
            else if (param == 3)
            {
                image = "tip.png";
            }
            else if (param == 4)
            {
                image = "techincal.png";
            }

            Button button = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.White,
                Text = header,
                Image = image,
                WidthRequest = 350,
                HeightRequest = 250,

            };

            button.Clicked += (sender, args) =>
            {
                if (param == 1) {
                    Navigation.PushAsync(new SectionOne());
                } else if (param == 2)
                {
                   Navigation.PushAsync(new SectionTwo());
                }
                else if (param == 3)
                {
                    Navigation.PushAsync(new SectionThree());
                }
                else if (param == 4)
                {
                    Navigation.PushAsync(new SectionFour());
                    
                }
            };

            parent.Children.Add(button); 
            return button;
        }
    }
}
