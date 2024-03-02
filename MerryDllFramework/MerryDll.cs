using HDT668_HID_CMD;
using MerryKing;
using MerryTest.testitem;
using MESDLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WindowsFormsApplication1;
using MerryTestFramework.testitem.Headset;
using MerryTestFramework.testitem.Computer;
using NAudio.CoreAudioApi;
using BlueNinjaSoftware.HIDLib;
namespace MerryDllFramework
{
    
    public class MerryDll : IMerryDll
    {
 
        // 天线通道（经示波器调测，通道要加1才能正确显示）
        public static string CH1 = "1";  // 原值为 "1"
        public static string CH20 = "20"; // 原值为 "20"
        public static string CH38 = "38"; // 原值为 "38"
        // 天线
        public static string ANT1 = "0"; // 天线1
        public static string ANT2 = "1"; // 天线2      
        readonly VolumeTestCustom VolumeTestPlan = new VolumeTestCustom();

        Dictionary<string, object> Config;
        public object Interface(Dictionary<string, object> keyValues) => Config = keyValues;

        #region 通用DLL类实例化区 ：实例化MerryTestFramework.testitem.dll中帮助类
        public Command Command = new Command();
        public GetHandle Gethandle = new GetHandle();
        private MerryTestFramework.testitem.Headset.OldButtonTest OldButton = new MerryTestFramework.testitem.Headset.OldButtonTest();
        Data data = new Data();

        #endregion

        /// <summary>
        /// 必要，因为反射不会自动创建构造函数
        /// </summary>
        public MerryDll()
        {

        }
        public string[] GetDllInfo()
        {
            string dllname = "DLL 名称        ：HDT608";
            string dllfunction = "Dll功能说明 ：HDT608机型测试模块";
            string dllHistoryVersion = "历史Dll版本：0.0.0.1";
            string dllVersion = "当前Dll版本：23.11.16.0";
            string[] info = { dllname,
                dllfunction,
                dllHistoryVersion,
                dllVersion};
            return info;
        }

      
        #region 主程序调用方法区
        public bool CheckType(string type)
        {
            return type == data._type;
        }

        public string GetMessage(string key)
        {
            return data.messagedic[key];
        }
        public bool StartRun()
        {
            data.formsData.Add("");
            return true;
        }
        bool flag = false;
        int i;
        public bool Start(List<string> formsData, IntPtr _handle)
        {
            data.handle = _handle;
            data.formsData = formsData;
            data.formsData.Add("");
            return true;     
        }
      
