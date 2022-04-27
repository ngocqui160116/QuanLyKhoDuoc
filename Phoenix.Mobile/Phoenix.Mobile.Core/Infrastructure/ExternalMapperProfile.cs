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
using Phoenix.Shared.Reason;
using Phoenix.Mobile.Core.Models.Reason;
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
using Phoenix.Shared.Inventory;
using Phoenix.Mobile.Core.Models.Inventory;
using Phoenix.Shared.InventoryTags;
using Phoenix.Mobile.Core.Models.InventoryTags;
using Phoenix.Shared.MedicineItem;
using Phoenix.Mobile.Core.Models.MedicineItem;
using Phoenix.Shared.Stock;
using Phoenix.Mobile.Core.Models.Stock;
using Phoenix.Shared.StockInfo;
using Phoenix.Mobile.Core.Models.StockInfo;
using Phoenix.Shared;
using Phoenix.Mobile.Core.Models.User;

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
            CreateMap<ReasonDto, ReasonModel>();
            CreateMap<GroupDto, GroupModel>();
            CreateMap<MedicineDto, MedicineModel>();
              //  .ForMember(d => d.Supplier, o => o.MapFrom(s => s.Supplier.Name));
            CreateMap<StaffDto, StaffModel>();
            CreateMap<InputDto, InputModel>();
            CreateMap<InputInfoDto, InputInfoModel>();
            CreateMap<OutputDto, OutputModel>();
            CreateMap<OutputInfoDto, OutputInfoModel>();
            CreateMap<SupplierDto, SupplierModel>();
            CreateMap<UnitDto, UnitModel>();
            CreateMap<InventoryTagsDto, InventoryTagsModel>();
            CreateMap<InventoryDto, InventoryModel>();
            CreateMap<MedicineItemDto, MedicineItemModel>();
            CreateMap<StockDto, StockModel>();
            CreateMap<StockInfoDto, StockInfoModel>();
            CreateMap<UserDto, UserModel>();
        }
    }
}
