using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Data.Models
{
    public class Score
    {
        [Key]
        public int Id { get; set; }

        public string PlayerName { get; set; }

        public int Points { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}
