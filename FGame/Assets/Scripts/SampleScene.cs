using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SampleScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject usertext = GameObject.Find("UsernameText");
        GameObject winnumtext = GameObject.Find("WinnumText");
        usertext.GetComponent<Text>().text = "玩家：" + PublicData.playername;
        winnumtext.GetComponent<Text>().text = "胜场：" + PublicData.winnum.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
