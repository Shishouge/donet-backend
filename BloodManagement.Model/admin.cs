using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;


namespace BloodManagement.Model
{
    [SugarTable("admin")]
    public class admin
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string password { get; set; }
    }
}
