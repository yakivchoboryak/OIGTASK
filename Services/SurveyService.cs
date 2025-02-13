using Microsoft.EntityFrameworkCore;
using SurveyManagement.App.Interfaces;
using SurveyManagement.Domain.Entities;
using SurveyManagement.Infrastructure.Persistence;

namespace SurveyManagement.App.Services
{
    //TODO IMPLEMENT IREPOSITORY
    //USE IREPOSITORY TO INTERACT WITH DBCONTEXT
    public class SurveyService : ISurveyService
    {
        private readonly SurveyDbContext _context;

        public SurveyService(SurveyDbContext context)
        {
            _context = context;
        }

        public async Task AddQuestionAsync(int surveyId, string questionText, QuestionType questionType)
        {
            var question = new SurveyQuestion
            {
                SurveyId = surveyId,
                QuestionText = questionText,
                QuestionType = questionType
            };

            _context.SurveyQuestions.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SurveyQuestion>> GetSurveyQuestionsAsync(int surveyId)
        {
            return await _context.SurveyQuestions
                .Where(q => q.SurveyId == surveyId)
                .ToListAsync();
        }

        public async Task<int> ScheduleSurveyAsync(string name, string description, DateTime start, DateTime end, int userId)
        {
            CheckScheduleDates(start, end);

            // Validate that user exists before assigning
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
                throw new KeyNotFoundException("User not found.");

            var survey = new Survey
            {
                Name = name,
                Description = description,
                StartDate = start,
                EndDate = end,
                Status = SurveyStatus.Scheduled,
                CreatedById = userId
            };

            _context.Surveys.Add(survey);
            await _context.SaveChangesAsync();
            return survey.Id;
        }


        public async Task<bool> RescheduleSurveyAsync(int surveyId, DateTime newStart, DateTime newEnd)
        {
            var survey = await _context.Surveys.FindAsync(surveyId);
            if (survey == null) return false;

            CheckScheduleDates(newStart, newEnd);

            try
            {
                survey.RescheduleSurvey(newStart, newEnd);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> CloseSurveyAsync(int surveyId)
        {
            var survey = await _context.Surveys.FindAsync(surveyId);
            if (survey == null) return false;

            try
            {
                survey.CloseSurvey();
                await _context.SaveChangesAsync();
                return true;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<Survey>> GetAllSurveysAsync()
        {
            return await _context.Surveys.ToListAsync();
        }

        public async Task<(List<Survey>, int)> GetPaginatedSurveysAsync(int page, int pageSize)
        {
            var totalSurveys = await _context.Surveys.CountAsync();

            var surveys = await _context.Surveys
                .OrderByDescending(s => s.StartDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (surveys, totalSurveys);
        }

        public async Task<bool> DeleteSurveyAsync(int surveyId)
        {
            var survey = await _context.Surveys.FindAsync(surveyId);
            if (survey == null) return false;

            _context.Surveys.Remove(survey);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(List<SurveyResult>, int)> GetPaginatedSurveyResultsAsync(int surveyId, int page, int pageSize)
        {
            var totalResults = await _context.SurveyResults.Where(r => r.SurveyId == surveyId).CountAsync();

            var results = await _context.SurveyResults
                .Where(r => r.SurveyId == surveyId)
                .Include(r => r.User)
                .OrderByDescending(r => r.SubmittedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (results, totalResults);
        }

        public async Task<Survey?> GetSurveyByIdAsync(int surveyId)
        {
            return await _context.Surveys
                .Include(s => s.CreatedBy)
                .Include(s => s.SurveyQuestions)
                .FirstOrDefaultAsync(s => s.Id == surveyId);
        }

        public async Task<bool> UpdateSurveyAsync(Survey survey)
        {
            var existingSurvey = await _context.Surveys.FindAsync(survey.Id);
            if (existingSurvey == null) return false;
            CheckScheduleDates(survey.StartDate,survey.EndDate);

            existingSurvey.Name = survey.Name;
            existingSurvey.Description = survey.Description;
            existingSurvey.StartDate = survey.StartDate;
            existingSurvey.EndDate = survey.EndDate;
            existingSurvey.Status = survey.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        private static void CheckScheduleDates(DateTime start, DateTime end)
        {
            if (start < DateTime.UtcNow)
                throw new ArgumentException("Survey start time must be in the future.");

            if (end <= start.AddHours(1))
                throw new ArgumentException("Survey end time must be at least 1 hour after start.");
        }
    }
}
