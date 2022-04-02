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
                //var inputList = _dataContext.InputInfos.Where(p => p.IdInput == request.Id);
                //var outputList = _dataContext.OutputInfos.Where(p => p.IdOutput == request.Id);

                //int sumInput = 0;
                //int sumOutput = 0;

                //if (inputList != null)
                //{
                //    sumInput = (int)inputList.Sum(p => p.Count);
                //}
                //if (outputList != null)
                //{
                //    sumOutput = (int)outputList.Sum(p => p.Count);
                //}

                //int Soluong = sumInput - sumOutput;

                // setup query
                var query = _dataContext.OutputInfos.AsQueryable();

                // filter
                if (!string.IsNullOrEmpty(request.IdOutput))
                {
                    query = query.Where(d => d.IdOutput.Contains(request.IdOutput));
                }
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
            Output.Id = request.IdOutput;
            Output.IdStaff = request.IdStaff;
            Output.DateOutput = request.DateOutput;
            Output.IdReason = request.IdReason;
            Output.Status = request.Status;

            _dataContext.Outputs.Add(Output);
            await _dataContext.SaveChangesAsync();

            var OutputInfo = new OutputInfo();
            OutputInfo.IdOutput = request.IdOutput;
            OutputInfo.IdMedicine = request.IdMedicine;
            OutputInfo.IdInputInfo = request.IdInputInfo;
            OutputInfo.Count = request.Count;
            OutputInfo.Total = request.Total;


            _dataContext.OutputInfos.Add(OutputInfo);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> CreateOutputInventory(OutputInfoRequest request)
        {
            var Output = new Output();
            Output.Id = request.IdOutput;
            Output.IdStaff = request.IdStaff;
            Output.DateOutput = request.DateOutput;
            Output.IdReason = request.IdReason;
            Output.Status = request.Status;

            _dataContext.Outputs.Add(Output);
            await _dataContext.SaveChangesAsync();

            var OutputInfo = new OutputInfo();
            OutputInfo.IdOutput = request.IdOutput;
            OutputInfo.IdMedicine = request.IdMedicine;
            OutputInfo.IdInputInfo = request.IdInputInfo;
            OutputInfo.Count = request.Count;
            OutputInfo.Total = request.Total;

            _dataContext.OutputInfos.Add(OutputInfo);
            await _dataContext.SaveChangesAsync();

            var inventory = _dataContext.Inventories.Find(request.IdMedicine);

            inventory.IdMedicine = request.IdMedicine;
            inventory.Count = inventory.Count - request.Count;

            await _dataContext.SaveChangesAsync();

            var InventoryTags = new InventoryTags();
            InventoryTags.DocumentId = "PX002";
            InventoryTags.ExpiredDate = DateTime.Now;
            InventoryTags.DocumentDate = DateTime.Now;
            InventoryTags.LotNumber = 2;
            InventoryTags.UnitPrice = 2;
            InventoryTags.TotalPrice = 2;
            InventoryTags.SupplierId = 32;

            InventoryTags.DocumentType = 1;
            InventoryTags.MedicineId = request.IdMedicine;
            InventoryTags.Qty_After = InputInfo.Count;
            InventoryTags.Qty = 0;
            InventoryTags.Qty_Before = inventory.Count;


            _dataContext.InventoryTags.Add(InventoryTags);
            await _dataContext.SaveChangesAsync();

            return new CrudResult() { IsOk = true };
        }
    }
}
