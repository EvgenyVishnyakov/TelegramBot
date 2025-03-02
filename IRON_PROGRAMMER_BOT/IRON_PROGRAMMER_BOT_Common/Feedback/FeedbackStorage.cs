
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

        public static List<string> Managers = new List<string>() { "@eovtelega" };//, "@Alexey_G_M", "@BeauMalheur" 

        public static List<string> GetManagers()
        {
            return Managers;
        }
    }
}
