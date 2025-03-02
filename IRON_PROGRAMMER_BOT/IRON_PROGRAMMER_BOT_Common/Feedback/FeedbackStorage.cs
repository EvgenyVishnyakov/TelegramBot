
namespace IRON_PROGRAMMER_BOT_Common.Feedback
{
    public static class FeedbackStorage
    {
        public static Dictionary<string, List<long>> listTutors = new Dictionary<string, List<long>>()
    {
        { "Основы программирования", new List<long>() { 125185438, 1142309198, 243336774 } },
        { "Для продвинутых", new List<long>() { 125185438, 1142309198, 243336774 } },
        { "LINQ", new List<long>() { 125185438, 1142309198, 243336774 } }
    };
        public static List<long> Managers = new List<long>() { 125185438, 1142309198, 243336774 };

        public static List<long> GetManagers()
        {
            return Managers;
        }
    }
}
