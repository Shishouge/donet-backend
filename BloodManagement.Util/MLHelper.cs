using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BloodManagement.Util
{
    public class MLHelper
    {
        [DllImport("Cal.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)] //引入dll，并设置字符集

        public static extern float Add(float a, float b);
        [DllImport("Cal.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)] //引入dll，并设置字符集
        public static extern float Sub(float a, float b);
        [DllImport("Cal.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)] //引入dll，并设置字符集
        public static extern float Mul(float a, float b);
        [DllImport("Cal.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)] //引入dll，并设置字符集
        public static extern float Div(float a, float b);
        public List<float> sevenData { get; set; }
        public float sum { get; set; }
        public List<float> AData { get; set; }
        public List<float> BData { get; set; }
        public List<float> OData { get; set; }
        public List<float> ABData { get; set; }
        public List<float> RhPData { get; set; }

        public List<string> dates { get; set; }

        public MLHelper(List<float> sevenData, float sum)
        {
            //A, B, O, AB, RhN
            float[] arr = new float[] { 0.279F, 0.292F, 0.344F, 0.082F, 0.003F };
            this.sevenData = sevenData;
            this.sum = sum;
            this.AData = new List<float>();
            this.BData = new List<float>();
            this.OData = new List<float>();
            this.ABData = new List<float>();
            this.RhPData = new List<float>();
            for (int i=0;i<sevenData.Count;i++)
            {
                float f = sevenData[i] * arr[0];
                int x = (int)(f * 100);
                f = (float)(x * 1.0) / 100;
                AData.Add(f);
            }
            for (int i = 0; i < sevenData.Count; i++)
            {
                float f = sevenData[i] * arr[1];
                int x = (int)(f * 100);
                f = (float)(x * 1.0) / 100;
                BData.Add(f);
            }
            for (int i = 0; i < sevenData.Count; i++)
            {
                float f = sevenData[i] * arr[2];
                int x = (int)(f * 100);
                f = (float)(x * 1.0) / 100;
                OData.Add(f);
            }
            for (int i = 0; i < sevenData.Count; i++)
            {
                float f = sevenData[i] * arr[3];
                int x = (int)(f * 100);
                f = (float)(x * 1.0) / 100;
                ABData.Add(f);
            }
            for (int i = 0; i < sevenData.Count; i++)
            {
                float f = sevenData[i] * arr[4];
                int x = (int)(f * 100);
                f = (float)(x * 1.0) / 100;
                RhPData.Add(f);
            }
            nextSevenDays n = new nextSevenDays();
            this.dates = n.getdays();
        }

        public MLHelper()
        {
        }
    }
}
