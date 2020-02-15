using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartialViewCRUD.ViewModel
{
    public class ViewModelStudent
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public int IDClass { get; set; }
        public string ClassName { get; set; }
    }
}