namespace SurveyManagement.Domain.Entities
{
    public class Survey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } //todo: make field private/protected and move interactions w it only to methods
        public SurveyStatus Status { get; set; } //todo: make field private/protected and move interactions w it only to methods


        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }


        public ICollection<SurveyResult> SurveyResults { get; set; } = new List<SurveyResult>();
        public ICollection<SurveyQuestion> SurveyQuestions { get; set; } = new List<SurveyQuestion>();

        public void ScheduleSurvey(DateTime start, DateTime end)
        {
            if (start <= DateTime.Now)
                throw new InvalidOperationException("A survey can only be scheduled for a future date/time.");

            if (end <= start.AddHours(1))
                throw new InvalidOperationException("The end date must be at least one hour after the start date.");

            StartDate = start;
            EndDate = end;
            Status = SurveyStatus.Scheduled;
        }

        public void RescheduleSurvey(DateTime newStart, DateTime newEnd)
        {
            if (Status != SurveyStatus.Scheduled)
                throw new InvalidOperationException("Only scheduled surveys can be rescheduled.");

            if (newStart <= DateTime.Now)
                throw new InvalidOperationException("Rescheduling must be for a future date/time.");

            if (newEnd <= newStart.AddHours(1))
                throw new InvalidOperationException("End date must be at least one hour after start.");

            StartDate = newStart;
            EndDate = newEnd;
        }

        public void CloseSurvey()
        {
            if (Status != SurveyStatus.Active)
                throw new InvalidOperationException("Only active surveys can be closed.");

            Status = SurveyStatus.Completed;
            EndDate = DateTime.Now;
        }
    }

    public enum SurveyStatus
    {
        Concept,
        Scheduled,
        Active,
        Completed
    }
}

