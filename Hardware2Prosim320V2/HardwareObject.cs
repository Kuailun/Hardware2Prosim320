using Hardware2Prosim320;
using ProSimSDK;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hardware2Prosim320V2
{
    class HardwareObject
    {
        private List<byte> buffer = new List<byte>(80);
        private A320_Data_Glare a320_data_glare;
        private A320_Data_TQ a320_data_tq;
        private A320_Data_CDU a320_data_cdu_L;
        private A320_Data_CDU a320_data_cdu_R;
        private A320_Data_FC_YOKE a320_data_yoke_L;
        private A320_Data_FC_YOKE a320_data_yoke_R;
        private ProSimConnect m_connection;
        private SerialPort m_port;
        private bool stopthread = true;
        private int interval = 50;
        // 声明线程
        private Thread td_Glare;
        private Thread td_TQ;
        //初始化接口转化
        HardwareCalculation h = new HardwareCalculation();

        public enum HardwareID
        {
            Glare,TQ,CDU,YokeL,YokeR,Null
        }
        public string m_portID = "COM0";
        public int m_portBaudRate = 9600;
        public bool HardwareChecked = false;
        public HardwareID HardwareName =HardwareID.Null;
        public enum HardwareStatus
        {
            Established,Initialized,Readytoconnect,Inconnect,Closed
        }
        public HardwareStatus status = HardwareStatus.Established;
        public HardwareObject()
        {
            td_Glare = new Thread(td_GlareSend);
            td_TQ = new Thread(td_TQSend);
        }
        public bool HardwareCreate()
        {
            switch (HardwareName)
            {
                case HardwareID.Glare:
                    if (a320_data_glare == null)
                    {
                        a320_data_glare = new A320_Data_Glare();
                        a320_data_cdu_L = new A320_Data_CDU();
                        a320_data_cdu_R = new A320_Data_CDU();

                        status = HardwareStatus.Initialized;
                    }
                    break;
                case HardwareID.TQ:
                    if (a320_data_tq == null)
                    {
                        a320_data_tq = new A320_Data_TQ();

                        status = HardwareStatus.Initialized;
                    }
                    break;
                case HardwareID.CDU:
                    break;
                case HardwareID.YokeL:
                    if (a320_data_yoke_L == null)
                    {
                        a320_data_yoke_L = new A320_Data_FC_YOKE();

                        status = HardwareStatus.Initialized;
                    }
                    break;
                case HardwareID.YokeR:
                    if (a320_data_yoke_R == null)
                    {
                        a320_data_yoke_R = new A320_Data_FC_YOKE();

                        status = HardwareStatus.Initialized;
                    }
                    break;
                case HardwareID.Null:
                    break;

            }
            return true;
        }
        public bool Connect(ref ProSimConnect p_connection)
        {
            m_connection = p_connection;
            if(HardwareName!=HardwareID.Null&&m_portID!="COM0")
            {
                if(m_port==null)
                {
                    m_port = new SerialPort();
                    m_port.PortName = m_portID;
                    m_port.BaudRate = m_portBaudRate;
                    m_port.StopBits = StopBits.One;
                    m_port.DataBits = 8;
                    m_port.Parity = 0;
                    m_port.ReadTimeout = 1;
                }
                
                try
                {
                    if (!m_port.IsOpen)
                    {
                        m_port.Open();
                        m_port.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }



            //Glare
            if (a320_data_glare != null)
            {
                //FCU
                a320_data_glare.S_FCU_ALTITUDE = new DataRef("system.switches.S_FCU_ALTITUDE", 100, m_connection);
                a320_data_glare.S_FCU_ALTITUDE_SCALE = new DataRef("system.switches.S_FCU_ALTITUDE_SCALE", 100, m_connection);
                a320_data_glare.S_FCU_AP1 = new DataRef("system.switches.S_FCU_AP1", 100, m_connection);
                a320_data_glare.S_FCU_AP2 = new DataRef("system.switches.S_FCU_AP2", 100, m_connection);
                a320_data_glare.S_FCU_APPR = new DataRef("system.switches.S_FCU_APPR", 100, m_connection);
                a320_data_glare.S_FCU_ATHR = new DataRef("system.switches.S_FCU_ATHR", 100, m_connection);
                a320_data_glare.S_FCU_EXPED = new DataRef("system.switches.S_FCU_EXPED", 100, m_connection);
                a320_data_glare.S_FCU_HDGVS_TRKFPA = new DataRef("system.switches.S_FCU_HDGVS_TRKFPA", 100, m_connection);
                a320_data_glare.S_FCU_HEADING = new DataRef("system.switches.S_FCU_HEADING", 100, m_connection);
                a320_data_glare.S_FCU_LOC = new DataRef("system.switches.S_FCU_LOC", 100, m_connection);
                a320_data_glare.S_FCU_METRIC_ALT = new DataRef("system.switches.S_FCU_METRIC_ALT", 100, m_connection);
                a320_data_glare.S_FCU_SPD_MACH = new DataRef("system.switches.S_FCU_SPD_MACH", 100, m_connection);
                a320_data_glare.S_FCU_SPEED = new DataRef("system.switches.S_FCU_SPEED", 100, m_connection);
                a320_data_glare.S_FCU_VERTICAL_SPEED = new DataRef("system.switches.S_FCU_VERTICAL_SPEED", 100, m_connection);
                a320_data_glare.A_FCU_ALTITUDE = new DataRef("system.analog.A_FCU_ALTITUDE", 100, m_connection);
                a320_data_glare.A_FCU_HEADING = new DataRef("system.analog.A_FCU_HEADING", 100, m_connection);
                a320_data_glare.A_FCU_LIGHTING = new DataRef("system.analog.A_FCU_LIGHTING", 100, m_connection);
                a320_data_glare.A_FCU_LIGHTING_TEXT = new DataRef("system.analog.A_FCU_LIGHTING_TEXT", 100, m_connection);
                a320_data_glare.A_FCU_SPEED = new DataRef("system.analog.A_FCU_SPEED", 100, m_connection);
                a320_data_glare.A_FCU_VS = new DataRef("system.analog.A_FCU_VS", 100, m_connection);
                a320_data_glare.B_FCU_HEADING_DASHED = new DataRef("system.gates.B_FCU_HEADING_DASHED", 100, m_connection);
                a320_data_glare.B_FCU_POWER = new DataRef("system.gates.B_FCU_POWER", 100, m_connection);
                a320_data_glare.B_FCU_SPEED_DASHED = new DataRef("system.gates.B_FCU_SPEED_DASHED", 100, m_connection);
                a320_data_glare.B_FCU_SPEED_MACH = new DataRef("system.gates.B_FCU_SPEED_MACH", 100, m_connection);
                a320_data_glare.B_FCU_TRACK_FPA_MODE = new DataRef("system.gates.B_FCU_TRACK_FPA_MODE", 100, m_connection);
                a320_data_glare.B_FCU_VERTICALSPEED_DASHED = new DataRef("system.gates.B_FCU_VERTICALSPEED_DASHED", 100, m_connection);
                a320_data_glare.I_FCU_AP1 = new DataRef("system.indicators.I_FCU_AP1", 100, m_connection);
                a320_data_glare.I_FCU_AP2 = new DataRef("system.indicators.I_FCU_AP2", 100, m_connection);
                a320_data_glare.I_FCU_APPR = new DataRef("system.indicators.I_FCU_APPR", 100, m_connection);
                a320_data_glare.I_FCU_ATHR = new DataRef("system.indicators.I_FCU_ATHR", 100, m_connection);
                a320_data_glare.I_FCU_EXPED = new DataRef("system.indicators.I_FCU_EXPED", 100, m_connection);
                a320_data_glare.I_FCU_HEADING_MANAGED = new DataRef("system.indicators.I_FCU_HEADING_MANAGED", 100, m_connection);
                a320_data_glare.I_FCU_HEADING_VS_MODE = new DataRef("system.indicators.I_FCU_HEADING_VS_MODE", 100, m_connection);
                a320_data_glare.I_FCU_LOC = new DataRef("system.indicators.I_FCU_LOC", 100, m_connection);
                a320_data_glare.I_FCU_MACH_MODE = new DataRef("system.indicators.I_FCU_MACH_MODE", 100, m_connection);
                a320_data_glare.I_FCU_SPEED_MANAGED = new DataRef("system.indicators.I_FCU_SPEED_MANAGED", 100, m_connection);
                a320_data_glare.I_FCU_SPEED_MODE = new DataRef("system.indicators.I_FCU_SPEED_MODE", 100, m_connection);
                a320_data_glare.I_FCU_TRACK_FPA_MODE = new DataRef("system.indicators.I_FCU_TRACK_FPA_MODE", 100, m_connection);
                a320_data_glare.I_FCU_ALTITUDE_MANAGED = new DataRef("system.indicators.I_FCU_ALTITUDE_MANAGED", 100, m_connection);
                a320_data_glare.N_FCU_ALTITUDE = new DataRef("system.numerical.N_FCU_ALTITUDE", 100, m_connection);
                a320_data_glare.N_FCU_HEADING = new DataRef("system.numerical.N_FCU_HEADING", 100, m_connection);
                a320_data_glare.N_FCU_LIGHTING = new DataRef("system.numerical.N_FCU_LIGHTING", 100, m_connection);
                a320_data_glare.N_FCU_LIGHTING_TEXT = new DataRef("system.numerical.N_FCU_LIGHTING_TEXT", 100, m_connection);
                a320_data_glare.N_FCU_SPEED = new DataRef("system.numerical.N_FCU_SPEED", 100, m_connection);
                a320_data_glare.N_FCU_VS = new DataRef("system.numerical.N_FCU_VS", 100, m_connection);
                a320_data_glare.E_FCU_ALTITUDE = new DataRef("system.encoders.E_FCU_ALTITUDE", 100, m_connection);
                a320_data_glare.E_FCU_HEADING = new DataRef("system.encoders.E_FCU_HEADING", 100, m_connection);
                a320_data_glare.E_FCU_SPEED = new DataRef("system.encoders.E_FCU_SPEED", 100, m_connection);
                a320_data_glare.E_FCU_VS = new DataRef("system.encoders.E_FCU_VS", 100, m_connection);

                //EFIS1
                a320_data_glare.S_FCU_EFIS1_ARPT = new DataRef("system.switches.S_FCU_EFIS1_ARPT", 100, m_connection);
                a320_data_glare.S_FCU_EFIS1_BARO_MODE = new DataRef("system.switches.S_FCU_EFIS1_BARO_MODE", 100, m_connection);
                a320_data_glare.S_FCU_EFIS1_BARO_STD = new DataRef("system.switches.S_FCU_EFIS1_BARO_STD", 100, m_connection);
                a320_data_glare.S_FCU_EFIS1_CSTR = new DataRef("system.switches.S_FCU_EFIS1_CSTR", 100, m_connection);
                a320_data_glare.S_FCU_EFIS1_FD = new DataRef("system.switches.S_FCU_EFIS1_FD", 100, m_connection);
                a320_data_glare.S_FCU_EFIS1_LS = new DataRef("system.switches.S_FCU_EFIS1_LS", 100, m_connection);
                a320_data_glare.S_FCU_EFIS1_NAV1 = new DataRef("system.switches.S_FCU_EFIS1_NAV1", 100, m_connection);
                a320_data_glare.S_FCU_EFIS1_NAV2 = new DataRef("system.switches.S_FCU_EFIS1_NAV2", 100, m_connection);
                a320_data_glare.S_FCU_EFIS1_ND_MODE = new DataRef("system.switches.S_FCU_EFIS1_ND_MODE", 100, m_connection);
                a320_data_glare.S_FCU_EFIS1_ND_ZOOM = new DataRef("system.switches.S_FCU_EFIS1_ND_ZOOM", 100, m_connection);
                a320_data_glare.S_FCU_EFIS1_NDB = new DataRef("system.switches.S_FCU_EFIS1_NDB", 100, m_connection);
                a320_data_glare.S_FCU_EFIS1_VORD = new DataRef("system.switches.S_FCU_EFIS1_VORD", 100, m_connection);
                a320_data_glare.S_FCU_EFIS1_WPT = new DataRef("system.switches.S_FCU_EFIS1_WPT", 100, m_connection);
                a320_data_glare.A_FCU_EFIS1_BARO_HPA = new DataRef("system.switches.A_FCU_EFIS1_BARO_HPA", 100, m_connection);
                a320_data_glare.A_FCU_EFIS1_BARO_INCH = new DataRef("system.switches.A_FCU_EFIS1_BARO_INCH", 100, m_connection);
                a320_data_glare.B_FCU_EFIS1_BARO_INCH = new DataRef("system.gates.B_FCU_EFIS1_BARO_INCH", 100, m_connection);
                a320_data_glare.B_FCU_EFIS1_BARO_STD = new DataRef("system.gates.B_FCU_EFIS1_BARO_STD", 100, m_connection);
                a320_data_glare.I_FCU_EFIS1_ARPT = new DataRef("system.indicators.I_FCU_EFIS1_ARPT", 100, m_connection);
                a320_data_glare.I_FCU_EFIS1_CSTR = new DataRef("system.indicators.I_FCU_EFIS1_CSTR", 100, m_connection);
                a320_data_glare.I_FCU_EFIS1_FD = new DataRef("system.indicators.I_FCU_EFIS1_FD", 100, m_connection);
                a320_data_glare.I_FCU_EFIS1_LS = new DataRef("system.indicators.I_FCU_EFIS1_LS", 100, m_connection);
                a320_data_glare.I_FCU_EFIS1_NDB = new DataRef("system.indicators.I_FCU_EFIS1_NDB", 100, m_connection);
                a320_data_glare.I_FCU_EFIS1_QNH = new DataRef("system.indicators.I_FCU_EFIS1_QNH", 100, m_connection);
                a320_data_glare.I_FCU_EFIS1_VORD = new DataRef("system.indicators.I_FCU_EFIS1_VORD", 100, m_connection);
                a320_data_glare.I_FCU_EFIS1_WPT = new DataRef("system.indicators.I_FCU_EFIS1_WPT", 100, m_connection);
                a320_data_glare.N_FCU_EFIS1_BARO_HPA = new DataRef("system.numerical.N_FCU_EFIS1_BARO_HPA", 100, m_connection);
                a320_data_glare.N_FCU_EFIS1_BARO_INCH = new DataRef("system.numerical.N_FCU_EFIS1_BARO_INCH", 100, m_connection);
                a320_data_glare.E_FCU_EFIS1_BARO = new DataRef("system.encoders.E_FCU_EFIS1_BARO", 100, m_connection);

                //EFIS2
                a320_data_glare.S_FCU_EFIS2_ARPT = new DataRef("system.switches.S_FCU_EFIS2_ARPT", 100, m_connection);
                a320_data_glare.S_FCU_EFIS2_BARO_MODE = new DataRef("system.switches.S_FCU_EFIS2_BARO_MODE", 100, m_connection);
                a320_data_glare.S_FCU_EFIS2_BARO_STD = new DataRef("system.switches.S_FCU_EFIS2_BARO_STD", 100, m_connection);
                a320_data_glare.S_FCU_EFIS2_CSTR = new DataRef("system.switches.S_FCU_EFIS2_CSTR", 100, m_connection);
                a320_data_glare.S_FCU_EFIS2_FD = new DataRef("system.switches.S_FCU_EFIS2_FD", 100, m_connection);
                a320_data_glare.S_FCU_EFIS2_LS = new DataRef("system.switches.S_FCU_EFIS2_LS", 100, m_connection);
                a320_data_glare.S_FCU_EFIS2_NAV1 = new DataRef("system.switches.S_FCU_EFIS2_NAV1", 100, m_connection);
                a320_data_glare.S_FCU_EFIS2_NAV2 = new DataRef("system.switches.S_FCU_EFIS2_NAV2", 100, m_connection);
                a320_data_glare.S_FCU_EFIS2_ND_MODE = new DataRef("system.switches.S_FCU_EFIS2_ND_MODE", 100, m_connection);
                a320_data_glare.S_FCU_EFIS2_ND_ZOOM = new DataRef("system.switches.S_FCU_EFIS2_ND_ZOOM", 100, m_connection);
                a320_data_glare.S_FCU_EFIS2_NDB = new DataRef("system.switches.S_FCU_EFIS2_NDB", 100, m_connection);
                a320_data_glare.S_FCU_EFIS2_VORD = new DataRef("system.switches.S_FCU_EFIS2_VORD", 100, m_connection);
                a320_data_glare.S_FCU_EFIS2_WPT = new DataRef("system.switches.S_FCU_EFIS2_WPT", 100, m_connection);
                a320_data_glare.A_FCU_EFIS2_BARO_HPA = new DataRef("system.switches.A_FCU_EFIS2_BARO_HPA", 100, m_connection);
                a320_data_glare.A_FCU_EFIS2_BARO_INCH = new DataRef("system.switches.A_FCU_EFIS2_BARO_INCH", 100, m_connection);
                a320_data_glare.B_FCU_EFIS2_BARO_INCH = new DataRef("system.gates.B_FCU_EFIS2_BARO_INCH", 100, m_connection);
                a320_data_glare.B_FCU_EFIS2_BARO_STD = new DataRef("system.gates.B_FCU_EFIS2_BARO_STD", 100, m_connection);
                a320_data_glare.I_FCU_EFIS2_ARPT = new DataRef("system.indicators.I_FCU_EFIS2_ARPT", 100, m_connection);
                a320_data_glare.I_FCU_EFIS2_CSTR = new DataRef("system.indicators.I_FCU_EFIS2_CSTR", 100, m_connection);
                a320_data_glare.I_FCU_EFIS2_FD = new DataRef("system.indicators.I_FCU_EFIS2_FD", 100, m_connection);
                a320_data_glare.I_FCU_EFIS2_LS = new DataRef("system.indicators.I_FCU_EFIS2_LS", 100, m_connection);
                a320_data_glare.I_FCU_EFIS2_NDB = new DataRef("system.indicators.I_FCU_EFIS2_NDB", 100, m_connection);
                a320_data_glare.I_FCU_EFIS2_QNH = new DataRef("system.indicators.I_FCU_EFIS2_QNH", 100, m_connection);
                a320_data_glare.I_FCU_EFIS2_VORD = new DataRef("system.indicators.I_FCU_EFIS2_VORD", 100, m_connection);
                a320_data_glare.I_FCU_EFIS2_WPT = new DataRef("system.indicators.I_FCU_EFIS2_WPT", 100, m_connection);
                a320_data_glare.N_FCU_EFIS2_BARO_HPA = new DataRef("system.numerical.N_FCU_EFIS2_BARO_HPA", 100, m_connection);
                a320_data_glare.N_FCU_EFIS2_BARO_INCH = new DataRef("system.numerical.N_FCU_EFIS2_BARO_INCH", 100, m_connection);
                a320_data_glare.E_FCU_EFIS2_BARO = new DataRef("system.encoders.E_FCU_EFIS2_BARO", 100, m_connection);

                //WARN1
                a320_data_glare.I_CTN_WARN1_ARROW = new DataRef("system.indicators.I_FC_SIDESTICK_PRIORITY_CAPT_ARROW", 100, m_connection);
                a320_data_glare.I_CTN_WARN1_CAPT = new DataRef("system.indicators.I_FC_SIDESTICK_PRIORITY_CAPT", 100, m_connection);
                a320_data_glare.I_CTN_WARN1_CAUTION = new DataRef("system.indicators.I_MIP_MASTER_CAUTION_CAPT", 100, m_connection);
                a320_data_glare.I_CTN_WARN1_CAUTION_L = new DataRef("system.indicators.I_MIP_MASTER_CAUTION_CAPT_L", 100, m_connection);
                a320_data_glare.I_CTN_WARN1_WARNING = new DataRef("system.indicators.I_MIP_MASTER_WARNING_CAPT", 100, m_connection);
                a320_data_glare.I_CTN_WARN1_WARNING_L = new DataRef("system.indicators.I_MIP_MASTER_WARNING_CAPT_L", 100, m_connection);
                a320_data_glare.I_CTN_WARN1_AUTOLAND = new DataRef("system.indicators.I_MIP_AUTOLAND_CAPT", 100, m_connection);
                a320_data_glare.S_CTN_WARN1_CHRONO = new DataRef("system.switches.S_MIP_CHRONO_CAPT", 100, m_connection);
                a320_data_glare.S_CTN_WARN1_MASTER_CAUTION = new DataRef("system.switches.S_MIP_MASTER_CAUTION_CAPT", 100, m_connection);
                a320_data_glare.S_CTN_WARN1_MASTER_WARNING = new DataRef("system.switches.S_MIP_MASTER_WARNING_CAPT", 100, m_connection);

                //WARN2
                a320_data_glare.I_CTN_WARN2_ARROW = new DataRef("system.indicators.I_FC_SIDESTICK_PRIORITY_FO_ARROW", 100, m_connection);
                a320_data_glare.I_CTN_WARN2_CAPT = new DataRef("system.indicators.I_FC_SIDESTICK_PRIORITY_FO", 100, m_connection);
                a320_data_glare.I_CTN_WARN2_CAUTION = new DataRef("system.indicators.I_MIP_MASTER_CAUTION_FO", 100, m_connection);
                a320_data_glare.I_CTN_WARN2_CAUTION_L = new DataRef("system.indicators.I_MIP_MASTER_CAUTION_FO_L", 100, m_connection);
                a320_data_glare.I_CTN_WARN2_WARNING = new DataRef("system.indicators.I_MIP_MASTER_WARNING_FO", 100, m_connection);
                a320_data_glare.I_CTN_WARN2_WARNING_L = new DataRef("system.indicators.I_MIP_MASTER_WARNING_FO_L", 100, m_connection);
                a320_data_glare.I_CTN_WARN2_AUTOLAND = new DataRef("system.indicators.I_MIP_AUTOLAND_FO", 100, m_connection);
                a320_data_glare.S_CTN_WARN2_CHRONO = new DataRef("system.switches.S_MIP_CHRONO_FO", 100, m_connection);
                a320_data_glare.S_CTN_WARN2_MASTER_CAUTION = new DataRef("system.switches.S_MIP_MASTER_CAUTION_FO", 100, m_connection);
                a320_data_glare.S_CTN_WARN2_MASTER_WARNING = new DataRef("system.switches.S_MIP_MASTER_WARNING_FO", 100, m_connection);

                //ESP
                a320_data_glare.I_ENG_FAULT_1 = new DataRef("system.indicators.I_ENG_FAULT_1", 100, m_connection);
                a320_data_glare.I_ENG_FAULT_2 = new DataRef("system.indicators.I_ENG_FAULT_2", 100, m_connection);
                a320_data_glare.I_ENG_FIRE_1 = new DataRef("system.indicators.I_ENG_FIRE_1", 100, m_connection);
                a320_data_glare.I_ENG_FIRE_2 = new DataRef("system.indicators.I_ENG_FIRE_2", 100, m_connection);
                a320_data_glare.S_ENG_MASTER_1 = new DataRef("system.switches.S_ENG_MASTER_1", 100, m_connection);
                a320_data_glare.S_ENG_MASTER_2 = new DataRef("system.switches.S_ENG_MASTER_2", 100, m_connection);
                a320_data_glare.S_ENG_MODE = new DataRef("system.switches.S_ENG_MODE", 100, m_connection);
            }

            //TQ
            if (a320_data_tq != null)
            {
                a320_data_tq.A_FC_THROTTLE_LEFT_INPUT = new DataRef("system.analog.A_FC_THROTTLE_LEFT_INPUT", 100, m_connection);
                a320_data_tq.A_FC_THROTTLE_RIGHT_INPUT = new DataRef("system.analog.A_FC_THROTTLE_RIGHT_INPUT", 100, m_connection);
                a320_data_tq.A_FC_ELEVATOR_TRIM = new DataRef("system.analog.A_FC_ELEVATOR_TRIM", 100, m_connection);
                a320_data_tq.B_FC_ELEVATOR_TRIM_MOTOR_POWR = new DataRef("system.gates.B_FC_ELEVATOR_TRIM_MOTOR_POWER", 100, m_connection);
                a320_data_tq.FC_ELEVATOR = new DataRef("aircraft.flightControls.trim.elevator", 100, m_connection);
                a320_data_tq.S_FC_THR_INST_DISCONNECT1 = new DataRef("system.switches.S_FC_THR_INST_DISCONNECT1", 100, m_connection);
                a320_data_tq.S_FC_THR_INST_DISCONNECT2 = new DataRef("system.switches.S_FC_THR_INST_DISCONNECT2", 100, m_connection);
            }

            //CDUL
            if (a320_data_cdu_L != null)
            {
                a320_data_cdu_L.A_CDU_BRIGHTNESS = new DataRef("system.analog.A_CDU1_BRIGHTNESS", 100, m_connection);
                a320_data_cdu_L.I_CDU1_FAIL = new DataRef("system.indicators.I_CDU1_FAIL", 100, m_connection);
                a320_data_cdu_L.I_CDU1_FM = new DataRef("system.indicators.I_CDU1_FM", 100, m_connection);
                a320_data_cdu_L.I_CDU1_FM1 = new DataRef("system.indicators.I_CDU1_FM1", 100, m_connection);
                a320_data_cdu_L.I_CDU1_FM2 = new DataRef("system.indicators.I_CDU1_FM2", 100, m_connection);
                a320_data_cdu_L.I_CDU1_IND = new DataRef("system.indicators.I_CDU1_IND", 100, m_connection);
                a320_data_cdu_L.I_CDU1_MCDU_MENU = new DataRef("system.indicators.I_CDU1_MCDU_MENU", 100, m_connection);
                a320_data_cdu_L.I_CDU1_RDY = new DataRef("system.indicators.I_CDU1_RDY", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_0 = new DataRef("system.switches.S_CDU1_KEY_0", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_1 = new DataRef("system.switches.S_CDU1_KEY_1", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_2 = new DataRef("system.switches.S_CDU1_KEY_2", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_3 = new DataRef("system.switches.S_CDU1_KEY_3", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_4 = new DataRef("system.switches.S_CDU1_KEY_4", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_5 = new DataRef("system.switches.S_CDU1_KEY_5", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_6 = new DataRef("system.switches.S_CDU1_KEY_6", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_7 = new DataRef("system.switches.S_CDU1_KEY_7", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_8 = new DataRef("system.switches.S_CDU1_KEY_8", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_9 = new DataRef("system.switches.S_CDU1_KEY_9", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_A = new DataRef("system.switches.S_CDU1_KEY_A", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_AIRPORT = new DataRef("system.switches.S_CDU1_KEY_AIRPORT", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_ARROW_DOWN = new DataRef("system.switches.S_CDU1_KEY_ARROW_DOWN", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_ARROW_LEFT = new DataRef("system.switches.S_CDU1_KEY_ARROW_LEFT", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_ARROW_RIGHT = new DataRef("system.switches.S_CDU1_KEY_ARROW_RIGHT", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_ARROW_UP = new DataRef("system.switches.S_CDU1_KEY_ARROW_UP", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_ATC_COM = new DataRef("system.switches.S_CDU1_KEY_ATC_COM", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_B = new DataRef("system.switches.S_CDU1_KEY_B", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_C = new DataRef("system.switches.S_CDU1_KEY_C", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_CLEAR = new DataRef("system.switches.S_CDU1_KEY_CLEAR", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_D = new DataRef("system.switches.S_CDU1_KEY_D", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_DATA = new DataRef("system.switches.S_CDU1_KEY_DATA", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_DIR = new DataRef("system.switches.S_CDU1_KEY_DIR", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_DOT = new DataRef("system.switches.S_CDU1_KEY_DOT", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_E = new DataRef("system.switches.S_CDU1_KEY_E", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_F = new DataRef("system.switches.S_CDU1_KEY_F", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_FLPN = new DataRef("system.switches.S_CDU1_KEY_FLPN", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_FUEL_PRED = new DataRef("system.switches.S_CDU1_KEY_FUEL_PRED", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_G = new DataRef("system.switches.S_CDU1_KEY_G", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_H = new DataRef("system.switches.S_CDU1_KEY_H", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_I = new DataRef("system.switches.S_CDU1_KEY_I", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_INIT = new DataRef("system.switches.S_CDU1_KEY_INIT", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_J = new DataRef("system.switches.S_CDU1_KEY_J", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_K = new DataRef("system.switches.S_CDU1_KEY_K", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_L = new DataRef("system.switches.S_CDU1_KEY_L", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_LSK1L = new DataRef("system.switches.S_CDU1_KEY_LSK1L", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_LSK1R = new DataRef("system.switches.S_CDU1_KEY_LSK1R", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_LSK2L = new DataRef("system.switches.S_CDU1_KEY_LSK2L", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_LSK2R = new DataRef("system.switches.S_CDU1_KEY_LSK2R", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_LSK3L = new DataRef("system.switches.S_CDU1_KEY_LSK3L", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_LSK3R = new DataRef("system.switches.S_CDU1_KEY_LSK3R", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_LSK4L = new DataRef("system.switches.S_CDU1_KEY_LSK4L", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_LSK4R = new DataRef("system.switches.S_CDU1_KEY_LSK4R", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_LSK5L = new DataRef("system.switches.S_CDU1_KEY_LSK5L", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_LSK5R = new DataRef("system.switches.S_CDU1_KEY_LSK5R", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_LSK6L = new DataRef("system.switches.S_CDU1_KEY_LSK6L", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_LSK6R = new DataRef("system.switches.S_CDU1_KEY_LSK6R", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_M = new DataRef("system.switches.S_CDU1_KEY_M", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_MENU = new DataRef("system.switches.S_CDU1_KEY_MENU", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_MINUS = new DataRef("system.switches.S_CDU1_KEY_MINUS", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_N = new DataRef("system.switches.S_CDU1_KEY_N", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_O = new DataRef("system.switches.S_CDU1_KEY_O", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_OVFLY = new DataRef("system.switches.S_CDU1_KEY_OVFLY", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_P = new DataRef("system.switches.S_CDU1_KEY_P", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_PERF = new DataRef("system.switches.S_CDU1_KEY_PERF", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_PROG = new DataRef("system.switches.S_CDU1_KEY_PROG", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_Q = new DataRef("system.switches.S_CDU1_KEY_Q", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_R = new DataRef("system.switches.S_CDU1_KEY_R", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_RAD_NAV = new DataRef("system.switches.S_CDU1_KEY_RAD_NAV", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_S = new DataRef("system.switches.S_CDU1_KEY_S", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_SEC_FPLN = new DataRef("system.switches.S_CDU1_KEY_SEC_FPLN", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_SLASH = new DataRef("system.switches.S_CDU1_KEY_SLASH", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_SPACE = new DataRef("system.switches.S_CDU1_KEY_SPACE", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_T = new DataRef("system.switches.S_CDU1_KEY_T", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_U = new DataRef("system.switches.S_CDU1_KEY_U", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_V = new DataRef("system.switches.S_CDU1_KEY_V", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_W = new DataRef("system.switches.S_CDU1_KEY_W", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_X = new DataRef("system.switches.S_CDU1_KEY_X", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_Y = new DataRef("system.switches.S_CDU1_KEY_Y", 100, m_connection);
                a320_data_cdu_L.S_CDU_KEY_Z = new DataRef("system.switches.S_CDU1_KEY_Z", 100, m_connection);
            }

            //CDUR
            if (a320_data_cdu_R != null)
            {
                a320_data_cdu_R.A_CDU_BRIGHTNESS = new DataRef("system.analog.A_CDU2_BRIGHTNESS", 100, m_connection);
                a320_data_cdu_R.I_CDU2_FAIL = new DataRef("system.indicators.I_CDU2_FAIL", 100, m_connection);
                a320_data_cdu_R.I_CDU2_FM = new DataRef("system.indicators.I_CDU2_FM", 100, m_connection);
                a320_data_cdu_R.I_CDU2_FM1 = new DataRef("system.indicators.I_CDU2_FM1", 100, m_connection);
                a320_data_cdu_R.I_CDU2_FM2 = new DataRef("system.indicators.I_CDU2_FM2", 100, m_connection);
                a320_data_cdu_R.I_CDU2_IND = new DataRef("system.indicators.I_CDU2_IND", 100, m_connection);
                a320_data_cdu_R.I_CDU2_MCDU_MENU = new DataRef("system.indicators.I_CDU2_MCDU_MENU", 100, m_connection);
                a320_data_cdu_R.I_CDU2_RDY = new DataRef("system.indicators.I_CDU2_RDY", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_0 = new DataRef("system.switches.S_CDU2_KEY_0", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_1 = new DataRef("system.switches.S_CDU2_KEY_1", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_2 = new DataRef("system.switches.S_CDU2_KEY_2", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_3 = new DataRef("system.switches.S_CDU2_KEY_3", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_4 = new DataRef("system.switches.S_CDU2_KEY_4", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_5 = new DataRef("system.switches.S_CDU2_KEY_5", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_6 = new DataRef("system.switches.S_CDU2_KEY_6", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_7 = new DataRef("system.switches.S_CDU2_KEY_7", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_8 = new DataRef("system.switches.S_CDU2_KEY_8", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_9 = new DataRef("system.switches.S_CDU2_KEY_9", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_A = new DataRef("system.switches.S_CDU2_KEY_A", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_AIRPORT = new DataRef("system.switches.S_CDU2_KEY_AIRPORT", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_ARROW_DOWN = new DataRef("system.switches.S_CDU2_KEY_ARROW_DOWN", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_ARROW_LEFT = new DataRef("system.switches.S_CDU2_KEY_ARROW_LEFT", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_ARROW_RIGHT = new DataRef("system.switches.S_CDU2_KEY_ARROW_RIGHT", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_ARROW_UP = new DataRef("system.switches.S_CDU2_KEY_ARROW_UP", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_ATC_COM = new DataRef("system.switches.S_CDU2_KEY_ATC_COM", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_B = new DataRef("system.switches.S_CDU2_KEY_B", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_C = new DataRef("system.switches.S_CDU2_KEY_C", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_CLEAR = new DataRef("system.switches.S_CDU2_KEY_CLEAR", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_D = new DataRef("system.switches.S_CDU2_KEY_D", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_DATA = new DataRef("system.switches.S_CDU2_KEY_DATA", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_DIR = new DataRef("system.switches.S_CDU2_KEY_DIR", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_DOT = new DataRef("system.switches.S_CDU2_KEY_DOT", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_E = new DataRef("system.switches.S_CDU2_KEY_E", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_F = new DataRef("system.switches.S_CDU2_KEY_F", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_FLPN = new DataRef("system.switches.S_CDU2_KEY_FLPN", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_FUEL_PRED = new DataRef("system.switches.S_CDU2_KEY_FUEL_PRED", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_G = new DataRef("system.switches.S_CDU2_KEY_G", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_H = new DataRef("system.switches.S_CDU2_KEY_H", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_I = new DataRef("system.switches.S_CDU2_KEY_I", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_INIT = new DataRef("system.switches.S_CDU2_KEY_INIT", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_J = new DataRef("system.switches.S_CDU2_KEY_J", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_K = new DataRef("system.switches.S_CDU2_KEY_K", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_L = new DataRef("system.switches.S_CDU2_KEY_L", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_LSK1L = new DataRef("system.switches.S_CDU2_KEY_LSK1L", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_LSK1R = new DataRef("system.switches.S_CDU2_KEY_LSK1R", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_LSK2L = new DataRef("system.switches.S_CDU2_KEY_LSK2L", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_LSK2R = new DataRef("system.switches.S_CDU2_KEY_LSK2R", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_LSK3L = new DataRef("system.switches.S_CDU2_KEY_LSK3L", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_LSK3R = new DataRef("system.switches.S_CDU2_KEY_LSK3R", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_LSK4L = new DataRef("system.switches.S_CDU2_KEY_LSK4L", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_LSK4R = new DataRef("system.switches.S_CDU2_KEY_LSK4R", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_LSK5L = new DataRef("system.switches.S_CDU2_KEY_LSK5L", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_LSK5R = new DataRef("system.switches.S_CDU2_KEY_LSK5R", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_LSK6L = new DataRef("system.switches.S_CDU2_KEY_LSK6L", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_LSK6R = new DataRef("system.switches.S_CDU2_KEY_LSK6R", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_M = new DataRef("system.switches.S_CDU2_KEY_M", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_MENU = new DataRef("system.switches.S_CDU2_KEY_MENU", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_MINUS = new DataRef("system.switches.S_CDU2_KEY_MINUS", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_N = new DataRef("system.switches.S_CDU2_KEY_N", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_O = new DataRef("system.switches.S_CDU2_KEY_O", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_OVFLY = new DataRef("system.switches.S_CDU2_KEY_OVFLY", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_P = new DataRef("system.switches.S_CDU2_KEY_P", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_PERF = new DataRef("system.switches.S_CDU2_KEY_PERF", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_PROG = new DataRef("system.switches.S_CDU2_KEY_PROG", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_Q = new DataRef("system.switches.S_CDU2_KEY_Q", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_R = new DataRef("system.switches.S_CDU2_KEY_R", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_RAD_NAV = new DataRef("system.switches.S_CDU2_KEY_RAD_NAV", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_S = new DataRef("system.switches.S_CDU2_KEY_S", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_SEC_FPLN = new DataRef("system.switches.S_CDU2_KEY_SEC_FPLN", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_SLASH = new DataRef("system.switches.S_CDU2_KEY_SLASH", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_SPACE = new DataRef("system.switches.S_CDU2_KEY_SPACE", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_T = new DataRef("system.switches.S_CDU2_KEY_T", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_U = new DataRef("system.switches.S_CDU2_KEY_U", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_V = new DataRef("system.switches.S_CDU2_KEY_V", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_W = new DataRef("system.switches.S_CDU2_KEY_W", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_X = new DataRef("system.switches.S_CDU2_KEY_X", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_Y = new DataRef("system.switches.S_CDU2_KEY_Y", 100, m_connection);
                a320_data_cdu_R.S_CDU_KEY_Z = new DataRef("system.switches.S_CDU2_KEY_Z", 100, m_connection);
            }

            //Yoke L
            if (a320_data_yoke_L != null)
            {
                a320_data_yoke_L.A_FC_PITCH = new DataRef("system.analog.A_FC_CAPT_PITCH", 100, m_connection);
                a320_data_yoke_L.A_FC_ROLL = new DataRef("system.analog.A_FC_CAPT_ROLL", 100, m_connection);
                a320_data_yoke_L.A_FC_TILLER = new DataRef("system.analog.A_FC_CAPT_TILLER", 100, m_connection);
                a320_data_yoke_L.S_FC_DISCONNECT = new DataRef("system.switches.S_FC_CAPT_INST_DISCONNECT", 100, m_connection); //新增侧杆按键 2018.8.27
                //脚蹬
                a320_data_yoke_L.A_FC_BRAKE_LEFT = new DataRef("system.analog.A_FC_BRAKE_LEFT_CAPT", 100, m_connection);
                a320_data_yoke_L.A_FC_BRAKE_RIGHT = new DataRef("system.analog.A_FC_BRAKE_RIGHT_CAPT", 100, m_connection);
                a320_data_yoke_L.A_FC_CAPT_RUDDER = new DataRef("system.analog.A_FC_CAPT_RUDDER", 100, m_connection);

            }

            //Yoke R
            if (a320_data_yoke_R != null)
            {
                a320_data_yoke_R.A_FC_PITCH = new DataRef("system.analog.A_FC_FO_PITCH", 100, m_connection);
                a320_data_yoke_R.A_FC_ROLL = new DataRef("system.analog.A_FC_FO_ROLL", 100, m_connection);
                a320_data_yoke_R.A_FC_TILLER = new DataRef("system.analog.A_FC_FO_TILLER", 100, m_connection);
                a320_data_yoke_R.S_FC_DISCONNECT = new DataRef("system.switches.S_FC_FO_INST_DISCONNECT", 100, m_connection); //新增侧杆按键 2018.8.27
                //脚蹬
                a320_data_yoke_R.A_FC_BRAKE_LEFT = new DataRef("system.analog.A_FC_BRAKE_LEFT_FO", 100, m_connection);
                a320_data_yoke_R.A_FC_BRAKE_RIGHT = new DataRef("system.analog.A_FC_BRAKE_RIGHT_FO", 100, m_connection);
            }

            Thread.Sleep(200);
            stopthread = false;
            if (m_port != null && m_port.IsOpen)
            {
                switch (HardwareName)
                {
                    case HardwareID.Glare:
                        td_Glare.Start();
                        break;
                    case HardwareID.TQ:
                        td_TQ.Start();
                        break;
                    default:
                        break;
                }
                
            }
            

            return true;
        }
        public bool Disconnect()
        {
            stopthread = true;
            if(m_port!=null)
            {
                if (m_port.IsOpen)
                {
                    m_port.Close();
                    GC.Collect();
                }
                else
                {

                }
            }
            
            return true;
        }
        public void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            if (sp.IsOpen)
            {
                byte[] byteRead0 = new byte[sp.BytesToRead];
                sp.Read(byteRead0, 0, byteRead0.Length);
                sp.DiscardInBuffer();
                buffer.AddRange(byteRead0);
                while (buffer.Count >= 8)
                {
                    if (
                        buffer[0] == 0x90 ||
                        buffer[0] == 0x91 ||
                        buffer[0] == 0xA0 ||
                        buffer[0] == 0xA1 ||
                        buffer[0] == 0xA2 ||
                        buffer[0] == 0xA3 ||
                        buffer[0] == 0xA4 ||
                        buffer[0] == 0xA5 ||
                        buffer[0] == 0xA6 ||
                        buffer[0] == 0xA7 ||
                        buffer[0] == 0xA8 ||
                        buffer[0] == 0xA9 ||
                        buffer[0] == 0xB0 ||
                        buffer[0] == 0xB1 ||
                        buffer[0] == 0xB2 ||
                        buffer[0] == 0xB3 ||
                        buffer[0] == 0xB4 ||
                        buffer[0] == 0xB5 ||
                        buffer[0] == 0xB6 ||
                        buffer[0] == 0xB7 ||
                        buffer[0] == 0xB8 ||
                        buffer[0] == 0xB9 ||
                        buffer[0] == 0xC0 ||
                        buffer[0] == 0xC1 ||
                        buffer[0] == 0xC2 ||
                        buffer[0] == 0xC3 ||
                        buffer[0] == 0xC4 ||
                        buffer[0] == 0xC5 ||
                        buffer[0] == 0xD0 ||
                        buffer[0] == 0xD1 ||
                        buffer[0] == 0xD2 ||
                        buffer[0] == 0xD3 ||
                        buffer[0] == 0xD4 ||
                        buffer[0] == 0xE0 ||
                        buffer[0] == 0xE1 ||
                        buffer[0] == 0xE2 ||
                        buffer[0] == 0xE3 ||
                        buffer[0] == 0xE4 ||
                        buffer[0] == 0xE5 ||
                        buffer[0] == 0xE6 
                        )
                    {
                        byte[] byteRead = new byte[8];
                        buffer.CopyTo(0, byteRead, 0, 8);
                        if (buffer.Count >= 8)
                        {
                            buffer.RemoveRange(0, 8);
                        }


                        try
                        {
                            // 发动机启动面板 按键
                            if (byteRead[0] == 0x91 && byteRead.Length == 8)
                            {
                                h.H2P_ENG(byteRead, ref a320_data_glare);
                            }

                            //发动机启动面板 指示灯
                            if (byteRead[0] == 0x90 && byteRead.Length == 8)
                            {

                            }

                            //左注意警告 按键
                            if (byteRead[0] == 0xA0 && byteRead.Length == 8)
                            {
                                h.H2P_WARN_L(byteRead, ref a320_data_glare);
                            }

                            //左注意警告 指示灯
                            if (byteRead[0] == 0xA1 && byteRead.Length == 8)
                            {

                            }

                            //右注意警告 按键
                            if (byteRead[0] == 0xB0 && byteRead.Length == 8)
                            {
                                h.H2P_WARN_R(byteRead, ref a320_data_glare);
                            }

                            //右注意警告 指示灯
                            if (byteRead[0] == 0xB1 && byteRead.Length == 8)
                            {

                            }

                            //左侧杆 （侧杆操纵、转弯手轮、ca刹车）操作  2018.12.3
                            if (byteRead[0] == 0xA2 && byteRead.Length == 8)
                            {
                                h.H2P_Stick_1(byteRead, ref a320_data_yoke_L);
                            }

                            //左侧杆 （方向舵）操作 2018.12.3
                            if (byteRead[0] == 0xA8 && byteRead.Length == 8)
                            {
                                h.H2P_Stick_2(byteRead, ref a320_data_yoke_L);
                            }

                            //左侧杆 （按键）操作 2018.12.3
                            if (byteRead[0] == 0xA9 && byteRead.Length == 8)
                            {
                                h.H2P_Stick_3(byteRead, ref a320_data_yoke_L);
                            }

                            //右侧杆 操作
                            if (byteRead[0] == 0xA3 && byteRead.Length == 8)
                            {
                                h.H2P_Stick_1(byteRead, ref a320_data_yoke_R);
                            }

                            //右侧杆 （按键）操作 2018.12.3
                            if (byteRead[0] == 0xC5 && byteRead.Length == 8)
                            {
                                h.H2P_Stick_3(byteRead, ref a320_data_yoke_R);
                            }

                            //左MCDU 按键1
                            if (byteRead[0] == 0xA4 && byteRead.Length == 8)
                            {
                                h.H2P_MCDU_1(byteRead, ref a320_data_cdu_L);
                            }

                            //左MCDU 按键2
                            if (byteRead[0] == 0xA5 && byteRead.Length == 8)
                            {
                                h.H2P_MCDU_2(byteRead, ref a320_data_cdu_L);
                            }

                            //左MCDU 按键2
                            if (byteRead[0] == 0xA6 && byteRead.Length == 8)
                            {
                                h.H2P_MCDU_3(byteRead, ref a320_data_cdu_L);
                            }

                            //左MCDU 指示灯1
                            if (byteRead[0] == 0xA7 && byteRead.Length == 8)
                            {

                            }


                            //右MCDU 按键1
                            if (byteRead[0] == 0xB2 && byteRead.Length == 8)
                            {
                                h.H2P_MCDU_1(byteRead, ref a320_data_cdu_R);
                            }

                            //右MCDU 按键2
                            if (byteRead[0] == 0xB3 && byteRead.Length == 8)
                            {
                                h.H2P_MCDU_2(byteRead, ref a320_data_cdu_R);
                            }

                            //右MCDU 按键2
                            if (byteRead[0] == 0xB4 && byteRead.Length == 8)
                            {
                                h.H2P_MCDU_3(byteRead, ref a320_data_cdu_R);
                            }

                            //右MCDU 指示灯1
                            if (byteRead[0] == 0xB5 && byteRead.Length == 8)
                            {

                            }
                            //遮光罩 EFIS左 按键
                            if (byteRead[0] == 0xC0 && byteRead.Length == 8)
                            {
                                h.H2P_EFIS_L_1(byteRead, ref a320_data_glare);
                            }

                            //遮光罩 EFIS左 指示灯
                            if (byteRead[0] == 0xC1 && byteRead.Length == 8)
                            {

                            }

                            //遮光罩 EFIS左 波段开关
                            if (byteRead[0] == 0xC2 && byteRead.Length == 8)
                            {
                                h.H2P_EFIS_L_2(byteRead, ref a320_data_glare);
                            }

                            //遮光罩 EFIS左 数码管
                            if (byteRead[0] == 0xC3 && byteRead.Length == 8)
                            {

                            }

                            //遮光罩 EFIS左 编码器
                            if (byteRead[0] == 0xC4 && byteRead.Length == 8)
                            {
                                h.H2P_EFIS_L_3(byteRead, ref a320_data_glare);
                            }

                            //遮光罩 EFIS右 按键
                            if (byteRead[0] == 0xD0 && byteRead.Length == 8)
                            {
                                h.H2P_EFIS_R_1(byteRead, ref a320_data_glare);
                            }

                            //遮光罩 EFIS右 指示灯
                            if (byteRead[0] == 0xD1 && byteRead.Length == 8)
                            {

                            }

                            //遮光罩 EFIS右 波段开关
                            if (byteRead[0] == 0xD2 && byteRead.Length == 8)
                            {
                                h.H2P_EFIS_R_2(byteRead, ref a320_data_glare);
                            }

                            //遮光罩 EFIS右 数码管
                            if (byteRead[0] == 0xD3 && byteRead.Length == 8)
                            {

                            }

                            //遮光罩 EFIS右 编码器
                            if (byteRead[0] == 0xD4 && byteRead.Length == 8)
                            {
                                h.H2P_EFIS_R_3(byteRead, ref a320_data_glare);
                            }

                            //遮光罩 FCU   按键 
                            if (byteRead[0] == 0xE0 && byteRead.Length == 8)
                            {
                                h.H2P_FCU_1(byteRead, ref a320_data_glare);
                            }

                            //遮光罩 FCU   指示灯 
                            if (byteRead[0] == 0xE1 && byteRead.Length == 8)
                            {

                            }

                            //遮光罩 FCU   波段开关 
                            if (byteRead[0] == 0xE2 && byteRead.Length == 8)
                            {
                                h.H2P_FCU_2(byteRead, ref a320_data_glare);
                            }

                            //遮光罩 FCU   数码管 1
                            if (byteRead[0] == 0xE3 && byteRead.Length == 8)
                            {

                            }

                            //遮光罩 FCU   数码管 2
                            if (byteRead[0] == 0xE4 && byteRead.Length == 8)
                            {

                            }

                            //遮光罩 FCU   数码管 3
                            if (byteRead[0] == 0xE5 && byteRead.Length == 8)
                            {

                            }

                            //遮光罩 FCU   编码器
                            if (byteRead[0] == 0xE6 && byteRead.Length == 8)
                            {
                                h.H2P_FCU_3(byteRead, ref a320_data_glare);
                            }

                            //油门台   油门操作
                            if (byteRead[0] == 0xB6 && byteRead.Length == 8)
                            {
                                h.H2P_TQ1(byteRead, ref a320_data_tq);
                            }

                            //油门台   配平轮操作
                            if (byteRead[0] == 0xB7 && byteRead.Length == 8)
                            {
                                h.H2P_TQ2(byteRead, ref a320_data_tq);
                            }

                            //油门台   配平轮转动
                            if (byteRead[0] == 0xB8 && byteRead.Length == 8)
                            {

                            }

                            //油门台   配平轮停止
                            if (byteRead[0] == 0xB9 && byteRead.Length == 8)
                            {

                            }

                        }
                        catch (Exception ee)
                        {

                        }
                        GC.Collect();
                    }
                    else
                    {
                        buffer.RemoveRange(0, 1);
                    }
                }
            }
        }
        private void td_GlareSend()
        {
            while (!stopthread)
            {
                byte[] byte2send;
                byte2send = h.P2H_EFIS_L_1(ref a320_data_glare);
                m_port.Write(byte2send, 0, byte2send.Length);
                byte2send = h.P2H_EFIS_L_2(ref a320_data_glare);
                m_port.Write(byte2send, 0, byte2send.Length);

                byte2send = h.P2H_EFIS_R_1(ref a320_data_glare);
                m_port.Write(byte2send, 0, byte2send.Length);
                byte2send = h.P2H_EFIS_R_2(ref a320_data_glare);
                m_port.Write(byte2send, 0, byte2send.Length);

                byte2send = h.P2H_FCU_1(ref a320_data_glare);
                m_port.Write(byte2send, 0, byte2send.Length);
                byte2send = h.P2H_FCU_2(ref a320_data_glare);
                m_port.Write(byte2send, 0, byte2send.Length);
                byte2send = h.P2H_FCU_3(ref a320_data_glare);
                m_port.Write(byte2send, 0, byte2send.Length);
                byte2send = h.P2H_FCU_4(ref a320_data_glare);
                m_port.Write(byte2send, 0, byte2send.Length);
                byte2send = h.P2H_WARN_L(ref a320_data_glare);
                m_port.Write(byte2send, 0, byte2send.Length);
                byte2send = h.P2H_WARN_R(ref a320_data_glare);
                m_port.Write(byte2send, 0, byte2send.Length);
                byte2send = h.P2H_ENG(ref a320_data_glare);
                m_port.Write(byte2send, 0, byte2send.Length);
                //临时
                byte2send = h.P2H_MCDU_L1(ref a320_data_cdu_L);
                m_port.Write(byte2send, 0, byte2send.Length);

                byte2send = h.P2H_MCDU_R1(ref a320_data_cdu_R);
                m_port.Write(byte2send, 0, byte2send.Length);
                Thread.Sleep(interval);
            }
            td_Glare = new Thread(td_GlareSend);
            GC.Collect();
        }
        private void td_TQSend()
        {
            while (!stopthread)
            {
                byte[] byte2send;
                byte2send = h.P2H_TQ_1(ref a320_data_tq);
                m_port.Write(byte2send, 0, byte2send.Length);
                Thread.Sleep(interval);
            }
            td_TQ = new Thread(td_TQSend);
            GC.Collect();
        }
    }
}
