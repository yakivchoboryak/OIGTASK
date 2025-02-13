namespace SurveyManagement.Domain.Entities
{
    public class SurveyQuestion
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }

        public string QuestionText { get; set; }
        public QuestionType QuestionType { get; set; }

    }

    //TODO implement answers
    public enum QuestionType
    {
        Text,
        MultipleChoice,
        Checkbox,
        Numeric 
    }
}
