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
    public partial class ItemInformation : ContentPage
    {
        private Item _item;
        public ItemInformation()
        {
            InitializeComponent();            
        }

        protected override void OnAppearing()
        {
            _item = (Item)BindingContext;            
            base.OnAppearing();
        }

        private void UpdateItem(object sender, EventArgs e)
        {            
            _item.Aisle = EntryAisle.Text;
            _item.Bay = EntryBay.Text;
            _item.ItemName = EntryItemName.Text;
            _item.Location = EntryLocation.Text;
            App.Database.SaveItem(_item);

            DependencyService.Get<IMessage>().ShortAlert("Item" + _item.ItemName + "has been updated");
        }

        private async void DeleteItem(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Warning", "Do you want delete this Item?", "Yes", "No");
            if(result) 
            {                
                App.Database.DeleteItem(_item.Id);
                await Navigation.PopAsync();
                Console.WriteLine();
            }
        }
    }
}