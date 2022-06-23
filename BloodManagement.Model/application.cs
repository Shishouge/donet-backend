using System;
using System.Collections.Generic;
using System.Text;
namespace BloodManagement.Model
{
    public class application
    {
        public application()
        {
        }

        public application(string hospital, string section, string doctor_name, string patient_name, 
            string patient_sex, int patient_age, string patient_IDcard, string patient_phone, int applicate_year, 
            int applicate_month, int applicate_day, string blood_type, int blood_num, string xuexing, string applicate_reason,
            int out_storage, int blood_ID)
        {
            this.hospital = hospital;
            this.section = section;
            this.doctor_name = doctor_name;
            this.patient_name = patient_name;
            this.patient_sex = patient_sex;
            this.patient_age = patient_age;
            this.patient_IDcard = patient_IDcard;
            this.patient_phone = patient_phone;
            this.applicate_year = applicate_year;
            this.applicate_month = applicate_month;
            this.applicate_day = applicate_day;
            this.blood_type = blood_type;
            this.blood_num = blood_num;
            this.xuexing = xuexing;
            this.applicate_reason = applicate_reason;
            this.out_storage = out_storage;
            this.blood_ID = blood_ID;
        }

        public int ID { get; set; }                            //申请单序号

        public string hospital { get; set; }                   //申请医院

        public string section { get; set; }                    //申请科室

        public string doctor_name { get; set; }                //责任医生

        public string patient_name { get; set; }               //用血病人姓名

        public string patient_sex { get; set; }                //病人性别
        public int patient_age { get; set; }                   //病人姓名

        public string patient_IDcard { get; set; }             //病人身份证号

        public string patient_phone { get; set; }              //病人电话号码
        public int applicate_year { get; set; }                //申请日期-年
        public int applicate_month { get; set; }               //月
        public int applicate_day { get; set; }                 //日

        public string blood_type { get; set; }                 //血液类型
        public int blood_num { get; set; }                     //用血量
      
        public string xuexing { get; set; }                    //血型

        public string applicate_reason { get; set; }           //用血原因
        public int out_storage { get; set; }                     //是否出库 已出库1 未出库0

        public int blood_ID { get; set; }                        //申请血液ID
    }
}
