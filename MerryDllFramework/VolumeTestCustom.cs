#region Assembly MerryTestFramework.testitem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// D:\RuanWenQiang\CongViec\SourceCode\HDT608_DLL\HDT608_DLL 2023.11.16\MerryDllFramework\bin\Debug\MerryTestFramework.testitem.dll
// Decompiled with ICSharpCode.Decompiler 7.1.0.6543
#endregion

using System.Windows.Forms;
using MerryTestFramework.testitem.Forms;

namespace MerryTestFramework.testitem.Computer
{
    public class VolumeTestCustom
    {
        public bool volumetest(bool flag, string name)
        {
            ProgressBarsCustom progressBars = new ProgressBarsCustom(name);
            if (flag)
            {
                progressBars.Show();
                progressBars.VolumeUpTestNO();
            }
            else
            {
                progressBars.Show();
                progressBars.VolumeDownTestNO();
            }

            if (progressBars.DialogResult == DialogResult.Yes)
            {
                progressBars.Dispose();
                return true;
            }

            progressBars.Dispose();
            return false;
        }

        public bool volumetestNot(bool flag, string name)
        {
            ProgressBarsCustom progressBars = new ProgressBarsCustom(name);
            if (flag)
            {
                progressBars.Show();
                progressBars.VolumeUpTest();
            }
            else
            {
                progressBars.Show();
                progressBars.VolumeDownTest();
            }

            if (progressBars.DialogResult == DialogResult.Yes)
            {
                progressBars.Dispose();
                return true;
            }

            progressBars.Dispose();
            return false;
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
