using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.AutoMapper
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            //eslestirme islemleri gerceklestirilecek
            //hangi turden gelirse digerine otomatik cevir
            //CreateMap<T,TResult>().ReverseMap();
        }
    }
}
