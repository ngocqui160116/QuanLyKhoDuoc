using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Input;
using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Input;
using Phoenix.Shared.InputInfo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class InputPageModel : BasePageModel
    {
        private readonly IInputInfoService _inputInfoService;
        private readonly IDialogService _dialogService;

        public ObservableCollection<InputInfoDto> Input { get; set; }

        public InputPageModel(IInputInfoService InputInfoService, IDialogService dialogService)
        {
            _inputInfoService = InputInfoService;
            _dialogService = dialogService;

        }

        public override async void Init(object initData)
        {
            Input = new ObservableCollection<InputInfoDto>(Inputs);
            //base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Danh sách phiếu nhập";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            var data = await _inputInfoService.GetAllInputInfo(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Inputs = data;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Inputs));
            }
        }

        #region properties
        public List<InputInfoModel> Inputs { get; set; } = new List<InputInfoModel>();
        public InputInfoRequest request { get; set; } = new InputInfoRequest();

        #endregion

        #region AddInputCommand

        public Command AddInputCommand => new Command(async (p) => await AddInputExecute(), (p) => !IsBusy);

        private async Task AddInputExecute()
        {
            await CoreMethods.PushPageModel<AddInputPageModel>();
        }
        #endregion

        InputModel _selectedInput;

        public InputModel SelectedInput
        {
            get
            {
                return _selectedInput;
            }
            set
            {
                _selectedInput = value;
                if (value != null)
                    InputSelected.Execute(value);
            }
        }

        public Command<InputModel> InputSelected
        {
            get
            {
                return new Command<InputModel>(async (Input) => {
                    await CoreMethods.PushPageModel<InputInfoPageModel>(Input);
                });
            }
        }
    }
}
