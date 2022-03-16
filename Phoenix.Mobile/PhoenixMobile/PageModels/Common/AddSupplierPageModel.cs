using Phoenix.Framework.Extensions;
using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Supplier;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AddSupplierPageModel : BasePageModel
    {
        private readonly ISupplierService _supplierService;
        private readonly IDialogService _dialogService;
        public AddSupplierPageModel(ISupplierService supplierService, IDialogService dialogService)
        {
            _supplierService = supplierService;
            _dialogService = dialogService;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thêm nhà cung cấp";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
        }

       


        #region SupplierCommand

        public Command SupplierCommand => new Command(async (p) => await SupplierExecute(), (p) => !IsBusy);

        private async Task SupplierExecute()
        {
            await CoreMethods.PushPageModel<SupplierPageModel>();
        }
        #endregion

        #region AddSupplierCommand
        public Command AddSupplierCommand => new Command(async (p) => await AddSupplierExecute(), (p) => !IsBusy);
        private async Task AddSupplierExecute()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;
                if (Name.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập tên nhà cung cấp");
                    IsBusy = false;
                    return;
                }

                if (PhoneNumber.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập số điện thoại");
                    IsBusy = false;
                    return;
                }
                if (Email.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập email");
                    IsBusy = false;
                    return;
                }
                if (Address.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập địa chỉ");
                    IsBusy = false;
                    return;
                }
                var data = await _supplierService.AddSupplier(new SupplierRequest
                {
                    Name = Name,
                    PhoneNumber = PhoneNumber,
                    Email = Email,
                    Address = Address
                });
                await CoreMethods.PushPageModel<SupplierPageModel>();
                await _dialogService.AlertAsync("Thêm thành công");
                IsBusy = false;
                
            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Thêm thất bại");
            }
        }
        #endregion


        #region properties
        public string SearchText { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        #endregion
    }

}
