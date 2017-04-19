
using System;
using Xamarin.Forms;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class PlanCS : ContentPage
    {
        List<string> listEvent = new List<string>();
        List<string> listDate = new List<string>();
        List<string> listType = new List<string>();
        StackLayout parent = null;

        public PlanCS()
        {

            parent = new StackLayout();
            Padding = new Thickness(10, 10, 0, 10);
            Title = "Harmonogram štúdia";
            var scrollView = new ScrollView { Content = parent };
            Content = scrollView;

            var editor = new Label { Text = "loading...", HeightRequest = 300 };

            var assembly = typeof(PlanCS).GetTypeInfo().Assembly;

            Stream stream = assembly.GetManifestResourceStream("App1.HS.csv");

            if (Device.OS == TargetPlatform.Android)
            {
                using (var reader = new System.IO.StreamReader(stream, Encoding.GetEncoding("Windows-1250"), true))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');

                        listEvent.Add(values[0]);
                        listDate.Add(values[1]);
                        listType.Add(values[2]);
                    }
                }
            };

            if (Device.OS == TargetPlatform.Windows)
            {
                using (var reader = new System.IO.StreamReader(stream, Encoding.GetEncoding("latin1"), true))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');

                        listEvent.Add(values[0]);
                        listDate.Add(values[1]);
                        listType.Add(values[2]);
                    }
                }
                /*
                Label title = new Label
                {
                    Text = "''Windows-1250' is not a supported encoding name by Windows :(  ! Reallly ? :D",
                };
                parent.Children.Add(title);
                */
            };

            int lenth = listEvent.Count;

            for (int i = 0; i < lenth; i++) { 
            Label title = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Text = listEvent[i],
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };
            parent.Children.Add(title);

            Label title2 = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Text = listType[i],
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Green,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };
            parent.Children.Add(title2);

            Label title3 = new Label
            {
                 HorizontalOptions = LayoutOptions.EndAndExpand,
                 VerticalOptions = LayoutOptions.Center,
                 Text = listDate[i],
                 FontAttributes = FontAttributes.Bold,
                 TextColor = Color.Red,
                 FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
             };
             parent.Children.Add(title3);

            }

        }
    }
}
