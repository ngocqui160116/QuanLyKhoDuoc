﻿using Falcon.Web.Core.Helpers;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.InventoryTags;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IInventoryTagsService
    {

        Task<BaseResponse<InventoryTagsDto>> GetAllInventoryTags(InventoryTagsRequest request);
    }
    public class InventoryTagsService : IInventoryTagsService
    {
        private readonly DataContext _dataContext;
        public InventoryTagsService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<BaseResponse<InventoryTagsDto>> GetAllInventoryTags(InventoryTagsRequest request)
        {
            var result = new BaseResponse<InventoryTagsDto>();

            try
            {
                // setup query
                var query = _dataContext.InventoryTags.AsQueryable();

                // filter
                

                query = query.OrderByDescending(d => d.Id);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<InventoryTagsDto>();

            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}