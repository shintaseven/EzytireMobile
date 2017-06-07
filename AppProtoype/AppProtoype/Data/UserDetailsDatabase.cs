using AppProtoype.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms.SQLite.SQLite;

namespace AppProtoype.Data
{
    public class UserDetailsDatabase
    {
        private SQLiteConnection database = null;
        static object locker = new object();
        UserDetailsModel UserDetailsModel
        {
            get; set;
        }

        public UserDetailsDatabase()
        {
            database =
                DependencyService.Get<ISQLite>().
                GetConnection();
            database.CreateTable<UserDetailsModel>();

            this.UserDetailsModel =
                new UserDetailsModel();

            // If the table is empty, initialize the collection
            //if (!database.Table<UserDetailsModel>().Any())
            //{
            //    //AddNewCustomer();
            //}
        }


        public int SaveUserDetails(UserDetailsModel user)
        {
            lock (locker)
            {
                UserDetailsModel existingUser = new UserDetailsModel();
                
                if (existingUser != null)
                {
                    user.UserID = existingUser.UserID;
                    database.Update(user);
                    return user.UserID;
                }
                else
                {
                    return database.Insert(user);
                }
            }
        }

        public UserDetailsModel GetCurrentUser()
        {
            lock (locker)
            {
                return database.Table<UserDetailsModel>().LastOrDefault();
            }
        }
    }
}
