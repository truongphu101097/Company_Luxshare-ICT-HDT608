using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MerryDllFramework
{
    public class ReadSoundevice
    {
        [DllImport("winmm.dll", SetLastError = true)]
        static extern uint waveOutGetNumDevs();

        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutGetDevCaps(uint hwo, ref WAVEOUTCAPS pwoc, uint cbwoc);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WAVEOUTCAPS
        {
            public ushort wMid;
            public ushort wPid;
            public uint vDriverVersion;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szPname;
            public uint dwFormats;
            public ushort wChannels;
            public ushort wReserved1;
            public uint dwSupport;
        }

        public string GetSoundDevices()
        {
            string headsetName = "";
            string[] headsetdeviceName;
            uint devices = waveOutGetNumDevs();
            string[] result = new string[devices];
            WAVEOUTCAPS caps = new WAVEOUTCAPS();

            for (uint i = 0; i < devices; i++)
            {
                waveOutGetDevCaps(i, ref caps, (uint)Marshal.SizeOf(caps));
                result[i] = caps.szPname;
            }

            for (int j = 0; j < devices; j++)
            {
                if (result[j].Contains("Logitech H570e Stereo") || result[j].Contains("Logicool H570e Stereo"))
                {
                    headsetName = result[j];
                }

            }

            headsetdeviceName = headsetName.Split('(', ')');

            if (headsetdeviceName[1] == "" || headsetdeviceName[1] == null)
            {
                return false.ToString();
            }
            return headsetdeviceName[1].ToString();
        }

    }
}
