using Codex.SalarySurvey.Domain.Contracts;
using Codex.SalarySurvey.Domain.Contracts.Repositories;
using Codex.SalarySurvey.Domain.Contracts.Services;
using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.SalarySurvey.Domain
{
    public class SurveyQuestionService : ISurveyQuestionService
    {
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SurveyQuestionService(ISurveyQuestionRepository surveyQuestionRepository, IUnitOfWork unitOfWork)
        {
            _surveyQuestionRepository = surveyQuestionRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DetailedQuestion> GetSurveyQuestions(int surveyId, int? userId)
        {
            return _surveyQuestionRepository.GetSurveyQuestions(surveyId, userId);
        }
    }
}
