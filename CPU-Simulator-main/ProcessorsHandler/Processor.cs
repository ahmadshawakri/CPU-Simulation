namespace CPUManager
{
    public class Processor
    {
        public string? Id { get; set; }
        public ProcessorState State { get; set; }
        public TaskInfo? CurrentTask { get; set; }
        ClockCycle clockCycle = ClockCycle.GetInstance();

        public Processor() { }

        public Processor(string? id)
        {
            Id = id;
            this.State = ProcessorState.Idle;
            CurrentTask = null;
        }

        public void AssignTask(TaskInfo task)
        {
            State = ProcessorState.Busy;
            CurrentTask = task;
            CurrentTask.State = TaskState.Executing;
            CurrentTask.HasAssigned = this.Id;
        }

        public void ProcessTask()
        {
            CurrentTask.RequestedTime--;
            if (CurrentTask.RequestedTime == 0)
                CompleteTask();
        }

        public void CompleteTask()
        {
            State = ProcessorState.Idle;
            CurrentTask.CompletedTime = clockCycle.CycleCounter;
            this.CurrentTask.State = TaskState.Completed;
            CurrentTask = null;
        }

        public TaskInfo? InterruptCurrentTask()
        {
            if (State == ProcessorState.Busy)
            {
                CurrentTask.RequestedTime--;
                if (CurrentTask.RequestedTime == 0)
                {
                    CompleteTask();
                }
                this.State = ProcessorState.Idle;
                return this.CurrentTask;
            }
            return null;
        }
    }
}
