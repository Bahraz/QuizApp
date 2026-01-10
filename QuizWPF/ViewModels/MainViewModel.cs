using QuizApp.Data.Entities;
using QuizApp.Data.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuizWPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly QuizRepository _quizRepository;

        public ObservableCollection<Quiz> Quizzes { get; } = new();
        public ObservableCollection<Question> Questions { get; } = new();
        public ObservableCollection<Answer> Answers { get; } = new();

        private Quiz? _selectedQuiz;
        public Quiz? SelectedQuiz
        {
            get => _selectedQuiz;
            set
            {
                _selectedQuiz = value;
                OnPropertyChanged();
                LoadQuestions();
            }
        }

        private Question? _selectedQuestion;
        public Question? SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;
                OnPropertyChanged();
                LoadAnswers();
            }
        }

        public MainViewModel(QuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
            LoadQuizzes();
        }

        private async void LoadQuizzes()
        {
            Quizzes.Clear();
            foreach (var quiz in _quizRepository.GetAll())
                Quizzes.Add(quiz);
        }

        private async void LoadQuestions()
        {
            Questions.Clear();
            Answers.Clear();

            if (SelectedQuiz == null)
                return;

            var quiz = await _quizRepository
                .GetQuizWithQuestionsAndAnswersAsync(SelectedQuiz.IdQuiz);

            foreach (var q in quiz!.Questions)
                Questions.Add(q);
        }

        private void LoadAnswers()
        {
            Answers.Clear();
            if (SelectedQuestion == null)
                return;

            foreach (var a in SelectedQuestion.Answers)
                Answers.Add(a);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
