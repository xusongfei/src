namespace Lead.Detect.PrimMotionADLink.ADLink
{
    //[StructLayout(LayoutKind.Sequential)]
    //public struct APS_Define
    //public cla APS_Define
    internal enum APS_Define
    {
        // Initial option
        INIT_AUTO_CARD_ID = 0x00, // (Bit 0) CardId assigned by system, Input parameter of APS_initial( cardId, "MODE" )
        INIT_MANUAL_ID = 0x1, //CardId manual by dip switch, Input parameter of APS_initial=( cardId, "MODE" )
        INIT_PARALLEL_FIXED = 0x02, // (Bit 1) Fixed axis indexing mode in Parallel type
        INIT_SERIES_FIXED = 0x04, // (Bit 2) Fixed axis indexing mode in Series type
        INIT_NOT_RESET_DO = 0x08, // (Bit 3) HSL Digital output not reset, (DO status will follow the slave status.)
        INIT_PARAM_IGNORE = 0x00, // (Bit 4-5) Load parameter method - ignore, keep current value
        INIT_PARAM_LOAD_DEFAULT = 0x10, // (Bit 4-5) Load parameter method - load parameter as default value 
        INIT_PARAM_LOAD_FLASH = 0x20, // (Bit 4-5) Load parameter method - load parameter from flash memory
        INIT_MNET_INTERRUPT = 0x40, // (Bit 6) Enable MNET interrupt mode. (Support motion interrupt for MotionNet series)

        // Board parameter define =(General),
        PRB_EMG_LOGIC = 0x0, // Board EMG logic
        PRB_WDT0_LIMIT = 0x10, // Set / Get watch dog limit.
        PRB_WDT0_COUNTER = 0x11, // Reset Wdt / Get Wdt_Count_Value
        PRB_WDT0_UNIT = 0x12, // wdt_unit
        PRB_WDT0_ACTION = 0x13, // wdt_action

        PRB_DO_LOGIC = 0x14, //DO logic, 0: no invert; 1: invert
        PRB_DI_LOGIC = 0x15, //DI logic, 0: no invert; 1: invert

        MHS_GET_SERVO_OFF_INFO = 0x16, //
        MHS_RESET_SERVO_OFF_INFO = 0x0017,
        MHS_GET_ALL_STATE = 0x0018,

        PRB_TMR0_BASE = 0x20, // Set TMR Value
        PRB_TMR0_VALUE = 0x21, // Get timer System.Int32 count value

        PRB_SYS_TMP_MONITOR = 0x30, // Get system temperature monitor data
        PRB_CPU_TMP_MONITOR = 0x31, // Get CPU temperature monitor data
        PRB_AUX_TMP_MONITOR = 0x32, // Get AUX temperature monitor data

        PRB_UART_MULTIPLIER = 0x40, // Set UART Multiplier

        PRB_PSR_MODE = 0x90, // Config pulser mode
        PRB_PSR_EA_LOGIC = 0x91, // Set EA inverted
        PRB_PSR_EB_LOGIC = 0x92, // Set EB inverted

        // Board parameter define =(For PCI-8253/56),
        PRB_DENOMINATOR = 0x80, // Floating number denominator

        //   PRB_PSR_MODE   =(0x90),  // Config pulser mode
        PRB_PSR_ENABLE = 0x91, // Enable/disable pulser mode
        PRB_BOOT_SETTING = 0x100, // Load motion parameter method when DSP boot

        PRB_PWM0_MAP_DO = 0x110, // Enable & Map PWM0 to Do channels
        PRB_PWM1_MAP_DO = 0x111, // Enable & Map PWM1 to Do channels
        PRB_PWM2_MAP_DO = 0x112, // Enable & Map PWM2 to Do channels
        PRB_PWM3_MAP_DO = 0x113, // Enable & Map PWM3 to Do channels

        // PTP buffer mode define
        PTP_OPT_ABORTING = 0x00000000,
        PTP_OPT_BUFFERED = 0x00001000,
        PTP_OPT_BLEND_LOW = 0x00002000,
        PTP_OPT_BLEND_PREVIOUS = 0x00003000,
        PTP_OPT_BLEND_NEXT = 0x00004000,
        PTP_OPT_BLEND_HIGH = 0x00005000,

        ITP_OPT_ABORT_BLEND = 0x00000000,
        ITP_OPT_ABORT_FORCE = 0x00001000,
        ITP_OPT_ABORT_STOP = 0x00002000,
        ITP_OPT_BUFFERED = 0x00003000,
        ITP_OPT_BLEND_DEC_EVENT = 0x00004000,
        ITP_OPT_BLEND_RES_DIST = 0x00005000,
        ITP_OPT_BLEND_RES_DIST_PERCENT = 0x00006000,

        //Latch parameter number define. [Only for PCI-8158A]
        //////////////////////////////////////
        LTC_ENC_IPT_MODE = 0x00,
        LTC_ENC_EA_INV = 0x01,
        LTC_ENC_EB_INV = 0x02,
        LTC_ENC_EZ_CLR_LOGIC = 0x03,
        LTC_ENC_EZ_CLR_EN = 0x04,
        LTC_ENC_SIGNAL_FILITER_EN = 0x05,
        LTC_FIFO_HIGH_LEVEL = 0x06,
        LTC_SIGNAL_FILITER_EN = 0x07,
        LTC_SIGNAL_TRIG_LOGIC = 0x08,
        //////////////////////////////////////

        // Board parameter define =(For PCI-8392 SSCNET), 
        PRB_SSC_APPLICATION = 0x10000, // Reserved
        PRB_SSC_CYCLE_TIME = 0x10000, // SSCNET cycle time selection=(vaild befor start sscnet),
        PRB_PARA_INIT_OPT = 0x00020, // Initial boot mode.
        PRB_WATCH_DOG_LIMIT = 0x00010, // Set / Get watch dog limit.
        PRB_WATCH_DOG_COUNTER = 0x00011, //Watch dog counter

        // Board parameter define =(For DPAC), 
        PRB_DPAC_DISPLAY_MODE = 0x10001, //DPAC Display mode
        PRB_DPAC_DI_MODE = 0x10002, //Set DI pin modes

        PRB_DPAC_THERMAL_MONITOR_NO = 0x20001, //DPAC TEST
        PRB_DPAC_THERMAL_MONITOR_VALUE = 0x20002, //DPAC TEST

        // move option define
        OPT_ABSOLUTE = 0x00000000,
        OPT_RELATIVE = 0x00000001,
        OPT_WAIT = 0x00000100,

        // Axis parameter define =(General),
        PRA_EL_LOGIC = 0x00, // EL logic
        PRA_ORG_LOGIC = 0x01, // ORG logic
        PRA_EL_MODE = 0x02, // EL stop mode
        PRA_MDM_CONDI = 0x03, // Motion done condition
        PRA_EL_EXCHANGE = 0x04, //PEL, MEL exchange enable

        PRA_ALM_LOGIC = 0x04, // ALM logic [PCI-8253/56 only]
        PRA_ZSP_LOGIC = 0x05, // ZSP logic [PCI-8253/56 only]
        PRA_EZ_LOGIC = 0x06, // EZ logic  [PCI-8253/56 only]
        PRA_STP_DEC = 0x07, // Stop deceleration
        PRA_SD_DEC = 0x07, // Stop deceleration
        PRA_SPEL_EN = 0x08, // SPEL Enable
        PRA_SMEL_EN = 0x09, // SMEL Enable
        PRA_EFB_POS0 = 0x0A, // EFB position 0
        PRA_SPEL_POS = 0x0A, // EFB position 0
        PRA_EFB_POS1 = 0x0B, // EFB position 1
        PRA_SMEL_POS = 0x0B, // EFB position 1
        PRA_EFB_CONDI0 = 0x0C, // EFB position 0 condition 
        PRA_EFB_CONDI1 = 0x0D, // EFB position 1 condition 
        PRA_EFB_SRC0 = 0x0E, // EFB position 0 source
        PRA_EFB_SRC1 = 0x0F, // EFB position 1 source 
        PRA_HOME_MODE = 0x10, // home mode
        PRA_HOME_DIR = 0x11, // homing direction
        PRA_HOME_CURVE = 0x12, // homing curve parten=(T or s curve),
        PRA_HOME_ACC = 0x13, // Acceleration deceleration rate 
        PRA_HOME_VS = 0x14, // homing start velocity
        PRA_HOME_VM = 0x15, // homing max velocity
        PRA_HOME_VA = 0x16, // homing approach velocity [PCI-8253/56 only]
        PRA_HOME_SHIFT = 0x17, // The shift from ORG [PCI-8254/58 only]
        PRA_HOME_EZA = 0x18, // EZ alignment enable
        PRA_HOME_VO = 0x19, // Homing leave ORG velocity
        PRA_HOME_OFFSET = 0x1A, // The escape pulse amounts=(Leaving home by position),
        PRA_HOME_POS = 0x1B, // The position from ORG [PCI-8254/58 only]

        PRA_CURVE = 0x20, // Move curve pattern
        PRA_SF = 0x20, // Move s-factor
        PRA_ACC = 0x21, // Move acceleration
        PRA_DEC = 0x22, // Move deceleration
        PRA_VS = 0x23, // Move start velocity
        PRA_VM = 0x24, // Move max velocity
        PRA_VE = 0x25, // Move end velocity
        PRA_SACC = 0x26, // S curve acceleration
        PRA_SDEC = 0x27, // S curve deceleration
        PRA_ACC_SR = 0x28, // S curve ratio in acceleration( S curve with linear acceleration)
        PRA_DEC_SR = 0x29, // S curve ratio in deceleration( S curve with linear deceleration)
        PRA_PRE_EVENT_DIST = 0x2A, //Pre-event distance
        PRA_POST_EVENT_DIST = 0x2B, //Post-event distance

        //following only for V2...
        PRA_DIST = 0x30, // Move distance
        PRA_MAX_VELOCITY = 0x31, // Maximum velocity
        PRA_SCUR_PERCENTAGE = 0x32, // Scurve percentage
        PRA_BLENDING_MODE = 0x33, // Blending mode
        PRA_STOP_MODE = 0x34, // Stop mode
        PRA_STOP_DELRATE = 0x35, // Stop function deceleration rate

        PRA_PT_STOP_ENDO = 0x32, // Disable do when point table stopping.
        PRA_PT_STP_DO_EN = 0x32, // Disable do when point table stopping.


        PRA_PT_STOP_DO = 0x33, // Set do value when point table stopping.
        PRA_PT_STP_DO = 0x33, // Set do value when point table stopping.


        PRA_PWM_OFF = 0x34, // Disable specified PWM output when ASTP input signal is active.
        PRA_DO_OFF = 0x35, // Set DO value when ASTP input signal is active.

        PRA_MOVE_RATIO = 0x88, //Move ratio

        PRA_JG_MODE = 0x40, // Jog mode
        PRA_JG_DIR = 0x41, // Jog move direction
        PRA_JG_CURVE = 0x42, // Jog curve parten=(T or s curve),
        PRA_JG_SF = 0x42, // Jog curve parten=(T or s curve)
        PRA_JG_ACC = 0x43, // Jog move acceleration
        PRA_JG_DEC = 0x44, // Jog move deceleration
        PRA_JG_VM = 0x45, // Jog move max velocity
        PRA_JG_STEP = 0x46, // Jog offset =(For step mode),
        PRA_JG_OFFSET = 0x46, // Jog offset =(For step mode),
        PRA_JG_DELAY = 0x47, // Jog delay =(For step mode),
        PRA_JG_MAP_DI_EN = 0x48, // (I32) Enable Digital input map to jog command signal
        PRA_JG_P_JOG_DI = 0x49, // (I32) Mapping configuration for positive jog and digital input.
        PRA_JG_N_JOG_DI = 0x4A, // (I32) Mapping configuration for negative jog and digital input.
        PRA_JG_JOG_DI = 0x4B, // (I32) Mapping configuration for jog and digital input.

        PRA_MDN_DELAY = 0x50, // NSTP delay setting
        PRA_SINP_WDW = 0x51, // Soft INP window setting
        PRA_SINP_STBL = 0x52, // Soft INP stable cycle
        PRA_SINP_STBT = 0x52, // Soft INP stable cycle
        PRA_SERVO_LOGIC = 0x53, //  SERVO logic

        PRA_GEAR_MASTER = 0x60, // (I32) Select gearing master
        PRA_GEAR_ENGAGE_RATE = 0x61, // (F64) Gear engage rate
        PRA_GEAR_RATIO = 0x62, // (F64) Gear ratio
        PRA_GANTRY_PROTECT_1 = 0x63, // (F64) E-gear gantry mode protection level 1
        PRA_GANTRY_PROTECT_2 = 0x64, // (F64) E-gear gantry mode protection level 2

        // Axis parameter define =(For PCI-8253/56),
        PRA_PLS_IPT_MODE = 0x80, // Pulse input mode setting
        PRA_PLS_OPT_MODE = 0x81, // Pulse output mode setting
        PRA_MAX_E_LIMIT = 0x82, // Maximum encoder count limit
        PRA_ENC_FILTER = 0x83, // Encoder filter
        PRA_ENCODER_FILTER = 0x83, // Encoder filter


        PRA_EGEAR = 0x84, // E-Gear ratio
        PRA_ENCODER_DIR = 0x85, // Encoder direction
        PRA_POS_UNIT_FACTOR = 0x86, // position unit factor setting

        PRA_KP_SHIFT = 0x9B, //Proportional control result shift
        PRA_KI_SHIFT = 0x9c, //Integral control result shift 

        PRA_KD_SHIFT = 0x9D, // Derivative control result shift

        PRA_KVFF_SHIFT = 0x9E, //Velocity feed-forward control result shift
        PRA_KAFF_SHIFT = 0x9F, //Acceleration feed-forward control result shift

        PRA_PID_SHIFT = 0xA0, //PID control result shift 


        PRA_KP_GAIN = 0x90, // PID controller Kp gain
        PRA_KI_GAIN = 0x91, // PID controller Ki gain
        PRA_KD_GAIN = 0x92, // PID controller Kd gain
        PRA_KFF_GAIN = 0x93, // Feed forward Kff gain
        PRA_KVFF_GAIN = 0x93, // Feed forward Kff gain
        PRA_KVGTY_GAIN = 0x94, // Gantry controller Kvgty gain
        PRA_KPGTY_GAIN = 0x95, // Gantry controller Kpgty gain
        PRA_IKP_GAIN = 0x96, // PID controller Kp gain in torque mode
        PRA_IKI_GAIN = 0x97, // PID controller Ki gain in torque mode
        PRA_IKD_GAIN = 0x98, // PID controller Kd gain in torque mode
        PRA_IKFF_GAIN = 0x99, // Feed forward Kff gain in torque mode
        PRA_KAFF_GAIN = 0x9A, // Acceleration feedforward Kaff gain

        //following only for V2...
        PRA_VOLTAGE_MAX = 0x9B, // Maximum output limit
        PRA_VOLTAGE_MIN = 0x9C, // Minimum output limit

        PRA_M_INTERFACE = 0x100, // Motion System.Int32erface 

        PRA_M_VOL_RANGE = 0x110, // Motor voltage input range
        PRA_M_MAX_SPEED = 0x111, // Motor maximum speed 
        PRA_M_ENC_RES = 0x112, // Motor encoder resolution

        PRA_V_OFFSET = 0x120, // Voltage offset
        PRA_SERVO_V_BIAS = 0x120, // Voltage offset
        PRA_DZ_LOW = 0x121, // Dead zone low side
        PRA_DZ_UP = 0x122, // Dead zone up side
        PRA_SAT_LIMIT = 0x123, // Voltage saturation output limit
        PRA_SERVO_V_LIMIT = 0x123, // Voltage saturation output limit
        PRA_ERR_C_LEVEL = 0x124, // Error counter check level
        PRA_ERR_POS_LEVEL = 0x124, // Error counter check level
        PRA_V_INVERSE = 0x125, // Output voltage inverse
        PRA_SERVO_V_INVERSE = 0x125, // Output voltage inverse
        PRA_DZ_VAL = 0x126, // Dead zone output value
        PRA_IW_MAX = 0x127, // Integral windup maximum value
        PRA_IW_MIN = 0x128, // Integral windup minimum value
        PRA_BKL_DIST = 0x129, // Backlash distance
        PRA_BKL_CNSP = 0x12a, // Backlash consumption
        PRA_INTEGRAL_LIMIT = 0x12B, // (I32) Integral limit
        PRA_D_SAMPLE_TIME = 0x12C, // (I32) Derivative Sample Time

        PRA_PSR_LINK = 0x130, // Connect pulser number
        PRA_PSR_RATIO = 0X131, // Set pulser ratio

        PRA_BIQUAD0_A1 = 0x132, // (F64) Biquad filter0 coefficient A1
        PRA_BIQUAD0_A2 = 0x133, // (F64) Biquad filter0 coefficient A2
        PRA_BIQUAD0_B0 = 0x134, // (F64) Biquad filter0 coefficient B0
        PRA_BIQUAD0_B1 = 0x135, // (F64) Biquad filter0 coefficient B1
        PRA_BIQUAD0_B2 = 0x136, // (F64) Biquad filter0 coefficient B2
        PRA_BIQUAD0_DIV = 0x137, // (F64) Biquad filter0 divider
        PRA_BIQUAD1_A1 = 0x138, // (F64) Biquad filter1 coefficient A1
        PRA_BIQUAD1_A2 = 0x139, // (F64) Biquad filter1 coefficient A2
        PRA_BIQUAD1_B0 = 0x13A, // (F64) Biquad filter1 coefficient B0
        PRA_BIQUAD1_B1 = 0x13B, // (F64) Biquad filter1 coefficient B1
        PRA_BIQUAD1_B2 = 0x13C, // (F64) Biquad filter1 coefficient B2
        PRA_BIQUAD1_DIV = 0x13D, // (F64) Biquad filter1 divider
        PRA_FRIC_GAIN = 0x13E, // (F64) Friction voltage compensation

        PRA_DA_TYPE = 0x140, // DAC output type
        PRA_CONTROL_MODE = 0x141, // Closed loop control mode

        // Axis parameter define =(For PCI-8154/58),
        // Input/Output Mode
        PRA_PLS_IPT_LOGIC = 0x200, //Reverse pulse input counting
        PRA_FEEDBACK_SRC = 0x201, //Select feedback conter

        //IO Config
        PRA_ALM_MODE = 0x210, //ALM Mode
        PRA_INP_LOGIC = 0x211, //INP Logic
        PRA_SD_EN = 0x212, //SD Enable -- Bit 8
        PRA_SD_MODE = 0x213, //SD Mode
        PRA_SD_LOGIC = 0x214, //SD Logic
        PRA_SD_LATCH = 0x215, //SD Latch
        PRA_ERC_MODE = 0x216, //ERC Mode
        PRA_ERC_LOGIC = 0x217, //ERC logic
        PRA_ERC_LEN = 0x218, //ERC pulse width
        PRA_RESET_COUNTER = 0x219, //Reset counter when home move is complete
        PRA_PLS_IPT_FLT = 0x21B, //EA/EB Filter Enable
        PRA_INP_MODE = 0x21C, //INP Mode
        PRA_LTC_LOGIC = 0x21D, //LTC LOGIC
        PRA_IO_FILTER = 0x21E, //+-EZ, SD, ORG, ALM, INP filter
        PRA_COMPENSATION_PULSE = 0x221, //BACKLASH PULSE
        PRA_COMPENSATION_MODE = 0x222, //BACKLASH MODE
        PRA_LTC_SRC = 0x223, //LTC Source
        PRA_LTC_DEST = 0x224, //LTC Destination
        PRA_LTC_DATA = 0x225, //Get LTC DATA
        PRA_GCMP_EN = 0x226, // CMP Enable
        PRA_GCMP_POS = 0x227, // Get CMP position
        PRA_GCMP_SRC = 0x228, // CMP source
        PRA_GCMP_ACTION = 0x229, // CMP Action
        PRA_GCMP_STS = 0x22A, // CMP Status
        PRA_VIBSUP_RT = 0x22B, // Vibration Reverse Time
        PRA_VIBSUP_FT = 0x22C, // Vibration Forward Time
        PRA_LTC_DATA_SPD = 0x22D, // Choose latch data for current speed or error position
        PRA_GPDO_SEL = 0x230, //Select DO/CMP Output mode
        PRA_GPDI_SEL = 0x231, //Select DO/CMP Output mode
        PRA_GPDI_LOGIC = 0x232, //Set gpio input logic
        PRA_RDY_LOGIC = 0x233, //RDY logic

        //Fixed Speed
        PRA_SPD_LIMIT = 0x240, // Set Fixed Speed
        PRA_MAX_ACCDEC = 0x241, // Get max acceleration by fixed speed
        PRA_MIN_ACCDEC = 0x242, // Get max acceleration by fixed speed
        PRA_ENABLE_SPD = 0x243, // Disable/Enable Fixed Speed only for HSL-4XMO.

        //Continuous Move
        PRA_CONTI_MODE = 0x250, // Continuous Mode
        PRA_CONTI_BUFF = 0x251, // Continuous Buffer

        //Simultaneous Move
        PRA_SYNC_STOP_MODE = 0x260, // Sync Mode

        // PCI-8144 axis parameter define
        PRA_CMD_CNT_EN = 0x10000,
        PRA_MIO_SEN = 0x10001,
        PRA_START_STA = 0x10002,
        PRA_SPEED_CHN = 0x10003,
        PRA_ORG_STP = 0x1A,

        // Axis parameter define =(For PCI-8392 SSCNET),
        PRA_SSC_SERVO_PARAM_SRC = 0x10000, //Servo parameter source  
        PRA_SSC_SERVO_ABS_POS_OPT = 0x10001, //Absolute position system option
        PRA_SSC_SERVO_ABS_CYC_CNT = 0x10002, //Absolute cycle counter of servo driver
        PRA_SSC_SERVO_ABS_RES_CNT = 0x10003, //Absolute resolution counter of servo driver
        PRA_SSC_TORQUE_LIMIT_P = 0x10004, //Torque limit positive =(0.1%),
        PRA_SSC_TORQUE_LIMIT_N = 0x10005, //Torque limit negative =(0.1%),
        PRA_SSC_TORQUE_CTRL = 0x10006, //Torque control
        PRA_SSC_RESOLUTION = 0x10007, //resolution =(E-gear),
        PRA_SSC_GMR = 0x10008, //resolution (New E-gear)
        PRA_SSC_GDR = 0x10009, //resolution (New E-gear)

        // Sampling parameter define
        SAMP_PA_RATE = 0x0, //Sampling rate
        SAMP_PA_EDGE = 0x2, //Edge select
        SAMP_PA_LEVEL = 0x3, //Level select
        SAMP_PA_TRIGCH = 0x5, //Select trigger channel
        SAMP_PA_SEL = 0x6,

        SAMP_PA_SRC_CH0 = 0x10, //Sample source of channel 0
        SAMP_PA_SRC_CH1 = 0x11, //Sample source of channel 1
        SAMP_PA_SRC_CH2 = 0x12, //Sample source of channel 2
        SAMP_PA_SRC_CH3 = 0x13, //Sample source of channel 3

        // Sampling source
        SAMP_AXIS_MASK = 0xF00,
        SAMP_PARAM_MASK = 0xFF,
        SAMP_COM_POS = 0x00, //command position
        SAMP_FBK_POS = 0x01, //feedback position
        SAMP_CMD_VEL = 0x02, //command velocity
        SAMP_FBK_VEL = 0x03, //feedback velocity
        SAMP_MIO = 0x04, //motion IO
        SAMP_MSTS = 0x05, //motion status
        SAMP_MSTS_ACC = 0x06, //motion status acc
        SAMP_MSTS_MV = 0x07, //motion status at max velocity
        SAMP_MSTS_DEC = 0x08, //motion status at dec
        SAMP_MSTS_CSTP = 0x09, //motion status CSTP
        SAMP_MSTS_NSTP = 0x0A, //motion status NSTP
        SAMP_MSTS_MDN = 0x0A, //motion status NSTP
        SAMP_MIO_INP = 0x0B, //motion status INP
        SAMP_MIO_ZERO = 0x0C, //motion status ZERO
        SAMP_MIO_ORG = 0x0D, //motion status OGR
        SAMP_CONTROL_VOL = 0x20, // Control command voltage
        SAMP_GTY_DEVIATION = 0x21, // Gantry deviation
        SAMP_ENCODER_RAW = 0x22, // Encoder raw data
        SAMP_ERROR_COUNTER = 0x23, // Error counter data
        SAMP_ERROR_POS = 0x23, //Error position [PCI-8254/58]
        SAMP_PTBUFF_RUN_INDEX = 0x24, //Point table running index

        //Only for PCI-8392
        SAMP_SSC_MON_0 = 0x10, // SSCNET servo monitor ch0
        SAMP_SSC_MON_1 = 0x11, // SSCNET servo monitor ch1
        SAMP_SSC_MON_2 = 0x12, // SSCNET servo monitor ch2
        SAMP_SSC_MON_3 = 0x13, // SSCNET servo monitor ch3

        //Only for PCI-8254/8, AMP-204/8C
        SAMP_COM_POS_F64 = 0x10, // Command position
        SAMP_FBK_POS_F64 = 0x11, // Feedback position
        SAMP_CMD_VEL_F64 = 0x12, // Command velocity
        SAMP_FBK_VEL_F64 = 0x13, // Feedback velocity
        SAMP_CONTROL_VOL_F64 = 0x14, // Control command voltage
        SAMP_ERR_POS_F64 = 0x15, // Error position
        SAMP_PWM_FREQUENCY_F64 = 0x18, // PWM frequency (Hz)
        SAMP_PWM_DUTY_CYCLE_F64 = 0x19, // PWM duty cycle (%)
        SAMP_PWM_WIDTH_F64 = 0x1A, // PWM width (ns)
        SAMP_VAO_COMP_VEL_F64 = 0x1B, // Composed velocity for Laser power control (pps)
        SAMP_PTBUFF_COMP_VEL_F64 = 0x1C, // Composed velocity of point table
        SAMP_PTBUFF_COMP_ACC_F64 = 0x1D, // Composed acceleration of point table

        //FieldBus parameter define
        PRF_COMMUNICATION_TYPE = 0x00, // FiledBus Communication Type=(Full/half duplex),
        PRF_TRANSFER_RATE = 0x01, // FiledBus Transfer Rate
        PRF_HUB_NUMBER = 0x02, // FiledBus Hub Number
        PRF_INITIAL_TYPE = 0x03, // FiledBus Initial Type(Clear/Reserve Do area)
        PRF_CHKERRCNT_LAYER = 0x04, // Set the check error count layer.

        //Gantry parameter number define [Only for PCI-8392, PCI-8253/56]
        GANTRY_MODE = 0x0,
        GENTRY_DEVIATION = 0x1,
        GENTRY_DEVIATION_STP = 0x2,

        // Filter parameter number define [Only for PCI-8253/56]
        FTR_TYPE_ST0 = 0x00, // Station 0 filter type
        FTR_FC_ST0 = 0x01, // Station 0 filter cutoff frequency
        FTR_BW_ST0 = 0x02, // Station 0 filter bandwidth
        FTR_ENABLE_ST0 = 0x03, // Station 0 filter enable/disable
        FTR_TYPE_ST1 = 0x10, // Station 1 filter type
        FTR_FC_ST1 = 0x11, // Station 1 filter cutoff frequency
        FTR_BW_ST1 = 0x12, // Station 1 filter bandwidth
        FTR_ENABLE_ST1 = 0x13, // Station 1 filter enable/disable

        // Device name define
        DEVICE_NAME_NULL = 0xFFFF,
        DEVICE_NAME_PCI_8392 = 0,
        DEVICE_NAME_PCI_825X = 1,
        DEVICE_NAME_PCI_8154 = 2,
        DEVICE_NAME_PCI_785X = 3,
        DEVICE_NAME_PCI_8158 = 4,
        DEVICE_NAME_PCI_7856 = 5,
        DEVICE_NAME_ISA_DPAC1000 = 6,
        DEVICE_NAME_ISA_DPAC3000 = 7,
        DEVICE_NAME_PCI_8144 = 8,
        DEVICE_NAME_PCI_825458 = 9,
        DEVICE_NAME_PCI_8102 = 10,
        DEVICE_NAME_PCI_V8258 = 11,
        DEVICE_NAME_PCI_V8254 = 12,
        DEVICE_NAME_PCI_8158A = 13,
        DEVICE_NAME_AMP_20408C = 14,

///////////////////////////////////////////////
//   HSL Slave module definition
///////////////////////////////////////////////
        SLAVE_NAME_UNKNOWN = 0x000,
        SLAVE_NAME_HSL_DI32 = 0x100,
        SLAVE_NAME_HSL_DO32 = 0x101,
        SLAVE_NAME_HSL_DI16DO16 = 0x102,
        SLAVE_NAME_HSL_AO4 = 0x103,
        SLAVE_NAME_HSL_AI16AO2_VV = 0x104,
        SLAVE_NAME_HSL_AI16AO2_AV = 0x105,
        SLAVE_NAME_HSL_DI16UL = 0x106,
        SLAVE_NAME_HSL_DI16RO8 = 0x107,
        SLAVE_NAME_HSL_4XMO = 0x108,
        SLAVE_NAME_HSL_DI16_UCT = 0x109,
        SLAVE_NAME_HSL_DO16_UCT = 0x10A,
        SLAVE_NAME_HSL_DI8DO8 = 0x10B,

///////////////////////////////////////////////
//   MNET Slave module definition
///////////////////////////////////////////////
        SLAVE_NAME_MNET_1XMO = 0x200,
        SLAVE_NAME_MNET_4XMO = 0x201,
        SLAVE_NAME_MNET_4XMO_C = 0x202,

//Trigger parameter number define. [Only for DB-8150]
        TG_PWM0_PULSE_WIDTH = 0x00,
        TG_PWM1_PULSE_WIDTH = 0x01,
        TG_PWM0_MODE = 0x02,
        TG_PWM1_MODE = 0x03,
        TG_TIMER0_INTERVAL = 0x04,
        TG_TIMER1_INTERVAL = 0x05,
        TG_ENC0_CNT_DIR = 0x06,
        TG_ENC1_CNT_DIR = 0x07,
        TG_IPT0_MODE = 0x08,
        TG_IPT1_MODE = 0x09,
        TG_EZ0_CLEAR_EN = 0x0A,
        TG_EZ1_CLEAR_EN = 0x0B,
        TG_EZ0_CLEAR_LOGIC = 0x0C,
        TG_EZ1_CLEAR_LOGIC = 0x0D,
        TG_CNT0_SOURCE = 0x0E,
        TG_CNT1_SOURCE = 0x0F,
        TG_FTR0_EN = 0x10,
        TG_FTR1_EN = 0x11,
        TG_DI_LATCH0_EN = 0x12,
        TG_DI_LATCH1_EN = 0x13,
        TG_DI_LATCH0_EDGE = 0x14,
        TG_DI_LATCH1_EDGE = 0x15,
        TG_DI_LATCH0_VALUE = 0x16,
        TG_DI_LATCH1_VALUE = 0x17,
        TG_TRGOUT_MAP = 0x18,
        TG_TRGOUT_LOGIC = 0x19,
        TG_FIFO_LEVEL = 0x1A,
        TG_PWM0_SOURCE = 0x1B,
        TG_PWM1_SOURCE = 0x1C,
//////////////////////////////////////

//Trigger parameter number define. [Only for PCI-8253/56]
        TG_LCMP0_SRC = 0x00,
        TG_LCMP1_SRC = 0x01,
        TG_TCMP0_SRC = 0x02,
        TG_TCMP1_SRC = 0x03,

        TG_LCMP0_EN = 0x04,
        TG_LCMP1_EN = 0x05,
        TG_TCMP0_EN = 0x06,
        TG_TCMP1_EN = 0x07,

        TG_TRG0_SRC = 0x10,
        TG_TRG1_SRC = 0x11,
        TG_TRG2_SRC = 0x12,
        TG_TRG3_SRC = 0x13,

        TG_TRG0_PWD = 0x14,
        TG_TRG1_PWD = 0x15,
        TG_TRG2_PWD = 0x16,
        TG_TRG3_PWD = 0x17,

        TG_TRG0_CFG = 0x18,
        TG_TRG1_CFG = 0x19,
        TG_TRG2_CFG = 0x1A,
        TG_TRG3_CFG = 0x1B,
        TMR_ITV = 0x20,
        TMR_EN = 0x21,

        //Trigger parameter number define. [Only for MNET-4XMO-C & HSL-4XMO]
        TG_CMP0_SRC = 0x00,
        TG_CMP1_SRC = 0x01,
        TG_CMP2_SRC = 0x02,
        TG_CMP3_SRC = 0x03,
        TG_CMP0_EN = 0x04,
        TG_CMP1_EN = 0x05,
        TG_CMP2_EN = 0x06,
        TG_CMP3_EN = 0x07,
        TG_CMP0_TYPE = 0x08,
        TG_CMP1_TYPE = 0x09,
        TG_CMP2_TYPE = 0x0A,
        TG_CMP3_TYPE = 0x0B,
        TG_CMPH_EN = 0x0C, //Not for HSL-4XMO
        TG_CMPH_DIR_EN = 0x0D, //Not for HSL-4XMO
        TG_CMPH_DIR = 0x0E, //Not for HSL-4XMO
        TG_ENCH_CFG = 0x20, //Not for HSL-4XMO
        TG_TRG0_CMP_DIR = 0x21, //Only for HSL-4XMO
        TG_TRG1_CMP_DIR = 0x22, //Only for HSL-4XMO
        TG_TRG2_CMP_DIR = 0x23, //Only for HSL-4XMO
        TG_TRG3_CMP_DIR = 0x24, //Only for HSL-4XMO

        //Trigger parameter number define. [Only for PCI-8258]
        TGR_LCMP0_SRC = 0x00,
        TGR_LCMP1_SRC = 0x01,
        TGR_TCMP0_SRC = 0x02,
        TGR_TCMP1_SRC = 0x03,

        TGR_TCMP0_DIR = 0x04,
        TGR_TCMP1_DIR = 0x05,
        TGR_TRG_EN = 0x06,

        TGR_TRG0_SRC = 0x10,
        TGR_TRG1_SRC = 0x11,
        TGR_TRG2_SRC = 0x12,
        TGR_TRG3_SRC = 0x13,

        TGR_TRG0_PWD = 0x14,
        TGR_TRG1_PWD = 0x15,
        TGR_TRG2_PWD = 0x16,
        TGR_TRG3_PWD = 0x17,

        TGR_TRG0_LOGIC = 0x18,
        TGR_TRG1_LOGIC = 0x19,
        TGR_TRG2_LOGIC = 0x1A,
        TGR_TRG3_LOGIC = 0x1B,

        TGR_TRG0_TGL = 0x1C,
        TGR_TRG1_TGL = 0x1D,
        TGR_TRG2_TGL = 0x1E,
        TGR_TRG3_TGL = 0x1F,

        TIMR_ITV = 0x20,
        TIMR_DIR = 0x21,
        TIMR_RING_EN = 0x22,
        TIMR_EN = 0x23,

        //Trigger parameter number define. [Only for PCI-8158A]
        TIG_LCMP0_SRC = 0x00,
        TIG_LCMP1_SRC = 0x01,
        TIG_LCMP2_SRC = 0x02,
        TIG_LCMP3_SRC = 0x03,
        TIG_LCMP4_SRC = 0x04,
        TIG_LCMP5_SRC = 0x05,
        TIG_LCMP6_SRC = 0x06,
        TIG_LCMP7_SRC = 0x07,
        TIG_TCMP0_SRC = 0x08,
        TIG_TCMP1_SRC = 0x09,
        TIG_TCMP2_SRC = 0x0A,
        TIG_TCMP3_SRC = 0x0B,
        TIG_TCMP4_SRC = 0x0C,
        TIG_TCMP5_SRC = 0x0D,
        TIG_TCMP6_SRC = 0x0E,
        TIG_TCMP7_SRC = 0x0F,
        TIG_TRG0_EN = 0x10,
        TIG_TRG1_EN = 0x11,
        TIG_TRG2_EN = 0x12,
        TIG_TRG3_EN = 0x13,
        TIG_TRG4_EN = 0x14,
        TIG_TRG5_EN = 0x15,
        TIG_TRG6_EN = 0x16,
        TIG_TRG7_EN = 0x17,
        TIG_TRG0_SRC = 0x18,
        TIG_TRG1_SRC = 0x19,
        TIG_TRG2_SRC = 0x1A,
        TIG_TRG3_SRC = 0x1B,
        TIG_TRG4_SRC = 0x1C,
        TIG_TRG5_SRC = 0x1D,
        TIG_TRG6_SRC = 0x1E,
        TIG_TRG7_SRC = 0x1F,
        TIG_TRG0_PWD = 0x20,
        TIG_TRG1_PWD = 0x21,
        TIG_TRG2_PWD = 0x20,
        TIG_TRG3_PWD = 0x23,
        TIG_TRG4_PWD = 0x24,
        TIG_TRG5_PWD = 0x25,
        TIG_TRG6_PWD = 0x26,
        TIG_TRG7_PWD = 0x27,
        TIG_TRG0_LOGIC = 0x28,
        TIG_TRG1_LOGIC = 0x29,
        TIG_TRG2_LOGIC = 0x2A,
        TIG_TRG3_LOGIC = 0x2B,
        TIG_TRG4_LOGIC = 0x2C,
        TIG_TRG5_LOGIC = 0x2D,
        TIG_TRG6_LOGIC = 0x2E,
        TIG_TRG7_LOGIC = 0x2F,
        TIG_TRG0_TGL = 0x30,
        TIG_TRG1_TGL = 0x31,
        TIG_TRG2_TGL = 0x32,
        TIG_TRG3_TGL = 0x33,
        TIG_TRG4_TGL = 0x34,
        TIG_TRG5_TGL = 0x35,
        TIG_TRG6_TGL = 0x36,
        TIG_TRG7_TGL = 0x37,
        TIG_PWMTMR0_ITV = 0x40,
        TIG_PWMTMR1_ITV = 0x41,
        TIG_PWMTMR2_ITV = 0x42,
        TIG_PWMTMR3_ITV = 0x43,
        TIG_PWMTMR4_ITV = 0x44,
        TIG_PWMTMR5_ITV = 0x45,
        TIG_PWMTMR6_ITV = 0x46,
        TIG_PWMTMR7_ITV = 0x47,
        TIG_TMR0_ITV = 0x50,
        TIG_TMR0_DIR = 0x51,

        // Motion IO status bit number define.
        MIO_ALM = 0, // Servo alarm.
        MIO_PEL = 1, // Positive end limit.
        MIO_MEL = 2, // Negative end limit.
        MIO_ORG = 3, // ORG (Home)
        MIO_EMG = 4, // Emergency stop
        MIO_EZ = 5, // EZ.
        MIO_INP = 6, // In position.
        MIO_SVON = 7, // Servo on signal.
        MIO_RDY = 8, // Ready.
        MIO_WARN = 9, // Warning.
        MIO_ZSP = 10, // Zero speed.
        MIO_SPEL = 11, // Soft positive end limit.
        MIO_SMEL = 12, // Soft negative end limit.
        MIO_TLC = 13, // Torque is limited by torque limit value.
        MIO_ABSL = 14, // Absolute position lost.
        MIO_STA = 15, // External start signal.
        MIO_PSD = 16, // Positive slow down signal
        MIO_MSD = 17, // Negative slow down signal

        // Motion status bit number define.
        MTS_CSTP = 0, // Command stop signal. 
        MTS_VM = 1, // At maximum velocity.
        MTS_ACC = 2, // In acceleration.
        MTS_DEC = 3, // In deceleration.
        MTS_DIR = 4, // (Last)Moving direction.
        NSTP = 5, // Normal stop(Motion done).
        MTS_HMV = 6, // In home operation.
        MTS_SMV = 7, // Single axis move( relative, absolute, velocity move).
        MTS_LIP = 8, // Linear interpolation.
        MTS_CIP = 9, // Circular interpolation.
        MTS_VS = 10, // At start velocity.
        MTS_PMV = 11, // Point table move.
        MTS_PDW = 12, // Point table dwell move.
        MTS_PPS = 13, // Point table pause state.
        MTS_SLV = 14, // Slave axis move.
        MTS_JOG = 15, // Jog move.
        MTS_ASTP = 16, // Abnormal stop.
        MTS_SVONS = 17, // Servo off stopped.
        MTS_EMGS = 18, // EMG / SEMG stopped.
        MTS_ALMS = 19, // Alarm stop.
        MTS_WANS = 20, // Warning stopped.
        MTS_PELS = 21, // PEL stopped.
        MTS_MELS = 22, // MEL stopped.
        MTS_ECES = 23, // Error counter check level reaches and stopped.
        MTS_SPELS = 24, // Soft PEL stopped.
        MTS_SMELS = 25, // Soft MEL stopped.
        MTS_STPOA = 26, // Stop by others axes.
        MTS_GDCES = 27, // Gantry deviation error level reaches and stopped.
        MTS_GTM = 28, // Gantry mode turn on.
        MTS_PAPB = 29, // Pulsar mode turn on.

        //Following definition for PCI-8254/8
        MTS_MDN = 5, // Motion done. 0: In motion, 1: Motion done ( It could be abnormal stop)
        MTS_WAIT = 10, // Axis is in waiting state. ( Wait move trigger )
        MTS_PTB = 11, // Axis is in point buffer moving. ( When this bit on, MDN and ASTP will be cleared )
        MTS_BLD = 17, // Axis (Axes) in blending moving
        MTS_PRED = 18, // Pre-distance event, 1: event arrived. The event will be clear when axis start moving 
        MTS_POSTD = 19, // Post-distance event. 1: event arrived. The event will be clear when axis start moving
        MTS_GER = 28, // 1: In geared ( This axis as slave axis and it follow a master specified in axis parameter. )

        //define error code
        ERR_NoError = 0, //No Error	
        ERR_OSVersion = -1, // Operation System type mismatched
        ERR_OpenDriverFailed = -2, // Open device driver failed - Create driver interface failed
        ERR_InsufficientMemory = -3, // System memory insufficiently
        ERR_DeviceNotInitial = -4, // Cards not be initialized
        ERR_NoDeviceFound = -5, // Cards not found(No card in your system)
        ERR_CardIdDuplicate = -6, // Cards' ID is duplicated. 
        ERR_DeviceAlreadyInitialed = -7, // Cards have been initialed 
        ERR_InterruptNotEnable = -8, // Cards' interrupt events not enable or not be initialized
        ERR_TimeOut = -9, // Function time out
        ERR_ParametersInvalid = -10, // Function input parameters are invalid
        ERR_SetEEPROM = -11, // Set data to EEPROM (or nonvolatile memory) failed
        ERR_GetEEPROM = -12, // Get data from EEPROM (or nonvolatile memory) failed
        ERR_FunctionNotAvailable = -13, // Function is not available in this step, The device is not support this function or Internal process failed
        ERR_FirmwareError = -14, // Firmware error, please reboot the system
        ERR_CommandInProcess = -15, // Previous command is in process
        ERR_AxisIdDuplicate = -16, // Axes' ID is duplicated.
        ERR_ModuleNotFound = -17, // Slave module not found.
        ERR_InsufficientModuleNo = -18, // System ModuleNo insufficiently
        ERR_HandShakeFailed = -19, // HandSake with the DSP out of time.
        ERR_FILE_FORMAT = -20, // Config file format error.(cannot be parsed)
        ERR_ParametersReadOnly = -21, // Function parameters read only.
        ERR_DistantNotEnough = -22, // Distant is not enough for motion.
        ERR_FunctionNotEnable = -23, // Function is not enabled.
        ERR_ServerAlreadyClose = -24, // Server already closed.
        ERR_DllNotFound = -25, // Related dll is not found, not in correct path.
        ERR_TrimDAC_Channel = -26,
        ERR_Satellite_Type = -27,
        ERR_Over_Voltage_Spec = -28,
        ERR_Over_Current_Spec = -29,
        ERR_SlaveIsNotAI = -30,
        ERR_Over_AO_Channel_Scope = -31,
        ERR_DllFuncFailed = -32, // Failed to invoke dll function. Extension Dll version is wrong.
        ERR_FeederAbnormalStop = -33, //Feeder abnormal stop, External stop or feeding stop
        ERR_Read_ModuleType_Dismatch = -34,
        ERR_Win32Error = -1000, // No such INT number, or WIN32_API error, contact with ADLINK's FAE staff.
        ERR_DspStart = -2000, // The base for DSP error

        // Motion IO status bit value define.
        MIO_ALM_V = 0x1, // Servo alarm.
        MIO_PEL_V = 0x2, // Positive end limit.
        MIO_MEL_V = 0x4, // Negative end limit.
        MIO_ORG_V = 0x8, // ORG (Home).
        MIO_EMG_V = 0x10, // Emergency stop.
        MIO_EZ_V = 0x20, // EZ.
        MIO_INP_V = 0x40, // In position.
        MIO_SVON_V = 0x80, // Servo on signal.
        MIO_RDY_V = 0x100, // Ready.
        MIO_WARN_V = 0x200, // Warning.
        MIO_ZSP_V = 0x400, // Zero speed.
        MIO_SPEL_V = 0x800, // Soft positive end limit.
        MIO_SMEL_V = 0x1000, // Soft negative end limit.
        MIO_TLC_V = 0x2000, // Torque is limited by torque limit value.
        MIO_ABSL_V = 0x4000, // Absolute position lost.
        MIO_STA_V = 0x8000, // External start signal.
        MIO_PSD_V = 0x10000, // Positive slow down signal.
        MIO_MSD_V = 0x20000, // Negative slow down signal.

// Motion status bit define.
        MTS_CSTP_V = 0x1, // Command stop signal. 
        MTS_VM_V = 0x2, // At maximum velocity.
        MTS_ACC_V = 0x4, // In acceleration.
        MTS_DEC_V = 0x8, // In deceleration.
        MTS_DIR_V = 0x10, // (Last)Moving direction.
        MTS_NSTP_V = 0x20, // Normal stop(Motion done).
        MTS_HMV_V = 0x40, // In home operation.
        MTS_SMV_V = 0x80, // Single axis move( relative, absolute, velocity move).
        MTS_LIP_V = 0x100, // Linear interpolation.
        MTS_CIP_V = 0x200, // Circular interpolation.
        MTS_VS_V = 0x400, // At start velocity.
        MTS_PMV_V = 0x800, // Point table move.
        MTS_PDW_V = 0x1000, // Point table dwell move.
        MTS_PPS_V = 0x2000, // Point table pause state.
        MTS_SLV_V = 0x4000, // Slave axis move.
        MTS_JOG_V = 0x8000, // Jog move.
        MTS_ASTP_V = 0x10000, // Abnormal stop.
        MTS_SVONS_V = 0x20000, // Servo off stopped.
        MTS_EMGS_V = 0x40000, // EMG / SEMG stopped.
        MTS_ALMS_V = 0x80000, // Alarm stop.
        MTS_WANS_V = 0x100000, // Warning stopped.
        MTS_PELS_V = 0x200000, // PEL stopped.
        MTS_MELS_V = 0x400000, // MEL stopped.
        MTS_ECES_V = 0x800000, // Error counter check level reaches and stopped.
        MTS_SPELS_V = 0x1000000, // Soft PEL stopped.
        MTS_SMELS_V = 0x2000000, // Soft MEL stopped.
        MTS_STPOA_V = 0x4000000, // Stop by others axes.
        MTS_GDCES_V = 0x8000000, // Gantry deviation error level reaches and stopped.
        MTS_GTM_V = 0x10000000, // Gantry mode turn on.
        MTS_PAPB_V = 0x20000000, // Pulsar mode turn on.

        // PointTable, option
        PT_OPT_ABS = 0x00000000, // move, absolute
        PT_OPT_REL = 0x00000001, // move, relative
        PT_OPT_LINEAR = 0x00000000, // move, linear
        PT_OPT_ARC = 0x00000004, // move, arc
        PT_OPT_FC_CSTP = 0x00000000, // signal, command stop (finish condition)
        PT_OPT_FC_INP = 0x00000010, // signal, in position
        PT_OPT_LAST_POS = 0x00000020, // last point index
        PT_OPT_DWELL = 0x00000040, // dwell
        PT_OPT_RAPID = 0x00000080, // rapid positioning
        PT_OPT_NOARC = 0x00010000, // do not add arc
        PT_OPT_SCUVE = 0x00000002 // s-curve
    }
}