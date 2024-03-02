#region Assembly MerryTestFramework.testitem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// D:\RuanWenQiang\CongViec\SourceCode\HDT608_DLL\HDT608_DLL 2023.11.16\MerryDllFramework\bin\Debug\MerryTestFramework.testitem.dll
// Decompiled with ICSharpCode.Decompiler 7.1.0.6543
#endregion

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using NAudio.CoreAudioApi;

namespace MerryTestFramework.testitem.Forms
{
    internal class ProgressBarsCustom : Form
    {
        private int i = 0;

        private IContainer components = null;

        internal ProgressBar progressBar1;

        internal System.Windows.Forms.Timer timer1;

        private Label label1;

        public ProgressBarsCustom(string name)
        {
            InitializeComponent();
            Text = name;
            label1.Text = name;
        }

        private void ProgressBars_Load(object sender, EventArgs e)
        {
            i = 0;
            timer1.Interval = 500;
            timer1.Enabled = true;
        }

        private void SetSysVolume(int volumeLevel)
        {
            try
            {
                MMDeviceEnumerator mMDeviceEnumerator = new MMDeviceEnumerator();
                MMDevice defaultAudioEndpoint = mMDeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                defaultAudioEndpoint.AudioEndpointVolume.MasterVolumeLevelScalar = (float)volumeLevel / 100f;
            }
            catch (Exception)
            {
            }
        }

