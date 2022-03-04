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
using Phoenix.Shared.Invoice;
using Phoenix.Mobile.Core.Models.Invoice;
using Phoenix.Shared.Invoice_Detail;
using Phoenix.Mobile.Core.Models.Invoice_Detail;
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
            CreateMap<MedicineDto, MedicineModel>();
            CreateMap<StaffDto, StaffModel>();
            CreateMap<CustomerDto, CustomerModel>();
            CreateMap<InvoiceDto, InvoiceModel>();
            CreateMap<Invoice_DetailDto, Invoice_DetailModel>();
            CreateMap<GroupDto, GroupModel>();
            CreateMap<UnitDto, UnitModel>();
        }
    }
}
