using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App1
{
    public class TSModel
    {
        public string Predmet { get; set; }
        public string RT_dat { get; set; }
        public string RT_cas { get; set; }
        public string RT_miest { get; set; }
        public string OT_dat { get; set; }
        public string OT_cas { get; set; }
        public string OT_miest { get; set; }
        public ICommand Command { set; get; }
        public Type PageType { get; set; }
    }
}
