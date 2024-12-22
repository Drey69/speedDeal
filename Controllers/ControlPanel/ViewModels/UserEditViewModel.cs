using Microsoft.AspNetCore.Mvc.Rendering;
using SpeedDeal.DbModels;

namespace SpeedDeal.Controllers.ControlPanel.ViewModels;

public class UserEditViewModel
{
    public List<SelectListItem> RolesList { get; set; }
    public User User { get; set; }
}