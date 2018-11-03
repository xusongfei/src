namespace Lead.Detect.ThermoAOITrajectoryLib
{
    public interface ITsp
    {
        double RunTsp();


         double MinLength { get;  }
         int[] Order { get;  }
    }
}