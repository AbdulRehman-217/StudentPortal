using System;
using System.Collections.Generic;
using System.Text;

namespace StudentPortal.Models.Dtos
{
    public class EmailDto
    {
            public string Subject { get; set; }
            public string Body { get; set; }

            public string FromName { get; set; }
            public string ToEmail { get; set; }
            public string CCEmail { get; set; }
    }
}