        public string Run(string message)
        {
            try
            {
                data.CloseHandel();
                data.OpenHandel(data.RXPID, data.RXVID, data.TXPID, data.TXVID);          
                Info.HeadsetHandle = data.headsethandle1;
                Info.DongleHandle = data.donglehandel3;
                Info.HeadsetPath = data.headsetpath1;
                Info.DonglePath = data.donglepath3;

                Thread.Sleep(200);
                string[] str = message.Split('-');
                Info.TpTestname = str[0];
                switch (str[0])
                {
                    case "GetTXPidVid": return GetTXPidVid().ToString();
                    case "GetRXPidVid": return GetRXPidVid().ToString();
                    case "GetDeviceName": return GetDeviceName().ToString();

                    case "GetDongleFW": return GetDongleFW().ToString();
                    case "GetDongleID": return GetDongleID().ToString();
                    case "GetHeadsetFW": return GetHeadsetFW().ToString();
                    case "Pair": return Pair().ToString();
                    case "GetHeadsetID": return GetHeadsetID().ToString();
                    
                    
                    case "GetBatteryVoltage": return GetBatteryVoltage().ToString();
                    case "ResetHeadset": return ResetHeadset().ToString();
                    case "PowerOn": return PowerOn().ToString();
                    case "PowerOff": return PowerOff().ToString();

                    case "Delay5000": return Delay5000().ToString();
                    case "CallDll": return CallDll();

                    case "Pair2": return Pair2().ToString();
                    case "SideTone": return SideTone().ToString();
                    //LED
                    case "LeftLED": return TestPowerLeftLED().ToString();
                    case "RightLED": return TestPowerRightLED().ToString();
                    case "MuteLED": return TestMuteLed1().ToString();
                    case "DongleLED": return DongleLED().ToString();

                    //ButtonTest
                    case "MuteButtonTest": return MuteButtonTest();
                    case "DongleButtonTest": return DongleButtonTest();

                    // 按键音量调节测试
                    //case "VolumeDown": return TestVolumeDown();
                    //case "VolumeUp": return TestVolumeUp();
                    case "VolumeDown": return VloDown().ToString();//VolUp
                    case "VolumeUp": return VolUp().ToString();//VolUp Checkmute DongleArbiterID

                    /* 耳机天线选择 ant1 ant2 */
                    /* 耳机通道选择 ch1 ch20 ch38 */
                    case "headsetAnt1Ch1": return SetRFChannel(data.RXVID, data.RXPID, ANT1, CH1).ToString();
                    case "headsetAnt1Ch20": return SetRFChannel(data.RXVID, data.RXPID, ANT1, CH20).ToString();
                    case "headsetAnt1Ch38": return SetRFChannel(data.RXVID, data.RXPID, ANT1, CH38).ToString();
                    case "headsetAnt2Ch1": return SetRFChannel(data.RXVID, data.RXPID, ANT2, CH1).ToString();
                    case "headsetAnt2Ch20": return SetRFChannel(data.RXVID, data.RXPID, ANT2, CH20).ToString();
                    case "headsetAnt2Ch38": return SetRFChannel(data.RXVID, data.RXPID, ANT2, CH38).ToString();
                    /* dongle天线选择 ant1 ant2 */
                    /* dongle通道选择 ch1 ch20 ch38 */
                    case "dongleAnt1Ch1": return SetRFChannel(data.TXVID, data.TXPID, ANT1, CH1).ToString();
                    case "dongleAnt1Ch20": return SetRFChannel(data.TXVID, data.TXPID, ANT1, CH20).ToString();
                    case "dongleAnt1Ch38": return SetRFChannel(data.TXVID, data.TXPID, ANT1, CH38).ToString();
                    case "dongleAnt2Ch1": return SetRFChannel(data.TXVID, data.TXPID, ANT2, CH1).ToString();
                    case "dongleAnt2Ch20": return SetRFChannel(data.TXVID, data.TXPID, ANT2, CH20).ToString();
                    case "dongleAnt2Ch38": return SetRFChannel(data.TXVID, data.TXPID, ANT2, CH38).ToString();

                    default: return "False";
                }
            }
            catch
            {
                return "False";
            }
        }
        #endregion

        #region 被指令调用方法区

        #region 定义结构体存储数据
        // 定义结构体存储数据
        struct Info
        {
            /// <summary>
            /// Set Report ID指令
            /// </summary>
            public static string SetCommand;

            /// <summary>
            /// Get Report ID指令
            /// </summary>
            public static string GetCommand;

            /// <summary>
            /// 存储颜色
            /// </summary>
            public static string color;

            /// <summary>
            /// 存储AB1568 FW版本
            /// </summary>
            public static string AB1568;

            /// <summary>
            /// 存储Headset Handle
            /// </summary>
            public static IntPtr HeadsetHandle;

            /// <summary>
            /// 存储Dongle Handle
            /// </summary>
            public static IntPtr DongleHandle;

            /// <summary>
            /// 存储写入的BD
            /// </summary>
            public static string BD;

            /// <summary>
            /// 存储Headset BD
            /// </summary>
            public static string HeadsetBD;

            /// <summary>
            /// 存储Dongle BD
            /// </summary>
            public static string DongleBD;


            public static string HeadsetPath;

            public static string DonglePath;

            /// <summary>
            /// 存储触摸测试下标值
            /// </summary>
            public static string index;

