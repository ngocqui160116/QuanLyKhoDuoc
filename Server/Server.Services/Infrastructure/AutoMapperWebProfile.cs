using AutoMapper;
using Phoenix.Server.Data.Entity;
using Phoenix.Shared.Reason;
using Phoenix.Shared.Group;
using Phoenix.Shared.Input;
using Phoenix.Shared.InputInfo;
using Phoenix.Shared.Medicine;
using Phoenix.Shared.Output;
using Phoenix.Shared.OutputInfo;
using Phoenix.Shared.Staff;
using Phoenix.Shared.Supplier;
using Phoenix.Shared.Unit;
using Phoenix.Shared.InventoryTags;
using Phoenix.Shared.Inventory;
using Phoenix.Shared.Stock;
using Phoenix.Shared.StockInfo;

namespace Phoenix.Server.Services.Infrastructure
{
    public class AutoMapperWebProfile : Profile
    {
        public AutoMapperWebProfile()
        {
            //Import
            CreateMap<Reason, ReasonDto>();
            CreateMap<Supplier, SupplierDto>();
            CreateMap<Medicine, MedicineDto>()
                .ForMember(d => d.GroupName, o => o.MapFrom(s => s.Group.Name));
            CreateMap<Group, GroupDto>();
            CreateMap<Staff, StaffDto>();
            CreateMap<Input, InputDto>();
            CreateMap<Output, OutputDto>()
                .ForMember(d => d.NameReason, o => o.MapFrom(s => s.Reason.NameReason))
                .ForMember(d => d.NameStaff, o => o.MapFrom(s => s.Staff.Name));
            CreateMap<InputInfo, InputInfoDto>()
                .ForMember(d => d.MedicineName, o => o.MapFrom(s => s.Medicine.Name));
            CreateMap<Input, InputDto>()
                .ForMember(d => d.NameStaff, o => o.MapFrom(s => s.Staff.Name))
                .ForMember(d => d.SupplierName, o => o.MapFrom(s => s.Supplier.Name));
            //.ForMember(d => d.SupplierName, o => o.MapFrom(s => s.Supplier.Name));
            CreateMap<OutputInfo, OutputInfoDto>()
               .ForMember(d => d.MedicineName, o => o.MapFrom(s => s.Medicine.Name))
               .ForMember(d => d.Batch, o => o.MapFrom(s =>s.Inventory.LotNumber));

            CreateMap<Unit, UnitDto>();
            CreateMap<InventoryTags, InventoryTagsDto>()
                .ForMember(d => d.MedicineName, o => o.MapFrom(s => s.Medicine.Name))
                .ForMember(d => d.DocumentTypeName, o => o.MapFrom(s => s.documentType.Name));
            CreateMap<Inventory, InventoryDto>()
                .ForMember(d => d.HSD, o => o.MapFrom(s => s.InputInfo.DueDate))
                .ForMember(d => d.MedicineName, o => o.MapFrom(s => s.Medicine.Name));
            //.ForMember(d => d.SupplierName, o => o.MapFrom(s => s.Supplier.Name));
            CreateMap<Stock, StockDto>()
                .ForMember(d => d.StaffName, o => o.MapFrom(s => s.Staff.Name));
            CreateMap<StockInfo, StockInfoDto>()
                .ForMember(d => d.MedicineName, o => o.MapFrom(s => s.Inventory.Medicine.Name))
                .ForMember(d => d.Batch, o => o.MapFrom(s => s.Inventory.LotNumber))
                .ForMember(d => d.UnitName, o => o.MapFrom(s => s.Inventory.Medicine.Unit.Name));
        }
    }
}

