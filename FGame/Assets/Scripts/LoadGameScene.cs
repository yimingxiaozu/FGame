using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using cn.bmob.io;
using cn.bmob.api;
using cn.bmob.json;
using cn.bmob.tools;
using cn.bmob.response;
using cn.bmob.http;
using System;
using Newtonsoft.Json;

public class LoadGameScene : MonoBehaviour {
    private BmobUnity Bmob;
    // Use this for initialization
    void Start()
    {
        BmobDebug.Register(print);
        Bmob = gameObject.GetComponent<BmobUnity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public const String TABLENAME = "UserTable";
    public class MyBmobUser : BmobUser
    {
        public BmobInt gamenum { get; set; }
        public BmobInt winnum { get; set; }
        public override void write(BmobOutput output, bool all)
        {
            base.write(output, all);

            output.Put("gamenum", this.gamenum);
            output.Put("winnum", this.winnum);
        }

        public override void readFields(BmobInput input)
        {
            base.readFields(input);

            this.gamenum = input.getInt("gamenum");
            this.winnum = input.getInt("winnum");
        }
    }
    public void Signup()
    {
        MyBmobUser user = new MyBmobUser();
        InputField playername = GameObject.Find("Panel/PlayerName").GetComponent<InputField>();
        InputField password = GameObject.Find("Panel/Password").GetComponent<InputField>();
        InputField email = GameObject.Find("Panel/Email").GetComponent<InputField>();
        GameObject zhuangtai = GameObject.Find("Panel/ZhuangtaiText");
        user.username = playername.text;
        user.password = password.text;
        user.email = email.text;
        user.winnum = 0;
        user.gamenum = 0;
        string tmpstr = "";
        Bmob.Signup<MyBmobUser>(user, (resp, exception) =>
        {
            if (exception != null)
            {
                print("注册失败, 失败原因为： " + exception.Message);
                for (int i = 0; i < exception.Message.Length; i++)
                {
                    if (exception.Message[i] == '{')
                        tmpstr = exception.Message.Substring(i);
                }
                if (tmpstr == "")
                    zhuangtai.GetComponent<Text>().text = "注册失败, 失败原因为： " + exception.Message;
                else
                {
                    JavaScriptObject jsonobj = JavaScriptConvert.DeserializeObject<JavaScriptObject>(tmpstr);
                    zhuangtai.GetComponent<Text>().text = "注册失败, 失败原因为： " + jsonobj["error"];
                }
                return;
            }

            print("注册成功");
            zhuangtai.GetComponent<Text>().text = "注册成功,请登录";
        });
    }
    public void Login()
    {
        InputField playername = GameObject.Find("Panel/PlayerName").GetComponent<InputField>();
        InputField password = GameObject.Find("Panel/Password").GetComponent<InputField>();
        GameObject zhuangtai = GameObject.Find("Panel/ZhuangtaiText");
        GameObject canvas = GameObject.Find("Canvas");
        string tmpstr="";
        FindUser(playername.text);
        Bmob.Login<MyBmobUser>(playername.text, password.text, (resp, exception) => {
            if (exception != null)
            {
                print("登录失败, 失败原因为： " + exception.Message);
                for(int i=0;i<exception.Message.Length;i++)
                {
                    if (exception.Message[i] == '{')
                        tmpstr = exception.Message.Substring(i);
                }
                JavaScriptObject jsonobj= JavaScriptConvert.DeserializeObject<JavaScriptObject>(tmpstr);
                zhuangtai.GetComponent<Text>().text = "登录失败, 失败原因为： " + jsonobj["error"];
                return;
            }

            print("登录成功, @" + resp.username + "$[" + resp.sessionToken + "]");
            zhuangtai.GetComponent<Text>().text = "登录成功，欢迎" + resp.username+"\n请点击进入";
            print("登录成功, 当前用户对象Session： " + BmobUser.CurrentUser.sessionToken);
            GameObject startbutton = canvas.transform.Find("Panel/StartButton").gameObject;
            startbutton.SetActive(true);
        });
    }
    public void Startgame()
    {
        SceneManager.LoadScene("Scenes/XuanzeMoshi");

    }
    public void ResetPassword()
    {
        GameObject zhuangtai = GameObject.Find("Panel/ZhuangtaiText");
        InputField email = GameObject.Find("Panel/Email").GetComponent<InputField>();
        string tmpstr = "";
        Bmob.Reset(email.text, (resp, exception) => {
            if (exception != null)
            {

                print("重置密码请求失败, 失败原因为： " + exception.Message);
                for (int i = 0; i < exception.Message.Length; i++)
                {
                    if (exception.Message[i] == '{')
                        tmpstr = exception.Message.Substring(i);
                }
                JavaScriptObject jsonobj = JavaScriptConvert.DeserializeObject<JavaScriptObject>(tmpstr);
                zhuangtai.GetComponent<Text>().text = "重置密码请求失败, 失败原因为： " + jsonobj["error"];
                return;
            }

            print("重置密码请求发送成功！");
            zhuangtai.GetComponent<Text>().text = "重置密码请求发送成功！";
        });
    }
    public void FindUser(string username)
    {
        BmobQuery query = new BmobQuery();
        query.WhereEqualTo("username", username);
        Bmob.Find<MyBmobUser>(BmobUser.TABLE, query, (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }

            List<MyBmobUser> list = resp.results;
            foreach (var user in list)
            {
                print("获取的对象为： " + user.ToString());
                PublicData.playername = username;
                PublicData.gamenum = user.gamenum.Get();
                PublicData.winnum = user.winnum.Get();
                if (PublicData.gamenum != 0)
                    PublicData.winrate = (PublicData.winnum * 1.0 / PublicData.gamenum * 100);
                else
                    PublicData.winrate = 0;
                print(PublicData.gamenum + " " + PublicData.winnum + " " + PublicData.winrate);
            }
        });
    }

