using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TestDataBaseEx.Model;

namespace TestDataBaseEx.Controller {
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase {
        private readonly Connection _conn;

        public TaskController(IOptions<Connection> options) {
            _conn = options.Value;
        }

        [HttpGet]
        public IActionResult Get() {
            TESTDbContext testDbContext = new TESTDbContext(_conn.Default);

            var returnvisitlist = (from retur in testDbContext.ReturnVisitTasks select retur).ToList();

            var taskEndList = (from tel in returnvisitlist
                               where tel.ActualTime != null
                               group tel by tel.ConsultantId into g
                               select new { g.Key, tasktype = "已经完成的任务", count = g.Count() });

            var taskOutofDateList = (from tel in returnvisitlist
                                     where tel.ActualTime == null && Convert.ToDateTime(tel.ExpectedTime.ToShortDateString()) <
                                           Convert.ToDateTime(DateTime.Now.ToShortDateString())
                                     group tel by tel.ConsultantId into g
                                     select new { g.Key, tasktype = "已经过期未回访的任务", count = g.Count() });

            var taskToBeEndList = (from tel in returnvisitlist
                                   where tel.ActualTime == null && Convert.ToDateTime(tel.ExpectedTime.ToShortDateString()) >=
                                         Convert.ToDateTime(DateTime.Now.ToShortDateString())
                                   group tel by tel.ConsultantId into g
                                   select new { g.Key, tasktype = "未处理的任务", count = g.Count() });

            var salesList = (from consultant in testDbContext.Consultants select consultant).ToList();

            var salesTaskList = (from c in salesList
                                 join te in taskEndList on c.Id equals te.Key
                                 select new { c.ConsultantName, te.Key, te.tasktype, te.count }).ToList();

            var salesOutofDateList = (from c in salesList
                                      join te in taskOutofDateList on c.Id equals te.Key
                                      select new { c.ConsultantName, te.Key, te.tasktype, te.count }).ToList();

            var salesToBeEndList = (from c in salesList
                                    join te in taskToBeEndList on c.Id equals te.Key
                                    select new { c.ConsultantName, te.Key, te.tasktype, te.count }).ToList();

            salesTaskList.AddRange(salesOutofDateList);
            salesTaskList.AddRange(salesToBeEndList);

            return Ok(salesTaskList);
        }

    }
}
