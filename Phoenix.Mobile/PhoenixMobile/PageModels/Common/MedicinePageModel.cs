using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Mobile.Core.Models.Unit;
using Phoenix.Mobile.Core.Models.Vendor;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Medicine;
using Phoenix.Shared.MedicineItem;
using Phoenix.Shared.Unit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{

    public class MedicinePageModel : BasePageModel
    {
        private readonly IMedicineService _medicineService;
        private readonly IMedicineItemService _medicineItemService;
        private readonly IDialogService _dialogService;
        //public ObservableCollection<MedicineModel> Medicine { get; set; }

        public MedicinePageModel(IMedicineService medicineService, IMedicineItemService medicineItemService, IDialogService dialogService)
        {
            _medicineService = medicineService;
            _medicineItemService = medicineItemService;
            _dialogService = dialogService;
        }

        public override async void Init(object initData)
        {
            // Medicine = new ObservableCollection<MedicineModel>(Medicines);
            if (initData != null)
            {
                Medicine = (MedicineModel)initData;
               

            }
            else
            {
                Medicine = new MedicineModel();

            }
            //base.Init(initData);
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

        #region AddMedicineItemCommand
        public Command AddMedicineItemCommand => new Command(async (p) => await AddMedicineItemExecute(), (p) => !IsBusy);
        private async Task AddMedicineItemExecute()
        {

            try
            {
              

                var data = await _medicineItemService.AddMedicineItem(new MedicineItemRequest
                {
                   Medicine_Id = Medicine.IdMedicine
                });
                await CoreMethods.PushPageModel<AddInputPageModel>();

               // await _dialogService.AlertAsync("Lưu thành công");
                IsBusy = false;

            }
            catch (Exception e)
            {
                //await _dialogService.AlertAsync("Lưu thất bại");
            }
        }
        #endregion

        #region SelectMedicine

        MedicineModel _selectedMedicine;

        public MedicineModel SelectedMedicine
        {
            get
            {
                return _selectedMedicine;
            }
            set
            {
                _selectedMedicine = value;
                if (value != null)
                MedicineSelected.Execute(value);
                
            }
            
        }

        public Command<MedicineModel> MedicineSelected
        {
            get
            {
                return new Command<MedicineModel>(async (Medicine) =>
                {
                    var data = _medicineItemService.AddMedicineItem(new MedicineItemRequest
                    {
                        Medicine_Id = Medicine.IdMedicine
                    });
                    CoreMethods.PushPageModel<AddInputPageModel>(Medicine);
                   
                });
            }
        }

#endregion

        #region properties
        public MedicineModel Medicine { get; set; }
        public List<MedicineModel> Medicines { get; set; } = new List<MedicineModel>();
        public List<MedicineModel> ListMedicines { get; set; } = new List<MedicineModel>();
        public MedicineRequest request { get; set; } = new MedicineRequest();
        public int IdMedicine { get; set; }
        #endregion

        #region AddMedicineCommand

        public Command AddMedicineCommand => new Command(async (p) => await AddMedicineExecute(), (p) => !IsBusy);

        private async Task AddMedicineExecute()
        {
            await CoreMethods.PushPageModel<AddMedicinePageModel>();
        }
        #endregion

       
    }
}
