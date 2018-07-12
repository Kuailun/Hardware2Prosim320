using ProSimSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hardware2Prosim320
{
    public partial class Hardware2Prosim : Form
    {
        // Our main ProSim connection
        ProSimConnect connection = new ProSimConnect();
        // 声明串口
        SerialPort sp_Glare = null;
        SerialPort sp_TQ = null;
        SerialPort sp_StickL = null;
        SerialPort sp_StickR = null;
        SerialPort sp_CDUL = null;
        SerialPort sp_CDUR = null;
        // 硬件连接标志
        bool bGlare;
        bool bTQ;
        bool bCDUL;
        bool bCDUR;
        bool bStickL;
        bool bStickR;
        // 数据区
        A320_Data_Glare a320_data_glare;
        A320_Data_TQ a320_data_tq;


        public Hardware2Prosim()
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
            textBox_CDUL.Enabled = false;
            textBox_CDUR.Enabled = false;
            textBox_StickL.Enabled = false;
            textBox_StickR.Enabled = false;
        }

        /// <summary>
        /// 设置界面上的信息提示
        /// </summary>
        /// <param name="p_string">
        /// 要显示的信息
        /// </param>
        public void WriteLine(string p_string)
        {
            label_Status.Text = "状态：" + p_string;
        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            if (connection.isConnected == false)
            {
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
                //获得硬件选择情况
                GetHardwareStatus();
                //映射Prosim变量到类中
                
                OpenComms();
                ConnectProsim();
            }
            else
            {
                connection.Dispose();
                connection = new ProSimConnect();
                WriteLine("已断开与ProSim320的连接");
                button_Connect.Text = "连接";
                CloseComms();
            }   
        }

        /// <summary>
        /// 检查设备连接情况，打开串口通讯
        /// </summary>
        public void OpenComms()
        {
            if(bGlare)
            {
                a320_data_glare = new A320_Data_Glare();
                OpenCom(ref sp_Glare, textBox_Glare.Text, 9600);
            }
            if(bTQ)
            {
                a320_data_tq = new A320_Data_TQ();
                OpenCom(ref sp_TQ, textBox_TQ.Text, 9600);
            }
            if(bStickL)
            {
                OpenCom(ref sp_StickL, textBox_StickL.Text, 9600);
            }
            if(bStickR)
            {
                OpenCom(ref sp_StickR, textBox_StickR.Text, 9600);
            }
            if(bCDUL)
            {
                OpenCom(ref sp_CDUL, textBox_CDUL.Text, 9600);
            }
            if(bCDUR)
            {
                OpenCom(ref sp_CDUR, textBox_CDUR.Text, 9600);
            }
        }

        /// <summary>
        /// 用于点击断开连接Prosim时断开串口
        /// </summary>
        public void CloseComms()
        {
            if(sp_Glare!=null)
            {
                if (sp_Glare.IsOpen)
                {
                    sp_Glare.Close();
                }
            }
            if (sp_TQ != null)
            {
                if (sp_TQ.IsOpen)
                {
                    sp_TQ.Close();
                }
            }
            if (sp_StickL != null)
            {
                if (sp_StickL.IsOpen)
                {
                    sp_StickL.Close();
                }
            }
            if (sp_StickR != null)
            {
                if (sp_StickR.IsOpen)
                {
                    sp_StickR.Close();
                }
            }
            if (sp_CDUL != null)
            {
                if (sp_CDUL.IsOpen)
                {
                    sp_CDUL.Close();
                }
            }
            if (sp_CDUR != null)
            {
                if (sp_CDUR.IsOpen)
                {
                    sp_CDUR.Close();
                }
            }
        }

        /// <summary>
        /// 获取硬件选择情况
        /// </summary>
        public void GetHardwareStatus()
        {
            bGlare = checkBox_Glare.Checked;
            bTQ = checkBox_TQ.Checked;
            bCDUL = checkBox_CDUL.Checked;
            bCDUR = checkBox_CDUR.Checked;
            bStickL = checkBox_StickL.Checked;
            bStickR = checkBox_StickR.Checked;
        }

        /// <summary>
        /// 对需要连接的硬件打开串口
        /// </summary>
        /// <param name="sp">串口变量，引用模式</param>
        /// <param name="portName">串口号</param>
        /// <param name="baudRate">波特率</param>
        public void OpenCom(ref SerialPort sp, string portName, int baudRate)
        {
            sp = new SerialPort();
            sp.PortName = portName;
            sp.BaudRate = baudRate;
            sp.StopBits = StopBits.One;
            sp.DataBits = 8;
            sp.Parity = 0;
            sp.ReadTimeout = 1;
            try
            {
                if (!sp.IsOpen)
                {
                    sp.Open();
                    sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                }
            }
            catch (Exception e)
            {
                WriteLine(portName+":打开串口错误");
            }
        }

        public void ConnectProsim()
        {
            if(a320_data_glare!=null)
            {
                //FCU
                a320_data_glare.S_FCU_ALTITUDE = new DataRef("system.switches.S_FCU_ALTITUDE", 100, connection);
                a320_data_glare.S_FCU_ALTITUDE_SCALE = new DataRef("system.switches.S_FCU_ALTITUDE_SCALE", 100, connection);
                a320_data_glare.S_FCU_AP1 = new DataRef("system.switches.S_FCU_AP1", 100, connection);
                a320_data_glare.S_FCU_AP2 = new DataRef("system.switches.S_FCU_AP1", 100, connection);
                a320_data_glare.S_FCU_APPR = new DataRef("system.switches.S_FCU_APPR", 100, connection);
                a320_data_glare.S_FCU_ATHR = new DataRef("system.switches.S_FCU_ATHR", 100, connection);
                a320_data_glare.S_FCU_EXPED = new DataRef("system.switches.S_FCU_EXPED", 100, connection);
                a320_data_glare.S_FCU_HDGVS_TRKFPA = new DataRef("system.switches.S_FCU_HDGVS_TRKFPA", 100, connection);
                a320_data_glare.S_FCU_HEADING = new DataRef("system.switches.S_FCU_HEADING", 100, connection);
                a320_data_glare.S_FCU_LOC = new DataRef("system.switches.S_FCU_LOC", 100, connection);
                a320_data_glare.S_FCU_METRIC_ALT = new DataRef("system.switches.S_FCU_METRIC_ALT", 100, connection);
                a320_data_glare.S_FCU_SPD_MACH = new DataRef("system.switches.S_FCU_SPD_MACH", 100, connection);
                a320_data_glare.S_FCU_SPEED = new DataRef("system.switches.S_FCU_SPEED", 100, connection);
                a320_data_glare.S_FCU_VERTICAL_SPEED = new DataRef("system.switches.S_FCU_VERTICAL_SPEED", 100, connection);
                a320_data_glare.A_FCU_ALTITUDE = new DataRef("system.analog.A_FCU_ALTITUDE", 100, connection);
                a320_data_glare.A_FCU_HEADING = new DataRef("system.analog.A_FCU_HEADING", 100, connection);
                a320_data_glare.A_FCU_LIGHTING = new DataRef("system.analog.A_FCU_LIGHTING", 100, connection);
                a320_data_glare.A_FCU_LIGHTING_TEXT = new DataRef("system.analog.A_FCU_LIGHTING_TEXT", 100, connection);
                a320_data_glare.A_FCU_SPEED = new DataRef("system.analog.A_FCU_SPEED", 100, connection);
                a320_data_glare.A_FCU_VS = new DataRef("system.analog.A_FCU_VS", 100, connection);
                a320_data_glare.B_FCU_HEADING_DASHED = new DataRef("system.gates.B_FCU_HEADING_DASHED", 100, connection);
                a320_data_glare.B_FCU_POWER = new DataRef("system.gates.B_FCU_POWER", 100, connection);
                a320_data_glare.B_FCU_SPEED_DASHED = new DataRef("system.gates.B_FCU_SPEED_DASHED", 100, connection);
                a320_data_glare.B_FCU_SPEED_MACH = new DataRef("system.gates.B_FCU_SPEED_MACH", 100, connection);
                a320_data_glare.B_FCU_TRACK_FPA_MODE = new DataRef("system.gates.B_FCU_TRACK_FPA_MODE", 100, connection);
                a320_data_glare.B_FCU_VERTICALSPEED_DASHED = new DataRef("system.gates.B_FCU_VERTICALSPEED_DASHED", 100, connection);
                a320_data_glare.I_FCU_AP1 = new DataRef("system.indicators.I_FCU_AP1", 100, connection);
                a320_data_glare.I_FCU_AP2 = new DataRef("system.indicators.I_FCU_AP2", 100, connection);
                a320_data_glare.I_FCU_APPR = new DataRef("system.indicators.I_FCU_APPR", 100, connection);
                a320_data_glare.I_FCU_ATHR = new DataRef("system.indicators.I_FCU_ATHR", 100, connection);
                a320_data_glare.I_FCU_EXPED = new DataRef("system.indicators.I_FCU_EXPED", 100, connection);
                a320_data_glare.I_FCU_HEADING_MANAGED = new DataRef("system.indicators.I_FCU_HEADING_MANAGED", 100, connection);
                a320_data_glare.I_FCU_HEADING_VS_MODE = new DataRef("system.indicators.I_FCU_HEADING_VS_MODE", 100, connection);
                a320_data_glare.I_FCU_LOC = new DataRef("system.indicators.I_FCU_LOC", 100, connection);
                a320_data_glare.I_FCU_MACH_MODE = new DataRef("system.indicators.I_FCU_MACH_MODE", 100, connection);
                a320_data_glare.I_FCU_SPEED_MANAGED = new DataRef("system.indicators.I_FCU_SPEED_MANAGED", 100, connection);
                a320_data_glare.I_FCU_SPEED_MODE = new DataRef("system.indicators.I_FCU_SPEED_MODE", 100, connection);
                a320_data_glare.I_FCU_TRACK_FPA_MODE = new DataRef("system.indicators.I_FCU_TRACK_FPA_MODE", 100, connection);
                a320_data_glare.N_FCU_HEADING = new DataRef("system.numerical.N_FCU_HEADING", 100, connection);
                a320_data_glare.N_FCU_LIGHTING = new DataRef("system.numerical.N_FCU_LIGHTING", 100, connection);
                a320_data_glare.N_FCU_LIGHTING_TEXT = new DataRef("system.numerical.N_FCU_LIGHTING_TEXT", 100, connection);
                a320_data_glare.N_FCU_SPEED = new DataRef("system.numerical.N_FCU_SPEED", 100, connection);
                a320_data_glare.N_FCU_VS = new DataRef("system.numerical.N_FCU_VS", 100, connection);
                a320_data_glare.E_FCU_ALTITUDE = new DataRef("system.encoders.E_FCU_ALTITUDE", 100, connection);
                a320_data_glare.E_FCU_HEADING = new DataRef("system.encoders.E_FCU_HEADING", 100, connection);
                a320_data_glare.E_FCU_SPEED = new DataRef("system.encoders.E_FCU_SPEED", 100, connection);
                a320_data_glare.E_FCU_VS = new DataRef("system.encoders.E_FCU_VS", 100, connection);

                //EFIS1
                a320_data_glare.S_FCU_EFIS1_ARPT = new DataRef("system.switches.S_FCU_EFIS1_ARPT", 100, connection);
                a320_data_glare.S_FCU_EFIS1_BARO_MODE = new DataRef("system.switches.S_FCU_EFIS1_BARO_MODE", 100, connection);
                a320_data_glare.S_FCU_EFIS1_BARO_STD = new DataRef("system.switches.S_FCU_EFIS1_BARO_STD", 100, connection);
                a320_data_glare.S_FCU_EFIS1_CSTR = new DataRef("system.switches.S_FCU_EFIS1_CSTR", 100, connection);
                a320_data_glare.S_FCU_EFIS1_FD = new DataRef("system.switches.S_FCU_EFIS1_FD", 100, connection);
                a320_data_glare.S_FCU_EFIS1_LS = new DataRef("system.switches.S_FCU_EFIS1_LS", 100, connection);
                a320_data_glare.S_FCU_EFIS1_NAV1 = new DataRef("system.switches.S_FCU_EFIS1_NAV1", 100, connection);
                a320_data_glare.S_FCU_EFIS1_NAV2 = new DataRef("system.switches.S_FCU_EFIS1_NAV2", 100, connection);
                a320_data_glare.S_FCU_EFIS1_ND_MODE = new DataRef("system.switches.S_FCU_EFIS1_ND_MODE", 100, connection);
                a320_data_glare.S_FCU_EFIS1_ND_ZOOM = new DataRef("system.switches.S_FCU_EFIS1_ND_ZOOM", 100, connection);
                a320_data_glare.S_FCU_EFIS1_NDB = new DataRef("system.switches.S_FCU_EFIS1_NDB", 100, connection);
                a320_data_glare.S_FCU_EFIS1_VORD = new DataRef("system.switches.S_FCU_EFIS1_VORD", 100, connection);
                a320_data_glare.S_FCU_EFIS1_WPT = new DataRef("system.switches.S_FCU_EFIS1_WPT", 100, connection);
                a320_data_glare.A_FCU_EFIS1_BARO_HPA = new DataRef("system.switches.A_FCU_EFIS1_BARO_HPA", 100, connection);
                a320_data_glare.A_FCU_EFIS1_BARO_INCH = new DataRef("system.switches.A_FCU_EFIS1_BARO_INCH", 100, connection);
                a320_data_glare.B_FCU_EFIS1_BARO_INCH = new DataRef("system.gates.B_FCU_EFIS1_BARO_INCH", 100, connection);
                a320_data_glare.B_FCU_EFIS1_BARO_STD = new DataRef("system.gates.B_FCU_EFIS1_BARO_STD", 100, connection);
                a320_data_glare.I_FCU_EFIS1_ARPT = new DataRef("system.indicators.I_FCU_EFIS1_ARPT", 100, connection);
                a320_data_glare.I_FCU_EFIS1_CSTR = new DataRef("system.indicators.I_FCU_EFIS1_CSTR", 100, connection);
                a320_data_glare.I_FCU_EFIS1_FD = new DataRef("system.indicators.I_FCU_EFIS1_FD", 100, connection);
                a320_data_glare.I_FCU_EFIS1_LS = new DataRef("system.indicators.I_FCU_EFIS1_LS", 100, connection);
                a320_data_glare.I_FCU_EFIS1_NDB = new DataRef("system.indicators.I_FCU_EFIS1_NDB", 100, connection);
                a320_data_glare.I_FCU_EFIS1_QNH = new DataRef("system.indicators.I_FCU_EFIS1_QNH", 100, connection);
                a320_data_glare.I_FCU_EFIS1_VORD = new DataRef("system.indicators.I_FCU_EFIS1_VORD", 100, connection);
                a320_data_glare.I_FCU_EFIS1_WPT = new DataRef("system.indicators.I_FCU_EFIS1_WPT", 100, connection);
                a320_data_glare.N_FCU_EFIS1_BARO_HPA = new DataRef("system.numerical.N_FCU_EFIS1_BARO_HPA", 100, connection);
                a320_data_glare.N_FCU_EFIS1_BARO_INCH = new DataRef("system.numerical.N_FCU_EFIS1_BARO_INCH", 100, connection);
                a320_data_glare.E_FCU_EFIS1_BARO = new DataRef("system.encoders.E_FCU_EFIS1_BARO", 100, connection);

                //EFIS2
                a320_data_glare.S_FCU_EFIS2_ARPT = new DataRef("system.switches.S_FCU_EFIS2_ARPT", 100, connection);
                a320_data_glare.S_FCU_EFIS2_BARO_MODE = new DataRef("system.switches.S_FCU_EFIS2_BARO_MODE", 100, connection);
                a320_data_glare.S_FCU_EFIS2_BARO_STD = new DataRef("system.switches.S_FCU_EFIS2_BARO_STD", 100, connection);
                a320_data_glare.S_FCU_EFIS2_CSTR = new DataRef("system.switches.S_FCU_EFIS2_CSTR", 100, connection);
                a320_data_glare.S_FCU_EFIS2_FD = new DataRef("system.switches.S_FCU_EFIS2_FD", 100, connection);
                a320_data_glare.S_FCU_EFIS2_LS = new DataRef("system.switches.S_FCU_EFIS2_LS", 100, connection);
                a320_data_glare.S_FCU_EFIS2_NAV1 = new DataRef("system.switches.S_FCU_EFIS2_NAV1", 100, connection);
                a320_data_glare.S_FCU_EFIS2_NAV2 = new DataRef("system.switches.S_FCU_EFIS2_NAV2", 100, connection);
                a320_data_glare.S_FCU_EFIS2_ND_MODE = new DataRef("system.switches.S_FCU_EFIS2_ND_MODE", 100, connection);
                a320_data_glare.S_FCU_EFIS2_ND_ZOOM = new DataRef("system.switches.S_FCU_EFIS2_ND_ZOOM", 100, connection);
                a320_data_glare.S_FCU_EFIS2_NDB = new DataRef("system.switches.S_FCU_EFIS2_NDB", 100, connection);
                a320_data_glare.S_FCU_EFIS2_VORD = new DataRef("system.switches.S_FCU_EFIS2_VORD", 100, connection);
                a320_data_glare.S_FCU_EFIS2_WPT = new DataRef("system.switches.S_FCU_EFIS2_WPT", 100, connection);
                a320_data_glare.A_FCU_EFIS2_BARO_HPA = new DataRef("system.switches.A_FCU_EFIS2_BARO_HPA", 100, connection);
                a320_data_glare.A_FCU_EFIS2_BARO_INCH = new DataRef("system.switches.A_FCU_EFIS2_BARO_INCH", 100, connection);
                a320_data_glare.B_FCU_EFIS2_BARO_INCH = new DataRef("system.gates.B_FCU_EFIS2_BARO_INCH", 100, connection);
                a320_data_glare.B_FCU_EFIS2_BARO_STD = new DataRef("system.gates.B_FCU_EFIS2_BARO_STD", 100, connection);
                a320_data_glare.I_FCU_EFIS2_ARPT = new DataRef("system.indicators.I_FCU_EFIS2_ARPT", 100, connection);
                a320_data_glare.I_FCU_EFIS2_CSTR = new DataRef("system.indicators.I_FCU_EFIS2_CSTR", 100, connection);
                a320_data_glare.I_FCU_EFIS2_FD = new DataRef("system.indicators.I_FCU_EFIS2_FD", 100, connection);
                a320_data_glare.I_FCU_EFIS2_LS = new DataRef("system.indicators.I_FCU_EFIS2_LS", 100, connection);
                a320_data_glare.I_FCU_EFIS2_NDB = new DataRef("system.indicators.I_FCU_EFIS2_NDB", 100, connection);
                a320_data_glare.I_FCU_EFIS2_QNH = new DataRef("system.indicators.I_FCU_EFIS2_QNH", 100, connection);
                a320_data_glare.I_FCU_EFIS2_VORD = new DataRef("system.indicators.I_FCU_EFIS2_VORD", 100, connection);
                a320_data_glare.I_FCU_EFIS2_WPT = new DataRef("system.indicators.I_FCU_EFIS2_WPT", 100, connection);
                a320_data_glare.N_FCU_EFIS2_BARO_HPA = new DataRef("system.numerical.N_FCU_EFIS2_BARO_HPA", 100, connection);
                a320_data_glare.N_FCU_EFIS2_BARO_INCH = new DataRef("system.numerical.N_FCU_EFIS2_BARO_INCH", 100, connection);
                a320_data_glare.E_FCU_EFIS2_BARO = new DataRef("system.encoders.E_FCU_EFIS2_BARO", 100, connection);
            }
            

            //TQ
            if(a320_data_tq!=null)
            {
                a320_data_tq.A_FC_THROTTLE_LEFT_INPUT = new DataRef("system.analog.A_FC_THROTTLE_LEFT_INPUT", 100, connection);
                a320_data_tq.A_FC_THROTTLE_RIGHT_INPUT = new DataRef("system.analog.A_FC_THROTTLE_RIGHT_INPUT", 100, connection);
            }
        }
        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            try
            {
                if (sp.IsOpen)
                {
                    byte[] byteRead = new byte[sp.BytesToRead];
                    sp.Read(byteRead, 0, byteRead.Length);
                    if (byteRead[0] == 240)
                    {
                        int eng_l = byteRead[1];
                        int eng_r = byteRead[2];
                        if (eng_l >= 128)
                        {
                            eng_l = eng_l - 256;
                        }
                        if (eng_r >= 128)
                        {
                            eng_r = eng_r - 256;
                        }

                        if (eng_l < 0)
                        {
                            eng_l = (eng_l + 20) * 52;
                        }
                        else if (eng_l >= 0 && eng_l <= 24)
                        {
                            eng_l = 2001 + eng_l * 41;
                        }
                        else if (eng_l >= 25 && eng_l <= 34)
                        {
                            eng_l = 3001 + (eng_l - 25) * 99;
                        }
                        else if (eng_l >= 35 && eng_l <= 44)
                        {
                            eng_l = 4001 + (eng_l - 35) * 99;
                        }
                        else if (eng_l >= 45)
                        {
                            eng_l = 5001;
                        }

                        if (eng_r < 0)
                        {
                            eng_r = (eng_r + 20) * 52;
                        }
                        else if (eng_r >= 0 && eng_r <= 24)
                        {
                            eng_r = 2001 + eng_r * 41;
                        }
                        else if (eng_r >= 25 && eng_r <= 34)
                        {
                            eng_r = 3001 + (eng_r - 25) * 99;
                        }
                        else if (eng_r >= 35 && eng_r <= 44)
                        {
                            eng_r = 4001 + (eng_r - 35) * 99;
                        }
                        else if (eng_r >= 45)
                        {
                            eng_r = 5001;
                        }

                        Console.WriteLine("{0},{1}", eng_l, eng_r);
                    }
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
            }

            //throw new NotImplementedException();
        }


        private void checkBox_Glare_CheckedChanged(object sender, EventArgs e)
        {
            textBox_Glare.Enabled = false;
            if (checkBox_Glare.Checked)
            {
                textBox_Glare.Enabled = true;
            }
        }
        private void checkBox_TQ_CheckedChanged(object sender, EventArgs e)
        {
            textBox_TQ.Enabled = false;
            if (checkBox_TQ.Checked)
            {
                textBox_TQ.Enabled = true;
            }
        }
        private void checkBox_CDUL_CheckedChanged(object sender, EventArgs e)
        {
            textBox_CDUL.Enabled = false;
            if (checkBox_CDUL.Checked)
            {
                textBox_CDUL.Enabled = true;
            }
        }
        private void checkBox_CDUR_CheckedChanged(object sender, EventArgs e)
        {
            textBox_CDUR.Enabled = false;
            if (checkBox_CDUR.Checked)
            {
                textBox_CDUR.Enabled = true;
            }
        }
        private void checkBox_StickL_CheckedChanged(object sender, EventArgs e)
        {
            textBox_StickL.Enabled = false;
            if (checkBox_StickL.Checked)
            {
                textBox_StickL.Enabled = true;
            }
        }
        private void checkBox_StickR_CheckedChanged(object sender, EventArgs e)
        {
            textBox_StickR.Enabled = false;
            if (checkBox_StickR.Checked)
            {
                textBox_StickR.Enabled = true;
            }
        }
    }
}
