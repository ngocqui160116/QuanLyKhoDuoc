using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Mobile.Core.Models.Inventory;
using Phoenix.Mobile.Core.Models.OutputInfo;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.InputInfo;
using Phoenix.Shared.Inventory;
using Phoenix.Shared.MedicineItem;
using Phoenix.Shared.OutputInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class InventoryPageModel :BasePageModel
    {
        private readonly IMedicineItemService _medicineItemService;
        private readonly IInputInfoService _inputinfoService;
        private readonly IOutputInfoService _outputinfoService;
        private readonly IInventoryService _InventoryService;
        private readonly IDialogService _dialogService;

        public InventoryPageModel(IInputInfoService inputinfoService, IMedicineItemService medicineItemService, IInventoryService InventoryService, IOutputInfoService outputinfoService, IDialogService dialogService)
        {
            _medicineItemService = medicineItemService;
            _inputinfoService = inputinfoService;
            _outputinfoService = outputinfoService;
            _InventoryService = InventoryService;
            _dialogService = dialogService;

        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);
            CurrentPage.Title = "Thuốc tồn kho";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await LoadData();
        }

        private async Task LoadData()
        {
           

            var data = await _InventoryService.GetAllInventory(InventoryRequest);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Inventory = data;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(Inventory));
            }

            var data1 = await _outputinfoService.GetAllOutputInfo(OutputInfoRequest);
            if (data1 == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                OutputInfos = data1;
                //RaisePropertyChanged("Vendors");
                RaisePropertyChanged(nameof(OutputInfos));
            }
        }

        #region properties
        public List<InputInfoModel> Inventorys { get; set; } = new List<InputInfoModel>();
        public InputInfoRequest request { get; set; } = new InputInfoRequest();
        public List<InventoryModel> Inventory { get; set; } = new List<InventoryModel>();
        public InventoryRequest InventoryRequest { get; set; } = new InventoryRequest();

        public List<OutputInfoModel> OutputInfos { get; set; } = new List<OutputInfoModel>();
        public OutputInfoRequest OutputInfoRequest { get; set; } = new OutputInfoRequest();

        public OutputInfoDto outputInfoDto { get; set; }
        public InventoryModel InventoryModel { get; set; }


        #endregion

        #region BackCommand
        public Command BackCommand => new Command(async (p) => await Home(), (p) => !IsBusy);

        public async Task Home()
        {
            CoreMethods.PushPageModel<WarehousePageModel>();
        }

        #endregion

        #region SelectInventory

        InventoryModel _selectedInventory;

        public InventoryModel SelectedInventory
        {
            get
            {
                return _selectedInventory;
            }
            set
            {
                _selectedInventory = value;
                if (value != null)
                    InventorySelected.Execute(value);

            }

        }

        public Command<InventoryModel> InventorySelected
        {
            get
            {
                return new Command<InventoryModel>(async (Inventory) =>
                {
                    var data = _medicineItemService.AddItemInventory(new MedicineItemRequest
                    {
                        Medicine_Id = Inventory.IdMedicine,
                        Batch = (int)Inventory.LotNumber,
                        Count = (int)Inventory.Count,
                        DueDate = Inventory.HSD,
                        InputPrice = (double)Inventory.UnitPrice,
                        Inventory_Id = Inventory.Id,
                        Amount = 0

                    });
                    CoreMethods.PushPageModel<AddOutputPageModel>(Inventory);

                });
            }
        }

        #endregion

        #region Search

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            Inventory = GetSearchResults(query);
        });


        public List<InventoryModel> GetSearchResults(string queryString)
        {

            var normalizedQuery = queryString;
            return Inventory.Where(f => f.MedicineName.Contains(normalizedQuery)).ToList();
        }
        #endregion

    }
}
