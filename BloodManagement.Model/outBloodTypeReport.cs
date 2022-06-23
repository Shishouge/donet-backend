using System;
using System.Collections.Generic;
using System.Text;

namespace BloodManagement.Model
{
    public class outBloodTypeReport
    {
        public int year { get; set; }
        public int month { get; set; }
        public string type { get; set; }
        public int num { get; set; }

        public outBloodTypeReport()
        {
        }

        public outBloodTypeReport(int year, int month, string type, int num)
        {
            this.year = year;
            this.month = month;
            this.type = type;
            this.num = num;
        }
    }
}
