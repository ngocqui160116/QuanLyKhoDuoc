using AutoMapper;
using Phoenix.Server.Data.Entity;
using Phoenix.Shared.Vendor;
using Phoenix.Shared.Customer;
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

            CreateMap<Customer, CustomerDto>();

            CreateMap<Group, GroupDto>();

            CreateMap<Input, InputDto>();

            CreateMap<InputInfo, InputInfoDto>();

            CreateMap<Medicine, MedicineDto>();

            CreateMap<Output, OutputDto>();

            CreateMap<OutputInfo, OutputInfoDto>();

            CreateMap<Staff, StaffDto>();

            CreateMap<Supplier, SupplierDto>();

            CreateMap<Unit, UnitDto>();

        }
    }
}

