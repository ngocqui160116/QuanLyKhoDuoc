using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AddInputInfoPageModel : BasePageModel
    {
        private readonly IMedicineService _medicineService;
        private readonly IDialogService _dialogService;

        public AddInputInfoPageModel(IMedicineService medicineService, IDialogService dialogService)
        {
            _medicineService = medicineService;
            _dialogService = dialogService;

        }

        public override async void Init(object initData)
        {
            //base.Init(initData);
            if (initData != null)
            {
                Medicine = (MedicineModel)initData;
                
            }
            else
            {
                Medicine = new MedicineModel();
                
            }
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thêm Thuốc";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }
        private async Task LoadData()
        {
            NameMedicine = Medicine.Name;
            ListMedicine = new ObservableCollection<MedicineModel>()
            {
                new MedicineModel()
                {
                    IdMedicine = Medicine.IdMedicine,
                    Name = Medicine.Name,
                    RegistrationNumber = Medicine.RegistrationNumber
                }

            };

        }

        #region properties
        public MedicineModel Medicine { get; set; }
        public ObservableCollection<MedicineModel> ListMedicine { get; set; }
        public InputInfoModel inputInfoModel { get; set; }
        public string NameMedicine { get; set; }
        public string IdBatch { get; set; }
        public DateTime HSD { get; set; } = DateTime.Now;
        public int Count { get; set; }
        public double InputPrice { get; set; }
        public string NameUnit { get; set; }
        public string SDK { get; set; }
        #endregion

        #region properties
     
        public ObservableCollection<InputInfoModel> ListInputInfo { get; set; }

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
                return new Command<MedicineModel>(async (Medicine) => {
                    //await CoreMethods.PushPageModel<AddInputPageModel>(Medicine);
                    await CoreMethods.PushPageModel<MedicinePageModel>();
                });
            }
        }

        #endregion

        #region AddMedicineCommand
        public Command AddMedicineCommand => new Command(async (p) => await AddMedicineExecute(), (p) => !IsBusy);
        private async Task AddMedicineExecute()
        {
            try
            {

                await CoreMethods.PushPageModel<AddInputPageModel>();
                //await _dialogService.AlertAsync("Thêm thành công");
                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Thêm thất bại");

            }
        }
        #endregion

        public Command<InputInfoModel> InputSelected
        {
            get
            {
                return new Command<InputInfoModel>(async (InputInfo) =>
                {
                    //await CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn"+Count, "Đóng");
                    await CoreMethods.PushPageModel<AddInputInfoPageModel>(InputInfo);
                });
            }
        }

       

    }
}
