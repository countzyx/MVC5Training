﻿using System.ComponentModel.DataAnnotations;

namespace Users.Models.UserViewModels {
    public class RoleModificationModel {
        [Required]
        public string RoleName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}