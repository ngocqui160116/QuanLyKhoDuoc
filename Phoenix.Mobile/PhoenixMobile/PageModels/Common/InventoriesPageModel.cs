using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Mobile.Core.Models.Inventory;
using Phoenix.Mobile.Core.Models.OutputInfo;
using Phoenix.Mobile.Core.Services.Common;
using Phoenix.Mobile.Helpers;
using Phoenix.Shared.Inventory;
using Phoenix.Shared.MedicineItem;
using Phoenix.Shared.OutputInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class InventoriesPageModel : BasePageModel
    {
        private readonly IMedicineItemService _medicineItemService;
        private readonly IInputInfoService _inputinfoService;
        private readonly IOutputInfoService _outputinfoService;
        private readonly IInventoryService _InventoryService;
        private readonly IDialogService _dialogService;

        public InventoriesPageModel(IInputInfoService inputinfoService, IMedicineItemService medicineItemService, IInventoryService InventoryService, IOutputInfoService outputinfoService, IDialogService dialogService)
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
            #region Inventory

            var data = await _InventoryService.GetAllInventory(InventoryRequest);
            if (data == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                Inventory = data;
                RaisePropertyChanged(nameof(Inventory));
            }
            #endregion

            #region OutputInfo

            var data1 = await _outputinfoService.GetAllOutputInfo(OutputInfoRequest);
            if (data1 == null)
            {
                await _dialogService.AlertAsync("Lỗi kết nối mạng!", "Lỗi", "OK");
            }
            else
            {
                OutputInfos = data1;
                RaisePropertyChanged(nameof(OutputInfos));
            }
            #endregion
        }

        #region properties
        public List<InventoryModel> Inventory { get; set; } = new List<InventoryModel>();
        public InventoryRequest InventoryRequest { get; set; } = new InventoryRequest();
        public List<OutputInfoModel> OutputInfos { get; set; } = new List<OutputInfoModel>();
        public OutputInfoRequest OutputInfoRequest { get; set; } = new OutputInfoRequest();
        public InventoryModel InventoryModel { get; set; }
        #endregion

        #region SelectInventories

        InventoryModel _selectedInventories;

        public InventoryModel SelectedInventories
        {
            get
            {
                return _selectedInventories;
            }
            set
            {
                _selectedInventories = value;
                if (value != null)
                    InventoriesSelected.Execute(value);

            }

        }

        public Command<InventoryModel> InventoriesSelected
        {
            get
            {
                return new Command<InventoryModel>(async (Inventories) =>
                {
                    var data = _medicineItemService.AddItemInventory(new MedicineItemRequest
                    {
                        Medicine_Id = Inventories.IdMedicine,
                        Batch = (int)Inventories.LotNumber,
                        Count = (int)Inventories.Count,
                        DueDate = Inventories.HSD,
                        InputPrice = (double)Inventories.UnitPrice,
                        Inventory_Id = Inventories.Id,
                        Amount = 0

                    });
                    CoreMethods.PushPageModel<AddStockPageModel>(Inventories);

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
