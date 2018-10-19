namespace Lead.Detect.Interfaces.Dev
{
    public class MoveProfile
    {
        private double _conversionFactor = 1.0; //To convert to count;
        private double _speed;
        private double _speedPercent = 1.0;

        public MoveProfile()
        {
        }

        public MoveProfile(string name, string axisName)
        {
            Name = name;
            AxisName = axisName;
        }

        public MoveProfile(MoveProfile copyProfile)
        {
            Name = copyProfile.Name;
            AxisName = copyProfile.AxisName;
            Distance = copyProfile.Distance;
            Acceleration = copyProfile.Acceleration;
            Deceleration = copyProfile.Deceleration;
            Jerk = copyProfile.Jerk;
            Speed = copyProfile.Speed;
        }

        public int AxisId { get; set; }

        public double AxisLead { get; set; }

        public string Name { get; set; }

        public double Distance { get; set; }

        public double Speed
        {
            get { return _speed * _speedPercent; }
            set { _speed = value; }
        }

        public double Acceleration { get; set; }

        public double Deceleration { get; set; }

        public double Jerk { get; set; }

        public string AxisName { get; set; }

        public void SpeedChangeHandler(double percent)
        {
            if (percent >= 0)
                _speedPercent = percent / 100.0;

            _conversionFactor = -_conversionFactor;
        }

        //public static MoveProfile GetMoveProfile(SimpleElement profileNode)
        //{
        //    if (profileNode == null) return null;
        //    string name = profileNode.Attribute("name");
        //    string axisName = profileNode.Attribute("axisname");
        //    MoveProfile profile = new MoveProfile(name, axisName);
        //    double dist = double.Parse(profileNode.Attribute("position"));
        //    double speed = double.Parse(profileNode.Attribute("speed"));
        //    double acc = double.Parse(profileNode.Attribute("acc"));
        //    double dcc = double.Parse(profileNode.Attribute("dcc"));
        //    double jerk = double.Parse(profileNode.Attribute("jerk"));
        //    profile.Distance = dist;
        //    profile.Speed = speed;
        //    profile.Acceleration = acc;
        //    profile.Deceleration = dcc;
        //    profile.Jerk = jerk;
        //    return profile;
        //}
        public override string ToString()
        {
            return Name;
        }
    }
}