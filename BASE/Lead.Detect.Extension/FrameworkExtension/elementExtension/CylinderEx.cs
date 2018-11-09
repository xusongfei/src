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
    public class CylinderEx : EleCylinder, ICylinderEx, IElement
    {
        public CylinderEx()
        {

        }

        public CylinderEx(EleCylinder cy, MotionCardWrapper wrapper = null)
        {
            var props = cy.GetType().GetProperties();
            foreach (var p in props)
            {
                p.SetValue(this, p.GetValue(cy));
            }

            DriverCard = new MotionCardWrapper((IMotionCard)DevPrimsManager.Instance.GetPrimByName(Driver));
        }

        [XmlIgnore]
        public MotionCardWrapper DriverCard { get; protected set; }
        [XmlIgnore]
        public MotionCardWrapper DriverCard2 { get; protected set; }

        public void Build(StateMachine machine)
        {
            DriverCard = machine.MotionExs.Values.First(m => m.Name == Driver);
        }

        private IDiEx[] _diexs;

        public IDiEx[] GetDiExs()
        {
            if (_diexs == null)
            {
                _diexs = new[]
                {
                    new DiEx()
                    {
                        Name = Name + "0",
                        Description = Description + "0",
                        Driver = Driver,
                        DriverCard = DriverCard,
                        Enable = Enable,
                        Port = DiOrg,
                        Type = EleDiType.Open,
                    },
                    new DiEx()
                    {
                        Name = Name + "1",
                        Description = Description + "1",
                        Driver = Driver,
                        DriverCard = DriverCard,
                        Enable = Enable,
                        Port = DiWork,
                        Type = EleDiType.Open,
                    },
                };
            }

            return _diexs;
        }


        private IDoEx[] _doexs;

        public IDoEx[] GetDoExs()
        {
            if (_doexs == null)
            {
                _doexs = new[]
                {
                    new DoEx()
                    {
                        Name = Name + "0",
                        Description = Description + "0",
                        Driver = Driver,
                        DriverCard = DriverCard2,
                        Enable = Enable,
                        Port = DoOrg,
                        Type = EleDoType.DO,
                    },
                    new DoEx()
                    {
                        Name = Name + "1",
                        Description = Description + "1",
                        Driver = Driver,
                        DriverCard = DriverCard2,
                        Enable = Enable,
                        Port = DoWork,
                        Type = EleDoType.DO,
                    },
                };
            }

            return _doexs;
        }


        public override string ToString()
        {
            return $"{Name} {Type} {Description} {Driver} {DiOrg} {DiWork} {DoOrg} {DoWork} {Enable}";
        }



        public string Export()
        {
            if (Driver == Driver2)
            {
                return $"{Name} {Type} {Description} {Driver} {DiOrg} {DiWork} {DoOrg} {DoWork} {Enable}";
            }
            else
            {
                return $"{Name} {Type} {Description} {Driver} {DiOrg} {DiWork} {Driver2} {DoOrg} {DoWork} {Enable}";
            }
        }

        public void Import(string line, StateMachine machine)
        {
            var data = line.Split(' ');

            int i = 0;

            int id = int.Parse(data[i++]);
            Name = data[i++];
            Type = (EleDoType)Enum.Parse(typeof(EleDoType), data[i++]);
            Description = data[i++];

            if (data.Length == 10)
            {
                Driver = data[i++];
                Driver2 = Driver;
                DiOrg = int.Parse(data[i++]);
                DiWork = int.Parse(data[i++]);
                DoOrg = int.Parse(data[i++]);
                DoWork = int.Parse(data[i++]);
                Enable = bool.Parse(data[i++]);
            }
            else if(data.Length == 11)
            {
                Driver = data[i++];
                DiOrg = int.Parse(data[i++]);
                DiWork = int.Parse(data[i++]);
                Driver2 = data[i++];
                DoOrg = int.Parse(data[i++]);
                DoWork = int.Parse(data[i++]);
                Enable = bool.Parse(data[i++]);
            }
            else
            {
                throw new Exception($"Cylinder {Name} Format Error");
            }



            DriverCard = machine.MotionExs.Values.FirstOrDefault(m => m.Name == Driver);
            if (DriverCard == null)
            {
                throw new FormatException($"Parsing Error: Not Found Motion Driver Instance {line} {Driver}");
            }


            DriverCard2 = machine.MotionExs.Values.FirstOrDefault(m => m.Name == Driver2);
            if (DriverCard2 == null)
            {
                throw new FormatException($"Parsing Error: Not Found Motion Driver Instance {line} {Driver}");
            }


            if (machine.CylinderExs.ContainsKey(id))
            {
                return;
            }
            machine.CylinderExs.Add(id, this);
        }
    }
}