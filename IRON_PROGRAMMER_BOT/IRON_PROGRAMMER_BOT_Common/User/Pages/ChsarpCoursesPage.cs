using IRON_PROGRAMMER_BOT_Common.Services;
using IRON_PROGRAMMER_BOT_Common.StepikAPI;
using Microsoft.Extensions.DependencyInjection;

namespace IRON_PROGRAMMER_BOT_Common.User.Pages
{
    public class ChsarpCoursesPage(IServiceProvider services)
    {
        public async Task<List<Course>> GetKeyBoardAsync()
        {
            var stepikApiProvider = services.GetRequiredService<StepikApiProvider>();
            return await stepikApiProvider.GetCoursesAsync(596721262);
        }
    }
}
