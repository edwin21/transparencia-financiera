using System.Data;
using System.Diagnostics;
using FusionCharts.DataEngine;
using FusionCharts.Visualization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TransparenciaFinanciera.Models;

namespace TransparenciaFinanciera.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TransparenciaFinancieraContext _context;
    private readonly IWebHostEnvironment _appEnvironment;
    public HomeController(ILogger<HomeController> logger, TransparenciaFinancieraContext context, IWebHostEnvironment appEnvironment)
    {
        _logger = logger;
        _context = context;
        _appEnvironment = appEnvironment;
    }

    public IActionResult Index()
    {
        ViewData["Datos"] = GetData();
        return View();
    }
    [HttpGet]
    public  IActionResult Ejemplo()
    {
      
        var data = _context.Egreso.ToList();
        return PartialView("_Ejemplo", data);
    }
    [HttpGet]
    public IActionResult CargarDependencias()
    {
        var data = LoadJson();
        return PartialView("_Dependencias", data);
    }
    [HttpGet]
    public IActionResult CargarGrafica(string _dependencia)
    {
        var data = LoadJson();
        var dependencia = data.FirstOrDefault(a => a.dependencia == _dependencia);
        ViewData["Datos"] = GenerarGraficaDependencia(dependencia);//  GetData();
        return PartialView("_Grafica", dependencia);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public List<DTO.IngresosComparativo> LoadJson()
    {
        var path = _appEnvironment.WebRootPath;
        using (StreamReader r = new StreamReader(_appEnvironment.WebRootPath+"/js/dataDemo.json"))
        {
            string json = r.ReadToEnd();
            List<DTO.IngresosComparativo> items = JsonConvert.DeserializeObject<List<DTO.IngresosComparativo>>(json);
            return items;
        }

    }


    // create a public property. OnGet method() set the chart configuration json in this property.
    // When the page is being loaded, OnGet method will be  invoked
    public string ChartJson { get; internal set; }
    public string GetData()
    {

        // create data table to store data
        DataTable ChartData = new DataTable();
        // Add columns to data table
        ChartData.Columns.Add("Programming Language", typeof(System.String));
        ChartData.Columns.Add("Users", typeof(System.Double));
        // Add rows to data table

        ChartData.Rows.Add("Java", 62000);
        ChartData.Rows.Add("Python", 46000);
        ChartData.Rows.Add("Javascript", 38000);
        ChartData.Rows.Add("C++", 31000);
        ChartData.Rows.Add("C#", 27000);
        ChartData.Rows.Add("PHP", 14000);
        ChartData.Rows.Add("Perl", 14000);

        // Create static source with this data table
        StaticSource source = new StaticSource(ChartData);
        // Create instance of DataModel class
        DataModel model = new DataModel();
        // Add DataSource to the DataModel
        model.DataSources.Add(source);
        // Instantiate Column Chart
        Charts.ColumnChart column = new Charts.ColumnChart("first_chart");
        // Set Chart's width and height
        column.Width.Pixel(700);
        column.Height.Pixel(400);
        // Set DataModel instance as the data source of the chart
        column.Data.Source = model;
        // Set Chart Title
        column.Caption.Text = "Most popular programming language";
        // Set chart sub title
        column.SubCaption.Text = "2017-2018";
        // hide chart Legend
        column.Legend.Show = false;
        // set XAxis Text
        column.XAxis.Text = "Programming Language";
        // Set YAxis title
        column.YAxis.Text = "User";
        // set chart theme
        column.ThemeName = FusionChartsTheme.ThemeName.FUSION;
        // set chart rendering json
        return  column.Render();
    }
    public string GenerarGraficaDependencia(DTO.IngresosComparativo info)
    {

        // create data table to store data
        DataTable ChartData = new DataTable();
        // Add columns to data table
        ChartData.Columns.Add("Ingreso programdo e ingreso real", typeof(System.String));
        ChartData.Columns.Add("Dinero", typeof(System.Double));
        // Add rows to data table

        ChartData.Rows.Add("Ingreso programado", info.ingresoProgramado);
        ChartData.Rows.Add("Ingreso real", info.ingresoReal);
 
        // Create static source with this data table
        StaticSource source = new StaticSource(ChartData);
        // Create instance of DataModel class
        DataModel model = new DataModel();
        // Add DataSource to the DataModel
        model.DataSources.Add(source);
        // Instantiate Column Chart
        Charts.ColumnChart column = new Charts.ColumnChart("first_chart");
        // Set Chart's width and height
        column.Width.Pixel(700);
        column.Height.Pixel(400);
        // Set DataModel instance as the data source of the chart
        column.Data.Source = model;
        // Set Chart Title
        column.Caption.Text = "Ingreso programdo e ingreso real";
        // Set chart sub title
        column.SubCaption.Text = info.mes;//"2017-2018";
        // hide chart Legend
        column.Legend.Show = false;
        // set XAxis Text
        column.XAxis.Text = "Ingresos";
        // Set YAxis title
        column.YAxis.Text = "Dinero";
        // set chart theme
        column.ThemeName = FusionChartsTheme.ThemeName.FUSION;
        // set chart rendering json
        return column.Render();
    }

}
