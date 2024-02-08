using ProjectClone.Areas.Admin.Data;
using ProjectClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClone.Areas.Admin.Utility
{
    public interface IAdminRepo:IDisposable
    {
        IEnumerable<BookTableViewModel> GetBookings();
        BookTableViewModel GetBookingDetails(int BookId, int UserId, int RoomId);
        void DeleteRecord(int BookId);
        bool AddRooms(RoomsViewModel room);
        bool EditRooms(RoomsViewModel room);
        bool DeleteRooms(int id);
        IEnumerable<RoleViewModel> getRoleList();
        IEnumerable<UsersViewModel> getUserList();
        void UpdateRole(int UserId, string UserRole);
        void DeleteUser(int UserId);
    }
}
