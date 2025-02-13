namespace SurveyManagement.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Survey> CreatedSurveys { get; set; } = new List<Survey>();

        public ICollection<SurveyResult> SurveyResults { get; set; } = new List<SurveyResult>();
    }
}
