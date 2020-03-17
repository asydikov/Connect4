using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Connect4
{
    static class Connect4
    {
        private const char Coin = 'x';
        private const char Empty = '-';
        private const int RowsCount = 6;
        private const int ColumnsCount = 7;
        private static char[][] TwoDimentionalBoard;
        private static List<int> WinColumns;

        public static List<int> Start(string board)
        {
            TwoDimentionalBoard = new char[RowsCount][];

            WinColumns = new List<int>();

            ParseBoardFromString(board);

            CheckWinColumns();

            return WinColumns;
        }

        private static void ParseBoardFromString(string board)
        {
            board = Regex.Replace(board, @"\s+", string.Empty);

            var boardRows = Enumerable.Range(0, board.Length / ColumnsCount)
                             .Select(i => board.Substring(i * ColumnsCount, ColumnsCount)).ToArray();

            for (int rowIndex = 0; rowIndex < boardRows.Count(); rowIndex++)
            {
                TwoDimentionalBoard[rowIndex] = boardRows[rowIndex].ToCharArray();
            }
        }

        private static void CheckWinColumns()
        {
            var possibleNewCoinLocations = TwoDimentionalBoard.Select((row, rowIndex) => row.Select((column, colIndex) =>
            {
                if (IsColumnEnd(TwoDimentionalBoard, rowIndex, colIndex))
                {
                    return new { rowId = rowIndex, colId = colIndex };
                }
                else
                {
                    return null;
                }
            })).SelectMany(x => x.Where(y => y != null));

            foreach (var point in possibleNewCoinLocations)
            {
                CheckDiagonal(point.rowId, point.colId);
                CheckHorizontal(point.rowId, point.colId);
                CheckVertical(point.rowId, point.colId);
            
            }

            WinColumns = WinColumns.Select(column => column + 1).ToList();
            WinColumns.Sort();
        }

        private static bool IsColumnEnd(char[][] board, int rowIndex, int colIndex)
        {
            if (rowIndex < 5 && board.ElementAt(rowIndex).ElementAt(colIndex) == Empty && board[rowIndex + 1][colIndex] != Empty)
            {
                return true;
            }
            else if (rowIndex == 5 && board[rowIndex][colIndex] == Empty)
            {
                return true;
            }
            return false;
        }

        private static void CheckDiagonal(int coinRowIndex, int coinColIndex)
        {
            var firstDiagonalCoins = new List<int>() { coinColIndex };
            var secondDiagonalCoins = new List<int>() { coinColIndex };

            for (int row = 1; row < 4; row++)
            {
                // first diagonal
                if (IsRowIndexInArrayRange(coinRowIndex - row) && IsColIndexInArrayRange(coinColIndex - row))
                {
                    if (TwoDimentionalBoard[coinRowIndex - row][coinColIndex - row] == Coin)
                    {
                        AccumulateCoins(firstDiagonalCoins, coinColIndex - row);
                    }
                }
                if (IsRowIndexInArrayRange(coinRowIndex + row) && IsColIndexInArrayRange(coinColIndex + row))
                {
                    if (TwoDimentionalBoard[coinRowIndex + row][coinColIndex + row] == Coin)
                    {
                        AccumulateCoins(firstDiagonalCoins, coinColIndex + row);
                    }
                }
                // second diagonal 
                if (IsRowIndexInArrayRange(coinRowIndex - row) && IsColIndexInArrayRange(coinColIndex + row))
                {
                    if (TwoDimentionalBoard[coinRowIndex - row][coinColIndex + row] == Coin)
                    {
                        AccumulateCoins(secondDiagonalCoins, coinColIndex + row);
                    }
                }
                if (IsRowIndexInArrayRange(coinRowIndex + row) && IsColIndexInArrayRange(coinColIndex - row))
                {
                    if (TwoDimentionalBoard[coinRowIndex + row][coinColIndex - row] == Coin)
                    {
                        AccumulateCoins(secondDiagonalCoins, coinColIndex - row);
                    }
                }
                if (firstDiagonalCoins.Count() > 3 || secondDiagonalCoins.Count() > 3)
                {
                    WinColumns.Add(coinColIndex);
                    return;
                }
            }
        }

        private static void CheckHorizontal(int coinRowIndex, int coinColIndex)
        {
            var neighborCoins = new List<int>() { coinColIndex };
            for (int row = 1; row < 4; row++)
            {
                if (IsColIndexInArrayRange(coinColIndex - row) && TwoDimentionalBoard[coinRowIndex][coinColIndex - row] == Coin)
                {
                    AccumulateCoins(neighborCoins, coinColIndex - row);
                }
                if (IsColIndexInArrayRange(coinColIndex + row) && TwoDimentionalBoard[coinRowIndex][coinColIndex + row] == Coin)
                {
                    AccumulateCoins(neighborCoins, coinColIndex + row);
                }
            }

            if (neighborCoins.Count() > 3 || neighborCoins.Count() > 3)
            {
                WinColumns.Add(coinColIndex);
                return;
            }
        }

        private static void CheckVertical(int coinRowIndex, int coinColIndex)
        {
            var neighborCoins = new List<int>() { coinRowIndex };
            for (int row = 1; row < 4; row++)
            {
                if (IsRowIndexInArrayRange(coinRowIndex - row) && TwoDimentionalBoard[coinRowIndex - row][coinColIndex] == Coin)
                {
                    AccumulateCoins(neighborCoins, coinRowIndex - row);
                }
                if (IsRowIndexInArrayRange(coinRowIndex + row) && TwoDimentionalBoard[coinRowIndex + row][coinColIndex] == Coin)
                {
                    AccumulateCoins(neighborCoins, coinRowIndex + row);
                }
            }

            if (neighborCoins.Count() > 3 || neighborCoins.Count() > 3)
            {
                WinColumns.Add(coinColIndex);
                return;
            }
        }

        private static void AccumulateCoins(List<int> coinsAmount, int coinIndexToAdd)
        {
            coinsAmount.Sort();

            if (coinsAmount.First() - coinIndexToAdd == 1 || coinIndexToAdd - coinsAmount.Last() == 1)
            {
                coinsAmount.Add(coinIndexToAdd);
            }
        }

        static bool IsRowIndexInArrayRange(int index) => index < RowsCount && index > -1;
        static bool IsColIndexInArrayRange(int index) => index < ColumnsCount && index > -1;

        private struct CoinPoint
        {
            int Row { get; }
            int Column { get; }
            public CoinPoint(int row, int column)
            {
                Row = row;
                Column = column;
            }
        }

    }
}
