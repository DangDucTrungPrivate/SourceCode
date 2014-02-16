using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GCMP.Models.Entities;

namespace GCMP.Models.Helper
{
    public static class Const
    {

        // * general roles
        public const string User = "User";
        public const string Admin = "Administrator";
        public const string Moderator = "Moderator";
        public const string Guest = "Guest";

        // * general status of users
        public const string UActive = "Active";
        public const string Ubaned = "Baned";
        public const string UConfirm = "Confirm";
        public const string UDeactive = "Deactive";

        // * general status of category
        public const string CActive = "Available";
        public const string AUnactive = "Not Available";
        

        // * general status of cards
        public const string CaActive = "Available";
        public const string CaClosed = "Closed";
    }
}