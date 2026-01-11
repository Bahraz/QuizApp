using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    IdQuiz = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.IdQuiz);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    IdQuestion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contents = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdQuiz = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.IdQuestion);
                    table.ForeignKey(
                        name: "FK_Questions_Quizzes_IdQuiz",
                        column: x => x.IdQuiz,
                        principalTable: "Quizzes",
                        principalColumn: "IdQuiz",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    IdAnswer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdQuestion = table.Column<int>(type: "int", nullable: false),
                    IsTrue = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.IdAnswer);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_IdQuestion",
                        column: x => x.IdQuestion,
                        principalTable: "Questions",
                        principalColumn: "IdQuestion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "IdQuiz", "CategoryName" },
                values: new object[,]
                {
                    { 1, "C# – podstawy" },
                    { 2, "Bazy danych" },
                    { 3, "Podstawy IT" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "IdQuestion", "Contents", "IdQuiz" },
                values: new object[,]
                {
                    { 1, "Co oznacza skrót CLR?", 1 },
                    { 2, "Jakim typem jest string?", 1 },
                    { 3, "Do czego służy foreach?", 1 },
                    { 4, "Co to jest namespace?", 1 },
                    { 5, "Jakie słowo tworzy obiekt?", 1 },
                    { 6, "Co oznacza skrót SQL?", 2 },
                    { 7, "Do czego służy SELECT?", 2 },
                    { 8, "Co to jest tabela?", 2 },
                    { 9, "Co oznacza PRIMARY KEY?", 2 },
                    { 10, "Co oznacza NULL?", 2 },
                    { 11, "Co oznacza skrót CPU?", 3 },
                    { 12, "Do czego służy pamięć RAM?", 3 },
                    { 13, "Co to jest system operacyjny?", 3 },
                    { 14, "Do czego służy router?", 3 },
                    { 15, "Co to jest adres IP?", 3 }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "IdAnswer", "IdQuestion", "IsTrue", "Text" },
                values: new object[,]
                {
                    { 1, 1, true, "Common Language Runtime" },
                    { 2, 1, false, "Central Logic Register" },
                    { 3, 1, false, "Class Loader Runtime" },
                    { 4, 1, false, "Code Level Resource" },
                    { 5, 2, true, "Typem referencyjnym" },
                    { 6, 2, false, "Typem wartościowym" },
                    { 7, 2, false, "Typem logicznym" },
                    { 8, 2, false, "Typem dynamicznym" },
                    { 9, 3, true, "Iteruje po kolekcji" },
                    { 10, 3, false, "Tworzy klasę" },
                    { 11, 3, false, "Kończy program" },
                    { 12, 3, false, "Obsługuje wyjątki" },
                    { 13, 4, true, "Przestrzeń nazw" },
                    { 14, 4, false, "Typ zmiennej" },
                    { 15, 4, false, "Biblioteka DLL" },
                    { 16, 4, false, "Metoda" },
                    { 17, 5, true, "new" },
                    { 18, 5, false, "create" },
                    { 19, 5, false, "make" },
                    { 20, 5, false, "build" },
                    { 21, 6, true, "Structured Query Language" },
                    { 22, 6, false, "Simple Query List" },
                    { 23, 6, false, "System Query Logic" },
                    { 24, 6, false, "Standard Question Lang." },
                    { 25, 7, true, "Pobierania danych" },
                    { 26, 7, false, "Usuwania danych" },
                    { 27, 7, false, "Tworzenia tabel" },
                    { 28, 7, false, "Zmiany struktury" },
                    { 29, 8, true, "Zbiór danych w wierszach i kolumnach" },
                    { 30, 8, false, "Plik tekstowy" },
                    { 31, 8, false, "Program" },
                    { 32, 8, false, "Serwer" },
                    { 33, 9, true, "Unikalny identyfikator" },
                    { 34, 9, false, "Hasło użytkownika" },
                    { 35, 9, false, "Relację tabel" },
                    { 36, 9, false, "Indeks bazy" },
                    { 37, 10, true, "Brak wartości" },
                    { 38, 10, false, "Zero" },
                    { 39, 10, false, "Pusty tekst" },
                    { 40, 10, false, "Błąd danych" },
                    { 41, 11, true, "Central Processing Unit" },
                    { 42, 11, false, "Computer Personal Unit" },
                    { 43, 11, false, "Central Program Utility" },
                    { 44, 11, false, "Control Process User" },
                    { 45, 12, true, "Pamięć operacyjna" },
                    { 46, 12, false, "Dysk twardy" },
                    { 47, 12, false, "Procesor" },
                    { 48, 12, false, "Karta graficzna" },
                    { 49, 13, true, "Zarządza sprzętem i programami" },
                    { 50, 13, false, "Edytor tekstu" },
                    { 51, 13, false, "Gra komputerowa" },
                    { 52, 13, false, "Przeglądarka" },
                    { 53, 14, true, "Łączy sieci komputerowe" },
                    { 54, 14, false, "Chłodzi komputer" },
                    { 55, 14, false, "Przechowuje dane" },
                    { 56, 14, false, "Wyświetla obraz" },
                    { 57, 15, true, "Adres urządzenia w sieci" },
                    { 58, 15, false, "Hasło WiFi" },
                    { 59, 15, false, "Typ kabla" },
                    { 60, 15, false, "System operacyjny" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_IdQuestion",
                table: "Answers",
                column: "IdQuestion");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_IdQuiz",
                table: "Questions",
                column: "IdQuiz");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Quizzes");
        }
    }
}
