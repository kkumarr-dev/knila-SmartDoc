using SmartDoc.Models;
using SmartDoc.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDoc.Services
{
    public class AppoinmentService : IAppoinmentService
    {
        private readonly IAppoinmentRepo _appoinmentRepo;
        public AppoinmentService(IAppoinmentRepo appoinmentRepo)
        {
            _appoinmentRepo = appoinmentRepo;
        }

        public async Task<List<AppoinmentViewModel>> GetAllAppoinments()
        {
            return await _appoinmentRepo.GetAllAppoinments();
        }
        public async Task<List<AppoinmentViewModel>> GetAppoinments()
        {
            return await _appoinmentRepo.GetAppoinments();
        }
        public async Task<bool> PerformAppoinment(AppoinmentViewModel ap)
        {
            return await _appoinmentRepo.PerformAppoinment(ap);
        }
        public async Task<List<AppoinmentViewModel>> GetAppoinmentByPatientId(int patientId)
        {
            return await _appoinmentRepo.GetAppoinmentByPatientId(patientId);
        }
        public async Task<List<AppoinmentViewModel>> GetAppoinmentByDoctorId(int doctorId)
        {
            return await _appoinmentRepo.GetAppoinmentByDoctorId(doctorId);
        }
        public async Task<AppoinmentViewModel> GetAppoinmentById(int appointId)
        {
            return await _appoinmentRepo.GetAppoinmentById(appointId);
        }
        public async Task<CreateAppoinmentViewModel> CreateAppoinment()
        {
            return await _appoinmentRepo.CreateAppoinment();
        }
        public async Task<object> GetFilledTimings(DateTime date, int doctorId)
        {
            return await _appoinmentRepo.GetFilledTimings(date,doctorId);
        }
    }
}
