    
    using BackEnd.Domain.IRepositories;
using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class AnswerQuestionnaireServices : IAnswerQuestionnaireServices
    {
        private readonly IAnswerQuestionnaireRepository _answerQuestionnaireRepository;
        public AnswerQuestionnaireServices(IAnswerQuestionnaireRepository answerQuestionnaireRepository)
        {
            _answerQuestionnaireRepository = answerQuestionnaireRepository;
        }

        public async Task<List<AnswerQuestionnaires>> AnswerQuestionnairesList(int questionnairesId, int userId)
        {
            return await _answerQuestionnaireRepository.AnswerQuestionnairesList(questionnairesId, userId);
        }

        public async Task DeleteAnswerQuestionnaire(AnswerQuestionnaires answerQuestionnaires)
        {
            await _answerQuestionnaireRepository.DeleteAnswerQuestionnaire(answerQuestionnaires);
        }

        public async Task<List<AnswerQuestionnaireDetails>> GetAnswerList(int answerQuestionnaireId)
        {
            return await _answerQuestionnaireRepository.GetAnswerList(answerQuestionnaireId);
        }

        public async Task<int> GetQuestionnaireIdByAnswerId(int answerQuestionnaireId)
        {
            return await _answerQuestionnaireRepository.GetQuestionnaireIdByAnswerId(answerQuestionnaireId);
        }

        public async Task SavesAnswerQuestionnaire(AnswerQuestionnaires answerQuestionnaires)
        {
            await _answerQuestionnaireRepository.SavesAnswerQuestionnaire(answerQuestionnaires);
        }

        public async Task<AnswerQuestionnaires> SearchAnswerQuestionnaire(int questionnaireAnswerId, int userId)
        {
            return await _answerQuestionnaireRepository.SearchAnswerQuestionnaire(questionnaireAnswerId, userId);
        }
    }
}
