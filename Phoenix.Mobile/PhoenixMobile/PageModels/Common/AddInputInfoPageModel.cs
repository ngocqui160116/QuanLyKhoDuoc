using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
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
                IsClose = true;
                IsOpen = false;
            }
            else
            {
                Medicine = new MedicineModel();
                
            }
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Nhập thông tin phiếu nhập";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }
        private async Task LoadData()
        {
            ListMedicine = new ObservableCollection<MedicineModel>()
            {
                new MedicineModel()
                {
                    IdMedicine = Medicine.IdMedicine,
                    Name = Medicine.Name,
                    NameUnit = Medicine.NameUnit,
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
        public bool IsClose { get; set; } = false;
        public bool IsOpen { get; set; } = true;
        #endregion

        #region properties

        public ObservableCollection<InputInfoModel> ListInputInfo { get; set; }

        #endregion

        

       public ICommand PerformSearch => new Command<MedicineModel>((MedicineModel Medicine) =>
        {
             CoreMethods.DisplayAlert("Thêm thành công","Bạn đã chọn" + Medicine.Name,"Đóng");
             CoreMethods.PushPageModel<AddInputPageModel>(Medicine);
        });

        #region AddMedicineCommand
        public ICommand AddMedicineCommand => new Command(async (p) => await AddMedicineExecute(), (p) => !IsBusy);
        private async Task AddMedicineExecute()
        {
            inputInfoModel = new InputInfoModel()
            {
                IdMedicine = Medicine.IdMedicine,
                MedicineName = Medicine.Name,
                IdBatch = IdBatch,
                DueDate = HSD,
                Count = Count,
                InputPrice = InputPrice
            };
            //await CoreMethods.DisplayAlert("Thêm thành công", "Bạn đã chọn" + Medicine.Name, "Đóng");
            CoreMethods.PushPageModel<AddInputPageModel>(inputInfoModel);
        }
        #endregion

        #region SelectMedicine
        public Command SelectMedicine => new Command(async (p) => await SelectMedicineExecute(), (p) => !IsBusy);
        private async Task SelectMedicineExecute()
        {
            
            await CoreMethods.PushPageModel<MedicinePageModel>();
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
