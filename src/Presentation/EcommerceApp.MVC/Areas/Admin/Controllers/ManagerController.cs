using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Services.AdminService;
using EcommerceApp.Application.Services.ManagerService;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManagerController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IManagerService _managerService;
        public ManagerController(IAdminService adminService, IManagerService managerService)
        {
            _adminService = adminService;
            _managerService = managerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddManager()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddManager(AddManagerDTO addManagerDTO)
        {
            if (ModelState.IsValid)
            {
                await _adminService.CreateManager(addManagerDTO);
                return RedirectToAction(nameof(ListOfManagers));
            }
            
            return View();
        }

        public async Task<IActionResult> AddPersonnel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonnel(AddPersonelDTO addPersonelDTO)
        {
            if (ModelState.IsValid)
            {
                await _managerService.CreatePersonel(addPersonelDTO);
                return RedirectToAction(nameof(ListOfPersonnel));
            }

            return View();
        }

        public async Task<IActionResult> ListOfPersonnel()
        {
            var personnels = await _managerService.GetPersonnel();
            return View(personnels);
        }




        public async Task<IActionResult> ListOfManagers()
        {
            var managers = await _adminService.GetManager();
            return View(managers);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateManager(Guid id)
        {
            var updateManager =await _adminService.GetManager(id);
            return View(updateManager);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateManager(UpdateManagerDTO updateManagerDTO)
        {
            if (ModelState.IsValid)
            {
                await _adminService.UpdateManager(updateManagerDTO);

                return RedirectToAction(nameof(ListOfManagers));
            }
             return View(updateManagerDTO);
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePersonnel(Guid id)
        {
            var updatePersonnel = await _managerService.GetPersonnel(id);
            return View(updatePersonnel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePersonnel(UpdatePersonnelDTO updatePersonnelDTO)
        {
            if (ModelState.IsValid)
            {
                await _managerService.UpdatePersonnel(updatePersonnelDTO);

                return RedirectToAction(nameof(ListOfPersonnel));
            }
            return View(updatePersonnelDTO);
        }
        public async Task<IActionResult> DeleteManager(Guid id)
        {
            await _adminService.DeleteManager(id);
            return RedirectToAction(nameof(ListOfManagers));
        }

        public async Task<IActionResult> DeletePersonnel(Guid id)
        {
            await _managerService.DeletePersonnel(id);
            return RedirectToAction(nameof(ListOfPersonnel));
        }
    }
}
