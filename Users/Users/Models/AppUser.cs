﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Users.Models {
    public enum Cities {
        LONDON, PARIS, CHICAGO
    }


    public class AppUser : IdentityUser {
        public Cities City { get; set; }
    }
}