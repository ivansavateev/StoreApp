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

            ItemList.ItemsSource = PopulateUIItems();
            string aijsnhdjan = BayPage.BindingContextProperty.ToString();
            
            base.OnAppearing();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ItemInformation itemInformation = new ItemInformation();
            itemInformation.BindingContext = (Item)e.SelectedItem;
            await Navigation.PushAsync(itemInformation);
        }

        private List<UIItem> PopulateUIItems()
        {
            string[] aisleBay = new string[2];
            aisleBay = (string[])BindingContext;
            List<Item> itemsList = App.Database.GetCertainItems(aisleBay[0], aisleBay[1], MainPage.IsTopStock);
            List<UIItem> uiItemsList = new List<UIItem>();
            foreach (Item item in itemsList)
            {
                bool greenBackground = default;
                if(item.Location == "")
                {
                    greenBackground = false;
                } else
                {
                    greenBackground = true;
                }
                UIItem uiItem = new UIItem(item, greenBackground, !App.Database.DownStockContaining(item));
                uiItemsList.Add(uiItem);
            }
            return uiItemsList;
        }
    }
}