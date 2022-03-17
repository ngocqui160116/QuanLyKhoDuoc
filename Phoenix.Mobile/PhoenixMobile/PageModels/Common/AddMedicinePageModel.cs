using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Group;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Group;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AddMedicinePageModel : BasePageModel
    {
        private readonly IGroupService _groupService;
        private readonly IDialogService _dialogService;

        public AddMedicinePageModel(IGroupService groupService, IDialogService dialogService)
        {
            _groupService = groupService;
            _dialogService = dialogService;

        }

        public override async void Init(object initData)
        {
            base.Init(initData);
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
        }

        #region UnitCommand

        public Command UnitCommand => new Command(async (p) => await UnitExecute(), (p) => !IsBusy);

        private async Task UnitExecute()
        {
            await CoreMethods.PushPageModel<UnitPageModel>();
        }
        #endregion

        #region properties
        public List<GroupModel> Groups { get; set; } = new List<GroupModel>();
        public GroupRequest request { get; set; } = new GroupRequest();

        public string NameGroup { get; set; }
        #endregion

        #region GroupCommand

        public Command GroupCommand => new Command(async (p) => await GroupExecute(), (p) => !IsBusy);

        private async Task GroupExecute()
        {
            await CoreMethods.DisplayAlert("Bạn đã chọn:", "Thông báo", "Đóng");
        }
        #endregion
    }
}
