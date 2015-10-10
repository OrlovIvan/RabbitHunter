using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitHunter
{
    //класс символа зайца
    class RabbitSymbol
    {
        protected char sym;
        protected char true_sym;                                  //истинный символ зайца. Нужен для операции освобождения из коробки
        protected ConsoleColor rabbitColor;

        // ******************** методы ********************
        public RabbitSymbol()
        {}

        public RabbitSymbol(char sym1, ConsoleColor rabbitColor1)
        {
            this.sym = sym1;
            this.rabbitColor = rabbitColor1;
        }

        // ******************** свойства ********************
        public char GetSym
        {
            get { return sym; }
        }
        public ConsoleColor GetrColor
        {
            get { return rabbitColor; }
        }
    }

    //непосредственно заяц. С позицией, направлением движения и пр.
    class MyObject : RabbitSymbol
    {
        protected Vector pos, direction;

        protected int reg_x1, reg_x2, reg_y1, reg_y2; //координаты выделенного региона

        // ******************** методы ********************
        public virtual void Move()
        {
            pos = pos + direction;
        }

        protected int GetRandomDir()
        {
            Random rnd = new Random();
            return (rnd.Next(0, 3) - 1);
        }

        //запоминание зайцем границ вольера
        public void SetRegion(NewRegion region)
        {
            this.Reg_X1 = region.Reg_X1;
            this.Reg_X2 = region.Reg_X2;
            this.Reg_Y1 = region.Reg_Y1;
            this.Reg_Y2 = region.Reg_Y2;
        }

        public void Show()
        {
            Clear();
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.ForegroundColor = rabbitColor;
            Console.Write(this.Sym);
            Console.ResetColor();
        }

        //очистка места, которое занимал заяц
        public void Clear()
        {
            Console.SetCursorPosition(pos.X_Old, pos.Y_Old);
            Console.Write(' ');
        }

        //уничтожение символа зайца при попадании в коробку. Криво, конечно. TODO: Лучше бы реально садить его в коробку
        public void Destroy()
        {
            sym = ' ';
            pos.X = Reg_X2 + 2;
            pos.Y = Reg_Y2 + 2;
            direction.X = 0;
            direction.Y = 0;
        }

        //освобождение зайца из коробки
        public void Free()
        {
            this.pos.X = (this.reg_x1 + this.reg_x2) / 2 + this.GetRandomDir();
            this.pos.Y = (this.reg_y1 + this.reg_y2) / 2 + this.GetRandomDir();
            this.direction.X = this.GetRandomDir();
            this.direction.Y = this.GetRandomDir();
            this.sym = this.true_sym;
        }

        public Vector GetPosition()
        {
           return pos;
        }

        public Vector GetDirection()
        {
           return direction;
        }

        // ******************** свойства ********************
        protected int Reg_X1
        {
            get { return reg_x1; }
            set { reg_x1 = value; }
        }
        protected int Reg_X2
        {
            get { return reg_x2; }
            set { reg_x2 = value; }
        }
        protected int Reg_Y1
        {
            get { return reg_y1; }
            set { reg_y1 = value; }
        }
        protected int Reg_Y2
        {
            get { return reg_y2; }
            set { reg_y2 = value; }
        }

        protected char Sym
        {
            get { return sym; }
        }

        protected ConsoleColor rColor
        {
            get { return rabbitColor; }
        }        
    }
}
