
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography.X509Certificates;

namespace SpeedDeal.DbModels
{
    public class DealLink
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Uri { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }



       public DealLink()
        { 
            
            Name = string.Empty;
            Description = string.Empty;
            Uri = string.Empty;
        }

    }
}
