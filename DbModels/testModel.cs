namespace SpeedDeal.DbModels
{
    public class TestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Amount { get; set; }

       public TestModel()
        { 
            Name = string.Empty;
            Description = string.Empty;
            Amount = 0;
        }

    }
}
