using AirControllerWebService.Models;
using AirControllerWebService.Models.Database;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace AirControllerWebService.Helper
{
    public static class ManageDB
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static bool ChangeStatus(byte mode)
        {
            try
            {
                db.Status.FirstOrDefault().Mode = mode;
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public static byte? GetStatus()
        {
            try
            {
                byte? status = db.Status.FirstOrDefault().Mode;
                return (status ?? 0);
            }
            catch
            {
                return 0;
            }
        }

        public static bool SetCO2Value(int value)
        {
            try
            {
                var level = new Level()
                {
                    Value = value,
                    DateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                };

                db.Level.Add(level);
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static int? GetCO2LastValue()
        {
            try
            {
                var value = db.Level.ToList().Last().Value;
                return value;
            }
            catch
            {
                return null;
            }
        }
    }
}