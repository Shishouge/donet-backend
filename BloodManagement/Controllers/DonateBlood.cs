using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodManagement.IService;
using BloodManagement.ML;
using BloodManagement.Model;
using BloodManagement.Service;
using BloodManagement.Util;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Redis;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BloodManagement.Controllers
{
    [Route("[controller]")]
    public class DonateBlood : Controller
    {
        IDonateBloodService donateBloodService = new DonateBloodServiceImpl();
        RedisHelper redisHelper = new RedisHelper("127.0.0.1:6379");
        //// GET: api/<controller>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        //通过血液类型和血型查找未过期血液
        [HttpGet("findBlood")]
        public List<records> findBlood(string blood_type,string xuexing)
        {
            string key = "bloodListOF" + blood_type + xuexing;
            string value = redisHelper.GetValue(key);
            if (!string.IsNullOrWhiteSpace(value))
            {
                List < records > list= JsonConvert.DeserializeObject<List<records>>(value);
                return list;
            }
            else
            {
                List<records> bloodList = donateBloodService.findBlood(d => d.blood_type == blood_type && d.xuexing == xuexing && d.outofDate == 0);
                redisHelper.SetValue(key, JsonConvert.SerializeObject(bloodList));
                return bloodList;
            }
            

            //return donateBloodService.findBlood(d => d.blood_type == blood_type && d.xuexing == xuexing && d.outofDate==0);
        }

        // POST api/<controller>
        //插入献血信息
        [HttpGet("insertBlood")]
        public int insertBlood(string donor_name, string donor_sex, int donor_age, string donor_IDcard, string donor_phone,
            string donor_address, string donate_date,
            string blood_type, int blood_num, string xuexing, string hisMedical)
        {
            //System.Diagnostics.Debug.Write(donor_name);
            DateProcess dateProcess = new DateProcess(donate_date);
            int y = dateProcess.getYear();
            int m = dateProcess.getMonth();
            int d = dateProcess.getDay();
            return donateBloodService.insertRecords(new Model.records(donor_name, donor_sex, donor_age, donor_IDcard, donor_phone,
               donor_address, y,m,d, blood_type, blood_num, xuexing, hisMedical, 0));
        }

        //处理废血
        [HttpGet("outofBloodProc")]
        public int outofBloodProc(int ID)
        {
            
            return donateBloodService.outofBloodProc(ID);

        }

        //查找过期废血
        [HttpGet("findOutofBlood")]
        public List<records> findOutofBlood(string date)
        {
            DateProcess dateProcess = new DateProcess(date);
            int year = dateProcess.getYear();
            int month = dateProcess.getMonth();
            int day = dateProcess.getDay();

            string key = "outofBlood" + date;
            string value = redisHelper.GetValue(key);
            if (!string.IsNullOrWhiteSpace(value))
            {
                List<records> list = JsonConvert.DeserializeObject<List<records>>(value);
                return list;
            }
            else
            {
                List<records> bloodList = donateBloodService.findOutofBlood(year, month - 1, day);
                redisHelper.SetValue(key, JsonConvert.SerializeObject(bloodList));
                return bloodList;
            }

            

        }

        //更新某年某月入库血型报告
        [HttpGet("upgradeXuexingReport")]
        public int upgradeXuexingReport(int year,int month)
        {
            return donateBloodService.upgradeXuexingReport(year, month);
        }

        //查找某年某月入库血型报告
        [HttpGet("findXuexingReportByMonth")]
        public List<xuexingMonthReport> findXuexingReportByMonth(int year, int month)
        {
            return donateBloodService.findXuexingReportByMonth(year, month);
        }

        //更新某年某月入库血液类型报告
        [HttpGet("updateBloodTypeReport")]
        public int updateBloodTypeReport(int year,int month)
        {
            return donateBloodService.updateBloodTypeReport(year, month);
        }

        //查找某年某月入库血液类型报告
        [HttpGet("findBloodTypeReportByMonth")]
        public List<bloodTypeMonthReport> findBloodTypeReportByMonth(int year, int month)
        {
            return donateBloodService.findBloodTypeReportByMonth(year, month);
        }

        //查找过去半年入库血型报告
        [HttpGet("findLineReport")]
        public List<ReportHelper> findLineReport(int year,int month)
        {
            return donateBloodService.findLineReport(year, month);
        }

        //预测未来一个月血型入库
        [HttpGet("predict")]
        public MLHelper predictXuexing()
        {
            Train train = new Train();
            return train.run();
        }



    }
}
