﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using FAS.BLL;
using FAS.Domain;
using FAS.WebUI.Models;

namespace FAS.WebUI.Controllers
{
    public class BankController : Controller
    {
        private readonly IBankService BankService;

        public BankController(IBankService bankService)
        {
            this.BankService = bankService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await BankService.Get().ProjectTo<SimpleBankViewModel>().ToListAsync());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBankViewModel model)
        {
            if (ModelState.IsValid)
            {
                await BankService.CreateAsync(Mapper.Map<Bank>(model));
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            await BankService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Change(Guid id)
        {
            var bank = Mapper.Map<ChangeBankViewModel>(BankService.Get(id));
            return View(bank);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Change(ChangeBankViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bank = BankService.Get(model.Id);
                bank.Name = model.Name;
                await BankService.UpdateAsync(bank);
                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}