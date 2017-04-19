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

    public partial class Templates : ContentPage
    {
        private List<Post> textFinal;
        private string textReplace;
        private string textReplaceTitle;

        StackLayout parent = null;

        public Templates()
        {
            parent = new StackLayout();

            var scrollView = new ScrollView { Content = parent };
            Content = scrollView;
        }

        protected override async void OnAppearing()
        {
            var client = new HttpClient(new NativeMessageHandler());
            var content = await client.GetStringAsync("http://mechatronika.cool/noviny/wp-json/wp/v2/pages");
            var myData = JsonConvert.DeserializeObject<List<Post>>(content).Where(Post => Post.id == 57);
            textFinal = new List<Post>(myData);
            textReplace = textFinal[0].content.rendered.ToString();
            textReplaceTitle = textFinal[0].title.rendered.ToString();
            string textonLabel = Regex.Replace(textReplace, "<.*?>", String.Empty);
            textonLabel = Regex.Replace(textonLabel, "&#8211", String.Empty);

            Title = textReplaceTitle;
   
            Label title = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                Text = textonLabel,
                TextColor = Color.Black,
                Margin = new Thickness(10, 10, 10, 5),
            };
           
            parent.Children.Add(title);

            base.OnAppearing();
        }

    }
}
