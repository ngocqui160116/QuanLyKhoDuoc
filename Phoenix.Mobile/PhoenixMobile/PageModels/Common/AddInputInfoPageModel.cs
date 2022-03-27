using Phoenix.Framework.Extensions;
using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Common;
using Phoenix.Mobile.Core.Models.Group;
using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Mobile.Core.Models.Unit;
using Phoenix.Mobile.Core.Services;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Group;
using Phoenix.Shared.Medicine;
using Phoenix.Shared.Unit;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AddInputInfoPageModel : BasePageModel
    {
        private readonly IUnitService _unitService;
        private readonly IMedicineService _medicineService;
        private readonly IDialogService _dialogService;

        public AddInputInfoPageModel(IMedicineService medicineService, IUnitService unitService, IDialogService dialogService)
        {
            _medicineService = medicineService;
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
            CurrentPage.Title = "Thêm Thuốc";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }
        private async Task LoadData()
        {
            NameMedicine = Medicine.Name;

            var data = await _unitService.GetAllUnit(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Units = data;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Units));
            }
        }

        #region properties
        public MedicineModel Medicine { get; set; }

        public string NameMedicine { get; set; }
        public string IdBatch { get; set; }
        public DateTime HSD { get; set; } = DateTime.Now;
        public int Count { get; set; }
        public int IdUnit { get; set; }
        public string NameUnit { get; set; }

        #endregion

        #region properties

        public List<UnitModel> Units { get; set; } = new List<UnitModel>();
        public UnitRequest request { get; set; } = new UnitRequest();
        public ObservableCollection<InputInfoModel> ListInputInfo { get; set; }

        UnitModel _selectedUnit;

        #endregion

        #region AddInputCommand

        public Command AddInputCommand => new Command(async (p) => await AddInputExecute(), (p) => !IsBusy);

        private async Task AddInputExecute()
        {

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
                    await _dialogService.AlertAsync("Số lượng phải lớn hơn 0");
                    IsBusy = false;
                    return;
                }
                if (IdUnit.Equals(0))
                {
                    await _dialogService.AlertAsync("Vui lòng Chọn Đơn vị tính");
                    IsBusy = false;
                    return;
                }


                // await CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn " + data, "Đóng");

                await CoreMethods.PushPageModel<AddInputPageModel>();

                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Thêm thất bại");

            }
            //await CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn " + Count, "Đóng");
            await CoreMethods.PushPageModel<AddInputPageModel>();
        }
        #endregion


        #region SelectedUnit
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

    }
}
