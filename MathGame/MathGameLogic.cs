namespace MathGame
{
    public class MathGameLogic
    {
        //Lista que guarda o histórico de resultados
        public List<string> GameHistory { get; set; } = new List<string>();

        //Menu de opções
        public void ShowMenu()
        {
            System.Console.WriteLine("Select an operation to play:");
            System.Console.WriteLine("1. Summation");
            System.Console.WriteLine("2. Subtraction");
            System.Console.WriteLine("3. Multiplication");
            System.Console.WriteLine("4. Division");
            System.Console.WriteLine("5. Random operation");
            System.Console.WriteLine("6. Show history");
            System.Console.WriteLine("7. Change dificulty");
            System.Console.WriteLine("8. Exit");
        }

        public int MathOperation(int firstNumber, int secondNumber, char operation)
        {
            int result;
            switch (operation)
            {
                case '+':
                    result = firstNumber + secondNumber;
                    break;
                case '-':
                    result = firstNumber - secondNumber;
                    break;
                case '*':
                    result = firstNumber * secondNumber;
                    break;
                case '/':
                    result = firstNumber / secondNumber;
                    break;
                default:
                    System.Console.WriteLine("Invalid operation.");
                    return 0;
            }
            GameHistory.Add($"{firstNumber} {operation} {secondNumber} = {result}");
            return result;
        }
    }
}