using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Invoice;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Invoice;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class InvoicePageModel : BasePageModel
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IDialogService _dialogService;

        public InvoicePageModel(IInvoiceService invoiceService, IDialogService dialogService)
        {
            _invoiceService = invoiceService;
            _dialogService = dialogService;

        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Danh sách Hóa đơn nhập";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            var data = await _invoiceService.GetAllInvoice(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Invoices = data;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Invoices));
            }
        }

        #region properties
        public List<InvoiceModel> Invoices { get; set; } = new List<InvoiceModel>();
        public InvoiceRequest request { get; set; } = new InvoiceRequest();

        #endregion

        #region AddInvoiceCommand

        public Command AddInvoiceCommand => new Command(async (p) => await AddInvoiceExecute(), (p) => !IsBusy);

        private async Task AddInvoiceExecute()
        {
            await CoreMethods.PushPageModel<AddInvoicePageModel>();
        }
        #endregion
    }
}
