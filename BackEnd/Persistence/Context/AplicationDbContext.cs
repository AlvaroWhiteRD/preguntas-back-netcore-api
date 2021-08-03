using BackEnd.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Persistence.Context
{
    public class AplicationDbContext: DbContext
    {
        public DbSet<Users> Users { get; set; }

        public DbSet<Answers> Answers { get; set; }

        public DbSet<Questionnaires> Questionnaires { get; set; }

        public DbSet<Questions> Questions { get; set; }
        public DbSet<AnswerQuestionnaireDetails> AnswerQuestionnaireDetails { get; set; }
        public DbSet<AnswerQuestionnaires> AnswerQuestionnaires { get; set; }

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options): base(options)
        {

        }
    }
}
