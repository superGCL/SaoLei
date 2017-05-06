using System;
using System.IO;

namespace SaoLei
{
    class Program
    {
        static void Main(string[] args)
        {
            Mine game = new Mine();
            game.Init();
            game.start();

            int row, col;
            string line;

            while(true)
            {
                game.show();

                //show some message
                Console.WriteLine("Please Input the location of row and col:[row, col]:");

                line = Console.ReadLine();
                string[] numbers = line.Split(new char[] {' '});
                if (numbers.Length != 2) continue;

                row = Convert.ToInt32(numbers[0]);
                col = Convert.ToInt32(numbers[1]);

                game.click(row, col);

                //判断游戏是否结束，结束了则退出循环
                if(game.status == 2)
                {
                    break;
                }
            }

            //游戏结束后，输出所有地雷的位置
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("-2 means mime!");
            game.show(true);
        }
    }
}