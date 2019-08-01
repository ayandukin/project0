using ArmoryDb.DataAccess;
using ArmoryDb.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace ArmoryDb.App
{
    /// <summary>
    /// Unique business rules.
    /// The repository is static as there is no reason to create multiple objects of this class; static methods always callable.
    /// </summary>
    public static class Rules
    {
        /// <summary>
        /// Checks if the selected item is an automatic firearm
        /// </summary>
        /// <param name="name">name of the item</param>
        /// <returns>true if automatic</returns>
        public static bool IsAutomatic(string name)
        {
            Item item = ArmoryRepo.GetItem(name);
            return item.Automatic ? true : false;
        }
    }
}
