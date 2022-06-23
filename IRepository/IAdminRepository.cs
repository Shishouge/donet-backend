using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BloodManagement.Model;


namespace IRepository
{
    public interface IAdminRepository
    {
        //登录
        List<admin> login(Expression<Func<admin, bool>> whereExpression);
    }
}
