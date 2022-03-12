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
    public class AddInputPageModel :BasePageModel
    {
        private readonly ISupplierService _supplierService;
        private readonly IDialogService _dialogService;

        public AddInputPageModel(ISupplierService supplierService, IDialogService dialogService)
        {
            _supplierService = supplierService;
            _dialogService = dialogService;

        }
        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thêm phiếu nhập";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            var data = await _supplierService.GetAllSupplier(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Suppliers = data;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Suppliers));
            }
        }

        #region properties
        public List<SupplierModel> Suppliers { get; set; } = new List<SupplierModel>();
        public SupplierRequest request { get; set; } = new SupplierRequest();

        #endregion

        #region StoreCommand

        public Command StoreCommand => new Command(async (p) => await StoreExecute(), (p) => !IsBusy);

        private async Task StoreExecute()
        {
            await CoreMethods.PushPageModel<StorePageModel>();
        }
        #endregion
    }
}
