using System;
using System.Collections.Generic;
using System.Text;


namespace BloodManagement.Model
{
    public class outXuexingReport
    {
        public outXuexingReport()
        {
        }

        public outXuexingReport(int year, int month, string xuexing, int num)
        {
            this.year = year;
            this.month = month;
            this.xuexing = xuexing;
            this.num = num;
        }

        public int year { get; set; }                                    //出库年份 

        public int month { get; set; }                                   //出库月份
        public string xuexing { get; set; }                              //血型
        public int num { get; set; }                                     //数量

    }
}
