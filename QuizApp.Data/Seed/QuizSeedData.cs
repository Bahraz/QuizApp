using QuizApp.Data.Entities;

namespace QuizApp.Data.Seed
{
    public static class QuizSeedData
    {
        public static Quiz[] Quizzes =>
        [
            new Quiz { IdQuiz = 1, CategoryName = "C# – podstawy" },
            new Quiz { IdQuiz = 2, CategoryName = "Bazy danych" },
            new Quiz { IdQuiz = 3, CategoryName = "Podstawy IT" }
        ];

        public static Question[] Questions =>
        [
            new Question { IdQuestion = 1,  Contents = "Co oznacza skrót CLR?", IdQuiz = 1 },
            new Question { IdQuestion = 2,  Contents = "Jakim typem jest string?", IdQuiz = 1 },
            new Question { IdQuestion = 3,  Contents = "Do czego służy foreach?", IdQuiz = 1 },
            new Question { IdQuestion = 4,  Contents = "Co to jest namespace?", IdQuiz = 1 },
            new Question { IdQuestion = 5,  Contents = "Jakie słowo tworzy obiekt?", IdQuiz = 1 },

            new Question { IdQuestion = 6,  Contents = "Co oznacza skrót SQL?", IdQuiz = 2 },
            new Question { IdQuestion = 7,  Contents = "Do czego służy SELECT?", IdQuiz = 2 },
            new Question { IdQuestion = 8,  Contents = "Co to jest tabela?", IdQuiz = 2 },
            new Question { IdQuestion = 9,  Contents = "Co oznacza PRIMARY KEY?", IdQuiz = 2 },
            new Question { IdQuestion = 10, Contents = "Co oznacza NULL?", IdQuiz = 2 },

            new Question { IdQuestion = 11, Contents = "Co oznacza skrót CPU?", IdQuiz = 3 },
            new Question { IdQuestion = 12, Contents = "Do czego służy pamięć RAM?", IdQuiz = 3 },
            new Question { IdQuestion = 13, Contents = "Co to jest system operacyjny?", IdQuiz = 3 },
            new Question { IdQuestion = 14, Contents = "Do czego służy router?", IdQuiz = 3 },
            new Question { IdQuestion = 15, Contents = "Co to jest adres IP?", IdQuiz = 3 }
         ];

