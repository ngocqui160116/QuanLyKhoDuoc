using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Phoenix.Mobile.Core.Models.Vendor;
using Phoenix.Mobile.Models.Common;
using Phoenix.Mobile.PageModels.Common;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Phoenix.Mobile.Pages.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SupplierPage : ContentPage
    {
   
        public SupplierPage()
        {
            InitializeComponent();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            //await Application.Current.MainPage.Navigation.PushModalAsync(new AccountPage(selectedItem.Name));
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}