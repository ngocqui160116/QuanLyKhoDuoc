using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Mobile.Core.Models.MedicineItem;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.MedicineItem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AddInputInfoPageModel : BasePageModel
    {
        private readonly IMedicineItemService _medicineItemService;
        private readonly IDialogService _dialogService;

        public AddInputInfoPageModel(IMedicineItemService medicineItemService, IDialogService dialogService)
        {
            _medicineItemService = medicineItemService;
            _dialogService = dialogService;

        }

        public override async void Init(object initData)
        {
            if (initData != null)
            {
                MedicineItem = (MedicineItemModel)initData;
            }
            else
            {
                MedicineItem = new MedicineItemModel();
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
            HSD = DateTime.Now;
            Id = MedicineItem.Id;
            IdMedicine = MedicineItem.Medicine_Id;
            NameMedicine = MedicineItem.MedicineName;
            IdBatch = MedicineItem.Batch;
            Count = MedicineItem.Count;
            InputPrice = MedicineItem.InputPrice;
            HSD = MedicineItem.DueDate;
            Total = MedicineItem.Total;
        }

        #region properties

        public MedicineModel Medicine { get; set; }
        public MedicineItemModel MedicineItem { get; set; }
        public List<MedicineItemModel> MedicineItems { get; set; } = new List<MedicineItemModel>();
        public InputInfoModel inputInfoModel { get; set; }
        public int Id { get; set; }
        public int IdMedicine { get; set; }
        public string NameMedicine { get; set; }
        public int IdBatch { get; set; }
        public DateTime HSD { get; set; } = DateTime.Now;
        public int Count { get; set; }
        public double InputPrice { get; set; }
        public double Total { get; set; }
        public string NameUnit { get; set; }
        public string SDK { get; set; }
        public bool IsClose { get; set; } = false;
        public bool IsOpen { get; set; } = true;
        #endregion

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
                if (IdBatch.Equals(0))
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
        }
        #endregion

        #region UpdateMedicineItemCommand
        public Command UpdateMedicineItemCommand => new Command(async (p) => await UpdateMedicineItemExecute(), (p) => !IsBusy);
        private async Task UpdateMedicineItemExecute()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;

                var data = await _medicineItemService.UpdateMedicineItem(Id, new MedicineItemRequest
                {
                    Id = Id,
                    Medicine_Id = IdMedicine,
                    Batch = IdBatch,
                    Count = Count,
                    InputPrice = InputPrice,
                    DueDate = HSD
                });
                await CoreMethods.PushPageModel<AddInputPageModel>();
                //await _dialogService.AlertAsync("Cập nhật thành công");
                IsBusy = false;

            }
            catch (Exception e)
            {
                //await _dialogService.AlertAsync("Cập nhật thất bại");
            }
        }
        #endregion

        #region SelectMedicine
        public Command SelectMedicine => new Command(async (p) => await SelectMedicineExecute(), (p) => !IsBusy);
        private async Task SelectMedicineExecute()
        {
            
            await CoreMethods.PushPageModel<MedicinePageModel>(Medicine);
        }
        #endregion
    }
}
