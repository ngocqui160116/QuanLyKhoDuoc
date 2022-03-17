using FreshMvvm;
using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Group;
using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Group;
using Phoenix.Shared.Medicine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class EditMedicinePageModel : BasePageModel
    {
        private readonly IGroupService _groupService;
        private readonly IDialogService _dialogService;

        public EditMedicinePageModel(IGroupService groupService, IDialogService dialogService)
        {
            _groupService = groupService;
            _dialogService = dialogService;

        }
        #region properties
       public MedicineModel Medicine { get; set; }

        #endregion
        public override async void Init(object initData)
        {
            if (initData != null)
            {
                Medicine = (MedicineModel)initData;
            }
            else
            {
                Medicine = new MedicineModel();
            }
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thông tin Thuốc";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
         
        }

    }
}
