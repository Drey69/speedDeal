namespace SpeedDeal.DbModels;

public class Messages
{
    public int Id { get; set; }
    public string Message { get; set; }
    public int  UserId { get; set; }
    public DateTime Created { get; set; }
}