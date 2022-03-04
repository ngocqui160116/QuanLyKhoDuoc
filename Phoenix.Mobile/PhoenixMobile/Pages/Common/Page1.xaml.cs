using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Phoenix.Mobile.Pages.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1(int Id, string TenNhaCungCap, string Phone)
        {
            InitializeComponent();

            txtName.Text = TenNhaCungCap;
            txtPhone.Text = Phone;
        }
    }
}