            /// <summary>
            /// 存储TP基础值上限
            /// </summary>
            public static string[] BasicUpper;

            /// <summary>
            /// 存储TP基础值下限
            /// </summary>
            public static string[] BasicLower;

            /// <summary>
            /// 存储TP触摸结果名称
            /// </summary>
            public static string TpTestname;

            /// <summary>
            /// 存储TP触摸结果
            /// </summary>
            public static string TpTouchValues;

            public static string[] RGB;

            public static string Lux_R;

            public static string Lux_L;

        }
        #endregion


        #region LED测试

        //private bool VloDown()
        //{
        //    return VolumeTestPlan.volumetest(false, "Vui lòng vặn giảm âm lượng\n下调音量");
        //}
        //private bool VolUp()
        //{
        //    return VolumeTestPlan.volumetest(true, "Vui lòng vặn tăng âm lượng\n上调音量");
        //}

        private bool VloDown()
        {
            return VolumeTestPlan.volumetest(false, "Vui lòng vặn giảm âm lượng\n下调音量");
        }
        private bool VolUp()
        {
            return VolumeTestPlan.volumetest(true, "Vui lòng vặn tăng âm lượng\n上调音量");
        }

        public static bool SetRFChannel(string vid, string pid, string ch, string channel)
        {
            return SCPI.avnera.Main(new string[] { vid, pid, ch, channel }) == 1;
        }


        public string OpenHandle()
        {
            data.OpenHandel(data.RXPID, data.RXVID, data.TXPID, data.TXVID);

            return true.ToString();

        }

        public string CloseHandle()
        {
            data.CloseHandel();
            return true.ToString();

        }

        public string GetPidVid()
        {
            data.OpenHandel(data.RXPID, data.RXVID, data.TXPID, data.TXVID);
            //Thread.Sleep(500);
            string pathHeadset = data.headsetpath1;

            //string fw = "";
            string[] item = pathHeadset.Split('&');

            string[] vidItem = item[0].Split('_');
            string[] pidItem = item[1].Split('_');

            string PIDVID = "P" + pidItem[1].ToUpper() + "V" + vidItem[1].ToUpper();

            data.CloseHandel();

            if (pidItem[1] == null || pidItem[1] == "" || vidItem[1] == null || vidItem[1] == "")
            {
                return false.ToString();
            }
            return PIDVID;

        }

        public string GetVersionLogi()
        {
            data.OpenHandel(data.RXPID, data.RXVID, data.TXPID, data.TXVID);
            //Thread.Sleep(500);
            string pathHeadset = data.headsetpath1;

            //string fw = "";
            string[] item = pathHeadset.Split('&', '_', 'l', '#');


            string version = string.Concat(item[8], item[6]);

            data.CloseHandel();

            if (version == null || version == "")
            {
                return false.ToString();
            }
            return version;

        }

        public Constant con = new Constant();
        public string GetHeadsetPIDVID()
        {
            IList<HIDDevice> devList = HIDManagement.GetDevices((ushort)con.Hex2Ten(data.RXVID));
            HIDDevice hidDevice = devList[0];
            string[] m = Convert.ToString(devList[0]).Split('x', ',');
            //int rever = Convert.ToInt32(hidDevice.ProductID);
            string PV = "V" + m[1] + " P" + m[3];
            //convert decimal to hex


            if (PV == null)
            {
                return false.ToString();
            }
            return PV;


        }
        public string GetDonglePIDVID()
        {
            IList<HIDDevice> devList = HIDManagement.GetDevices((ushort)con.Hex2Ten(data.TXVID));
            HIDDevice hidDevice = devList[0];
            string[] m = Convert.ToString(devList[0]).Split('x', ',');
            //int rever = Convert.ToInt32(hidDevice.ProductID);
            string PV = "V" + m[1] + " P" + m[3];
            //convert decimal to hex


            if (PV == null)
            {
                return false.ToString();
            }
            return PV;


        }

