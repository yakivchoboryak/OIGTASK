using SurveyManagement.Domain.Entities;

namespace SurveyManagement.App.Interfaces
{
    public interface ISurveyService
    {
        Task AddQuestionAsync(int surveyId, string questionText, QuestionType questionType);
        Task<List<SurveyQuestion>> GetSurveyQuestionsAsync(int surveyId);
        Task<int> ScheduleSurveyAsync(string name, string description, DateTime start, DateTime end, int userId);
        Task<bool> RescheduleSurveyAsync(int surveyId, DateTime newStart, DateTime newEnd);
        Task<bool> CloseSurveyAsync(int surveyId);
        Task<List<Survey>> GetAllSurveysAsync();
        Task<(List<Survey>, int)> GetPaginatedSurveysAsync(int page, int pageSize);
        Task<(List<SurveyResult>, int)> GetPaginatedSurveyResultsAsync(int surveyId, int page, int pageSize);
        Task<bool> DeleteSurveyAsync(int surveyId);
        Task<Survey?> GetSurveyByIdAsync(int surveyId);
        Task<bool> UpdateSurveyAsync(Survey survey);
    }
}
