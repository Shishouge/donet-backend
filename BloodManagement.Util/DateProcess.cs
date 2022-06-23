using System;

namespace BloodManagement.Util
{
    public class DateProcess
    {
        public string date;
        public int year;
        public int month;
        public int day;

        public DateProcess(string date)
        {
            this.date = date;
        }

        public DateProcess(int year, int month, int day)
        {
            this.year = year;
            this.month = month;
            this.day = day;
        }

        public int getYear()
        {
            string[] split = date.Split('-');
            int year = Convert.ToInt32(split[0]);
            return year;
        }

        public int getMonth()
        {
            string[] split = date.Split('-');
            int month = Convert.ToInt32(split[1]);
            return month;
        }

        public int getDay()
        {
            string[] split = date.Split('-');
            int day = Convert.ToInt32(split[2]);
            return day;
        }

        public string getDate()
        {
            string date = year.ToString() + '-' + month.ToString() + '-' + day.ToString();
            return date;
        }
        
    }
}
