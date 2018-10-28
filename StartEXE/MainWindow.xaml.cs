using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Threading;

namespace StartEXE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string myDesktopPath;
        private string[] myEXE = { "Prosim-System.lnk", "Skalarki.lnk", "Hardware2Prosim320.lnk", "Prosim-Audio.lnk", "P3D.lnk" };
        //private string[] myEXE = { "Prosim-System.lnk", "Prosim-Audio.lnk", "P3D.lnk" };
        bool mystate = false;
        public MainWindow()
        {
            InitializeComponent();
            myDesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Disp("检测文件夹");
            if (Directory.Exists(myDesktopPath + "\\启动"))
            {
                Disp("启动文件夹存在");
                mystate = true;
                for (int i = 0; i < myEXE.Length; i++)
                {
                    bool isExist = File.Exists(myDesktopPath + "\\启动\\" + myEXE[i]);
                    if(isExist==false)
                    {
                        mystate = false;
                    }
                    Disp("检测：" + myEXE[i] + "......"+isExist);
                }                
            }
            else
            {
                Disp("启动文件夹不存在，退出");
                return;
            }
            
        }
        private void Disp(string p_string)
        {
            Box_Text.AppendText(p_string+"\n");
            Box_Text.ScrollToEnd();
        }
        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            if(mystate)
            {
                Disp("检测成功，开始启动......");
                for(int i=0;i<myEXE.Length;i++)
                {
                    Disp("启动：" + myEXE[i]);
                    Process.Start(myDesktopPath+ "\\启动\\" + myEXE[i]);
                    switch(i)
                    {
                        case 0:
                            Thread.Sleep(30000);
                            break;
                        case 1:
                            Thread.Sleep(5000);
                            break;
                        case 2:
                            Thread.Sleep(1000);
                            break;
                        case 3:
                            Thread.Sleep(10000);
                            break;
                    }
                }
            }
            else
            {
                Disp("无法开始运行程序");
            }
        }
    }
}
