using System;
using System.Collections.Generic;
using System.Text;
using BloodManagement.IRepository;
using BloodManagement.Model;
using BloodManagement.Model.Helper;
using BloodManagement.Repository.sugar;
using IRepository;
using SqlSugar;

namespace BloodManagement.Repository
{
    public class ApplicationRepo:IApplicationRepo
    {
        private DbContext context;
        private SqlSugarClient db;
        private SimpleClient<application> entityDB;

        internal SqlSugarClient Db
        {
            get { return db; }
            private set { db = value; }
        }
        public DbContext Context
        {
            get { return context; }
            set { context = value; }
        }
        public ApplicationRepo()
        {
            DbContext.Init(BaseDBConfig.ConnectionString);
            DbContext.DbType = DbType.MySql;
            context = DbContext.GetDbContext();
            db = context.Db;
            entityDB = context.GetEntityDB<application>(db);
        }
        
        //插入申请信息
        public int insertApplication(application model)
        {
            lock(this)
            {
               var i = db.Insertable(model).ExecuteReturnBigIdentity();
               return i.ObjToInt();
            }
 
        }

        //返回未出库申请信息
        public List<application> findApplication()
        {
            return db.Queryable<application>().Where(it => it.out_storage==0).ToList();
        }

        public int outStorage(int ID)
        {
            lock(this)
            {

                var i = db.Updateable<application>()
                    .SetColumns(it => it.out_storage == 1)
                    .Where(it => it.ID == ID)
                    .ExecuteCommand();
                return i.ObjToInt();
            }

        }

        //更新某月出库血型报告
        public int updateOutXuexingReport(int year, int month)
        {
            lock(this)
            {
                var list = db.Queryable<application>()
                 .GroupBy(it => new { it.applicate_year, it.applicate_month, it.xuexing }) //可以多字段
                 .Where(it => it.applicate_year == year && it.applicate_month == month)
                 .Select(it => new { num = SqlFunc.AggregateSum(it.blood_num), year = it.applicate_year, month = it.applicate_month, xuexing = it.xuexing })
                 .ToList();

                int result = 0;
                //System.Diagnostics.Debug.Write(list[0]);
                //System.Diagnostics.Debug.Write(list[1]);

                //查找是否已经更新过
                List<outXuexingReport> reports = db.Queryable<outXuexingReport>().Where(it => it.year == year && it.month == month).ToList();
                //如果没有更新过
                if (reports.Count == 0)
                {
                    for (var i = 0; i < list.Count; i++)
                    {
                        var j = db.Insertable(new outXuexingReport(list[i].year, list[i].month, list[i].xuexing,list[i].num )).ExecuteReturnBigIdentity();
                        result += j.ObjToInt();
                    }
                }

                return result;
            }

        }

        //查找某月血型出库报告
        public List<outXuexingReport> findOutXuexingReportByMonth(int year, int month)
        {
            return db.Queryable<outXuexingReport>().Where(it => it.year == year && it.month == month).ToList();
        }

        //更新某月血液类型出库报告
        public int updateOutBloodTypeReport(int year, int month)
        {
            lock(this)
            {
               var list = db.Queryable<application>()
                 .GroupBy(it => new { it.applicate_year, it.applicate_month, it.blood_type }) //可以多字段
                 .Where(it => it.applicate_year == year && it.applicate_month == month)
                 .Select(it => new { num = SqlFunc.AggregateSum(it.blood_num), year = it.applicate_year, month = it.applicate_month, type = it.blood_type })
                 .ToList();

                int result = 0;
                System.Diagnostics.Debug.Write(list[0]);
                System.Diagnostics.Debug.Write(list[1]);

                //查找是否已经更新过
                List<outBloodTypeReport> reports = db.Queryable<outBloodTypeReport>().Where(it => it.year == year && it.month == month).ToList();
                //如果没有更新过
                if (reports.Count == 0)
                {
                    for (var i = 0; i < list.Count; i++)
                    {
                        var j = db.Insertable(new outBloodTypeReport(list[i].year, list[i].month, list[i].type, list[i].num)).ExecuteReturnBigIdentity();
                        result += j.ObjToInt();
                    }
                }

                return result;
            }
 
        }

        //查找某月血液类型出库报告
        public List<outBloodTypeReport> findOutBloodTypeReportByMonth(int year, int month)
        {
            return db.Queryable<outBloodTypeReport>().Where(it => it.year == year && it.month == month).ToList();
        }
    }
}
