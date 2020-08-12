using System;
using System.IO;
using Java.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LowesApp
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "Items.db";
        private static ItemRepository _database;

        public static ItemRepository Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new ItemRepository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return _database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }


        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