    public class BmobGameObject : BmobTable
    {
        private String fTable;
        public String playername { get; set; }
        public String password { get; set; }
        //构造函数
        public BmobGameObject() { }

        //构造函数
        public BmobGameObject(String tableName)
        {
            this.fTable = tableName;
        }

        public override string table
        {
            get
            {
                if (fTable != null)
                {
                    return fTable;
                }
                return base.table;
            }
        }
        public override void readFields(BmobInput input)
        {
            base.readFields(input);

            this.password = input.getString("password");
            this.playername = input.getString("playername");
        }

        public override void write(BmobOutput output, Boolean all)
        {
            base.write(output, all);

            output.Put("password", this.password);
            output.Put("playername", this.playername);
        }
    }
    public void create()
    {
        var data = new BmobGameObject();
        InputField playername = GameObject.Find("Panel/PlayerName").GetComponent<InputField>();
        InputField password = GameObject.Find("Panel/Password").GetComponent<InputField>();
        data.playername = playername.text;
        data.password = playername.text;

        Bmob.Create(BmobUser.TABLE, data, (resp, exception) =>
        {
            if (exception != null)
            {
                print("保存失败, 失败原因为： " + exception.Message);
                return;
            }

            print("保存成功, @" + resp.createdAt);
        });
    }

    


    /*    //对应要操作的数据表
        public const string TABLE_NAME = "UserTable";
        //接下来要操作的数据的数据

        private BmobUnity bmobUnity;
        // Use this for initialization
        void Start () {
            bmobUnity = gameObject.GetComponent<BmobUnity>();
            BmobDebug.Register(print);
        }
        public void ChangeToScene(string sceneToChangeTo)
        {
            BmobQuery query = new BmobQuery(); 
            var usertable = new UserTable();
            InputField playername = GameObject.Find("Panel/PlayerName").GetComponent<InputField>();
            InputField password = GameObject.Find("Panel/Password").GetComponent<InputField>();
            if(playername.text==null||password.text==null)
            {
                print("用户名和密码不能为空");
            }
            usertable.playername = playername.text;
            usertable.password = password.text;

            bmobUnity.Create(TABLE_NAME, usertable, (resp, exception) =>
            {
                if (exception != null)
                {
                    print("保存失败, 失败原因为： " + exception.Message);
                    return;
                }
                print("保存成功, @" + resp.createdAt);
            });
            SceneManager.LoadScene(sceneToChangeTo);
        }
        // Update is called once per frame
        void Update () {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }*/
}
