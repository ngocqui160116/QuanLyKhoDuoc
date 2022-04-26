using Phoenix.Framework.Extensions;
using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Services;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Auth
{
   
    public class RegisterPageModel : BasePageModel
    {
        private readonly IDialogService _dialogService;
        private readonly IUserService _userService;

        public RegisterPageModel(IDialogService dialogService, IUserService userService)
        {
            _dialogService = dialogService;
            _userService = userService;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Đăng ký tài khoản";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e); 
        }

        #region AddUserCommand
        public Command AddUserCommand => new Command(async (p) => await AddUserExecute(), (p) => !IsBusy);
        private async Task AddUserExecute()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;
                if (UserName.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập tên đăng nhập");
                    IsBusy = false;
                    return;
                }

                if (Password.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập mật khẩu");
                    IsBusy = false;
                    return;
                }
                if (Name.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập họ tên");
                    IsBusy = false;
                    return;
                }
                if (Phone.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập số điện thoại");
                    IsBusy = false;
                    return;
                }
                if (Password.Length < 6)
                {
                    await CoreMethods.DisplayAlert("Lỗi", "Mật khẩu phải lớn hơn 6 ký tự", "OK");
                    IsBusy = false;
                    return;
                }

                if (Phone.CheckPhoneNumber() == false)
                {
                    await CoreMethods.DisplayAlert("Lỗi", "Vui lòng nhập đúng số điện thoại", "OK");
                    IsBusy = false;
                    return;
                }

                var data = await _userService.CreateUser(new UserRequest
                {
                    UserName = UserName,
                    Name = Name,
                    Password = Password,
                    PhoneNumber = Phone
                });
                await CoreMethods.PopPageModel();
                _dialogService.Toast("Đăng ký thành công");
                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Đăng ký thất bại");

            }
        }
        #endregion

        #region Properties
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        #endregion
    }
}
