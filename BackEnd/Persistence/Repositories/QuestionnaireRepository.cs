using BackEnd.Domain.IRepositories;
using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Persistence.Repositories
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private readonly AplicationDbContext _context;
        public QuestionnaireRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateQuestionnaire(Questionnaires questionnaires)
        {
             _context.Add(questionnaires);
             await _context.SaveChangesAsync();
        }

        public async Task<List<Questionnaires>> GetListQuestionnaireByUser(int userID)
        {
           var QuestionnarioreList = await _context.Questionnaires.Where(x=>x.Active == 1 && x.UserId == userID).ToListAsync();
            return QuestionnarioreList;
        }

        public async Task<List<Questionnaires>> GetListQuestionnaires()
        {
            var listQuestionnaire = await _context.Questionnaires.Where(x=> x.Active == 1)
                                    .Select(o=> new Questionnaires { 
                                        Id = o.Id,
                                        Name = o.Name,
                                        Description = o.Description,
                                        CreationDate = o.CreationDate,
                                        User = new Users { Username = o.User.Username }
                                    }).ToListAsync();

            return listQuestionnaire;
        }

        public async Task<Questionnaires> GetQuestionnairesByID(int QuestionnaireID)
        {
            var questionnaire = await _context.Questionnaires.Where(x=> x.Id == QuestionnaireID
                                    && x.Active == 1)
                                .Include(x=> x.questionList)
                                .ThenInclude(x=> x.answerList)
                                .FirstOrDefaultAsync();

            return questionnaire;
        }

        public async Task QuestionnaireDelete(Questionnaires questionnaires)
        {
            questionnaires.Active = 0;
            _context.Entry(questionnaires).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Questionnaires> QuestionnaireSearch(int questionnaireID, int userID)
        {
            var search = await _context.Questionnaires.Where(x=>x.Id == questionnaireID
                       && x.Active ==1 && x.UserId == userID).FirstOrDefaultAsync();
            return search;
        }
    }
}