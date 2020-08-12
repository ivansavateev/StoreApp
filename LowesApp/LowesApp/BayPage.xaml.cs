using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LowesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BayPage : ContentPage
    {
        public BayPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            BayList.ItemsSource = App.Database.GetBays(this.BindingContext.ToString(), MainPage.IsTopStock);
            base.OnAppearing();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {            
            string selectedItem = (string)e.SelectedItem;
            string[] aisleBay = new string[2];
            aisleBay[0] = BindingContext.ToString();
            aisleBay[1] = selectedItem;
            ItemPage itemPage = new ItemPage();
            itemPage.BindingContext = aisleBay;            
            await Navigation.PushAsync(itemPage);
        }
    }
}