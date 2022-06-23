using BloodManagement.IRepository;
using BloodManagement.IService;
using BloodManagement.Model;
using BloodManagement.Repository;
using BloodManagement.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BloodManagement.Service
{
    public class DonateBloodServiceImpl:IDonateBloodService
    {
        IDonateBloodRepo donateBlood = new DonateBloodRepo();
        
        public List<records> findBlood(Expression<Func<records, bool>> whereExpression)
        {
            
            return donateBlood.findBlood(whereExpression);
        }

        public int insertRecords(records model)
        {
                return donateBlood.insertRecords(model);         
        }

        public List<records> findOutofBlood(int year,int month, int day)
        {
            return donateBlood.findOutofBlood(year,month, day);
        }

        public int outofBloodProc(int ID)
        {
            return donateBlood.outofBloodProc(ID);
        }

        public int upgradeXuexingReport(int year, int month)
        {
            return donateBlood.updateXuexingReport(year, month);
        }

        public List<xuexingMonthReport> findXuexingReportByMonth(int year, int month)
        {
            return donateBlood.findXuexingReportByMonth(year, month);
        }

        public int updateBloodTypeReport(int year, int month)
        {
            return donateBlood.updateBloodTypeReport(year, month);
        }

        public List<bloodTypeMonthReport> findBloodTypeReportByMonth(int year, int month)
        {
            return donateBlood.findBloodTypeReportByMonth(year, month);
        }

        //查找过去半年入库血液类型折线图数据
        public List<ReportHelper> findLineReport(int year, int month)
        {
            List<List<xuexingMonthReport>> reports = new List<List<xuexingMonthReport>>();
            List<ReportHelper> reportHelpers = new List<ReportHelper>();
            var y = year;
            var m = month;
            for(var i=0;i<6;i++)
            {
                m = m-1;
                //跨年处理 年减1，
                if(m<=0)
                {
                    y = y - 1;
                    m = 12;
                }
                reports.Add(donateBlood.findXuexingReportByMonth(y, m));
                //reports[i]:[{xuexing:"A",num:400,year:"2022",month:"5"},{xuexing:"B",num:"400",year:"2022",month:"5"}]
            }
            string[] xuexings = new string[] { "A","B","O","AB","RhN","RhP" };
            //对每个血型
            for(var i=0;i<xuexings.Length;i++)
            {
                ReportHelper reportHelper = new ReportHelper();
                reportHelper.name = xuexings[i];
                reportHelper.data = new List<int>();
                bool flag = false;
                //遍历
                for(var j=5;j>=0;j--)
                {
                    flag = false;
                    for(var k=0;k<reports[j].Count;k++)
                    {
                        if(reports[j][k].xuexing==xuexings[i])
                        {
                            reportHelper.data.Add(reports[j][k].num);
                            flag = true;
                            
                        }

                    }
                    if(!flag)
                       reportHelper.data.Add(0);
                }
                reportHelpers.Add(reportHelper);
            }
            return reportHelpers;


        }
    }
}
