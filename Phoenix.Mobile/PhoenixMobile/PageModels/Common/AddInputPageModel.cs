using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Mobile.Core.Models.Supplier;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.InputInfo;
using Phoenix.Shared.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AddInputPageModel :BasePageModel
    {
        private readonly ISupplierService _supplierService;
        private readonly IInputInfoService _inputInfoService;
        private readonly IDialogService _dialogService;

        public AddInputPageModel(ISupplierService supplierService, IInputInfoService inputInfoService, IDialogService dialogService)
        {
            _supplierService = supplierService;
            _inputInfoService = inputInfoService;
            _dialogService = dialogService;

        }
        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thêm phiếu nhập";
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

            var data1 = await _inputInfoService.GetAllInputInfo(InputInfoRequest);
            if (data1 == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                InputInfos = data1;

                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(InputInfos));
            }
        }

        #region properties
        public List<SupplierModel> Suppliers { get; set; } = new List<SupplierModel>();
        public SupplierRequest request { get; set; } = new SupplierRequest();

        public static List<InputInfoModel> InputInfos { get; set; } = new List<InputInfoModel>();
        public InputInfoRequest InputInfoRequest { get; set; } = new InputInfoRequest();

        #endregion

        #region Search

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            SearchResults = GetSearchResults(query);
        });

        // public static List<InputModel> Fruits { get; set; } 
        public static List<InputInfoModel> GetSearchResults(string queryString)
        {
            var normalizedQuery = queryString?.ToLower() ?? "";
            return InputInfos.Where(f => f.MedicineName.Contains(normalizedQuery)).ToList();
        }

        List<InputInfoModel> searchResults = InputInfos;
        public List<InputInfoModel> SearchResults
        {
            get
            {
                return searchResults;
            }
            set
            {
                searchResults = value;
            }
        }

        #endregion
    }
}
