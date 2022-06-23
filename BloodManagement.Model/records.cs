using System;

namespace BloodManagement.Model
{
    public class records
    {
        public records()
        {
        }

        public records(string donor_name, string donor_sex, int donor_age, string donor_IDcard, string donor_phone, 
            string donor_address, int donate_year, int donate_month, int donate_day, 
            string blood_type, int blood_num, string xuexing, string hisMedical, int outofDate)
        {
            this.donor_name = donor_name;
            this.donor_sex = donor_sex;
            this.donor_age = donor_age;
            this.donor_IDcard = donor_IDcard;
            this.donor_phone = donor_phone;
            this.donor_address = donor_address;
            this.donate_year = donate_year;
            this.donate_month = donate_month;
            this.donate_day = donate_day;
            this.blood_type = blood_type;
            this.blood_num = blood_num;
            this.xuexing = xuexing;
            this.hisMedical = hisMedical;
            this.outofDate = outofDate;
        }

        public int ID { get; set; }                             //存档序号

        public string donor_name { get; set; }                  //献血人姓名

        public string donor_sex { get; set; }                   //献血人性别
        public int donor_age { get; set; }                      //献血人年龄

        public string donor_IDcard { get; set; }                //献血人身份证号

        public string donor_phone { get; set;}                  //献血人电话

        public string donor_address { get; set; }               //献血人住址
        public int donate_year { get; set; }                    //献血日期-年
        public int donate_month { get; set; }                   //献血日期-月
        public int donate_day { get; set; }                     //献血日期-日
        public string blood_type { get; set; }                  //血液类型（成分血/全血）
        public int blood_num { get; set; }                      //血液量
        public string xuexing { get; set; }                     //血型
        public string hisMedical { get; set; }                  //献血人既往病史
        public int outofDate { get; set; }                      //是否过期 已过期1 未过期0



    }
}
