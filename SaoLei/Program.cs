using System;
using System.Text;

namespace SaoLei
{
    class Program
    {
        static void Main(string[] args)
        {
            //必须设置这三行，否则乱码
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.OutputEncoding = Encoding.GetEncoding("GB2312");
            Console.InputEncoding = Encoding.GetEncoding("GB2312");

            Mine game = new Mine();
            game.Init();
            game.start();

            int row, col;
            string line;

            while(true)
            {
                game.show();

                //提示用户输入坐标
                Console.WriteLine("请输入您要点击的位置的坐标:[row, col]:");

                line = Console.ReadLine();
                string[] numbers = line.Split(new char[] {' '});
                if (numbers.Length != 2)
                {
                    Console.WriteLine("无效的输入，请重新输入！");
                    continue;
                }

                try
                {
                    row = Convert.ToInt32(numbers[0]);
                    col = Convert.ToInt32(numbers[1]);
                    game.click(row, col);
                }
                catch
                {
                    Console.WriteLine("请输入数字！");
                }
                
                //判断游戏是否结束，结束了则退出循环
                if(game.isOver())
                {
                    break;
                }
            }

            //游戏结束后，输出所有地雷的位置
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("* means mime!");
            game.show(true);
        }
    }
}