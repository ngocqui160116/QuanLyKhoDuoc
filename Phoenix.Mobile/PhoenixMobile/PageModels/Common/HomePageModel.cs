using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Vendor;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Mobile.Pages.Auth;
using Phoenix.Mobile.Pages.Common;
using System;
using System.Collections.Generic;
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


        #region InventoryCommand

        public Command InventoryCommand => new Command(async (p) => await InventoryExecute(), (p) => !IsBusy);

        private async Task InventoryExecute()
        {
            await CoreMethods.PushPageModel<InventoryPageModel>();
        }
        #endregion

        #region InputCommand

        public Command InputCommand => new Command(async (p) => await InputExecute(), (p) => !IsBusy);

        private async Task InputExecute()
        {
            await CoreMethods.PushPageModel<InputPageModel>();
        }
        #endregion


        #region MedicineCommand

        public Command MedicineCommand => new Command(async (p) => await MedicineExecute(), (p) => !IsBusy);

        private async Task MedicineExecute()
        {
            await CoreMethods.PushPageModel<MedicinePageModel>();
        }
        #endregion


       
    }
    
}
