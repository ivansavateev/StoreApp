using Android.Widget;
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
    public partial class CreateItem : ContentPage
    {
        
        public CreateItem()
        {
            InitializeComponent();
        }

        private void SaveItem(object sender, EventArgs e)
        {
            Item item = new Item();
            item.Aisle = EntryAisle.Text;
            item.Bay = EntryBay.Text;
            item.ItemName = EntryItemName.Text;
            item.IsTopStock = MainPage.IsTopStock;
            if (MainPage.IsTopStock)
            {
                item.Location = App.Database.GetItemFromDownStock(item.Aisle, item.Bay, item.ItemName).Location;
            }
            else
            {
                item.Location = EntryLocation.Text;
            }            
            App.Database.SaveItem(item);

            DependencyService.Get<IMessage>().ShortAlert("Item" + item.ItemName + "has been created");
        }

        protected override void OnAppearing()
        {
            EntryAisle.Text = PreviousEntries.AisleEntry;
            EntryBay.Text = PreviousEntries.BayEntry;
            LastItemsList.ItemsSource = App.Database.GetAllItems((x, y) => y.Id.CompareTo(x.Id), MainPage.IsTopStock);
            EntryLocation.IsEnabled = !MainPage.IsTopStock;
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            PreviousEntries.AisleEntry = EntryAisle.Text;
            PreviousEntries.BayEntry = EntryBay.Text;

            base.OnDisappearing();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ItemInformation itemInformation = new ItemInformation();
            itemInformation.BindingContext = (Item)e.SelectedItem;
            await Navigation.PushAsync(itemInformation);
        }        
    }


    public static class PreviousEntries
    {
        public static string AisleEntry;
        public static string BayEntry;
    }
}