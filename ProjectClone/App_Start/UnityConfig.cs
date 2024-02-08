
using ProjectClone.Areas.Admin.Utility;
using ProjectClone.Areas.Common.Utility;
using ProjectClone.Areas.User.Utility;
using ProjectClone.Utility;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace ProjectClone
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<ICommonRepo, CommonRepo>();
            container.RegisterType<IUserRepo, UserRepo>();
            container.RegisterType<IAdminRepo, AdminRepo>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}