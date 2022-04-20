using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartDoc.Entities;
using SmartDoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDoc.Repository
{
    public class AppoinmentRepo : IAppoinmentRepo
    {
        private readonly AppDBContext _dBContext;
        public AppoinmentRepo(AppDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<List<AppoinmentViewModel>> GetAllAppoinments()
        {
            var model = new List<AppoinmentViewModel>();
            var data = await (from a in _dBContext.Appointments
                              join d in _dBContext.Users on a.DoctorId equals d.UserId
                              join p in _dBContext.Users on a.PatientId equals p.UserId
                              select new AppoinmentViewModel
                              {
                                  AppoinmentId = a.AppoinmentId,
                                  FromDateTime = a.FromDateTime,
                                  ToDateTime = a.ToDateTime,
                                  CreatedDate = a.CreatedDate,
                                  DoctorId = a.DoctorId,
                                  IsActive = a.IsActive,
                                  PatientId = a.PatientId,
                                  StatusId = a.StatusId,
                                  UpdatedDate = a.UpdatedDate,
                                  DoctorName = $"{d.FirstName} {d.LastName}",
                                  PatientName = $"{p.FirstName} {p.LastName}",
                                  age = $"{(DateTime.Now.Year - p.DateOfBirth.Year)} Years",
                                  AppoinmentDate = a.FromDateTime.Date
                              }).ToListAsync();
            return data;
        }
        public async Task<bool> PerformAppoinment(AppoinmentViewModel ap)
        {
            var res = false;
            var checkAp = await _dBContext.Appointments.Where(x => x.AppoinmentId == ap.AppoinmentId).FirstOrDefaultAsync();
            if (checkAp != null)
            {
                checkAp.FromDateTime = ap.AppoinmentDate.Date + ap.FromDateTime.TimeOfDay;
                checkAp.ToDateTime = ap.AppoinmentDate.Date + ap.ToDateTime.TimeOfDay;
                checkAp.DoctorId = ap.DoctorId;
                checkAp.PatientId = ap.PatientId;
                checkAp.UpdatedDate = DateTime.Now;
                checkAp.StatusId = ap.StatusId;
                _dBContext.Appointments.Update(checkAp);
                res = await _dBContext.SaveChangesAsync() > 0;
            }
            else
            {
                var dbAp = new Appointments
                {
                    FromDateTime = ap.AppoinmentDate.Date + ap.FromDateTime.TimeOfDay,
                    ToDateTime = ap.AppoinmentDate.Date + ap.ToDateTime.TimeOfDay,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    StatusId = 1,
                    UpdatedDate = DateTime.Now,
                    DoctorId = ap.DoctorId,
                    PatientId = _dBContext.UserId
                };
                await _dBContext.Appointments.AddAsync(dbAp);
                res = await _dBContext.SaveChangesAsync() > 0;
            }
            return res;
        }

        public async Task<List<AppoinmentViewModel>> GetAppoinments()
        {
            var data = await GetAllAppoinments();
            if (_dBContext.RoleId == 2)
            {
                data = data.Where(x => x.DoctorId == _dBContext.UserId).ToList();
            }
            else
            {
                data = data.Where(x => x.PatientId == _dBContext.UserId).ToList();
            }
            return data;
        }
        public async Task<List<AppoinmentViewModel>> GetAppoinmentByPatientId(int patientId)
        {
            var data = await GetAllAppoinments();
            data = data.Where(x => x.PatientId == patientId).ToList();
            return data;
        }
        public async Task<List<AppoinmentViewModel>> GetAppoinmentByDoctorId(int doctorId)
        {
            var data = await GetAllAppoinments();
            data = data.Where(x => x.DoctorId == doctorId).ToList();
            return data;
        }
        public async Task<AppoinmentViewModel> GetAppoinmentById(int appointId)
        {
            var allData = await GetAllAppoinments();
            var data = allData.Where(x => x.AppoinmentId == appointId).FirstOrDefault();
            return data;
        }
        public async Task<CreateAppoinmentViewModel> CreateAppoinment()
        {
            var docData = await _dBContext.Users.Where(x => x.RoleId == 2)
                .Select(x => new SelectListItem
                {
                    Value = x.UserId.ToString(),
                    Text = $"{ x.FirstName } { x.LastName }"
                }).ToListAsync();
            var data = new CreateAppoinmentViewModel
            {
                DoctorList = docData
            };
            return data;
        }
    }
}
