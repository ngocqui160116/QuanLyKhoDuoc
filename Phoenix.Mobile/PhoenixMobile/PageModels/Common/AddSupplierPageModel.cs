using Phoenix.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AddSupplierPageModel : BasePageModel
    {
        public AddSupplierPageModel()
        {
           
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
    }

}
