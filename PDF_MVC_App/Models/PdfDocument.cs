﻿namespace PDF_MVC_App.Models
{
  
        public class PdfDocument
        {
            public int Id { get; set; }
            public string FileName { get; set; }
            public byte[] Content { get; set; }
        }

    
}
