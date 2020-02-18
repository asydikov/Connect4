using System;
using System.Collections.Generic;
using System.IO;

namespace Connect4
{
    class Program
    {
        static void Main(string[] args)
        {
            string[][] arr = new string[6][];

            ParseBoardFromFile(arr);

            RefreshBoardDisplay(arr);

            Console.Write("Win Columns: ");
            List<int> possibleWinColumns = CheckWinColumns(arr);
            possibleWinColumns.ForEach(num => Console.Write($"{num} "));
            Console.WriteLine();

            Console.WriteLine("Please enter column number to put a coin:");
            int coinColIndex = Convert.ToInt32(Console.ReadLine());
            PutCoinOnBoard(coinColIndex, arr);

            if (possibleWinColumns.IndexOf(coinColIndex) != -1)
            {
                Console.WriteLine("You won!!");
            }
            else
            {
                Console.WriteLine("You lose!!");
            }

            Console.ReadKey();
        }

        static void ParseBoardFromFile(string[][] arr)
        {
            int row = 0;
            IEnumerable<string> lines = File.ReadLines(Path.Combine(Environment.CurrentDirectory, "input.txt"));
            foreach (string line in lines)
            {
                arr[row] = new string[7];
                for (int col = 0; col < line.Length; col++)
                {
                    arr[row][col] = line[col].ToString();
                }
                row++;
            }
        }

        static void RefreshBoardDisplay(string[][] arr)
        {
            foreach (string[] rows in arr)
            {
                foreach (string column in rows)
                {
                    Console.Write(column);
                }
                Console.WriteLine();
            }
        }

        static void PutCoinOnBoard(int colIndex, string[][] arr)
        {
            for (int row = 0; row < 6; row++)
            {
                if (arr[row][colIndex] != "-")
                {
                    arr[row - 1][colIndex] = "x";
                    // You told me this is not good idea to put a break,
                    // but what is the best way to avoid of 
                    // the loop proceeding once we have found a row index?
                    break;
                }
            }
            RefreshBoardDisplay(arr);
        }

        static List<int> CheckWinColumns(string[][] arr)
        {
            List<int> winCols = new List<int>();
            int southNeghbours, westNeghbours, eastNeghbours = 0;

            for (int column = 0; column < 7; column++)
            {
                southNeghbours = 0;
                westNeghbours = 0;
                eastNeghbours = 0;

                // The row index where the new coin will be put
                int rowIndex = 0;

                for (int row = 0; row < 6; row++)
                {
                    if (arr[row][column] != "-")
                    {
                        rowIndex = row - 1;
                        break;
                    }
                }

                // looking for south neighbours
                for (int i = rowIndex + 1; i < 6; i++)
                {
                    if (IsRowIndexInArrayRange(i) && arr[i][column] == "x")
                        southNeghbours += 1;
                }
                if (southNeghbours == 3)
                {
                    winCols.Add(column);
                    continue;
                }

                // looking for west and east neighbours
                for (int i = 1; i < 4; i++)
                {
                    if (IsColIndexInArrayRange(column + i) && arr[rowIndex][column + i] == "x")
                        eastNeghbours += 1;

                    if (IsColIndexInArrayRange(column - i) && arr[rowIndex][column - i] == "x")
                        westNeghbours += 1;
                }

                if (eastNeghbours + westNeghbours > 2)
                {
                    winCols.Add(column);
                    continue;
                }
            }

            return winCols;
        }

        static bool IsRowIndexInArrayRange(int index) => index < 6 && index > -1;
        static bool IsColIndexInArrayRange(int index) => index < 7 && index > -1;
    }
}
