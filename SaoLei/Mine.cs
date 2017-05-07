using System;
using System.Collections.Generic;
using System.Text;

namespace SaoLei
{
    public class Mine
    {
        private int width { set; get; }
        private int height { set; get; }
        private int mineNumber { set; get; }
        private int[,] map { set; get; }
        private int[] direction;//用来遍历中心周围八个位置
        /// <summary>
        /// 游戏状态，0-未开始，1-进行中，2-已结束
        /// </summary>
        public int status { private set; get; }

        public void Init(int width = 12, int height = 12, int number = 9)
        {
            this.width = width;
            this.height = height;
            this.mineNumber = number;
            this.status = 0;
            direction = new int[3] { 0, 1, -1 };

            map = new int[height, width];
        }

        public void start()
        {
            //初始化地图
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j] = -1;
                }
            }

            //初始化地雷
            Random rand = new Random();
            for (int i = 0; i < mineNumber;)
            {
                int row = rand.Next(height - 2) + 1;
                int col = rand.Next(width - 2) + 1;
                if (map[row, col] != -2)
                {
                    map[row, col] = -2;//-2代表地雷
                    i++;
                }
            }
        }

        /// <summary>
        /// 输出地图
        /// </summary>
        /// <param name="showAll">显示地雷，若为true，则显示所有的地雷</param>
        public void show(bool showAll = false)
        {
            //输出头部信息
            Console.Write("   |");
            for (int i = 1; i < width - 1; i++)
            {
                Console.Write("{0,2} ", i);
            }
            Console.WriteLine("\n---|-------------------------------->col");

            //输出地图信息
            if (showAll)
            {
                for (int row = 1; row < height - 1; row++)
                {
                    Console.Write("{0,2} |", row);
                    for (int col = 1; col < width - 1; col++)
                    {
                        if (map[row, col] == -1)
                        {
                            Console.Write("{0,2} ", "#");
                        }
                        else if (map[row, col] == -2)
                        {
                            Console.Write("{0,2} ", "*");
                        }
                        else
                        {
                            Console.Write("{0,2} ", map[row, col]);
                        }
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                for (int row = 1; row < height - 1; row++)
                {
                    Console.Write("{0,2} |", row);
                    for (int col = 1; col < width - 1; col++)
                    {
                        if (map[row, col] == -1 || map[row, col] == -2)
                        {
                            Console.Write("{0,2} ", "#");
                        }
                        else
                        {
                            Console.Write("{0,2} ", map[row, col]);
                        }
                    }
                    Console.WriteLine();
                }
            }

            //输出底部信息
            Console.WriteLine("   |");
            Console.WriteLine("   V");
            Console.WriteLine("  row");
        }

        /// <summary>
        /// 计算当前位置周围8个位置中地雷的总数
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public int calculate(int row, int col)
        {
            int counter = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (map[row + direction[i], col + direction[j]] == -2)
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }

        /// <summary>
        /// 选中[row,col]位置
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void click(int row, int col)
        {
            //输入的位置不合法
            if (row <= 0 || row > (height - 2) || col <= 0 || col > (width - 2)) return;

            //如果当前位置是-2（雷），则游戏结束
            if (map[row, col] == -2)
            {
                status = 2;//游戏结束
                Console.WriteLine("You lose!");
                return;
            }

            int number = calculate(row, col);
            //如果当前位置周围的地雷数量为0，则显示周围8个格子的数值，并自动递归拓展周围值为0的格子
            if (0 == number)
            {
                //首先设置当前位置为0
                map[row, col] = 0;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (row + direction[i] <= (height - 2) && col + direction[j] <= (width - 2) && row + direction[i] >= 1 && col + direction[j] >= 1 && //定义边界
                           !(direction[i] == 0 && direction[j] == 0) && //两个方向不能同时为0，否则会递归调用自身
                           map[row + direction[i], col + direction[j]] == -1 //保证不能返回调用
                            )
                        {
                            click(row + direction[i], col + direction[j]);
                        }
                    }
                }
            }
            else
            {
                map[row, col] = number;
            }

            //检查游戏是否结束
            if (isWin() == true)
            {
                Console.WriteLine("You Win!");
            }
        }

        /// <summary>
        /// 检查游戏是否结束
        /// </summary>
        /// <returns></returns>
        public bool isOver()
        {
            return status == 2;
        }

        /// <summary>
        /// 检查玩家是否已经赢了
        /// </summary>
        /// <returns></returns>
        private bool isWin()
        {
            /*
             * 所有的位置都已经被点击过了，就认为玩家赢了 
             * 
             */

            int count = 0;

            for (int row = 1; row < height - 1; row++)
            {
                for (int col = 1; col < width - 1; col++)
                {
                    if (map[row, col] == -1)
                    {
                        count++;
                    }
                }
            }

            return count == 0;
        }
    }
}
