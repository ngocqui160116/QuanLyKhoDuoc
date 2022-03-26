using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Mobile.Core.Models.Supplier;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Input;
using Phoenix.Shared.InputInfo;
using Phoenix.Shared.Supplier;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AddInputPageModel :BasePageModel
    {
        private readonly ISupplierService _supplierService;
        private readonly IInputInfoService _inputInfoService;
        private readonly IInputService _inputService;
        private readonly IDialogService _dialogService;

        public ObservableCollection<InputInfoModel> InputInfo { get; set; }
        public AddInputPageModel(ISupplierService supplierService, IInputInfoService inputInfoService, IInputService inputService, IDialogService dialogService)
        {
            _supplierService = supplierService;
            _inputInfoService = inputInfoService;
            _inputService = inputService;
            _dialogService = dialogService;

        }
        public override async void Init(object initData)
        {
            //base.Init(initData);
            //InputInfo = new ObservableCollection<InputInfoModel>(InputInfos);
            if (initData != null)
            {
                Medicine = (MedicineModel)initData;
            }
            else
            {
                Medicine = new MedicineModel();  
            }
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thêm phiếu nhập";
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
            IdMedicine = Medicine.IdMedicine;
            SDK = Medicine.RegistrationNumber;
            Name = Medicine.Name;
            NameGroup = Medicine.GroupName;
            Active = Medicine.Active;
            Content = Medicine.Content;
            Packing = Medicine.Packing;
            NameUnit = Medicine.NameUnit;
            IdUnit = Medicine.IdUnit;

#endif
            IsBusy = false;

            var data = await _supplierService.GetAllSupplier(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
                
            }
            else
            {
                Suppliers = data;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Suppliers));
            }

            var data1 = await _inputInfoService.GetAllInputInfo(InputInfoRequest);
            if (data1 == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                InputInfos = data1;

                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(InputInfos));
            }
        }


        #region properties
        public List<SupplierModel> Suppliers { get; set; } = new List<SupplierModel>();
        public SupplierRequest request { get; set; } = new SupplierRequest();
        public MedicineModel Medicine { get; set; }
        public static List<InputInfoModel> InputInfos { get; set; } = new List<InputInfoModel>();
        public InputInfoRequest InputInfoRequest { get; set; } = new InputInfoRequest();

        #endregion

        #region AddInputCommand
        public Command AddInputCommand => new Command(async (p) => await AddInputExecute(), (p) => !IsBusy);
        private async Task AddInputExecute()
        {
            try
            {
                //if (IsBusy) return;
                //IsBusy = true;
                //if (Name.IsNullOrEmpty())
                //{
                //    await _dialogService.AlertAsync("Vui lòng nhập tên thuốc");
                //    IsBusy = false;
                //    return;
                //}

                //if (SDK.IsNullOrEmpty())
                //{
                //    await _dialogService.AlertAsync("Vui lòng nhập số đăng ký");
                //    IsBusy = false;
                //    return;
                //}

                var data = await _inputInfoService.AddInputInfo(new InputInfoRequest
                {
                    IdMedicine = IdMedicine,
                    IdSupplier = SelectedSupplier.IdSupplier,
                    IdBatch = "2",
                    IdStaff = 3,
                    Id = "HD006"
                }); ;
                await CoreMethods.PushPageModel<InputPageModel>();
               
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

        public int IdMedicine { get; set; }
        public string SDK { get; set; }
        public string Name { get; set; }
        public int IdSupplier { get; set; }
        public string Active { get; set; }
        public string Content { get; set; }
        public string Packing { get; set; }
        public int IdUnit { get; set; }
        public string NameGroup { get; set; }
        public string NameUnit { get; set; }
        public string IdBatch { get; set; }
        #endregion

        #region AddMedicineCommand

        public Command AddMedicineCommand => new Command(async (p) => await AddMedicineExecute(), (p) => !IsBusy);

        private async Task AddMedicineExecute()
        {
            await CoreMethods.PushPageModel<MedicinePageModel>();
        }
        #endregion

        SupplierModel _selectedSupplier;
   

        public SupplierModel SelectedSupplier
        {
            get
            {
                return _selectedSupplier = Suppliers[0];
            }
            set
            {
                _selectedSupplier = value;
                if (value != null)
                    IdSupplier = value.IdSupplier;
            }
        }

        #region Search

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            SearchResults = GetSearchResults(query);
        });

        // public static List<InputModel> Fruits { get; set; } 
        public static List<InputInfoModel> GetSearchResults(string queryString)
        {
            var normalizedQuery = queryString?.ToLower() ?? "";
            return InputInfos.Where(f => f.MedicineName.Contains(normalizedQuery)).ToList();
        }

        List<InputInfoModel> searchResults = InputInfos;
        public List<InputInfoModel> SearchResults
        {
            get
            {
                return searchResults;
            }
            set
            {
                searchResults = value;
                
            }
        }

        #endregion
    }
}
