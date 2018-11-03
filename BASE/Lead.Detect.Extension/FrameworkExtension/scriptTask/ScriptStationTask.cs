using System;
using System.IO;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace Lead.Detect.FrameworkExtension.scriptTask
{
    public class ScriptStationTask : StationTask
    {
        public static string DefaultScriptFolder = @".\Config\ScriptTasks";
        static ScriptStationTask()
        {
            if (!Directory.Exists(DefaultScriptFolder))
            {
                Directory.CreateDirectory(DefaultScriptFolder);
            }


        }


        public string RunningScript { get; set; }

        public string ResettingScript { get; set; }


        private Func<int> _resettingAction;
        private Func<int> _runningAction;


        public ScriptStationTask(int id, string name, Station station) : base(id, name, station)
        {
            var reset = Path.Combine(DefaultScriptFolder, $"{Name}_Resetting.script");

            var run = Path.Combine(DefaultScriptFolder, $"{Name}_Running.script");

            if (File.Exists(reset))
            {
                ResettingScript = File.ReadAllText(reset);
            }

            if (File.Exists(run))
            {
                ResettingScript = File.ReadAllText(run);
            }

        }

        public void Load()
        {
            var reset = Path.Combine(DefaultScriptFolder, $"{Name}_Resetting.script");
            var run = Path.Combine(DefaultScriptFolder, $"{Name}_Running.script");

            if (File.Exists(reset))
            {
                ResettingScript = File.ReadAllText(reset);
            }

            if (File.Exists(run))
            {
                RunningScript = File.ReadAllText(run);
            }
        }

        public void Save()
        {
            var reset = Path.Combine(DefaultScriptFolder, $"{Name}_Resetting.script");
            var run = Path.Combine(DefaultScriptFolder, $"{Name}_Running.script");

            File.WriteAllText(reset, ResettingScript);
            File.WriteAllText(run, RunningScript);
        }


        protected override int ResetLoop()
        {
            if (_resettingAction == null)
            {
                CompileResettingScript();
            }

            return _resettingAction.Invoke();
        }



        protected override int RunLoop()
        {
            if (_runningAction == null)
            {
                CompileRunnginScript();
            }

            return _runningAction.Invoke();
        }


        public void CompileRunnginScript()
        {
            var asm = ScriptCompileHelper.Compile(RunningScript);
            var method = asm.GetType("ScriptTask").GetMethod("Running");
            _runningAction = (Func<int>)Delegate.CreateDelegate(typeof(Func<int>), method);
        }


        public void CompileResettingScript()
        {
            var asm = ScriptCompileHelper.Compile(ResettingScript);
            var method = asm.GetType("ScriptTask").GetMethod("Resetting");
            _resettingAction = (Func<int>)Delegate.CreateDelegate(typeof(Func<int>), method);
        }


    }
}