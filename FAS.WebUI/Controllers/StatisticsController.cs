using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using SimpleChart = System.Web.Helpers;

namespace FAS.WebUI.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        public ActionResult Index()
        {
            return View();
        }
        //public FileContentResult GetChart()
        //{
        //    var dates = new List<Tuple<int, string>>(
        //          new[]
        //                 {
        //                   new Tuple<int, string> (65, "January"),
        //                   new Tuple<int, string> (69, "February"),
        //                   new Tuple<int, string> (90, "March"),
        //                   new Tuple<int, string> (81, "April"),
        //                   new Tuple<int, string> (81, "May"),
        //                   new Tuple<int, string> (55, "June"),
        //                   new Tuple<int, string> (40, "July")
        //                 }
        //          );

        //    var chart = new Chart();
        //    chart.Width = 700;
        //    chart.Height = 300;
        //    chart.BackColor = Color.FromArgb(211, 223, 240);
        //    chart.BorderlineDashStyle = ChartDashStyle.Solid;
        //    chart.BackSecondaryColor = Color.White;
        //    chart.BackGradientStyle = GradientStyle.TopBottom;
        //    chart.BorderlineWidth = 1;
        //    chart.Palette = ChartColorPalette.BrightPastel;
        //    chart.BorderlineColor = Color.FromArgb(26, 59, 105);
        //    chart.RenderType = RenderType.BinaryStreaming;
        //    chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
        //    chart.AntiAliasing = AntiAliasingStyles.All;
        //    chart.TextAntiAliasingQuality = TextAntiAliasingQuality.Normal;
        //    chart.Titles.Add(CreateTitle());
        //    chart.Legends.Add(CreateLegend());
        //    chart.Series.Add(CreateSeries(dates, SeriesChartType.Line, Color.Red));
        //    chart.ChartAreas.Add(CreateChartArea());

        //    var ms = new System.IO.MemoryStream();
        //    chart.SaveImage(ms);
        //    return File(ms.GetBuffer(), @"image/png");
        //}
        //Создание заголовка выглядит следующим образом:
        [NonAction]
        public Title CreateTitle()
        {
            Title title = new Title();
            title.Text = "Result Chart";
            title.ShadowColor = Color.FromArgb(32, 0, 0, 0);
            title.Font = new Font("Trebuchet MS", 14F, FontStyle.Bold);
            title.ShadowOffset = 3;
            title.ForeColor = Color.FromArgb(26, 59, 105);

            return title;
        }
        [NonAction]
        public Series CreateSeries(IList<Tuple<int, string>> results,
       SeriesChartType chartType,
       Color color)
        {
            var seriesDetail = new Series();
            seriesDetail.Name = "Result Chart";
            seriesDetail.IsValueShownAsLabel = false;
            seriesDetail.Color = color;
            seriesDetail.ChartType = chartType;
            seriesDetail.BorderWidth = 2;
            seriesDetail["DrawingStyle"] = "Cylinder";
            seriesDetail["PieDrawingStyle"] = "SoftEdge";
            DataPoint point;

            foreach (var result in results)
            {
                point = new DataPoint();
                point.AxisLabel = result.Item2;
                point.YValues = new double[] { result.Item1 };
                seriesDetail.Points.Add(point);
            }
            seriesDetail.ChartArea = "Result Chart";

            return seriesDetail;
        }
        //Создание легенды выглядит следующим образом:
        [NonAction]
        public Legend CreateLegend()
        {
            var legend = new Legend();
            legend.Name = "Result Chart";
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            legend.BackColor = Color.Transparent;
            legend.Font = new Font(new FontFamily("Trebuchet MS"), 9);
            legend.LegendStyle = LegendStyle.Row;

            return legend;
        }
        //Последним штрихом создадим область, в которой данный график будет отображен.
        [NonAction]
        public ChartArea CreateChartArea()
        {
            var chartArea = new ChartArea();
            chartArea.Name = "Result Chart";
            chartArea.BackColor = Color.Transparent;
            chartArea.AxisX.IsLabelAutoFit = false;
            chartArea.AxisY.IsLabelAutoFit = false;
            chartArea.AxisX.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            chartArea.AxisY.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.Interval = 1;
            return chartArea;
        }
        public ActionResult CreateChart()
        {
            const string Blue = @"<Chart BackColor=""#D3DFF0"" BackGradientStyle=""TopBottom"" BackSecondaryColor=""White"" BorderColor=""26, 59, 105"" BorderlineDashStyle=""Solid"" BorderWidth=""2"" Palette=""BrightPastel"">
                    <ChartAreas>
                           <ChartArea Name=""Default"" _Template_=""All"" BackColor=""64, 165, 191, 228"" BackGradientStyle=""TopBottom"" BackSecondaryColor=""White"" BorderColor=""64, 64, 64, 64"" BorderDashStyle=""Solid"" ShadowColor=""Transparent"" />
                    </ChartAreas>
                    <Legends>
                           <Legend _Template_=""All"" BackColor=""Transparent"" Font=""Trebuchet MS, 8.25pt, style=Bold"" IsTextAutoFit=""False"" />
                    </Legends>
                    <BorderSkin SkinStyle=""Emboss"" />
                    </Chart>";
            var chart = new SimpleChart.Chart(width: 700, height: 300, theme: Blue)
            .AddTitle("График посещений")
            .AddSeries(
                  name: "Моя программа",
                  chartType: "Line",
                  xValue: new[] { "Peter", "Andrew", "Julie", "Mary", "Dave" },
                  yValues: new[] { "2", "6", "4", "5", "3" })
            .AddLegend()
            .Write();


            return null;
        }
    }
}