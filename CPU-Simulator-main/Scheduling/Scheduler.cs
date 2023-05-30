namespace CPUManager
{
    public class Scheduler : IScheduler
    {
        public PriorityQueue<TaskInfo, string> taskQueue;
        public List<Processor> processors;
        public int queueCounter { get; private set; }
        ClockCycle clockCycle = ClockCycle.GetInstance();

        private Scheduler()
        {
            taskQueue = new PriorityQueue<TaskInfo, string>();
            processors = new List<Processor>();
            queueCounter = 0;
        }

        private static Scheduler? instance = null;

        public static Scheduler GetInstance()
        {
            if (instance == null)
            {
                instance = new Scheduler();
            }
            return instance;
        }

        public void ScheduleAssigning(Processor availableProcessor)
        {
            if (taskQueue.Count > 0)
            {
                TaskInfo task = taskQueue.Dequeue();
                availableProcessor.AssignTask(task);
            }
        }

        public void ScheduleInterruption(List<Processor> processors)
        {
            if (taskQueue.Count > 0)
            {
                TaskInfo firstTask = taskQueue.Peek();
                if (firstTask.Priority == "High")
                {
                    foreach (Processor processor in processors)
                    {
                        if (processor?.CurrentTask?.Priority == "Low")
                        {
                            TaskInfo task = taskQueue.Dequeue();
                            TaskInfo interruptedTask = processor.InterruptCurrentTask();
                            if (interruptedTask is not null)
                                taskQueue.Enqueue(interruptedTask, interruptedTask.Priority);
                            processor.AssignTask(task);
                        }
                    }
                }
            }
        }

        public void AddTaskToQueue(List<TaskInfo> tasks)
        {
            foreach (TaskInfo task in tasks)
            {
                if (clockCycle.CycleCounter == task.CreationTime)
                {
                    taskQueue.Enqueue(task, task.Priority);
                    queueCounter++;
                }
            }
        }
    }
}
