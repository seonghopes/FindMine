using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_Mine
{
    class Cell
    {
        // 지뢰
        private bool is_mine = false;
        // 깃발
        private bool is_flag = false; // is_open이 false일때만 ture가능
        // cell
        private bool is_open = false;
        // 주변 지뢰갯수
        private int around_mines = 0;

        // 셀 주변의 다른 셀 객체 저장 (3 ~ 8)
        private List<Cell> around_cells = new List<Cell>();


        public Cell()
        {

        }

        public void PutMine()
        {
            is_mine = true;
        }

        public bool IsMine() { return is_mine; }

        public void Open() 
        {
            is_open = true;
            if (around_mines == 0)
            {
                foreach (Cell c in around_cells)
                {
                    if (c.IsOpen() || c.IsMine() || c.IsFlag())
                        continue;
                    c.Open();
                }
            }
        }
        public bool IsOpen() { return is_open; }

        public void ToggleFlag() { is_flag = !is_flag; }

        public bool IsFlag() { return is_flag; }

        public void AddAroundCell(Cell c)
        {
            around_cells.Add(c);
        }

        public void UpdateAroundMines()
        {
            int cnt = 0;
            foreach (Cell item in around_cells)
            {
                //cnt += item.is_mine ? 1 : 0;
                if (item.is_mine)
                    cnt++;
            }
            around_mines = cnt;  // 주변 지뢰 갯수 업데이트
        }


        public void Draw()
        {
            char c = '.';  // cell이 안열렸으면 .(점) 기본상태
            if (is_open)
            {
                if (is_mine)
                    c = '*';
                else if (around_mines > 0)
                    c = (char)('0' + around_mines); // 0 <= around_mines <= 8   '0'의 ASCII 값은 48  
                else                                //  char + int	내부적으로 ASCII값 + 정수 연산
                    c = '_';
            }
            else if (is_flag)
            {
                c = 'P'; // 깃발 모양 P
            }
            Console.Write(c);
        }
    }
}
