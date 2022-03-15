using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace Restart_Program
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Run();
            Environment.Exit(0);
        }

        public void Run()
        {
            string filePath = @"D:\Project\5. SVI\Debug\Sic_Run.exe";
            bool bResult = false;

            ProcessStartInfo psi = new ProcessStartInfo();
            Process process = new Process();

            psi.FileName = filePath;
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;

            process.StartInfo = psi;
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(filePath);
            process.EnableRaisingEvents = true;

            IntPtr handle = IntPtr.Zero;
            process.Exited += (sender, e) => { MessageBox.Show("프로그램이 종료되었습니다. 확인해 주세요."); };

            for(int i = 0; i < 30; i++)
            {
                SpinWait.SpinUntil(() =>
                {
                    Process[] is_run = Process.GetProcessesByName("Sic_Run");
                    if(is_run.Length == 0)
                    {
                        process.Start();
                        bResult = true;
                    }
                    return bResult;
                }, TimeSpan.FromMilliseconds(1000));

                if(bResult == true)
                {
                    break;
                }
            }
            if(bResult == false)
            {
                MessageBox.Show(" 타임 아웃입니다. 재시도 해주세요. ");
                Environment.Exit(0);
            }
            Environment.Exit(0);
        }
    }
}
