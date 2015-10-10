using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitHunter
{
    class GreenRabbit : MyObject
    {
        // ******************** методы ********************
        public GreenRabbit()
        {
            pos = new Vector(0, 0);
            direction = new Vector(0, 0);
            sym = '*';
            this.true_sym = this.sym;
            rabbitColor = ConsoleColor.Green;
        }

        public GreenRabbit(Vector pos, Vector direction, char sym)
        {
            this.pos = pos;
            this.direction = direction;
            this.sym = sym;
            this.true_sym = sym;
            rabbitColor = ConsoleColor.Green;
        }

        ~GreenRabbit()
        {
            Clear();
        }

        public override void Move()
        {
            CheckRegionBorders();

            pos = pos + direction;
        }

        private void CheckRegionBorders()
        {
            if (pos.X > this.Reg_X2 - 2 || pos.X < this.Reg_X1 + 2)
                direction.X = -direction.X;
            if (pos.Y > this.Reg_Y2 - 2 || pos.Y < this.Reg_Y1 + 2)
                direction.Y = -direction.Y;

            if (direction.X == 0)
            {
                direction.X = this.GetRandomDir();
            }

            if (direction.Y == 0)
            {
                direction.Y = this.GetRandomDir();
            }
        }

        public void ShowXY(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("PosX=" + pos.X + " PosY=" + pos.Y + " DirX=" + direction.X + " DirY=" + direction.Y + " ");
        }
    }
}
