using Microsoft.Win32.TaskScheduler;
using System;
using System.Diagnostics;

namespace CreateScheduledTask
{
    class ScheduledTaskUtils
    {
        public ScheduledTaskUtils()
        {

        }

        public void CreateScheduledTask(String TaskName, String FileToRun)
        {
            TaskService TaskServiceObj = new TaskService();
            TaskDefinition TaskDefinitionObj = TaskServiceObj.NewTask();
            TaskDefinitionObj.RegistrationInfo.Description = TaskName;
            TaskDefinitionObj.Triggers.Add(new LogonTrigger { });
            TaskDefinitionObj.Settings.AllowDemandStart = true;
            TaskDefinitionObj.Settings.Compatibility = TaskCompatibility.V2_3;
            TaskDefinitionObj.Settings.DisallowStartIfOnBatteries = false;
            TaskDefinitionObj.Settings.StopIfGoingOnBatteries = false;
            TaskDefinitionObj.Settings.IdleSettings.StopOnIdleEnd = false;
            TaskDefinitionObj.Settings.ExecutionTimeLimit = TimeSpan.Zero;
            TaskDefinitionObj.Settings.AllowHardTerminate = false;
            TaskDefinitionObj.Settings.Enabled = true;
            TaskDefinitionObj.Settings.MultipleInstances = TaskInstancesPolicy.StopExisting;
            TaskDefinitionObj.Settings.Priority = ProcessPriorityClass.High;
            TaskDefinitionObj.Principal.RunLevel = TaskRunLevel.Highest;
            TaskDefinitionObj.Actions.Add(new ExecAction(FileToRun));
            TaskServiceObj.RootFolder.RegisterTaskDefinition(TaskName, TaskDefinitionObj);
            TaskServiceObj.Dispose();
            TaskDefinitionObj.Dispose();
        }
    }
}