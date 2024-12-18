using System.Collections.Generic;
using System.Linq;
using System.Collections;
using SpeedDeal.DbModels;

namespace SpeedDeal.Controllers.ControlPanel.ViewModels
{
    public class ControlPanelViewModel
    {
        public List<ControlPanelPageItem> Menus { get; protected set; }
        public User User { get; protected set; }  
        public ControlPanelViewModel(User user,
         List<ControlPanelPageItem> menus)
        {
            User = user;
            Menus = menus;
        }
    }

    public class ControlPanelPageItem
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string RoleName {get; set;}
    }
}