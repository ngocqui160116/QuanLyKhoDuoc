using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Input;
using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.InputInfo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class InputInfoPageModel : BasePageModel
    {
        private readonly IInputInfoService _inputInfoService;
        private readonly IDialogService _dialogService;
        public InputInfoPageModel(IInputInfoService inputInfoService, IDialogService dialogService)
        {

            _inputInfoService = inputInfoService;
            _dialogService = dialogService;

        }
        public override async void Init(object initData)
        {
            //base.Init(initData);
            if (initData != null)
            {
                Input = (InputModel)initData;
            }
            else
            {
                Input = new InputModel();
            }
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Chi tiết phiếu nhập";
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
            InputInfos = Input.InputInfo;

            IdInput = Input.Id;
            SupplierName = Input.SupplierName;
            NameStaff = Input.NameStaff;
            DateInput = Input.DateInput;

#endif
            IsBusy = false;

        }


        #region properties
        public InputModel Input { get; set; }
        public List<InputInfoDto> InputInfos { get; set; }

        public string SearchText { get; set; }
        public string IdInput { get; set; }
        public string SupplierName { get; set; }
        public string Status { get; set; }
        public DateTime DateInput { get; set; }
        public string NameStaff { get; set; }
        public double Total { get; set; }
        #endregion
    }
}
