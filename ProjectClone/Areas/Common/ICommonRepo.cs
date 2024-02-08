
using ProjectClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClone.Areas.Common.Utility
{
    public interface ICommonRepo:IDisposable
    {
      
        bool CheckUser(UsersViewModel users);
        bool Register(UsersViewModel users);

        void InsertDefaultRole(int UserId);
        bool LogInData(UsersViewModel users);
        UsersViewModel GetUserData(string UserName);
        string GetUserRole(int UserId);
        IEnumerable<RoomsViewModel> GetOtherRooms(int id);
        void LogOut();
        int GetAvailByDate(DateTime date, int id);
    }
}
