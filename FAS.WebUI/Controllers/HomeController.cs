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
using FAS.Web.Controllers;
using System.Collections;

namespace FAS.WebUI.Controllers
{
    [Authorize]
    public class HomeController : AppController
    {
        public HomeController(IUserService userService) : base(userService) { }
        public async Task<ActionResult> IndexHome()
        {
            int hour = DateTime.Now.Hour;
            if (hour < 7)
            { ViewBag.Greeting = "Доброго времени суток"; }
            if (hour > 7 && hour < 11)
            { ViewBag.Greeting = "Доброе утро";}
            if (hour > 11 && hour < 20)
            { ViewBag.Greeting = "Добрый день";}
            if (hour > 17)
            { ViewBag.Greeting = "Добрый вечер"; }
            return View(await UserService.Get().ProjectTo<ChangeUserViewModel>().ToListAsync());
        }
        
        public ActionResult About()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await UserService.Get().ToListAsync());
        }

       
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var userE = await GetCurrentUserAsync();
            var user = Mapper.Map<ChangeUserViewModel>(userE);
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.MiddleName = model.MiddleName;
                user.PhoneNumber = model.PhoneNumber;
                user.AverageIncome = model.AverageIncome;
                await UserService.UpdateAsync(user);
                return RedirectToAction("IndexHome", "Home");
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
        public async Task<ActionResult> Change(/*Guid id*/)
        {
            var userC = await GetCurrentUserAsync();
            var user = Mapper.Map<ChangeUserViewModel>(userC);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Change(ChangeUserViewModel model, ChangeAddressViewModel modelA)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                //var user = await UserService.GetAsync(model.Id);
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