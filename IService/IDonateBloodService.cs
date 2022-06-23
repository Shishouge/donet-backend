using BloodManagement.Model;
using BloodManagement.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BloodManagement.IService
{
    public interface IDonateBloodService
    {
        //插入血液信息
        int insertRecords(records model);

        //根据血液类型和血型查找血液
        List<records> findBlood(Expression<Func<records, bool>> whereExpression);

        //根据月份天数处理废血
        List<records> findOutofBlood(int year,int month, int day);

        //根据ID处理废血
        int outofBloodProc(int ID);

        //更新某年某月入库血型报告
        int upgradeXuexingReport(int year, int month);

        //查找某月入库血型报告
        List<xuexingMonthReport> findXuexingReportByMonth(int year, int month);

        //更新某年某月入库血液类型报告
        int updateBloodTypeReport(int year, int month);

        //查找某月入库血液类型报告
        List<bloodTypeMonthReport> findBloodTypeReportByMonth(int year, int month);

        //查找过去半年入库血液类型折线图数据
        List<ReportHelper> findLineReport(int year, int month);
        

    }
}
