using FAS.BLL;
using FAS.Domain;
using FAS.Web.Controllers;
using FAS.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FAS.WebUI.Controllers
{
    [Authorize]
    public class ChartController : AppController
    {
        private readonly IBankService bankService;
        private readonly string[] mounts;

        public ChartController(IUserService userService, IBankService bankService)
            : base(userService)
        {
            this.bankService = bankService;
            this.mounts = new[] { "Январь", "Февраль", "Март",
                                  "Апрель", "Май", "Июнь",
                                  "Июль", "Август", "Сентябрь",
                                  "Октябрь", "Ноябрь", "Декабрь" };
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CostByCategory()
        {
            var data = await GetDataGroup(x => x.Category.Name);

            return Json(data.Select(x => new ChartModel<double>()
            {
                Category = x.Key,
                Value = x.Sum(t => t.Comission)
            }), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public async Task<JsonResult> TransactionGroupByBank()
        {
            var dbGroup = (await GetDataGroup(x => x.Bank.Name))
                            .Select(x => new ChartModel<int>
                            {
                                Category = x.Key,
                                Value = x.Count()
                            })
                            .ToList();

            foreach (var bank in bankService.Get())
            {
                if (!dbGroup.Any(x => x.Category.Equals(bank.Name)))
                {
                    dbGroup.Add(new ChartModel<int> { Category = bank.Name, Value = 0 });
                }
            }

            return Json(dbGroup.OrderBy(x => x.Category), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public async Task<JsonResult> IncomeExpensesGroup()
        {
            var res = from tr in GetAllTransactions(await GetCurrentUserAsync())
                                .AsEnumerable()
                      group tr by tr.CreatedOn.Month into gr
                      select new StackChartModel
                      {
                          Category = mounts[gr.Key - 1],
                          Expenses = gr.Where(t => t.TransactionType.Name.Equals("Снятие со счета"))
                                        .Sum(t => t.Comission),
                          Incomes = gr.Where(t => t.TransactionType.Name.Equals("Поступление средств"))
                                       .Sum(t => t.Comission)
                      };

            return Json(res, JsonRequestBehavior.DenyGet);
        }

        private IEnumerable<Transaction> GetAllTransactions(User user)
        {
            return user.Scores.SelectMany(x => x.Transactions);
        }

        private async Task<IOrderedEnumerable<IGrouping<string, Transaction>>> GetDataGroup(
            Func<Transaction, string> groupBy)
        {
            return GetAllTransactions(await GetCurrentUserAsync())
                    .AsEnumerable()
                    .GroupBy(groupBy)
                    .OrderBy(x => x.Key);
        }  
    }
}