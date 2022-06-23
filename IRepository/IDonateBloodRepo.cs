using BloodManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BloodManagement.IRepository
{
    public interface IDonateBloodRepo
    {
        //插入献血信息
        int insertRecords(records model);

        //根据血液类型和血型查找血液
        List<records> findBlood(Expression<Func<records, bool>> whereExpression);

        //根据月份查找废血
        List<records> findOutofBlood(int year,int month,int day);

        //根据ID处理废血
        int outofBloodProc(int ID);

        //更新某月入库血型报告（年份 月份 A B AB O RHN RHP）
        int updateXuexingReport(int year,int month);

        //查找某月血型入库报告
        List<xuexingMonthReport> findXuexingReportByMonth(int year,int month);

        //更新某月入库血液类型报告
        int updateBloodTypeReport(int year, int month);

        //查找某月血液类型入库报告
        List<bloodTypeMonthReport> findBloodTypeReportByMonth(int year, int month);
 
    }
}
