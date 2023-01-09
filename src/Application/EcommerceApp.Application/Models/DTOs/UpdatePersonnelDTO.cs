using EcommerceApp.Application.Extensions;
using EcommerceApp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Models.DTOs
{
    public class UpdatePersonnelDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Bos Gecilemez")]
        [MaxLength(25, ErrorMessage = "25 karakterden fazla giremezsiniz!!!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Cannot Empty")]
        [MaxLength(50, ErrorMessage = "50 karakterden fazla giremezsiniz!!!")]
        public string Surname { get; set; }

        public DateTime? UpdateDate => DateTime.Now;

        public Status Status => Status.Modified;
        public Roles Roles => Roles.Personnel;
        public string? ImagePath { get; set; }

        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }
    }
}
