using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDoc.Models
{
    public class AppoinmentViewModel
    {
        public int AppoinmentId { get; set; }
        public DateTime AppoinmentDate { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public int StatusId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long PatientId { get; set; }
        public string PatientName { get; set; }
        public string age { get; set; }
        public long DoctorId { get; set; }
        public string DoctorName { get; set; }
    }
}
