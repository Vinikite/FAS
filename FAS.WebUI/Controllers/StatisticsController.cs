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
        public ActionResult Remote_Data_Binding()
        {
            ViewData["years"] = new[] { "2000", "2001", "2002", "2003", "2004", "2005", "2006", "2007", "2008", "2009" };
            return View();
        }

        private Dictionary<int, string> resolutionColors = new Dictionary<int, string>() {
            {1,"#ccc"},
            {2,"#c00"}
        };

        [HttpPost]
        public ActionResult _SpainElectricityProduction()
        {
            var screenResolutions = ChartDataRepository.WorldScreenResolution();
            var viewModel = new List<TransactionItemModel>();

            //for (var i = 0; i < screenResolutions.Count; i++)
            //{
            //    var data = screenResolutions[i];
            //    var model = new ScreenResolutionRemoteDataViewModel(data);
            //    if (model.Year == "2006" && model.Resolution == "1024x768")
            //    {
            //        model.Color = resolutionColors[2];
            //    }
            //    else if (model.Resolution == "Other")
            //    {
            //        model.Color = resolutionColors[1];
            //    }

            //    viewModel.Add(model);
            //}

            return Json(viewModel);
        }
    }
}