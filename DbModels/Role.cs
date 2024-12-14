

using SpeedDeal.DbModels;

public class Role()
{
    public int RoleId { get; set; }
    public string RoleName { get; set;} = "";
    public List<User> Users { get; set; } = new List<User>();
    public List<Permission> Permisions { get; set; } = new List<Permission>();
}