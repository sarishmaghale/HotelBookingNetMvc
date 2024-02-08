using ProjectClone.Areas.Admin.Data;
using ProjectClone.Areas.User.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClone.Areas.User.Utility
{
    public interface IUserRepo:IDisposable
    {
        BookingViewModel GetDetails(int id, string UserName);

        bool VerifyRequest(BookingViewModel books);
       int ConfirmBooking(BookingViewModel books);
        BookingViewModel GetBookingDetails(int Id);
        IEnumerable<BookingViewModel> GetUserBookings(string name);
    }
}
