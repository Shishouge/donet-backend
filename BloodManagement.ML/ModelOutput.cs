using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodManagement.ML
{
    public class ModelOutput
    {
        public float[] forecasted_count { get; set; }
        public float[] lower_count { get; set; }
        public float[] upper_count { get; set; }
    }
}
