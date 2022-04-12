using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Staff;
using Phoenix.Mobile.Core.Services;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared;
using Phoenix.Shared.Staff;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class ProfilePageModel : BasePageModel
    {
        private readonly IDialogService _dialogService;
        private readonly IAuthService _authService;
        private readonly IStaffService _staffService;

        public ProfilePageModel(IDialogService dialogService, IStaffService staffService, IAuthService authService)
        {
            _staffService = staffService;
            _dialogService = dialogService;
            _authService = authService;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thông tin tài khoản";
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

           


        #region Users
        var data = await _authService.GetUserFromToken();
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Users = data;
                RaisePropertyChanged(nameof(Users));
            }
            #endregion

            #region Staff
            var data1 = await _staffService.GetStaffById(Users.Id);
            if (data1 == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Staffs = data1;
                RaisePropertyChanged(nameof(Staffs));
            }

            IdStaff = Staffs.IdStaff;
            Name = Staffs.Name;
            Birth = Staffs.Birth;
            Gender = Staffs.Gender;
            PhoneNumber = Staffs.PhoneNumber;
            Address = Staffs.Address;
            Authority = Staffs.Authority;
            User_Id = Staffs.User_Id;

            #endregion
            IsBusy = false;
        }
        #region propertiesCODE

        public int IdStaff { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; } = DateTime.Today;
        public string Gender { get; set; } = "Nam";
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Authority { get; set; }
        public int User_Id { get; set; }

        #endregion

        #region properties
        public bool IsEnabled { get; set; } = false;
        public UserDto Users { get; set; } = new UserDto();
        public StaffDto Staffs { get; set; } = new StaffDto();
        public StaffRequest StaffRequest { get; set; } = new StaffRequest();

        #endregion

        #region EditCommand
        public Command EditCommand => new Command(async (p) => await EditExecute(), (p) => !IsBusy);
        private async Task EditExecute()
        {
            IsEnabled = true;

        }
        #endregion

        #region UpdateStaffCommand
        public Command UpdateStaffCommand => new Command(async (p) => await UpdateStaffExecute(), (p) => !IsBusy);
        private async Task UpdateStaffExecute()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;

                var data = await _staffService.UpdateStaff(IdStaff, new StaffRequest
                {
                    IdStaff = IdStaff,
                    Name = Name,
                    Birth = Birth,
                    Gender = Gender,
                    PhoneNumber = PhoneNumber,
                    Address = Address,
                    Authority = Authority,
                    User_Id = User_Id
                 });
                //await CoreMethods.PushPageModel<SupplierPageModel>();
                await _dialogService.AlertAsync("Cập nhật thành công");
                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Cập nhật thất bại");
            }
        }
        #endregion
    }
}
