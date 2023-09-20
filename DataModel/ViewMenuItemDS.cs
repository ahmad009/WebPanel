using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ViewMenuItemDS
    {
        public ViewMenuItemDS()
        {
            MenuItems = new List<ViewMenuItemDS>();
        }
        public int MenuItemID { get; set; }
        public int OperationId { get; set; }
        public bool IsSelected { get; set; }
        public bool IsHilighted { get; set; }
        public List<ViewMenuItemDS> MenuItems { get; set; }

        public bool IsLeaf { get; set; }

        public string Title { get; set; }
        public string Url { get; set; }
    }
    public class ViewMenuDS
    {
        public ViewMenuDS()
        {
            MenuItems = new List<ViewMenuItemDS>();
        }
        public List<ViewMenuItemDS> MenuItems { get; set; }
        public int SelectedMenuItemId { get; set; }
    }
}
