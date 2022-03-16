using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class EditSupplierPageModel : BasePageModel
    {
       
        private readonly IDialogService _dialogService;

        public EditSupplierPageModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

        }

        public override async void Init(object initData)
        {
            //var Medicine = initData as Medicines;
            //base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thông tin nhà cung cấp";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

        }

    }
}

