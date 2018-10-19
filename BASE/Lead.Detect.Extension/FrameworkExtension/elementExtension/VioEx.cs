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
    public class VioEx : EleVio, IVioEx, IElement
    {
        public VioEx()
        {
        }

        public VioEx(EleVio vio, IMotionWrapper wrapper = null)
        {
            var props = vio.GetType().GetProperties();
            foreach (var p in props)
            {
                p.SetValue(this, p.GetValue(vio));
            }

            DriverCard = new MotionCardWrapper((IMotionCard) DevPrimsManager.Instance.GetPrimByName(Driver));
        }

        [XmlIgnore]
        public IMotionWrapper DriverCard { get; protected set; }

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
            Type = (EleVioType)Enum.Parse(typeof(EleVioType), data[i++]);
            Description = data[i++];
            Driver = data[i++];
            Port = int.Parse(data[i++]);
            Enable = bool.Parse(data[i++]);


            DriverCard = machine.MotionExs.Values.FirstOrDefault(m => m.Name == Driver);
            if (DriverCard == null)
            {
                throw new FormatException($"Parsing Error: Not Found Motion Driver Instance {line} {Driver}");
            }


            if (machine.VioExs.ContainsKey(id))
            {
                return;
            }
            machine.VioExs.Add(id, this);
        }
    }
}