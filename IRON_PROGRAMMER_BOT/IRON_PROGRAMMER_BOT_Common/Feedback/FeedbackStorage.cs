
namespace IRON_PROGRAMMER_BOT_Common.Feedback
{
    public static class FeedbackStorage
    {
        public static Dictionary<string, List<Dictionary<string, string>>> listTutors = new Dictionary<string, List<Dictionary<string, string>>>()
    {
        {
            "Основы программирования", new List<Dictionary<string, string>>()
            {
                new Dictionary<string, string>(){{ "Вишняков Евгений", "@eovtelega" }},
                new Dictionary<string, string>(){{ "Миронов Алексей", "@Alexey_G_M" }},
                new Dictionary<string, string>(){{ "Иосиф Дзеранов", "@JosefDzeranov" }},
                new Dictionary<string, string>(){{ "Поваляев Иван", "@BeauMalheur" }}}
        },
        {
            "Для продвинутых", new List<Dictionary<string, string>>()
            {
                new Dictionary<string, string>(){{ "Вишняков Евгений", "@eovtelega" }},
                new Dictionary<string, string>(){{ "Миронов Алексей", "@Alexey_G_M" }},
                new Dictionary<string, string>(){{ "Поваляев Иван", "@BeauMalheur" }}}
        },
        {
            "LINQ", new List<Dictionary<string, string>>()
             {
                new Dictionary<string, string>(){{ "Вишняков Евгений", "@eovtelega" }},
                new Dictionary<string, string>(){{ "Миронов Алексей", "@Alexey_G_M" }}}
            }
        };

        public static List<long> Managers = new List<long>() { 125185438, 1142309198, 243336774 };

        public static List<long> GetManagers()
        {
            return Managers;
        }
    }
}
