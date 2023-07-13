using AspnetNoteMVC6.DataContext;
using AspnetNoteMVC6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspnetNoteMVC6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using (var db = new AspnetNoteDbContext())
            {
                var users = db.Users.ToList(); // 데이터베이스에서 모든 사용자 검색
                return View(users); // 사용자 목록을 인덱스 뷰에 전달
            }
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
    }
}