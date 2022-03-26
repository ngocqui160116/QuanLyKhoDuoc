using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Supplier;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.OutputInfo;
using Phoenix.Shared.Supplier;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AddOutputPageModel : BasePageModel
    {
        private readonly ISupplierService _supplierService;
        private readonly IOutputInfoService _outputInfoService;
        private readonly IDialogService _dialogService;

        public AddOutputPageModel(ISupplierService supplierService, IOutputInfoService outputInfoService , IDialogService dialogService)
        {
            _supplierService = supplierService;
            _outputInfoService = outputInfoService;
            _dialogService = dialogService;

        }
        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thêm phiếu xuất";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            var data = await _supplierService.GetAllSupplier(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Suppliers = data;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Suppliers));
            }
        }

        #region properties
        public List<SupplierModel> Suppliers { get; set; } = new List<SupplierModel>();
        public SupplierRequest request { get; set; } = new SupplierRequest();
        
        public int IdReason { get; set; }
        public DateTime HSD { get; set; } = DateTime.Now;
        #endregion

        #region AddOutputCommand
        public Command AddOutputCommand => new Command(async (p) => await AddOutputExecute(), (p) => !IsBusy);
        private async Task AddOutputExecute()
        {
            try
            {

                var data = await _outputInfoService.AddOutputInfo(new OutputInfoRequest
                {
                    Id = "HDX003",
                    IdMedicine = 21,
                    IdInputInfo = 1,
                    IdReason = 2,
                    Total = 2000,
                    IdStaff = 3,
                    Count = 100,
                    DateOutput = HSD
                }); ;
                await CoreMethods.PushPageModel<OutputPageModel>();

                await _dialogService.AlertAsync("Thêm thành công");
                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Thêm thất bại");
            }
        }
        #endregion
    }
}
