using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitHunter
{
    //класс коробки-ловушки
    class BoxTrap : MyObject
    {
        //положение открытого края
        public enum OpenSideDir
        {
            osdUp,
            osdRight,
            osdDown,
            osdLeft
        }

        int size;
        OpenSideDir OpenDir;
        List<RabbitSymbol> trapped_obj = new List<RabbitSymbol>();

        public BoxTrap(int pos_x, int pos_y, int size)                  //конструктор
        {
            OpenDir = OpenSideDir.osdUp;
            pos = new Vector(pos_x, pos_y);
            this.sym = '&';

            if (size > 2 && size < 5)
                this.size = size-1;
            else
                this.size = 3;

            BoxDraw();
        }


        // ******************** методы ********************
        private void BoxClear()
        {
            VLine LineLeft = new VLine(pos.X, pos.Y, pos.Y + size, ' ');
            LineLeft.DrawMultiple(pos.X, pos.X + size);
        }

        public void BoxDraw()
        {
            VLine LineLeft = new VLine(pos.X, pos.Y, pos.Y + size, this.sym);
            VLine LineRight = new VLine(pos.X + size, pos.Y, pos.Y + size, this.sym);
            HLine HLineUp = new HLine(pos.X, pos.X + size, pos.Y, this.sym);
            HLine HLineDown = new HLine(pos.X, pos.X + size, pos.Y + size, this.sym);

            if (OpenDir != OpenSideDir.osdLeft)  LineLeft.Draw();
            if (OpenDir != OpenSideDir.osdRight) LineRight.Draw();
            if (OpenDir != OpenSideDir.osdUp)    HLineUp.Draw();
            if (OpenDir != OpenSideDir.osdDown)  HLineDown.Draw();

            BoxTrappedObjDraw(OpenDir);
        }

        //отрисовка зайца в коробке, если он там есть
        private void BoxTrappedObjDraw(OpenSideDir OpenSide)
        {
            int i = 0;
            foreach (RabbitSymbol c in trapped_obj)
                {
                    if (c.GetSym != ' ')
                    {
                        switch (OpenDir)
                        {
                            case OpenSideDir.osdLeft:
                                {
                                    if(i == 0)
                                        TrappedSymDraw(pos.X+size-1, pos.Y+1 ,c);
                                    else if(i == 1)
                                        TrappedSymDraw(pos.X+size-1, pos.Y+2 ,c);
                                }
                                break;
                            case OpenSideDir.osdRight:
                                {
                                    if(i == 0)
                                        TrappedSymDraw(pos.X+1, pos.Y+size-1 ,c);
                                    else if(i == 1)
                                        TrappedSymDraw(pos.X+1, pos.Y+size-2 ,c);
                                }
                                break;
                            case OpenSideDir.osdUp:
                                {
                                    if(i == 0)
                                        TrappedSymDraw(pos.X+size-1, pos.Y+size-1 ,c);
                                    else if(i == 1)
                                        TrappedSymDraw(pos.X+size-2, pos.Y+size-1 ,c);
                                }
                                break;
                            case OpenSideDir.osdDown:
                                {
                                    if(i == 0)
                                        TrappedSymDraw(pos.X+1, pos.Y+1 ,c);
                                    else if(i == 1)
                                        TrappedSymDraw(pos.X+2, pos.Y+1 ,c);
                                }
                                break;
                        }
                    }
                i++;
                }
            
        }

        //отрисовка символа зайца в коробке
        private void TrappedSymDraw(int x, int y, RabbitSymbol rSymbol)
        {
            Console.SetCursorPosition(x,y);
            Console.ForegroundColor = rSymbol.GetrColor;
            Console.Write(rSymbol.GetSym);
            Console.ResetColor();
        }

        public void BoxRotate()
        {
            if (OpenDir != OpenSideDir.osdLeft)
                OpenDir++;
            else
                OpenDir = OpenSideDir.osdUp;

            BoxClear();
            BoxDraw();
        }

        public void BoxMove(ref Vector NewDir)
        {
            if (((pos.X + size > this.Reg_X2 - 2) && (NewDir.X == 1))
                || ((pos.X < this.Reg_X1 + 2) && (NewDir.X == -1)))
            {
                NewDir.X = 0;
            }
            if (((pos.Y+size > this.Reg_Y2 - 2) && (NewDir.Y == 1))
                || ((pos.Y < this.Reg_Y1 + 2) && (NewDir.Y == -1)))
                NewDir.Y = 0;

            BoxClear();
            this.pos = this.pos + NewDir;
            BoxDraw();
            NewDir.X = 0;
            NewDir.Y = 0;
        }

        //проверка на "пойманность" объекта
        public bool CheckTrapped(Vector obj_position, Vector obj_direction, char symbol, ConsoleColor rColor)
        {
            bool res = false;

            switch(OpenDir)
            {
                case OpenSideDir.osdLeft:
                    if((obj_position.X == this.pos.X) && (obj_position.Y >= this.pos.Y) && (obj_position.Y <= this.pos.Y+size)
                        && (obj_direction.X == 1))
                        res = true;
                    break;
                case OpenSideDir.osdRight:
                    if(((obj_position.X == this.pos.X+size) && (obj_position.Y >= this.pos.Y) && (obj_position.Y <= this.pos.Y+size))
                       && (obj_direction.X == -1))
                        res = true;
                    break;
                case OpenSideDir.osdUp:
                    if(((obj_position.Y == this.pos.Y) && (obj_position.X >= this.pos.X) && (obj_position.X <= this.pos.X+size))
                        && (obj_direction.Y == 1))
                        res = true;
                    break;
                case OpenSideDir.osdDown:
                    if(((obj_position.Y == this.pos.Y + size) && (obj_position.X >= this.pos.X) && (obj_position.X <= this.pos.X+size))
                        && (obj_direction.Y == -1))
                        res = true;
                    break;
            }

            if (res)
            {
                RabbitSymbol rSym = new RabbitSymbol(symbol, rColor);
                trapped_obj.Add(rSym);                                  //если поймали, добавляем символ в список пойманных
            }

            return res;
        }

        //освобождение зайцев из коробки
        public void FreeRabbits()
        {
            trapped_obj.Clear();
        }

        // ******************** свойства ********************

        public Vector GetBoxPosition
        {
            get { return this.pos; }
        }

        public OpenSideDir GetOpenSidePosition
        {
            get { return this.OpenDir; }
        }

        public int GetTrappedRabbitCount
        {
            get { return this.trapped_obj.Count; }
        }

    }
}
