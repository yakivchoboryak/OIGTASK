namespace SurveyManagement.Domain.Entities
{
    public class SurveyResult
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Answer { get; set; } //not implemented yet
        public DateTime SubmittedAt { get; set; }
    }
}
