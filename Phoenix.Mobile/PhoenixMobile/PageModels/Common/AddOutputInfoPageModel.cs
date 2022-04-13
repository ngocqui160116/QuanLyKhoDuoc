using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.MedicineItem;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.MedicineItem;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AddOutputInfoPageModel : BasePageModel
    {
        private readonly IMedicineItemService _medicineItemService;

        private readonly IDialogService _dialogService;

        public AddOutputInfoPageModel(IMedicineItemService medicineItemService, IDialogService dialogService)
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

            MedicineItems = new List<MedicineItemModel>
            {
                new MedicineItemModel()
                {
                    Id = MedicineItem.Id,
                    Medicine_Id = MedicineItem.Medicine_Id,
                    Batch = MedicineItem.Batch,
                    Count = MedicineItem.Count,
                    InputPrice =  MedicineItem.InputPrice,
                    Total = MedicineItem.Total,
                    DueDate = MedicineItem.DueDate,
                    UnitName = MedicineItem.UnitName,
                    Inventory_Id = MedicineItem.Inventory_Id,
                    Amount = MedicineItem.Amount
                }
            };

            HSD = DateTime.Now;
            Id = MedicineItem.Id;
            IdMedicine = MedicineItem.Medicine_Id;
            NameMedicine = MedicineItem.MedicineName;
            IdBatch = MedicineItem.Batch;
            Count = MedicineItem.Count;
            InputPrice = MedicineItem.InputPrice;
            HSD = MedicineItem.DueDate;
            Total = MedicineItem.Total;
            Amount = MedicineItem.Amount;
        }

        #region properties

        public MedicineItemModel MedicineItem { get; set; }
        public List<MedicineItemModel> MedicineItems { get; set; }
        public int Id { get; set; }
        public int IdMedicine { get; set; }
        public string NameMedicine { get; set; }
        public int IdBatch { get; set; }
        public DateTime HSD { get; set; } = DateTime.Now;
        public int Count { get; set; }
        public int Amount { get; set; }
        public double InputPrice { get; set; }
        public double Total { get; set; }
        public string NameUnit { get; set; }
        public string SDK { get; set; }
        public bool IsClose { get; set; } = false;
        public bool IsOpen { get; set; } = true;
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
                    Count = Count,
                    Medicine_Id = IdMedicine,
                    Batch = IdBatch,
                    DueDate = HSD,
                    InputPrice = InputPrice,
                    Amount = Amount
                });
                await CoreMethods.PushPageModel<AddOutputPageModel>();
                //await _dialogService.AlertAsync("Cập nhật thành công");
                IsBusy = false;

            }
            catch (Exception e)
            {
                //await _dialogService.AlertAsync("Cập nhật thất bại");
            }
        }
        #endregion
    }

}
