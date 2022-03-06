using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Customer;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Customer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class CustomerPageModel : BasePageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IDialogService _dialogService;
        public CustomerPageModel(ICustomerService customerService, IDialogService dialogService)
        {
            _customerService = customerService;
            _dialogService = dialogService;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Nhà cung cấp";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            var data = await _customerService.GetAllCustomer(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Customers = data;
                //RaisePropertyChanged("Customers");
                RaisePropertyChanged(nameof(Customers));
            }
        }

        #region properties
        public List<CustomerModel> Customers { get; set; } = new List<CustomerModel>();

        public CustomerRequest request { get; set; } = new CustomerRequest();

        public string SearchText { get; set; }
        #endregion

        #region AddCustomerCommand

        public Command AddCustomerCommand => new Command(async (p) => await AddCustomerExecute(), (p) => !IsBusy);

        private async Task AddCustomerExecute()
        {
            await CoreMethods.PushPageModel<AddCustomerPageModel>();
        }
        #endregion

       
    }
}
