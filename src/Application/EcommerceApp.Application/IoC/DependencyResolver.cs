using Autofac;
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
            base.Load(builder);
        }
    }
}
