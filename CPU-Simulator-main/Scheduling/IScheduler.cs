namespace CPUManager
{
    interface IScheduler
    {
        void ScheduleAssigning(Processor availableProcessor);
        void ScheduleInterruption(List<Processor> processors);
        void AddTaskToQueue(List<TaskInfo> tasks);
    }
}
