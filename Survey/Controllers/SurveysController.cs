using Microsoft.AspNetCore.Mvc;
using SurveyManagement.App.Interfaces;
using SurveyManagement.Domain.Entities;

namespace SurveyManagement.App.Controllers
{
    public class SurveysController : Controller
    {
        private readonly ISurveyService _surveyService;

        public SurveysController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        //paginated view of surveys
        [HttpGet]
        public async Task<IActionResult> SurveyList(int page = 1, int pageSize = 2)
        {
            var (surveys, totalCount) = await _surveyService.GetPaginatedSurveysAsync(page, pageSize);
            ViewBag.TotalCount = totalCount;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            return View(surveys);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("CreateSurvey", new Survey());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Survey survey)
        {
            ModelState.Remove(nameof(survey.CreatedBy));
            if (!ModelState.IsValid)
                return View("CreateSurvey", survey);

            await _surveyService.ScheduleSurveyAsync(
                survey.Name, survey.Description, survey.StartDate, survey.EndDate, survey.CreatedById
            );

            return RedirectToAction("SurveyList");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _surveyService.DeleteSurveyAsync(id);
            return RedirectToAction("SurveyList");
        }

        //Todo check this approach
        [HttpGet]
        [Route("survey/{id:int}")]
        public async Task<IActionResult> Survey(int id)
        {
            var questions = await _surveyService.GetSurveyQuestionsAsync(id);
            ViewBag.SurveyId = id;
            return View(questions);
        }

        [HttpGet]
        [Route("survey-details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var survey = await _surveyService.GetSurveyByIdAsync(id);
            if (survey == null)
            {
                return NotFound();
            }

            return View("SurveyDetails", survey);
        }

        [HttpPost]
        [Route("update-survey")]
        public async Task<IActionResult> UpdateSurvey(Survey survey)
        {
            bool success = await _surveyService.UpdateSurveyAsync(survey);
            if (!success) return NotFound();

            TempData["SuccessMessage"] = "Survey updated successfully!";

            return RedirectToAction("SurveyList");
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(int surveyId, string questionText, QuestionType questionType)
        {
            await _surveyService.AddQuestionAsync(surveyId, questionText, questionType);
            return RedirectToAction("Details", new { id = surveyId });
        }
    }
}
