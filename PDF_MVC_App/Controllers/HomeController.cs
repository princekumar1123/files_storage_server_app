using Microsoft.AspNetCore.Mvc;
using PDF_MVC_App.Models;
using System.Diagnostics;

namespace PDF_MVC_App.Controllers
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
            return View();
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


//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using System.IO;
//using System.Threading.Tasks;
//using PDF_MVC_App.Data;
//using PDF_MVC_App.Models;

//public class HomeController : Controller
//{
//    private readonly PDF_MVC_AppContext _dbContext;

//    public HomeController(PDF_MVC_AppContext dbContext)
//    {
//        _dbContext = dbContext;
//    }

//    public IActionResult Index()
//    {
//        return View();
//    }

//    [HttpPost]
//    public async Task<IActionResult> Upload(IFormFile file)
//    {
//        if (file != null && file.Length > 0)
//        {
//            using (var memoryStream = new MemoryStream())
//            {
//                await file.CopyToAsync(memoryStream);
//                var pdfDocument = new PdfDocument
//                {
//                    FileName = file.FileName,
//                    Content = memoryStream.ToArray()
//                };
//                _dbContext.PdfDocuments.Add(pdfDocument);
//                await _dbContext.SaveChangesAsync();
//            }
//        }

//        return RedirectToAction(nameof(Index));
//    }


//    public async Task<IActionResult> Download(int id)
//    {
//        var pdfDocument = await _dbContext.PdfDocuments.FirstOrDefaultAsync(doc => doc.Id == id);
//        if (pdfDocument == null)
//        {
//            return NotFound();
//        }

//        return File(pdfDocument.Content, "application/pdf", pdfDocument.FileName);
//    }
//}
