using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PDF_MVC_App.Data;
using PDF_MVC_App.Models;

namespace PDF_MVC_App.Controllers
{
    public class PdfDocumentsController : Controller
    {
        private readonly PDF_MVC_AppContext _context;

        private readonly IConfiguration configuration;

        public PdfDocumentsController(PDF_MVC_AppContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }



        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var pdfDocument = new PdfDocument
                    {
                        FileName = $"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}-{file.FileName}",
                        Content = memoryStream.ToArray()
                    };
                    _context.PdfDocuments.Add(pdfDocument);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Download(int id)
        {
            var pdfDocument = await _context.PdfDocuments.FirstOrDefaultAsync(doc => doc.Id == id);
            if (pdfDocument == null)
            {
                return NotFound();
            }

            return File(pdfDocument.Content, "application/pdf", pdfDocument.FileName);
        }

        // GET: PdfDocuments


        public  IActionResult Index(int page = 1, int pageSize = 5)
        {
            Console.WriteLine("Im page"+page);
            // Retrieve data from your data source
            var totalItems = _context.PdfDocuments.Count(); // Total number of items
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = _context.PdfDocuments.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;

            return View(items);
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.PdfDocuments.ToListAsync());
        //}

        //public ActionResult Index(string search)
        //{
        //    _context.
        //    SampleDBEntities sd = new SampleDBEntities();
        //    List<PdfDocuments> listemp = sd.PdfDocuments.ToList();
        //}

        // GET: PdfDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pdfDocument = await _context.PdfDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pdfDocument == null)
            {
                return NotFound();
            }

            return View(pdfDocument);
        }

        // GET: PdfDocuments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PdfDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FileName,Content")] PdfDocument pdfDocument)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pdfDocument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pdfDocument);
        }

        // GET: PdfDocuments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pdfDocument = await _context.PdfDocuments.FindAsync(id);
            if (pdfDocument == null)
            {
                return NotFound();
            }
            return View(pdfDocument);
        }

        // POST: PdfDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FileName,Content")] PdfDocument pdfDocument)
        {
            if (id != pdfDocument.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pdfDocument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PdfDocumentExists(pdfDocument.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pdfDocument);
        }

        // GET: PdfDocuments/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var pdfDocument = await _context.PdfDocuments
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (pdfDocument == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(pdfDocument);
        //}

        // POST: PdfDocuments/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var pdfDocument = await _context.PdfDocuments.FindAsync(id);
            if (pdfDocument != null)
            {
                _context.PdfDocuments.Remove(pdfDocument);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PdfDocumentExists(int id)
        {
            return _context.PdfDocuments.Any(e => e.Id == id);
        }
    }
}
