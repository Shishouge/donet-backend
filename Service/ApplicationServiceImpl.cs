using BloodManagement.IRepository;
using BloodManagement.IService;
using BloodManagement.Model;
using BloodManagement.Repository;
using BloodManagement.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodManagement.Service
{
    public class ApplicationServiceImpl : IApplicationService
    {
        IApplicationRepo applicationRepo = new ApplicationRepo();

        //查找未出库申请单
        public List<application> findAllication()
        {
            return applicationRepo.findApplication();
        }

        public List<outBloodTypeReport> findOutBloodTypeReportByMonth(int year, int month)
        {
            return applicationRepo.findOutBloodTypeReportByMonth(year, month);
        }

        public List<outXuexingReport> findOutXuexingReportByMonth(int year, int month)
        {
            return applicationRepo.findOutXuexingReportByMonth(year, month);
        }

        //插入申请信息
        public int insertRecords(application model)
        {
            return applicationRepo.insertApplication(model);
        }

        public int outStorage(int ID)
        {
            return applicationRepo.outStorage(ID);
        }

        public int updateOutBloodTypeReport(int year, int month)
        {
            return applicationRepo.updateOutBloodTypeReport(year, month);
        }

        public int updateOutXuexingReport(int year, int month)
        {
            return applicationRepo.updateOutXuexingReport(year, month);
        }

        public List<ReportHelper> findOutLineReport(int year, int month)
        {
            List<List<outXuexingReport>> reports = new List<List<outXuexingReport>>();
            List<ReportHelper> reportHelpers = new List<ReportHelper>();
            var y = year;
            var m = month;
            for (var i = 0; i < 6; i++)
            {
                m = m - 1;
                //跨年处理 年减1，
                if (m <= 0)
                {
                    y = y - 1;
                    m = 12;
                }
                reports.Add(applicationRepo.findOutXuexingReportByMonth(y, m));
                //reports[i]:[{xuexing:"A",num:400,year:"2022",month:"5"},{xuexing:"B",num:"400",year:"2022",month:"5"}]
            }
            string[] xuexings = new string[] { "A", "B", "O", "AB", "RhN", "RhP" };
            //对每个血型
            for (var i = 0; i < xuexings.Length; i++)
            {
                ReportHelper reportHelper = new ReportHelper();
                reportHelper.name = xuexings[i];
                reportHelper.data = new List<int>();
                bool flag = false;
                //遍历
                for (var j = 5; j >= 0; j--)
                {
                    flag = false;
                    for (var k = 0; k < reports[j].Count; k++)
                    {
                        if (reports[j][k].xuexing == xuexings[i])
                        {
                            reportHelper.data.Add(reports[j][k].num);
                            flag = true;

                        }

                    }
                    if (!flag)
                        reportHelper.data.Add(0);
                }
                reportHelpers.Add(reportHelper);
            }
            return reportHelpers;

        }
    }
}
