namespace Lead.Detect.ThermoAOITrajectoryLib
{
    public interface ITsp
    {
        double RunTcp();


         double MinLength { get;  }
         int[] Order { get;  }
    }
}