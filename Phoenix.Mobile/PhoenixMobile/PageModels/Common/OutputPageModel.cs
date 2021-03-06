using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Output;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class OutputPageModel : BasePageModel
    {
        private readonly IOutputService _OutputService;
        private readonly IDialogService _dialogService;

        public OutputPageModel(IOutputService OutputService, IDialogService dialogService)
        {
            _OutputService = OutputService;
            _dialogService = dialogService;

        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Danh sách phiếu xuất";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            #region Output

            var data = await _OutputService.GetAllOutput(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Outputs = data;
                RaisePropertyChanged(nameof(Outputs));
            }
            #endregion
        }

        #region properties
        public List<OutputModel> Outputs { get; set; } = new List<OutputModel>();
        public OutputRequest request { get; set; } = new OutputRequest();

        #endregion

        #region AddOutputCommand

        public Command AddOutputCommand => new Command(async (p) => await AddOutputExecute(), (p) => !IsBusy);

        private async Task AddOutputExecute()
        {
            await CoreMethods.PushPageModel<AddOutputPageModel>();
        }
        #endregion

        #region BackCommand
        public Command BackCommand => new Command(async (p) => await Home(), (p) => !IsBusy);

        public async Task Home()
        {
            CoreMethods.PushPageModel<WarehousePageModel>();
        }

        #endregion

        #region SelectedOutput

        OutputModel _selectedOutput;

        public OutputModel SelectedOutput
        {
            get
            {
                return _selectedOutput;
            }
            set
            {
                _selectedOutput = value;
                if (value != null)
                    OutputSelected.Execute(value);
            }
        }

        public Command<OutputModel> OutputSelected
        {
            get
            {
                return new Command<OutputModel>(async (Output) =>
                {
                    //await CoreMethods.DisplayAlert("Thông báo", "Bạn đã chọn"+SelectedOutput.Id, "Đóng");
                    await CoreMethods.PushPageModel<OutputInfoPageModel>(Output);
                });
            }
        }
        #endregion

        #region Search

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            Outputs = GetSearchResults(query);
        });


        public List<OutputModel> GetSearchResults(string queryString)
        {

            var normalizedQuery = queryString;
            return Outputs.Where(f => f.Id.ToString().Contains(normalizedQuery)).ToList();
        }
        #endregion

    }
}
