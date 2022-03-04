using Phoenix.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AccountPageModel : BasePageModel
    {
        public AccountPageModel(string Name)
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
    }

}
