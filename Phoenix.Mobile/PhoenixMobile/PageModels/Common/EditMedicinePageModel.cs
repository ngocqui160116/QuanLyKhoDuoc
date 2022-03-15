using FreshMvvm;
using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Group;
using Phoenix.Mobile.Core.Models.Medicine;
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
    public class EditMedicinePageModel : BasePageModel
    {
        private readonly IGroupService _groupService;
        private readonly IDialogService _dialogService;

        //public EditMedicinePageModel(IGroupService groupService, IDialogService dialogService)
        //{
        //    _groupService = groupService;
        //    _dialogService = dialogService;

        //}

        public EditMedicinePageModel()
        {
        }


        public List<MedicineModel> Medicines { get; set; }
        public override async void Init(object initData)
        {
            //var Medicine = initData as Medicines;
            //base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thông tin Thuốc";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
         
        }

    }
}
