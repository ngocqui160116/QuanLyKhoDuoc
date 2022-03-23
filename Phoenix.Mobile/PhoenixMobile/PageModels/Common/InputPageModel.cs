using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Input;
using Phoenix.Mobile.Core.Models.Input;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Input;
using Phoenix.Shared.Input;
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
        private readonly IInputService _InputService;
        private readonly IDialogService _dialogService;

        public ObservableCollection<InputDto> Input { get; set; }

        public InputPageModel(IInputService InputService, IDialogService dialogService)
        {
            _InputService = InputService;
            _dialogService = dialogService;

        }

        public override async void Init(object initData)
        {
            Input = new ObservableCollection<InputDto>(Inputs);
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
            var data = await _InputService.GetAllInput(request);
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
        public List<InputModel> Inputs { get; set; } = new List<InputModel>();
        public InputRequest request { get; set; } = new InputRequest();


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
                    //await CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn"+SelectedInput.Id, "Đóng");
                    await CoreMethods.PushPageModel<InputInfoPageModel>(Input);
                });
            }
        }
    }
}
