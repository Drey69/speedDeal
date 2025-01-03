﻿namespace SpeedDeal.DbModels
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = "";
        public int RoleId { get; set; }
        public byte[] Salt { get; set; } = new byte[0];

        virtual public Role Role { get; set; }
        virtual public Theme Theme { get; set; } = new Theme { Color = "black", BackColor = "white" };
    }
}