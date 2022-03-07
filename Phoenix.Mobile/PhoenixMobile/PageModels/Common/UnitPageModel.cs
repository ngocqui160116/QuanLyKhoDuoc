using Phoenix.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class UnitPageModel: BasePageModel
    {
        public UnitPageModel()
        {

        }
        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Đơn vị tính";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
        }

        #region StoreCommand

        public Command StoreCommand => new Command(async (p) => await StoreExecute(), (p) => !IsBusy);

        private async Task StoreExecute()
        {
            await CoreMethods.PushPageModel<StorePageModel>();
        }
        #endregion
    }
}
