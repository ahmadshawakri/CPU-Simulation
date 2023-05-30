using System.Xml.Serialization;

namespace CPUManager
{
    [XmlRoot("SimulationConfig")]
    public class Simulator
    {
        [XmlElement("NumOfProcessors")]
        public int NumOfProcessors { get; set; }

        [XmlArray("Tasks")]
        [XmlArrayItem("Task")]
        public List<TaskInfo>? Tasks { get; set; }
        public List<Processor>? Processors { get; set; }
        Scheduler cpuScheduler = Scheduler.GetInstance();
        ClockCycle clockCycle = ClockCycle.GetInstance();

        public Simulator() { }

        public Simulator(int numOfProcessors, List<TaskInfo>? tasks)
        {
            this.NumOfProcessors = numOfProcessors;
            this.Tasks = tasks;
            this.Processors = new List<Processor>();
        }

        public void RunSimulation()
        {
            CreateProcessors();
            while (
                cpuScheduler.queueCounter < this.Tasks?.Count
                || this.Tasks.Any(task => task.State != TaskState.Completed)
            )
            {
                clockCycle.ClockCycleIncrement();
                Processor availableProcessor = this.GetAvailableProcessors();

                if (availableProcessor != null)
                {
                    cpuScheduler.ScheduleAssigning(availableProcessor);
                }
                else
                {
                    cpuScheduler.ScheduleInterruption(Processors);
                }

                Processors
                    ?.Where(processor => processor.State == ProcessorState.Busy)
                    .Select(processor =>
                    {
                        processor.ProcessTask();
                        return processor;
                    })
                    .ToList();

                cpuScheduler.AddTaskToQueue(Tasks);
            }
        }

        private void CreateProcessors()
        {
            for (int i = 0; i < NumOfProcessors; i++)
            {
                Processors?.Add(new Processor($"P{i + 1}"));
            }
        }

        private Processor? GetAvailableProcessors()
        {
            foreach (Processor processor in Processors)
            {
                if (processor.State == ProcessorState.Idle)
                {
                    return processor;
                }
            }
            return null;
        }
    }
}
