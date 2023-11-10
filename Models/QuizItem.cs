using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SortSmart.Models.DeweyDecimalModel;

namespace SortSmart.Models
{
    public class QuizItem
    {
        public string QuestionText { get; set; }
        public IList<Answer> Answers { get; set; }
        public int CorrectAnswerIndex { get; set; }

        // Reference to the related Dewey Decimal classification
        public ClassificationNode DeweyDecimal { get; set; }
    }
    public class Answer
    {
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }

        // Optionally reference Dewey Decimal Classification here if each answer is directly related to a different classification
        public ClassificationNode RelatedDeweyDecimal { get; set; }
    }
}
