using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChongZhi : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }
    public void Chongzhi()
    {
        PublicData.BlueCount = 0;
        PublicData.RedCount = 0;
        PublicData.RoundCount = 1;
        PublicData.type = 0;
        PublicData.bechosen = 0;
        PublicData.gamelock = false;
        GameObject canvas = GameObject.Find("Canvas");
        GameObject bluewintext = canvas.transform.Find("BlueWinText").gameObject;
        GameObject redwintext = canvas.transform.Find("RedWinText").gameObject;
        bluewintext.SetActive(false);
        redwintext.SetActive(false);
        for (int i = 1; i <= 3; i++)
        { Sprite tmpsprite;
            tmpsprite = Resources.Load("img/type" + i, typeof(Sprite)) as Sprite;

            GameObject choose = canvas.transform.Find("BlueSide/Choose" + i).gameObject;
            choose.GetComponent<ChooseType>().type = i;
            choose.GetComponent<ChooseType>().rotate = 0;
            choose.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
            Image tmpimage = choose.GetComponent<Image>();
            tmpimage.sprite = tmpsprite;

            choose = canvas.transform.Find("RedSide/Choose" + i).gameObject;
            choose.GetComponent<ChooseType>().type = i;
            choose.GetComponent<ChooseType>().rotate = 0;
            choose.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
            tmpimage = choose.GetComponent<Image>();
            tmpimage.sprite = tmpsprite;
        }
        for (int i = 1; i <= 8; i++)
        {
            for (int j = 1; j <= 12; j++)
            {
                GameObject Gezi = canvas.transform.Find("Panel/Image" + i + "-" + j).gameObject;
                Gezi.GetComponent<Gezi>().conquer = false;
                Image tmpimage = Gezi.GetComponent<Image>();
                Sprite tmpsprite = Resources.Load("img/gezi", typeof(Sprite)) as Sprite;
                tmpimage.sprite = tmpsprite;
            }
        }
        GameObject bluescore = canvas.transform.Find("BlueSide/BlueScore").gameObject;
        bluescore.GetComponent<Text>().text = "0";
        GameObject redscore = canvas.transform.Find("RedSide/RedScore").gameObject;
        redscore.GetComponent<Text>().text = "0";
    }
    public void Fanhui()
    {
        Chongzhi();
        SceneManager.LoadScene("Scenes/XuanzeMoshi");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
