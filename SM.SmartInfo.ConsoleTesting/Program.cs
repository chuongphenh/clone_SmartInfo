using System;
using System.IO;
using System.Collections.Generic;
using SM.SmartInfo.Service.Management;

namespace SM.SmartInfo.ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Start");
                TestService();
                Console.Write("Done");
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            Console.Read();
        }

        static void TestService()
        {
            try
            {
                Console.WriteLine("Service is starting...");

                ServiceManager serviceManager = new ServiceManager();
                serviceManager.StartService();

                Console.WriteLine("Service is started");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Starting service failed. Error: " + ex.Message);
            }
        }

        static void GenPassword()
        {
            string fileInput = @"C:\Users\RohnAnh\Desktop\emp.csv";
            string[] arrEmp = File.ReadAllLines(fileInput);
            List<string> lstScript = new List<string>();
            foreach (string emp in arrEmp)
            {
                string[] arrVal = emp.Split(',');
                if (arrVal.Length != 3)
                    continue;

                int empID = int.Parse(arrVal[0]);
                string userName = arrVal[1];
                string name = arrVal[2];
                string rawPass = string.Format("{0}@{1}", userName, empID);
                string password = Utils.SHA.GenerateSHA512String(rawPass);
                lstScript.Add(string.Format("{0},{1},{2},{3}", name, userName, rawPass, password));
            }
            File.WriteAllLines(@"C:\Users\RohnAnh\Desktop\emp_pass.csv", lstScript);
        }
    }
}