using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Sprites;
public class Gezi : MonoBehaviour {
    public int x;
    public int y;
    public bool conquer;
    // Use this for initialization
    void Start () {
		
	}
    int rotate;
    GameObject tmpchoose;
    public static bool AllJudge(int X,int Y,int []xn,int []yn,int num)
    {
        bool judge=false;
        bool tmpjudge= true;
        int tmpx, tmpy;
        GameObject tmpgezi;
        for (int i = 0; i < num; i++)
        {
            tmpx = X + xn[i];
            tmpy = Y + yn[i];
            tmpgezi = GameObject.Find("Image" + tmpx + "-" + tmpy);
            if (!tmpgezi)
                tmpjudge=false;
            else if (tmpgezi.GetComponent<Gezi>().conquer == true)
                tmpjudge=false;
        }
        judge = judge || tmpjudge;
        if(tmpjudge == true && PublicData.RoundCount%2==0) //ai需求获取旋转次数
        {
            PublicData.ai_gezi_rotate = 0;
        }
        tmpjudge = true;
        for (int i = 0; i < num; i++)
        {
            tmpx = X + -yn[i];
            tmpy = Y + xn[i];
            tmpgezi = GameObject.Find("Image" + tmpx + "-" + tmpy);
            if (!tmpgezi)
                tmpjudge = false;
            else if (tmpgezi.GetComponent<Gezi>().conquer == true)
                tmpjudge = false;
        }
        judge = judge || tmpjudge;
        if (tmpjudge == true && PublicData.RoundCount % 2 == 0) //ai需求获取旋转次数
        {
            PublicData.ai_gezi_rotate = 1;
        }
        tmpjudge = true;
        for (int i = 0; i < num; i++)
        {
            tmpx = X + -xn[i];
            tmpy = Y + -yn[i];
            tmpgezi = GameObject.Find("Image" + tmpx + "-" + tmpy);
            if (!tmpgezi)
                tmpjudge = false;
            else if (tmpgezi.GetComponent<Gezi>().conquer == true)
                tmpjudge = false;
        }
        judge = judge || tmpjudge;
        if (tmpjudge == true && PublicData.RoundCount % 2 == 0) //ai需求获取旋转次数
        {
            PublicData.ai_gezi_rotate = 2;
        }
        tmpjudge = true;
        for (int i = 0; i < num; i++)
        {
            tmpx = X + yn[i];
            tmpy = Y + -xn[i];
            tmpgezi = GameObject.Find("Image" + tmpx + "-" + tmpy);
            if (!tmpgezi)
                tmpjudge = false;
            else if (tmpgezi.GetComponent<Gezi>().conquer == true)
                tmpjudge = false;
        }
        judge = judge || tmpjudge;
        if (tmpjudge == true && PublicData.RoundCount % 2 == 0) //ai需求获取旋转次数
        {
            PublicData.ai_gezi_rotate = 3;
        }
        return judge;
    }
    bool Judge(int X,int Y,int[]xn,int []yn,int num)
    {
        int tmpx, tmpy;
        int tmp;
        if (PublicData.bechosen>3)
        {
            tmpchoose = GameObject.Find("RedSide/Choose"+(PublicData.bechosen-3));
        }
        else
        {
            tmpchoose = GameObject.Find("BlueSide/Choose" + PublicData.bechosen);
        }
        rotate=tmpchoose.gameObject.GetComponent<ChooseType>().rotate;
        rotate %= 4;
        if(rotate==1)
        {
            for (int i = 0; i < num; i++)
            {
                tmp = xn[i];
                xn[i] = -yn[i];
                yn[i] = tmp;
            }
        }
        else if(rotate==2)
        {
            for (int i = 0; i < num; i++)
            {
                xn[i] = -xn[i];
                yn[i] = -yn[i];
            }
        }
        else if(rotate==3)
        {
            for (int i = 0; i < num; i++)
            {
                tmp = xn[i];
                xn[i] = yn[i];
                yn[i] = -tmp;
            }
        }
        for(int i=0;i<num;i++)
        {
            tmpx = X + xn[i];
            tmpy = Y + yn[i];
            GameObject tmpgezi = GameObject.Find("Image" + tmpx + "-" + tmpy);
            if (!tmpgezi)
                return false;
            if (tmpgezi.GetComponent<Gezi>().conquer == true)
                return false;
        }
        return true;
    }
    public static void Paint(int x,int y,int[] xn, int[] yn, int num)
    {
        int X, Y;
        GameObject bluescore = GameObject.Find("BlueScore");
        GameObject redscore = GameObject.Find("RedScore");
        for (int i = 0; i <num; i++)
        {
            X = x + xn[i];
            Y = y + yn[i];
            GameObject tmpgezi = GameObject.Find("Image" + X + "-" + Y);   
            Image tmpimage = tmpgezi.gameObject.GetComponent<Image>();
            Sprite tmpsprite;
            if (PublicData.RoundCount%2==1)
            {
                tmpsprite = Resources.Load("img/blue_gezi", typeof(Sprite)) as Sprite;
            }
            else
            {
                tmpsprite = Resources.Load("img/red_gezi", typeof(Sprite)) as Sprite;
            }
            tmpimage.sprite = tmpsprite;
            tmpgezi.GetComponent<Gezi>().conquer = true;
        }
        if (PublicData.RoundCount % 2 == 1)
        {
            PublicData.BlueCount += num;
            bluescore.GetComponent<Text>().text = PublicData.BlueCount.ToString();
        }
        else
        {
            PublicData.RedCount += num;
            redscore.GetComponent<Text>().text = PublicData.RedCount.ToString();
        }
            
        PublicData.RoundCount++;
        PublicData.type = 0;
        ChangeType();
        print("蓝色方块数" + PublicData.BlueCount);
        print("红色方块数" + PublicData.RedCount);
        if(PublicData.RoundCount%2==0) //判断是否是AI回合
        {
            AIRound.AI();
        }
    }
	public void GeziClick()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (PublicData.gamelock == true)
            return;
        int type = PublicData.type;
        if (type == 0)
            return;
    //    Shape shapetype = new Shape(PublicData.xn[type-1], PublicData.yn[type - 1], PublicData.num[type - 1]);
        //判断存在以及判断是否conquer
        
