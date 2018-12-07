using ProSimSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hardware2Prosim320V2
{
    public partial class Form1 : Form
    {
        //Prosim 连接体
        ProSimConnect connection = new ProSimConnect();
        HardwareObject a320_Glare;
        HardwareObject a320_TQ;
        HardwareObject a320_YokeL;
        HardwareObject a320_YokeR;

        public Form1()
        {
            InitializeComponent();
            Initialize();
        }
        /// <summary>
        /// 初始化组件
        ///     显示组件
        /// </summary>
        public void Initialize()
        {
            textBox_Glare.Enabled = false;
            textBox_TQ.Enabled = false;
            textBox_YokeL.Enabled = false;
            textBox_YokeR.Enabled = false;

            checkBox_Glare.Checked = true;
            checkBox_TQ.Checked = true;
            checkBox_YokeL.Checked = true;
            checkBox_YokeR.Checked = true;
        }

        private void WriteLine(string p_line)
        {
            richTextBox1.AppendText(p_line + "\n");
        }
        private void ClearLine()
        {
            richTextBox1.Clear();
        }
        private void button_Connect_Click(object sender, EventArgs e)
        {
            if (connection.isConnected == false)
            {
                //清除显示区
                ClearLine();    
                try
                {
                    //连接本地端Prosim
                    connection.Connect("127.0.0.1");
                }
                catch (Exception ex)
                {
                    WriteLine("无法连接ProSim320程序");
                    return;
                }
                WriteLine("已连接ProSim320程序");
                button_Connect.Text = "断开连接";
                
                //获得硬件选择情况并创建
                GetHardwareStatus();

                Thread.Sleep(200);
                //StartThreads();
            }
            else
            {
                connection.Dispose();
                connection = new ProSimConnect();
                WriteLine("已断开与ProSim320的连接");
                button_Connect.Text = "连接";

                a320_Glare.Disconnect();
                a320_TQ.Disconnect();
                a320_YokeL.Disconnect();
                a320_YokeR.Disconnect();

                //回收内存，Prosim接口没有多次运行的回收机制
                GC.Collect();
            }
        }
        /// <summary>
        /// 获取硬件选择情况
        /// </summary>
        public void GetHardwareStatus()
        {
            if(a320_Glare==null)
            {
                a320_Glare = new HardwareObject();
                a320_Glare.HardwareName = HardwareObject.HardwareID.Glare;
                a320_Glare.HardwareCreate();
            }
            a320_Glare.HardwareChecked = checkBox_Glare.Checked;
            if(a320_Glare.HardwareChecked)
            {
                a320_Glare.m_portID = textBox_Glare.Text;
                bool ret=a320_Glare.Connect(ref connection);
                if (!ret) WriteLine("打开Glare错误");
            }

            if (a320_TQ == null)
            {
                a320_TQ = new HardwareObject();
                a320_TQ.HardwareName = HardwareObject.HardwareID.TQ;
                a320_TQ.HardwareCreate();
            }
            a320_TQ.HardwareChecked = checkBox_Glare.Checked;
            if (a320_TQ.HardwareChecked)
            {
                a320_TQ.m_portID = textBox_TQ.Text;
                bool ret = a320_TQ.Connect(ref connection);
                if (!ret) WriteLine("打开TQ错误");
            }

            if (a320_YokeL == null)
            {
                a320_YokeL = new HardwareObject();
                a320_YokeL.HardwareName = HardwareObject.HardwareID.YokeL;
                a320_YokeL.HardwareCreate();
            }
            a320_YokeL.HardwareChecked = checkBox_YokeL.Checked;
            if (a320_YokeL.HardwareChecked)
            {
                a320_YokeL.m_portID = textBox_YokeL.Text;
                bool ret = a320_YokeL.Connect(ref connection);
                if (!ret) WriteLine("打开YokeL错误");
            }

            if (a320_YokeR == null)
            {
                a320_YokeR = new HardwareObject();
                a320_YokeR.HardwareName = HardwareObject.HardwareID.YokeR;
                a320_YokeR.HardwareCreate();
            }
            a320_YokeR.HardwareChecked = checkBox_YokeR.Checked;
            if (a320_YokeR.HardwareChecked)
            {
                a320_YokeR.m_portID = textBox_YokeR.Text;
                bool ret = a320_YokeR.Connect(ref connection);
                if (!ret) WriteLine("打开YokeR错误");
            }



        }

        private void textBox_Glare_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_TQ_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_YokeL_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox_YokeR_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
