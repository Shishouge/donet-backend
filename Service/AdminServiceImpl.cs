using BloodManagement.Model;
using BloodManagement.Repository;
using IRepository;
using IService;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Service
{
    public class AdminServiceImpl:IAdminService
    {
        IAdminRepository adminRepo = new AdminRepo();
        public List<admin> login(Expression<Func<admin, bool>> whereExpression)
        {
            return adminRepo.login(whereExpression);
        }

    }
}
