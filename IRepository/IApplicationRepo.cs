using System;
using System.Collections.Generic;
using System.Text;
using BloodManagement.Model;

namespace BloodManagement.IRepository
{
    public interface IApplicationRepo
    {
        //申请信息插入
        int insertApplication(application model);

        //查找未出库申请单
        List<application> findApplication();

        //标记出库信息
        int outStorage(int ID);

        //更新某月出库血型报告（年份 月份 A B AB O RHN RHP）
        int updateOutXuexingReport(int year, int month);

        //查找某月血型出库报告
        List<outXuexingReport> findOutXuexingReportByMonth(int year, int month);

        //更新某月出库血液类型报告
        int updateOutBloodTypeReport(int year, int month);

        //查找某月血液类型出库报告
        List<outBloodTypeReport> findOutBloodTypeReportByMonth(int year, int month);

        

    }


}
