﻿using MerryTestFramework.testitem.Computer;
using MerryTestFramework.testitem.Headset;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MerryDllFramework
{
    /// <summary>
    /// 参数存放类
    /// </summary>
    internal class Data
    {
        Selected Selected;
        /// <summary>
        /// 必要，标识当前机型名
        /// </summary>
        readonly internal string _type = "HDT608";
        readonly internal string _dllpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private ReadFile rf = new ReadFile();
        private GetHandle gh = new GetHandle();
        bool flag = false;
        string value;
        private void ShowInfo()
        {
            while (true)
            {
                if (flag)
                {
                    flag = false;
                    View View = new View();
                    View.i =value;
                    View.ShowDialog();
                }
                Thread.Sleep(1000);
            }
        }
        internal Data()
        {
            Selected = new Selected();
            Selected.ShowDialog();
            value = Selected.i;
            Thread t = new Thread(ShowInfo);
            t.Start();
            flag = true;
            //加载Message以及Config文件
            messagedic = rf.GetDicDataAsync($"./TestItem/{_type}/Message.txt", '=').Result;
            if (Selected.i == "2")
            {
                configdic = rf.GetDicDataAsync($"./TestItem/{_type}/Config.txt", '=').Result;
            }
            if (Selected.i == "1")
            {
                configdic = rf.GetDicDataAsync($"./TestItem/{_type}/Config2.txt", '=').Result;
            }
            //加载设备PIDVID
            TXPID = configdic["TXPID"];
            TXVID = configdic["TXVID"];
            RXPID = configdic["RXPID"];
            RXVID = configdic["RXVID"];
            TXFW = configdic["TXFW"];
            RXFW = configdic["RXFW"];
            DongleButtonTest = messagedic["DongleButtonTest"];
            MuteButtonTest = messagedic["MuteButtonTest"];
            LeftLED = messagedic["LeftLED"];
            RightLED = messagedic["RightLED"];
            MuteLED = messagedic["MuteLED"];
            DongleLED = messagedic["DongleLED"];
            SideTone = messagedic["SideTone"];
           
        }
        internal string TXFW = null;
        internal string RXFW = null;
        internal string TXPID = null;
        internal string TXVID = null;
        internal string RXPID = null;
        internal string RXVID = null;

        internal string DongleButtonTest = "";
        internal string MuteButtonTest = "";
        internal string LeftLED = "";
        internal string RightLED = "";
        internal string MuteLED = "";
        internal string SideTone = "";
        internal string DongleLED = "";

        internal IntPtr headsethandle1 = IntPtr.Zero;
        internal IntPtr headsethandle2 = IntPtr.Zero;
        internal IntPtr headsethandle3 = IntPtr.Zero;
        internal IntPtr headsethandle4 = IntPtr.Zero;

        internal string headsetpath1 = null;
        internal string headsetpath2 = null;
        internal string headsetpath3 = null;
        internal string headsetpath4 = null;

        internal IntPtr donglehandel1 = IntPtr.Zero;
        internal IntPtr donglehandel2 = IntPtr.Zero;
        internal IntPtr donglehandel3 = IntPtr.Zero;
        internal IntPtr donglehandel4 = IntPtr.Zero;

        internal string donglepath1 = null;
        internal string donglepath2 = null;
        internal string donglepath3 = null;
        internal string donglepath4 = null;
        /// <summary>
        /// 存储message文本字典
        /// </summary>
        internal Dictionary<string, string> messagedic = new Dictionary<string, string>();
        /// <summary>
        /// 存储config文本字典
        /// </summary>
        internal Dictionary<string, string> configdic = new Dictionary<string, string>();
        /// <summary>
        /// 存储主程序传送参数
        /// </summary>
        internal List<string> formsData = new List<string>();
        /// <summary>
        /// 存储主程序主窗体句柄
        /// </summary>
        internal IntPtr handle = IntPtr.Zero;
        /// <summary>
        /// 关闭Handel(防止某些耳机PIDVID会改变)
        /// </summary>
        internal void CloseHandel()
        {
            gh.CloseHandle();

            headsethandle1 = gh.headsethandle[0];
            headsethandle2 = gh.headsethandle[1];
            headsethandle3 = gh.headsethandle[2];
            headsethandle4 = gh.headsethandle[3];

            headsetpath1 = gh.headsetpath[0];
            headsetpath2 = gh.headsetpath[1];
            headsetpath3 = gh.headsetpath[2];
            headsetpath4 = gh.headsetpath[3];

            donglehandel1 = gh.donglehandle[0];
            donglehandel2 = gh.donglehandle[1];
            donglehandel3 = gh.donglehandle[2];
            donglehandel4 = gh.donglehandle[3];

            donglepath1 = gh.donglepath[0];
            donglepath2 = gh.donglepath[1];
            donglepath3 = gh.donglepath[2];
            donglepath4 = gh.donglepath[3];
        }
        /// <summary>
        /// 打开Handel(防止某些耳机PIDVID会改变)
        /// </summary>
        internal void OpenHandel(string RXPID, string RXVID, string TXPID, string TXVID)
        {
            gh.gethandle(RXPID, RXVID, TXPID, TXVID);

            headsethandle1 = gh.headsethandle[0];
            headsethandle2 = gh.headsethandle[1];
            headsethandle3 = gh.headsethandle[2];
            headsethandle4 = gh.headsethandle[3];

            headsetpath1 = gh.headsetpath[0];
            headsetpath2 = gh.headsetpath[1];
            headsetpath3 = gh.headsetpath[2];
            headsetpath4 = gh.headsetpath[3];

            donglehandel1 = gh.donglehandle[0];
            donglehandel2 = gh.donglehandle[1];
            donglehandel3 = gh.donglehandle[2];
            donglehandel4 = gh.donglehandle[3];

            donglepath1 = gh.donglepath[0];
            donglepath2 = gh.donglepath[1];
            donglepath3 = gh.donglepath[2];
            donglepath4 = gh.donglepath[3];
        }
    }
}
