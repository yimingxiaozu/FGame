using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class PublicData {
    public static string playername = "";
    public static int winnum = 0;
    public static int gamenum = 0;
    public static double winrate = 0;
    public static int BlueCount = 0;
    public static int RedCount = 0;
    public static int RoundCount = 1;
    public static int type = 0;
    public static int bechosen = 0; //123蓝色方3个 456红色方3个 0表示没有
    public static int[][] xn = new int[14][]{new int[3]{0,1,0},
                                            new int[4]{0,0,1,1},
                                            new int[4]{0,0,-1,0},
                                            new int[4]{0,0,1,1},
                                            new int[4]{0,0,-1,-1},
                                            new int[3]{0,0,0},
                                            new int[4]{0,0,0,0},
                                            new int[4]{0,0,0,-1},
                                            new int[4]{0,0,0,1},
                                            new int[5]{0,0,0,1,2},
                                            new int[5]{0,1,1,2,2},
                                            new int[5]{0,1,1,2,2},
                                            new int[5]{0,0,0,0,0},
                                            new int[5]{0,0,0,-1,-1}};
    
    public static int[][] yn = new int[14][]{new int[3]{0,0,1},
                                            new int[4]{0,1,0,1},
                                            new int[4]{0,-1,0,1},
                                            new int[4]{0,1,1,2},
                                            new int[4]{0,1,1,2},
                                            new int[3]{0,1,2},
                                            new int[4]{0,1,2,3},
                                            new int[4]{0,1,2,2},
                                            new int[4]{0,1,2,2},
                                            new int[5]{0,1,2,0,0},
                                            new int[5]{0,0,1,0,1},
                                            new int[5]{0,-1,0,-1,0},
                                            new int[5]{0,1,2,3,4},
                                            new int[5]{0,-1,1,-1,1}};
    public static int []num = {3,4,4,4,4,3,4,4,4,5,5,5,5,5};
    public static bool gamelock = false;
    public static int gamemoshi = 0;
}