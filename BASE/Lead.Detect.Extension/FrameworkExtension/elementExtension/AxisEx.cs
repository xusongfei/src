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
    public class AxisEx : EleAxis, IAxisEx, IElement
    {
        public AxisEx()
        {
        }

        public AxisEx(EleAxis axis, IMotionWrapper wrapper = null)
        {
            var props = axis.GetType().GetProperties();
            foreach (var p in props)
            {
                p.SetValue(this, p.GetValue(axis));
            }

            DriverCard = new MotionCardWrapper((IMotionCard)DevPrimsManager.Instance.GetPrimByName(Driver));
        }

        [XmlIgnore]
        public IMotionWrapper DriverCard { get; protected set; }

        public void Build(StateMachine machine)
        {
            DriverCard = machine.MotionExs.Values.First(m => m.Name == Driver);
        }

        public override string ToString()
        {
            return $"{Name} {Type} {Description} {Driver} {AxisChannel} {Enable} {AxisLead:F2} {AxisPlsPerRoll} {AxisSpeed:F2} {AxisAcc:F2} {HomeOrder} {HomeDir} {HomeMode} {HomeVm:F2}";
        }

        public string Export()
        {
            return $"{Name} {Type} {Description} {Driver} {AxisChannel} {Enable} {AxisLead:F2} {AxisPlsPerRoll} {AxisSpeed:F2} {AxisAcc:F2} {HomeOrder} {HomeDir} {HomeMode} {HomeVm:F2}";
        }

        public void Import(string line, StateMachine machine)
        {
            var data = line.Split(' ');

            int i = 0;
            int id = int.Parse(data[i++]);
            Id = id;
            Name = data[i++];
            Type = (EleAxisType)Enum.Parse(typeof(EleAxisType), data[i++]);
            Description = data[i++];
            Driver = data[i++];
            AxisChannel = int.Parse(data[i++]);
            Enable = bool.Parse(data[i++]);

            AxisLead = double.Parse(data[i++]);
            AxisPlsPerRoll = int.Parse(data[i++]);
            AxisSpeed = double.Parse(data[i++]);
            AxisAcc = double.Parse(data[i++]);

            HomeOrder = int.Parse(data[i++]);
            HomeDir = int.Parse(data[i++]);
            HomeMode = int.Parse(data[i++]);
            HomeVm = double.Parse(data[i++]);

            DriverCard = machine.MotionExs.Values.FirstOrDefault(m => m.Name == Driver);
            if (DriverCard == null)
            {
                throw new FormatException($"Parsing Error: Not Found Motion Driver Instance {line} {Driver}");
            }


            if (machine.AxisExs.ContainsKey(id))
            {
                return;
            }
            machine.AxisExs.Add(id, this);
        }
    }



}