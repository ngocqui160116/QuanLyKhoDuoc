using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class HomePageModel : BasePageModel
    {
        private readonly IVendorService _vendorService;
        private readonly IDialogService _dialogService;
        public HomePageModel(IDialogService dialogService, IVendorService vendorService)
        {
            _dialogService = dialogService;
            _vendorService = vendorService;
        }
        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "HomePage";
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

        #region MedicineCommand

        public Command MedicineCommand => new Command(async (p) => await MedicineExecute(), (p) => !IsBusy);

        private async Task MedicineExecute()
        {
            await CoreMethods.PushPageModel<MedicinePageModel>();
        }
        #endregion

        #region WarehouseCommand

        public Command WarehouseCommand => new Command(async (p) => await WarehouseExecute(), (p) => !IsBusy);

        private async Task WarehouseExecute()
        {
            await CoreMethods.PushPageModel<WarehousePageModel>();
        }
        #endregion

    }

}
