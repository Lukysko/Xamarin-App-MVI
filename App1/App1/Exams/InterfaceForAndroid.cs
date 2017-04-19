using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public interface InterfaceForAndroid
    {
         void setEventCalendar(string predmet = "default", int month = 1, int day = 1, int start = 1, string description = "Exam");
    }
}
