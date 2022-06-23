using BloodManagement.Model;
using BloodManagement.Repository.sugar;
using IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BloodManagement.Repository
{
    public class AdminRepo:IAdminRepository
    {
        private DbContext context;
        private SqlSugarClient db;
        private SimpleClient<admin> entityDB;

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
        public AdminRepo()
        {
            DbContext.Init(BaseDBConfig.ConnectionString);
            DbContext.DbType = DbType.MySql;
            context = DbContext.GetDbContext();
            db = context.Db;

            entityDB = context.GetEntityDB<admin>(db);
        }

        public List<admin> login(Expression<Func<admin, bool>> whereExpression)
        {

            return entityDB.GetList(whereExpression);
            
        }
    }
}
