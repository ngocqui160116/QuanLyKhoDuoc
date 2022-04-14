using AutoMapper;
using Phoenix.Server.Data.Entity;
using Phoenix.Shared.Vendor;
using Phoenix.Shared.Reason;
using Phoenix.Shared.Medicine;
using Phoenix.Shared.Staff;
using Phoenix.Shared.Group;
using Phoenix.Shared.Unit;
using Phoenix.Shared.Inventory;
using Phoenix.Shared.Input;
using Phoenix.Shared.Output;
using Phoenix.Shared.OutputInfo;
using Phoenix.Shared.Supplier;
using Phoenix.Shared.InputInfo;
using Phoenix.Shared.InventoryTags;
using Phoenix.Shared.DocumentType;
using Phoenix.Shared.MedicineItem;
using Phoenix.Shared;
using Falcon.Web.Core.Auth;
using Phoenix.Shared.Stock;

namespace Phoenix.Server.Services.Infrastructure
{
    public class AutoMapperApiProfile : Profile
    {
        public AutoMapperApiProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Vendor, VendorDto>()
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.Medicine_Image.AbsolutePath));

            CreateMap<Reason, ReasonDto>();

            CreateMap<Group, GroupDto>();
            CreateMap<InventoryTags, InventoryTagsDto>()
                .ForMember(d => d.DocumentTypeName, o => o.MapFrom(s => s.documentType.Name))
                .ForMember(d => d.MedicineName, o => o.MapFrom(s => s.Medicine.Name));
            CreateMap<DocumentType, DocumentTypeDto>();
            CreateMap<Inventory, InventoryDto>()
                .ForMember(d => d.UnitPrice, o => o.MapFrom(s => s.InputInfo.InputPrice))
                .ForMember(d => d.HSD, o => o.MapFrom(s => s.InputInfo.DueDate))
                .ForMember(d => d.MedicineName, o => o.MapFrom(s => s.Medicine.Name));
            CreateMap<Input, InputDto>()
                
                .ForMember(d => d.SupplierName, o => o.MapFrom(s => s.Supplier.Name))
                .ForMember(d => d.NameStaff, o => o.MapFrom(s => s.Staff.Name));
            CreateMap<InputInfo, InputInfoDto>()
                .ForMember(d => d.UnitName, o => o.MapFrom(s => s.Medicine.Unit.Name));
            CreateMap<Medicine, MedicineDto>()
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.Medicine_Image.RelativePath))
                .ForMember(d => d.GroupName, o => o.MapFrom(s => s.Group.Name))
                .ForMember(d => d.NameUnit, o => o.MapFrom(s => s.Unit.Name));

            CreateMap<Output, OutputDto>()
                .ForMember(d => d.NameReason, o => o.MapFrom(s => s.Reason.NameReason))
                .ForMember(d => d.NameStaff, o => o.MapFrom(s => s.Staff.Name));
            CreateMap<OutputInfo, OutputInfoDto>()
                .ForMember(d => d.Inventory_Id, o => o.MapFrom(s => s.Inventory.Id))
                .ForMember(d => d.Batch, o => o.MapFrom(s => s.Inventory.LotNumber))
                 .ForMember(d => d.InputPrice, o => o.MapFrom(s => s.Inventory.UnitPrice))
                .ForMember(d => d.DueDate, o => o.MapFrom(s => s.Inventory.InputInfo.DueDate))
                .ForMember(d => d.MedicineName, o => o.MapFrom(s => s.Inventory.Medicine.Name));

            CreateMap<Staff, StaffDto>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.UserName))
                .ForMember(d => d.Roles, o => o.MapFrom(s => s.User.Roles))
                .ForMember(d => d.User_Id, o => o.MapFrom(s => s.User.Id));

            CreateMap<Supplier, SupplierDto>();

            CreateMap<Unit, UnitDto>();
            CreateMap<MedicineItem, MedicineItemDto>();
            CreateMap<Stock, StockDto>();
            CreateMap<StockInfo, StockInfoDto>();

        }
    }
}

