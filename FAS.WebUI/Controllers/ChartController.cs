using FAS.BLL;
using FAS.Domain;
using FAS.Web.Controllers;
using FAS.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FAS.WebUI.Controllers
{
    [Authorize]
    public class ChartController : AppController
    {
        private readonly IBankService bankService;

        public ChartController(IUserService userService, IBankService bankService)
            : base(userService)
        {
            this.bankService = bankService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CostByCategory()
        {
            return Json(await GetDataGroup(x => x.Category.Name), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public async Task<JsonResult> TransactionGroupByBank()
        {
            var dbGroup = (await GetDataGroup(x => x.Bank.Name)).ToList();

            foreach (var bank in bankService.Get())
            {
                if (!dbGroup.Any(x => x.Category.Equals(bank.Name)))
                {
                    dbGroup.Add(new ChartModel { Category = bank.Name, Value = 0 });
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
                          Category = gr.Key.ToString(),
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

        private async Task<IEnumerable<ChartModel>> GetDataGroup(Func<Transaction, string> groupBy)
        {
            return GetAllTransactions(await GetCurrentUserAsync())
                    .AsEnumerable()
                    .GroupBy(groupBy)
                    .Select(x => new ChartModel
                    {
                        Category = x.Key,
                        Value = x.Count()
                    });
        }

        
    }
}