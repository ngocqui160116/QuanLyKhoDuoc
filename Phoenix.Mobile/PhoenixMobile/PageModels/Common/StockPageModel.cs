using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Stock;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Stock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class StockPageModel : BasePageModel
    {
        private readonly IStockService _StockService;
        private readonly IDialogService _dialogService;

        // public ObservableCollection<StockDto> Stock { get; set; }

        public StockPageModel(IStockService StockService, IDialogService dialogService)
        {
            _StockService = StockService;
            _dialogService = dialogService;

        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Kiểm kho";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            var data = await _StockService.GetAllStock(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Stocks = data;

                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Stocks));
            }
        }
            #region properties
            public List<StockModel> Stocks { get; set; } = new List<StockModel>();
            public StockRequest request { get; set; } = new StockRequest();
            public string Id { get; set; }
            public DateTime Date { get; set; }
        #endregion

        #region AddStockCommand

        public Command AddStockCommand => new Command(async (p) => await AddStockExecute(), (p) => !IsBusy);

        private async Task AddStockExecute()
        {
            await CoreMethods.PushPageModel<AddStockPageModel>();
        }
        #endregion

        #region SelectStock

        StockModel _selectedStock;

        public StockModel SelectedStock
        {
            get
            {
                return _selectedStock;
            }
            set
            {
                _selectedStock = value;
                if (value != null)
                    StockSelected.Execute(value);
            }
        }

        public Command<StockModel> StockSelected
        {
            get
            {
                return new Command<StockModel>(async (Stock) => {
                    //await CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn"+SelectedStock.Id, "Đóng");
                    await CoreMethods.PushPageModel<StockInfoPageModel>(Stock);
                });
            }
        }
        #endregion
    }
}
