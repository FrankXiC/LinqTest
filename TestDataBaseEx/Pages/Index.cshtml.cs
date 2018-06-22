using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using TestDataBaseEx.Model;

namespace TestDataBaseEx.Pages {
    public class IndexModel : PageModel {
        public Connection _conn;

        public IndexModel(IOptions<Connection> options) {
            _conn = options.Value;
        }
        public void OnGet() {
            TESTDbContext testDbContext = new TESTDbContext(_conn.Default);


            #region 问题1
            //var customerlist = (from cust in testDbContext.Customers select cust).ToList();

            //var returnvisitlist = (from retur in testDbContext.ReturnVisitTasks select retur).ToList();

            //var returnlist = (from c in customerlist
            //                  join r in returnvisitlist on c.Id equals r.CustomerId
            //                  where (c.CreateTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
            //                       ||
            //                       (Convert.ToDateTime(r.ActualTime).ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
            //                  select new { Customer = c }).Distinct().ToList();
            #endregion


            #region 问题2
            //var customerlist = (from cust in testDbContext.Customers select cust).ToList();

            //var returnvisitlist = (from retur in testDbContext.ReturnVisitTasks where retur.ActualTime == null select retur).ToList();

            //var visitList = (from visit in testDbContext.VisitRecords where visit.VisitTime != null select visit).ToList();

            //var returnlist = (from v in visitList
            //                  join r in returnvisitlist on v.CustomerId equals r.CustomerId
            //                  select new { r.CustomerId }).Distinct().ToList();

            //var returnCustomer = (from c in customerlist
            //                      join custId in returnlist on c.Id equals custId.CustomerId
            //                      select new { Customer = c }).Distinct();
            #endregion


            #region 问题3
            //var returnvisitlist = (from retur in testDbContext.ReturnVisitTasks select retur).ToList();

            //var taskEndList = (from tel in returnvisitlist
            //                   where tel.ActualTime != null
            //                   group tel by tel.ConsultantId into g
            //                   select new { g.Key, tasktype = "已经完成的任务", count = g.Count() });

            //var taskOutofDateList = (from tel in returnvisitlist
            //                         where tel.ActualTime == null && Convert.ToDateTime(tel.ExpectedTime.ToShortDateString()) <
            //                               Convert.ToDateTime(DateTime.Now.ToShortDateString())
            //                         group tel by tel.ConsultantId into g
            //                         select new { g.Key, tasktype = "已经过期未回访的任务", count = g.Count() });

            //var taskToBeEndList = (from tel in returnvisitlist
            //                       where tel.ActualTime == null && Convert.ToDateTime(tel.ExpectedTime.ToShortDateString()) >=
            //                             Convert.ToDateTime(DateTime.Now.ToShortDateString())
            //                       group tel by tel.ConsultantId into g
            //                       select new { g.Key, tasktype = "未处理的任务", count = g.Count() });

            //var salesList = (from consultant in testDbContext.Consultants select consultant).ToList();

            //var salesTaskList = (from c in salesList
            //                     join te in taskEndList on c.Id equals te.Key
            //                     select new { c.ConsultantName, te.Key, te.tasktype, te.count }).ToList();

            //var salesOutofDateList = (from c in salesList
            //                          join te in taskOutofDateList on c.Id equals te.Key
            //                          select new { c.ConsultantName, te.Key, te.tasktype, te.count }).ToList();

            //var salesToBeEndList = (from c in salesList
            //                        join te in taskToBeEndList on c.Id equals te.Key
            //                        select new { c.ConsultantName, te.Key, te.tasktype, te.count }).ToList();

            //salesTaskList.AddRange(salesOutofDateList);
            //salesTaskList.AddRange(salesToBeEndList);
            #endregion


            #region 问题4
            var customerList = (from ctlist in testDbContext.Customers select ctlist).ToList();

            var returnVistList = (from relist in testDbContext.ReturnVisitTasks
                                  group relist by relist.CustomerId into g
                                  let maxActual = g.Max(p => p.ActualTime)
                                  select new {
                                      g.Key,
                                      MaxActualTime = maxActual,
                                      intent = g.FirstOrDefault(p => p.ActualTime == maxActual).Intent
                                  }).ToList();

            var visitList = (from v in testDbContext.VisitRecords
                             group v by v.CustomerId into g
                             let maxVisit = g.Max(p => p.VisitTime)
                             select new {
                                 g.Key,
                                 maxVisitTime = maxVisit,
                                 intent = g.FirstOrDefault(p => p.VisitTime == maxVisit).Intent
                             }).ToList();

            var ctReturnList = (from c in customerList
                                join r in returnVistList on c.Id equals r.Key into cr
                                from r in cr.DefaultIfEmpty()
                                join vl in visitList on c.Id equals vl.Key into cvl
                                from vl in cvl.DefaultIfEmpty()
                                select new {
                                    CustomerName = c.CustomerName,
                                    Returnintent = cr.Select(t => t.intent).FirstOrDefault(),
                                    MaxActualTime = cr.Select(t => t.MaxActualTime).FirstOrDefault(),
                                    VisitIntent = cvl.Select(t => t.intent).FirstOrDefault(),
                                    MaxVisitTime = cvl.Select(t => t.maxVisitTime).FirstOrDefault()
                                }).ToList();

            #endregion
        }
    }
}
