using AutoMapper;
using Phoenix.Server.Data.Entity;
using Phoenix.Shared.Vendor;
using Phoenix.Shared.Reason;
using Phoenix.Shared.Medicine;
using Phoenix.Shared.Staff;
using Phoenix.Shared.Group;
using Phoenix.Shared.Unit;
using Phoenix.Shared.Input;
using Phoenix.Shared.Output;
using Phoenix.Shared.OutputInfo;
using Phoenix.Shared.Supplier;
using Phoenix.Shared.InputInfo;

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

            CreateMap<Input, InputDto>()
                .ForMember(d => d.NameStaff, o => o.MapFrom(s => s.Staff.Name));
            CreateMap<InputInfo, InputInfoDto>()
                //.ForMember(d => d.NameSupplier, o => o.MapFrom(s => s.Supplier.Name))
                //.ForMember(d => d.NameMedicine, o => o.MapFrom(s => s.Medicine.Name))
                .ForMember(d => d.DateInput, o => o.MapFrom(s => s.Input.DateInput))
                .ForMember(d => d.SDK, o => o.MapFrom(s => s.Medicine.RegistrationNumber));
            CreateMap<Medicine, MedicineDto>()
                .ForMember(d => d.GroupName, o => o.MapFrom(s => s.Group.Name))
                .ForMember(d => d.NameUnit, o => o.MapFrom(s => s.Unit.Name));
               


            CreateMap<Output, OutputDto>()
             .ForMember(d => d.NameStaff, o => o.MapFrom(s => s.Staff.Name));
            CreateMap<OutputInfo, OutputInfoDto>();

            CreateMap<Staff, StaffDto>();

            CreateMap<Supplier, SupplierDto>();

            CreateMap<Unit, UnitDto>();

        }
    }
}

