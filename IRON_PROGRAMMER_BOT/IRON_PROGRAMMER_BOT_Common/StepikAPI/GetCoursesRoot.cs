using IRON_PROGRAMMER_BOT_Common.StepikAPI;
using Newtonsoft.Json;

public record GetCoursesRoot
{
    [JsonProperty("courses")]
    public List<Course> Courses { get; set; }
}
