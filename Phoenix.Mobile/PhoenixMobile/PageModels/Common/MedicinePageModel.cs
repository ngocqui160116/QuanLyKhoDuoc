using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Mobile.Core.Models.Unit;
using Phoenix.Mobile.Core.Models.Vendor;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Medicine;
using Phoenix.Shared.Unit;
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

    public class MedicinePageModel : BasePageModel
    {
        private readonly IMedicineService _medicineService;
        private readonly IDialogService _dialogService;
        //public ObservableCollection<MedicineModel> Medicine { get; set; }

        public MedicinePageModel(IMedicineService medicineService, IDialogService dialogService)
        {
            _medicineService = medicineService;
            _dialogService = dialogService;
        }

        public override async void Init(object initData)
        {
            // Medicine = new ObservableCollection<MedicineModel>(Medicines);
            if (initData != null)
            {
                Medicine = (MedicineModel)initData;
               

            }
            else
            {
                Medicine = new MedicineModel();

            }
            //base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Danh mục thuốc";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
            var data = await _medicineService.GetAllMedicine(request);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Medicines = data;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Medicines));
            }
        }

        #region SelectMedicine

        MedicineModel _selectedMedicine;

        public MedicineModel SelectedMedicine
        {
            get
            {
                return _selectedMedicine;
            }
            set
            {
                _selectedMedicine = value;
                if (value != null)
                //ListMedicines.Add(SelectedMedicine);
                MedicineSelected.Execute(value);
                
            }
            
        }
        #endregion

        //#region MedicineSelectedCommand
        //public ICommand MedicineSelectedCommand => new Command(async (p) => await MedicineSelected(), (p) => !IsBusy);
        //private async Task MedicineSelected()
        //{

        //    await CoreMethods.PushPageModel<AddInputInfoPageModel>(ListMedicines);



        //    //await CoreMethods.DisplayAlert("Thêm thành công", "Bạn đã chọn" + Medicine.Name, "Đóng");

        //}
        //#endregion

        #region MedicineSelected
        public Command<MedicineModel> MedicineSelected
        {
            get
            {
                return new Command<MedicineModel>(async (Medicine) =>
                {
                    //await CoreMethods.PushPageModel<AddInputPageModel>(Medicine);
                    await CoreMethods.PushPageModel<AddInputInfoPageModel>(Medicine);
                });
            }
        }

#endregion


        #region properties
        public MedicineModel Medicine { get; set; }
        public List<MedicineModel> Medicines { get; set; } = new List<MedicineModel>();
        public List<MedicineModel> ListMedicines { get; set; } = new List<MedicineModel>();
        public MedicineRequest request { get; set; } = new MedicineRequest();
        public int IdMedicine { get; set; }
        #endregion

        #region AddMedicineCommand

        public Command AddMedicineCommand => new Command(async (p) => await AddMedicineExecute(), (p) => !IsBusy);

        private async Task AddMedicineExecute()
        {
            await CoreMethods.PushPageModel<AddMedicinePageModel>();
        }
        #endregion

        //#region Search

        //public ICommand PerformSearch => new Command<string>((string query) =>
        //{
        //    SearchResults = GetSearchResults(query);
        //});

        //// public static List<InputModel> Fruits { get; set; } 
        //public static List<MedicineModel> GetSearchResults(string queryString)
        //{
        //    var normalizedQuery = queryString?.ToLower() ?? "";
        //    return Medicines.Where(f => f.Name.ToUpperInvariant().Contains(normalizedQuery)).ToList();
        //}

        //List<MedicineModel> searchResults = Medicines;
        //public List<MedicineModel> SearchResults
        //{
        //    get
        //    {
        //        return searchResults;
        //    }
        //    set
        //    {
        //        searchResults = value;
        //    }
        //}

        //#endregion
    }
}
