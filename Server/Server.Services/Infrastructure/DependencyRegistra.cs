using Falcon.Web.Core.Caching;
using Falcon.Web.Core.Infrastructure;
using Falcon.Web.Core.Log;
using Falcon.Web.Core.Security;
using Falcon.Web.Core.Settings;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.Framework;
using SimpleInjector;
using SettingService = Phoenix.Server.Services.Framework.SettingService;
using Falcon.Services.Users;
using Falcon.Core.Data;
using Falcon.Core.Domain.Users;
using Phoenix.Server.Services.MainServices.Users;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Server.Services.MainServices;

namespace Phoenix.Server.Services.Infrastructure
{
    public static class DependencyRegistra
    {
        public static void Register(Container container)
        {
            //DB
            container.Register<DataContext>(Lifestyle.Scoped);
            //Framework
            container.Register<ILogger, Logger>(Lifestyle.Scoped);
            container.Register<ITokenValidation, TokenValidation>(Lifestyle.Scoped);
            container.Register<ICacheManager, MemoryCacheManager>(Lifestyle.Scoped);
            container.Register<IEncryptionService, EncryptionService>(Lifestyle.Scoped);
            container.Register<ISettingService, SettingService>(Lifestyle.Scoped);
            //Register service in dll
            container.Register<IUserService, UserService>(Lifestyle.Scoped);
            container.Register<UserAuthService>(Lifestyle.Scoped);
            //to use in web admin
            container.Register<SettingService>(Lifestyle.Scoped);
            container.Register<IVendorService, VendorService>(Lifestyle.Scoped);
            container.Register<IReasonService, ReasonService>(Lifestyle.Scoped);
            container.Register<IGroupService, GroupService>(Lifestyle.Scoped);
            container.Register<IInputService, InputService>(Lifestyle.Scoped);
            container.Register<IInputInfoService, InputInfoService>(Lifestyle.Scoped);
            container.Register<IStaffService, StaffService>(Lifestyle.Scoped);
            container.Register<IOutputService, OutputService>(Lifestyle.Scoped);
            container.Register<IOutputInfoService, OutputInfoService>(Lifestyle.Scoped);
            container.Register<ISupplierService, SupplierService>(Lifestyle.Scoped);
            container.Register<IMedicineService, MedicineService>(Lifestyle.Scoped);
            container.Register<IUnitService, UnitService>(Lifestyle.Scoped);

            EngineContext.Current.Init(new SimpleContainer(container));
        }
        public static void ApiServerRegister(Container container)
        {
            Register(container);
            
        }
    }
}