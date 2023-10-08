﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedDeal.DbModels;

public class User
{
    public int Id { get; set; }
    public Guid Uid { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    [ForeignKey("Group")]
    public int Group {get; set;}
}