using System;
using System.Collections.Generic;
using System.Text;

namespace BloodManagement.Model
{
    public class bloodTypeMonthReport
    {
        public bloodTypeMonthReport()
        {
        }

        public bloodTypeMonthReport(int year, int month, string type, int num)
        {
            this.year = year;
            this.month = month;
            this.type = type;
            this.num = num;
        }

        public int year { get; set; }                                    //入库年份 

        public int month { get; set; }                                   //入库月份

        public string type { get; set; }                                //血液类型（全血，血小板，粒细胞，外周血干细胞）
        public int num { get; set; }                                    //入库数量

    }
}

