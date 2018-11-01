using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Sprites;
public class Gezi : MonoBehaviour {
    public int x;
    public int y;
    public bool conquer=false;
	// Use this for initialization
	void Start () {
		
	}
    bool Judge(int []xn,int []yn,int num)
    {
        int X, Y;
        for(int i=0;i<num;i++)
        {
            X = x + xn[i];
            Y = y + yn[i];
            GameObject tmpgezi = GameObject.Find("Image" + X + "-" + Y);
            if (!tmpgezi)
                return false;
            if (tmpgezi.GetComponent<Gezi>().conquer == true)
                return false;
        }
        return true;
    }
    void Paint(int []xn,int []yn,int num)
    {
        int X, Y;
        GameObject bluescore = GameObject.Find("BlueScore");
        GameObject redscore = GameObject.Find("RedScore");
        for (int i = 0; i < num; i++)
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
        print("蓝色方块数" + PublicData.BlueCount);
        print("红色方块数" + PublicData.RedCount);
    }
	public void GeziClick()
    {
        int []Xn= {0,0,1};
        int []Yn= {0,1,0};
        int num = 3;
        //判断存在以及判断是否conquer
        if(Judge(Xn,Yn,num))
        {
            Paint(Xn, Yn, num);
        }
        
    }
	// Update is called once per frame
	void Update () {
		
	}
}
