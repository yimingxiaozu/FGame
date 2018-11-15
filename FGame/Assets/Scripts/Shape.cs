using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape{
        public int[] yn;
        public int[] xn;
        public int num;
        public Shape(int[] xn, int[] yn, int num)
        {
            this.num = num;
            for (int i = 0; i < num; i++)
            {
                this.xn[i] = xn[i];
                this.yn[i] = yn[i];
            }
        }
}
