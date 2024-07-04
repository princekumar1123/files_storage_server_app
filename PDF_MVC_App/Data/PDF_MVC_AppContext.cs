using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PDF_MVC_App.Models;

namespace PDF_MVC_App.Data
{
    public class PDF_MVC_AppContext : DbContext
    {
        public PDF_MVC_AppContext (DbContextOptions<PDF_MVC_AppContext> options)
            : base(options)
        {
        }

        public DbSet<PDF_MVC_App.Models.PdfDocument> PdfDocuments { get; set; } = default!;
    }
}
