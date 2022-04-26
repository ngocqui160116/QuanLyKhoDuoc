using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Input;
using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Input;
using Phoenix.Shared.InputInfo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class InputInfoPageModel : BasePageModel
    {
        private readonly IInputInfoService _inputInfoService;
        private readonly IInputService _inputService;
        private readonly IDialogService _dialogService;
        public InputInfoPageModel(IInputService inputService, IInputInfoService inputInfoService, IDialogService dialogService)
        {
            _inputService = inputService;
            _inputInfoService = inputInfoService;
            _dialogService = dialogService;
        }
        public override async void Init(object initData)
        {
            //base.Init(initData);
            if (initData != null)
            {
                Input = (InputModel)initData;
            }
            else
            {
                Input = new InputModel();
            }
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Chi tiết phiếu nhập";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {


            if (IsBusy) return;
            IsBusy = true;
#if DEBUG
            InputInfos = Input.InputInfo;
            IdInput = Input.Id;
            SupplierName = Input.SupplierName;
            NameStaff = Input.NameStaff;
            DateInput = Input.DateInput;
            Status = Input.Status;
#endif
            IsBusy = false;

            if(Status == "Chờ duyệt")
            {
                IsEnabled = true;
                color = Color.Black;
            }
            else if(Status == "Đã hủy")
            {
                color = Color.Red;
            }
            else
            {
                color = Color.ForestGreen;
            }

        }

        #region properties
        public bool IsEnabled { get; set; }
        public InputModel Input { get; set; }
        public List<InputInfoDto> InputInfos { get; set; }
        public string SearchText { get; set; }
        public int IdInput { get; set; }
        public string SupplierName { get; set; }
        public string Status { get; set; }
        public DateTime DateInput { get; set; }
        public string NameStaff { get; set; }
        public double Total { get; set; }
        public Color color { get; set; }
        public List<InputInfoModel> inputInfoModels { get; set; } = new List<InputInfoModel>();
        public InputInfoRequest request { get; set; } = new InputInfoRequest();
        #endregion

        #region UpdateStatusCommand
        public Command UpdateStatusCommand => new Command(async (p) => await UpdateStatusExecute(), (p) => !IsBusy);
        private async Task UpdateStatusExecute()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;

                var data = await _inputService.UpdateStatus(IdInput, new InputRequest
                {
                    Status = "Đã hủy"                 
                });
                IsEnabled = false;
                await CoreMethods.PushPageModel<InputPageModel>();
                await _dialogService.AlertAsync("Cập nhật thành công");
                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Cập nhật thất bại");
            }
        }
        #endregion

        #region UpdateStatusCommand
        public Command CompleteCommand => new Command(async (p) => await CompleteExecute(), (p) => !IsBusy);
        private async Task CompleteExecute()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;

                var data = await _inputInfoService.Complete(IdInput, request);
                IsEnabled = false;
                await CoreMethods.PushPageModel<InputPageModel>();
                await _dialogService.AlertAsync("Thêm thành công");
                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Thêm thất bại");
            }
        }
        #endregion
    }
}
