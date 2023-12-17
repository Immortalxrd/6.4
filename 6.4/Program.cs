using System;
using System.Linq;

class Program
{
    static char[][] board = { new char[] { '1', '2', '3' }, new char[] { '4', '5', '6' }, new char[] { '7', '8', '9' } };
    static char currentPlayer = 'X';

    static void Main()
    {
        while (!IsGameFinished())
        {
            Console.Clear();
            DrawBoard();

            int choice;
            bool validInput = false;

            while (!validInput)
            {
                Console.Write($"Player {currentPlayer}, enter the cell number (1-9): ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 9 && !IsCellOccupied(choice))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            }

            UpdateBoard(choice);
            SwitchPlayer();
        }

        Console.Clear();
        DrawBoard();

        if (IsWinner())
        {
            Console.WriteLine($"Player {currentPlayer} wins!");
        }
        else
        {
            Console.WriteLine("It's a tie!");
        }
    }

    static void DrawBoard()
    {
        foreach (var row in board)
        {
            Console.WriteLine(string.Join(" ", row));
        }
    }

    static void UpdateBoard(int choice)
    {
        char symbol = (currentPlayer == 'X') ? 'X' : 'O';
        int row = (choice - 1) / 3;
        int col = (choice - 1) % 3;

        board[row][col] = symbol;
    }

    static void SwitchPlayer()
    {
        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
    }

    static bool IsCellOccupied(int choice)
    {
        int row = (choice - 1) / 3;
        int col = (choice - 1) % 3;

        return board[row][col] == 'X' || board[row][col] == 'O';
    }

    static bool IsGameFinished()
    {
        return IsBoardFull() || IsWinner();
    }

    static bool IsBoardFull()
    {
        return board.All(row => row.All(cell => cell == 'X' || cell == 'O'));
    }

    static bool IsWinner()
    {
        return CheckRows() || CheckColumns() || CheckDiagonals();
    }

    static bool CheckRows()
    {
        return board.Any(row => row[0] == row[1] && row[1] == row[2]);
    }

    static bool CheckColumns()
    {
        return Enumerable.Range(0, 3).Any(i => board[0][i] == board[1][i] && board[1][i] == board[2][i]);
    }

    static bool CheckDiagonals()
    {
        return (board[0][0] == board[1][1] && board[1][1] == board[2][2]) || (board[0][2] == board[1][1] && board[1][1] == board[2][0]);
    }
}
