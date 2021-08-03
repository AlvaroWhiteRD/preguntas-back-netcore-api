using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Domain.Models
{
    public class Questionnaires
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public int Active { get; set; }

        public int UserId { get; set; }

        public Users User { get; set; }

        public List<Questions> questionList { get; set; }

    }
}