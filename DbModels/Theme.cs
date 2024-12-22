namespace SpeedDeal.DbModels
{
    public class Theme
    {
        public int Id { get; set; }
        public int UserId {get; set;}
        public string Name { get; set; } = "";
        public string BackColor {get; set;} = "";
        public string Color {get; set;} = "";
    }
}