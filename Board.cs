using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Find_Mine
{
    class Board
    {
        public const int N_COLS = 10;
        public const int N_ROWS = 10;
        // c# 2차원 배열

        private Cell[,] board = new Cell[N_ROWS, N_COLS];

        public Board()
        {
            Reset();
        }

        public void Draw()
        {
            for (int r = 0; r < N_ROWS; r++)
            {
                for (int c = 0; c < N_COLS; c++)
                {
                    //Cell cell = new Cell();
                    //board[r, c] = cell;
                    board[r, c].Draw();
                }
                Console.WriteLine();  // 행이 바뀌면 줄바꿈
            }
        }

        // Board 에서 해당위치 셀 열기 
        // 깃발이 있으면 리턴, 지뢰면 game over 처리용 bool 값 반환
        public bool Open(int c, int r)   // false : game over
        {
            Cell cell = board[r, c];

            if (cell.IsFlag())        // 플레그(깃발이)가 있으면 열지않음
                return true;

            cell.Open();

            return !(cell.IsMine());
        }

        public void ToggleFlag(int c, int r)  // x,y 좌표처럼
        {
            board[r, c].ToggleFlag();
        }

        // 지뢰 심기 (Random 사용)
        public void PutMines(int n_mines = 15)
        {
            Random rnd = new Random();

            for (int i = 0; i < n_mines; i++)
            {
                int r = rnd.Next(N_ROWS);
                int c = rnd.Next(N_COLS);

                if (board[r, c].IsMine())   // 같은곳에 여러번 지뢰가 배치되는경우
                {
                    i--;    // 이번 반복문 카운트 취소
                    continue;
                }

                board[r, c].PutMine();
            }

            for (int r = 0; r < N_ROWS; r++)
            {
                for (int c = 0; c < N_COLS; c++)
                {
                    board[r, c].UpdateAroundMines();
                }
            }
        }

        public void Reset()
        {
            // 10 X 10 cell 채우기
            for (int r = 0; r < N_ROWS; r++)
            {
                for (int c = 0; c < N_COLS; c++)
                {
                    //Cell cell = new Cell();
                    //board[r, c] = cell;
                    board[r, c] = new Cell();
                }
            }

            for (int r = 0; r < N_ROWS; r++)
            {
                for (int c = 0; c < N_COLS; c++)
                {
                    for (int i = Math.Max(0, r - 1); i <= Math.Min(N_ROWS - 1, r + 1); i++)             // 중요..!!
                    {
                        for (int j = Math.Max(0, c - 1); j <= Math.Min(N_COLS - 1, c + 1); j++)
                        {
                            if (i == r && j == c) // 셀 자신은 제외
                                continue;

                            board[r, c].AddAroundCell(board[i, j]);
                        }
                    }
                }
            }
        }

        // 전체 셀 열기
        public void OpenAll()
        {
            for (int r = 0; r < N_ROWS; r++)
            {
                for (int c = 0; c < N_COLS; c++)
                {

                    board[r, c].Open();
                }
            }
        }

        public void NewGame()
        {
            Reset();
            PutMines();
            //Draw();
        }


    }
}
