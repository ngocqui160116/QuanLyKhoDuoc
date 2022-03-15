using Phoenix.Mobile.Core.Models.Reason;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Reason;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IReasonService
    {
        Task<List<ReasonModel>> GetAllReason(ReasonRequest request);
    }

    public class ReasonService : IReasonService
    {
        private readonly IReasonProxy _ReasonProxy;
        public ReasonService(IReasonProxy ReasonProxy)
        {
            _ReasonProxy = ReasonProxy;
        }
        public async Task<List<ReasonModel>> GetAllReason(ReasonRequest request)
        {
            var reason = await _ReasonProxy.GetAllReason(request);
            return reason.Data.MapTo<ReasonModel>();
        }

        
    }
}
