using Phoenix.Framework.Extensions;
using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Group;
using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Mobile.Core.Models.Unit;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Group;
using Phoenix.Shared.Medicine;
using Phoenix.Shared.Unit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class EditMedicinePageModel : BasePageModel
    {
        private readonly IGroupService _groupService;
        private readonly IMedicineService _medicineService;
        private readonly IUnitService _unitService;
        private readonly IDialogService _dialogService;

        public EditMedicinePageModel(IMedicineService medicineService, IUnitService unitService, IGroupService groupService, IDialogService dialogService)
        {
            _medicineService = medicineService;
            _groupService = groupService;
            _unitService = unitService;
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
            CurrentPage.Title = "Thông tin Thuốc";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            if (IsBusy) return;
            IsBusy = true;
#if DEBUG
            IdMedicine = Medicine.IdMedicine;
            SDK = Medicine.RegistrationNumber;
            Name = Medicine.Name;
            NameGroup = Medicine.GroupName;
            Active = Medicine.Active;
            Content = Medicine.Content;
            Packing = Medicine.Packing;
            NameUnit = Medicine.NameUnit;
            IdUnit = Medicine.IdUnit;
            IdGroup = Medicine.IdGroup;
           
#endif
            IsBusy = false;

            #region Group

            var data = await _groupService.GetAllGroup(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Groups = data;
                RaisePropertyChanged(nameof(Groups));
            }
            #endregion

            #region Unit
            var data1 = await _unitService.GetAllUnit(unitrequest);
            if (data1 == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Units = data1;
                RaisePropertyChanged(nameof(Units));
            }
            #endregion
        }

        #region properties
        public bool IsEnabled { get; set; } = false;
        public MedicineModel Medicine { get; set; }

        GroupModel _selectedGroup;

        UnitModel _selectedUnit;
        public List<GroupModel> Groups { get; set; } = new List<GroupModel>();
        public List<UnitModel> Units { get; set; } = new List<UnitModel>();
        public GroupRequest request { get; set; } = new GroupRequest();
        public UnitRequest unitrequest { get; set; } = new UnitRequest();

        #endregion

        #region properties
        public string SearchText { get; set; }
        public int IdMedicine { get; set; }
        public string SDK { get; set; }
        public string Name { get; set; }
        public int IdGroup { get; set; }
        public string Active { get; set; }
        public string Content { get; set; }
        public string Packing { get; set; }
        public int IdUnit { get; set; }
        public string NameGroup { get; set; }
        public string NameUnit { get; set; }

        #endregion

        #region EditCommand
        public Command EditCommand => new Command(async (p) => await EditExecute(), (p) => !IsBusy);
        private async Task EditExecute()
        {
            IsEnabled = true;  
        }
        #endregion

        #region UpdateMedicineCommand
        public Command UpdateMedicineCommand => new Command(async (p) => await UpdateMedicineExecute(), (p) => !IsBusy);
        private async Task UpdateMedicineExecute()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;
                if (Name.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập tên thuốc");
                    IsBusy = false;
                    return;
                }

                if (SDK.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập số đăng ký");
                    IsBusy = false;
                    return;
                }

                if (Active.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập hoạt chất thuốc");
                    IsBusy = false;
                    return;
                }

                if (Content.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập hàm lượng thuốc");
                    IsBusy = false;
                    return;
                }

                if (Packing.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập QC đóng gói");
                    IsBusy = false;
                    return;
                }

                var data = await _medicineService.UpdateMedicine(IdMedicine, new MedicineRequest
                {
                    IdMedicine = IdMedicine,
                    RegistrationNumber = SDK,
                    Name = Name,
                    IdGroup = IdGroup,
                    Active = Active,
                    Content = Content,
                    Packing = Packing,
                    IdUnit = IdUnit
                });
                await CoreMethods.PushPageModel<MedicinePageModel>();
                await _dialogService.AlertAsync("Cập nhật thành công");
                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Cập nhật thất bại");
            }
        }
        #endregion

        #region SelectedGroup
        public GroupModel SelectedGroup
        {
            get
            {
                return _selectedGroup;
            }
            set
            {
                _selectedGroup = value;
                if (value != null)
                    IdGroup = value.IdGroup;
            }
        }
        #endregion

        #region SelectedGroup
        public UnitModel SelectedUnit
        {
            get
            {
                return _selectedUnit;
            }
            set
            {
                _selectedUnit = value;
                if (value != null)
                    IdUnit = value.Id;
            }
        }
        #endregion

        #region DeleteMedicineCommand
        public Command DeleteMedicineCommand => new Command(async (p) => await DeleteMedicineExecute(), (p) => !IsBusy);
        private async Task DeleteMedicineExecute()
        {
            //await CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn:" + IdMedicine, "Đóng");
            try
            {
                if (IsBusy) return;
                IsBusy = true;

                var data = await _medicineService.DeleteMedicine(IdMedicine);
                await CoreMethods.PushPageModel<MedicinePageModel>();
                await _dialogService.AlertAsync("Xóa thành công");
                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Xóa thất bại");
            }
        }
        #endregion
    }
}
