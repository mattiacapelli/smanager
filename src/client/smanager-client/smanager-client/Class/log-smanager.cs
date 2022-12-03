using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreHardwareMonitor.Hardware;

namespace smanager_client
{
    public class log_smanager
    {
        string authkey;
        string client_ip = "";
        string client_uid = "";

        public log_smanager(string authkey, string client_ip)
        {
            this.authkey = authkey;
            this.client_ip = client_ip;
        }

        public class UpdateVisitor : IVisitor
        {
            public void VisitComputer(IComputer computer)
            {
                computer.Traverse(this);
            }
            public void VisitHardware(IHardware hardware)
            {
                hardware.Update();
                foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
            }
            public void VisitSensor(ISensor sensor) { }
            public void VisitParameter(IParameter parameter) { }
        }

        public string getHardData()
        {
            string cpu_total = "";

            string memory_total = "";
            string memory_used = "";
            string memory_free = "";

            string vmemory_total = "";
            string vmemory_used = "";
            string vmemory_free = "";
            string packet = "";

            Computer computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true, 
                IsMotherboardEnabled = true,
                IsControllerEnabled = true,
                IsNetworkEnabled = true,
                IsStorageEnabled = true
            };
            try
            {
                computer.Open();
                computer.Accept(new UpdateVisitor());

                foreach (IHardware hardware in computer.Hardware)
                {
                    foreach (ISensor sensor in hardware.Sensors)
                    {
                        //Console.WriteLine("\tSensor: {0} | Value: {1}", sensor.Name, sensor.Value);

                        switch (sensor.Name)
                        {
                            case "CPU Total":
                                Console.WriteLine("\tSensor: {0} | Value: {1}", sensor.Name, sensor.Value);
                                cpu_total = sensor.Value.ToString();
                                break;

                            case "Memory Used":
                                Console.WriteLine("\tSensor: {0} | Value: {1}", sensor.Name, sensor.Value);
                                memory_used = sensor.Value.ToString();
                                break;

                            case "Memory Available":
                                Console.WriteLine("\tSensor: {0} | Value: {1}", sensor.Name, sensor.Value);
                                memory_free = sensor.Value.ToString();
                                break;

                            case "Memory":
                                Console.WriteLine("\tSensor: {0} | Value: {1}", sensor.Name, sensor.Value);
                                memory_total = sensor.Value.ToString();
                                break;

                            case "Virtual Memory":
                                Console.WriteLine("\tSensor: {0} | Value: {1}", sensor.Name, sensor.Value);
                                vmemory_total = sensor.Value.ToString();
                                break;

                            case "Virtual Memory Used":
                                Console.WriteLine("\tSensor: {0} | Value: {1}", sensor.Name, sensor.Value);
                                vmemory_used = sensor.Value.ToString();
                                break;

                            case "Virtual Memory Available":
                                Console.WriteLine("\tSensor: {0} | Value: {1}", sensor.Name, sensor.Value);
                                vmemory_free = sensor.Value.ToString();
                                break;


                        }

                    }
                }

                computer.Close();
            }
            catch (Exception ex)
            { Console.WriteLine("Error"); Console.WriteLine(ex.Message); }

            //Get datetime now
            DateTime now = DateTime.Now;
            string date = now.ToString("yyyy-MM-dd | HH:mm:ss");

            packet = "{\"type\": \"" + "data" + "\",\"authkey\": \"" + authkey + "\",\"datetime\": \"" + date + "\", \"cpu\": \"" + cpu_total + "\", \"ram_used\": \"" + memory_used + "\", \"ram_free\":\"" + memory_free + "\", \"ram_total\":\"" + memory_total + "\", \"vram_total\":\"" + vmemory_total + "\", \"vram_used\":\"" + vmemory_used + "\", \"vram_free\":\"" + vmemory_free + "\"}";

            //{\"cpu\": \"" + cpu_total + "\", \"ram_used\": \"" + memory_used + "\", \"ram_free\":\"" + memory_free + "\"}
            return packet;
        }
    }
}
