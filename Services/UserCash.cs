using SpeedDeal.DbModels;

namespace SpeedDeal.Services
{
    public class UserCash
    {
        public UserCash(List<User> users)
        {
            _users = users;
        }
        private List<User> _users;

        public User? GetCurrentUser(HttpContext context)
        {
            if(context == null)
            {
                return null;
            }
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim != null)
            {
                return _users.FirstOrDefault(u => u.Id.ToString() == userIdClaim.Value);
            }
            return null;
        }
    }
}
