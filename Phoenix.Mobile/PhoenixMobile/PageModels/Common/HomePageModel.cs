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


        #region CustomerCommand

        public Command CustomerCommand => new Command(async (p) => await CustomerExecute(), (p) => !IsBusy);

        private async Task CustomerExecute()
        {
            await CoreMethods.PushPageModel<CustomerPageModel>();
        }
        #endregion


        #region InvoiceCommand

        public Command InvoiceCommand => new Command(async (p) => await InvoiceExecute(), (p) => !IsBusy);

        private async Task InvoiceExecute()
        {
            await CoreMethods.PushPageModel<InvoicePageModel>();
        }
        #endregion


        #region MedicineCommand

        public Command MedicineCommand => new Command(async (p) => await MedicineExecute(), (p) => !IsBusy);

        private async Task MedicineExecute()
        {
            await CoreMethods.PushPageModel<MedicinePageModel>();
        }
        #endregion


        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
          
        }
    }
    
}
