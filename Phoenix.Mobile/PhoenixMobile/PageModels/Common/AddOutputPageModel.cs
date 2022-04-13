using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Mobile.Core.Models.MedicineItem;
using Phoenix.Mobile.Core.Models.Reason;
using Phoenix.Mobile.Core.Models.Staff;
using Phoenix.Mobile.Core.Models.Supplier;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.MedicineItem;
using Phoenix.Shared.OutputInfo;
using Phoenix.Shared.Reason;
using Phoenix.Shared.Staff;
using Phoenix.Shared.Supplier;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
                //RaisePropertyChanged("Vendors");
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
        public List<ReasonModel> Reasons { get; set; } = new List<ReasonModel>();
        public ReasonRequest request { get; set; } = new ReasonRequest();
        public List<StaffModel> Staffs { get; set; } = new List<StaffModel>();
        public StaffRequest StaffRequest { get; set; } = new StaffRequest();
        public MedicineItemModel MedicineItem { get; set; }

        //public ObservableCollection<MedicineModel> ListMedicine { get; set; }
        //public List<InputInfoModel> ListMedicine { get; set; }

        #endregion

        #region properties
        public bool IsClose { get; set; } = false;
        public bool IsOpen { get; set; } = true;
        public int IdMedicine { get; set; }
        public string Names { get; set; } = "Nahn";
        //public int SoLuong { get; set; }

        string _name;
        string _surname;

        //public string Names
        //{
        //    get { return _name; }
        //    set
        //    {
        //        _name = value;

        //    }
        //}

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;

            }
        }
        public int IdReason { get; set; }
        public int IdStaff { get; set; }
        public DateTime HSD { get; set; } = DateTime.Now;
        #endregion

        //#region AddOutputCommand
        //public Command AddOutputCommand => new Command(async (p) => await AddOutputExecute(), (p) => !IsBusy);
        //private async Task AddOutputExecute()
        //{
        //    try
        //    {

        //        var data = await _outputInfoService.CreateOutputInfo( new OutputInfoRequest
        //        {
        //            IdReason = SelectedReason.IdReason,
        //            IdStaff = SelectedStaff.IdStaff,
        //            Count = 100,
        //            DateOutput = HSD
        //        });
        //        //await CoreMethods.PushPageModel<OutputPageModel>();

        //        CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn" +SelectedReason.IdReason +SelectedStaff.IdStaff, "Đóng");

        //        await _dialogService.AlertAsync("Thêm thành công");
        //        IsBusy = false;

        //    }
        //    catch (Exception e)
        //    {
        //        await _dialogService.AlertAsync("Thêm thất bại");
        //    }
        //}
        //#endregion

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
