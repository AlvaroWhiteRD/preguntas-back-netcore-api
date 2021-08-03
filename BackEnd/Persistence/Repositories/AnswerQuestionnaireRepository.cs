
using BackEnd.Domain.IRepositories;
using BackEnd.Domain.Models;
using BackEnd.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Persistence.Repositories
{
    public class AnswerQuestionnaireRepository : IAnswerQuestionnaireRepository
    {
        private readonly AplicationDbContext _context;
        public AnswerQuestionnaireRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AnswerQuestionnaires>> AnswerQuestionnairesList(int questionnairesId, int userId)
        {
            var answerQuestionnairesList = await _context.AnswerQuestionnaires.Where(
                                    x => x.QuestionnairesId == questionnairesId && x.Active == 1
                                 && x.Questionnaires.UserId == userId).OrderByDescending(x => x.CreationDate).ToListAsync();

            return answerQuestionnairesList;
        }

        public async Task DeleteAnswerQuestionnaire(AnswerQuestionnaires answerQuestionnaires)
        {
            answerQuestionnaires.Active = 0;
            _context.Entry(answerQuestionnaires).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<AnswerQuestionnaireDetails>> GetAnswerList(int answerQuestionnaireId)
        {
            var answerList = await _context.AnswerQuestionnaireDetails.Where(x =>
                                     x.AnswerQuestionnairesId == answerQuestionnaireId)
                                    .Select(x => new AnswerQuestionnaireDetails
                                    {
                                        AnswersId = x.AnswersId
                                    }).ToListAsync();
            return answerList;
        }

        public async Task<int> GetQuestionnaireIdByAnswerId(int answerQuestionnaireId)
        {
            var questionnaire = await _context.AnswerQuestionnaires.Where(x =>
                                x.Id == answerQuestionnaireId && x.Active == 1).FirstOrDefaultAsync();


            return questionnaire.QuestionnairesId;
        }

        public async Task SavesAnswerQuestionnaire(AnswerQuestionnaires answerQuestionnaires)
        {
            answerQuestionnaires.Active = 1;
            answerQuestionnaires.CreationDate = DateTime.Now;
            _context.Add(answerQuestionnaires);
            await _context.SaveChangesAsync();
        }

        public async Task<AnswerQuestionnaires> SearchAnswerQuestionnaire(int questionnaireAnswerId, int userId)
        {
            var answerQuestionnaire = await _context.AnswerQuestionnaires.Where(
                x => x.Id == questionnaireAnswerId && x.Questionnaires.UserId == userId
                && x.Active == 1).FirstOrDefaultAsync();

            return answerQuestionnaire;
        }
    }
}
