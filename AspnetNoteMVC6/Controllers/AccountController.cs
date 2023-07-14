using AspnetNoteMVC6.DataContext;
using AspnetNoteMVC6.Models;
using AspnetNoteMVC6.ViewModel;
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

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            //id, pw - 필수
            if(ModelState.IsValid)
            {
                using (var db = new AspnetNoteDbContext())
                {
                    // Linq - 메서드 체이닝
                    // => : A go to B
                    //var user = db.Users.FirstOrDefault(u => u.UserId == model.UserId && u.UserPassword == model.UserPassword); //db에 있는 usertable중 제일 첫번째 값 나옴
                    //== 메모리 낭비
                    var users = db.Users.FirstOrDefault(u => u.UserId.Equals(model.UserId) && u.UserPassword.Equals(model.UserPassword));

                    if(users != null)
                    {
                        //로그인에 성공
                        return RedirectToAction("LoginSuccess", "Home");
                    }

                    //로그인에 실페
                    ModelState.AddModelError(string.Empty, "사용자 ID 혹은 비밀번호가 올바르지 않습니다.");
                }
            }
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
