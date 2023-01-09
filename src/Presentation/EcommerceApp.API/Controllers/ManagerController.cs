using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Models.VMs;
using EcommerceApp.Application.Services.AdminService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcommerceApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public ManagerController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("GetManagers")]

        public async Task<ActionResult<List<ListOfManagerVM>>> GetAllManager()
        {
            var manager = await _adminService.GetManager();
            if (manager==null)
            {
                return NotFound();
            }
            return Ok(manager);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UpdateManagerDTO>> GetManager(Guid id)
        {
            var manager = await _adminService.GetManager(id);
            if (manager == null)
            {
                return NotFound();
            }
            return Ok(manager);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UpdateManagerDTO>> DeleteManager([FromBody]Guid id)
        {
            await  _adminService.DeleteManager(id);
        
            return Ok();
        }

        //[HttpPost]
        //public async Task<ActionResult> CreateManager([FromBody]AddManagerDTO addManagerDTO)
        //{
        //    try
        //    {
        //        await _adminService.CreateManager(addManagerDTO);
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
                
        //    }
        //    return Ok(addManagerDTO);
        //}
    }
    
}
