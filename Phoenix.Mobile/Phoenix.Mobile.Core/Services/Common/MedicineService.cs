﻿using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Medicine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IMedicineService
    {
        Task<List<MedicineModel>> GetAllMedicine(MedicineRequest request);
        Task<MedicineModel> AddMedicine(MedicineRequest request);
        Task<MedicineModel> UpdateMedicine(int IdMedicine, MedicineRequest request);
    }

    public class MedicineService : IMedicineService
    {
        private readonly IMedicineProxy _MedicineProxy;
        public MedicineService(IMedicineProxy MedicineProxy)
        {
            _MedicineProxy = MedicineProxy;
        }
        public async Task<List<MedicineModel>> GetAllMedicine(MedicineRequest request)
        {
            var medicine = await _MedicineProxy.GetAllMedicine(request);
            return medicine.Data.MapTo<MedicineModel>();
        }
        public async Task<MedicineModel> AddMedicine(MedicineRequest request)
        {
            var data = await _MedicineProxy.AddMedicine(request);
            return data.MapTo<MedicineModel>();
        }
        public async Task<MedicineModel> UpdateMedicine(int IdMedicine, MedicineRequest request)
        {
            var data = await _MedicineProxy.UpdateMedicine(IdMedicine, request);
            return data.MapTo<MedicineModel>();
        }
    }
}
