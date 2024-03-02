using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NAudio.CoreAudioApi;
using System.Windows.Forms;

namespace TestMute
{
    public static class TestVol
    {
        public static string TestVolDown()
        {

            var devEnum = new MMDeviceEnumerator();
            var defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            var volume = defaultDevice.AudioEndpointVolume;
            //float leftVolumePercent = volume.Channels[0].VolumeLevelScalar * 100;
            //float rightVolumePercent = volume.Channels[1].VolumeLevelScalar * 100;


            float masterVolumePercent = volume.MasterVolumeLevelScalar * 100;


            bool flag2 = false;

            //test giam am luong
            //Console.WriteLine("Van num xoay giam am luong...");

            AutoClosingMessageBox.Show("Bam giam am luong", "Notification", 3000);

            float dn1 = volume.MasterVolumeLevelScalar * 100;
            Thread.Sleep(100);
            float dn2 = volume.MasterVolumeLevelScalar * 100;
            Thread.Sleep(1000);
            float dn3 = volume.MasterVolumeLevelScalar * 100;

            if (dn1 > dn2 || dn2 == 0 || dn1 > dn3)
            {
                flag2 = true;
            }
            else
            {

            }

            if (flag2)
            {
                return true.ToString();
            }
            else
            {
                return false.ToString();

            }

            //Thread.Sleep(2000);

        }

        public static string TestVolUp()
        {
            var devEnum = new MMDeviceEnumerator();
            var defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            var volume = defaultDevice.AudioEndpointVolume;
            //float leftVolumePercent = volume.Channels[0].VolumeLevelScalar * 100;
            //float rightVolumePercent = volume.Channels[1].VolumeLevelScalar * 100;


            float masterVolumePercent = volume.MasterVolumeLevelScalar * 100;

            bool flag1 = false;
            AutoClosingMessageBox.Show("Bam tang am luong", "Notification", 3000);

            float up1 = volume.MasterVolumeLevelScalar * 100;
            Thread.Sleep(100);
            float up2 = volume.MasterVolumeLevelScalar * 100;
            Thread.Sleep(1000);
            float up3 = volume.MasterVolumeLevelScalar * 100;

            if (up2 > up1 || up2 == 100 || up3 > up1)
            {
                flag1 = true;
                //break;
            }
            else
            {

            }

            if (flag1)
            {
                return true.ToString();
            }
            else
            {
                return false.ToString();

            }
        }

    }

    public class AutoClosingMessageBox
    {
        System.Threading.Timer _timeoutTimer;
        string _caption;
        AutoClosingMessageBox(string text, string caption, int timeout)
        {
            _caption = caption;
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                null, timeout, System.Threading.Timeout.Infinite);
            using (_timeoutTimer)
                MessageBox.Show(text, caption);
        }
        public static void Show(string text, string caption, int timeout)
        {
            new AutoClosingMessageBox(text, caption, timeout);
        }
        void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
            if (mbWnd != IntPtr.Zero)
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            _timeoutTimer.Dispose();
        }
        const int WM_CLOSE = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
    }
}