        private int GetSysVolume()
        {
            int result = 0;
            try
            {
                MMDeviceEnumerator mMDeviceEnumerator = new MMDeviceEnumerator();
                MMDevice defaultAudioEndpoint = mMDeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                result = (int)(defaultAudioEndpoint.AudioEndpointVolume.MasterVolumeLevelScalar * 100f);
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public void VolumeUpTest()
        {
            timer1.Enabled = false;
            Thread.Sleep(500);
            SetSysVolume(1);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int num = 1;
            while (true)
            {
                double num2 = (double)stopwatch.ElapsedMilliseconds / 1000.0;
                if (num2 > 15)
                {
                    base.DialogResult = DialogResult.No;
                    Close();
                    return;
                }

                int num3 = GetSysVolume() + 1;
                if (num > num3)
                {
                    base.DialogResult = DialogResult.No;
                    Close();
                    return;
                }

                num = num3;
                if (num3 >= 20)
                {
                    break;
                }

                progressBar1.Value = num3 * 5;
                Application.DoEvents();
                Thread.Sleep(20);
            }

            base.DialogResult = DialogResult.Yes;
            Close();
        }

        public void VolumeDownTest()
        {
            timer1.Enabled = false;
            SetSysVolume(99);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int num = 99;
            while (true)
            {
                double num2 = (double)stopwatch.ElapsedMilliseconds / 1000.0;
                if (num2 > 15.0)
                {
                    base.DialogResult = DialogResult.No;
                    Close();
                    return;
                }

                int sysVolume = GetSysVolume();
                if (num < sysVolume)
                {
                    base.DialogResult = DialogResult.No;
                    Close();
                    return;
                }

                num = sysVolume;
                if (100 - sysVolume >= 20)
                {
                    break;
                }

                progressBar1.Value = (100 - sysVolume) * 5;
                Application.DoEvents();
                Thread.Sleep(20);
            }

            base.DialogResult = DialogResult.Yes;
            Close();
        }

        public void VolumeUpTestNO()
        {
            timer1.Enabled = false;
            Thread.Sleep(500);
            SetSysVolume(1);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int num = 1;
            while (true)
            {
                double num2 = (double)stopwatch.ElapsedMilliseconds / 1000.0;
                if (num2 > 13.0)
                {
                    base.DialogResult = DialogResult.No;
                    Close();
                    return;
                }

                int num3 = GetSysVolume() + 1;
                num = num3;
                if (num3 >= 99)
                {
                    break;
                }

                progressBar1.Value = num3;
                Application.DoEvents();
                Thread.Sleep(20);
            }

            base.DialogResult = DialogResult.Yes;
            Close();
        }

        public void VolumeDownTestNO()
        {
            timer1.Enabled = false;
            SetSysVolume(99);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int num = 99;
            while (true)
            {
                double num2 = (double)stopwatch.ElapsedMilliseconds / 1000.0;
                if (num2 > 13.0)
                {
                    base.DialogResult = DialogResult.No;
                    Close();
                    return;
                }

                int sysVolume = GetSysVolume();
                num = sysVolume;
                if (100 - sysVolume >= 99)
                {
                    break;
                }

                progressBar1.Value = (100 - sysVolume);
                Application.DoEvents();
                Thread.Sleep(20);
            }

            base.DialogResult = DialogResult.Yes;
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i += 5;
            progressBar1.Value = i;
            if (i >= 100)
            {
                base.DialogResult = DialogResult.No;
                timer1.Enabled = false;
                Close();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            progressBar1 = new System.Windows.Forms.ProgressBar();
            timer1 = new System.Windows.Forms.Timer(components);
            label1 = new System.Windows.Forms.Label();
            SuspendLayout();
            progressBar1.Location = new System.Drawing.Point(2, 149);
            progressBar1.Margin = new System.Windows.Forms.Padding(4);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(605, 71);
            progressBar1.TabIndex = 2;
            timer1.Interval = 500;
            timer1.Tick += new System.EventHandler(timer1_Tick);
            label1.Font = new System.Drawing.Font("新宋体", 28.2f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(2, 2);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(605, 143);
            label1.TabIndex = 3;
            label1.Text = "label1";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 15f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(608, 228);
            base.Controls.Add(label1);
            base.Controls.Add(progressBar1);
            base.Name = "ProgressBars";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "ProgressBars";
            base.TopMost = true;
            base.Load += new System.EventHandler(ProgressBars_Load);
            ResumeLayout(false);
        }
    }
}
#if false // Decompilation log
'20' items in cache
------------------
Resolve: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\mscorlib.dll'
------------------
Resolve: 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.dll'
------------------
Resolve: 'System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Core.dll'
------------------
Resolve: 'Newtonsoft.Json, Version=6.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed'
Could not find by name: 'Newtonsoft.Json, Version=6.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed'
------------------
Resolve: 'System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Data.dll'
------------------
Resolve: 'System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.dll'
------------------
Resolve: 'MESDLL, Version=21.0.0.1, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'MESDLL, Version=21.0.0.1, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\RuanWenQiang\CongViec\SourceCode\HDT608_DLL\HDT608_DLL 2023.11.16\MerryDllFramework\bin\Debug\MESDLL.dll'
------------------
Resolve: 'System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Windows.Forms.dll'
------------------
Resolve: 'SwATE_Net, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
Could not find by name: 'SwATE_Net, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
------------------
Resolve: 'NAudio, Version=1.9.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'NAudio, Version=1.9.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\RuanWenQiang\CongViec\SourceCode\HDT608_DLL\HDT608_DLL 2023.11.16\MerryDllFramework\bin\Debug\NAudio.dll'
------------------
Resolve: 'System.Management, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Management, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Management.dll'
------------------
Resolve: 'Bass.Net, Version=2.4.11.1, Culture=neutral, PublicKeyToken=b7566c273e6ef480'
Found single assembly: 'Bass.Net, Version=2.4.11.1, Culture=neutral, PublicKeyToken=b7566c273e6ef480'
Load from: 'D:\RuanWenQiang\CongViec\SourceCode\HDT608_DLL\HDT608_DLL 2023.11.16\MerryDllFramework\bin\Debug\Bass.Net.dll'
------------------
Resolve: 'Microsoft.DirectX.DirectSound, Version=1.0.2902.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'
Found single assembly: 'Microsoft.DirectX.DirectSound, Version=1.0.2902.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'
Load from: 'D:\RuanWenQiang\CongViec\SourceCode\HDT608_DLL\HDT608_DLL 2023.11.16\MerryDllFramework\bin\Debug\Microsoft.DirectX.DirectSound.dll'
------------------
Resolve: 'Microsoft.DirectX, Version=1.0.2902.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'
Found single assembly: 'Microsoft.DirectX, Version=1.0.2902.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'
Load from: 'D:\RuanWenQiang\CongViec\SourceCode\HDT608_DLL\HDT608_DLL 2023.11.16\MerryDllFramework\bin\Debug\Microsoft.DirectX.dll'
------------------
Resolve: 'Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\Microsoft.CSharp.dll'
------------------
Resolve: 'PC_VolumeControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'PC_VolumeControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\RuanWenQiang\CongViec\SourceCode\HDT608_DLL\HDT608_DLL 2023.11.16\MerryDllFramework\bin\Debug\PC_VolumeControl.dll'
#endif
