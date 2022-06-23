using System;
using System.Collections.Generic;
using System.Text;

namespace BloodManagement.Model
{
    public class xuexingMonthReport
    {
        public xuexingMonthReport()
        {
        }

        public xuexingMonthReport(int year, int month, int num, string xuexing)
        {
            this.year = year;
            this.month = month;
            this.num = num;
            this.xuexing = xuexing;
        }

        public int year { get; set; }                                    //入库年份 

        public int month { get; set; }                                   //入库月份

        public int num { get; set; }                                     //入库数量

        public string xuexing { get; set; }                              //入库血型

    }
}
