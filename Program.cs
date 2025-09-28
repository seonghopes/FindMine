namespace Find_Mine
{
    internal class Program
    {
        static bool InRange(char v, char start, char end)
        {
            //return (v >= start && v <= end);
            if (v < start)
                return false;
            if (v > end)
                return false;

            return true;

        }
        static void Main(string[] args)
        {
            Board b = new Board();
            //b.ToggleFlag(1,2);
            //b.Open(3, 1);
            //b.Draw();
            // b.OpenAll();
            b.NewGame();
            //b.Draw();

            // 반복문으로 사용자에게 입력값 받기 및 예외처리
            while (true)
            {
                string input = Console.ReadLine(); // x(0~9), y(0~9), Open여부(0,1)   971  970 
                if (input.Length != 3)
                {
                    Console.WriteLine("다시 입력해주세요");
                    continue;
                }

                char c = input[0];
                char r = input[1];
                char cmd = input[2];

                bool ok = InRange(c, '0', '9') && InRange(r, '0', '9') && InRange(cmd, '0', '1');
                if (!ok)
                {
                    Console.WriteLine("다시 입력해주세요");
                    continue;
                }

                // 이제부터 진짜 !!!
                int cc = c - '0';
                int rr = r - '0';
                if (cmd == '0')
                {
                    bool game_over = !b.Open(cc, rr);
                    if (game_over)
                    {
                        b.OpenAll();
                        b.Draw();
                        Console.WriteLine("GAME OVER!");
                        break;
                    }
                    else
                    {
                        b.Draw();
                    }

                }
                else
                {
                    b.ToggleFlag(cc, rr);
                    b.Draw();
                }
            }
        }
    }
}
