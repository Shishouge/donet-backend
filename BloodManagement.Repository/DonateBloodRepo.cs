using BloodManagement.IRepository;
using BloodManagement.Model;
using BloodManagement.Model.Helper;
using BloodManagement.Repository.sugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BloodManagement.Repository
{
    public class DonateBloodRepo : IDonateBloodRepo
    {
        private DbContext context;
        private SqlSugarClient db;
        private SimpleClient<records> entityDB;

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
        public DonateBloodRepo()
        {
            DbContext.Init(BaseDBConfig.ConnectionString);
            DbContext.DbType = DbType.MySql;
            context = DbContext.GetDbContext();
            db = context.Db;
            entityDB = context.GetEntityDB<records>(db);
        }
        //插入献血信息
        public int insertRecords(records model)
        {
            lock(this)
            {
                var i = db.Insertable(model).ExecuteReturnBigIdentity();
                return i.ObjToInt();
            }

        }

        //根据血液类型和血型查找血液
        public List<records> findBlood(Expression<Func<records, bool>> whereExpression)
        {
            return entityDB.GetList(whereExpression);
        }

        //按照日期查找废血
        public List<records> findOutofBlood(int year,int month,int day)
        {
            //var i=db.Updateable<records>()
            //    .SetColumns(it => it.outofDate == 1)
            //    .Where(it => it.donate_year==year &&it.donate_month == month && it.donate_day==day)
            //    .ExecuteCommand();
            return db.Queryable<records>().Where(it => it.donate_year == year && it.donate_month == month && it.donate_day == day).ToList();
        }

        //标记过期血液
        public int outofBloodProc(int ID)
        {
            lock(this)
            {
                var i = db.Updateable<records>()
                    .SetColumns(it => it.outofDate == 1)
                    .Where(it => it.ID == ID)
                    .ExecuteCommand();
                return i.ObjToInt();
            }

        }

        //更新某月入库血型报告

        public int updateXuexingReport(int year, int month)
        {
            //按年份 月份 血型查找 依次更新
            lock(this)
            {
               var list = db.Queryable<records>()
                 .GroupBy(it => new { it.donate_year, it.donate_month,it.xuexing }) //可以多字段
                 .Where(it=>it.donate_year==year && it.donate_month==month)
                 .Select(it => new { num = SqlFunc.AggregateSum(it.blood_num), year = it.donate_year,month=it.donate_month,xuexing=it.xuexing })
                 .ToList();

                int result = 0;
                //System.Diagnostics.Debug.Write(list[0]);
                //System.Diagnostics.Debug.Write(list[1]);

                //查找是否已经更新过
                List<xuexingMonthReport> reports=db.Queryable<xuexingMonthReport>().Where(it => it.year == year && it.month == month ).ToList();
                //如果没有更新过
                if (reports.Count==0)
                {
                    for (var i=0;i<list.Count;i++)
                    {
                        var j = db.Insertable(new xuexingMonthReport(list[i].year, list[i].month, list[i].num, list[i].xuexing)).ExecuteReturnBigIdentity();
                        result += j.ObjToInt();
                    }
                }
            
                return result;
            }
 
        }
        //查找某月血型入库数量
        public List<xuexingMonthReport> findXuexingReportByMonth(int year,int month)
        {
            return db.Queryable<xuexingMonthReport>().Where(it => it.year==year&&it.month == month).ToList();
        }

        //更新某月血液类型入库数量
        public int updateBloodTypeReport(int year, int month)
        {
            lock(this)
            {
                var list = db.Queryable<records>()
                 .GroupBy(it => new { it.donate_year, it.donate_month, it.blood_type }) //可以多字段
                 .Where(it => it.donate_year == year && it.donate_month == month)
                 .Select(it => new { num = SqlFunc.AggregateSum(it.blood_num), year = it.donate_year, month = it.donate_month, type = it.blood_type })
                 .ToList();

                int result = 0;
                System.Diagnostics.Debug.Write(list[0]);
                System.Diagnostics.Debug.Write(list[1]);

                //查找是否已经更新过
                List<bloodTypeMonthReport> reports = db.Queryable<bloodTypeMonthReport>().Where(it => it.year == year && it.month == month).ToList();
                //如果没有更新过
                if (reports.Count == 0)
                {
                    for (var i = 0; i < list.Count; i++)
                    {
                        var j = db.Insertable(new bloodTypeMonthReport(list[i].year, list[i].month, list[i].type, list[i].num)).ExecuteReturnBigIdentity();
                        result += j.ObjToInt();
                    }
                }

                return result;
            }

        }

        //查找某月血液类型入库报告
        public List<bloodTypeMonthReport> findBloodTypeReportByMonth(int year, int month)
        {
            return db.Queryable<bloodTypeMonthReport>().Where(it => it.year == year && it.month == month).ToList();
        }

        
    }
}
