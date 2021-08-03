using BackEnd.Domain.IRepositories;
using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class QuestionnaireServices : IQuestionnaireService
    {
        private readonly IQuestionnaireRepository _questionnaireRepository;
        public QuestionnaireServices(IQuestionnaireRepository questionnaireRepository)
        {
            _questionnaireRepository = questionnaireRepository;
        }

        public async Task CreateQuestionnaire(Questionnaires questionnaires)
        {
            await _questionnaireRepository.CreateQuestionnaire(questionnaires);
        }

        public async Task<List<Questionnaires>> GetListQuestionnaireByUser(int userID)
        {
            return await _questionnaireRepository.GetListQuestionnaireByUser(userID);
             
        }

        public async Task<List<Questionnaires>> GetListQuestionnaires()
        {
            return await _questionnaireRepository.GetListQuestionnaires();
        }

        public async Task<Questionnaires> GetQuestionnairesByID(int QuestionnaireID)
        {
            return await _questionnaireRepository.GetQuestionnairesByID(QuestionnaireID);
        }

        public async Task QuestionnaireDelete(Questionnaires questionnaires)
        {
            await _questionnaireRepository.QuestionnaireDelete(questionnaires);
        }

        public async Task<Questionnaires> QuestionnaireSearch(int questionnaireID, int userID)
        {
            return await _questionnaireRepository.QuestionnaireSearch(questionnaireID, userID);
        }
    }
}
