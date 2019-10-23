using Codex.SalarySurvey.Domain.Contracts;
using Codex.SalarySurvey.Domain.Contracts.Repositories;
using Codex.SalarySurvey.Domain.Contracts.Services;
using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codex.SalarySurvey.Domain
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly ISurveyQuestionRepository _surveyQuestionRepository;
        private readonly IQuestionAnswerRepository _questionAnswerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SurveyService(ISurveyRepository surveyRepository,
            ISurveyQuestionRepository surveyQuestionRepository,
            IQuestionAnswerRepository questionAnswerRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _surveyRepository = surveyRepository;
            _surveyQuestionRepository = surveyQuestionRepository;
            _questionAnswerRepository = questionAnswerRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public DetailedSurvey GetDefaultSurvey()
        {
            return _surveyRepository.GetDefaultSurvey();
        }

        public QuestionsSurvey StartSurvey(int userId, int surveyId, JoiningPlatform platform)
        {
            // Get the user survey record if exists.
            var userSurvey = _surveyRepository.GetUserSurvey(userId, surveyId);
            if (userSurvey == null)
            {
                userSurvey = new UserSurvey
                {
                    UserId = userId,
                    SurveyId = surveyId,
                    StartedOn = DateTime.UtcNow,
                    PageCompleted = 0,
                    JoiningPlatform = (int)platform
                };

                _surveyRepository.AddUserSurvey(userSurvey);
                _unitOfWork.Commit();
            }

            var data = new QuestionsSurvey();
            data.PageNum = userSurvey.PageCompleted + 1;
            data.Questions = _surveyQuestionRepository.GetSurveyQuestions(surveyId, userId);

            return data;
        }

        public void SaveSurveyAnswers(int userId, DetailedSurvey survey, List<QuestionAnswer> answers, int pageNum)
        {
            List<QuestionAnswer> answersToUpdate = null;
            List<QuestionAnswer> answersToCreate = null;

            // Get the user survey record to save the page state.
            var userSurvey = _surveyRepository.GetUserSurvey(userId, survey.Id);

            // Check if the survey is already filled by the user.
            if (userSurvey.CompletedOn.HasValue)
                throw new InvalidOperationException("Survey is already completed!");

            // Fill the user id in the answer objects.
            answers.ForEach(a => a.UserId = userId);

            // Get the saved answers.
            var savedAnswers = _questionAnswerRepository.GetSurveyAnswers(userId, survey.Id);

            // Get the answers that already have records in the database to update them.
            // Otherwise, create new answer records.
            if (savedAnswers.Any())
            {
                IEnumerable<int> intersect = savedAnswers
                  .Select(a => a.SurveyQuestionId).Intersect(answers.Select(a => a.SurveyQuestionId));

                // Records to update.
                answersToUpdate = savedAnswers.Where(a => intersect.Contains(a.SurveyQuestionId)).ToList();

                // Records to create.
                answersToCreate = answers.Where(a => !intersect.Contains(a.SurveyQuestionId)).ToList();
            }
            else
            {
                answersToCreate = answers;
            }

            // Save the answers of the given survey page.
            // Update the existing records.
            QuestionAnswer tmp = null;

            if (answersToUpdate != null && answersToUpdate.Any())
            {
                foreach (var ans in answersToUpdate)
                {
                    tmp = answers.FirstOrDefault(a => a.SurveyQuestionId == ans.SurveyQuestionId);
                    ans.Answer = tmp?.Answer;
                    ans.EmployerId = tmp?.EmployerId;

                    _questionAnswerRepository.Update(ans);
                }
            }

            // Create new records.
            if (answersToCreate != null && answersToCreate.Any())
            {
                answersToCreate.ForEach(a => a.CreatedOn = DateTime.UtcNow);
                _questionAnswerRepository.AddList(answersToCreate);
            }

            // Save the completed page number.
            userSurvey.PageCompleted = pageNum;

            // Check if it is the last page, then close the survey as completed for this user.
            if (pageNum == survey.NumOfPages)
            {
                // Change status to completed by filling "CompletedOn" field.
                userSurvey.CompletedOn = DateTime.UtcNow;
            }

            // Commit the changes to the database.
            _unitOfWork.Commit();
        }

        public void ResetSurveyAnswers(string phone, int surveyId)
        {
            User user = _userRepository.GetByPhone(phone);
            var userSurvey = _surveyRepository.GetUserSurvey(user.Id, surveyId);

            userSurvey.PageCompleted = 0;
            userSurvey.CompletedOn = null;

            var savedAnswers = _questionAnswerRepository.GetSurveyAnswers(user.Id, surveyId);
            if (savedAnswers.Any())
            {
                foreach (var ans in savedAnswers)
                {
                    _questionAnswerRepository.Delete(ans);
                }
            }

            _unitOfWork.Commit();
        }
    }
}
