using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Domain.Models
{
    public class Answers
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }
        [Required]
        public bool CorrectAnswer { get; set; }
      
        public int QuestionId { get; set; }

        public Questions Question { get; set; }


    }
}