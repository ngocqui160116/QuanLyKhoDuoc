using AutoMapper;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Web.Areas.Admin.Models.Input;
using Phoenix.Server.Web.Areas.Admin.Models.InputInfo;
using Phoenix.Server.Web.Areas.Admin.Models.Inventory;
using Phoenix.Server.Web.Areas.Admin.Models.InventoryTags;
using Phoenix.Server.Web.Areas.Admin.Models.Medicine;
using Phoenix.Server.Web.Areas.Admin.Models.Output;
using Phoenix.Server.Web.Areas.Admin.Models.Stock;
using Phoenix.Server.Web.Areas.Admin.Models.StockInfo;
using Phoenix.Server.Web.Areas.Admin.Models.Supplier;
using Phoenix.Server.Web.Areas.Admin.Models.Unit;

namespace Phoenix.Server.Web.Infrastructure
{
    public class AutoMapperExtendWebProfile : Profile
    {
        public AutoMapperExtendWebProfile()
        {
            CreateMap<Supplier, SupplierModel>();
            CreateMap<Medicine, MedicineModel>();
            CreateMap<Input, InputModel>();
            CreateMap<InputInfo, InputInfoModel>();
            CreateMap<Output, OutputModel>();
            CreateMap<OutputInfo, OutputInfoModel>();
            CreateMap<InventoryTags, InventoryTagsModel>();
            CreateMap<Inventory, InventoryModel>();
            CreateMap<Stock, StockModel>();
            CreateMap<StockInfo, StockInfoModel>();
            CreateMap<Unit, UnitModel>();
        }
    }
}