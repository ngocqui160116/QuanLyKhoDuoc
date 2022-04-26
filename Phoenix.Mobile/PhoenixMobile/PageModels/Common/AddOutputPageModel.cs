using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.MedicineItem;
using Phoenix.Mobile.Core.Models.Reason;
using Phoenix.Mobile.Core.Models.Staff;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.MedicineItem;
using Phoenix.Shared.OutputInfo;
using Phoenix.Shared.Reason;
using Phoenix.Shared.Staff;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AddOutputPageModel : BasePageModel
    {
        private readonly IMedicineItemService _medicineItemService;
        private readonly IReasonService _reasonService;
        private readonly IStaffService _staffService;
        private readonly IOutputInfoService _outputInfoService;
        private readonly IDialogService _dialogService;
        public AddOutputPageModel(IReasonService reasonService, IMedicineItemService medicineItemService, IStaffService staffService, IOutputInfoService outputInfoService , IDialogService dialogService)
        {
            _reasonService = reasonService;
            _medicineItemService = medicineItemService;
            _staffService = staffService;
            _outputInfoService = outputInfoService;
            _dialogService = dialogService;

        }
        public override async void Init(object initData)
        {
            base.Init(initData);
            IsClose = true;
            IsOpen = false;
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thêm phiếu xuất";
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

            #region Reason
            var data = await _reasonService.GetAllReason(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Reasons = data;
                RaisePropertyChanged(nameof(Reasons));
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
                RaisePropertyChanged(nameof(Staffs));
            }

            #endregion
        }

        #region properties
        public List<MedicineItemModel> MedicineItems { get; set; } = new List<MedicineItemModel>();
        public MedicineItemRequest MedicineItemRequest { get; set; } = new MedicineItemRequest();
        public List<ReasonModel> Reasons { get; set; } = new List<ReasonModel>();
        public ReasonRequest request { get; set; } = new ReasonRequest();
        public List<StaffModel> Staffs { get; set; } = new List<StaffModel>();
        public StaffRequest StaffRequest { get; set; } = new StaffRequest();
        #endregion

        #region properties
        public bool IsClose { get; set; } = false;
        public bool IsOpen { get; set; } = true;
        public string abc { get; set; }
        public int Id { get; set; }
        public int Medicine_Id { get; set; }
        public string MedicineName { get; set; }
        public int Batch { get; set; }
        public int Count { get; set; }
        public double InputPrice { get; set; }
        public double Total { get; set; }
        public DateTime DueDate { get; set; }
        public string UnitName { get; set; }
        public int? Inventory_Id { get; set; }
        public int Amount { get; set; }
        public int IdMedicine { get; set; }
        public string Names { get; set; }
        public string Name { get; set; }
        public int IdReason { get; set; }
        public int IdStaff { get; set; }
        public DateTime HSD { get; set; } = DateTime.Now;
        #endregion

        #region AddOutputCommand
        public Command AddOutputCommand => new Command(async (p) => await AddOutputExecute(), (p) => !IsBusy);
        private async Task AddOutputExecute()
        {
            try
            {
                var data = await _outputInfoService.CreateOutputInfo(new OutputInfoRequest
                {
                    IdReason = IdReason,
                    IdStaff = IdStaff,
                    DateOutput = HSD
                });

                var data1 = await _medicineItemService.DeleteAll();
                await CoreMethods.PushPageModel<OutputPageModel>();
                //CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn" + SelectedReason.IdReason + SelectedStaff.IdStaff, "Đóng");
                await _dialogService.AlertAsync("Thêm thành công");
                IsBusy = false;
            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Thêm thất bại");
            }
        }
        #endregion

        #region AddMedicineCommand

        public Command AddMedicineCommand => new Command(async (p) => await AddMedicineExecute(), (p) => !IsBusy);

        private async Task AddMedicineExecute()
        {
            await CoreMethods.PushPageModel<InventoryPageModel>();
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
                    CoreMethods.PushPageModel<AddOutputInfoPageModel>(MedicineItemModel);

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

        #region BackCommand
        public Command BackCommand => new Command(async (p) => await Output(), (p) => !IsBusy);

        public async Task Output()
        {
            CoreMethods.PushPageModel<OutputPageModel>();
        }

        #endregion

        #region SelectReason

        ReasonModel _selectedReason;
        public ReasonModel SelectedReason
        {
            get
            {
                return _selectedReason;
            }
            set
            {
                _selectedReason = value;
                if (value != null)
                    IdReason = value.IdReason;
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
    }
}
