namespace Lead.Detect.Base.GlobalTask
{
    public class TasksFactory
    {
        private static readonly TasksFactory _instance = new TasksFactory();

        public static TasksFactory Instance
        {
            get { return TasksFactory._instance; }
        }
    }
}