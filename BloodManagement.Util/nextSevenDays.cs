using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BloodManagement.Util
{
    public class nextSevenDays
    {
        public List<string> getdays()
        {
            var dt = DateTime.Now;
            string path = "E:/大三下/.NET/Project/Backend/BloodManagement/BloodManagement.ML/Data/test.txt";
            ClearTxt(path);
            FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);//创建文件流
            StreamWriter sw = new StreamWriter(fs);
            List<string> dates = new List<string>();

            for (var i=0;i<7;i++)
            {
                var d=dt.AddDays(i);
                string temp = d.ToShortDateString().ToString();
                dates.Add(temp);
                System.Diagnostics.Debug.Write(temp);
                sw.WriteLine(temp);
            }
            sw.Close();//关闭写入器
            fs.Close();//关闭文件流
            return dates;
        }

        public void ClearTxt(String txtPath)
        {
            //String appDir = System.AppDomain.CurrentDomain.BaseDirectory + @"Txt\" + txtPath;
            FileStream stream = File.Open(txtPath, FileMode.OpenOrCreate, FileAccess.Write);
            stream.Seek(0, SeekOrigin.Begin);
            stream.SetLength(0);
            stream.Close();
        }
    }
}
