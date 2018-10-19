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
    public class DiEx : EleDi, IDiEx, IElement
    {
        public DiEx()
        {
        }

        public DiEx(EleDi eleDi, IMotionWrapper wrapper = null)
        {
            var props = eleDi.GetType().GetProperties();
            foreach (var p in props)
            {
                p.SetValue(this, p.GetValue(eleDi));
            }

            DriverCard = wrapper ?? new MotionCardWrapper((IMotionCard)DevPrimsManager.Instance.GetPrimByName(Driver));
        }

        [XmlIgnore]
        public IMotionWrapper DriverCard { get; set; }

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
            Type = (EleDiType)Enum.Parse(typeof(EleDiType), data[i++]);
            Description = data[i++];
            Driver = data[i++];
            Port = int.Parse(data[i++]);
            Enable = bool.Parse(data[i++]);
            DriverCard = machine.MotionExs.Values.FirstOrDefault(m => m.Name == Driver);
            if (DriverCard == null)
            {
                throw new FormatException($"Parsing Error: Not Found Motion Driver Instance {line} {Driver}");
            }

            if (machine.DiExs.ContainsKey(id))
            {
                return;
            }
            machine.DiExs.Add(id, this);
        }

        public static void Import(string type, string line, StateMachine machine)
        {
            IDiEx di = new DiEx();
            di.Import(line, machine);
            di = machine.Find<IDiEx>(di.Name);

            switch (type)
            {
                case "ESTOP":
                    {
                        machine.DiEstop.Add(machine.DiEstop.Count + 1, di);
                    }
                    break;
                case "START":
                    {
                        machine.DiStart.Add(machine.DiStart.Count + 1, di);
                    }
                    break;
                case "STOP":
                    {
                        machine.DiStop.Add(machine.DiStop.Count + 1, di);
                    }
                    break;
                case "RESET":
                    {
                        machine.DiReset.Add(machine.DiReset.Count + 1, di);
                    }
                    break;
                default:
                    throw new Exception($"DI IMPORT ERROR TYPE {type}");
            }
        }
    }
}