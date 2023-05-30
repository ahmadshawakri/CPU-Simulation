using System.Collections.Generic;
using Xunit;

namespace CPUManager;

public class AddingTasksToQueueTest
{
    [Fact]
    public void AddTaskToQueue_TaskAddedToQueueAndSortedBasedOnPriority()
    {
        // Arrange
        TaskInfo task1 = new TaskInfo
        {
            Id = "t1",
            CreationTime = 4,
            Priority = "High"
        };
        TaskInfo task2 = new TaskInfo
        {
            Id = "t2",
            CreationTime = 3,
            Priority = "Low"
        };
        TaskInfo task3 = new TaskInfo
        {
            Id = "t3",
            CreationTime = 2,
            Priority = "High"
        };
        List<TaskInfo> tasks = new List<TaskInfo> { task1, task2, task3 };
        Scheduler scheduler = Scheduler.GetInstance();
        ClockCycle clockCycleCounter = ClockCycle.GetInstance();
        int expectedQueueCount = 3;

        // Act
        while (scheduler.taskQueue.Count < tasks.Count)
        {
            clockCycleCounter.ClockCycleIncrement();
            scheduler.AddTaskToQueue(tasks);
        }

        // Assert
        Assert.Equal(expectedQueueCount, scheduler.taskQueue.Count);
        Assert.Equal(task3, scheduler.taskQueue.Dequeue());
        Assert.Equal(task1, scheduler.taskQueue.Dequeue());
        Assert.Equal(task2, scheduler.taskQueue.Dequeue());
    }
}
