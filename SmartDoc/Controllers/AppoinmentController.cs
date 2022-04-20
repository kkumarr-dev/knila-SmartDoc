using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartDoc.Models;
using SmartDoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDoc.Controllers
{
    [Authorize]
    public class AppoinmentController : Controller
    {
        private readonly IAppoinmentService _appoinmentService;
        public AppoinmentController(IAppoinmentService appoinmentService)
        {
            _appoinmentService = appoinmentService;
        }
        public async Task<IActionResult> Index()
        {
            var res = await _appoinmentService.GetAppoinments();
            return View(res);
        }
        public async Task<IActionResult> AllAppoinments()
        {
            var res = await _appoinmentService.GetAllAppoinments();
            return PartialView("Appoinments",res);
        }

        public async Task<IActionResult> Appoinments()
        {
            var res = await _appoinmentService.GetAppoinments();
            return PartialView(res);
        }
        public async Task<IActionResult> GetAppoinmentById(int appointId)
        {
            var res = await _appoinmentService.GetAppoinmentById(appointId);
            return View();
        }
        public async Task<IActionResult> CreateAppoinment()
        {
            var res = await _appoinmentService.CreateAppoinment();
            return PartialView(res);
        }
        public async Task<IActionResult> PerformAppoinment(AppoinmentViewModel model)
        {
            var res = await _appoinmentService.PerformAppoinment(model);
            return Ok(res);
        }
        public async Task<IActionResult> ApproveAppoinment(int appointId)
        {
            var res = false;
            var data = await _appoinmentService.GetAppoinmentById(appointId);
            if (data != null)
            {
                data.StatusId = 2;
                res = await _appoinmentService.PerformAppoinment(data);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RejectAppoinment(int appointId)
        {
            var res = false;
            var data = await _appoinmentService.GetAppoinmentById(appointId);
            if (data != null)
            {
                data.StatusId = 3;
                res = await _appoinmentService.PerformAppoinment(data);
            }
            return RedirectToAction("Index");
        }
    }
}
