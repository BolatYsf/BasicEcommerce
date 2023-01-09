using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Services.AdminService
{
    public interface IAdminService
    {
        Task CreateManager(AddManagerDTO addManagerDTO);
  

        Task<List<ListOfManagerVM>> GetManager();
       

        Task<UpdateManagerDTO> GetManager(Guid id);
        Task UpdateManager(UpdateManagerDTO managerDTO);

        Task DeleteManager(Guid id);
    }
}
