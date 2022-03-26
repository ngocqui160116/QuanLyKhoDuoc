﻿using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Mobile.Core.Models.OutputInfo;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.InputInfo;
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
        private readonly IDialogService _dialogService;

        public InventoryPageModel(IInputInfoService inputinfoService, IOutputInfoService outputinfoService, IDialogService dialogService)
        {
            _inputinfoService = inputinfoService;
            _outputinfoService = outputinfoService;
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
           

            var data = await _inputinfoService.GetAllInputInfo(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Inventorys = data;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Inventorys));
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

        public List<OutputInfoModel> OutputInfos { get; set; } = new List<OutputInfoModel>();
        public OutputInfoRequest OutputInfoRequest { get; set; } = new OutputInfoRequest();

        public OutputInfoDto outputInfoDto { get; set; }

       

        #endregion

        #region SelectedItemCommand

        public Command SelectedItemCommand => new Command(async (p) => await SelectedItemCommandExecute(), (p) => !IsBusy);

        private async Task SelectedItemCommandExecute()
        {
            await _dialogService.AlertAsync("Bạn đã chọn:", "Thông báo", "OK");
            // await CoreMethods.PushPageModel<AddMedicinePageModel>();
        }
        #endregion
    }
}
