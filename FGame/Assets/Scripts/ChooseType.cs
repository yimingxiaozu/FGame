using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseType : MonoBehaviour {
    public int type;
    public bool blueside;
    public int choosenum;
    public int rotate = 0;
	// Use this for initialization
	void Start () {
		
	}
	public void ChooseClick()
    {
        if(blueside==true)
        {
            if(choosenum==PublicData.bechosen)
            {
                Rotate();
            }
        }
        else
        {
            if (choosenum == PublicData.bechosen-2)
            {
                Rotate();
            }
        }
        if (PublicData.gamelock == true)
            return;
        if (PublicData.RoundCount%2==1&&blueside==true|| PublicData.RoundCount % 2 == 0 && blueside == false)
        {
            PublicData.type = type;
            print("选择类型" + type);
            if (blueside == true)
                PublicData.bechosen = choosenum;
            else
                PublicData.bechosen = 2 + choosenum;
            print("bechosen:" + PublicData.bechosen);
        }
    }
    void Rotate()
    {
        this.transform.Rotate(0, 0, 90); //逆时针
        rotate++;
        print("旋转一次 "+rotate);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
