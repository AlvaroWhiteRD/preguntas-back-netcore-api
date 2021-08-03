using BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Domain.IServices
{
    public interface IQuestionnaireService
    {
        Task CreateQuestionnaire(Questionnaires questionnaires);
        Task<List<Questionnaires>> GetListQuestionnaireByUser(int userID);
        Task<Questionnaires> GetQuestionnairesByID(int QuestionnaireID);

        Task<Questionnaires> QuestionnaireSearch(int questionnaireID, int userID);

        Task QuestionnaireDelete(Questionnaires questionnaires);

        Task<List<Questionnaires>> GetListQuestionnaires();

    }
}