        public static Answer[] Answers =>
        [
            new Answer { IdAnswer = 1,  Text = "Common Language Runtime", IsTrue = true,  IdQuestion = 1 },
            new Answer { IdAnswer = 2,  Text = "Central Logic Register",  IsTrue = false, IdQuestion = 1 },
            new Answer { IdAnswer = 3,  Text = "Class Loader Runtime",   IsTrue = false, IdQuestion = 1 },
            new Answer { IdAnswer = 4,  Text = "Code Level Resource",    IsTrue = false, IdQuestion = 1 },

            new Answer { IdAnswer = 5,  Text = "Typem referencyjnym", IsTrue = true,  IdQuestion = 2 },
            new Answer { IdAnswer = 6,  Text = "Typem wartościowym", IsTrue = false, IdQuestion = 2 },
            new Answer { IdAnswer = 7,  Text = "Typem logicznym",    IsTrue = false, IdQuestion = 2 },
            new Answer { IdAnswer = 8,  Text = "Typem dynamicznym",  IsTrue = false, IdQuestion = 2 },

            new Answer { IdAnswer = 9,  Text = "Iteruje po kolekcji", IsTrue = true,  IdQuestion = 3 },
            new Answer { IdAnswer = 10, Text = "Tworzy klasę",       IsTrue = false, IdQuestion = 3 },
            new Answer { IdAnswer = 11, Text = "Kończy program",    IsTrue = false, IdQuestion = 3 },
            new Answer { IdAnswer = 12, Text = "Obsługuje wyjątki",  IsTrue = false, IdQuestion = 3 },

            new Answer { IdAnswer = 13, Text = "Przestrzeń nazw", IsTrue = true,  IdQuestion = 4 },
            new Answer { IdAnswer = 14, Text = "Typ zmiennej",   IsTrue = false, IdQuestion = 4 },
            new Answer { IdAnswer = 15, Text = "Biblioteka DLL", IsTrue = false, IdQuestion = 4 },
            new Answer { IdAnswer = 16, Text = "Metoda",         IsTrue = false, IdQuestion = 4 },

            new Answer { IdAnswer = 17, Text = "new",    IsTrue = true,  IdQuestion = 5 },
            new Answer { IdAnswer = 18, Text = "create", IsTrue = false, IdQuestion = 5 },
            new Answer { IdAnswer = 19, Text = "make",   IsTrue = false, IdQuestion = 5 },
            new Answer { IdAnswer = 20, Text = "build",  IsTrue = false, IdQuestion = 5 },

            new Answer { IdAnswer = 21, Text = "Structured Query Language", IsTrue = true,  IdQuestion = 6 },
            new Answer { IdAnswer = 22, Text = "Simple Query List",        IsTrue = false, IdQuestion = 6 },
            new Answer { IdAnswer = 23, Text = "System Query Logic",       IsTrue = false, IdQuestion = 6 },
            new Answer { IdAnswer = 24, Text = "Standard Question Lang.",  IsTrue = false, IdQuestion = 6 },

            new Answer { IdAnswer = 25, Text = "Pobierania danych", IsTrue = true,  IdQuestion = 7 },
            new Answer { IdAnswer = 26, Text = "Usuwania danych",  IsTrue = false, IdQuestion = 7 },
            new Answer { IdAnswer = 27, Text = "Tworzenia tabel",  IsTrue = false, IdQuestion = 7 },
            new Answer { IdAnswer = 28, Text = "Zmiany struktury", IsTrue = false, IdQuestion = 7 },

            new Answer { IdAnswer = 29, Text = "Zbiór danych w wierszach i kolumnach", IsTrue = true,  IdQuestion = 8 },
            new Answer { IdAnswer = 30, Text = "Plik tekstowy",                        IsTrue = false, IdQuestion = 8 },
            new Answer { IdAnswer = 31, Text = "Program",                             IsTrue = false, IdQuestion = 8 },
            new Answer { IdAnswer = 32, Text = "Serwer",                              IsTrue = false, IdQuestion = 8 },

            new Answer { IdAnswer = 33, Text = "Unikalny identyfikator", IsTrue = true,  IdQuestion = 9 },
            new Answer { IdAnswer = 34, Text = "Hasło użytkownika",     IsTrue = false, IdQuestion = 9 },
            new Answer { IdAnswer = 35, Text = "Relację tabel",         IsTrue = false, IdQuestion = 9 },
            new Answer { IdAnswer = 36, Text = "Indeks bazy",           IsTrue = false, IdQuestion = 9 },

            new Answer { IdAnswer = 37, Text = "Brak wartości", IsTrue = true,  IdQuestion = 10 },
            new Answer { IdAnswer = 38, Text = "Zero",          IsTrue = false, IdQuestion = 10 },
            new Answer { IdAnswer = 39, Text = "Pusty tekst",   IsTrue = false, IdQuestion = 10 },
            new Answer { IdAnswer = 40, Text = "Błąd danych",   IsTrue = false, IdQuestion = 10 },

            new Answer { IdAnswer = 41, Text = "Central Processing Unit", IsTrue = true,  IdQuestion = 11 },
            new Answer { IdAnswer = 42, Text = "Computer Personal Unit", IsTrue = false, IdQuestion = 11 },
            new Answer { IdAnswer = 43, Text = "Central Program Utility",IsTrue = false, IdQuestion = 11 },
            new Answer { IdAnswer = 44, Text = "Control Process User",   IsTrue = false, IdQuestion = 11 },

            new Answer { IdAnswer = 45, Text = "Pamięć operacyjna", IsTrue = true,  IdQuestion = 12 },
            new Answer { IdAnswer = 46, Text = "Dysk twardy",       IsTrue = false, IdQuestion = 12 },
            new Answer { IdAnswer = 47, Text = "Procesor",         IsTrue = false, IdQuestion = 12 },
            new Answer { IdAnswer = 48, Text = "Karta graficzna",  IsTrue = false, IdQuestion = 12 },

            new Answer { IdAnswer = 49, Text = "Zarządza sprzętem i programami", IsTrue = true,  IdQuestion = 13 },
            new Answer { IdAnswer = 50, Text = "Edytor tekstu",                 IsTrue = false, IdQuestion = 13 },
            new Answer { IdAnswer = 51, Text = "Gra komputerowa",               IsTrue = false, IdQuestion = 13 },
            new Answer { IdAnswer = 52, Text = "Przeglądarka",                  IsTrue = false, IdQuestion = 13 },

            new Answer { IdAnswer = 53, Text = "Łączy sieci komputerowe", IsTrue = true,  IdQuestion = 14 },
            new Answer { IdAnswer = 54, Text = "Chłodzi komputer",        IsTrue = false, IdQuestion = 14 },
            new Answer { IdAnswer = 55, Text = "Przechowuje dane",        IsTrue = false, IdQuestion = 14 },
            new Answer { IdAnswer = 56, Text = "Wyświetla obraz",         IsTrue = false, IdQuestion = 14 },

            new Answer { IdAnswer = 57, Text = "Adres urządzenia w sieci", IsTrue = true,  IdQuestion = 15 },
            new Answer { IdAnswer = 58, Text = "Hasło WiFi",               IsTrue = false, IdQuestion = 15 },
            new Answer { IdAnswer = 59, Text = "Typ kabla",                IsTrue = false, IdQuestion = 15 },
            new Answer { IdAnswer = 60, Text = "System operacyjny",        IsTrue = false, IdQuestion = 15 }
        ];
    }
}
