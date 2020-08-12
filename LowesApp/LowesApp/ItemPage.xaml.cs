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
    public partial class ItemPage : ContentPage
    {
        public ItemPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            string[] aisleBay = new string[2];
            aisleBay = (string[])BindingContext;
            ItemList.ItemsSource = App.Database.GetCertainItems(aisleBay[0],aisleBay[1], MainPage.IsTopStock);
            string aijsnhdjan = BayPage.BindingContextProperty.ToString();
            
            base.OnAppearing();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ItemInformation itemInformation = new ItemInformation();
            itemInformation.BindingContext = (Item)e.SelectedItem;
            await Navigation.PushAsync(itemInformation);
        }
    }
}