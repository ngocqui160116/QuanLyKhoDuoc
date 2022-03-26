using Phoenix.Framework.Extensions;
using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Common;
using Phoenix.Mobile.Core.Models.Group;
using Phoenix.Mobile.Core.Models.InputInfo;
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
                InputInfo = (InputInfoModel)initData;
            }
            else
            {
                InputInfo = new InputInfoModel();
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
        public InputInfoModel InputInfo { get; set; }
        public string IdBatch { get; set; }
        public DateTime HSD { get; set; } = DateTime.Now;
        public int Count { get; set; }
        public int IdUnit { get; set; }
        public string NameUnit { get; set; }

        #endregion

        #region properties

        public List<UnitModel> Units { get; set; } = new List<UnitModel>();
        public UnitRequest request { get; set; } = new UnitRequest();

        public List<string> Data { get; set; }

        UnitModel _selectedUnit;

        #endregion

        #region AddInputCommand

        public Command AddInputCommand => new Command(async (p) => await AddInputExecute(), (p) => !IsBusy);

        private async Task AddInputExecute()
        {
            var response = await _dialogService.DisplayPromptAsync("Question", "What is your name?", "Yes", "No", "type your name");
           
            //try
            //{
            //    if (IsBusy) return;
            //    IsBusy = true;
            //    if (IdBatch.IsNullOrEmpty())
            //    {
            //        await _dialogService.AlertAsync("Vui lòng nhập số lô");
            //        IsBusy = false;
            //        return;
            //    }

               
            //    if (Count.Equals(0))
            //    {
            //        await _dialogService.AlertAsync("Số lượng phải lớn hơn 0");
            //        IsBusy = false;
            //        return;
            //    }
            //    if (IdUnit.Equals(0))
            //    {
            //        await _dialogService.AlertAsync("Vui lòng Chọn Đơn vị tính");
            //        IsBusy = false;
            //        return;
            //    }

            //   // await CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn " + data, "Đóng");

            //    await CoreMethods.PushPageModel<AddInputPageModel>(IdBatch+Count+IdUnit+HSD);
               
            //    IsBusy = false;

            //}
            //catch (Exception e)
            //{
            //    await _dialogService.AlertAsync("Thêm thất bại");

            //}
            ////await CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn " + Count, "Đóng");
            ////await CoreMethods.PushPageModel<AddInputPageModel>();
        }
        #endregion

        //#region SelectInput

        //InputInfoModel _selectedInput;

        //public InputInfoModel SelectedInput
        //{
        //    get
        //    {
        //        return _selectedInput;
        //    }
        //    set
        //    {
        //        _selectedInput = value;
        //        if (value != null)
        //            InputSelected.Execute(value);
        //    }
        //}

        //public Command<InputInfoModel> InputSelected
        //{
        //    get
        //    {
        //        return new Command<InputInfoModel>(async (Input) => {
        //            await CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn"+SelectedInput.Id + IdBatch + Count + HSD, "Đóng");
        //            //await CoreMethods.PushPageModel<InputInfoPageModel>(Input);
        //        });
        //    }
        //}
        //#endregion

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
