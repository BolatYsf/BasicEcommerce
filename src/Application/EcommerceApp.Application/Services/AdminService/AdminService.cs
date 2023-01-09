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
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Services.AdminService
{
    public class AdminService:IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepo _employeeRepo;

        public AdminService(IMapper mapper,IEmployeeRepo employeeRepo)
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo;
        }

        public async Task CreateManager(AddManagerDTO addManagerDTO)
        {
           var addEmployee= _mapper.Map<Employee>(addManagerDTO);
            if (addEmployee.UploadPath!=null)
            {
                //dosya yolu okudum
                using var image = Image.Load(addManagerDTO.UploadPath.OpenReadStream());
                //resım boyutu ayarladım
                image.Mutate(x => x.Resize(600, 560)); //

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

        public async Task<List<ListOfManagerVM>> GetManager()
        {
            var managers = await _employeeRepo.GetFilteredList(
                select: x => new ListOfManagerVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Roles = x.Roles,
                    ImagePath=x.ImagePath

                }, where: x => ((x.Status == Status.Active || x.Status == Status.Modified) && x.Roles == Roles.Manager ), orderBy: x => x.OrderBy(x => x.Name));
            return managers;
        }

        public async Task<UpdateManagerDTO> GetManager(Guid id)
        {
            var manager =await _employeeRepo.GetFilteredFirstOrDefault(select:x=> new UpdateManagerVM
            {
                Id=x.Id,
                Name=x.Name,
                Surname=x.Surname,
                ImagePath=x.ImagePath,

            },where:x=>x.Id==id );

            var updateManagerDTO= _mapper.Map<UpdateManagerDTO>(manager);
            return updateManagerDTO;
        }

        public async Task UpdateManager(UpdateManagerDTO managerDTO)
        {
            var model =await _employeeRepo.GetDefault(x => x.Id == managerDTO.Id);

            model.Name = managerDTO.Name;
            model.Surname = managerDTO.Surname;
            
            model.UploadPath = managerDTO.UploadPath;
            model.Status = managerDTO.Status;

            using var image = Image.Load(managerDTO.UploadPath.OpenReadStream());
            //resım boyutu ayarladım
            image.Mutate(x => x.Resize(600, 560)); //

            Guid guid = Guid.NewGuid();
            image.Save($"wwwroot/images/{guid}.png");

            model.ImagePath = ($"/images/{guid}.png");

            await _employeeRepo.Update(model);
        }

        public async Task DeleteManager(Guid id)
        {
            var model =await _employeeRepo.GetDefault(x => x.Id == id );
            model.DeleteDate = DateTime.Now;
            model.Status = Status.Passive;
            
            await _employeeRepo.Delete(model);
        }

    }
}
