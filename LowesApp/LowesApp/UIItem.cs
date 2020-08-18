using System;
using System.Collections.Generic;
using System.Text;

namespace LowesApp
{
    class UIItem : Item
    {
        public bool BackGroundColorGreen { get; private set; }
        public bool TextColorRed { get; private set; }
        public UIItem(Item item, bool backGroundColorGreen, bool textColorRed)
        {
            Id = item.Id;
            ItemName = item.ItemName;
            Bay = item.Bay;
            Aisle = item.Aisle;
            Location = item.Location;
            IsTopStock = item.IsTopStock;
            BackGroundColorGreen = backGroundColorGreen;
            TextColorRed = textColorRed;
        }
    }
}
