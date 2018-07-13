using ProSimSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputTest
{
    class Program
    {
        static DataRef Eng_L;
        static DataRef Eng_R;

        static void Main(string[] args)
        {
            Console.WriteLine("程序名称：InputTest");
            Console.WriteLine("程序版本：V1.0");
            Console.WriteLine("发布时间：2018.7.12");
            Console.WriteLine("开发者：柴宇宸");
            Console.WriteLine("");
            //ProSim连接
            ProSimConnect connection = new ProSimConnect();
            try
            {
                connection.Connect("127.0.0.1");
            }
            catch (Exception ex)
            {
                Console.WriteLine("未连接ProSim320程序");
                Console.Read();
                Environment.Exit(0);
            }
            Console.WriteLine("已连接ProSim320程序");

            DataRefDescription[] descriptions = connection.getDataRefDescriptions().ToArray();
            Eng_L = new DataRef("system.encoders.E_FCU_ALTITUDE", 100, connection);
            //Eng_R = new DataRef("system.analog.A_FC_THROTTLE_RIGHT_INPUT", 100, connection);

            Console.Title = "MyConsoleApp";
            while (true)
            {
                Console.Write("请输入信息：");
                string eng = Console.ReadLine();
                string[] engs = eng.Split(new char[] { ' ' });

                int engl = int.Parse(engs[0]);
                //int engr = int.Parse(engs[1]);
                Eng_L.value = engl;
                //Eng_R.value = engr;
            }
        }
    }
}
