using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseType : MonoBehaviour {
    public int type;
    public bool blueside;
	// Use this for initialization
	void Start () {
		
	}
	public void ChooseClick()
    {
        if (PublicData.gamelock == true)
            return;
        if (PublicData.RoundCount%2==1&&blueside==true|| PublicData.RoundCount % 2 == 0 && blueside == false)
        {
            PublicData.type = type;
            print("选择类型" + type);
            if (blueside == true)
                PublicData.bechosen = type;
            else
                PublicData.bechosen = 2 + type;
            print("bechosen:" + PublicData.bechosen);
        }
        
    }
	// Update is called once per frame
	void Update () {
		
	}
}
