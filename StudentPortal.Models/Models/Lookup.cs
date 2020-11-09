using System;
using System.Collections.Generic;

namespace StudentPortal.Models.Models
{
    public partial class Lookup
    {
        public int LookupId { get; set; }
        public string LookupValue { get; set; }
        public string LookupType { get; set; }
        public int SortOrder { get; set; }
    }
}
