using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class PublicData {
    public static int BlueCount = 0;
    public static int RedCount = 0;
    public static int RoundCount = 1;
    public static int type = 0;
    public static int bechosen = 0; //12蓝色方2个 34红色方2个 0表示没有
    public static int[][] xn = new int[4][]{new int[3]{0,1,0},
                                            new int[4]{0,0,1,1},
                                            new int[4]{0,0,-1,0},
                                            new int[4]{0,0,1,1}};
    
    public static int[][] yn = new int[4][]{new int[3]{0,0,1},
                                            new int[4]{0,1,0,1},
                                            new int[4]{0,-1,0,1},
                                            new int[4]{0,1,1,2}};
    public static int []num = {3,4,4,4};
    public static bool gamelock = false;
}