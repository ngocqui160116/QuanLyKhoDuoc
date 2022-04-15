using Phoenix.Mobile.Core.Models.Stock;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Stock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class StockInfoPageModel : BasePageModel
    {

        public override async void Init(object initData)
        {
            if (initData != null)
            {
                Stock = (StockModel)initData;
            }
            else
            {
                Stock = new StockModel();
            }
            //base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Chi tiết Kiểm kho";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            if (IsBusy) return;
            IsBusy = true;
#if DEBUG
            StockInfos = Stock.StockInfo;

            IdStock = Stock.Id;
            NameReason = Stock.NameReason;
            NameStaff = Stock.NameStaff;
            DateStock = Stock.DateStock;

#endif
            IsBusy = false;

        }

        #region properties
        public StockModel Stock { get; set; }
        public List<StockInfoDto> StockInfos { get; set; }

        public string SearchText { get; set; }
        public int IdStock { get; set; }
        public string NameReason { get; set; }
        public string Status { get; set; }
        public string SupplierName { get; set; }
        public DateTime DateStock { get; set; }
        public string NameStaff { get; set; }
        public double Total { get; set; }
        #endregion
    }
}
