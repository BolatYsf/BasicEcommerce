using Autofac;
using AutoMapper;
using EcommerceApp.Application.AutoMapper;
using EcommerceApp.Application.Services.AdminService;
using EcommerceApp.Application.Services.LoginService;
using EcommerceApp.Application.Services.ManagerService;
using EcommerceApp.Domain.Repositories;
using EcommerceApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.IoC
{
    //nested 
    public class DependencyResolver:Module
    {
        //ıoc---yani interface cagırdıgım zaman onun concrete yapısnı getirmesi gerektigini islemi burada soyluyorum
        //builder.RegisterType<BaseRepo>().As<IBaseRepo>().InstancePerlifeTimeScope();

        //program.cs tarafından yapacagım eklenmesi buradan yapabilirsin

        


        protected override void Load(ContainerBuilder builder)
        {
            //repository addscope

            builder.RegisterType<EmployeeRepo>().As<IEmployeeRepo>().InstancePerLifetimeScope();

            //service addscope

            builder.RegisterType<AdminService>().As<IAdminService>().InstancePerLifetimeScope();
            builder.RegisterType<ManagerService>().As<IManagerService>().InstancePerLifetimeScope();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //Register Mapper Profile
                //Mapping dosyamızıda buraya ekliyoruz gidip startup'ta eklemek zorunda kalmayalım zaten burasının görevi oraya sağlamak olacak.
                cfg.AddProfile<Mapping>();
            }
            )).AsSelf().SingleInstance();



            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();

            builder.RegisterType<LoginService>().As<ILoginService>().InstancePerLifetimeScope();

            base.Load(builder);
        }

    }
}
