using Phoenix.Framework.Extensions;
using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using System;
using System.Collections.Generic;
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
           //so = ListMedicine.Count;
            if(so.Equals(0))
            {
                
                ListMedicine = new List<MedicineModel>()
                {
                    new MedicineModel()
                    {
                        IdMedicine = Medicine.IdMedicine,
                        Name = Medicine.Name,
                        NameUnit = Medicine.NameUnit,
                        RegistrationNumber = Medicine.RegistrationNumber
                    }

                };
               so = so + 1;
            }
            else
            {
                ListMedicine.Count.Equals(so);
                ListMedicine.Add(Medicine);
            }
            
        }

        #region properties
        public int so { get; set; }
        public MedicineModel Medicine { get; set; }
        public List<MedicineModel> ListMedicine { get; set; } = new List<MedicineModel>();
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

            try
            {
                if (IsBusy) return;
                IsBusy = true;
                if (IdBatch.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập số lô");
                    IsBusy = false;
                    return;
                }

                if (Count.Equals(0))
                {
                    await _dialogService.AlertAsync("Vui lòng nhập Số lượng lớn hơn 0");
                    IsBusy = false;
                    return;
                }


                CoreMethods.PushPageModel<AddInputPageModel>(inputInfoModel);
                
                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Thêm thất bại");

            }

            //await CoreMethods.DisplayAlert("Thêm thành công", "Bạn đã chọn" + Medicine.Name, "Đóng");
            
        }
        #endregion

        #region SelectMedicine
        public Command SelectMedicine => new Command(async (p) => await SelectMedicineExecute(), (p) => !IsBusy);
        private async Task SelectMedicineExecute()
        {
            
            await CoreMethods.PushPageModel<MedicinePageModel>();
        }
        #endregion
    }
}
