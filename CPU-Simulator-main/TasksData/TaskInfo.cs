namespace CPUManager
{
    public class TaskInfo
    {
        public string? Id { get; set; }
        public int CreationTime { get; set; }
        public int RequestedTime { get; set; }
        public int CompletedTime { get; set; }
        public string? Priority { get; set; }
        public string? HasAssigned { get; set; }
        public TaskState State { get; set; }

        public override string ToString()
        {
            return $"Task Id: {this.Id} With Priority: {this.Priority} Has Been Assigned to Processor: {this.HasAssigned} & Completed it's Execution at Clock Cycle: {this.CompletedTime}";
        }
    }
}
