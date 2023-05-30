namespace CPUManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HandleFilesIO InputOutputHandler = new HandleFilesIO();
            Simulator cpuData = InputOutputHandler.ReadFile<Simulator>("./IOFiles/example2.json");
            // Simulator cpuData = InputOutputHandler.ReadFile<Simulator>("./IOFiles/example.xml");
            Simulator cpuSimulator = new Simulator(cpuData.NumOfProcessors, cpuData.Tasks);

            cpuSimulator.RunSimulation();

            InputOutputHandler.WriteToFile<TaskInfo>(cpuSimulator.Tasks);
            Console.ReadKey();
        }
    }
}
