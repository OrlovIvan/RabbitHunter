using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitHunter
{
    class Vector
    {
        int x, y;                             //текущие координаты
        int x_old, y_old;                     //прошлые координаты точки

        // ******************** методы ********************
        public Vector(int x, int y) //к
        {
            this.x = x;
            this.y = y;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            Vector v3 = new Vector(0, 0);
            v3.x_old = v1.x;                  //запоминаются прошлые координаты
            v3.y_old = v1.y;
            v3.x = v1.x + v2.x;
            v3.y = v1.y + v2.y;
            return v3;
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            Vector v3 = new Vector(0, 0);
            v3.x_old = v1.x;                  //запоминаются прошлые координаты
            v3.y_old = v1.y;
            v3.x = v1.x - v2.x;
            v3.y = v1.y - v2.y;
            return v3;
        }

        // ******************** свойства ********************
        public int X //свойство x
        {
            get { return x; }
            set
            {
                this.x = value;
            }
        }

        public int Y //свойство y
        {
            get { return y; }
            set
            {
                this.y = value;
            }
        }

        public int X_Old //свойство x
        {
            get { return x_old; }
        }

        public int Y_Old //свойство y
        {
            get { return y_old; }
        }
    }
}
