using System;
using Xamarin.Forms;


namespace App1
{
    public class CreateRemind : ContentPage
    {
        StackLayout parent = null;

        private int timeR;
        private int dayR;
        private int monthR;
        private string descriptionR;

        private int timeO;
        private int dayO;
        private int monthO;
        private string descriptionO;

        Image picture;

        public CreateRemind(string predmet,string rt_dat, string rt_cas ,string rt_miest, string ot_dat, string ot_cas,string ot_miest) {

            parent = new StackLayout();
            Padding = new Thickness(10, 10, 0, 10);
            Title = "Potrobnosti";

            var scrollView = new ScrollView { Content = parent };
            Content = scrollView;

            Label label = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Navy,
                Text = predmet,
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };

            Label label1 = new Label {
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black,
                Text = "Riadny dátum "+ rt_dat + "\n" + "Riadny čas " + rt_cas + "\n" +
                "Miestnosť " + rt_miest  ,
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };

            Label label2 = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Purple,
                Text = "Opravný dátum " + ot_dat + "\n" + "Opravný čas " + ot_cas + "\n" +
                "Miestnosť " + ot_miest,
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };

            //Button riadny termin pripomienka
            Button buttonR = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.White,
                Text = "Vytvor pripomienku pre riadny termin.",
                IsVisible = true,
            };

            //Osetrenie zleho casu
            if (rt_cas == "dohodou" || rt_cas == "1+2" || rt_cas == "123" || rt_cas == "1234" || rt_cas == "")
            {
                buttonR.IsVisible = false;
            }
            else
            {
                timeR = Int32.Parse(rt_cas);
            }
            //Osetrenie zleho datumu 
            if (rt_dat == "dohodou" || rt_dat == "")
            {
                buttonR.IsVisible = false;
            }
            else
            {
                //string na den a mesiac
                string[] subStrings = rt_dat.Split('.');
                dayR = Int32.Parse(subStrings[0]);
                monthR = Int32.Parse(subStrings[1]);
            }

            // Miestnost skusky
            descriptionR = rt_miest;

            buttonR.Clicked += (sender, args) =>
            {
                // Pracujeme sa Android
                if (Device.OS == TargetPlatform.Android)
                {
                    DependencyService.Get<InterfaceForAndroid>().setEventCalendar(predmet,monthO,dayO,timeO,descriptionO);
                };
                buttonR.Text = "Pripomienka vytvorená.";
                picture.IsVisible = true;
            };

            //Button opravny termin pripomienka
            Button buttonO = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.White,
                Text = "Vytvor pripomienku pre opravný termin.",
            };

            //Osetrenie zleho casu
            if (ot_cas == "dohodou" || ot_cas == "1+2" || ot_cas == "123" || ot_cas == "1234" || ot_cas == "")
            {
                buttonO.IsVisible = false;
            }
            else
            {
                timeO = Int32.Parse(ot_cas);
            }
            //Osetrenie zleho datumu 
            if (ot_dat == "dohodou" || ot_dat == "")
            {
                buttonR.IsVisible = false;
            }
            else
            {
                //string na den a mesiac
                string[] subStrings = ot_dat.Split('.');
                dayO = Int32.Parse(subStrings[0]);
                monthO = Int32.Parse(subStrings[1]);
            }

            // Miestnost skusky
            descriptionO = ot_miest;

            buttonO.Clicked += (sender, args) =>
            {
                if (Device.OS == TargetPlatform.Android)
                {
                     DependencyService.Get<InterfaceForAndroid>().setEventCalendar(predmet, monthR, dayR, timeR, descriptionR);
                };
                buttonO.Text = "Pripomienka vytvorená.";
                picture.IsVisible = true;
            };

            picture = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                Source = "remind.png",
                IsVisible = false,
            };

            parent.Children.Add(label);
            parent.Children.Add(label1);
            parent.Children.Add(label2);
            parent.Children.Add(buttonR);
            parent.Children.Add(buttonO);
            parent.Children.Add(picture);
        }
    }
}