        int publicnum = PublicData.num[type - 1];
        int[] publicxn = new int[publicnum];      
        int []publicyn = new int[publicnum];
        for(int i=0;i< publicnum;i++)
        {
            publicxn[i]= PublicData.xn[type - 1][i];
            publicyn[i]= PublicData.yn[type - 1][i];
        }
        if (Judge(x,y, publicxn, publicyn, publicnum))
        {
            Paint(this.x,this.y, publicxn, publicyn, publicnum);
            tmpchoose.gameObject.GetComponent<ChooseType>().rotate = 0;
            tmpchoose.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            if(JudgeGameover()==false)//judge=true未结束 judge=false结束
            {
                print("游戏结束");
                PublicData.gamelock = true;
                if(PublicData.gamemoshi==1)
                {
                    if (PublicData.BlueCount > (PublicData.RedCount + 4))
                    {
                        print("蓝方获胜");
                        GameObject wintext = canvas.transform.Find("BlueWinText").gameObject;
                        wintext.SetActive(true);
                    }
                    else
                    {
                        print("红方获胜");
                        GameObject wintext = canvas.transform.Find("RedWinText").gameObject;
                        wintext.SetActive(true);
                    }
                }
                if(PublicData.gamemoshi==2)
                {
                    if(PublicData.RoundCount%2==0)
                    {
                        print("蓝方获胜");
                        GameObject wintext = canvas.transform.Find("BlueWinText").gameObject;
                        wintext.SetActive(true);
                    }
                    else
                    {
                        print("红方获胜");
                        GameObject wintext = canvas.transform.Find("RedWinText").gameObject;
                        wintext.SetActive(true);
                    }
                }
            }
        }
        
    }
    public static void ChangeType()
    {
        int chosen = PublicData.bechosen;
        GameObject choose;
        if (PublicData.bechosen<=3)
        {
            choose = GameObject.Find("BlueSide/Choose" + chosen);
        }
        else
        {
            chosen = chosen - 3;
            choose = GameObject.Find("RedSide/Choose" + chosen);
        }
        if(choose==null)
        {
            print("未能找到choose物体");
        }
        Image tmpimage = choose.gameObject.GetComponent<Image>();
        Sprite tmpsprite;
        System.Random ran = new System.Random();
        int randomkey = ran.Next(1, 15);//1~14
        print("随机数为"+randomkey);
        tmpsprite = Resources.Load("img/type"+randomkey, typeof(Sprite)) as Sprite;
        tmpimage.sprite = tmpsprite;
        choose.GetComponent<ChooseType>().type=randomkey;
    }
    bool JudgeGameover()
    {
        GameObject choose;
        int type;
        bool judge=false;//判断游戏是否结束 false表示结束
        
        for(int i=1;i<=3;i++) //2表示一边2个 后期变成3个
        {
            if(PublicData.RoundCount%2==1) 
            {
                choose = GameObject.Find("BlueSide/Choose" + i);
            }
            else
            {
                choose = GameObject.Find("RedSide/Choose" + i);
            }
            type=choose.gameObject.GetComponent<ChooseType>().type;
            for(int x=1;x<=8;x++)//逐格尝试放入
            {
                for(int y=1;y<=12;y++)
                {
                    judge = AllJudge(x, y, PublicData.xn[type - 1], PublicData.yn[type - 1], PublicData.num[type - 1]);

                    if (judge == true)
                        break;

                }
                if (judge == true)
                    break;
            }
            if (judge == true)
                break;
        }
        return judge;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
