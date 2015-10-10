using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitHunter
{
    //некий символ
    public class Symbol
    {
        int pos_x, pos_y;
        char symbol;

        // ******************** методы ********************
        public Symbol()
        {}

        public Symbol(int x, int y, char sym)
        {
            this.pos_x = x;
            this.pos_y = y;
            this.symbol = sym;
        }

        public char Sym
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public virtual void Draw()
        {
            Console.SetCursorPosition(pos_x, pos_y);
            Console.Write(this.Sym);
        }
    }

    public class HLine : Symbol
    {
        int x1, x2, y;

        List<Symbol> symList;

        // ******************** методы ********************
        public HLine(int x1, int x2, int y, char Sym)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y = y;

            symList = new List<Symbol>();
            for(int i=x1;i<=x2;++i)
            {
                Symbol NextSymbol = new Symbol(i, y, Sym);
                symList.Add(NextSymbol);
            }
        }

        public override void Draw()
        {
            foreach (Symbol s in symList)
            {
                s.Draw();
            }
        }
    }

    public class VLine : Symbol
    {
        int x, y1, y2;

        List<Symbol> symList;

        // ******************** методы ********************
        public VLine(int x, int y1, int y2, char Sym)
        {
            this.x = x;
            this.y1 = y1;
            this.y2 = y2;

            symList = new List<Symbol>();
            for(int i=y1;i<=y2;++i)
            {
                Symbol NextSymbol = new Symbol(x, i, Sym);
                symList.Add(NextSymbol);
            }
        }

        public override void Draw()
        {
            foreach (Symbol s in symList)
            {
                s.Draw();
            }
        }

        public void DrawMultiple(int x1, int x2)
        {
            for (int i = x1; i <= x2; ++i)
            {
                for (int j = y1; j <= y2; ++j)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(' ');
                }
            }
        }
    }

    //Выделенный регион. Игровое поле
    public class NewRegion
    {
        int reg_x1, reg_x2, reg_y1, reg_y2; //координаты выделенного региона

        // ******************** методы ********************
        public NewRegion(int x1, int y1, int x2, int y2)
        {
             DrawRegion(x1, y1, x2, y2);
        }

        //рисование ограниченного пространства для перемещения точки
        public void DrawRegion(int x1, int y1, int x2, int y2)
        {
            this.reg_x1 = x1;
            this.reg_x2 = x2;
            this.reg_y1 = y1;
            this.reg_y2 = y2;

            HLine h_line1 = new HLine(x1, x2, y1, '+');
            h_line1.Draw();
            HLine h_line2 = new HLine(x1, x2, y2, '+');
            h_line2.Draw();

            VLine v_line1 = new VLine(x1, y1, y2, '+');
            v_line1.Draw();
            VLine v_line2 = new VLine(x2, y1, y2, '+');
            v_line2.Draw();
        }

        // ******************** свойства ********************
        public int Reg_X1
        {
            get { return reg_x1; }
        }
        public int Reg_X2
        {
            get { return reg_x2; }
        }
        public int Reg_Y1
        {
            get { return reg_y1; }
        }
        public int Reg_Y2
        {
            get { return reg_y2; }
        }
    }
}
