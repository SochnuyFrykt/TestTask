using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestTask.Models;
using System.Text.Json;

namespace TestTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string inputLastName, string inputFirstName, string inputMiddleName, string inputBirthDay)
        {
            try
            {
                var humanModelList = new HumanModel()
                {
                    lastName = inputLastName.Replace(" ", ""),
                    firstName = inputFirstName.Replace(" ", ""),
                    middleName = inputMiddleName.Replace(" ", ""),
                    birthDay = inputBirthDay.Replace(" ", "")
                };

                string filePath = "User.json";
                string jsonString = JsonSerializer.Serialize(humanModelList);
                StreamWriter streamWriter = new StreamWriter(filePath);
                streamWriter.Write(jsonString);
                streamWriter.Close();
                return View();
            }
            catch
            {
                return Error();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}