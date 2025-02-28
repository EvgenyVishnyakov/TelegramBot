using System.Net.Http.Json;
using IRON_PROGRAMMER_BOT_Common.StepikAPI;

namespace IRON_PROGRAMMER_BOT_Common.Services
{
    public class StepikApiProvider(HttpClient httpClient)
    {
        public async Task<List<Course>> GetCoursesAsync(int teacherId)
        {
            try
            {
                var root = await httpClient.GetFromJsonAsync<GetCoursesRoot>($"http://stepik.org/api/courses?is_censored=false&is_unsuitable=false&order=-popularity&page=1&teacher={teacherId}");
                return root?.Courses ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];
            }
        }
    }
}
