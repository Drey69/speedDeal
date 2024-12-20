namespace SpeedDeal.Services
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.EntityFrameworkCore;
    using SpeedDeal.DbModels;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class LoadUserFilter : IAsyncActionFilter
    {
        private readonly AppDbContext _context;
        public LoadUserFilter(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userClam = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
            var userId = userClam == null ? null : userClam.Value;

            if (!string.IsNullOrEmpty(userId))
            {
               var user = await _context.Users
                    .Include(u => u.Theme)
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
                if (user != null)
                {
                    context.HttpContext.Items["CurrentUser"] = user;
                }
            }

            await next(); // Продолжаем выполнение действия
        }
    }
}
