using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace AspnetNoteMVC6.Controllers
{
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public UploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        //http://example.com/Upload/ImageUpload
        //http://example.com/api/upload
        [HttpPost, Route("api/upload")]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {
            // # 이미지나 파일을 업로드 할때 필요한 구성
            //1. Path(경로) - 어디에 저장할지 결정
            var path = Path.Combine(_environment.WebRootPath, @"images\upload");
            //2. Name(이름) - DataTime(DataTime을 이름으로 설정하면 다인원이 사용할떄 동시에 사진을 올리면 이름이 같아져 오류),
            //                  GUID(전역 고유 식별자)+GUID
            //3. Extension(확장자) -jpg, png,... txt
            //var fileName = file.FileName; //FileName에 확장자포함되어있음

            var fileFullName = file.FileName.Split('.');

            var fileName = $"{Guid.NewGuid()}.{fileFullName[1]}";

            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                //file.CopyTo(fileStream); //CopyTo 큰용량은 병목현상
                await file.CopyToAsync(fileStream);
            }

            return Ok(new { file = "/images/upload/" + fileName, success = true });
            //업로드만함 -> view리턴할 필요가 없음

            //#URL 접근 방식
            //ASP.NET - 호스트명/ + api/upload
            //JavaScript - 호스트명 + / + api/upload => http://example.comapi/upload
        }
    }
}
