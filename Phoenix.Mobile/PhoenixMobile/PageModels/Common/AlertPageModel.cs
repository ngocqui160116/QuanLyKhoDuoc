using Phoenix.Mobile.Core.Models.Vendor;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AlertPageModel : BasePageModel
    {
        public AlertPageModel()
        {
            
        }
        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "AlertPage";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            //TestString = "hello world";
        }

        #region Declaration         
        private string _mobilenumber = null;
        private int _limit = 8;
        #endregion
        public string MobileNumber
        {
            get { return _mobilenumber; }
            set
            {
                _mobilenumber = value;
                TextChangedCommand.Execute(_mobilenumber);
            }
        }
        public Command TextChangedCommand => new Command<string>(async (_mobilenumber) => await TextChanged(_mobilenumber));

        private async Task TextChanged(string p)
        {
            if (p.Length >= _limit)
                await CoreMethods.DisplayAlert("Your Bill amount ", "Info", "OK");
        }
    }
}
