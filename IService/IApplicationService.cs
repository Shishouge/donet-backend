using BloodManagement.Model;
using BloodManagement.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodManagement.IService
{
    public interface IApplicationService
    {
        //插入申请信息
        int insertRecords(application model);

        //返回未出库信息
        List<application> findAllication();

        //标记出库血液
        int outStorage(int ID);

        //更新某年某月出库血型报告
        int updateOutXuexingReport(int year, int month);

        //查找某月出库血型报告
        List<outXuexingReport> findOutXuexingReportByMonth(int year, int month);

        //更新某年某月出库血液类型报告
        int updateOutBloodTypeReport(int year, int month);

        //查找某月入库血液类型报告
        List<outBloodTypeReport> findOutBloodTypeReportByMonth(int year, int month);

        //查找过去半年出库血液类型折线图数据
        List<ReportHelper> findOutLineReport(int year, int month);
    }
}
