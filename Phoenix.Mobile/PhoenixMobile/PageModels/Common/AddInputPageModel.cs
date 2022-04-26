using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Mobile.Core.Models.MedicineItem;
using Phoenix.Mobile.Core.Models.Staff;
using Phoenix.Mobile.Core.Models.Supplier;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Input;
using Phoenix.Shared.InputInfo;
using Phoenix.Shared.MedicineItem;
using Phoenix.Shared.Staff;
using Phoenix.Shared.Supplier;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AddInputPageModel : BasePageModel
    {
        private readonly IMedicineItemService _medicineItemService;
        private readonly ISupplierService _supplierService;
        private readonly IStaffService _staffService;
        private readonly IInputService _inputService;
        private readonly IInputInfoService _inputInfoService;
        private readonly IDialogService _dialogService;

        public AddInputPageModel(ISupplierService supplierService, IInputInfoService inputInfoService, IInputService inputService, IMedicineItemService medicineItemService, IStaffService staffService, IOutputInfoService outputInfoService , IDialogService dialogService)
        {
            _supplierService = supplierService;
            _medicineItemService = medicineItemService;
            _staffService = staffService;
            _inputService = inputService;
            _inputInfoService = inputInfoService;
            _dialogService = dialogService;

        }
        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thêm phiếu nhập";
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

            #region Supplier

            var data = await _supplierService.GetAllSupplier(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");

            }
            else
            {
                Suppliers = data;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Suppliers));
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
        public InputInfoModel InputInfo { get; set; }
        public static List<InputInfoModel> InputInfos { get; set; } = new List<InputInfoModel>();
        public InputInfoModel infoModel { get; set; }
        public List<SupplierModel> Suppliers { get; set; } = new List<SupplierModel>();
        public SupplierRequest request { get; set; } = new SupplierRequest();
        public List<StaffModel> Staffs { get; set; } = new List<StaffModel>();
        public StaffRequest StaffRequest { get; set; } = new StaffRequest();
        public MedicineItemModel MedicineItem { get; set; }

        #endregion

        #region properties
        public float Total { get; set; }
        public int Medicine_Id { get; set; }
        public string MedicineName { get; set; }
        public string RegistrationNumber { get; set; }
        public string NameUnit { get; set; }
        public int Batch { get; set; }
        public int IdSupplier { get; set; }
        public int IdStaff { get; set; }
        public DateTime DateInput { get; set; } = DateTime.Now;

        #endregion

        #region AddInventoryCommand
        public Command AddInventoryCommand => new Command(async (p) => await AddInventoryExecute(), (p) => !IsBusy);
        private async Task AddInventoryExecute()
        {
            try
            {

                if (IsBusy) return;
                IsBusy = true;
                //if (infoModel.IdMedicine == 0)
                //{
                //    await _dialogService.AlertAsync("Vui lòng nhập thông tin thuốc");
                //    IsBusy = false;
                //    return;
                //}

                if (IdSupplier.Equals(0))
                {
                    await _dialogService.AlertAsync("Vui lòng chọn nhà cung cấp");
                    IsBusy = false;
                    return;
                }
                if (IdStaff.Equals(0))
                {
                    await _dialogService.AlertAsync("Vui lòng chọn người tạo");
                    IsBusy = false;
                    return;
                }

                var data = await _inputInfoService.AddInputInfo(new InputInfoRequest
                {
                    IdStaff = IdStaff,
                    IdSupplier = IdSupplier,
                    DateInput = DateInput,
                    Status = "Đã hoàn thành",
                });
                var data1 = await _medicineItemService.DeleteAll();

                await CoreMethods.PushPageModel<InputPageModel>();
                await _dialogService.AlertAsync("Thêm thành công");
                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Thêm thất bại");
            }
        }
        #endregion

        #region AddInput
        public Command AddInput => new Command(async (p) => await AddInputExecute(), (p) => !IsBusy);
        private async Task AddInputExecute()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;
                //if (infoModel.IdMedicine == 0)
                //{
                //    await _dialogService.AlertAsync("Vui lòng nhập thông tin thuốc");
                //    IsBusy = false;
                //    return;
                //}

                if (IdSupplier.Equals(0))
                {
                    await _dialogService.AlertAsync("Vui lòng chọn nhà cung cấp");
                    IsBusy = false;
                    return;
                }
                if (IdStaff.Equals(0))
                {
                    await _dialogService.AlertAsync("Vui lòng chọn người tạo");
                    IsBusy = false;
                    return;
                }

                var data = _inputService.CreateInput(new InputRequest
                {
                    IdStaff = IdStaff,
                    IdSupplier = IdSupplier,
                    DateInput = DateInput,
                    Status = "Đã Lưu",
                });

                var data1 = await _medicineItemService.DeleteAll();

                await CoreMethods.PushPageModel<InputPageModel>();
                await _dialogService.AlertAsync("Lưu thành công");
            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Lưu thất bại");
            }

        }
        #endregion

        #region AddMedicineCommand

        public Command AddMedicineCommand => new Command(async (p) => await AddMedicineExecute(), (p) => !IsBusy);

        private async Task AddMedicineExecute()
        {
            await CoreMethods.PushPageModel<MedicinePageModel>();
            
        }
        #endregion

        #region SelectItem

        public Command<MedicineItemModel> SelectItemCommand
        {
            get
            {
                return new Command<MedicineItemModel>(async (MedicineItemModel) =>
                {
                    //CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn: " +MedicineItemModel.MedicineName, "Đóng");
                    CoreMethods.PushPageModel<AddInputInfoPageModel>(MedicineItemModel);

                });
            }
        }
        #endregion

        #region RemoveItem

        public Command<MedicineItemModel> RemoveItemCommand
        {
            get
            {
                return new Command<MedicineItemModel>(async (MedicineItemModel) =>
                {
                try
                {
                    if (IsBusy) return;
                    IsBusy = true;

                        //CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn: " + MedicineItemModel.Id, "Đóng");

                        var data = await _medicineItemService.RemoveMedicineItem(MedicineItemModel.Id);
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

        #region SelectSupplier

        SupplierModel _selectedSupplier;
        public SupplierModel SelectedSupplier
        {
            get
            {
                return _selectedSupplier;
            }
            set
            {
                _selectedSupplier = value;
                if (value != null)
                    IdSupplier = value.IdSupplier;
            }
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

        #region BackCommand
        public Command BackCommand => new Command(async (p) => await Input(), (p) => !IsBusy);

        public async Task Input()
        {
            CoreMethods.PushPageModel<InputPageModel>();
        }

        #endregion
    }
}
