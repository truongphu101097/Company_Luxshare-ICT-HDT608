using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MerryTestFramework.testitem.Computer;
using MerryTestFramework.testitem.Headset;
using System.IO;
using System.Reflection;
using System.Threading;

namespace MerryDllFramework
{
    public partial class View : Form
    {
        readonly internal string _type = "HDT608";
        private ReadFile rf = new ReadFile();
        public string i;
        public View()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }
        private void View_Load(object sender, EventArgs e)
        {
            view();  
        }
        internal Dictionary<string, string> configdic = new Dictionary<string, string>();
        internal string TXFW = null;
        internal string RXFW = null;
        internal string TXPID = null;
        internal string TXVID = null;
        internal string RXPID = null;
        internal string RXVID = null;
        private void view()
        {
            if (i == "2")
            {
                configdic = rf.GetDicDataAsync($"./TestItem/{_type}/Config.txt", '=').Result;
            }
            if (i == "1")
            {
                configdic = rf.GetDicDataAsync($"./TestItem/{_type}/Config2.txt", '=').Result;
            }
            TXPID = configdic["TXPID"];
            TXVID = configdic["TXVID"];
            RXPID = configdic["RXPID"];
            RXVID = configdic["RXVID"];
            TXFW = configdic["TXFW"];
            RXFW = configdic["RXFW"];
            if (i == "1")
            {
                label1.Text = $"机型：HDT608_011_012\nTXPidVid:{TXPID}{TXVID}\nRXPidVid:{RXPID}{RXVID}\nDongleName: HyperX Cloud Flight Wireless\n HeadsetName:HyperX Cloud Flight Wireless\nFW_Dongle:{TXFW} and FW_Headset:{RXFW}";


            }
            if (i == "2")
            {
                label1.Text =$"机型：HDT608_017_018\nTXPidVid:{TXPID}{TXVID}\nRXPidVid:{RXPID}{RXVID}\nDongleName: HyperX Cloud Flight for PS\n HeadsetName:HyperX Cloud Flight for PS\nFW_Dongle:{TXFW} and FW_Headset:{RXFW}";
            }
        }
    }
}
