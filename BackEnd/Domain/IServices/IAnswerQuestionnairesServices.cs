using BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Domain.IServices
{
    public interface IAnswerQuestionnaireServices
    {
        Task SavesAnswerQuestionnaire(AnswerQuestionnaires answerQuestionnaires);

        Task<List<AnswerQuestionnaires>> AnswerQuestionnairesList(int questionnairesId, int userId);

        Task<AnswerQuestionnaires> SearchAnswerQuestionnaire(int questionnaireAnswerId, int userId);
        Task DeleteAnswerQuestionnaire(AnswerQuestionnaires answerQuestionnaires);

        Task<int> GetQuestionnaireIdByAnswerId(int answerQuestionnaireId);

        Task<List<AnswerQuestionnaireDetails>> GetAnswerList(int answerQuestionnaireId);
    }
}
