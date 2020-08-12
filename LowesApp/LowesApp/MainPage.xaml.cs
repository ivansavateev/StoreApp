using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LowesApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public static bool IsTopStock;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            RefreshAisleListView();
            base.OnAppearing();
        }        

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string selectedItem = (string)e.SelectedItem;
            BayPage bayPage = new BayPage();
            bayPage.BindingContext = selectedItem;
            await Navigation.PushAsync(bayPage);
        }

        private async void CreateItem(object sender, EventArgs e)
        {
            CreateItem createItem = new CreateItem();
            await Navigation.PushAsync(createItem);           
        }

        private void Search(object sender, EventArgs e)
        {
            SearchResults.ItemsSource = App.Database.GetCertainItems(EntrySearch.Text, IsTopStock);
        }

        private async void OnSearchItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ItemInformation itemInformation = new ItemInformation();
            itemInformation.BindingContext = (Item)e.SelectedItem;
            await Navigation.PushAsync(itemInformation);
        }

        private void ChangeStock(object sender, EventArgs e)
        {            
            if (IsTopStock)
            {
                IsTopStock = false;
                IsSomeStock.Text = "You are working DownStock";
            }
            else
            {
                IsTopStock = true;
                IsSomeStock.Text = "You are working TopStock";
            }
            RefreshAisleListView();
            RefreshSearch();
        }

        private void RefreshAisleListView()
        {
            AisleList.ItemsSource = App.Database.GetAisles(IsTopStock);
        }

        private void RefreshSearch()
        {
            EntrySearch.Text = "";
            SearchResults.ItemsSource = null;
        }
    }
}
