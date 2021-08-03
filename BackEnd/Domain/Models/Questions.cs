using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Domain.Models
{
    public class Questions
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }

        public List<Answers> answerList { get; set; }

        public Questionnaires Questionnaire { get; set; }

        public int QuestionnaireId { get; set; }



    }
}