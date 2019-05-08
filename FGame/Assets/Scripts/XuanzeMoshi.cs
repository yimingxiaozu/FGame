using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class XuanzeMoshi : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject usertext = GameObject.Find("UsernameText");
        GameObject winnumtext = GameObject.Find("WinnumText");
        GameObject winratetext = GameObject.Find("WinrateText");
        usertext.GetComponent<Text>().text = "玩家账号："+PublicData.playername;
        winnumtext.GetComponent<Text>().text = "胜场："+PublicData.winnum.ToString();
        winratetext.GetComponent<Text>().text = "胜率："+PublicData.winrate.ToString("f2")+"%";
    }
	
	// Update is called once per frame
	void Update () {
        GameObject usertext = GameObject.Find("UsernameText");
        GameObject winnumtext = GameObject.Find("WinnumText");
        GameObject winratetext = GameObject.Find("WinrateText");
        usertext.GetComponent<Text>().text = "玩家账号：" + PublicData.playername;
        winnumtext.GetComponent<Text>().text = "胜场：" + PublicData.winnum.ToString();
        PublicData.winrate = PublicData.winnum * 100.0 / PublicData.gamenum;
        winratetext.GetComponent<Text>().text = "胜率：" + PublicData.winrate.ToString("f2") + "%";
    }
}
