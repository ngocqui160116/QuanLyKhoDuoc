using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Supplier;
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
    public class EditSupplierPageModel : BasePageModel
    {
        private readonly ISupplierService _supplierService;
        private readonly IDialogService _dialogService;

        public EditSupplierPageModel(ISupplierService supplierService, IDialogService dialogService)
        {
            _supplierService = supplierService;
            _dialogService = dialogService;
        }

        public override async void Init(object initData)
        {
            //base.Init(initData);
            if (initData != null)
            {
                Supplier = (SupplierModel)initData;
            }
            else
            {
                Supplier = new SupplierModel();
            }
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thông tin nhà cung cấp";
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
            IdSupplier = Supplier.IdSupplier;
            Name = Supplier.Name;
            PhoneNumber = Supplier.PhoneNumber;
            Email = Supplier.Email;
            Address = Supplier.Address;

#endif
            IsBusy = false;
        }

        #region properties
        public SupplierModel Supplier { get; set; }
        public bool IsEnabled { get; set; } = false;
        public int IdSupplier { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        #endregion

        #region EditCommand
        public Command EditCommand => new Command(async (p) => await EditExecute(), (p) => !IsBusy);
        private async Task EditExecute()
        {
            IsEnabled = true;

        }
        #endregion

        #region UpdateSupplierCommand
        public Command UpdateSupplierCommand => new Command(async (p) => await UpdateSupplierExecute(), (p) => !IsBusy);
        private async Task UpdateSupplierExecute()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;

                var data = await _supplierService.UpdateSupplier(IdSupplier, new SupplierRequest
                {
                    IdSupplier = IdSupplier,
                    Name = Name,
                    PhoneNumber = PhoneNumber,
                    Email = Email,
                    Address = Address
                });
                await CoreMethods.PushPageModel<SupplierPageModel>();
                await _dialogService.AlertAsync("Cập nhật thành công");
                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Cập nhật thất bại");
            }
        }
        #endregion

        #region DeleteSupplierCommand
        public Command DeleteSupplierCommand => new Command(async (p) => await DeleteSupplierExecute(), (p) => !IsBusy);
        private async Task DeleteSupplierExecute()
        {
            //await CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn:" + IdSupplier, "Đóng");
            try
            {
                if (IsBusy) return;
                IsBusy = true;

                var data = await _supplierService.DeleteSupplier(IdSupplier);
                await CoreMethods.PushPageModel<SupplierPageModel>();
                await _dialogService.AlertAsync("Xóa thành công");
                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Xóa thất bại");
            }
        }
        #endregion
    }
}