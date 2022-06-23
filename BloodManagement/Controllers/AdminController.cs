using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BloodManagement.Model;
using BloodManagement.Util;
using CodeDLL;
using IRepository;
using IService;
using Microsoft.AspNetCore.Mvc;
using Redis;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BloodManagement.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {

        IAdminService adminService = new AdminServiceImpl();
        // GET: api/<controller>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        [HttpGet("login")]
        public bool Get(int ID,string password)
        {
            //Console.WriteLine("被调用到！");
            List<admin> admins= adminService.login(d => d.ID == ID && d.password == password);
            System.Diagnostics.Debug.Write(admins.Count);
            System.Diagnostics.Debug.Write(ID);
            System.Diagnostics.Debug.Write(password);

            if (admins.Count == 1)
                return true;
            return false;
        }

        [HttpGet("getcode")]

        public string getcode(string str)
        {
            unsafe
            {
                InvokeCon invoke = new InvokeCon();
                string s = new string('A', 32);
                IntPtr intPtrStr = (IntPtr)Marshal.StringToHGlobalAnsi(s);
                sbyte* sbyteStr = (sbyte*)intPtrStr;
                sbyte* result = invoke.EnCodeCli(sbyteStr);
                string tmp = new string(result);
                return tmp;
            }

        }

        [HttpGet("cal")]
        public float cal(float a,float b)
        {
            //System.Diagnostics.Debug.WriteLine(Sub(a, b));
            MLHelper mLHelper = new MLHelper();
            nextSevenDays n = new nextSevenDays();
            n.getdays();
            return MLHelper.Add(a, b);
        }
        RedisHelper redisHelper = new RedisHelper("127.0.0.1:6379");
        string value = "this is a test redis string";
        Result result = null;

        [HttpGet("redisget")]
        public string Search(string key)
        {
            string returnStr = "";
            if (!string.IsNullOrWhiteSpace(key))
            {
                string value = redisHelper.GetValue(key);
                if (!string.IsNullOrWhiteSpace(value))
                    returnStr = value;
                else
                    returnStr = "key的值不存在！";
            }
            else
                returnStr = "key的值不能为空！";
            return returnStr;
        }

        [HttpGet("redisinsert")]
        public Result Insert(string key)
        {
            result = new Result();
            if (!string.IsNullOrWhiteSpace(key))
            {
                bool isInsertSuccess = redisHelper.SetValue(key, value);
                result.ImplementationResults = isInsertSuccess;
                if (isInsertSuccess)
                {
                    // 查询mytestkey的实时值
                    var info = Search("mytestkey");
                    if (!string.IsNullOrWhiteSpace(info))
                        result.Value = info;
                }
            }
            else
                result.Error = "key的值不能为空！";
            return result;
        }

        [HttpGet("testClr")]
        public int getcode()
        {
           
            return code.getcode();
        }
    }
}
