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
using FAS.DAL;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using SimpleChart = System.Web.Helpers;
using Kendo.Mvc;


namespace FAS.WebUI.Controllers
{
    public class StatisticsController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

    }
}