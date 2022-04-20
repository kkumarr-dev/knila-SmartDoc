using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SmartDoc.Models
{
    public class CreateAppoinmentViewModel
    {
        public List<SelectListItem> DoctorList { get; set; }
    }
}
