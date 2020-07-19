using Microsoft.Win32.TaskScheduler;
using System;

namespace DeleteScheduledTask
{
    class ScheduledTaskUtils
    {
        public ScheduledTaskUtils()
        {
        }

        public void DeleteScheduledTask(String TaskName)
        {
            TaskService TaskServiceObj = new TaskService();
            TaskServiceObj.RootFolder.DeleteTask(TaskName, false);
            TaskServiceObj.Dispose();
        }
    }
}