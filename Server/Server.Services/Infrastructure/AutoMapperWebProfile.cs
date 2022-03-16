using AutoMapper;
using Phoenix.Server.Data.Entity;
using Phoenix.Shared.Customer;
using Phoenix.Shared.Group;
using Phoenix.Shared.Input;
using Phoenix.Shared.InputInfo;
using Phoenix.Shared.Medicine;
using Phoenix.Shared.Output;
using Phoenix.Shared.OutputInfo;
using Phoenix.Shared.Staff;
using Phoenix.Shared.Supplier;
using Phoenix.Shared.Unit;

namespace Phoenix.Server.Services.Infrastructure
{
    public class AutoMapperWebProfile : Profile
    {
        public AutoMapperWebProfile()
        {
            //Import
            CreateMap<Customer, CustomerDto>();
            CreateMap<Supplier, SupplierDto>();
            CreateMap<Medicine, MedicineDto>()
                .ForMember(d => d.GroupName, o => o.MapFrom(s => s.Group.Name))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.Medicine_Image.AbsolutePath)); 
            CreateMap<Group, GroupDto>();
            CreateMap<Staff, StaffDto>();
            CreateMap<Input, InputDto>();
            CreateMap<Output, OutputDto>();
            CreateMap<InputInfo, InputInfoDto>()
               .ForMember(d => d.MedicineName, o => o.MapFrom(s => s.Medicine.Name))
               .ForMember(d => d.SupplierName, o => o.MapFrom(s => s.Supplier.Name));
            CreateMap<OutputInfo, OutputInfoDto>()
               .ForMember(d => d.MedicineName, o => o.MapFrom(s => s.Medicine.Name));
               //.ForMember(d => d.CustomerName, o => o.MapFrom(s => s.Customer.Name));
            CreateMap<Unit, UnitDto>();
            CreateMap<Medicine_Image, Medicine_ImageDto>();
            /*CreateMap<Vendor, VendorDto>()
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.Medicine_Image.AbsolutePath));*/
        }
    }
}

