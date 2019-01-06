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

        Bmob.Create(TABLENAME, data, (resp, exception) =>
        {
            if (exception != null)
            {
                print("保存失败, 失败原因为： " + exception.Message);
                return;
            }

            print("保存成功, @" + resp.createdAt);
        });
    }
    public void Signup()
    {
        BmobUser user = new BmobUser();
        InputField playername = GameObject.Find("Panel/PlayerName").GetComponent<InputField>();
        InputField password = GameObject.Find("Panel/Password").GetComponent<InputField>();
        InputField email = GameObject.Find("Panel/Email").GetComponent<InputField>();
        user.username = playername.text;
        user.password = password.text;
        user.email = email.text;

        Bmob.Signup<BmobUser>(user, (resp, exception) =>
        {
            if (exception != null)
            {
                print("注册失败, 失败原因为： " + exception.Message);
                return;
            }

            print("注册成功");
            
        });
    }
    public void Login()
    {
        InputField playername = GameObject.Find("Panel/PlayerName").GetComponent<InputField>();
        InputField password = GameObject.Find("Panel/Password").GetComponent<InputField>();
        Bmob.Login<BmobUser>(playername.text, password.text, (resp, exception) => {
            if (exception != null)
            {
                print("登录失败, 失败原因为： " + exception.Message);
                return;
            }

            print("登录成功, @" + resp.username + "$[" + resp.sessionToken + "]");

            print("登录成功, 当前用户对象Session： " + BmobUser.CurrentUser.sessionToken);
            SceneManager.LoadScene("Scenes/XuanzeMoshi");
        });
    }
    void FindUser()
    {
        BmobQuery query = new BmobQuery();
        query.WhereEqualTo("username", "test");
        Bmob.Find<BmobUser>(BmobUser.TABLE, query, (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }

            List<BmobUser> list = resp.results;
            foreach (var user in list)
            {
                print("获取的对象为： " + user.ToString());
            }
        });
    }
    public void ResetPassword()
    {
        InputField email = GameObject.Find("Panel/Email").GetComponent<InputField>();
        Bmob.Reset(email.text, (resp, exception) => {
            if (exception != null)
            {
                print("重置密码请求失败, 失败原因为： " + exception.Message);
                return;
            }

            print("重置密码请求发送成功！");
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
