using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        public ListView ListView { get { return listView; } }

        public MasterPage()
        {
            InitializeComponent();
            BackgroundColor = Color.White;

            var masterPageItems = new List<MasterPageItem>();
            Title = "Menu";
       
                masterPageItems.Add(new MasterPageItem
                {
                    Title = "Novinky",
                    IconSource = "home.png",
                    TargetType = typeof(News)
                });
            
                masterPageItems.Add(new MasterPageItem
                {
                    Title = "Šablóny",
                    IconSource = "template.png",
                    TargetType = typeof(Templates)
                });
                masterPageItems.Add(new MasterPageItem
                {
                    Title = "Otváracie hodiny objektov",
                    IconSource = "open.png",
                    TargetType = typeof(OpenClockCS)
                });
                masterPageItems.Add(new MasterPageItem
                {
                    Title = "Zdroje informácií",
                    IconSource = "info.png",
                    TargetType = typeof(Resource)
                });
                masterPageItems.Add(new MasterPageItem
                {
                    Title = "Softvér",
                    IconSource = "softver.png",
                    TargetType = typeof(Softver)
                });
                masterPageItems.Add(new MasterPageItem
                {
                    Title = "Harmonogram štúdia",
                    IconSource = "study.png",
                    TargetType = typeof(PlanCS)
                });

                masterPageItems.Add(new MasterPageItem
                {
                    Title = "Rozvrh skúšok",
                    IconSource = "exam.png",
                    TargetType = typeof(ExamCS)
                });

                masterPageItems.Add(new MasterPageItem
                {
                    Title = "Udalosti",
                    IconSource = "events.png",
                    TargetType = typeof(PlanCS)
                });


            listView.ItemsSource = masterPageItems;
            
        }
    }
}
