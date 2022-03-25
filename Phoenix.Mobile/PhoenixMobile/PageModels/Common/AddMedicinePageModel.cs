using Phoenix.Framework.Extensions;
using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Common;
using Phoenix.Mobile.Core.Models.Group;
using Phoenix.Mobile.Core.Models.Unit;
using Phoenix.Mobile.Core.Services;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Group;
using Phoenix.Shared.Medicine;
using Phoenix.Shared.Unit;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AddMedicinePageModel : BasePageModel
    {
        private readonly IGroupService _groupService;
        private readonly ICameraService _cameraService;
        private readonly IUnitService _unitService;
        private readonly IMedicineService _medicineService;
        private readonly IDialogService _dialogService;

        public AddMedicinePageModel(IMedicineService medicineService, IGroupService groupService, IUnitService unitService, IDialogService dialogService)
        {
            _medicineService = medicineService;
            _groupService = groupService;
            _unitService = unitService;
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

            var data1 = await _unitService.GetAllUnit(unitrequest);
            if (data1 == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Units = data1;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Units));
            }
        }

        #region AddMedicineCommand
        public Command AddMedicineCommand => new Command(async (p) => await AddMedicineExecute(), (p) => !IsBusy);
        private async Task AddMedicineExecute()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;
                if (Name.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập tên thuốc");
                    IsBusy = false;
                    return;
                }

                if (SDK.IsNullOrEmpty())
                {
                    await _dialogService.AlertAsync("Vui lòng nhập số đăng ký");
                    IsBusy = false;
                    return;
                }
                
                var data = await _medicineService.AddMedicine(new MedicineRequest
                {
                    RegistrationNumber = SDK,
                    Name = Name,
                    IdGroup = SelectedGroup.IdGroup,
                    Active = Active,
                    Content = Content,
                    Packing = Packing,
                    IdUnit = SelectedUnit.Id
                });
                await CoreMethods.PushPageModel<MedicinePageModel>();
                await _dialogService.AlertAsync("Thêm thành công");
                IsBusy = false;

            }
            catch (Exception e)
            {
                await _dialogService.AlertAsync("Thêm thất bại");
            }
        }
        #endregion


        #region properties
        public string SearchText { get; set; }
        public int IdMedicine { get; set; }
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

        #region properties
        public List<GroupModel> Groups { get; set; } = new List<GroupModel>();
        public List<UnitModel> Units { get; set; } = new List<UnitModel>();
        public GroupRequest request { get; set; } = new GroupRequest();
        public UnitRequest unitrequest { get; set; } = new UnitRequest();

        GroupModel _selectedGroup;

        UnitModel _selectedUnit;
        public ImageSource image { get; set; }


        #endregion

        #region TakeCameraCommand

        public Command TakeCameraCommand => new Command(async (p) => await TakeCameraExecute(), (p) => !IsBusy);

        private async Task TakeCameraExecute()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await CoreMethods.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Test",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front
            });

            if (file == null)
                return;

            var result = new BinaryAsset();

            //CoreMethods.DisplayAlert("File Location", file.Path, "OK");

            image = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });

            //using (var ms = new MemoryStream())
            //{
            //    file.GetStream().CopyTo(ms);
            //    result.Path = file.Path;
            //    result.Content = ms.ToArray();
            //    //result.Ext = file.Path.GetFileType();
            //}

        }
        #endregion


        #region TakeImageCommand

        public Command TakeImageCommand => new Command(async (p) => await TakeImageExecute(), (p) => !IsBusy);

        private async Task TakeImageExecute()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await CoreMethods.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium,

            });


            if (file == null)
                return;

            image = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }
        #endregion

        public GroupModel SelectedGroup
        {
            get
            {

                return _selectedGroup;
            }
            set
            {
                _selectedGroup = value;
                if (value != null)
                    IdGroup = value.IdGroup;
            }
        }

       
        public UnitModel SelectedUnit
        {
            get
            {
                return _selectedUnit;
            }
            set
            {
                _selectedUnit = value;
                if (value != null)
                    IdUnit = value.Id;
            }
        }

        
    }
}
