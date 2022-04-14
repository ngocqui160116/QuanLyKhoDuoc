using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Mobile.Core.Models.MedicineItem;
using Phoenix.Mobile.Core.Models.Staff;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.MedicineItem;
using Phoenix.Shared.Staff;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AddStockPageModel : BasePageModel
    {
        private readonly IMedicineItemService _medicineItemService;
        private readonly IStaffService _staffService;
        private readonly IDialogService _dialogService;

        public AddStockPageModel( IMedicineItemService medicineItemService, IStaffService staffService, IDialogService dialogService)
        {
            _medicineItemService = medicineItemService;
            _staffService = staffService;
            _dialogService = dialogService;

        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thêm phiếu kiểm kho";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {

            #region MedicineItem
            var data2 = await _medicineItemService.GetAllMedicineItem(MedicineItemRequest);
            if (data2 == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                MedicineItems = data2;
                RaisePropertyChanged(nameof(MedicineItems));
            }
            #endregion


            #region Staff

            var data1 = await _staffService.GetAllStaff(StaffRequest);
            if (data1 == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");

            }
            else
            {
                Staffs = data1;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Staffs));
            }

            #endregion
        }

        #region properties
        public List<MedicineItemModel> MedicineItems { get; set; } = new List<MedicineItemModel>();
        public MedicineItemRequest MedicineItemRequest { get; set; } = new MedicineItemRequest();
        public List<MedicineModel> ListMedicine { get; set; }
        public List<StaffModel> Staffs { get; set; } = new List<StaffModel>();
        public StaffRequest StaffRequest { get; set; } = new StaffRequest();
        public MedicineItemModel MedicineItem { get; set; }

        #endregion

        #region properties
        public int IdMedicine { get; set; }
        public string Names { get; set; }
        public int IdStaff { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        #endregion

        #region AddMedicineCommand

        public Command AddMedicineCommand => new Command(async (p) => await AddMedicineExecute(), (p) => !IsBusy);

        private async Task AddMedicineExecute()
        {
            await CoreMethods.PushPageModel<InventoryPageModel>();
        }
        #endregion

        #region SelectStaff

        StaffModel _selectedStaff;
        public StaffModel SelectedStaff
        {
            get
            {
                return _selectedStaff;
            }
            set
            {
                _selectedStaff = value;
                if (value != null)
                    IdStaff = value.IdStaff;
            }
        }
        #endregion

    }
}
