using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitHunter
{
    class RedRabbit : MyObject
    {
        private int MoveCount;      //переменная для счёта шагов после удара о стенку
        bool DirChange;             //показатель изменёного состояния сознания зайца :) (после удара о стенку)

        // ******************** методы ********************
        public RedRabbit()
        {
            pos = new Vector(0, 0);
            direction = new Vector(0, 0);
            MoveCount = 0;
            DirChange = false;
            sym = '*';
            this.true_sym = this.sym;
            rabbitColor = ConsoleColor.Red;
        }

        public RedRabbit(Vector pos, Vector direction, char sym)
        {
            this.pos = pos;
            this.direction = direction;
            MoveCount = 0;
            DirChange = false;
            this.sym = sym;
            this.true_sym = sym;
            rabbitColor = ConsoleColor.Red;
        }

        public override void Move()
        {
            CheckRegionBorders(); 

            pos = pos + direction;
        }

        private void CheckRegionBorders()
        {
            if (pos.X > this.Reg_X2 - 2 || pos.X < this.Reg_X1 + 2)
            {
                direction.X = -direction.X;
                DirChange = true;
                direction.Y = 0;
            }
            if (pos.Y > this.Reg_Y2 - 2 || pos.Y < this.Reg_Y1 + 2)
                direction.Y = -direction.Y;

            if (direction.Y == 0 && DirChange)
            {
                if (MoveCount < 5)
                    MoveCount++;
                else
                {
                    MoveCount = 0;
                    if ((pos.Y != this.Reg_Y2 - 1) && (pos.Y != this.Reg_Y1 + 1))
                        direction.Y = this.GetRandomDir();
                    else
                    {
                        if (pos.Y == this.Reg_Y2 - 1)
                            direction.Y = -1;
                        else if (pos.Y == this.Reg_Y1 + 1)
                            direction.Y = 1;
                    }

                    if (direction.Y != 0)
                        DirChange = false;
                }
            }

            if (direction.X == 0)
            {
                direction.X = this.GetRandomDir();
            }
        }

        public void ShowXY(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("PosX=" + pos.X + " PosY=" + pos.Y + " DirX=" + direction.X + " DirY=" + direction.Y + " ");
        }

    }
}
