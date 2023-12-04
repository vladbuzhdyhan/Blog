﻿using Microsoft.AspNetCore.Identity;

namespace WebApplication3.ViewModels
{
    public class ChangeRoleViewModel
    {
        public string UserId { get; set; }
        public string Login { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRoleViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
