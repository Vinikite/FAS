using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FAS.BLL;
using FAS.Domain;
using FAS.WebUI.Infrastructure.Validators;
using FAS.WebUI.Models;

namespace FAS.WebUI.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult IndexHome()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        private readonly IUserService UserService;

        public HomeController(IUserService userService)
        {
            this.UserService = userService;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await UserService.Get().ToListAsync());//ProjectTo<SimpleUserViewModel>().ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Create(Guid id, Guid IdAddress)
        {
            var user = Mapper.Map<ChangeUserViewModel>(await UserService.GetAsync(id));
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserService.GetAsync(model.Id);

                if (user != null)
                {
                    ModelState.AddModelError("Id", "Id = null.");
                    return View(model);
                }


                user = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    PhoneNumber = model.PhoneNumber,
                    AverageIncome = model.AverageIncome
                };

                var createResult = await UserService.CreateWithInfoAsync(user);

                if (createResult.Succeeded)
                {
                    // send email to user 
                    ViewBag.Message = "Error send email to user.";
                }

                //ModelState.AddModelError("Email", createResult.Errors.Aggregate(String.Empty, (a, i) => a += i));
                return RedirectToAction("IndexHome");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            await UserService.DeleteAsync(id);
            return RedirectToAction("IndexHome");
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Change(Guid id)
        {
            var user = Mapper.Map<ChangeUserViewModel>(await UserService.GetAsync(id));
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Change(ChangeUserViewModel model, ChangeAddressViewModel modelA)
        {
            if (ModelState.IsValid)
            {
                var user = await UserService.GetAsync(model.Id);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.MiddleName = model.MiddleName;
                user.PhoneNumber = model.PhoneNumber;
                user.AverageIncome = model.AverageIncome;
                var userA = await UserService.GetAsync(modelA.Id);
                userA.MiddleName = modelA.City;
                await UserService.UpdateAsync(user);
                return RedirectToAction("IndexHome");

            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}