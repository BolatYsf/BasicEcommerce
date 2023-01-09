using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Services.ManagerService
{
    public interface IManagerService
    {
        Task CreatePersonel(AddPersonelDTO addPersonelDTO);


        Task<List<ListOfPersonelVM>> GetPersonnel();


        Task<UpdatePersonnelDTO> GetPersonnel(Guid id);
        Task UpdatePersonnel(UpdatePersonnelDTO updatePersonnelDTO);

        Task DeletePersonnel(Guid id);
    }
}