        private string GetTXPidVid()
        {
            return data.donglehandel1 != IntPtr.Zero
                ? $"P{data.TXPID}V{data.TXVID}"
                : "False";
        }

        private string GetRXPidVid()
        {
            return data.headsethandle1 != IntPtr.Zero
                ? $"P{data.RXPID}V{data.RXVID}"
                : "False";
        }

        public string GetDeviceName()
        {
            bool flag = false;
            if (data.TXPID== "1723")
            {
                IList<HIDDevice> devList = HIDManagement.GetDevices((ushort)con.Hex2Ten(data.RXVID));
                HIDDevice hidDevice = devList[0];
                string nameItem = hidDevice.ProductName;
                devList = HIDManagement.GetDevices((ushort)con.Hex2Ten(data.RXVID), (ushort)con.Hex2Ten(data.RXPID), false);

                if (nameItem == null || nameItem == "")
                {
                    return false.ToString();
                }

                else
                {
                    if (nameItem == "HyperX Cloud Flight Wireless") return true.ToString() + nameItem;
                }
            }
            if (data.TXPID == "0C8C")
            {
                IList<HIDDevice> devList = HIDManagement.GetDevices((ushort)con.Hex2Ten(data.RXVID));
                HIDDevice hidDevice = devList[0];
                string nameItem = hidDevice.ProductName;
                devList = HIDManagement.GetDevices((ushort)con.Hex2Ten(data.RXVID), (ushort)con.Hex2Ten(data.RXPID), false);

                if (nameItem == null || nameItem == "")
                {
                    return false.ToString();
                }

                else
                {

                    if (nameItem == "HyperX Cloud Flight for PS") return true.ToString() + nameItem;

                }
            }
            return false.ToString();

        }

        public string GetCheck()
        {
            return true.ToString();
        }
       
        public string IdPair = "";
        public string GetDongleID()
        {

            bool flag = false;
            string reValue = null;
            var value = "FF 0A 00 FD 04 00 00 05 81 D9 F3 04";
            var valueSaveID = "FF 05 00 39";
            var index = "11 12 13 14";
            var returnvalue = "FF 0F 05 FE 00 04 0F 08 81 D9 F3";

            if(Command.SetFeatureSend(value, 64, data.donglehandel4))
            {
                if(Command.GetFeatureReturn(value, 64, data.donglehandel4, index))
                {
                    flag = true;
                    IdPair = Command.ReturnValue;
                    return IdPair;
                }
                else
                {
                    flag = false;
                }
            }
            return false.ToString();

        }

