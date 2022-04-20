using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartDoc.Entities
{
    [Table("TblAppointments")]
    public class Appointments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppoinmentId { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public int StatusId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long PatientId { get; set; }
        public long DoctorId { get; set; }
        [ForeignKey("PatientId")]
        public Users UsersPatientId { get; set; }
        [ForeignKey("DoctorId")]
        public Users UsersDoctorId { get; set; }
    }
}
