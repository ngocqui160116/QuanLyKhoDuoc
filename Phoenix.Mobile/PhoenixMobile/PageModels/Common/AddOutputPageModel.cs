using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Mobile.Core.Models.Reason;
using Phoenix.Mobile.Core.Models.Staff;
using Phoenix.Mobile.Core.Models.Supplier;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
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
        private readonly IReasonService _reasonService;
        private readonly IStaffService _staffService;
        private readonly IOutputInfoService _outputInfoService;
        private readonly IDialogService _dialogService;

        public AddOutputPageModel(IReasonService reasonService, IStaffService staffService, IOutputInfoService outputInfoService , IDialogService dialogService)
        {
            _reasonService = reasonService;
            _staffService = staffService;
            _outputInfoService = outputInfoService;
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
            CurrentPage.Title = "Thêm phiếu xuất";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
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
        }

        #region properties
        public List<ReasonModel> Reasons { get; set; } = new List<ReasonModel>();
        public ReasonRequest request { get; set; } = new ReasonRequest();
        public List<StaffModel> Staffs { get; set; } = new List<StaffModel>();
        public StaffRequest StaffRequest { get; set; } = new StaffRequest();
        public MedicineModel Medicine { get; set; }
        public ObservableCollection<MedicineModel> ListMedicine { get; set; }

        #endregion

        #region properties
        public int IdMedicine { get; set; }
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

                var data = await _outputInfoService.AddOutputInfo(new OutputInfoRequest
                {
                    IdOutput = "HDX004",
                    IdMedicine = 21,
                    IdInputInfo = 1,
                    IdReason = 2,
                    Total = 2000,
                    IdStaff = 3,
                    Count = 100,
                    DateOutput = HSD
                });
                await CoreMethods.PushPageModel<OutputPageModel>();

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
            await CoreMethods.PushPageModel<MedicinePageModel>();
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
