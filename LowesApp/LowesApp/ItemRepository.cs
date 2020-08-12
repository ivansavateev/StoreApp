using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LowesApp
{
    public class ItemRepository
    {
        private SQLiteConnection _database;
        
        public ItemRepository(string dataBasePath)
        {
            _database = new SQLiteConnection(dataBasePath);
            _database.CreateTable<Item>();
        }

        public List<Item> GetCertainItems(string aisleNum, string bayNum, bool isTopStock)
        {
            List<Item> items = new List<Item>();
            foreach (var item in _database.Table<Item>().Where(x => x.Aisle == aisleNum && x.Bay == bayNum && x.IsTopStock == isTopStock))
            {
                items.Add(item);
            }
            return items;
        }

        public Item GetItemFromDownStock(string aisleNum, string bayNum, string itemName)
        {            
            foreach (var item in _database.Table<Item>().Where(x => x.Aisle == aisleNum && x.Bay == bayNum && x.ItemName == itemName && x.IsTopStock == false))
            {
                return item;
            }
            return new Item();
        }

        public List<Item> GetCertainItems(string itemNumber, bool isTopStock)
        {
            if (itemNumber != "")
            {
                List<Item> items = new List<Item>();
                foreach (var item in _database.Table<Item>().Where(x => x.ItemName.StartsWith(itemNumber) && x.IsTopStock == isTopStock))
                {
                    items.Add(item);
                }
                return items;
            }
            return new List<Item>();
        }

        public List<Item> GetAllItems()
        {
            List<Item> items = new List<Item>();
            foreach (var item in _database.Table<Item>())
            {
                items.Add(item);
            }
            return items;
        }

        public List<Item> GetAllItems(Comparison<Item> sortType, bool isTopStock)
        {
            List<Item> items = new List<Item>();
            foreach (var item in _database.Table<Item>().Where(x => x.IsTopStock == isTopStock))
            {
                items.Add(item);
            }
            items.Sort(sortType);
            return items;
        }

        public List<string> GetAisles(bool isTopStock)
        {
            List<string> aisles = new List<string>();
            foreach (var item in _database.Table<Item>().Where(x => x.Aisle != null && x.IsTopStock == isTopStock))
            {
                if (!aisles.Contains(item.Aisle))
                {
                    aisles.Add(item.Aisle);
                }
            }
            aisles.Sort();
            return aisles;
        }

        public List<string> GetBays(string aisleNum, bool isTopStock)
        {
            List<string> bays = new List<string>();
            foreach (var item in _database.Table<Item>().Where(x => x.Aisle == aisleNum && x.IsTopStock == isTopStock))
            {
                if (!bays.Contains(item.Bay))
                {
                    bays.Add(item.Bay);
                }
            }
            bays.Sort();
            return bays;
        }

        public int SaveItem(Item item)
        {
            if (item.Id != 0)
            {
                _database.Update(item);
                return item.Id;
            }
            else
            {
                return _database.Insert(item);
            }

        }

        public void DeleteItem(int id)
        {
            _database.Delete<Item>(id);
        }        

        //public void TempChange()
        //{
        //    foreach (var item in _database.Table<Item>().Where(x => x.Id > 0))
        //    {
        //        item.IsTopStock = true;
        //        SaveItem(item);
        //    }
        //}
    }
}
