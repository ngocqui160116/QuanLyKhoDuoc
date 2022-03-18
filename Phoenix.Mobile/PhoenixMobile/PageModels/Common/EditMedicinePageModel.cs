using FreshMvvm;
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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
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
            var data = await _groupService.GetAllGroup(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Groups = data;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Groups));
            }

            var data1 = await _unitService.GetAllUnit(unitrequest);
            if (data1 == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Units = data1;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Units));
            }
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

        #region EditCommand
        public Command EditCommand => new Command(async (p) => await EditExecute(), (p) => !IsBusy);
        private async Task EditExecute()
        {
            IsEnabled = true;
            
        }
        #endregion

        #region UpdateMedicineCommand
        public Command UpdateMedicineCommand => new Command(async (p) => await EpdateMedicineExecute(), (p) => !IsBusy);
        private async Task EpdateMedicineExecute()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;
                //if (Name.IsNullOrEmpty())
                //{
                //    await _dialogService.AlertAsync("Vui lòng nhập tên nhà cung cấp");
                //    IsBusy = false;
                //    return;
                //}

                //if (PhoneNumber.IsNullOrEmpty())
                //{
                //    await _dialogService.AlertAsync("Vui lòng nhập số điện thoại");
                //    IsBusy = false;
                //    return;
                //}
                //if (Email.IsNullOrEmpty())
                //{
                //    await _dialogService.AlertAsync("Vui lòng nhập email");
                //    IsBusy = false;
                //    return;
                //}
                //if (Address.IsNullOrEmpty())
                //{
                //    await _dialogService.AlertAsync("Vui lòng nhập địa chỉ");
                //    IsBusy = false;
                //    return;
                //}
                var data = await _medicineService.UpdateMedicine(IdMedicine, new MedicineRequest
                {
                    RegistrationNumber = SDK,
                    Name = Name,
                    IdGroup = SelectedGroup.IdGroup,
                    Active = Active,
                    Content = Content,
                    Packing = Packing,
                    IdUnit = SelectedUnit.Id
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

    }
}
