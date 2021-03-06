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

        public MedicinePageModel(IMedicineService medicineService, IMedicineItemService medicineItemService, IDialogService dialogService)
        {
            _medicineService = medicineService;
            _medicineItemService = medicineItemService;
            _dialogService = dialogService;
        }

        public override async void Init(object initData)
        {
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
            #region Medicine

            var data = await _medicineService.GetAllMedicine(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Medicines = data;
                RaisePropertyChanged(nameof(Medicines));
            }
            #endregion
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

        #region SelectItem

        public Command<MedicineModel> SelectItemCommand
        {
            get
            {
                return new Command<MedicineModel>(async (MedicineModel) =>
                {
                    try
                    {

                        var data = await _medicineItemService.AddMedicineItem(new MedicineItemRequest
                        {
                            Medicine_Id = MedicineModel.IdMedicine
                        });
                        await CoreMethods.PushPageModel<AddInputPageModel>();

                        // await _dialogService.AlertAsync("Lưu thành công");
                        IsBusy = false;

                    }
                    catch (Exception e)
                    {
                        //await _dialogService.AlertAsync("Lưu thất bại");
                    }

                });
            }
        }
        #endregion

        #region EditItem

        public Command<MedicineModel> EditItemCommand
        {
            get
            {
                return new Command<MedicineModel>(async (MedicineModel) =>
                {
                    //CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn: " +MedicineItemModel.MedicineName, "Đóng");
                    CoreMethods.PushPageModel<EditMedicinePageModel>(MedicineModel);

                });
            }
        }
        #endregion

        #region RemoveItem

        public Command<MedicineModel> RemoveItemCommand
        {
            get
            {
                return new Command<MedicineModel>(async (MedicineModel) =>
                {
                    try
                    {
                        if (IsBusy) return;
                        IsBusy = true;

                        //CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn: " + MedicineItemModel.Id, "Đóng");

                        var data = await _medicineService.DeleteMedicine(MedicineModel.IdMedicine);
                        // await CoreMethods.PushPageModel<AddOutputPageModel>();
                        await _dialogService.AlertAsync("Xóa thành công");
                        LoadData();
                        IsBusy = false;

                    }
                    catch (Exception e)
                    {
                        await _dialogService.AlertAsync("Xóa thất bại");
                    }
                    //CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn: " +MedicineItemModel.Id, "Đóng");
                    // CoreMethods.PushPageModel<AddInputInfoPageModel>(MedicineItemModel);

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

        #region BackCommand
        public Command BackCommand => new Command(async (p) => await Home(), (p) => !IsBusy);

        public async Task Home()
        {
            NavigationHelpers.ToMainPage();
        }

        #endregion

        #region Search

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            Medicines = GetSearchResults(query);
        });
        public List<MedicineModel> GetSearchResults(string queryString)
        {
            var normalizedQuery = queryString;
            return Medicines.Where(f => f.Name.Contains(normalizedQuery)).ToList();
        }
        #endregion
    }
}
