using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Data.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        // Klucz obcy
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
    }
}
