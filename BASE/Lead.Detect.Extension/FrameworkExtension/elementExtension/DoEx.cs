using System;
using System.Linq;
using System.Xml.Serialization;
using Lead.Detect.Base;
using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.motionDriver;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.FrameworkExtension.elementExtension
{
    public class DoEx : EleDo, IDoEx, IElement
    {
        public DoEx()
        {
        }

        public DoEx(EleDo eledo, MotionCardWrapper wrapper = null)
        {
            var props = eledo.GetType().GetProperties();
            foreach (var p in props)
            {
                p.SetValue(this, p.GetValue(eledo));
            }

            DriverCard = new MotionCardWrapper((IMotionCard)DevPrimsManager.Instance.GetPrimByName(Driver));
        }

        [XmlIgnore]
        public MotionCardWrapper DriverCard { get; set; }

        public void Build(StateMachine machine)
        {
            DriverCard = machine.MotionExs.Values.First(m => m.Name == Driver);
        }

        public override string ToString()
        {
            return $"{Name} {Type} {Description} {Driver} {Port} {Enable}";
        }

        public string Export()
        {
            return $"{Name} {Type} {Description} {Driver} {Port} {Enable}";
        }

        public void Import(string line, StateMachine machine)
        {
            var data = line.Split(' ');

            int i = 0;
            int id = int.Parse(data[i++]);

            Name = data[i++];
            Type = (EleDoType)Enum.Parse(typeof(EleDoType), data[i++]);
            Description = data[i++];
            Driver = data[i++];
            Port = int.Parse(data[i++]);
            Enable = bool.Parse(data[i++]);
            DriverCard = machine.MotionExs.Values.FirstOrDefault(m => m.Name == Driver);
            if (DriverCard == null)
            {
                throw new FormatException($"Parsing Error: Not Found Motion Driver Instance {line} {Driver}");
            }

            if (machine.DoExs.ContainsKey(id))
            {
                return;
            }
            machine.DoExs.Add(id, this);
        }

        public static void Import(string type, string line, StateMachine machine)
        {
            IDoEx doex = new DoEx();
            doex.Import(line, machine);
            doex = machine.Find<IDoEx>(doex.Name);

            switch (type)
            {
                case "LIGHTGREEN":
                    {
                        machine.DoLightGreen.Add(machine.DoLightGreen.Count + 1, doex);
                    }
                    break;
                case "LIGHTYELLOW":
                    {
                        machine.DoLightYellow.Add(machine.DoLightYellow.Count + 1, doex);
                    }
                    break;
                case "LIGHTRED":
                    {
                        machine.DoLightRed.Add(machine.DoLightRed.Count + 1, doex);
                    }
                    break;
                case "BUZZER":
                    {
                        machine.DoBuzzer.Add(machine.DoBuzzer.Count + 1, doex);
                    }
                    break;
                default:
                    throw new Exception($"DO IMPORT ERROR TYPE {type}");
            }
        }
    }
}