        public string GetHeadsetID()
        {
            bool flag = false;
            string reValue = null;
            var value = "FF 0A 00 FD 04 00 00 05 81 D9 F3 04";
            var valueSaveID = "FF 05 00 39";
            var index = "11 12 13 14";
            var returnvalue = "FF 0A 00 FD 04 00 00 05 81 D9 F3";

            if (Command.SetFeatureSend(value, 64, data.headsethandle2))
            {
                if (Command.GetFeatureReturn(value, 64, data.headsethandle2, index))
                {
                    flag = true;
                    var ID = Command.ReturnValue;
                    return ID;
                }
                else
                {
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }

            if (flag)
            {
                string[] concatValue = Command.ReturnValue.Split(' ');
                for (int i = 0; i < concatValue.Length; i++)
                {
                    reValue = string.Concat(reValue, concatValue[i]);
                }

                return reValue;
            }

            return false.ToString();

        }

        public string Pair()
        {
            bool flag = false;
            bool flagResult = false;
            string dongleID = null;
            string headsetID = null;
            var reValue = "";
            var valueDongleID = "FF 0A 00 FD 04 00 00 05 81 D9 F3 04";
            var valueHeadsetID = "FF 0A 00 FD 04 00 00 05 81 D9 F3 04";
            var valueSaveID = "FF 05 00 39";
            var value = "";
            var indexDongleID = "11 12 13 14";
            var indexHeadsetID = "11 12 13 14";

            //save Dongle ID after power cycle
            if (Command.SetFeatureSend(valueSaveID, 64, data.donglehandel4))
            {
                //check Dongle ID
                if (Command.SetFeatureSend(valueDongleID, 64, data.donglehandel4))
                {
                    if (Command.GetFeatureReturn(valueDongleID, 64, data.donglehandel4, indexDongleID))
                    {
                        dongleID = Command.ReturnValue;
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }

                }
                else
                {
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }

            //send Dongle ID to Headset for pair
            if (flag)
            {
                dongleID = Command.ReturnValue;
                value = String.Concat("07 88 01 ", dongleID);
                if(Command.WriteSend(value, 20, data.headsethandle1))
                {
                    flagResult = true;
                }
                else
                {
                    flagResult = false;
                }

            }
            else
            {
                return false.ToString();
            }

            //check HeadsetID
            if (flagResult)
            {
                if (Command.SetFeatureSend(valueHeadsetID, 64, data.headsethandle2))
                {
                    if (Command.GetFeatureReturn(valueHeadsetID, 64, data.headsethandle2, indexHeadsetID))
                    {
                        headsetID = Command.ReturnValue;
                        if (headsetID == dongleID)
                        {
                            flagResult = true;
                        }
                        else
                        {
                            flagResult = false;
                        }

                    }
                    else
                    {
                        flagResult = false;
                    }

                }
            }

            if (flagResult)
            {
                return true.ToString();
            }

            return false.ToString();

        }

        public string Pair2()
        {
            var value = "07 88 01" + " " + IdPair;
            if (IdPair != null) 
            {
                if (Command.WriteSend(value, 20, data.headsethandle1))
                    return true.ToString();
            }
            return false.ToString();
        }

        public string GetDongleFW()
        {
            var value = "21 FF 04";
            var returnvalue = "21 FF 04";
            var indexs = "3 4 5 6";
            if (Command.WriteSend(value, 20, data.donglehandel3))
            {
                if (Command.WriteReturn(value, 20, returnvalue, indexs, data.donglepath3, data.donglehandel3))
                {
                    var fw = "";
                    string[] item = Command.ReturnValue.Split(' ');
                    foreach (string i in item)
                    {
                        var a = Convert.ToInt32(i).ToString();
                        fw += a + ".";
                    }
                    fw = "V" + fw.Remove(fw.Length - 1, 1);
                    //fw += $"V3.1.{Convert.ToInt32(item[0], 16)}.{Convert.ToInt32(item[1], 16)}.{Convert.ToInt32(item[2], 16)}";

                    //Console.WriteLine($"{fw}");
                    if (fw == data.TXFW) return true.ToString();
                    return false + fw;
                }
            }
            return false.ToString();
        }

       

        public string GetHeadsetFW()
        {
            var value = "07 88 04";
            var returnvalue = "07 88 04";
            var indexs = "3 4 5 6";
            if (Command.WriteSend(value, 20, data.headsethandle1))
            {
                if (Command.WriteReturn(value, 20, returnvalue, indexs, data.headsetpath1, data.headsethandle1))
                {
                    var fw = "";
                    string[] item = Command.ReturnValue.Split(' ');
                    foreach (var i in item)
                    {
                        fw += $"{Convert.ToInt32(i, 16)}" + ".";
                    }
                    fw = "V" + fw.Remove(fw.Length - 1, 1);
                    //fw += $"V3.1.{Convert.ToInt32(item[0], 16)}.{Convert.ToInt32(item[1], 16)}.{Convert.ToInt32(item[2], 16)}";

                    if (fw == data.RXFW) return true.ToString();
                    return false + fw;
                }
            }
            return false.ToString();

        }

        public string MuteButtonTest()
        {
            bool flag = false;
            var valueTestModeOn = "07 88 02";
            var revalue= "aa";
            var indexs = "3";
            if (Command.WriteSend(valueTestModeOn, 20, data.headsethandle1))
            {
                if (OldButton.Buttontest(revalue, indexs, data.MuteButtonTest, Info.HeadsetPath))
                {
                    flag = true;
                }
                string a = revalue;
            }
            else
            {
                flag = false;
            }

            if (!flag)
            {
                return false.ToString();
            }
            return true.ToString();
        }

        public string DongleButtonTest()
        {
            var value = "21 FF 02 01";
            
            if (Command.WriteSend(value, 20, data.donglehandel3))
            {
                Messages messages = new Messages(data.DongleButtonTest, 100);
                messages.ShowDialog();
                if(messages.DialogResult == DialogResult.Yes)
                {
                    return true.ToString();
                }
              
            }
            
            return false.ToString();
        }


        public string ResetHeadset()
        {
            var value = "07 88 07";

            PowerOn();

            if (!Command.WriteSend(value, 20, data.headsethandle1))
            {
                return false.ToString();
            }
            return true.ToString();
            

        }

        public string PowerOn()
        {
            var value = "FF 00 00 70 01";


            if (!Command.SetFeatureSend(value, 64, data.headsethandle2))
            {
                return false.ToString();
            }
            return true.ToString();


        }

        public string PowerOff()
        {
            var value = "FF 00 00 70 00";


            if (!Command.SetFeatureSend(value, 64, data.headsethandle2))
            {
                return false.ToString();
            }
            return true.ToString();


        }

        public string GetBatteryVoltage()
        {
            bool flag = false;
            var value = "21 FF 05";
            var indexs = "3 4";
            var returnvalue = "21 FF 05";

            var batteryVol = "";

            if(Command.WriteReturn(value, 20, returnvalue, indexs, data.donglepath3, data.donglehandel3))
            {
                foreach(var item in Command.ReturnValue.Split(' '))
                {
                    flag = true;
                    batteryVol = String.Concat(batteryVol, item);
                    
                }
            }
            else
            {
                flag = false;
            }


            if (!flag)
            {
                return false.ToString();
            }
            return batteryVol = Convert.ToString(Convert.ToDouble(Convert.ToInt32(batteryVol, 16)) / 1000.000); 

        }

        public string SwitchDongletoAvnera()
        {
            bool flag = false;
            var value = "07 88 06 01";

            PowerOn();

            if (!Command.WriteSend(value, 20, data.headsethandle1))
            {
                Thread.Sleep(500);
                flag = false;
            }
            else
            {
                flag = true;
            }

            Thread.Sleep(5000);
            if (flag)
            {
                Gethandle.CloseHandle();
                if(Gethandle.gethandle(data.RXPID, data.RXVID, data.TXPID, data.TXVID)){
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }

            if (!flag)
            {
                return false.ToString();
            }          
            return true.ToString();

        }
        public string Delay5000()
        {
            Thread.Sleep(5000);
            return true.ToString();
        }

        public string CallDll()
        {
            return true.ToString();
        }

        public string MusicLRTest()
        {
            return plays.MusicPlayLR();
        }

        public string Music503kTest()
        {
            return plays.MusicPlay503k();
        }

        public string RecordTest()
        {
            return plays.RecordPlayTest();
        }

        //last program
        public string TestPowerLeftLED()
        {
            var valueOn = "07 88 03 02 01";
            var valueOff = "07 88 03 02 00";

            Command.WriteSend(valueOn, 20, Info.HeadsetHandle);

            //Command.WriteSend("07 88 03 02 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00", 20, Info.HeadsetHandle);
            Messages messages = new Messages(data.LeftLED, 100);
            messages.ShowDialog();
            if (messages.DialogResult == DialogResult.Yes)
            {
                Command.WriteSend(valueOff, 20, Info.HeadsetHandle);
                return true.ToString();
            }
            //if (MessageBox.Show(data.LeftLED, "Notification/Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    Command.WriteSend(valueOff, 20, Info.HeadsetHandle);
            //    return true.ToString();              
            //}

            return false.ToString();

        }

        public string TestPowerRightLED()
        {
            var valueOn = "07 88 03 01 01";
            var valueOff = "07 88 03 01 00";

            if (Command.WriteSend(valueOn, 20, Info.HeadsetHandle))
            {
                Messages messages = new Messages(data.RightLED, 100);
                messages.ShowDialog();
                if (messages.DialogResult == DialogResult.Yes)
                {
                    if (Command.WriteSend(valueOff, 20, Info.HeadsetHandle))
                        return true.ToString();
                }
            }
            return false.ToString();
        }

        public string TestMuteLed1()
        {
            var valueOn = "07 88 03 03 01";
            var valueOff = "07 88 03 03 00";

            if(Command.WriteSend(valueOn, 20, Info.HeadsetHandle))
            {
                Messages messages = new Messages(data.MuteLED, 100);
                messages.ShowDialog();
                if (messages.DialogResult == DialogResult.Yes)
                {
                    if(Command.WriteSend(valueOff, 20, Info.HeadsetHandle))
                    return true.ToString();
                }
            }

            //Command.WriteSend("07 88 03 02 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00", 20, Info.HeadsetHandle);
            
            return false.ToString();

        }

        public string SideTone()
        {
            var valueOn = "07 88 05 01";
            var valueOff = "07 88 05 00";

            Command.WriteSend(valueOn, 20, Info.HeadsetHandle);

            if (Command.WriteSend(valueOn, 20, Info.HeadsetHandle))
            {
                Messages messages = new Messages(data.SideTone, 100);
                messages.ShowDialog();
                if (messages.DialogResult == DialogResult.Yes)
                {
                    if (Command.WriteSend(valueOff, 20, Info.HeadsetHandle))
                        return true.ToString();
                }
            }

            return false.ToString();

        }

        public string TestMuteButton()
        {
            var value = "07 88 02 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
            if(Command.WriteSend(value, 20, Info.HeadsetHandle))
            {
                var redata = "aa";
                var index = "3";
                return OldButton.Buttontest(redata, index, data.MuteButtonTest, Info.HeadsetPath).ToString();

            }

            return false.ToString();
        }

        public string DongleLED()
        {
            var valueOn = "21 FF 03 01";
            var valueOff = "21 FF 03 00";

            Command.WriteSend(valueOn, 20, data.donglehandel3);

            if (Command.WriteSend(valueOn, 20, Info.HeadsetHandle))
            {
                Messages messages = new Messages(data.DongleLED, 100);
                messages.ShowDialog();
                if (messages.DialogResult == DialogResult.Yes)
                {
                    if (Command.WriteSend(valueOff, 20, Info.HeadsetHandle))
                        return true.ToString();
                }
            }
            return false.ToString();

        }

        public string CheckFWVersion()
        {
            var valueGetFWVer = "07 88 04";
            var indexs = "4 5 6 7";
            var returnvalueGetFWVer = "07 88 04 03";

            Command.WriteReturn(valueGetFWVer, 20, returnvalueGetFWVer, indexs, Info.HeadsetPath, Info.HeadsetHandle);

            var fw = "V";

            foreach (var item in Command.ReturnValue.Split(' '))
            {
                fw += $"{Convert.ToInt32(item, 16)}.";
            }

            if ((fw.Remove(fw.Length - 1, 1)).ToString() == "V1.0.1.0")
            {
                //MessageBox.Show((fw.Remove(fw.Length - 1, 1)).ToString());
                Command.GetReportSend(valueGetFWVer, 20, Info.HeadsetHandle);
                return true.ToString();
            }
            else
            {
                //MessageBox.Show("Khong dung Version");
                Command.GetReportSend(valueGetFWVer, 20, Info.HeadsetHandle);
                return false.ToString();
            }

        }

        public string CheckDongleVersion()
        {
            var valueGetDgVer = "06 00 02 00 9A 00 00 68 4A 8E 0A 00 00 00 BB 11";
            var indexs = "4 5 6 7 8";
            var returnvalueGetDgVer = "0B 00 BB 11";

            Command.WriteReturn(valueGetDgVer, 62, returnvalueGetDgVer, indexs, Info.DonglePath, Info.DongleHandle);

            var fw = "V";

            foreach (var item in Command.ReturnValue.Split(' '))
            {
                fw += $"{Convert.ToInt32(item, 16)}.";
            }

            if ((fw.Remove(fw.Length - 1, 1)).ToString() == "V4.1.0.1.0")
            {
                //MessageBox.Show((fw.Remove(fw.Length - 1, 1)).ToString());
                Command.GetReportSend(valueGetDgVer, 62, Info.DongleHandle);
                return true.ToString();
            }
            else
            {
                //MessageBox.Show("Khong dung Version");
                Command.GetReportSend(valueGetDgVer, 62, Info.DongleHandle);
                return false.ToString();
            }

        }

        


        #endregion


        #region 进度条
        // 弹窗调用的dll
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(
            IntPtr hWnd,        // 信息发往的窗口的句柄
            int Msg,            // 消息ID
            int wParam,         // 参数1
            int lParam            // 参数2
        );
        static ProgressBars msgbox;
        /// <summary>
        /// 进度条
        /// </summary>
        /// <param name="name">进度条显示的内容</param>
        /// <param name="time">进度条每一格的间隔时间</param>
        /// <returns></returns>
        private bool MessgBox(string name, int time)
        {
            try
            {
                msgbox = new ProgressBars(name, true, time);
                if (msgbox.ShowDialog() == DialogResult.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// 结束进度条
        /// </summary>
        public void Stop()
        {
            IntPtr ptr = FindWindow(null, "aaa");
            if (ptr != IntPtr.Zero)
                Thread.Sleep(50);
            msgbox.DialogResult = DialogResult.OK;
            Thread.Sleep(50);
            PostMessage(ptr, 0x10, 0, 0);
            return;
        }
        #endregion

        #region 读取ini
        // 读ini文件的dll
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpszReturnBuffer, int nSize, string lpFileName);

        // 根据传入的节点读取该节点下的所有内容
        public static Dictionary<string, string> GetKeys(string iniFile, string category)
        {
            byte[] buffer = new byte[2048];
            GetPrivateProfileSection(category, buffer, 2048, iniFile);
            String[] tmp = Encoding.Default.GetString(buffer).Trim('\0').Split('\0');
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (String entry in tmp)
            {
                string[] v = entry.Split('=');
                result.Add(v[0], v[1]);
            }
            return result;
        }

        /// <summary>
        /// 读取内容并进行处理
        /// </summary>
        /// <param name="data">读取的节点名称</param>
        /// <returns></returns>
        public static string Read(string data)
        {
            try
            {
                string value = "";
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\TP test upper and lower limits.ini"; // 获取读取文件的路径
                Dictionary<string, string> content = GetKeys(path, data); // 根据传入的节点在该文件中读取
                foreach (var item in content)
                {
                    value += item.Value + ",";
                }
                return value;
            }
            catch (Exception err)
            {
                return "False";
            }
        }
        #endregion

        #endregion
    }

    public class Constant
    {

        /// <summary>
        /// 十进制转换
        /// </summary>
        /// <param name="hexChar"></param>
        /// <returns></returns>
        /// 


        public static int HexChar2Value(string hexChar)
        {
            switch (hexChar)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    return Convert.ToInt32(hexChar);
                case "a":
                case "A":
                    return 10;
                case "b":
                case "B":
                    return 11;
                case "c":
                case "C":
                    return 12;
                case "d":
                case "D":
                    return 13;
                case "e":
                case "E":
                    return 14;
                case "f":
                case "F":
                    return 15;
                default:
                    return 0;
            }
        }

        public int Hex2Ten(string hex)
        {
            int ten = 0;
            for (int i = 0, j = hex.Length - 1; i < hex.Length; i++)
            {
                ten += HexChar2Value(hex.Substring(i, 1)) * (int)Math.Pow(16, j);
                j--;
            }

            return ten;
        }

    }
}
