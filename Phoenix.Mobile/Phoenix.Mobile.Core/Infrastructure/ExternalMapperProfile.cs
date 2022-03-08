using AutoMapper;
using Phoenix.Mobile.Core.Models.Common;
using Phoenix.Mobile.Core.Models.Setting;
using Phoenix.Mobile.Core.Models.Vendor;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Vendor;
using Phoenix.Shared.Medicine;
using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Shared.Staff;
using Phoenix.Mobile.Core.Models.Staff;
using Phoenix.Shared.Customer;
using Phoenix.Mobile.Core.Models.Customer;
using Phoenix.Shared.Input;
using Phoenix.Mobile.Core.Models.Input;
using Phoenix.Shared.InputInfo;
using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Shared.Output;
using Phoenix.Mobile.Core.Models.Output;
using Phoenix.Shared.OutputInfo;
using Phoenix.Mobile.Core.Models.OutputInfo;
using Phoenix.Shared.Supplier;
using Phoenix.Mobile.Core.Models.Supplier;
using Phoenix.Shared.Group;
using Phoenix.Mobile.Core.Models.Group;
using Phoenix.Shared.Unit;
using Phoenix.Mobile.Core.Models.Unit;

namespace Phoenix.Mobile.Core.Infrastructure
{
    public class ExternalMapperProfile : Profile
    {
        public ExternalMapperProfile()
        {

            //mapping dto to model
            CreateMap<CrudResult, CrudResultModel>();
            

            //setting
            CreateMap<SettingDto, SettingModel>();
            CreateMap<VendorDto, VendorModel>();
            CreateMap<CustomerDto, CustomerModel>();
            CreateMap<GroupDto, GroupModel>();
            CreateMap<MedicineDto, MedicineModel>();
            CreateMap<StaffDto, StaffModel>();
            CreateMap<InputDto, InputModel>();
            CreateMap<InputInfoDto, InputInfoModel>();
            CreateMap<SupplierDto, SupplierModel>();
            CreateMap<UnitDto, UnitModel>();
        }
    }
}
