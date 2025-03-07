
namespace IRON_PROGRAMMER_BOT_Common.Feedback
{
    public static class FeedbackStorage
    {
        public static Dictionary<string, List<long>> Tutors = new Dictionary<string, List<long>>()
    {
        { "BasicsProgrammingPage", new List<long>()
        {
            125185438, 1142309198,243336774
        }
            },
        { "AdvancedPage", new List<long>()
        {
            125185438, 1142309198,243336774
        }
            },
        { "LINQ", new List<long>()
        {
            125185438, 1142309198,243336774
        }
            },
         { "CollectionPage", new List<long>()
         {
             125185438, 1142309198,243336774
         }
            },
          { "AlgorithmPage", new List<long>()
          {
              125185438, 1142309198,243336774
          }
            },
           { "CodePage", new List<long>()
           {
               125185438, 1142309198,243336774
           }
            },
            { "EvolutionPage", new List<long>()
            {
               125185438, 1142309198,243336774
            }
            }
    };

        public static Dictionary<string, List<(string, long)>> Managers = new Dictionary<string, List<(string, long)>>()
{
    {
        "eovtelega", new List<(string, long)>
        {
            ("Евгений Вишняков", 125185438)
        }
    },
    {
        "Alexey_G_M", new List<(string, long)>
        {
            ("Алексей Миронов", 1142309198)
        }
    },
    {
        "BeauMalheur", new List<(string, long)>
        {
            ("Иван Поваляев", 125185438)
        }
    }
};

        public static Dictionary<string, List<long>> GetTutors()
        {
            return Tutors;
        }

        public static Dictionary<string, List<(string, long)>> GetManagers()
        {
            return Managers;
        }
    }
}
