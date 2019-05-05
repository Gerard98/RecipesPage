using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjektGG.DAL;
using ProjektGG.Models;
namespace ProjektGG.BL
{
    public class Authorization
    {
        
        public Boolean checkLogin(string username, string password)
        {
            DB db = new DB();
            return db.CheckLogin(username, password);
        }

        public Boolean AddUser(User user)
        {
            DB db = new DB();
            return db.AddUser(user);
        }

    }
}