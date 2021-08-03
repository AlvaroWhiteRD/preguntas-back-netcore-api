
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Domain.Models
{
    public class AnswerQuestionnaires
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string ParticipantName { get; set; }

        public DateTime CreationDate { get; set; }

        public int Active { get; set; }

        public int QuestionnairesId { get; set; }

        public Questionnaires Questionnaires { get; set; }

        public List<AnswerQuestionnaireDetails> AnswerQuestionnaireDetailList { get; set; }


    }
}