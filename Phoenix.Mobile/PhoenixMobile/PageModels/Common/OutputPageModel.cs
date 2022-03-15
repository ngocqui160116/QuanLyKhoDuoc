using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.OutputInfo;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.OutputInfo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class OutputPageModel : BasePageModel 
    {
        private readonly IOutputInfoService _OutputInfoService;
        private readonly IDialogService _dialogService;

        public OutputPageModel(IOutputInfoService OutputInfoService, IDialogService dialogService)
        {
            _OutputInfoService = OutputInfoService;
            _dialogService = dialogService;

        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Danh sách phiếu xuất";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            var data = await _OutputInfoService.GetAllOutputInfo(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Outputs = data;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Outputs));
            }
        }

        #region properties
        public List<OutputInfoModel> Outputs { get; set; } = new List<OutputInfoModel>();
        public OutputInfoRequest request { get; set; } = new OutputInfoRequest();

        #endregion

        #region AddOutputCommand

        public Command AddOutputCommand => new Command(async (p) => await AddOutputExecute(), (p) => !IsBusy);

        private async Task AddOutputExecute()
        {
            //await CoreMethods.PushPageModel<AddOutputPageModel>();
        }
        #endregion
    }
}
