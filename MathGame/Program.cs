using System.Diagnostics;
using MathGame;

MathGameLogic mathGame = new();
Random random = new();

int firstNumber;
int secondNumber;
int userMenuSelection;
int score = 0;
bool gameOver = false;


DifficultyLevel difficultyLevel = DifficultyLevel.Easy;

while (!gameOver)
{
    userMenuSelection = GetUserMenuSelection(mathGame);

    firstNumber = random.Next(1, 101);
    secondNumber = random.Next(1, 101);

    switch (userMenuSelection)
    {
        case 1:
            score = await PerformOperation(mathGame, firstNumber, secondNumber, score, '+', difficultyLevel);
            break;
        case 2:
            score = await PerformOperation(mathGame, firstNumber, secondNumber, score, '-', difficultyLevel);
            break;
        case 3:
            score = await PerformOperation(mathGame, firstNumber, secondNumber, score, '*', difficultyLevel);
            break;
        case 4:
            while (firstNumber % secondNumber != 0)
            {
                firstNumber = random.Next(1, 101);
                secondNumber = random.Next(1, 101);
            }
            score = await PerformOperation(mathGame, firstNumber, secondNumber, score, '/', difficultyLevel);
            break;
        case 5:
            int numberOfQuestions = 99;
            System.Console.WriteLine("Please enter the number of questions you want to attempt.");
            while (!int.TryParse(Console.ReadLine(), out numberOfQuestions))
            {
                System.Console.WriteLine("Please enter the number of questions you want to attempt.");
            }
            while (numberOfQuestions > 0)
            {
                int randomOperation = random.Next(1, 5);

                switch (randomOperation)
                {
                    case 1:
                        firstNumber = random.Next(1, 101);
                        secondNumber = random.Next(1, 101);
                        score = await PerformOperation(mathGame, firstNumber, secondNumber, score, '+', difficultyLevel);
                        break;
                    case 2:
                        firstNumber = random.Next(1, 101);
                        secondNumber = random.Next(1, 101);
                        score = await PerformOperation(mathGame, firstNumber, secondNumber, score, '-', difficultyLevel);
                        break;
                    case 3:
                        firstNumber = random.Next(1, 101);
                        secondNumber = random.Next(1, 101);
                        score = await PerformOperation(mathGame, firstNumber, secondNumber, score, '*', difficultyLevel);
                        break;
                    case 4:
                        while (firstNumber % secondNumber != 0)
                        {
                            firstNumber = random.Next(1, 101);
                            secondNumber = random.Next(1, 101);
                        }    
                        score = await PerformOperation(mathGame, firstNumber, secondNumber, score, '/', difficultyLevel);
                        break;
                }
                numberOfQuestions--;
            }
            break;
        case 6:
            System.Console.WriteLine("GAME HISTORY: \n");
            foreach (var operation in mathGame.GameHistory)
            {
                System.Console.WriteLine($"{operation}");
            }
            break;
        case 7:
            difficultyLevel = ChangeDifficulty();
            DifficultyLevel difficultyEnum = difficultyLevel;
            Enum.IsDefined(typeof(DifficultyLevel), difficultyEnum);
            System.Console.WriteLine($"Your nem difficult level: {difficultyLevel}");
            break;
        case 8:
            gameOver = true;
            System.Console.WriteLine($"Your final score is: {score}");
            break;
    }
}

static DifficultyLevel ChangeDifficulty()
{
    int userSelection = 0;
    System.Console.WriteLine("please enter a difficult level");
    System.Console.WriteLine("1. Easy");
    System.Console.WriteLine("2. Medium");
    System.Console.WriteLine("3. Hard");

    while (!int.TryParse(Console.ReadLine(), out userSelection) || userSelection < 1 || userSelection > 3)
    {
        System.Console.WriteLine("Please enter a valid option 1-3");
    }
    return userSelection switch
    {
        1 => DifficultyLevel.Easy,
        2 => DifficultyLevel.Medium,
        3 => DifficultyLevel.Hard,
        _ => DifficultyLevel.Easy,
    };
}

static void DisplayMathGameQuestion(int firstNumber, int secondNumber, char operation)
{
    System.Console.WriteLine($"{firstNumber} {operation} {secondNumber} = ??");
}

static int GetUserMenuSelection(MathGameLogic mathGame)
{
    int selection = -1;
    mathGame.ShowMenu();
    while (selection < 1 || selection > 8)
    {
        while (!int.TryParse(Console.ReadLine(), out selection))
        {
            System.Console.WriteLine("Enter a valid option 1-8");
        }
        if (selection < 1 || selection > 8)
        {
            System.Console.WriteLine("Enter a valid option 1-8");
        }
    }
    return selection;
}

static async Task<int?> GetUserResponse(DifficultyLevel difficulty)
{
    int timeout = (int)difficulty * 1000;
    using var cts = new CancellationTokenSource(timeout);

    Stopwatch stopwatch = new();
    stopwatch.Start();

    var inputTask = Task.Run(Console.ReadLine);
    var delayTask = Task.Delay(timeout);

    var completedTask = await Task.WhenAny(inputTask, delayTask);

    if (completedTask == inputTask)
    {
        string? result = await inputTask;
        if (result != null && int.TryParse(result, out int response))
        {
            Console.WriteLine(string.Format("Time taken to answer: {0:m\\:ss\\.fff}", stopwatch.Elapsed));
            return response;
        }
    }
    Console.WriteLine("Time is up!");
    return null;
}

static int ValidateResult(int result, int? userResponse, int score)
{
    if (result == userResponse)
    {
        System.Console.WriteLine("Your answer is correctly. You earned 5 ppoints");
        score += 5;
    }
    else
    {
        System.Console.WriteLine("Try again");
        System.Console.WriteLine($"Correct answer is: {result}");
    }
    return score;
}

static async Task<int> PerformOperation(MathGameLogic mathGame, int firstNumber, int secondNumber, int score, char operation, DifficultyLevel difficulty)
{
    int result;
    int? userResponse;
    DisplayMathGameQuestion(firstNumber, secondNumber, operation);
    result = mathGame.MathOperation(firstNumber, secondNumber, operation);
    userResponse = await GetUserResponse(difficulty);
    score = ValidateResult(result, userResponse, score);
    return score;
}
public enum DifficultyLevel
{
    Easy = 45,
    Medium = 30,
    Hard = 15
}