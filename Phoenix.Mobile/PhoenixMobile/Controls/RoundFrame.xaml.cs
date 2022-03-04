using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Phoenix.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundFrame : ContentView
    {
        public RoundFrame()
        {
            InitializeComponent();
        }

        public string Text
        {
            get
            {
                return lblName.Text;
            }
            set
            {
                lblName.Text = value;
            }
        }

        public ImageSource Icon
        {
            get
            {
                return img.Source;
            }
            set
            {
                img.Source = value;
            }
        }

        public Color BGColor
        {
            get
            {
                return frame.BackgroundColor;
            }
            set
            {
                frame.BackgroundColor = value;
            }
        }

        public ImageSource BackGroundImage
        {
            get
            {
                return imgBG.Source;
            }
            set
            {
                imgBG.Source = value;
            }
        }
    }
}