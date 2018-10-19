using System.Runtime.InteropServices;

namespace Lead.Detect.PrimMotionADLink.ADLink
{
    //ADLINK Struct++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [StructLayout(LayoutKind.Sequential)]
    public struct STR_SAMP_DATA_4CH
    {
        public int tick;
        public int data0; //Total channel = 4
        public int data1;
        public int data2;
        public int data3;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOVE_PARA
    {
        public short i16_accType; //Axis parameter
        public short i16_decType; //Axis parameter
        public int i32_acc; //Axis parameter
        public int i32_dec; //Axis parameter
        public int i32_initSpeed; //Axis parameter
        public int i32_maxSpeed; //Axis parameter
        public int i32_endSpeed; //Axis parameter
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT_DATA
    {
        public int i32_pos; // Position data (relative or absolute) (pulse)
        public short i16_accType; // Acceleration pattern 0: T-curve,  1: S-curve
        public short i16_decType; // Deceleration pattern 0: T-curve,  1: S-curve
        public int i32_acc; // Acceleration rate ( pulse / ss )
        public int i32_dec; // Deceleration rate ( pulse / ss )
        public int i32_initSpeed; // Start velocity	( pulse / s )
        public int i32_maxSpeed; // Maximum velocity  ( pulse / s )
        public int i32_endSpeed; // End velocity		( pulse / s )
        public int i32_angle; // Arc move angle    ( degree, -360 ~ 360 )
        public int u32_dwell; // Dwell times       ( unit: ms )
        public int i32_opt; // Option //0xABCD , D:0 absolute, 1:relative
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PNT_DATA
    {
        // Point table structure (One dimension)
        public uint u32_opt; // option, [0x00000000,0xFFFFFFFF]
        public int i32_x; // x-axis component (pulse), [-2147483648,2147484647]
        public int i32_theta; // x-y plane arc move angle (0.001 degree), [-360000,360000]
        public int i32_acc; // acceleration rate (pulse/ss), [0,2147484647]
        public int i32_dec; // deceleration rate (pulse/ss), [0,2147484647]
        public int i32_vi; // initial velocity (pulse/s), [0,2147484647]
        public int i32_vm; // maximum velocity (pulse/s), [0,2147484647]
        public int i32_ve; // ending velocity (pulse/s), [0,2147484647]
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PNT_DATA_2D
    {
        public uint u32_opt; // option, [0x00000000,0xFFFFFFFF]
        public int i32_x; // x-axis component (pulse), [-2147483648,2147484647]
        public int i32_y; // y-axis component (pulse), [-2147483648,2147484647]
        public int i32_theta; // x-y plane arc move angle (0.000001 degree), [-360000,360000]
        public int i32_acc; // acceleration rate (pulse/ss), [0,2147484647]
        public int i32_dec; // deceleration rate (pulse/ss), [0,2147484647]
        public int i32_vi; // initial velocity (pulse/s), [0,2147484647]
        public int i32_vm; // maximum velocity (pulse/s), [0,2147484647]
        public int i32_ve; // ending velocity (pulse/s), [0,2147484647]
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PNT_DATA_2D_F64
    {
        public uint u32_opt; // option, [0x00000000,0xFFFFFFFF]
        public double f64_x; // x-axis component (pulse), [-2147483648,2147484647]
        public double f64_y; // y-axis component (pulse), [-2147483648,2147484647]
        public double f64_theta; // x-y plane arc move angle (0.000001 degree), [-360000,360000]
        public double f64_acc; // acceleration rate (pulse/ss), [0,2147484647]
        public double f64_dec; // deceleration rate (pulse/ss), [0,2147484647]
        public double f64_vi; // initial velocity (pulse/s), [0,2147484647]
        public double f64_vm; // maximum velocity (pulse/s), [0,2147484647]
        public double f64_ve; // ending velocity (pulse/s), [0,2147484647]
        public double f64_sf; // s-factor [0.0 ~ 1.0]
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PNT_DATA_4DL
    {
        public uint u32_opt; // option, [0x00000000,0xFFFFFFFF]
        public int i32_x; // x-axis component (pulse), [-2147483648,2147484647]
        public int i32_y; // y-axis component (pulse), [-2147483648,2147484647]
        public int i32_z; // z-axis component (pulse), [-2147483648,2147484647]
        public int i32_u; // u-axis component (pulse), [-2147483648,2147484647]
        public int i32_acc; // acceleration rate (pulse/ss), [0,2147484647]
        public int i32_dec; // deceleration rate (pulse/ss), [0,2147484647]
        public int i32_vi; // initial velocity (pulse/s), [0,2147484647]
        public int i32_vm; // maximum velocity (pulse/s), [0,2147484647]
        public int i32_ve; // ending velocity (pulse/s), [0,2147484647]
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT_DATA_EX
    {
        public int i32_pos; //(Center)Position data (could be relative or absolute value) 
        public short i16_accType; //Acceleration pattern 0: T curve, 1:S curve   
        public short i16_decType; // Deceleration pattern 0: T curve, 1:S curve 
        public int i32_acc; //Acceleration rate ( pulse / sec 2 ) 
        public int i32_dec; //Deceleration rate ( pulse / sec 2  ) 
        public int i32_initSpeed; //Start velocity ( pulse / s ) 
        public int i32_maxSpeed; //Maximum velocity    ( pulse / s ) 
        public int i32_endSpeed; //End velocity  ( pulse / s )     
        public int i32_angle; //Arc move angle ( degree, -360 ~ 360 ) 
        public uint u32_dwell; //dwell times ( unit: ms ) *Divided by system cycle time. 
        public int i32_opt; //Point move option. (*) 
        public int i32_pitch; // pitch for helical move
        public int i32_totalheight; // total hight
        public short i16_cw; // cw or ccw
        public short i16_opt_ext; // option extend
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct POINT_DATA2
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public int[] i32_pos; // Position data (relative or absolute) (pulse)

        public int i32_initSpeed; // Start velocity	( pulse / s ) 
        public int i32_maxSpeed; // Maximum velocity  ( pulse / s ) 
        public int i32_angle; // Arc move angle    ( degree, -360 ~ 360 ) 
        public uint u32_dwell; // Dwell times       ( unit: ms ) 
        public int i32_opt; // Option //0xABCD , D:0 absolute, 1:relative
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PTSTS
    {
        public ushort BitSts; //b0: Is PTB work? [1:working, 0:Stopped]

        //b1: Is point buffer full? [1:full, 0:not full]
        //b2: Is point buffer empty? [1:empty, 0:not empty]
        //b3, b4, b5: Reserved for future
        //b6~: Be always 0
        public ushort PntBufFreeSpace;
        public ushort PntBufUsageSpace;
        public uint RunningCnt;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct POINT_DATA3
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] i32_pos;

        public int i32_maxSpeed;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] i32_endPos;

        public int i32_dir;
        public int i32_opt;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VAO_DATA
    {
        //Param
        public int outputType; //Output type, [0, 3]
        public int inputType; //Input type, [0, 1]
        public int config; //PWM configuration according to output type
        public int inputSrc; //Input source by axis, [0, 0xf]

        //Mapping table
        public int minVel; //Minimum linear speed, [ positive ]
        public int velInterval; //Speed interval, [ positive ]
        public int totalPoints; //Total points, [1, 32]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public int[] mappingDataArr; //mapping data array
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct LPSTS
    {
        public uint MotionLoopLoading;
        public uint HostLoopLoading;
        public uint MotionLoopLoadingMax;
        public uint HostLoopLoadingMax;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct DEBUG_DATA
    {
        public ushort ServoOffCondition;
        public double DspCmdPos;
        public double DspFeedbackPos;
        public double FpgaCmdPos;
        public double FpgaFeedbackPos;
        public double FpgaOutputVoltage;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DEBUG_STATE
    {
        public ushort AxisState;
        public ushort GroupState;
        public ushort AxisSuperState;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PTDWL
    {
        public double DwTime; //Unit is ms
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PTLINE
    {
        public int Dim;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] Pos;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PTA2CA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Index; //Index X,Y

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public double[] Center; //Center Arr

        public double Angle; //Angle
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PTA2CE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Index; //Index X,Y

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public double[] Center; //

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public double[] End; // 

        public short Dir; //
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PTA3CA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] Index; //Index X,Y

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] Center; //Center Arr

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] Normal; //Normal Arr

        public double Angle; //Angle
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PTA3CE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] Index; //Index X,Y

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] Center; //Center Arr

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] End; //End Arr

        public short Dir; //
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PTHCA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] Index; //Index X,Y

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] Center; //Center Arr

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] Normal; //Normal Arr

        public double Angle; //Angle
        public double DeltaH;
        public double FinalR;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PTHCE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] Index; //Index X,Y

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] Center; //Center Arr

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] Normal; //Normal Arr

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] End; //End Arr

        public short Dir; //
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PTINFO
    {
        public int Dimension;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public int[] AxisArr;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct STR_SAMP_DATA_8CH
    {
        public int tick;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public int[] data; //Total channel = 8
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SAMP_PARAM
    {
        public int rate; //Sampling rate
        public int edge; //Trigger edge
        public int level; //Trigger level
        public int trigCh; //Trigger channel
        private readonly int[,] sourceByCh;

        public SAMP_PARAM(int x, int y)
            : this()
        {
            sourceByCh = new int[x, y];
        }

        //Sampling source by channel, named sourceByCh[a][b], 
        //a: Channel
        //b: 0: Sampling source 1: Sampling axis
        //Sampling source: F64 data occupies two channel, I32 data occupies one channel.
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JOG_DATA
    {
        public short i16_jogMode; // Jog mode. 0:Free running mode, 1:Step mode
        public short i16_dir; // Jog direction. 0:positive, 1:negative direction
        public short i16_accType; // Acceleration pattern 0: T-curve,  1: S-curve
        public int i32_acc; // Acceleration rate ( pulse / ss )
        public int i32_dec; // Deceleration rate ( pulse / ss )
        public int i32_maxSpeed; // Positive value, maximum velocity  ( pulse / s )
        public int i32_offset; // Positive value, a step (pulse)
        public int i32_delayTime; // Delay time, ( range: 0 ~ 65535 millisecond, align by cycle time)
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HOME_PARA
    {
        public ushort u8_homeMode;
        public ushort u8_homeDir;
        public ushort u8_curveType;
        public int i32_orgOffset;
        public int i32_acceleration;
        public int i32_startVelocity;
        public int i32_maxVelocity;
        public int i32_OrgVelocity;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POS_DATA_2D
    {
        public uint u32_opt; // option, [0x00000000,0xFFFFFFFF]
        public int i32_x; // x-axis component (pulse), [-2147483648,2147484647]
        public int i32_y; // y-axis component (pulse), [-2147483648,2147484647]
        public int i32_theta; // x-y plane arc move angle (0.000001 degree), [-360000,360000]
    }


    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ASYNCALL
    {
        public void* h_event;
        public int i32_ret;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TSK_INFO
    {
        public ushort State; // 
        public ushort RunTimeErr; // 
        public ushort IP;
        public ushort SP;
        public ushort BP;
        public ushort MsgQueueSts;
    }

    //New ADCNC structure define
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct POS_DATA_2D_F64
    {
        /* This structure extends original point data contents from "I32" to "F64" 
                                           for internal computation. It's important to prevent data overflow. */
        public uint u32_opt; // option, [0x00000000, 0xFFFFFFFF]
        public double f64_x; // x-axis component (pulse), [-9223372036854775808, 9223372036854775807]
        public double f64_y; // y-axis component (pulse), [-9223372036854775808, 9223372036854775807]
        public double f64_theta; // x-y plane arc move angle (0.000001 degree), [-360000, 360000]
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POS_DATA_2D_RPS
    {
        /* This structure adds another variable to record what point was be saved */
        public uint u32_opt; // option, [0x00000000, 0xFFFFFFFF]
        public int i32_x; // x-axis component (pulse), [-2147483648, 2147483647]
        public int i32_y; // y-axis component (pulse), [-2147483648, 2147483647]
        public int i32_theta; // x-y plane arc move angle (0.000001 degree), [-360000, 360000]
        public uint crpi; // current reading point index
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POS_DATA_2D_F64_RPS
    {
        /* This structure adds another variable to record what point was be saved */
        public uint u32_opt; // option, [0x00000000, 0xFFFFFFFF]
        public double f64_x; // x-axis component (pulse), [-2147483648, 2147483647]
        public double f64_y; // y-axis component (pulse), [-2147483648, 2147483647]
        public double f64_theta; // x-y plane arc move angle (0.000001 degree), [-360000, 360000]
        public uint crpi; // current reading point index
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PNT_DATA_2D_EXT
    {
        public uint u32_opt; // option, [0x00000000,0xFFFFFFFF]
        public double f64_x; // x-axis component (pulse), [-2147483648,2147484647]
        public double f64_y; // y-axis component (pulse), [-2147483648,2147484647]
        public double f64_theta; // x-y plane arc move angle (0.000001 degree), [-360000,360000]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public double[] f64_acc; // acceleration rate (pulse/ss), [0,2147484647]

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public double[] f64_dec; // deceleration rate (pulse/ss), [0,2147484647]		

        public int crossover;
        public int Iboundary; // initial boundary

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public double[] f64_vi; // initial velocity (pulse/s), [0,2147484647]

        public uint vi_cmpr;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public double[] f64_vm; // maximum velocity (pulse/s), [0,2147484647]

        public uint vm_cmpr;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public double[] f64_ve; // ending velocity (pulse/s), [0,2147484647]

        public uint ve_cmpr;
        public int Eboundary; // end boundary		
        public double f64_dist; // point distance
        public double f64_angle; // path angle between previous & current point		
        public double f64_radius; // point radiua (used in arc move)
        public int i32_arcstate;
        public uint spt; // speed profile type

        // unit time measured by DSP sampling period
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public double[] t;

        // Horizontal & Vertical line flag
        public int HorizontalFlag;
        public int VerticalFlag;
    }

    public struct MCMP_POINT
    {
        public double axis_0;
        public double axis_1;
        public double axis_2;
        public double axis_3;

        public MCMP_POINT(double a0, double a1, double a2, double a3)
        {
            axis_0 = a0;
            axis_1 = a1;
            axis_2 = a2;
            axis_3 = a3;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct LATCH_POINT
    {
        public double position; // Latched
        public int ltcSrcInBit; // Latch source: bit0~7; bit 8~11; trigger channel
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++			
    public class APS168
    {
        [DllImport("APS168.dll")]
        public static extern int APS_absolute_arc_move(int Dimension, ref int Axis_ID_Array, ref int Center_Pos_Array, int Max_Arc_Speed, int Angle);

        // Circular interpolation( Support 2D and 3D ) [Only for PCI-8392, PCI-8253/56]
        [DllImport("APS168.dll")]
        public static extern int APS_absolute_arc_move_3pe(int Dimension, ref int Axis_ID_Array, ref int Pass_Pos_Array, ref int End_Pos_Array, int Max_Arc_Speed);

        // Helical interpolation [Only for PCI-8392, PCI-8253/56]
        [DllImport("APS168.dll")]
        public static extern int APS_absolute_helix_move(int Dimension, ref int Axis_ID_Array, ref int Center_Pos_Array, int Max_Arc_Speed, int Pitch, int TotalHeight, int CwOrCcw);

        // Interpolation
        [DllImport("APS168.dll")]
        public static extern int APS_absolute_linear_move(int Dimension, ref int Axis_ID_Array, ref int Position_Array, int Max_Linear_Speed);

        [DllImport("APS168.dll")]
        public static extern int APS_absolute_move(int Axis_ID, int Position, int Max_Speed);

        [DllImport("APS168.dll")]
        public static extern int APS_absolute_move_ovrd(int Axis_ID, int Position, int Max_Speed);

        [DllImport("APS168.dll")]
        public static extern int APS_absolute_move2(int Axis_ID, int Position, int Start_Speed, int Max_Speed, int End_Speed, int Acc_Rate, int Dec_Rate);

        [DllImport("APS168.dll")]
        public static extern int APS_arc2_ca(int[] Axis_ID_Array, int Option, double[] CenterArray, double Angle, ref double TransPara, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_arc2_ca_all(int[] Axis_ID_Array, int Option, double[] CenterArray, double Angle, ref double TransPara, double Vs, double Vm, double Ve, double Acc, double Dec,
            double SFac, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_arc2_ca_v(int[] Axis_ID_Array, int Option, double[] CenterArray, double Angle, ref double TransPara, double Vm, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_arc2_ce(int[] Axis_ID_Array, int Option, double[] CenterArray, double[] EndArray, short Dir, ref double TransPara, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_arc2_ce_all(int[] Axis_ID_Array, int Option, double[] CenterArray, double[] EndArray, short Dir, ref double TransPara, double Vs, double Vm, double Ve, double Acc,
            double Dec, double SFac, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_arc2_ce_v(int[] Axis_ID_Array, int Option, double[] CenterArray, double[] EndArray, short Dir, ref double TransPara, double Vm, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_arc3_ca(int[] Axis_ID_Array, int Option, double[] CenterArray, double[] NormalArray, double Angle, ref double TransPara, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_arc3_ca_all(int[] Axis_ID_Array, int Option, double[] CenterArray, double[] NormalArray, double Angle, ref double TransPara, double Vs, double Vm, double Ve,
            double Acc, double Dec, double SFac, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_arc3_ca_v(int[] Axis_ID_Array, int Option, double[] CenterArray, double[] NormalArray, double Angle, ref double TransPara, double Vm, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_arc3_ce(int[] Axis_ID_Array, int Option, double[] CenterArray, double[] EndArray, short Dir, ref double TransPara, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_arc3_ce_all(int[] Axis_ID_Array, int Option, double[] CenterArray, double[] EndArray, short Dir, ref double TransPara, double Vs, double Vm, double Ve, double Acc,
            double Dec, double SFac, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_arc3_ce_v(int[] Axis_ID_Array, int Option, double[] CenterArray, double[] EndArray, short Dir, ref double TransPara, double Vm, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_auto_sampling(int Board_ID, int StartStop);

        [DllImport("APS168.dll")]
        public static extern int APS_check_vao_param(int Board_ID, int Table_No, ref int Status);

        [DllImport("APS168.dll")]
        public static extern int APS_clear_nv_ram(int Board_ID, int RamNo);

        [DllImport("APS168.dll")]
        public static extern int APS_close();

        [DllImport("APS168.dll")]
        public static extern int APS_device_driver_version(int Board_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_disable_device(int DeviceName);

        [DllImport("APS168.dll")]
        public static extern int APS_emg_stop(int Axis_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_emg_stop_multi(int Dimension, ref int Axis_ID_Array);

        [DllImport("APS168.dll")]
        public static extern int APS_emg_stop_simultaneous_move(int Axis_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_enable_ltc_fifo(int Board_ID, int FLtcCh, int Enable);

        [DllImport("APS168.dll")]
        public static extern int APS_enable_trigger_fifo_cmp(int Board_ID, int FCmpCh, int Enable);

        [DllImport("APS168.dll")]
        public static extern int APS_field_bus_A_get_input(int Board_ID, int BUS_No, int MOD_No, int Ch_No, ref double AI_Value);

        [DllImport("APS168.dll")]
        public static extern int APS_field_bus_A_get_input_plc(int Board_ID, int BUS_No, int MOD_No, int Ch_No, ref double AI_Value, short RunStep);

        [DllImport("APS168.dll")]
        public static extern int APS_field_bus_A_get_output(int Board_ID, int BUS_No, int MOD_No, int Ch_No, ref double AO_Value);

        [DllImport("APS168.dll")]
        public static extern int APS_field_bus_A_set_output(int Board_ID, int BUS_No, int MOD_No, int Ch_No, double AO_Value);

        [DllImport("APS168.dll")]
        public static extern int APS_field_bus_A_set_output_plc(int Board_ID, int BUS_No, int MOD_No, int Ch_No, double AO_Value, short RunStep);

        [DllImport("APS168.dll")]
        public static extern int APS_field_bus_d_get_input(int Board_ID, int BUS_No, int MOD_No, ref int DI_Value);

        [DllImport("APS168.dll")]
        public static extern int APS_field_bus_d_get_output(int Board_ID, int BUS_No, int MOD_No, ref int DO_Value);

        [DllImport("APS168.dll")]
        public static extern int APS_field_bus_d_set_output(int Board_ID, int BUS_No, int MOD_No, int DO_Value);

        [DllImport("APS168.dll")]
        public static extern int APS_field_bus_slave_get_param(int Board_ID, int BUS_No, int MOD_No, int Ch_No, int ParaNum, ref int ParaDat);

        // Reserved functions
        [DllImport("APS168.dll")]
        public static extern int APS_field_bus_slave_set_param(int Board_ID, int BUS_No, int MOD_No, int Ch_No, int ParaNum, int ParaDat);

        [DllImport("APS168.dll")]
        public static extern int APS_free_feeder_group(int GroupId);

        [DllImport("APS168.dll")]
        public static extern int APS_get_axis_info(int Axis_ID, ref int Board_ID, ref int Axis_No, ref int Port_ID, ref int Module_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_get_axis_param(int Axis_ID, int AXS_Param_No, ref int AXS_Param);

        [DllImport("APS168.dll")]
        public static extern int APS_get_axis_param_f(int Axis_ID, int AXS_Param_No, ref double AXS_Param);

        [DllImport("APS168.dll")]
        public static extern int APS_get_backlash_en(int Axis_ID, ref int Enable);

        [DllImport("APS168.dll")]
        public static extern int APS_get_battery_status(int Board_ID, ref int Battery_status);

        [DllImport("APS168.dll")]
        public static extern int APS_get_board_param(int Board_ID, int BOD_Param_No, ref int BOD_Param);

        [DllImport("APS168.dll")]
        public static extern int APS_get_button_status(int Board_ID, ref int buttonstatus);

        [DllImport("APS168.dll")]
        public static extern int APS_get_card_name(int Board_ID, ref int CardName);

        // Motion IO & motion status
        [DllImport("APS168.dll")]
        public static extern int APS_get_command(int Axis_ID, ref int Command);

        //Raw command counter [Only for PCI-8254/8]
        [DllImport("APS168.dll")]
        public static extern int APS_get_command_counter(int Axis_ID, ref int Counter);

        // Monitor functions by float [Only for PCI-8254/8]
        [DllImport("APS168.dll")]
        public static extern int APS_get_command_f(int Axis_ID, ref double Command);

        [DllImport("APS168.dll")]
        public static extern int APS_get_command_velocity(int Axis_ID, ref int Velocity);

        [DllImport("APS168.dll")]
        public static extern int APS_get_command_velocity_f(int Axis_ID, ref double Velocity);

        //Control driver mode [Only for PCI-8254/8]
        [DllImport("APS168.dll")]
        public static extern int APS_get_curr_sys_ctrl_mode(int Axis_ID, ref int Mode);

        [DllImport("APS168.dll")]
        public static extern int APS_get_device_info(int Board_ID, int Info_No, ref int Info);

        //DPAC Display & Display Button
        [DllImport("APS168.dll")]
        public static extern int APS_get_display_data(int Board_ID, int displayDigit, ref int displayIndex);

        //Control driver mode [Only for PCI-8254/8]
        [DllImport("APS168.dll")]
        public static extern int APS_get_eep_curr_drv_ctrl_mode(int Board_ID, ref int ModeInBit);

        //Latch functins
        [DllImport("APS168.dll")]
        public static extern int APS_get_encoder(int Axis_ID, ref int Encoder);

        [DllImport("APS168.dll")]
        public static extern int APS_get_end_point_index(int Axis_ID, ref int Index);

        [DllImport("APS168.dll")]
        public static extern int APS_get_error_position(int Axis_ID, ref int Err_Pos);

        [DllImport("APS168.dll")]
        public static extern int APS_get_error_position_f(int Axis_ID, ref double Err_Pos);

        [DllImport("APS168.dll")]
        public static extern int APS_get_feedback_velocity(int Axis_ID, ref int Velocity);

        [DllImport("APS168.dll")]
        public static extern int APS_get_feedback_velocity_f(int Axis_ID, ref double Velocity);

        [DllImport("APS168.dll")]
        public static extern int APS_get_feeder_feed_index(int GroupId, ref int Index);

        [DllImport("APS168.dll")]
        public static extern int APS_get_feeder_group(int GroupId, ref int Dimension, ref int Axis_ID_Array);

        [DllImport("APS168.dll")]
        public static extern int APS_get_feeder_running_index(int GroupId, ref int Index);

        [DllImport("APS168.dll")]
        public static extern int APS_get_feeder_status(int GroupId, ref int State, ref int ErrCode);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_a_input(int Board_ID, int BUS_No, int MOD_No, int Ch_No, ref double AI_Value);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_a_input_plc(int Board_ID, int BUS_No, int MOD_No, int Ch_No, ref double AI_Value, short RunStep);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_a_output(int Board_ID, int BUS_No, int MOD_No, int Ch_No, ref double AO_Value);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_d_channel_input(int Board_ID, int BUS_No, int MOD_No, int Ch_No, ref int DI_Value);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_d_channel_output(int Board_ID, int BUS_No, int MOD_No, int Ch_No, ref int DO_Value);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_d_input(int Board_ID, int BUS_No, int MOD_No, ref int DI_Value);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_d_output(int Board_ID, int BUS_No, int MOD_No, ref int DO_Value);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_device_info(int Board_ID, int BUS_No, int MOD_No, int Info_No, ref int Info);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_encoder(int Board_ID, int BUS_No, int MOD_No, int EncCh, ref int EncCnt);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_int_factor_di(int Board_ID, int BUS_No, int MOD_No, ref int bitsOfCheck);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_int_factor_error(int Axis_ID, int Factor_No, ref int Enable);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_int_factor_motion(int Axis_ID, int Factor_No, ref int Enable);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_last_scan_info(int Board_ID, int BUS_No, ref int Info_Array, int Array_Size, ref int Info_Count);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_linear_cmp_remain_count(int Board_ID, int BUS_No, int MOD_No, int LCmpCh, ref int Cnt);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_master_type(int Board_ID, int BUS_No, ref int BUS_Type);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_param(int Board_ID, int BUS_No, int BUS_Param_No, ref int BUS_Param);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_slave_first_axisno(int Board_ID, int BUS_No, int MOD_No, ref int AxisNo, ref int TotalAxes);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_slave_mapto_AxisID(int Board_ID, int BUS_No, int MOD_No, ref int AxisID);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_slave_name(int Board_ID, int BUS_No, int MOD_No, ref int MOD_Name);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_slave_param(int Board_ID, int BUS_No, int MOD_No, int Ch_No, int ParaNum, ref int ParaDat);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_slave_type(int Board_ID, int BUS_No, int MOD_No, ref int MOD_Type);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_table_cmp_remain_count(int Board_ID, int BUS_No, int MOD_No, int TCmpCh, ref int Cnt);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_trigger_count(int Board_ID, int BUS_No, int MOD_No, int TrgCh, ref int TrgCnt);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_trigger_linear_cmp(int Board_ID, int BUS_No, int MOD_No, int LCmpCh, ref int CmpVal);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_trigger_param(int Board_ID, int BUS_No, int MOD_No, int Param_No, ref int Param_Val);

        [DllImport("APS168.dll")]
        public static extern int APS_get_field_bus_trigger_table_cmp(int Board_ID, int BUS_No, int MOD_No, int TCmpCh, ref int CmpVal);

        [DllImport("APS168.dll")]
        public static extern int APS_get_filter_param(int Axis_ID, int Filter_paramNo, ref int param_val);

        [DllImport("APS168.dll")]
        public static extern int APS_get_first_axisId(int Board_ID, ref int StartAxisID, ref int TotalAxisNum);

        [DllImport("APS168.dll")]
        public static extern int APS_get_gantry_axis(int Board_ID, int GroupNum, ref int Master_Axis_ID, ref int Slave_Axis_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_get_gantry_error(int Board_ID, int GroupNum, ref int GentryError);

        [DllImport("APS168.dll")]
        public static extern int APS_get_gantry_param(int Board_ID, int GroupNum, int ParaNum, ref int ParaDat);

        [DllImport("APS168.dll")]
        public static extern int APS_get_gear_status(int Axis_ID, ref int Status);

        [DllImport("APS168.dll")]
        public static extern int APS_get_int_factor(int Board_ID, int Item_No, int Factor_No, ref int Enable);

        [DllImport("APS168.dll")]
        public static extern int APS_get_jog_param(int Axis_ID, ref JOG_DATA pStr_Jog);

        [DllImport("APS168.dll")]
        public static extern int APS_get_latch_counter(int Axis_ID, int Src, ref int Counter);

        [DllImport("APS168.dll")]
        public static extern int APS_get_latch_data2(int Axis_ID, int LatchNum, ref int LatchData);

        [DllImport("APS168.dll")]
        public static extern int APS_get_latch_event(int Axis_ID, int Src, ref int Event);

        [DllImport("APS168.dll")]
        public static extern int APS_get_ltc_counter(int Board_ID, int CntNum, ref int CntValue);

        [DllImport("APS168.dll")]
        public static extern int APS_get_ltc_fifo_data(int Board_ID, int FLtcCh, ref int Data);

        [DllImport("APS168.dll")]
        public static extern int APS_get_ltc_fifo_free_space(int Board_ID, int FLtcCh, ref int FreeSpace);

        [DllImport("APS168.dll")]
        public static extern int APS_get_ltc_fifo_param(int Board_ID, int FLtcCh, int Param_No, ref int Param_Val);

        [DllImport("APS168.dll")]
        public static extern int APS_get_ltc_fifo_point(int Board_ID, int FLtcCh, ref int ArraySize, ref LATCH_POINT LatchPoint);

        [DllImport("APS168.dll")]
        public static extern int APS_get_ltc_fifo_status(int Board_ID, int FLtcCh, ref int Status);

        [DllImport("APS168.dll")]
        public static extern int APS_get_ltc_fifo_usage(int Board_ID, int FLtcCh, ref int Usage);

        //Motion queue status [Only for PCI-8254/8]
        [DllImport("APS168.dll")]
        public static extern int APS_get_mq_free_space(int Axis_ID, ref int Space);

        [DllImport("APS168.dll")]
        public static extern int APS_get_mq_usage(int Axis_ID, ref int Usage);

        [DllImport("APS168.dll")]
        public static extern int APS_get_multi_trigger_table_cmp(int Board_ID, int Dimension, MCMP_POINT DataArr);

        [DllImport("APS168.dll")]
        public static extern int APS_get_nv_ram(int Board_ID, int RamNo, int DataWidth, int Offset, ref int Data);

        [DllImport("APS168.dll")]
        public static extern int APS_get_point_table(int Axis_ID, int Index, ref POINT_DATA Point);

        [DllImport("APS168.dll")]
        public static extern int APS_get_point_table_ex(int Axis_ID, int Index, ref POINT_DATA_EX Point);

        [DllImport("APS168.dll")]
        public static extern int APS_get_position(int Axis_ID, ref int Position);

        [DllImport("APS168.dll")]
        public static extern int APS_get_position_f(int Axis_ID, ref double Position);

        [DllImport("APS168.dll")]
        public static extern int APS_get_pt_info(int Board_ID, int PtbId, ref PTINFO Info);

        [DllImport("APS168.dll")]
        public static extern int APS_get_pt_status(int Board_ID, int PtbId, ref PTSTS Status);


        // Pulser counter function
        [DllImport("APS168.dll")]
        public static extern int APS_get_pulser_counter(int Board_ID, ref int Counter);

        [DllImport("APS168.dll")]
        public static extern int APS_get_pwm_frequency(int Board_ID, int PWM_Ch, ref int Frequency);

        [DllImport("APS168.dll")]
        public static extern int APS_get_pwm_width(int Board_ID, int PWM_Ch, ref int Width);

        [DllImport("APS168.dll")]
        public static extern int APS_get_running_point_index(int Axis_ID, ref int Index);

        [DllImport("APS168.dll")]
        public static extern int APS_get_running_point_index2(int Axis_ID, ref int Index);

        [DllImport("APS168.dll")]
        public static extern int APS_get_sampling_count(int Board_ID, ref int SampCnt);

        [DllImport("APS168.dll")]
        public static extern int APS_get_sampling_data(int Board_ID, ref int Length, ref STR_SAMP_DATA_4CH DataArr, ref int Status);

        [DllImport("APS168.dll")]
        public static extern int APS_get_sampling_data_ex(int Board_ID, ref int Length, ref STR_SAMP_DATA_8CH DataArr, ref int Status);

        [DllImport("APS168.dll")]
        public static extern int APS_get_sampling_param(int Board_ID, int ParaNum, ref int ParaDat);

        [DllImport("APS168.dll")]
        public static extern int APS_get_sampling_param_ex(int Board_ID, ref SAMP_PARAM Param);

        [DllImport("APS168.dll")]
        public static extern int APS_get_slave_connect_quality(int Board_ID, int BUS_No, int MOD_No, ref int Sts_data);

        [DllImport("APS168.dll")]
        public static extern int APS_get_slave_online_status(int Board_ID, int BUS_No, int MOD_No, ref int Live);

        [DllImport("APS168.dll")]
        public static extern int APS_get_sscnet_link_status(int Board_ID, ref int Link_Status);

        [DllImport("APS168.dll")]
        public static extern int APS_get_sscnet_servo_abs_position(int Axis_ID, ref int Cyc_Cnt, ref int Res_Cnt);

        [DllImport("APS168.dll")]
        public static extern int APS_get_sscnet_servo_alarm(int Axis_ID, ref int Alarm_No, ref int Alarm_Detail);

        [DllImport("APS168.dll")]
        public static extern int APS_get_sscnet_servo_monitor_data(int Axis_ID, int Arr_Size, ref int Data_Arr);

        [DllImport("APS168.dll")]
        public static extern int APS_get_sscnet_servo_monitor_src(int Axis_ID, int Mon_No, ref int Mon_Src);

        [DllImport("APS168.dll")]
        public static extern int APS_get_sscnet_servo_param(int Axis_ID, int Para_No1, ref int Para_Dat1, int Para_No2, ref int Para_Dat2);

        [DllImport("APS168.dll")]
        public static extern int APS_get_start_point_index(int Axis_ID, ref int Index);

        //Motion Stop Code [Only for PCI-8254/8]
        [DllImport("APS168.dll")]
        public static extern int APS_get_stop_code(int Axis_ID, ref int Code);

        [DllImport("APS168.dll")]
        public static extern int APS_get_system_timer(int Board_ID, ref int Timer);

        [DllImport("APS168.dll")]
        public static extern int APS_get_target_position(int Axis_ID, ref int Targ_Pos);

        [DllImport("APS168.dll")]
        public static extern int APS_get_target_position_f(int Axis_ID, ref double Targ_Pos);

        [DllImport("APS168.dll")]
        public static extern int APS_get_task_info(int Board_ID, int TaskNum, ref TSK_INFO Info);

        [DllImport("APS168.dll")]
        public static extern int APS_get_task_mode(int Board_ID, int TaskNum, ref byte Mode, ref ushort LastIP);

        [DllImport("APS168.dll")]
        public static extern int APS_get_task_msg(int Board_ID, ushort QueueSts, ref ushort ActualSize, ref byte CharArr);

        [DllImport("APS168.dll")]
        public static extern int APS_get_timer_counter(int Board_ID, int TmrCh, ref int Cnt);

        [DllImport("APS168.dll")]
        public static extern int APS_get_trigger_count(int Board_ID, int TrgCh, ref int TrgCnt);

        [DllImport("APS168.dll")]
        public static extern int APS_get_trigger_encoder_counter(int Board_ID, int TrgCh, ref int TrgCnt);

        [DllImport("APS168.dll")]
        public static extern int APS_get_trigger_fifo_cmp(int Board_ID, int FCmpCh, ref int CmpVal);

        [DllImport("APS168.dll")]
        public static extern int APS_get_trigger_fifo_status(int Board_ID, int FCmpCh, ref int FifoSts);

        [DllImport("APS168.dll")]
        public static extern int APS_get_trigger_linear_cmp(int Board_ID, int LCmpCh, ref int CmpVal);

        [DllImport("APS168.dll")]
        public static extern int APS_get_trigger_param(int Board_ID, int Param_No, ref int Param_Val);

        [DllImport("APS168.dll")]
        public static extern int APS_get_trigger_table_cmp(int Board_ID, int TCmpCh, ref int CmpVal);

        [DllImport("APS168.dll")]
        public static extern int APS_get_vao_param(int Board_ID, int Param_No, ref int Param_Val);

        [DllImport("APS168.dll")]
        public static extern int APS_get_vao_param_ex(int Board_ID, int Table_No, ref VAO_DATA VaoData);

        [DllImport("APS168.dll")]
        public static extern int APS_get_vao_status(int Board_ID, ref int Status);

        [DllImport("APS168.dll")]
        public static extern int APS_get_virtual_board_info(int VirCardIndex, ref int Count);

        [DllImport("APS168.dll")]
        public static extern int APS_get_watch_timer(int Board_ID, ref int Timer);

        [DllImport("APS168.dll")]
        public static extern int APS_home_escape(int Axis_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_home_move(int Axis_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_home_move2(int Axis_ID, int Dir, int Acc, int Start_Speed, int Max_Speed, int ORG_Speed);

        // System & Initialization
        [DllImport("APS168.dll")]
        public static extern int APS_initial(ref int BoardID_InBits, int Mode);

        // Interrupt functions
        [DllImport("APS168.dll")]
        public static extern int APS_int_enable(int Board_ID, int Enable);

        [DllImport("APS168.dll")]
        public static extern int APS_int_no_to_handle(int Int_No);

        [DllImport("APS168.dll")]
        public static extern int APS_jog_mode_switch(int Axis_ID, int Turn_No);

        [DllImport("APS168.dll")]
        public static extern int APS_jog_start(int Axis_ID, int STA_On);

        [DllImport("APS168.dll")]
        public static extern int APS_line(int Dimension, int[] Axis_ID_Array, int Option, double[] PositionArray, ref double TransPara, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_line_all(int Dimension, int[] Axis_ID_Array, int Option, double[] PositionArray, ref double TransPara, double Vs, double Vm, double Ve, double Acc, double Dec,
            double SFac, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_line_v(int Dimension, int[] Axis_ID_Array, int Option, double[] PositionArray, ref double TransPara, double Vm, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_load_amc_program(int Board_ID, int TaskNum, string pFile, int Password);

        [DllImport("APS168.dll")]
        public static extern int APS_load_param_from_file(string pXMLFile);

        [DllImport("APS168.dll")]
        public static extern int APS_load_parameter_from_default(int Board_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_load_parameter_from_flash(int Board_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_load_sscnet_servo_abs_position(int Axis_ID, int Abs_Option, ref int Cyc_Cnt, ref int Res_Cnt);

        //Program download - APS
        [DllImport("APS168.dll")]
        public static extern int APS_load_vmc_program(int Board_ID, int TaskNum, string pFile, int Password);

        [DllImport("APS168.dll")]
        public static extern int APS_manual_latch(int Board_ID, int LatchSignalInBits);


        //For Single latch for PCI8154/58/MNET-4XMO-(C)
        [DllImport("APS168.dll")]
        public static extern int APS_manual_latch2(int Axis_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_motion_io_status(int Axis_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_motion_status(int Axis_ID);

        //Multi-axes simultaneuos move start/stop
        [DllImport("APS168.dll")]
        public static extern int APS_move_trigger(int Dimension, ref int Axis_ID_Array);

        [DllImport("APS168.dll")]
        public static extern int APS_point_table_continuous_move2(int Dimension, ref int Axis_ID_Array);

        [DllImport("APS168.dll")]
        public static extern int APS_point_table_move(int Dimension, ref int Axis_ID_Array, int StartIndex, int EndIndex);

        [DllImport("APS168.dll")]
        public static extern int APS_point_table_move3(int Dimension, ref int Axis_ID_Array, int StartIndex, int EndIndex);

        [DllImport("APS168.dll")]
        public static extern int APS_point_table_single_move2(int Axis_ID, int Index);

        [DllImport("APS168.dll")]
        public static extern int APS_point_table_status2(int Axis_ID, ref int Status);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_arc2_ca(int Board_ID, int PtbId, ref PTA2CA Prof, ref PTSTS Status);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_arc2_ce(int Board_ID, int PtbId, ref PTA2CE Prof, ref PTSTS Status);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_arc3_ca(int Board_ID, int PtbId, ref PTA3CA Prof, ref PTSTS Status);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_arc3_ce(int Board_ID, int PtbId, ref PTA3CE Prof, ref PTSTS Status);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_disable(int Board_ID, int PtbId);

        //Point table Feeder (Only for PCI-8254/8)
        [DllImport("APS168.dll")]
        public static extern int APS_pt_dwell(int Board_ID, int PtbId, ref PTDWL Prof, ref PTSTS Status);

        //enable & disable
        [DllImport("APS168.dll")]
        public static extern int APS_pt_enable(int Board_ID, int PtbId, int Dimension, int[] AxisArr);

        //Cmd buffer setting
        [DllImport("APS168.dll")]
        public static extern int APS_pt_ext_set_do_ch(int Board_ID, int PtbId, int Channel, int OnOff);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_ext_set_table_no(int Board_ID, int PtbId, int CtrlNo, int TableNo);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_get_error(int Board_ID, int PtbId, ref int ErrCode);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_get_vs(int Board_ID, int PtbId, ref double Vs);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_line(int Board_ID, int PtbId, ref PTLINE Prof, ref PTSTS Status);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_roll_back(int Board_ID, int PtbId, double Max_Speed);

        //Profile buffer setting
        [DllImport("APS168.dll")]
        public static extern int APS_pt_set_absolute(int Board_ID, int PtbId);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_set_acc(int Board_ID, int PtbId, double Acc);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_set_acc_dec(int Board_ID, int PtbId, double AccDec);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_set_dec(int Board_ID, int PtbId, double Dec);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_set_relative(int Board_ID, int PtbId);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_set_s(int Board_ID, int PtbId, double Sf);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_set_trans_blend_dec(int Board_ID, int PtbId, double Bp);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_set_trans_blend_dist(int Board_ID, int PtbId, double Bp);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_set_trans_blend_pcnt(int Board_ID, int PtbId, double Bp);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_set_trans_buffered(int Board_ID, int PtbId);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_set_trans_inp(int Board_ID, int PtbId);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_set_ve(int Board_ID, int PtbId, double Ve);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_set_vm(int Board_ID, int PtbId, double Vm);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_set_vs(int Board_ID, int PtbId, double Vs);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_spiral_ca(int Board_ID, int PtbId, ref PTHCA Prof, ref PTSTS Status);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_spiral_ce(int Board_ID, int PtbId, ref PTHCE Prof, ref PTSTS Status);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_start(int Board_ID, int PtbId);

        [DllImport("APS168.dll")]
        public static extern int APS_pt_stop(int Board_ID, int PtbId);

        //New Interface
        [DllImport("APS168.dll")]
        public static extern int APS_ptp(int Axis_ID, int Option, double Position, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_ptp_all(int Axis_ID, int Option, double Position, double Vs, double Vm, double Ve, double Acc, double Dec, double SFac, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_ptp_v(int Axis_ID, int Option, double Position, double Vm, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_read_a_input_data(int Board_ID, int Channel_No, ref int Raw_Data);

        [DllImport("APS168.dll")]
        public static extern int APS_read_a_input_value(int Board_ID, int Channel_No, ref double Convert_Data);

        //New AIO [Only for PCI-82548]
        [DllImport("APS168.dll")]
        public static extern int APS_read_a_output_value(int Board_ID, int Channel_No, ref double Convert_Data);

        [DllImport("APS168.dll")]
        public static extern int APS_read_d_channel_input(int Board_ID, int DI_Group, int Ch_No, ref int DI_Data);

        [DllImport("APS168.dll")]
        public static extern int APS_read_d_channel_output(int Board_ID, int DO_Group, int Ch_No, ref int DO_Data);

        [DllImport("APS168.dll")]
        public static extern int APS_read_d_input(int Board_ID, int DI_Group, ref int DI_Data);

        [DllImport("APS168.dll")]
        public static extern int APS_read_d_output(int Board_ID, int DO_Group, ref int DO_Data);

        //Virtual board settings  [Only for PCI-8254/8]
        [DllImport("APS168.dll")]
        public static extern int APS_register_virtual_board(int VirCardIndex, int Count);

        [DllImport("APS168.dll")]
        public static extern int APS_relative_arc_move(int Dimension, ref int Axis_ID_Array, ref int Center_Offset_Array, int Max_Arc_Speed, int Angle);

        [DllImport("APS168.dll")]
        public static extern int APS_relative_arc_move_3pe(int Dimension, ref int Axis_ID_Array, ref int Pass_PosOffset_Array, ref int End_PosOffset_Array, int Max_Arc_Speed);

        [DllImport("APS168.dll")]
        public static extern int APS_relative_helix_move(int Dimension, ref int Axis_ID_Array, ref int Center_PosOffset_Array, int Max_Arc_Speed, int Pitch, int TotalHeight, int CwOrCcw);

        [DllImport("APS168.dll")]
        public static extern int APS_relative_linear_move(int Dimension, ref int Axis_ID_Array, ref int Distance_Array, int Max_Linear_Speed);

        // Single axis motion
        [DllImport("APS168.dll")]
        public static extern int APS_relative_move(int Axis_ID, int Distance, int Max_Speed);

        //Only for MNET-1XMO/MNET-4XMO
        [DllImport("APS168.dll")]
        public static extern int APS_relative_move_ovrd(int Axis_ID, int Distance, int Max_Speed);

        [DllImport("APS168.dll")]
        public static extern int APS_relative_move2(int Axis_ID, int Distance, int Start_Speed, int Max_Speed, int End_Speed, int Acc_Rate, int Dec_Rate);

        [DllImport("APS168.dll")]
        public static extern int APS_release_simultaneous_move(int Axis_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_Release_simultaneous_move(int Axis_ID);

        //Dpac Function
        [DllImport("APS168.dll")]
        public static extern int APS_rescan_CF(int Board_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_reset_feeder_buffer(int GroupId);

        [DllImport("APS168.dll")]
        public static extern int APS_reset_field_bus_int_motion(int Axis_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_reset_field_bus_trigger_count(int Board_ID, int BUS_No, int MOD_No, int TrgCh);

        [DllImport("APS168.dll")]
        public static extern int APS_reset_int(int Int_No);

        [DllImport("APS168.dll")]
        public static extern int APS_reset_ltc_fifo(int Board_ID, int FLtcCh);

        [DllImport("APS168.dll")]
        public static extern int APS_reset_pt_buffer(int Board_ID, int PtbId);

        [DllImport("APS168.dll")]
        public static extern int APS_reset_sscnet_servo_alarm(int Axis_ID);

        // Comparing trigger functions
        [DllImport("APS168.dll")]
        public static extern int APS_reset_trigger_count(int Board_ID, int TrgCh);

        [DllImport("APS168.dll")]
        public static extern int APS_reset_wdt(int Board_ID, int WDT_No);

        [DllImport("APS168.dll")]
        public static extern int APS_save_amc_program(int Board_ID, int TaskNum, string pFile, int Password);

        //Only support PCI-7856/MoionNet series
        [DllImport("APS168.dll")]
        public static extern int APS_save_param_to_file(int Board_ID, string pXMLFile);

        // Flash function [Only for PCI-8253/56, PCI-8392(H)]
        [DllImport("APS168.dll")]
        public static extern int APS_save_parameter_to_flash(int Board_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_save_sscnet_servo_abs_position(int Board_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_save_sscnet_servo_param(int Board_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_save_vmc_program(int Board_ID, int TaskNum, string pFile, int Password);

        [DllImport("APS168.dll")]
        public static extern int APS_set_absolute_simultaneous_move(int Dimension, ref int Axis_ID_Array, ref int Position_Array, ref int Max_Speed_Array);

        [DllImport("APS168.dll")]
        public static extern int APS_set_axis_param(int Axis_ID, int AXS_Param_No, int AXS_Param);

        //Parameters setting by float [Only for PCI-8254/8]
        [DllImport("APS168.dll")]
        public static extern int APS_set_axis_param_f(int Axis_ID, int AXS_Param_No, double AXS_Param);

        [DllImport("APS168.dll")]
        public static extern int APS_set_backlash_en(int Axis_ID, int Enable);

        [DllImport("APS168.dll")]
        public static extern int APS_set_board_param(int Board_ID, int BOD_Param_No, int BOD_Param);

        [DllImport("APS168.dll")]
        public static extern int APS_set_command(int Axis_ID, int Command);

        [DllImport("APS168.dll")]
        public static extern int APS_set_command_f(int Axis_ID, double Command);

        [DllImport("APS168.dll")]
        public static extern int APS_set_display_data(int Board_ID, int displayDigit, int displayIndex);

        [DllImport("APS168.dll")]
        public static extern int APS_set_feeder_cfg_acc_type(int GroupId, int Type);

        [DllImport("APS168.dll")]
        public static extern int APS_set_feeder_ex_pause(int GroupId);

        [DllImport("APS168.dll")]
        public static extern int APS_set_feeder_ex_resume(int GroupId);

        [DllImport("APS168.dll")]
        public static extern int APS_set_feeder_ex_rollback(int GroupId, int Max_Speed);

        //Point table Feeder (Only for PCI-825x)
        [DllImport("APS168.dll")]
        public static extern int APS_set_feeder_group(int GroupId, int Dimension, ref int Axis_ID_Array);

        [DllImport("APS168.dll")]
        public static extern int APS_set_feeder_point_2D(int GroupId, ref PNT_DATA_2D PtArray, int Size, int LastFlag);

        //Field bus AIO slave function
        [DllImport("APS168.dll")]
        public static extern int APS_set_field_bus_a_output(int Board_ID, int BUS_No, int MOD_No, int Ch_No, double AO_Value);

        //Field bus AIO slave function
        [DllImport("APS168.dll")]
        public static extern int APS_set_field_bus_a_output_plc(int Board_ID, int BUS_No, int MOD_No, int Ch_No, double AO_Value, short RunStep);

        //Field bus DIO slave fucntions For PCI-8392(H)
        [DllImport("APS168.dll")]
        public static extern int APS_set_field_bus_d_channel_output(int Board_ID, int BUS_No, int MOD_No, int Ch_No, int DO_Value);

        //Field bus DIO slave fucntions For PCI-8392(H)
        [DllImport("APS168.dll")]
        public static extern int APS_set_field_bus_d_output(int Board_ID, int BUS_No, int MOD_No, int DO_Value);

        [DllImport("APS168.dll")]
        public static extern int APS_set_field_bus_encoder(int Board_ID, int BUS_No, int MOD_No, int EncCh, int EncCnt);

        [DllImport("APS168.dll")]
        public static extern int APS_set_field_bus_int_factor_di(int Board_ID, int BUS_No, int MOD_No, int bitsOfCheck);

        [DllImport("APS168.dll")]
        public static extern int APS_set_field_bus_int_factor_error(int Axis_ID, int Factor_No, int Enable);

        //[Only for PCI-7856 motion interrupt]
        [DllImport("APS168.dll")]
        public static extern int APS_set_field_bus_int_factor_motion(int Axis_ID, int Factor_No, int Enable);

        //Field bus master fucntions For PCI-8392(H)
        [DllImport("APS168.dll")]
        public static extern int APS_set_field_bus_param(int Board_ID, int BUS_No, int BUS_Param_No, int BUS_Param);

        //Field bus slave general functions
        [DllImport("APS168.dll")]
        public static extern int APS_set_field_bus_slave_param(int Board_ID, int BUS_No, int MOD_No, int Ch_No, int ParaNum, int ParaDat);

        [DllImport("APS168.dll")]
        public static extern int APS_set_field_bus_trigger_linear(int Board_ID, int BUS_No, int MOD_No, int LCmpCh, int StartPoint, int RepeatTimes, int Interval);

        [DllImport("APS168.dll")]
        public static extern int APS_set_field_bus_trigger_manual(int Board_ID, int BUS_No, int MOD_No, int TrgCh);

        [DllImport("APS168.dll")]
        public static extern int APS_set_field_bus_trigger_manual_s(int Board_ID, int BUS_No, int MOD_No, int TrgChInBit);

        //Field bus Comparing trigger functions
        [DllImport("APS168.dll")]
        public static extern int APS_set_field_bus_trigger_param(int Board_ID, int BUS_No, int MOD_No, int Param_No, int Param_Val);

        [DllImport("APS168.dll")]
        public static extern int APS_set_field_bus_trigger_table(int Board_ID, int BUS_No, int MOD_No, int TCmpCh, ref int DataArr, int ArraySize);

        // Digital filter functions. [Only for PCI-8253/56]
        [DllImport("APS168.dll")]
        public static extern int APS_set_filter_param(int Axis_ID, int Filter_paramNo, int param_val);

        [DllImport("APS168.dll")]
        public static extern int APS_set_gantry_axis(int Board_ID, int GroupNum, int Master_Axis_ID, int Slave_Axis_ID);

        // Gantry functions. [Only for PCI-8392, PCI-8253/56]
        [DllImport("APS168.dll")]
        public static extern int APS_set_gantry_param(int Board_ID, int GroupNum, int ParaNum, int ParaDat);

        [DllImport("APS168.dll")]
        public static extern int APS_set_int(int Int_No);

        [DllImport("APS168.dll")]
        public static extern int APS_set_int_factor(int Board_ID, int Item_No, int Factor_No, int Enable);

        [DllImport("APS168.dll")]
        public static extern int APS_set_int_factorH(int Board_ID, int Item_No, int Factor_No, int Enable);

        //JOG functions [Only for PCI-8392, PCI-8253/56]
        [DllImport("APS168.dll")]
        public static extern int APS_set_jog_param(int Axis_ID, ref JOG_DATA pStr_Jog, int Mask);

        //Latch Function: for latching multi-points
        [DllImport("APS168.dll")]
        public static extern int APS_set_ltc_counter(int Board_ID, int CntNum, int CntValue);

        [DllImport("APS168.dll")]
        public static extern int APS_set_ltc_fifo_param(int Board_ID, int FLtcCh, int Param_No, int Param_Val);


        //Multi-Trigger for PCI-8254/58
        //Declare Function APS_set_multi_trigger_table Lib "APS168.dll" (ByVal Board_ID As Integer, ByVal Dimension As Integer, ByRef DataArr As MCMP_POINT, ByVal ArraySize As Integer, ByVal Window As Integer) As Integer
        //Declare Function APS_get_multi_trigger_table_cmp Lib "APS168.dll" (ByVal Board_ID As Integer, ByVal Dimension As Integer, ByRef DataArr As MCMP_POINT) As Integer
        [DllImport("APS168.dll")]
        public static extern int APS_set_multi_trigger_table(int Board_ID, int Dimension, MCMP_POINT[] DataArr, int arrSize, int Windows);

        //nv RAM funciton
        [DllImport("APS168.dll")]
        public static extern int APS_set_nv_ram(int Board_ID, int RamNo, int DataWidth, int Offset, int Data);

        //Point table move
        [DllImport("APS168.dll")]
        public static extern int APS_set_point_table(int Axis_ID, int Index, ref POINT_DATA Point);

        [DllImport("APS168.dll")]
        public static extern int APS_set_point_table_4DL(ref int Axis_ID_Array, int Index, ref PNT_DATA_4DL Point);

        //Only for PCI-8392 to replace APS_set_point_table & APS_get_point_table
        [DllImport("APS168.dll")]
        public static extern int APS_set_point_table_ex(int Axis_ID, int Index, ref POINT_DATA_EX Point);

        //Point table move2
        [DllImport("APS168.dll")]
        public static extern int APS_set_point_table_mode2(int Axis_ID, int Mode);

        [DllImport("APS168.dll")]
        public static extern int APS_set_point_table_param3(int FirstAxid, int ParaNum, int ParaDat);

        [DllImport("APS168.dll")]
        public static extern int APS_set_point_table2(int Dimension, ref int Axis_ID_Array, int Index, ref POINT_DATA2 Point);

        //Point table Only for HSL-4XMO
        [DllImport("APS168.dll")]
        public static extern int APS_set_point_table3(int Dimension, ref int Axis_ID_Array, int Index, ref POINT_DATA3 Point);

        [DllImport("APS168.dll")]
        public static extern int APS_set_point_tableEx(int Axis_ID, int Index, ref PNT_DATA Point);

        [DllImport("APS168.dll")]
        public static extern int APS_set_point_tableEx_2D(int Axis_ID, int Axis_ID_2, int Index, ref PNT_DATA_2D Point);

        [DllImport("APS168.dll")]
        public static extern int APS_set_position(int Axis_ID, int Position);

        [DllImport("APS168.dll")]
        public static extern int APS_set_position_f(int Axis_ID, double Position);

        [DllImport("APS168.dll")]
        public static extern int APS_set_pulser_counter(int Board_ID, int Counter);

        [DllImport("APS168.dll")]
        public static extern int APS_set_pwm_frequency(int Board_ID, int PWM_Ch, int Frequency);

        [DllImport("APS168.dll")]
        public static extern int APS_set_pwm_on(int Board_ID, int PWM_Ch, int PWM_On);

        //PWM function
        [DllImport("APS168.dll")]
        public static extern int APS_set_pwm_width(int Board_ID, int PWM_Ch, int Width);

        //Simultaneous move [Only for MNET series and 8392]
        [DllImport("APS168.dll")]
        public static extern int APS_set_relative_simultaneous_move(int Dimension, ref int Axis_ID_Array, ref int Distance_Array, ref int Max_Speed_Array);

        // Sampling functions [Only for PCI-8392, PCI-8253/56]
        [DllImport("APS168.dll")]
        public static extern int APS_set_sampling_param(int Board_ID, int ParaNum, int ParaDat);

        // Sampling functions extension[Only for PCI-82548 for up to 8 channel]
        [DllImport("APS168.dll")]
        public static extern int APS_set_sampling_param_ex(int Board_ID, ref SAMP_PARAM Param);

        [DllImport("APS168.dll")]
        public static extern int APS_set_servo_on(int Axis_ID, int Servo_On);

        [DllImport("APS168.dll")]
        public static extern int APS_set_sscnet_control_mode(int Axis_ID, int Mode);

        [DllImport("APS168.dll")]
        public static extern int APS_set_sscnet_servo_monitor_src(int Axis_ID, int Mon_No, int Mon_Src);

        [DllImport("APS168.dll")]
        public static extern int APS_set_sscnet_servo_param(int Axis_ID, int Para_No1, int Para_Dat1, int Para_No2, int Para_Dat2);

        //Point table + IO - Pause / Resume
        [DllImport("APS168.dll")]
        public static extern int APS_set_table_move_ex_pause(int Axis_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_set_table_move_ex_resume(int Axis_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_set_table_move_ex_rollback(int Axis_ID, int Max_Speed);

        [DllImport("APS168.dll")]
        public static extern int APS_set_table_move_pause(int Axis_ID, int Pause_en);

        [DllImport("APS168.dll")]
        public static extern int APS_set_table_move_repeat(int Axis_ID, int Repeat_en);

        [DllImport("APS168.dll")]
        public static extern int APS_set_task_mode(int Board_ID, int TaskNum, byte Mode, ushort LastIP);

        [DllImport("APS168.dll")]
        public static extern int APS_set_timer_counter(int Board_ID, int TmrCh, int Cnt);

        [DllImport("APS168.dll")]
        public static extern int APS_set_trigger_encoder_counter(int Board_ID, int TrgCh, int TrgCnt);

        [DllImport("APS168.dll")]
        public static extern int APS_set_trigger_fifo_data(int Board_ID, int FCmpCh, ref int DataArr, int ArraySize, int ShiftFlag);

        [DllImport("APS168.dll")]
        public static extern int APS_set_trigger_linear(int Board_ID, int LCmpCh, int StartPoint, int RepeatTimes, int Interval);

        [DllImport("APS168.dll")]
        public static extern int APS_set_trigger_manual(int Board_ID, int TrgCh);

        [DllImport("APS168.dll")]
        public static extern int APS_set_trigger_manual_s(int Board_ID, int TrgChInBit);

        // Comparing trigger functions
        [DllImport("APS168.dll")]
        public static extern int APS_set_trigger_param(int Board_ID, int Param_No, int Param_Val);

        [DllImport("APS168.dll")]
        public static extern int APS_set_trigger_table(int Board_ID, int TCmpCh, int[] DataArr, int ArraySize);

        //VAO function(Laser function) [Only for PCI-8253/6]
        [DllImport("APS168.dll")]
        public static extern int APS_set_vao_param(int Board_ID, int Param_No, int Param_Val);

        [DllImport("APS168.dll")]
        public static extern int APS_set_vao_param_ex(int Board_ID, int Table_No, ref VAO_DATA VaoData);

        [DllImport("APS168.dll")]
        public static extern int APS_set_vao_table(int Board_ID, int Table_No, int MinVelocity, int VelInterval, int TotalPoints, int[] MappingDataArray);

        [DllImport("APS168.dll")]
        public static extern int APS_set_velocity_simultaneous_move(int Dimension, ref int Axis_ID_Array, ref int Max_Speed_Array);

        //Override functions [Only for MNET series]
        [DllImport("APS168.dll")]
        public static extern int APS_speed_override(int Axis_ID, int MaxSpeed);

        [DllImport("APS168.dll")]
        public static extern int APS_spiral_ca(int[] Axis_ID_Array, int Option, double[] CenterArray, double[] NormalArray, double Angle, double DeltaH, double FinalR, ref double TransPara,
            ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_spiral_ca_all(int[] Axis_ID_Array, int Option, double[] CenterArray, double[] NormalArray, double Angle, double DeltaH, double FinalR, ref double TransPara,
            double Vs, double Vm, double Ve, double Acc, double Dec, double SFac, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_spiral_ca_v(int[] Axis_ID_Array, int Option, double[] CenterArray, double[] NormalArray, double Angle, double DeltaH, double FinalR, ref double TransPara,
            double Vm, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_spiral_ce(int[] Axis_ID_Array, int Option, double[] CenterArray, double[] NormalArray, double[] EndArray, short Dir, ref double TransPara, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_spiral_ce_all(int[] Axis_ID_Array, int Option, double[] CenterArray, double[] NormalArray, double[] EndArray, short Dir, ref double TransPara, double Vs,
            double Vm, double Ve, double Acc, double Dec, double SFac, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_spiral_ce_v(int[] Axis_ID_Array, int Option, double[] CenterArray, double[] NormalArray, double[] EndArray, short Dir, ref double TransPara, double Vm,
            ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_start_feeder_move(int GroupId);

        [DllImport("APS168.dll")]
        public static extern int APS_start_field_bus(int Board_ID, int BUS_No, int Start_Axis_ID);

        //Gear/Gantry function
        [DllImport("APS168.dll")]
        public static extern int APS_start_gear(int Axis_ID, int Mode);

        [DllImport("APS168.dll")]
        public static extern int APS_start_simultaneous_move(int Axis_ID);

        // SSCNET-3 functions [Only for PCI-8392(H)] 
        [DllImport("APS168.dll")]
        public static extern int APS_start_sscnet(int Board_ID, ref int AxisFound_InBits);

        [DllImport("APS168.dll")]
        public static extern int APS_start_task(int Board_ID, int TaskNum, int CtrlCmd);

        [DllImport("APS168.dll")]
        public static extern int APS_start_timer(int Board_ID, int TrgCh, int Start);

        [DllImport("APS168.dll")]
        public static extern int APS_start_vao(int Board_ID, int Output_Ch, int Enable);

        [DllImport("APS168.dll")]
        public static extern int APS_stop_field_bus(int Board_ID, int BUS_No);

        [DllImport("APS168.dll")]
        public static extern int APS_stop_move(int Axis_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_stop_move_multi(int Dimension, ref int Axis_ID_Array);

        [DllImport("APS168.dll")]
        public static extern int APS_stop_simultaneous_move(int Axis_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_stop_sscnet(int Board_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_stop_wait_sampling(int Board_ID);

        [DllImport("APS168.dll")]
        public static extern int APS_switch_vao_table(int Board_ID, int Table_No);

        [DllImport("APS168.dll")]
        public static extern int APS_vel(int Axis_ID, int Option, double Vm, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_vel_all(int Axis_ID, int Option, double Vs, double Vm, double Ve, double Acc, double Dec, double SFac, ref ASYNCALL Wait);

        [DllImport("APS168.dll")]
        public static extern int APS_velocity_move(int Axis_ID, int Max_Speed);

        [DllImport("APS168.dll")]
        public static extern int APS_version();

        //[Only for PCI-8154/58]
        [DllImport("APS168.dll")]
        public static extern int APS_wait_error_int(int Board_ID, int Item_No, int Time_Out);

        [DllImport("APS168.dll")]
        public static extern int APS_wait_field_bus_error_int_motion(int Axis_ID, int Time_Out);

        [DllImport("APS168.dll")]
        public static extern int APS_wait_multiple_int(int Int_Count, ref int Int_No_Array, int Wait_All, int Time_Out);

        [DllImport("APS168.dll")]
        public static extern int APS_wait_single_int(int Int_No, int Time_Out);

        [DllImport("APS168.dll")]
        public static extern int APS_wait_trigger_sampling(int Board_ID, int Length, int PreTrgLen, int TimeOutMs, ref STR_SAMP_DATA_4CH DataArr);

        [DllImport("APS168.dll")]
        public static extern int APS_wait_trigger_sampling_async(int Board_ID, int Length, int PreTrgLen, int TimeOutMs, ref STR_SAMP_DATA_4CH DataArr);

        [DllImport("APS168.dll")]
        public static extern int APS_wait_trigger_sampling_async_ex(int Board_ID, int Length, int PreTrgLen, int TimeOutMs, ref STR_SAMP_DATA_8CH DataArr);

        [DllImport("APS168.dll")]
        public static extern int APS_wait_trigger_sampling_ex(int Board_ID, int Length, int PreTrgLen, int TimeOutMs, ref STR_SAMP_DATA_8CH DataArr);

        [DllImport("APS168.dll")]
        public static extern int APS_wdt_get_action_event(int Board_ID, int TimerNo, ref int EventByBit);

        [DllImport("APS168.dll")]
        public static extern int APS_wdt_get_counter(int Board_ID, int TimerNo, ref int Counter);

        [DllImport("APS168.dll")]
        public static extern int APS_wdt_get_timeout_period(int Board_ID, int TimerNo, ref int TimeOut);

        [DllImport("APS168.dll")]
        public static extern int APS_wdt_reset_counter(int Board_ID, int TimerNo);

        [DllImport("APS168.dll")]
        public static extern int APS_wdt_set_action_event(int Board_ID, int TimerNo, int EventByBit);

        //Watch dog timer
        [DllImport("APS168.dll")]
        public static extern int APS_wdt_start(int Board_ID, int TimerNo, int TimeOut);

        [DllImport("APS168.dll")]
        public static extern int APS_write_a_output_data(int Board_ID, int Channel_No, int Raw_Data);

        [DllImport("APS168.dll")]
        public static extern int APS_write_a_output_value(int Board_ID, int Channel_No, double Convert_Data);

        //PCI-82548 Only for channel I/O
        [DllImport("APS168.dll")]
        public static extern int APS_write_d_channel_output(int Board_ID, int DO_Group, int Ch_No, int DO_Data);

        //DIO & AIO
        [DllImport("APS168.dll")]
        public static extern int APS_write_d_output(int Board_ID, int DO_Group, int DO_Data);
    }
}