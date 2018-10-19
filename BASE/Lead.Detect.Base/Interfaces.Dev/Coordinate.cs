using System.Collections;

namespace Lead.Detect.Interfaces.Dev
{
    public class Coordinate
    {
        private string _name;
        private string _key;
        private uint _toleranceWindowCount;

        private Hashtable _coordinatesNames;

        public Coordinate()
        {
            _coordinatesNames = new Hashtable();
        }

        public void ClearPosition()
        {
            _coordinatesNames.Clear();
        }

        public void addCoordinateName(string axisName)
        {
            if (!_coordinatesNames.Contains(axisName))
                _coordinatesNames.Add(axisName, null);
        }

        public void setCoordinateMoveProfile(string axisName, MoveProfile profile)
        {
            if (_coordinatesNames.Contains(axisName))
                _coordinatesNames[axisName] = profile;
            else
                _coordinatesNames.Add(axisName, profile);
        }

        public MoveProfile getCoordinateMoveProfile(string axisName)
        {
            if (_coordinatesNames.Contains(axisName))
                return (MoveProfile) _coordinatesNames[axisName];
            else
                return null;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public uint toleranceCount
        {
            get { return _toleranceWindowCount; }
            set { _toleranceWindowCount = value; }
        }

        public Hashtable coordinatesNames
        {
            get { return _coordinatesNames; }
        }
    }
}