using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public static void AI()
    {
        GameObject choose;
        int type = 0;
        bool judge = false;
        System.Random ran = new System.Random();
        int randomkey = ran.Next(4, 7);
        print("ai随机数为" + randomkey);
        for (int i = randomkey; i <= randomkey+2; i++)//456红色方三个色块
        {
            choose = GameObject.Find("RedSide/Choose" + ((i+2)%3+1));
            type = choose.gameObject.GetComponent<ChooseType>().type;
            for (int x = 1; x <= 8; x++)//逐格尝试放入
            {
                for (int y = 1; y <= 12; y++)
                {
                    judge = Gezi.AllJudge(x, y, PublicData.xn[type - 1], PublicData.yn[type - 1], PublicData.num[type - 1]);

                    if (judge == true)
                    {
                        PublicData.ai_gezi_type = type;
                        PublicData.ai_gezi_x = x;
                        PublicData.ai_gezi_y = y;
                        break;
                    }
                }
                if (judge == true)
                    break;
            }
            if (judge == true)
            {
                PublicData.bechosen = (i+2)%3+1+3;
                break;
            }
                
        }
        if(judge==false)
        {
            return;
        }
        int publicnum = PublicData.num[PublicData.ai_gezi_type - 1];
        int[] publicxn = new int[publicnum];
        int[] publicyn = new int[publicnum];
        if(PublicData.ai_gezi_rotate == 0)
        {
            for (int i = 0; i < publicnum; i++)
            {
                publicxn[i] = PublicData.xn[type - 1][i];
                publicyn[i] = PublicData.yn[type - 1][i];
            }
        }
        else if (PublicData.ai_gezi_rotate == 1)
        {
            for (int i = 0; i < publicnum; i++)
            {
                publicxn[i] = -PublicData.yn[type - 1][i];
                publicyn[i] = PublicData.xn[type - 1][i];
            }
        }
        else if (PublicData.ai_gezi_rotate == 2)
        {
            for (int i = 0; i < publicnum; i++)
            {
                publicxn[i] = -PublicData.xn[type - 1][i];
                publicyn[i] = -PublicData.yn[type - 1][i];
            }
        }
        else if(PublicData.ai_gezi_rotate == 3)
        {
            for(int i = 0; i < publicnum; i++)
            {
                publicxn[i] = PublicData.yn[type - 1][i];
                publicyn[i] = -PublicData.xn[type - 1][i];
            }
        }
        Gezi.Paint(PublicData.ai_gezi_x, PublicData.ai_gezi_y, publicxn, publicyn, publicnum);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
