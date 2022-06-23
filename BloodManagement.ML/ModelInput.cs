using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodManagement.ML
{
    public class ModelInput
    {
        [LoadColumn(0)]
        public DateTime action_time { get; set; }
        [LoadColumn(1)]
        public float count { get; set; }
    }
}
