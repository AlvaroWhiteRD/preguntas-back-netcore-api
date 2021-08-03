

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Domain.Models
{
    public class AnswerQuestionnaireDetails
    {
        public int Id { get; set; }
        public int AnswerQuestionnairesId { get; set; }
        public AnswerQuestionnaires AnswerQuestionnaire { get; set; }
        public int AnswersId { get; set; }
        public Answers Answers { get; set; }
    }
}