using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Staff;
using Phoenix.Mobile.Core.Services;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared;
using Phoenix.Shared.Staff;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class ProfilePageModel : BasePageModel
    {
        private readonly IDialogService _dialogService;
        private readonly IAuthService _authService;

        public ProfilePageModel(IDialogService dialogService, IAuthService authService)
        {

            _dialogService = dialogService;
            _authService = authService;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thông tin tài khoản";
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

            #region Users
            var data1 = await _authService.GetUserFromToken();
            if (data1 == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Users = data1;
                RaisePropertyChanged(nameof(Users));
            }
#endregion

        }
        #region properties
        public UserDto Users { get; set; } = new UserDto();
       // public SupplierRequest request { get; set; } = new SupplierRequest();

        #endregion
    }
}
