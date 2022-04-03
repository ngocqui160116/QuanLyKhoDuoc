using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Mobile.Core.Models.InventoryTags;
using Phoenix.Mobile.Core.Models.OutputInfo;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.InputInfo;
using Phoenix.Shared.InventoryTags;
using Phoenix.Shared.OutputInfo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class InventoryPageModel :BasePageModel
    {

        private readonly IInputInfoService _inputinfoService;
        private readonly IOutputInfoService _outputinfoService;
        private readonly IInventoryTagsService _inventoryTagsService;
        private readonly IDialogService _dialogService;

        public InventoryPageModel(IInputInfoService inputinfoService, IInventoryTagsService inventoryTagsService, IOutputInfoService outputinfoService, IDialogService dialogService)
        {
            _inputinfoService = inputinfoService;
            _outputinfoService = outputinfoService;
            _inventoryTagsService = inventoryTagsService;
            _dialogService = dialogService;

        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thuốc tồn kho";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
           

            var data = await _inventoryTagsService.GetAllInventoryTags(InventoryTagsRequest);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                InventoryTags = data;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(InventoryTags));
            }

            var data1 = await _outputinfoService.GetAllOutputInfo(OutputInfoRequest);
            if (data1 == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                OutputInfos = data1;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(OutputInfos));
            }
        }

        #region properties
        public List<InputInfoModel> Inventorys { get; set; } = new List<InputInfoModel>();
        public InputInfoRequest request { get; set; } = new InputInfoRequest();
        public List<InventoryTagsModel> InventoryTags { get; set; } = new List<InventoryTagsModel>();
        public InventoryTagsRequest InventoryTagsRequest { get; set; } = new InventoryTagsRequest();

        public List<OutputInfoModel> OutputInfos { get; set; } = new List<OutputInfoModel>();
        public OutputInfoRequest OutputInfoRequest { get; set; } = new OutputInfoRequest();

        public OutputInfoDto outputInfoDto { get; set; }



        #endregion

        #region SelectedItemCommand

        #region SelectMedicine

        InputInfoModel _selectedInputInfo;

        public InputInfoModel SelectedInputInfo
        {
            get
            {
                return _selectedInputInfo;
            }
            set
            {
                _selectedInputInfo = value;
                if (value != null)
                    //ListMedicines.Add(SelectedMedicine);
                    InputInfoSelected.Execute(value);

            }

        }
        #region InputInfoSelected
        public Command<InputInfoModel> InputInfoSelected
        {
            get
            {
                return new Command<InputInfoModel>(async (InputInfo) =>
                {
                    //await CoreMethods.PushPageModel<AddInputPageModel>(Medicine);
                    await CoreMethods.PushPageModel<AddOutputPageModel>(InputInfo);
                });
            }
        }

        #endregion
        #endregion





        public Command SelectedItemCommand => new Command(async (p) => await SelectedItemCommandExecute(), (p) => !IsBusy);

        private async Task SelectedItemCommandExecute()
        {
            await _dialogService.AlertAsync("Bạn đã chọn:", "Thông báo", "OK");
            // await CoreMethods.PushPageModel<AddMedicinePageModel>();
        }
        #endregion
    }
}
