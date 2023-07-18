using AspnetNoteMVC6.DataContext;
using AspnetNoteMVC6.Models;
using AspnetNoteMVC6.ViewModel;
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
        /// 게시판 상세
        /// </summary>
        /// <param name="noteNo"></param>
        /// <returns></returns>
        public IActionResult Detail(int noteNo)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null) //로그인 체크
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }
            using (var db = new AspnetNoteDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNo.Equals(noteNo)); //DBdml Notes중에서 선택한 NoteNo와 일치하는것 출력
                return View(note);
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

            var userLoginKey = HttpContext.Session.GetInt32("USER_LOGIN_KEY");
            if (userLoginKey == null)
            {
                ModelState.AddModelError(string.Empty, "사용자 로그인 키가 없습니다."); // 오류 메시지 추가
                                                                            // 필요한 추가 조치 수행
            }
            else
            {
                var userNo = userLoginKey.Value;
                model.UserNo = userNo; // UserNo에 값을 할당합니다.
            }
            

            if (ModelState.IsValid==false) //ModelState.IsValid가 true가 될때 DB에 저장하고 싶음
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

            // 유효성 검사 오류 메시지 수집
            var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            foreach (var errorMessage in errorMessages)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
            }//The User field is required.

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
