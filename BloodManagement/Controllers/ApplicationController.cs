using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodManagement.IService;
using BloodManagement.Model;
using BloodManagement.Service;
using BloodManagement.Util;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BloodManagement.Controllers
{
    [Route("[controller]")]
    public class ApplicationController : Controller
    {
        IApplicationService applicationService = new ApplicationServiceImpl();


        //// GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        [HttpGet("insertApplication")]
        public int insertApplication(string hospital, string section, string doctor_name, string patient_name,
            string patient_sex, int patient_age, string patient_IDcard, string patient_phone, string date, string blood_type, int blood_num, string xuexing, string applicate_reason,
            int blood_ID)
        {
            DateProcess dateProcess = new DateProcess(date);
            int year = dateProcess.getYear();
            int month = dateProcess.getMonth();
            int day = dateProcess.getDay();
            int out_storage = 0;                 //表示未出库
            return applicationService.insertRecords(new Model.application(hospital, section, doctor_name, patient_name,
             patient_sex, patient_age, patient_IDcard, patient_phone, year,
             month, day, blood_type, blood_num, xuexing, applicate_reason,
             out_storage, blood_ID));
        }

        // GET: api/<controller>
        [HttpGet("findApplication")]
        public List<application> findApplication()
        {
            return applicationService.findAllication();
        }

        [HttpGet("outStorage")]
        public int outStorage(int ID)
        {
            System.Diagnostics.Debug.Write(ID);
            return applicationService.outStorage(ID);
        }

        //更新某年某月出库血型报告
        [HttpGet("updateOutXuexingReport")]
        public int updateOutXuexingReport(int year, int month)
        {
            return applicationService.updateOutXuexingReport(year, month);
        }

        //查找某年某月入库血型报告
        [HttpGet("findOutXuexingReportByMonth")]
        public List<outXuexingReport> findOutXuexingReportByMonth(int year, int month)
        {
            return applicationService.findOutXuexingReportByMonth(year, month);
        }

        //更新某年某月出库血液类型报告
        [HttpGet("updateOutBloodTypeReport")]
        public int updateOutBloodTypeReport(int year, int month)
        {
            return applicationService.updateOutBloodTypeReport(year, month);
        }

        //查找某年某月出库血液类型报告
        [HttpGet("findOutBloodTypeReportByMonth")]
        public List<outBloodTypeReport> findOutBloodTypeReportByMonth(int year, int month)
        {
            return applicationService.findOutBloodTypeReportByMonth(year, month);
        }

        //查找半年出库血液报告
        [HttpGet("findOutLineReport")]
        public List<ReportHelper> findOutLineReport(int year, int month)
        {
            return applicationService.findOutLineReport(year, month);
        }

    }
}
