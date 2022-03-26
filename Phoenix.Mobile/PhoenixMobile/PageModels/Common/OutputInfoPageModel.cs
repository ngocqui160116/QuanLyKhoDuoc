using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Output;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.OutputInfo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
  
    public class OutputInfoPageModel : BasePageModel
    {
        private readonly IOutputInfoService _OutputInfoService;
        private readonly IDialogService _dialogService;
        public OutputInfoPageModel(IOutputInfoService OutputInfoService, IDialogService dialogService)
        {

            _OutputInfoService = OutputInfoService;
            _dialogService = dialogService;

        }
        public override async void Init(object initData)
        {
            //base.Init(initData);
            if (initData != null)
            {
                Output = (OutputModel)initData;
            }
            else
            {
                Output = new OutputModel();
            }
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Chi tiết phiếu xuất";
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
           // OutputInfos = Output.OutputInfo;

            IdOutput = Output.Id;
            NameReason = Output.NameReason;
            NameStaff = Output.NameStaff;
            DateOutput = Output.DateOutput;

#endif
            IsBusy = false;

        }


        #region properties
        public OutputModel Output { get; set; }
        public List<OutputInfoDto> OutputInfos { get; set; }

        public string SearchText { get; set; }
        public string IdOutput { get; set; }
        public string NameReason { get; set; }
        public string Status { get; set; }
        public DateTime DateOutput { get; set; }
        public string NameStaff { get; set; }
        public double Total { get; set; }
        #endregion
    }
}
