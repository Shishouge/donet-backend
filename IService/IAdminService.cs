using BloodManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IService
{
    public interface IAdminService
    {
        List<admin> login(Expression<Func<admin, bool>> whereExpression);

 
    }
}
