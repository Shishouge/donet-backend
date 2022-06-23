using System;
using System.Collections.Generic;
using System.Text;

namespace BloodManagement.Util
{
    public class ReportHelper
    {
        public string name { get; set; }
        public string type = "line";
        public string stack = "Total";
        public List<int> data { get; set; }

        public ReportHelper(string xuexing, string type, string stack, List<int> data)
        {
            this.name = xuexing;
            this.type = type;
            this.stack = stack;
            this.data = data;
        }

        public ReportHelper()
        {
        }
    }
}
