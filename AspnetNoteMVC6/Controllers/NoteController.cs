using AspnetNoteMVC6.DataContext;
using AspnetNoteMVC6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspnetNoteMVC6.Controllers
{
    public class NoteController : Controller
    {
        /// <summary>
        /// 게시판 리스트
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null) //로그인 체크
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            //var list = new List<Note>();
            using (var db = new AspnetNoteDbContext())
            {
                var list = db.Notes.ToList();
                return View(list);
            }
        }
        /// <summary>
        /// 게시물 추가
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null) //로그인 체크
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Add(Note model)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null) //로그인 체크
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }
            //model.UserNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());
      
            if (ModelState.IsValid) 
            {
                using (var db = new AspnetNoteDbContext())
                {
                    db.Notes.Add(model); //DB에 입력
                    if (db.SaveChanges() > 0) 
                    { 
                        return Redirect("Index");
                    }
                    //db.SaveChanges();   //commit
                    //return Redirect("Index");같은 소속일떄 / == return RedirectToAction("Index", "Note");
                }
                ModelState.AddModelError(string.Empty, "게시물을 저장할 수 없습니다.");
            }

            return View(model);
        }
        /// <summary>
        /// 게시물 편집
        /// </summary>
        public IActionResult Edit()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null) //로그인 체크
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        /// <summary>
        /// 게시물 삭제
        /// </summary>
        public IActionResult Delete()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null) //로그인 체크
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

    }
}
