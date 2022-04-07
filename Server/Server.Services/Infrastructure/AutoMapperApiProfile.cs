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

namespace Phoenix.Server.Services.Infrastructure
{
    public class AutoMapperApiProfile : Profile
    {
        public AutoMapperApiProfile()
        {
            CreateMap<Vendor, VendorDto>()
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.Medicine_Image.AbsolutePath));

            CreateMap<Reason, ReasonDto>();

            CreateMap<Group, GroupDto>();
            CreateMap<InventoryTags, InventoryTagsDto>()
                .ForMember(d => d.MedicineName, o => o.MapFrom(s => s.Medicine.Name));
            CreateMap<DocumentType, DocumentTypeDto>();
            CreateMap<Inventory, InventoryDto>()
                //.ForMember(d => d.LotNumber, o => o.MapFrom(s => s.Medicine.Name))
                .ForMember(d => d.MedicineName, o => o.MapFrom(s => s.Medicine.Name));
            CreateMap<Input, InputDto>()
                
                .ForMember(d => d.SupplierName, o => o.MapFrom(s => s.Supplier.Name))
                .ForMember(d => d.NameStaff, o => o.MapFrom(s => s.Staff.Name));
            CreateMap<InputInfo, InputInfoDto>();
               // .ForMember(d => d.InputName, o => o.MapFrom(s => s.Input.));
            CreateMap<Medicine, MedicineDto>()
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.Medicine_Image.RelativePath))
                .ForMember(d => d.GroupName, o => o.MapFrom(s => s.Group.Name))
                .ForMember(d => d.NameUnit, o => o.MapFrom(s => s.Unit.Name));

            CreateMap<Output, OutputDto>()
                .ForMember(d => d.NameReason, o => o.MapFrom(s => s.Reason.NameReason))
                .ForMember(d => d.NameStaff, o => o.MapFrom(s => s.Staff.Name));
            CreateMap<OutputInfo, OutputInfoDto>();
                //.ForMember(d => d.DueDate, o => o.MapFrom(s => s.InputInfo.DueDate))
                //.ForMember(d => d.InputPrice, o => o.MapFrom(s => s.InputInfo.InputPrice))
                //.ForMember(d => d.MedicineName, o => o.MapFrom(s => s.Medicine.Name));

            CreateMap<Staff, StaffDto>();

            CreateMap<Supplier, SupplierDto>();

            CreateMap<Unit, UnitDto>();
            CreateMap<MedicineItem, MedicineItemDto>();

        }
    }
}

