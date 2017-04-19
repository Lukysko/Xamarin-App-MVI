using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Android.Provider;
using Java.Util;

[assembly: Xamarin.Forms.Dependency(typeof(App1.Droid.Aktivita))]

namespace App1.Droid
{
    public class Aktivita : InterfaceForAndroid
    {
        private int year = 2017;

        public Aktivita()
        {
        }

        //Vytvorenie pripomienky
        public void setEventCalendar(string predmet,int month, int day, int start, string description ) { 

            ContentValues eventValues = new ContentValues();
            eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 3);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, predmet);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, description);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(year, month-1, day, start, 0));
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, GetDateTimeMS(year, month-1, day, start+2, 0));
            eventValues.Put(CalendarContract.Events.InterfaceConsts.HasAlarm, true);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "Local");
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "Local");
            var eventUri = Forms.Context.ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
            long eventID = long.Parse(eventUri.LastPathSegment);
            string reminderUriString = "content://com.android.calendar/reminders";
            ContentValues reminderValues = new ContentValues();
            // reminderValues.Put(CalendarContract.Reminders.InterfaceConsts.CalendarId, _calId);
            reminderValues.Put(CalendarContract.Reminders.InterfaceConsts.EventId, eventID);
            reminderValues.Put(CalendarContract.Reminders.InterfaceConsts.Method, RemindersMethod.Alert.ToString());
            reminderValues.Put(CalendarContract.Reminders.InterfaceConsts.Minutes, 5);
            Android.Net.Uri url = Android.Net.Uri.Parse(reminderUriString);
            var reminderUri = Forms.Context.ContentResolver.Insert(url, reminderValues);
        }

        long GetDateTimeMS(int yr, int month, int day, int hr, int min)
        {
            Calendar c = Calendar.GetInstance(Java.Util.TimeZone.Default);

            c.Set(Calendar.DayOfMonth, day);
            c.Set(Calendar.HourOfDay, hr);
            c.Set(Calendar.Minute, min);
            c.Set(Calendar.Month, month);
            c.Set(Calendar.Year, yr);

            return c.TimeInMillis;
        }

    }
}