using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Services;
using Phoenix.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AccountPageModel : BasePageModel
    {

        private readonly IDialogService _dialogService;
        private readonly IAuthService _authService;

        public AccountPageModel(IDialogService dialogService, IAuthService authService)
        {
            _dialogService = dialogService;
            _authService = authService;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Cài đặt";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
        }

        #region ProfileCommand

        public Command ProfileCommand => new Command(async (p) => await ProfileExecute(), (p) => !IsBusy);

        private async Task ProfileExecute()
        {
            await CoreMethods.PushPageModel<ProfilePageModel>();
        }
        #endregion

        #region LogOutCommand

        public Command LogOutCommand => new Command(async (p) => await LogOutExecute(), (p) => !IsBusy);

        private async Task LogOutExecute()
        {
             // _authService.LogOut();
                Device.BeginInvokeOnMainThread(() =>
                {
                    NavigationHelpers.ToLoginPage();
                });
        }
        #endregion
    }

}
