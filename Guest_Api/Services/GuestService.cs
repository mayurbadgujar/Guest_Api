using Guest_Api.Model;

namespace Guest_Api.Services
{
    public class GuestService
    {
        private readonly List<Guest> _guests = new();

        public Guest AddGuest(Guest guest)
        {
            guest.Id = Guid.NewGuid();
            _guests.Add(guest);
            return guest;
        }

        public Guest GetGuestById(Guid id) => _guests.FirstOrDefault(g => g.Id == id);

        public IEnumerable<Guest> GetAllGuests() => _guests;

        public bool AddPhone(Guid guestId, string phone)
        {
            var guest = GetGuestById(guestId);
            if (guest == null || guest.PhoneNumbers.Contains(phone))
            {
                return false;
            }
            guest.PhoneNumbers.Add(phone);
            return true;
        }
    }
}
