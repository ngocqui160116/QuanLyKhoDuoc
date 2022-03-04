using AutoMapper;
using Phoenix.Server.Data.Entity;
using Phoenix.Shared.Vendor;
using Phoenix.Shared.Customer;
using Phoenix.Shared.Invoice;
using Phoenix.Shared.Invoice_Detail;
using Phoenix.Shared.Medicine;
using Phoenix.Shared.Staff;
using Phoenix.Shared.Group;
using Phoenix.Shared.Unit;

namespace Phoenix.Server.Services.Infrastructure
{
    public class AutoMapperApiProfile : Profile
    {
        public AutoMapperApiProfile()
        {
            CreateMap<Vendor, VendorDto>()
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.Medicine_Image.AbsolutePath));

            CreateMap<Customer, CustomerDto>();
            //.ForMember(d => d.IdCustomer, o => o.MapFrom(s => s.IdCustomer))
            //.ForMember(d => d.Name, o => o.MapFrom(s => s.Name));

            CreateMap<Group, GroupDto>();
            //// .ForMember(d => d.IdGroup, o => o.MapFrom(s => s.IdGroup))
            //.ForMember(d => d.Name, o => o.MapFrom(s => s.Name));

            CreateMap<Invoice, InvoiceDto>();
            //.ForMember(d => d.IdInvoice, o => o.MapFrom(s => s.IdInvoice))
            //.ForMember(d => d.IdStaff, o => o.MapFrom(s => s.IdStaff))
            //.ForMember(d => d.IdCustomer, o => o.MapFrom(s => s.IdCustomer));

            CreateMap<Invoice_Detail, Invoice_DetailDto>();
            //.ForMember(d => d.IdInvoice, o => o.MapFrom(s => s.IdInvoice))
            //.ForMember(d => d.IdMedicine, o => o.MapFrom(s => s.IdMedicine))
            //.ForMember(d => d.Unit, o => o.MapFrom(s => s.Unit));

            CreateMap<Medicine, MedicineDto>();
            //.ForMember(d => d.IdMedicine, o => o.MapFrom(s => s.IdMedicine))
            //.ForMember(d => d.RegistrationNumber, o => o.MapFrom(s => s.RegistrationNumber))
            //.ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            //.ForMember(d => d.IdGroup, o => o.MapFrom(s => s.IdGroup))
            //.ForMember(d => d.IdCustomer, o => o.MapFrom(s => s.IdCustomer))
            //.ForMember(d => d.Unit, o => o.MapFrom(s => s.Unit))
            //.ForMember(d => d.DateOfManufacture, o => o.MapFrom(s => s.DateOfManufacture))
            //.ForMember(d => d.DueDate, o => o.MapFrom(s => s.DueDate))
            //.ForMember(d => d.Status, o => o.MapFrom(s => s.Status));

            CreateMap<Staff, StaffDto>();
            //.ForMember(d => d.IdStaff, o => o.MapFrom(s => s.IdStaff))
            //.ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            //.ForMember(d => d.Gender, o => o.MapFrom(s => s.Gender))
            //.ForMember(d => d.Authority, o => o.MapFrom(s => s.Authority));

            CreateMap<Unit, UnitDto>();
               //.ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
               //.ForMember(d => d.Name, o => o.MapFrom(s => s.Name));
        }
    }
}

