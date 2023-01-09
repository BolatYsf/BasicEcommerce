using AutoMapper;
using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Models.VMs;
using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Enums;
using EcommerceApp.Domain.Repositories;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Services.ManagerService
{
    public class ManagerService : IManagerService
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IMapper _mapper;

        public ManagerService(IEmployeeRepo employeeRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }


        public async Task CreatePersonel(AddPersonelDTO addPersonelDTO)
        {
            var addEmployee = _mapper.Map<Employee>(addPersonelDTO);
            if (addEmployee.UploadPath != null)
            {
                
                using var image = Image.Load(addPersonelDTO.UploadPath.OpenReadStream());
              
                image.Mutate(x => x.Resize(600, 560)); 

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.png");

                addEmployee.ImagePath = ($"/images/{guid}.png");

                await _employeeRepo.Create(addEmployee);

            }

            else
            {
                addEmployee.ImagePath = ($"/images/default.png");
                await _employeeRepo.Create(addEmployee);
            }

        }

        public async Task DeletePersonnel(Guid id)
        {
            var getPersonnel=await _employeeRepo.GetDefault(x=>x.Id==id);
            getPersonnel.DeleteDate = DateTime.Now;
            getPersonnel.Status = Domain.Enums.Status.Passive;
            await _employeeRepo.Delete(getPersonnel);

        }

        public async Task<List<ListOfPersonelVM>> GetPersonnel()
        {
            var personnels = await _employeeRepo.GetFilteredList(select: x => new ListOfPersonelVM
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Roles = x.Roles,
                ImagePath = x.ImagePath
            }, where: x => ((x.Status == Status.Active || x.Status == Status.Modified) && x.Roles == Roles.Personnel), orderBy: x => x.OrderBy(x => x.Name));

            return personnels;
        }

        public async Task<UpdatePersonnelDTO> GetPersonnel(Guid id)
        {
            var personnel = await _employeeRepo.GetFilteredFirstOrDefault(select: x => new UpdatePersonnelDTO
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
           
                ImagePath = x.ImagePath,

            }, where: x => x.Id == id);

            var updatePersonnelDTO = _mapper.Map<UpdatePersonnelDTO>(personnel);
            return updatePersonnelDTO;
        }

        public async Task UpdatePersonnel(UpdatePersonnelDTO updatePersonnelDTO)
        {
            var model = await _employeeRepo.GetDefault(x => x.Id == updatePersonnelDTO.Id);

            model.Name = updatePersonnelDTO.Name;
            model.Surname = updatePersonnelDTO.Surname;

            model.UploadPath = updatePersonnelDTO.UploadPath;
            model.Status = updatePersonnelDTO.Status;

            using var image = Image.Load(updatePersonnelDTO.UploadPath.OpenReadStream());
           
            image.Mutate(x => x.Resize(600, 560)); //

            Guid guid = Guid.NewGuid();
            image.Save($"wwwroot/images/{guid}.png");

            model.ImagePath = ($"/images/{guid}.png");

            await _employeeRepo.Update(model);
        }
    }
}
