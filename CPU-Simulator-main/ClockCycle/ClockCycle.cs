namespace CPUManager
{
    public class ClockCycle
    {
        public int CycleCounter = 0;

        private ClockCycle() { }

        private static ClockCycle? instance = null;

        public static ClockCycle GetInstance()
        {
            if (instance == null)
            {
                instance = new ClockCycle();
            }
            return instance;
        }

        public void ClockCycleIncrement()
        {
            CycleCounter += 1;
        }
    }
}
