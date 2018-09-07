using ProSimSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardware2Prosim320
{
    class A320_Data_Glare
    {
        //FCU   IN
        public DataRef S_FCU_ALTITUDE;
        public DataRef S_FCU_ALTITUDE_SCALE;
        public DataRef S_FCU_AP1;
        public DataRef S_FCU_AP2;
        public DataRef S_FCU_APPR;
        public DataRef S_FCU_ATHR;
        public DataRef S_FCU_EXPED;
        public DataRef S_FCU_HDGVS_TRKFPA;
        public DataRef S_FCU_HEADING;
        public DataRef S_FCU_LOC;
        public DataRef S_FCU_METRIC_ALT;
        public DataRef S_FCU_SPD_MACH;
        public DataRef S_FCU_SPEED;
        public DataRef S_FCU_VERTICAL_SPEED;
        public DataRef A_FCU_ALTITUDE;
        public DataRef A_FCU_HEADING;
        public DataRef A_FCU_LIGHTING;
        public DataRef A_FCU_LIGHTING_TEXT;
        public DataRef A_FCU_SPEED;
        public DataRef A_FCU_VS;
        public DataRef B_FCU_HEADING_DASHED;
        public DataRef B_FCU_POWER;
        public DataRef B_FCU_SPEED_DASHED;
        public DataRef B_FCU_SPEED_MACH;
        public DataRef B_FCU_TRACK_FPA_MODE;
        public DataRef B_FCU_VERTICALSPEED_DASHED;
        public DataRef I_FCU_AP1;
        public DataRef I_FCU_AP2;
        public DataRef I_FCU_APPR;
        public DataRef I_FCU_ATHR;
        public DataRef I_FCU_EXPED;
        public DataRef I_FCU_HEADING_MANAGED;
        public DataRef I_FCU_HEADING_VS_MODE;
        public DataRef I_FCU_LOC;
        public DataRef I_FCU_MACH_MODE;
        public DataRef I_FCU_SPEED_MANAGED;
        public DataRef I_FCU_SPEED_MODE;
        public DataRef I_FCU_TRACK_FPA_MODE;
        public DataRef I_FCU_ALTITUDE_MANAGED;
        public DataRef N_FCU_ALTITUDE;
        public DataRef N_FCU_HEADING;
        public DataRef N_FCU_LIGHTING;
        public DataRef N_FCU_LIGHTING_TEXT;
        public DataRef N_FCU_SPEED;
        public DataRef N_FCU_VS;
        public DataRef E_FCU_ALTITUDE;
        public DataRef E_FCU_HEADING;
        public DataRef E_FCU_SPEED;
        public DataRef E_FCU_VS;


        //EFIS1 IN
        public DataRef S_FCU_EFIS1_ARPT;
        public DataRef S_FCU_EFIS1_BARO_MODE;
        public DataRef S_FCU_EFIS1_BARO_STD;
        public DataRef S_FCU_EFIS1_CSTR;
        public DataRef S_FCU_EFIS1_FD;
        public DataRef S_FCU_EFIS1_LS;
        public DataRef S_FCU_EFIS1_NAV1;
        public DataRef S_FCU_EFIS1_NAV2;
        public DataRef S_FCU_EFIS1_ND_MODE;
        public DataRef S_FCU_EFIS1_ND_ZOOM;
        public DataRef S_FCU_EFIS1_NDB;
        public DataRef S_FCU_EFIS1_VORD;
        public DataRef S_FCU_EFIS1_WPT;
        public DataRef A_FCU_EFIS1_BARO_HPA;
        public DataRef A_FCU_EFIS1_BARO_INCH;
        public DataRef B_FCU_EFIS1_BARO_INCH;
        public DataRef B_FCU_EFIS1_BARO_STD;
        public DataRef I_FCU_EFIS1_ARPT;
        public DataRef I_FCU_EFIS1_CSTR;
        public DataRef I_FCU_EFIS1_FD;
        public DataRef I_FCU_EFIS1_LS;
        public DataRef I_FCU_EFIS1_NDB;
        public DataRef I_FCU_EFIS1_QNH;
        public DataRef I_FCU_EFIS1_VORD;
        public DataRef I_FCU_EFIS1_WPT;
        public DataRef N_FCU_EFIS1_BARO_HPA;
        public DataRef N_FCU_EFIS1_BARO_INCH;
        public DataRef E_FCU_EFIS1_BARO;

        //EFIS1 IN
        public DataRef S_FCU_EFIS2_ARPT;
        public DataRef S_FCU_EFIS2_BARO_MODE;
        public DataRef S_FCU_EFIS2_BARO_STD;
        public DataRef S_FCU_EFIS2_CSTR;
        public DataRef S_FCU_EFIS2_FD;
        public DataRef S_FCU_EFIS2_LS;
        public DataRef S_FCU_EFIS2_NAV1;
        public DataRef S_FCU_EFIS2_NAV2;
        public DataRef S_FCU_EFIS2_ND_MODE;
        public DataRef S_FCU_EFIS2_ND_ZOOM;
        public DataRef S_FCU_EFIS2_NDB;
        public DataRef S_FCU_EFIS2_VORD;
        public DataRef S_FCU_EFIS2_WPT;
        public DataRef A_FCU_EFIS2_BARO_HPA;
        public DataRef A_FCU_EFIS2_BARO_INCH;
        public DataRef B_FCU_EFIS2_BARO_INCH;
        public DataRef B_FCU_EFIS2_BARO_STD;
        public DataRef I_FCU_EFIS2_ARPT;
        public DataRef I_FCU_EFIS2_CSTR;
        public DataRef I_FCU_EFIS2_FD;
        public DataRef I_FCU_EFIS2_LS;
        public DataRef I_FCU_EFIS2_NDB;
        public DataRef I_FCU_EFIS2_QNH;
        public DataRef I_FCU_EFIS2_VORD;
        public DataRef I_FCU_EFIS2_WPT;
        public DataRef N_FCU_EFIS2_BARO_HPA;
        public DataRef N_FCU_EFIS2_BARO_INCH;
        public DataRef E_FCU_EFIS2_BARO;

        //WARN1 IN
        public DataRef I_CTN_WARN1_ARROW;
        public DataRef I_CTN_WARN1_CAPT;
        public DataRef I_CTN_WARN1_CAUTION;
        public DataRef I_CTN_WARN1_CAUTION_L;
        public DataRef I_CTN_WARN1_WARNING;
        public DataRef I_CTN_WARN1_WARNING_L;
        public DataRef I_CTN_WARN1_AUTOLAND;
        public DataRef S_CTN_WARN1_CHRONO;
        public DataRef S_CTN_WARN1_MASTER_CAUTION;
        public DataRef S_CTN_WARN1_MASTER_WARNING;

        //WARN2 IN
        public DataRef I_CTN_WARN2_ARROW;
        public DataRef I_CTN_WARN2_CAPT;
        public DataRef I_CTN_WARN2_CAUTION;
        public DataRef I_CTN_WARN2_CAUTION_L;
        public DataRef I_CTN_WARN2_WARNING;
        public DataRef I_CTN_WARN2_WARNING_L;
        public DataRef I_CTN_WARN2_AUTOLAND;
        public DataRef S_CTN_WARN2_CHRONO;
        public DataRef S_CTN_WARN2_MASTER_CAUTION;
        public DataRef S_CTN_WARN2_MASTER_WARNING;

    }
    class A320_Data_TQ
    {
        public DataRef A_FC_THROTTLE_LEFT_INPUT;
        public DataRef A_FC_THROTTLE_RIGHT_INPUT;
    }
    class A320_Data_CDU
    {
        public DataRef A_CDU_BRIGHTNESS;
        public DataRef I_CDU_FAIL;
        public DataRef I_CDU_FM;
        public DataRef I_CDU_FM1;
        public DataRef I_CDU_FM2;
        public DataRef I_CDU_IND;
        public DataRef I_CDU_MCDU_MENU;
        public DataRef I_CDU_RDY;
        public DataRef S_CDU_KEY_0;
        public DataRef S_CDU_KEY_1;
        public DataRef S_CDU_KEY_2;
        public DataRef S_CDU_KEY_3;
        public DataRef S_CDU_KEY_4;
        public DataRef S_CDU_KEY_5;
        public DataRef S_CDU_KEY_6;
        public DataRef S_CDU_KEY_7;
        public DataRef S_CDU_KEY_8;
        public DataRef S_CDU_KEY_9;
        public DataRef S_CDU_KEY_A;
        public DataRef S_CDU_KEY_AIRPORT;
        public DataRef S_CDU_KEY_ARROW_DOWN;
        public DataRef S_CDU_KEY_ARROW_LEFT;        //???
        public DataRef S_CDU_KEY_ARROW_RIGHT;
        public DataRef S_CDU_KEY_ARROW_UP;
        public DataRef S_CDU_KEY_ATC_COM;
        public DataRef S_CDU_KEY_B;
        public DataRef S_CDU_KEY_C;
        public DataRef S_CDU_KEY_CLEAR;
        public DataRef S_CDU_KEY_D;
        public DataRef S_CDU_KEY_DATA;
        public DataRef S_CDU_KEY_DIR;
        public DataRef S_CDU_KEY_DOT;
        public DataRef S_CDU_KEY_E;
        public DataRef S_CDU_KEY_F;
        public DataRef S_CDU_KEY_FLPN;
        public DataRef S_CDU_KEY_FUEL_PRED;
        public DataRef S_CDU_KEY_G;
        public DataRef S_CDU_KEY_H;
        public DataRef S_CDU_KEY_I;
        public DataRef S_CDU_KEY_INIT;
        public DataRef S_CDU_KEY_J;
        public DataRef S_CDU_KEY_K;
        public DataRef S_CDU_KEY_L;
        public DataRef S_CDU_KEY_LSK1L;
        public DataRef S_CDU_KEY_LSK1R;
        public DataRef S_CDU_KEY_LSK2L;
        public DataRef S_CDU_KEY_LSK2R;
        public DataRef S_CDU_KEY_LSK3L;
        public DataRef S_CDU_KEY_LSK3R;
        public DataRef S_CDU_KEY_LSK4L;
        public DataRef S_CDU_KEY_LSK4R;
        public DataRef S_CDU_KEY_LSK5L;
        public DataRef S_CDU_KEY_LSK5R;
        public DataRef S_CDU_KEY_LSK6L;
        public DataRef S_CDU_KEY_LSK6R;
        public DataRef S_CDU_KEY_M;
        public DataRef S_CDU_KEY_MENU;
        public DataRef S_CDU_KEY_MINUS;
        public DataRef S_CDU_KEY_N;
        public DataRef S_CDU_KEY_O;
        public DataRef S_CDU_KEY_OVFLY;
        public DataRef S_CDU_KEY_P;
        public DataRef S_CDU_KEY_PERF;
        public DataRef S_CDU_KEY_PROG;
        public DataRef S_CDU_KEY_Q;
        public DataRef S_CDU_KEY_R;
        public DataRef S_CDU_KEY_RAD_NAV;
        public DataRef S_CDU_KEY_S;
        public DataRef S_CDU_KEY_SEC_FPLN;
        public DataRef S_CDU_KEY_SLASH;
        public DataRef S_CDU_KEY_SPACE;     //???
        public DataRef S_CDU_KEY_T;
        public DataRef S_CDU_KEY_U;
        public DataRef S_CDU_KEY_V;
        public DataRef S_CDU_KEY_W;
        public DataRef S_CDU_KEY_X;
        public DataRef S_CDU_KEY_Y;
        public DataRef S_CDU_KEY_Z;
    }
    class A320_Data_FC_YOKE
    {
        public DataRef A_FC_PITCH;
        public DataRef A_FC_ROLL;
        public DataRef A_FC_TILLER;
        public DataRef S_FC_DISCONNECT;
    }
}
