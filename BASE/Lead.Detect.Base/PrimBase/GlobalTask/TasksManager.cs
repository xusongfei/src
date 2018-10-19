using System.Collections.Generic;

namespace Lead.Detect.Base.GlobalTask
{
    public class TasksManager
    {
        private TasksManager()
        {
        }

        public static TasksManager Instance { get; } = new TasksManager();
        public List<IPrimTask> Tasks { get; set; } = new List<IPrimTask>();
    }


    public interface IPrimTask
    {
        int Id { get; }

        string Name { get; }
    }
}