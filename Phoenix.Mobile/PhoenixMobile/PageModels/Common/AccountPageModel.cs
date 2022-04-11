using Phoenix.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AccountPageModel : BasePageModel
    {
        
        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Cài đặt";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
        }

        #region ProfileCommand

        public Command ProfileCommand => new Command(async (p) => await ProfileExecute(), (p) => !IsBusy);

        private async Task ProfileExecute()
        {
            await CoreMethods.PushPageModel<ProfilePageModel>();
        }
        #endregion
    }

}
