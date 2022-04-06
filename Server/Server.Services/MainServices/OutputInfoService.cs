using Falcon.Core;
using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;

using Phoenix.Shared.OutputInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IOutputInfoService
    {
        Task<BaseResponse<OutputInfoDto>> GetAllOutputInfo(OutputInfoRequest request);
        Task<CrudResult> CreateOutputInventory(OutputInfoRequest request);
        Task<CrudResult> CreateOutputInfo(OutputInfoRequest request);
    }
    public class OutputInfoService : IOutputInfoService
    {
        private readonly DataContext _dataContext;
        public OutputInfoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<OutputInfoDto>> GetAllOutputInfo(OutputInfoRequest request)
        {
            var result = new BaseResponse<OutputInfoDto>();
            try
            {

                // setup query
                var query = _dataContext.OutputInfos.AsQueryable();

                // filter
               
                //query = query.Where(d => d.Count.Equals(Soluong));

                query = query.OrderByDescending(d => d.Id);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<OutputInfoDto>();
            }
            catch (Exception ex)
            {

            }



            return result;
        }


        // Task<CrudResult> CreateOutputInfo(OutputInfoRequest request);
        public async Task<CrudResult> CreateOutputInfo(OutputInfoRequest request)
        {
            var Output = new Output();

            Output.IdStaff = request.IdStaff;
            Output.DateOutput = request.DateOutput;
            Output.IdReason = request.IdReason;
            Output.Status = request.Status;

            _dataContext.Outputs.Add(Output);
            await _dataContext.SaveChangesAsync();

            var OutputInfo = new OutputInfo();

            OutputInfo.IdOutput = Output.Id;
            OutputInfo.IdMedicine = request.IdMedicine;
            
            OutputInfo.Count = request.Count;
            OutputInfo.Total = request.Total;

            _dataContext.OutputInfos.Add(OutputInfo);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> CreateOutputInventory(OutputInfoRequest request)
        {
            var Output = new Output();

            Output.IdStaff = request.IdStaff;
            Output.DateOutput = request.DateOutput;
            Output.IdReason = request.IdReason;
            Output.Status = request.Status;

            _dataContext.Outputs.Add(Output);
            await _dataContext.SaveChangesAsync();

            var OutputInfo = new OutputInfo();

            OutputInfo.IdOutput = Output.Id;
            OutputInfo.IdMedicine = request.IdMedicine;
            OutputInfo.Count = request.Count;
            OutputInfo.Total = request.Total;

            _dataContext.OutputInfos.Add(OutputInfo);
            await _dataContext.SaveChangesAsync();

            var inventories = _dataContext.Inventories.ToList()
                                        .FindAll(d => d.IdMedicine == request.IdMedicine && d.LotNumber == request.IdBatch);

           var inventory = inventories.FirstOrDefault();

            inventory.IdMedicine = request.IdMedicine;
            inventory.Count = inventory.Count - request.Count;
            inventory.LotNumber = inventory.LotNumber;
            inventory.IdInputInfo = null;

            await _dataContext.SaveChangesAsync();

            var InventoryTags = new InventoryTags();
            InventoryTags.DocumentId = "PX00" + OutputInfo.Id;
            InventoryTags.ExpiredDate = DateTime.Now;
            InventoryTags.DocumentDate = DateTime.Now;
            InventoryTags.LotNumber = (int)inventory.LotNumber;
            InventoryTags.UnitPrice = 2;
            InventoryTags.TotalPrice = 2;
            //InventoryTags.SupplierId = 32;

            InventoryTags.DocumentType = request.IdReason;
            InventoryTags.MedicineId = request.IdMedicine;

            InventoryTags.Qty_After = inventory.Count;
            InventoryTags.Qty = request.Count;
            InventoryTags.Qty_Before = 0;


            _dataContext.InventoryTags.Add(InventoryTags);
            await _dataContext.SaveChangesAsync();

            return new CrudResult() { IsOk = true };
        }
    }
}
