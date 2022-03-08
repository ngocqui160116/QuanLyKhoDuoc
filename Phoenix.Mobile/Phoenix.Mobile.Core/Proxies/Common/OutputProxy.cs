using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Output;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IOutputProxy
    {
        Task<List<OutputDto>> GetAllOutput(OutputRequest request);
    }

    public class OutputProxy : BaseProxy, IOutputProxy
    {
        public async Task<List<OutputDto>> GetAllOutput(OutputRequest request)
        {
            try
            {
                var api = RestService.For<IOutputApi>(GetHttpClient());
                var result = await api.GetAllOutput(request);
                if (result == null) return new List<OutputDto>();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new List<OutputDto>();
            }
        }
        public interface IOutputApi
        {
            [Post("/output/GetAllOutput")]
            Task<List<OutputDto>> GetAllOutput([Body] OutputRequest request);

        }
    }
}
