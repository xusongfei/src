namespace Lead.Detect.PrimVirtualCard.motionWrapper
{
    /// <summary>
    ///     axis motion params during move
    /// </summary>
    public class BitOperators
    {
        public static bool BIT_ENABLED(int word, int bit)
        {
            return (word & bit) != 0;
        }

        public static bool BIT_DISABLED(int word, int bit)
        {
            return (word & bit) == 0;
        }


        public static bool CheckBit(int sts, int bit, bool status)
        {
            if (status)
            {
                return BIT_ENABLED(sts, bit);
            }

            return BIT_DISABLED(sts, bit);
        }


        public static void SET_BITS(ref int word, int bits)
        {
            word |= bits;
        }

        public static void CLEAR_BITS(ref int word, int bits)
        {
            word &= ~bits;
        }


        public static void SetBit(ref int sts, int bit, bool status)
        {
            if (status)
            {
                SET_BITS(ref sts, bit);
            }
            else
            {
                CLEAR_BITS(ref sts, bit);
            }
        }
    }
}