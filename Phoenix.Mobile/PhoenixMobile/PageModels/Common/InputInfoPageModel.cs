using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
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
                InputInfo = (InputInfoModel)initData;
            }
            else
            {
                InputInfo = new InputInfoModel();
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
            Id = InputInfo.IdInput;
            SDK = InputInfo.RegistrationNumber;
            Name = InputInfo.Name;
            NameGroup = InputInfo.GroupName;
            Active = InputInfo.Active;
            Content = InputInfo.Content;
            Packing = InputInfo.Packing;
            NameUnit = InputInfo.NameUnit;
            IdUnit = InputInfo.IdUnit;
            IdGroup = InputInfo.IdGroup;

#endif
            IsBusy = false;



        }

        #region properties
        public bool IsEnabled { get; set; } = false;
        public InputInfoModel InputInfo { get; set; }

        public string SearchText { get; set; }
        public int IdInput { get; set; }
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
    }
}
