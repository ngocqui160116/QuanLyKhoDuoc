using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Unit;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Unit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class UnitPageModel: BasePageModel
    {
        private readonly IUnitService _unitService;
        private readonly IDialogService _dialogService;

        public UnitPageModel(IUnitService unitService, IDialogService dialogService)
        {
            _unitService = unitService;
            _dialogService = dialogService;

        }
      
        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Đơn vị tính";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            var data = await _unitService.GetAllUnit(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Units = data;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Units));
            }
        }

        #region SaveCommand

        public Command SaveCommand => new Command(async (p) => await SaveExecute(), (p) => !IsBusy);

        private async Task SaveExecute()
        {
            await CoreMethods.PushPageModel<MedicinePageModel>();
        }
        #endregion

        #region properties
        public List<UnitModel> Units { get; set; } = new List<UnitModel>();
        public UnitRequest request { get; set; } = new UnitRequest();

        #endregion
    }
}
