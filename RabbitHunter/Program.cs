//Игра "Зайцелов"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitHunter
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
           // int TrappedRabbitCount = 0; //счётчик пойманных зайцев

            Greetings();
            
            NewRegion region = new NewRegion(4,4,70,20);                                            //создание игрового поля

            /*
            //Для отладки!
            GreenRabbit greenRabbit = new GreenRabbit(new Vector(10, 10), new Vector(1, 1), '*');   //зелёный заяц
            RedRabbit redRabbit = new RedRabbit(new Vector(9, 6), new Vector(1, 1), 'O');           //красный заяц
            BoxTrap Box = new BoxTrap(11, 11, 4);                                                   //коробочка будет стоять прямо на пути объектов
            */

            Random rDirect = new Random();

            GreenRabbit greenRabbit = new GreenRabbit(new Vector(10, 10), new Vector(rDirect.Next(-1, 2), rDirect.Next(-1, 2)), '*');   //зелёный заяц
            RedRabbit redRabbit = new RedRabbit(new Vector(9, 6), new Vector(rDirect.Next(-1, 2), rDirect.Next(-1, 2)), 'O');           //красный заяц

            BoxTrap Box = new BoxTrap(60,10,4);                                                                                         //установка коробки-ловушки            

            Vector BoxDir = new Vector(0,0);

            greenRabbit.SetRegion(region);              //запоминание зайцами границ поля
            redRabbit.SetRegion(region);

            Box.SetRegion(region);                      //запоминание ловушкой игрового поля

            while (true)
            {                
                greenRabbit.Move();
                redRabbit.Move();
                greenRabbit.Show();
                redRabbit.Show();
                //greenRabbit.ShowXY(0, 0);

                #region BoxMoving
                //обработка движения ловушки
                if (Console.KeyAvailable)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    key = Console.ReadKey();
                    Console.ResetColor();
                    switch (key.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            BoxDir.X = -1;
                            Box.BoxMove(ref BoxDir);
                            break;
                        case ConsoleKey.RightArrow:
                            BoxDir.X = 1;
                            Box.BoxMove(ref BoxDir);
                            break;
                        case ConsoleKey.UpArrow:
                            BoxDir.Y = -1;
                            Box.BoxMove(ref BoxDir);
                            break;
                        case ConsoleKey.DownArrow:
                            BoxDir.Y = 1;
                            Box.BoxMove(ref BoxDir);
                            break;
                        case ConsoleKey.R:
                            Box.BoxRotate();
                            break;
                    }                    
                }

                #endregion

                //проверка на попадание зайца в ловушку
                #region CheckTrap
                if (Box.CheckTrapped(greenRabbit.GetPosition(), greenRabbit.GetDirection(), greenRabbit.GetSym, greenRabbit.GetrColor))
                {
                    greenRabbit.Destroy();
                    WriteTrappedrabbitsCount(Box.GetTrappedRabbitCount);
                }
                if (Box.CheckTrapped(redRabbit.GetPosition(), redRabbit.GetDirection(), redRabbit.GetSym, redRabbit.GetrColor))
                {
                    redRabbit.Destroy();
                    WriteTrappedrabbitsCount(Box.GetTrappedRabbitCount);
                }
                #endregion

                //если пойманы оба зайца
                if (Box.GetTrappedRabbitCount == 2)
                {
                    Box.BoxDraw();
                    RabbitsAreTrapped(); 
                    
                    while(true)
                    {
                        key = Console.ReadKey();
                        if (key.Key == ConsoleKey.Enter)                          //освобождение зайцев
                        {
                            Box.FreeRabbits();                                    //выпускаем их из коробки
                            WriteTrappedrabbitsCount(Box.GetTrappedRabbitCount);  //пишем об этом

                            greenRabbit.Free();                                   //даём каждому зайцу свободу
                            redRabbit.Free();

                            ClearRegion(region.Reg_X1, region.Reg_X2, region.Reg_Y1, region.Reg_Y2); //чистим рабочую область
                            Box.BoxDraw();
                            break;
                        }
                        else if (key.Key == ConsoleKey.Escape)                    //окончание игры
                            return;
                    }
                }

                System.Threading.Thread.Sleep(100);
            }

        }
        #region StringsFunctions
        static void Greetings()
        {
            ConsoleKeyInfo key;
            Console.SetCursorPosition(30, 10);
            Console.WriteLine("Зайцелов v1.0");
            Console.SetCursorPosition(30, 14);
            Console.WriteLine("Нажмите Enter");
            do
            {
                key = Console.ReadKey();
            }
            while (key.Key != ConsoleKey.Enter);

            Console.Clear();

            Console.SetCursorPosition(20, 10);
            Console.WriteLine("Стрелки - перемещение, R - поворот ловушки");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();

            Console.SetCursorPosition(35, 0);
            Console.WriteLine("Стрелки - перемещение, R - поворот ловушки");
        }

        static void ClearRegion(int x1, int x2, int y1, int y2)
        {
            for (int i = y1 + 1; i < y2; ++i)
            {
                Console.SetCursorPosition(x1 + 1, i);
                Console.WriteLine(("").PadRight(x2 - x1 - 1, ' '));
            }
        }

        static void WriteTrappedrabbitsCount(int count)
        {
            Console.SetCursorPosition(35, 1);
            Console.WriteLine("Пойманных зайцев: {0}", count);
        }

        static void RabbitsAreTrapped()
        {
            Console.SetCursorPosition(30, 10);
            Console.WriteLine("Зайцы пойманы.");
            Console.SetCursorPosition(14, 11);
            Console.WriteLine("Отпустить их (Enter) или закончить игру (Esc)?");
        }
        #endregion
    }
}
