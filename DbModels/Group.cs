using System.ComponentModel.DataAnnotations;

namespace SpeedDeal.DbModels;

public class Group
{
    [Key]
    public int Id { get; set; }
    public string Name  { get; set; }
    public string HidenName  { get; set; }
    virtual public List<User> Users  { get; set; }
}
