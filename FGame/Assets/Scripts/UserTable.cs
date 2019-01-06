using cn.bmob.io;
using cn.bmob.api;
using cn.bmob.json;
using cn.bmob.tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    //Game表对应的模型类
public class UserTable : BmobTable
    {

        private String fTable;
        //以下对应云端字段名称
        public String playername { get; set; }
        public String password { get; set; }

        //构造函数
        public UserTable() { }

        //构造函数
        public UserTable(String tableName)
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

        //读字段信息
        public override void readFields(BmobInput input)
        {
            base.readFields(input);

            this.password = input.getString("password");
            this.playername = input.getString("playername");
        }

        //写字段信息
        public override void write(BmobOutput output, bool all)
        {
            base.write(output, all);

            output.Put("password", this.password);
            output.Put("playername", this.playername);
        }
    }
