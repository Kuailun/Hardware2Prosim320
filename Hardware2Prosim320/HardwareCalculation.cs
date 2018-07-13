using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardware2Prosim320
{
    class HardwareCalculation
    {
        /// <summary>
        /// 右侧杆 操作
        /// </summary>
        /// <param name="p_byteRead"></param>
        /// <param name="p_data"></param>
        public void H2P_StickR(byte[] p_byteRead, ref A320_Data_FC_YOKE p_data)
        {
            int UpDown=p_byteRead[1];
            int LeftRight=p_byteRead[2];

            UpDown = Unsigned2Signed(UpDown);
            LeftRight = Unsigned2Signed(LeftRight);

            UpDown = 512 - UpDown * 32;
            LeftRight = 512 - LeftRight * 25;

            //计算完成后赋值给Prosim变量
            p_data.A_FC_PITCH.value = UpDown;
            p_data.A_FC_ROLL.value = LeftRight;
        }

        /// <summary>
        /// 左MCDU 按键1
        /// </summary>
        /// <param name="p_byteRead"></param>
        /// <param name="p_data"></param>
        public void H2P_MCDU_L_1(byte[] p_byteRead, ref A320_Data_CDU p_data)
        {
            int[] state = new int[4];

            //KB1
            state = SplitData(p_byteRead[1]);
            p_data.S_CDU_KEY_DIR.value = 1 - state[3];
            p_data.S_CDU_KEY_PROG.value = 1 - state[2];
            p_data.S_CDU_KEY_PERF.value = 1 - state[1];
            p_data.S_CDU_KEY_INIT.value = 1 - state[0];

            //KB2
            state = SplitData(p_byteRead[2]);
            p_data.S_CDU_KEY_DATA.value = 1 - state[3];
            //p_data..value = 1 - state[2];     BRT
            p_data.S_CDU_KEY_FLPN.value = 1 - state[1];
            p_data.S_CDU_KEY_RAD_NAV.value = 1 - state[0];

            //KB3
            state = SplitData(p_byteRead[3]);
            p_data.S_CDU_KEY_FUEL_PRED.value = 1 - state[3];
            p_data.S_CDU_KEY_SEC_FPLN.value = 1 - state[2];
            p_data.S_CDU_KEY_ATC_COM.value = 1 - state[1];
            p_data.S_CDU_KEY_MENU.value = 1 - state[0];

            //KB4
            state = SplitData(p_byteRead[4]);
            //p_data..value = 1 - state[3];     DIM
            p_data.S_CDU_KEY_AIRPORT.value = 1 - state[2];
            p_data.S_CDU_KEY_ARROW_LEFT.value = 1 - state[1];
            p_data.S_CDU_KEY_ARROW_UP.value = 1 - state[0];

            //KB5
            state = SplitData(p_byteRead[5]);
            p_data.S_CDU_KEY_ARROW_RIGHT.value = 1 - state[3];
            p_data.S_CDU_KEY_ARROW_DOWN.value = 1 - state[2];
            p_data.S_CDU_KEY_A.value = 1 - state[1];
            p_data.S_CDU_KEY_B.value = 1 - state[0];

            //KB6
            state = SplitData(p_byteRead[6]);
            p_data.S_CDU_KEY_C.value = 1 - state[3];
            p_data.S_CDU_KEY_D.value = 1 - state[2];
            p_data.S_CDU_KEY_E.value = 1 - state[1];
            p_data.S_CDU_KEY_F.value = 1 - state[0];

            //KB7
            state = SplitData(p_byteRead[7]);
            p_data.S_CDU_KEY_G.value = 1 - state[3];
            p_data.S_CDU_KEY_H.value = 1 - state[2];
            p_data.S_CDU_KEY_I.value = 1 - state[1];
            p_data.S_CDU_KEY_J.value = 1 - state[0];
        }

        /// <summary>
        /// 左MCDU 按键2
        /// </summary>
        /// <param name="p_byteRead"></param>
        /// <param name="p_data"></param>
        public void H2P_MCDU_L_2(byte[] p_byteRead, ref A320_Data_CDU p_data)
        {
            int[] state = new int[4];

            //KB8
            state = SplitData(p_byteRead[1]);
            p_data.S_CDU_KEY_K.value = 1 - state[3];
            p_data.S_CDU_KEY_L.value = 1 - state[2];
            p_data.S_CDU_KEY_M.value = 1 - state[1];
            p_data.S_CDU_KEY_N.value = 1 - state[0];

            //KB9
            state = SplitData(p_byteRead[2]);
            p_data.S_CDU_KEY_O.value = 1 - state[3];
            p_data.S_CDU_KEY_P.value = 1 - state[2];
            p_data.S_CDU_KEY_Q.value = 1 - state[1];
            p_data.S_CDU_KEY_R.value = 1 - state[0];

            //KB10
            state = SplitData(p_byteRead[3]);
            p_data.S_CDU_KEY_S.value = 1 - state[3];
            p_data.S_CDU_KEY_T.value = 1 - state[2];
            p_data.S_CDU_KEY_U.value = 1 - state[1];
            p_data.S_CDU_KEY_V.value = 1 - state[0];

            //KB11
            state = SplitData(p_byteRead[4]);
            p_data.S_CDU_KEY_W.value = 1 - state[3];
            p_data.S_CDU_KEY_X.value = 1 - state[2];
            p_data.S_CDU_KEY_Y.value = 1 - state[1];
            p_data.S_CDU_KEY_Z.value = 1 - state[0];

            //KB12
            state = SplitData(p_byteRead[5]);
            p_data.S_CDU_KEY_SLASH.value = 1 - state[3];
            p_data.S_CDU_KEY_SPACE.value = 1 - state[2];
            p_data.S_CDU_KEY_OVFLY.value = 1 - state[1];
            p_data.S_CDU_KEY_CLEAR.value = 1 - state[0];

            //KB13
            state = SplitData(p_byteRead[6]);
            p_data.S_CDU_KEY_1.value = 1 - state[3];
            p_data.S_CDU_KEY_2.value = 1 - state[2];
            p_data.S_CDU_KEY_3.value = 1 - state[1];
            p_data.S_CDU_KEY_4.value = 1 - state[0];

            //KB14
            state = SplitData(p_byteRead[7]);
            p_data.S_CDU_KEY_5.value = 1 - state[3];
            p_data.S_CDU_KEY_6.value = 1 - state[2];
            p_data.S_CDU_KEY_7.value = 1 - state[1];
            p_data.S_CDU_KEY_8.value = 1 - state[0];
        }

        /// <summary>
        /// 左MCDU 按键3
        /// </summary>
        /// <param name="p_byteRead"></param>
        /// <param name="p_data"></param>
        public void H2P_MCDU_L_3(byte[] p_byteRead, ref A320_Data_CDU p_data)
        {
            int[] state = new int[4];

            //KB15
            state = SplitData(p_byteRead[1]);
            p_data.S_CDU_KEY_9.value = 1 - state[3];
            p_data.S_CDU_KEY_DOT.value = 1 - state[2];
            p_data.S_CDU_KEY_0.value = 1 - state[1];
            p_data.S_CDU_KEY_MINUS.value = 1 - state[0];

            //KB16
            state = SplitData(p_byteRead[2]);
            p_data.S_CDU_KEY_LSK1L.value = 1 - state[3];
            p_data.S_CDU_KEY_LSK2L.value = 1 - state[2];
            p_data.S_CDU_KEY_LSK3L.value = 1 - state[1];
            p_data.S_CDU_KEY_LSK4L.value = 1 - state[0];

            //KB17
            state = SplitData(p_byteRead[3]);
            p_data.S_CDU_KEY_LSK5L.value = 1 - state[3];
            p_data.S_CDU_KEY_LSK6L.value = 1 - state[2];
            p_data.S_CDU_KEY_LSK1R.value = 1 - state[1];
            p_data.S_CDU_KEY_LSK2R.value = 1 - state[0];

            //KB18
            state = SplitData(p_byteRead[4]);
            p_data.S_CDU_KEY_LSK3R.value = 1 - state[3];
            p_data.S_CDU_KEY_LSK4R.value = 1 - state[2];
            p_data.S_CDU_KEY_LSK5R.value = 1 - state[1];
            p_data.S_CDU_KEY_LSK6R.value = 1 - state[0];
        }

        /// <summary>
        /// 遮光罩 EFIS左 按键
        /// </summary>
        /// <param name="p_byteRead"></param>
        /// <param name="p_data"></param>
        public void H2P_EFIS_L_1(byte[] p_byteRead, ref A320_Data_Glare p_data)
        {
            int[] state = new int[4];

            //KB1
            state = SplitData(p_byteRead[1]);
            p_data.S_FCU_EFIS1_CSTR.value = 1 - state[3];
            p_data.S_FCU_EFIS1_WPT.value = 1 - state[2];
            p_data.S_FCU_EFIS1_VORD.value = 1 - state[1];
            p_data.S_FCU_EFIS1_NDB.value = 1 - state[0];

            //KB2
            state = SplitData(p_byteRead[2]);
            p_data.S_FCU_EFIS1_ARPT.value = 1 - state[3];
            p_data.S_FCU_EFIS1_FD.value = 1 - state[2];
            p_data.S_FCU_EFIS1_LS.value = 1 - state[1];
            if(1-state[0]==1)//触发了
            {
                p_data.S_FCU_EFIS1_BARO_STD.value = 2;
            }            
            else
            {
                p_data.S_FCU_EFIS1_BARO_STD.value = 0;
            }

            //KB3
            state = SplitData(p_byteRead[3]);
            if (1 - state[3] == 1)//触发了
            {
                p_data.S_FCU_EFIS1_BARO_STD.value = 1;
            }
            else
            {
                p_data.S_FCU_EFIS1_BARO_STD.value = 0;
            }

        }

        /// <summary>
        /// 遮光罩 EFIS左 波段开关
        /// </summary>
        /// <param name="p_byteRead"></param>
        /// <param name="p_data"></param>
        public void H2P_EFIS_L_2(byte[] p_byteRead, ref A320_Data_Glare p_data)
        {
            if (p_byteRead[1] == 0x01)  p_data.S_FCU_EFIS1_BARO_MODE.value = 0; //inHg
            else                        p_data.S_FCU_EFIS1_BARO_MODE.value = 1; //hPa

            if (p_byteRead[2] == 0x01)      p_data.S_FCU_EFIS1_ND_MODE.value = 0; //ILS
            else if (p_byteRead[2] == 0x02) p_data.S_FCU_EFIS1_ND_MODE.value = 1; //VOR
            else if (p_byteRead[2] == 0x04) p_data.S_FCU_EFIS1_ND_MODE.value = 2; //NAV
            else if (p_byteRead[2] == 0x08) p_data.S_FCU_EFIS1_ND_MODE.value = 3; //ARC
            else if (p_byteRead[2] == 0x10) p_data.S_FCU_EFIS1_ND_MODE.value = 4; //PLAN

            if (p_byteRead[3] == 0x01)      p_data.S_FCU_EFIS1_ND_ZOOM.value = 0; //10
            else if (p_byteRead[3] == 0x02) p_data.S_FCU_EFIS1_ND_ZOOM.value = 1; //20
            else if (p_byteRead[3] == 0x04) p_data.S_FCU_EFIS1_ND_ZOOM.value = 2; //40
            else if (p_byteRead[3] == 0x08) p_data.S_FCU_EFIS1_ND_ZOOM.value = 3; //80
            else if (p_byteRead[3] == 0x10) p_data.S_FCU_EFIS1_ND_ZOOM.value = 4; //160
            else if (p_byteRead[3] == 0x20) p_data.S_FCU_EFIS1_ND_ZOOM.value = 5; //320

            if (p_byteRead[4] == 0x01)      p_data.S_FCU_EFIS1_NAV1.value = 0; //OFF
            else if (p_byteRead[4] == 0x02) p_data.S_FCU_EFIS1_NAV1.value = 1; //ADF
            else if (p_byteRead[4] == 0x04) p_data.S_FCU_EFIS1_NAV1.value = 2; //VOR

            if (p_byteRead[5] == 0x01)      p_data.S_FCU_EFIS1_NAV2.value = 0; //OFF
            else if (p_byteRead[5] == 0x02) p_data.S_FCU_EFIS1_NAV2.value = 1; //ADF
            else if (p_byteRead[5] == 0x04) p_data.S_FCU_EFIS1_NAV2.value = 2; //VOR
        }

        /// <summary>
        /// 遮光罩 EFIS左 编码器
        /// </summary>
        /// <param name="p_byteRead"></param>
        /// <param name="p_data"></param>
        public void H2P_EFIS_L_3(byte[] p_byteRead, ref A320_Data_Glare p_data)
        {
            if (p_byteRead[1] > 100)    p_data.E_FCU_EFIS1_BARO.value = 1;
            else if(p_byteRead[1]<100)  p_data.E_FCU_EFIS1_BARO.value = -1;
            else                        p_data.E_FCU_EFIS1_BARO.value = 0;
        }

        /// <summary>
        /// 遮光罩 EFIS右 按键
        /// </summary>
        /// <param name="p_byteRead"></param>
        /// <param name="p_data"></param>
        public void H2P_EFIS_R_1(byte[] p_byteRead, ref A320_Data_Glare p_data)
        {
            int[] state = new int[4];

            //KB1
            state = SplitData(p_byteRead[1]);
            p_data.S_FCU_EFIS2_CSTR.value = 1 - state[3];
            p_data.S_FCU_EFIS2_WPT.value = 1 - state[2];
            p_data.S_FCU_EFIS2_VORD.value = 1 - state[1];
            p_data.S_FCU_EFIS2_NDB.value = 1 - state[0];

            //KB2
            state = SplitData(p_byteRead[2]);
            p_data.S_FCU_EFIS2_ARPT.value = 1 - state[3];
            p_data.S_FCU_EFIS2_FD.value = 1 - state[2];
            p_data.S_FCU_EFIS2_LS.value = 1 - state[1];
            if (1 - state[0] == 1)//触发了
            {
                p_data.S_FCU_EFIS2_BARO_STD.value = 2;
            }
            else
            {
                p_data.S_FCU_EFIS2_BARO_STD.value = 0;
            }

            //KB3
            state = SplitData(p_byteRead[3]);
            if (1 - state[3] == 1)//触发了
            {
                p_data.S_FCU_EFIS2_BARO_STD.value = 1;
            }
            else
            {
                p_data.S_FCU_EFIS2_BARO_STD.value = 0;
            }

        }

        /// <summary>
        /// 遮光罩 EFIS右 波段开关
        /// </summary>
        /// <param name="p_byteRead"></param>
        /// <param name="p_data"></param>
        public void H2P_EFIS_R_2(byte[] p_byteRead, ref A320_Data_Glare p_data)
        {
            if (p_byteRead[1] == 0x01)      p_data.S_FCU_EFIS2_BARO_MODE.value = 0; //inHg
            else                            p_data.S_FCU_EFIS2_BARO_MODE.value = 1; //hPa

            if (p_byteRead[2] == 0x01)      p_data.S_FCU_EFIS2_ND_MODE.value = 0; //ILS
            else if (p_byteRead[2] == 0x02) p_data.S_FCU_EFIS2_ND_MODE.value = 1; //VOR
            else if (p_byteRead[2] == 0x04) p_data.S_FCU_EFIS2_ND_MODE.value = 2; //NAV
            else if (p_byteRead[2] == 0x08) p_data.S_FCU_EFIS2_ND_MODE.value = 3; //ARC
            else if (p_byteRead[2] == 0x10) p_data.S_FCU_EFIS2_ND_MODE.value = 4; //PLAN

            if (p_byteRead[3] == 0x01)      p_data.S_FCU_EFIS2_ND_ZOOM.value = 0; //10
            else if (p_byteRead[3] == 0x02) p_data.S_FCU_EFIS2_ND_ZOOM.value = 1; //20
            else if (p_byteRead[3] == 0x04) p_data.S_FCU_EFIS2_ND_ZOOM.value = 2; //40
            else if (p_byteRead[3] == 0x08) p_data.S_FCU_EFIS2_ND_ZOOM.value = 3; //80
            else if (p_byteRead[3] == 0x10) p_data.S_FCU_EFIS2_ND_ZOOM.value = 4; //160
            else if (p_byteRead[3] == 0x20) p_data.S_FCU_EFIS2_ND_ZOOM.value = 5; //320

            if (p_byteRead[4] == 0x01)      p_data.S_FCU_EFIS2_NAV1.value = 0; //OFF
            else if (p_byteRead[4] == 0x02) p_data.S_FCU_EFIS2_NAV1.value = 1; //ADF
            else if (p_byteRead[4] == 0x04) p_data.S_FCU_EFIS2_NAV1.value = 2; //VOR

            if (p_byteRead[5] == 0x01)      p_data.S_FCU_EFIS2_NAV2.value = 0; //OFF
            else if (p_byteRead[5] == 0x02) p_data.S_FCU_EFIS2_NAV2.value = 1; //ADF
            else if (p_byteRead[5] == 0x04) p_data.S_FCU_EFIS2_NAV2.value = 2; //VOR
        }

        /// <summary>
        /// 遮光罩 EFIS右 编码器
        /// </summary>
        /// <param name="p_byteRead"></param>
        /// <param name="p_data"></param>
        public void H2P_EFIS_R_3(byte[] p_byteRead, ref A320_Data_Glare p_data)
        {
            if (p_byteRead[1] > 100)        p_data.E_FCU_EFIS2_BARO.value = 1;
            else if (p_byteRead[1] < 100)   p_data.E_FCU_EFIS2_BARO.value = -1;
            else                            p_data.E_FCU_EFIS2_BARO.value = 0;
        }

        /// <summary>
        /// 遮光罩 FCU   按键 
        /// </summary>
        /// <param name="p_byteRead"></param>
        /// <param name="p_data"></param>
        public void H2P_FCU_1(byte[] p_byteRead,ref A320_Data_Glare p_data)
        {
            int[] state = new int[4];

            //KB1
            state = SplitData(p_byteRead[1]);
            p_data.S_FCU_SPD_MACH.value = 1 - state[3];
            p_data.S_FCU_HDGVS_TRKFPA.value = 1 - state[2];

            if (1 - state[1] == 1)//触发了
            {
                p_data.S_FCU_SPEED.value = 1;
            }
            else
            {
                p_data.S_FCU_SPEED.value = 0;
            }
            if (1 - state[0] == 1)//触发了
            {
                p_data.S_FCU_SPEED.value = 2;
            }
            else
            {
                p_data.S_FCU_SPEED.value = 0;
            }

            //KB2
            state = SplitData(p_byteRead[1]);
            if (1 - state[3] == 1)//触发了
            {
                p_data.S_FCU_HEADING.value = 1;
            }
            else
            {
                p_data.S_FCU_HEADING.value = 0;
            }
            if (1 - state[2] == 1)//触发了
            {
                p_data.S_FCU_HEADING.value = 2;
            }
            else
            {
                p_data.S_FCU_HEADING.value = 0;
            }
            if (1 - state[1] == 1)//触发了
            {
                p_data.S_FCU_ALTITUDE.value = 1;
            }
            else
            {
                p_data.S_FCU_ALTITUDE.value = 0;
            }
            if (1 - state[0] == 1)//触发了
            {
                p_data.S_FCU_ALTITUDE.value = 2;
            }
            else
            {
                p_data.S_FCU_ALTITUDE.value = 0;
            }

            //KB3
            state = SplitData(p_byteRead[1]);
            if (1 - state[3] == 1)//触发了
            {
                p_data.S_FCU_VERTICAL_SPEED.value = 1;
            }
            else
            {
                p_data.S_FCU_VERTICAL_SPEED.value = 0;
            }
            if (1 - state[2] == 1)//触发了
            {
                p_data.S_FCU_VERTICAL_SPEED.value = 2;
            }
            else
            {
                p_data.S_FCU_VERTICAL_SPEED.value = 0;
            }
            p_data.S_FCU_AP1.value = 1 - state[1];
            p_data.S_FCU_AP2.value = 1 - state[0];

            //KB4
            p_data.S_FCU_LOC.value = 1 - state[3];
            p_data.S_FCU_ATHR.value = 1 - state[2];
            p_data.S_FCU_EXPED.value = 1 - state[1];
            p_data.S_FCU_APPR.value = 1 - state[0];

            //KB5
            p_data.S_FCU_METRIC_ALT.value = 1 - state[3];
        }

        /// <summary>
        /// 遮光罩 FCU   波段开关
        /// </summary>
        /// <param name="p_byteRead"></param>
        /// <param name="p_data"></param>
        public void H2P_FCU_2(byte[] p_byteRead, ref A320_Data_Glare p_data)
        {
            if (p_byteRead[1] == 0x01)  p_data.S_FCU_ALTITUDE_SCALE.value = 0; //100
            else                        p_data.S_FCU_ALTITUDE_SCALE.value = 1; //1000
        }

        /// <summary>
        /// 遮光罩 FCU   编码器
        /// </summary>
        /// <param name="p_byteRead"></param>
        /// <param name="p_data"></param>
        public void H2P_FCU_3(byte[] p_byteRead, ref A320_Data_Glare p_data)
        {
            if (p_byteRead[1] > 100)        p_data.E_FCU_SPEED.value = 1;
            else if (p_byteRead[1] < 100)   p_data.E_FCU_SPEED.value = -1;
            else                            p_data.E_FCU_SPEED.value = 0;

            if (p_byteRead[2] > 100)        p_data.E_FCU_HEADING.value = 1;
            else if (p_byteRead[2] < 100)   p_data.E_FCU_HEADING.value = -1;
            else                            p_data.E_FCU_HEADING.value = 0;

            if (p_byteRead[3] > 100)        p_data.E_FCU_ALTITUDE.value = 1;
            else if (p_byteRead[3] < 100)   p_data.E_FCU_ALTITUDE.value = -1;
            else                            p_data.E_FCU_ALTITUDE.value = 0;

            if (p_byteRead[4] > 100)        p_data.E_FCU_VS.value = 1;
            else if (p_byteRead[4] < 100)   p_data.E_FCU_VS.value = -1;
            else                            p_data.E_FCU_VS.value = 0;
        }

        /// <summary>
        /// 油门台   油门操作
        /// </summary>
        /// <param name="p_byteRead"></param>
        /// <param name="p_data"></param>
        public void H2P_TQ1(byte[] p_byteRead, ref A320_Data_TQ p_data)
        {
            int eng_l = p_byteRead[1];
            int eng_r = p_byteRead[2];

            //符号数判断
            eng_l = Unsigned2Signed(eng_l);     
                
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

            eng_r = Unsigned2Signed(eng_r);

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

            //计算完成后赋值给Prosim变量
            p_data.A_FC_THROTTLE_LEFT_INPUT.value = eng_l;
            p_data.A_FC_THROTTLE_RIGHT_INPUT.value = eng_r;   
        }

        /// <summary>
        /// 接收的无符号数转为有符号数
        /// </summary>
        /// <param name="p_data"></param>
        /// <returns></returns>
        private int Unsigned2Signed(int p_data)
        {
            if(p_data>=128)
            {
                return p_data - 256;
            }
            else
            {
                return p_data;
            }                
        }

        /// <summary>
        /// 将数据按照每两位分割
        /// </summary>
        /// <param name="p_data"></param>
        /// <returns>0-按下   1-抬起</returns>
        private int[] SplitData(int p_data)
        {
            int[] m_data = new int[4];

            m_data[3] = p_data & 0xC0;
            m_data[2] = p_data & 0x30;
            m_data[1] = p_data & 0x0C;
            m_data[0] = p_data & 0x03;

            if (m_data[3] == 64) m_data[3] = 0;
            else m_data[3] = 1;

            if (m_data[2] == 16) m_data[2] = 0;
            else m_data[2] = 1;

            if (m_data[1] == 4) m_data[1] = 0;
            else m_data[1] = 1;

            if (m_data[0] == 1) m_data[0] = 0;
            else m_data[0] = 1;

            return m_data;
        }
    }   
}
