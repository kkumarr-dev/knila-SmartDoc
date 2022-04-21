using SmartDoc.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDoc.Repository
{
    public interface IAppoinmentRepo
    {
        Task<List<AppoinmentViewModel>> GetAllAppoinments();
        Task<List<AppoinmentViewModel>> GetAppoinments();
        Task<bool> PerformAppoinment(AppoinmentViewModel ap);
        Task<List<AppoinmentViewModel>> GetAppoinmentByPatientId(int patientId);
        Task<List<AppoinmentViewModel>> GetAppoinmentByDoctorId(int doctorId);
        Task<AppoinmentViewModel> GetAppoinmentById(int appointId);
        Task<CreateAppoinmentViewModel> CreateAppoinment();
        Task<object> GetFilledTimings(DateTime date, int doctorId);
    }
}
