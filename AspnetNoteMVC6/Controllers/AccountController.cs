using AspnetNoteMVC6.DataContext;
using AspnetNoteMVC6.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspnetNoteMVC6.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            if(ModelState.IsValid) //IsValid 사용자 이름 id pw 모두 받앗나 체크 null이면 false되어 유징 안돌아감
            {
                // Java try(SqlSession){} catch(){}
                //C# using
                using (var db = new AspnetNoteDbContext())
                {
                    db.Users.Add(model); // 메모리에 올림
                    db.SaveChanges(); // 실제 sql에 저장
                }

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
