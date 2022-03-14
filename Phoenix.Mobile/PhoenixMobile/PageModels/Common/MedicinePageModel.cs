using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Mobile.Core.Models.Unit;
using Phoenix.Mobile.Core.Models.Vendor;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Medicine;
using Phoenix.Shared.Unit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{

    public class MedicinePageModel : BasePageModel
    {
        private readonly IMedicineService _medicineService;
        private readonly IDialogService _dialogService;
        
        public MedicinePageModel(IMedicineService medicineService, IDialogService dialogService)
        {
            _medicineService = medicineService;
            _dialogService = dialogService;

        }
        
        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Danh mục thuốc";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            var data = await _medicineService.GetAllMedicine(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Medicines = data;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Medicines));
            }
        }

        #region properties
        public List<MedicineModel> Medicines { get; set; } = new List<MedicineModel>();
        public MedicineRequest request { get; set; } = new MedicineRequest();

        #endregion

        #region AddMedicineCommand

        public Command AddMedicineCommand => new Command(async (p) => await AddMedicineExecute(), (p) => !IsBusy);

        private async Task AddMedicineExecute()
        {
            await CoreMethods.PushPageModel<AddMedicinePageModel>();
        }
        #endregion

        #region SelectedItemCommand

        public Command SelectedItemCommand => new Command(async (p) => await SelectedItemCommandExecute(), (p) => !IsBusy);

        private async Task SelectedItemCommandExecute()
        {
            
            await CoreMethods.PushPageModel<EditMedicinePageModel>(Medicines);
        }
        #endregion

        
    }
}
