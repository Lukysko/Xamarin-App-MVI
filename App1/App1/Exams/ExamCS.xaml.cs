using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.IO;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExamCS : ContentPage
    {
        List<string> Predmet = new List<string>();
        List<string> RT_dat = new List<string>();
        List<string> RT_cas = new List<string>();
        List<string> RT_miest = new List<string>();
        List<string> OT_dat = new List<string>();
        List<string> OT_cas = new List<string>();
        List<string> OT_miest = new List<string>();

        private ObservableCollection<TSModel> _TSmodel;

        IEnumerable<TSModel> GetTSModel(string searchText = null)
        {
            if (String.IsNullOrWhiteSpace(searchText))
                return _TSmodel;
            var lowerWord = searchText.ToLower();
            return _TSmodel.Where(c => c.Predmet.ToLower().Contains(lowerWord));
        }

        public ExamCS()
        {
            InitializeComponent();

            Padding = new Thickness(5, 5, 5, 5);

            var assembly = typeof(PlanCS).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("App1.TS.csv");

            if (Device.OS == TargetPlatform.Android)
            {
            // Nacitanie obsahu z CSV do poli
            using (var reader = new System.IO.StreamReader(stream, Encoding.GetEncoding("Windows-1250"), true))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    Predmet.Add(values[2]);

                    RT_dat.Add(values[3]);
                    RT_cas.Add(values[4]);
                    RT_miest.Add(values[5]);

                    OT_dat.Add(values[6]);
                    OT_cas.Add(values[7]);
                    OT_miest.Add(values[8]);
                }
            }

            };

            if (Device.OS == TargetPlatform.Windows)
            {
                // Nacitanie obsahu z CSV do poli
                using (var reader = new System.IO.StreamReader(stream, Encoding.GetEncoding("latin1"), true))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');

                        Predmet.Add(values[2]);

                        RT_dat.Add(values[3]);
                        RT_cas.Add(values[4]);
                        RT_miest.Add(values[5]);

                        OT_dat.Add(values[6]);
                        OT_cas.Add(values[7]);
                        OT_miest.Add(values[8]);
                    }
                }

            };

            int length = Predmet.Count;

            // Naplnenie kolekcie TSModel udajmi
            _TSmodel = new ObservableCollection<TSModel>();
            for (int i = 0; i < length - 1; i++)
            {
                string PredmetSend = Predmet[i + 1];

                string RT_datSend = RT_dat[i + 1];
                string RT_casSend = RT_cas[i + 1];
                string RT_miestSend = RT_miest[i + 1];

                string OT_datSend = OT_dat[i + 1];
                string OT_casSend = OT_cas[i + 1];
                string OT_miestSend = OT_miest[i + 1];

                var obj = new TSModel()
                {
                    Predmet = Predmet[i + 1],

                    RT_dat = RT_dat[i + 1],
                    RT_cas = RT_cas[i + 1],
                    RT_miest = RT_miest[i + 1],

                    OT_dat = OT_dat[i + 1],
                    OT_cas = OT_cas[i + 1],
                    OT_miest = OT_miest[i + 1],

                    Command = new Command(() => Navigation.PushAsync(new CreateRemind(PredmetSend, RT_datSend, RT_casSend, RT_miestSend, OT_datSend, OT_casSend, OT_miestSend)))
                };
                _TSmodel.Add(obj);
            }
            listView.ItemsSource = GetTSModel();
            

        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listView.ItemsSource = GetTSModel(e.NewTextValue);
        }

    }
}
