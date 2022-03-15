using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
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
        Task<BaseResponse<OutputDto>> GetAllOutput(OutputRequest request);
    }

    public class OutputProxy : BaseProxy, IOutputProxy
    {
        public async Task<BaseResponse<OutputDto>> GetAllOutput(OutputRequest request)
        {
            try
            {
                var api = RestService.For<IOutputApi>(GetHttpClient());

                return await api.GetAllOutput(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public interface IOutputApi
        {
            [Post("/output/GetAllOutput")]
            Task<BaseResponse<OutputDto>> GetAllOutput([Body] OutputRequest request);

        }

    }
}
