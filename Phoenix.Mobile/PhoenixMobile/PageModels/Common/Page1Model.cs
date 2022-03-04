using Phoenix.Mobile.Helpers;
using System;

using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class Page1Model : BasePageModel
    {
        public Page1Model()
        {

        }

        private Command<object> backButtonCommand;

        #region Commands

        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public Command<object> BackButtonCommand
        {
            get
            {
                return this.backButtonCommand ?? (this.backButtonCommand = new Command<object>(this.BackButtonClicked));
            }
        }

        #endregion

        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void BackButtonClicked(object obj)
        {

                Application.Current.MainPage.Navigation.PopAsync();
          
        }


        public override async void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, true);
            CurrentPage.Title = "Main";
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            TestString = "hello world";
        }
        #region commands

        #endregion

        #region properties
        public string TestString { get; set; }
        #endregion
    }



}
