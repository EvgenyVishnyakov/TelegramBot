namespace IRON_PROGRAMMER_BOT_Common.Feedback
{
    public class Tutor(string firstName, string lastname, string phone)
    {
        public string FirstName { get; set; } = firstName;

        public string LastName { get; set; } = lastname;

        public string Phone { get; set; } = phone;

        public List<Tutor> TutorList { get; set; }

        public List<Tutor> AddTutor(Tutor tutor)
        {
            if (!TutorList.Contains(tutor))
                TutorList.Add(tutor);
            return TutorList;
        }
    }
}
