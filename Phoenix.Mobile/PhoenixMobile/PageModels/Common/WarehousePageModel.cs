using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class WarehousePageModel : BasePageModel
    {
        private readonly IVendorService _vendorService;
        private readonly IDialogService _dialogService;
        public WarehousePageModel(IDialogService dialogService, IVendorService vendorService)
        {
            _dialogService = dialogService;
            _vendorService = vendorService;
        }
        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Kho";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

        }


        #region StockCommand

        public Command StockCommand => new Command(async (p) => await StockExecute(), (p) => !IsBusy);

        private async Task StockExecute()
        {
            await CoreMethods.PushPageModel<AddInputInfoPageModel>();
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

        #region OutputCommand

        public Command OutputCommand => new Command(async (p) => await OutputExecute(), (p) => !IsBusy);

        private async Task OutputExecute()
        {
            await CoreMethods.PushPageModel<OutputPageModel>();
        }
        #endregion


       
    }
}
