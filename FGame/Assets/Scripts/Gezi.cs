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
    
    bool Judge(int X,int Y,int[]xn,int []yn,int num)
    {
        int tmpx, tmpy;
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
    void Paint(int[] xn, int[] yn, int num)
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
        print("蓝色方块数" + PublicData.BlueCount);
        print("红色方块数" + PublicData.RedCount);
    }
	public void GeziClick()
    {
        if (PublicData.gamelock == true)
            return;
        int type = PublicData.type;
        if (type == 0)
            return;
    //    Shape shapetype = new Shape(PublicData.xn[type-1], PublicData.yn[type - 1], PublicData.num[type - 1]);
        //判断存在以及判断是否conquer
        if(Judge(x,y,PublicData.xn[type - 1], PublicData.yn[type - 1], PublicData.num[type - 1]))
        {
            Paint(PublicData.xn[type - 1], PublicData.yn[type - 1], PublicData.num[type - 1]);
            ChangeType();
            if(JudgeGameover()==false)//judge=true未结束 judge=false结束
            {
                print("游戏结束");
                PublicData.gamelock = true;
            }
        }
        
    }
    void ChangeType()
    {
        int chosen = PublicData.bechosen;
        GameObject choose;
        if (PublicData.bechosen<=2)
        {
            choose = GameObject.Find("BlueSide/Choose" + chosen);
        }
        else
        {
            chosen = chosen - 2;
            choose = GameObject.Find("RedSide/Choose" + chosen);
        }
        if(choose==null)
        {
            print("未能找到choose物体");
        }
        Image tmpimage = choose.gameObject.GetComponent<Image>();
        Sprite tmpsprite;
        System.Random ran = new System.Random();
        int randomkey = ran.Next(1, 3);//1~2
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
        for(int i=1;i<=2;i++) //2表示一边2个 后期变成3个
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
            for(int x=1;x<=4;x++)//逐格尝试放入
            {
                for(int y=1;y<=9;y++)
                {
                    judge = Judge(x,y,PublicData.xn[type - 1], PublicData.yn[type - 1], PublicData.num[type - 1]);
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
