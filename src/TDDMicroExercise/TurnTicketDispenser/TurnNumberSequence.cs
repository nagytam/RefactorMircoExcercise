namespace TDDMicroExercises.TurnTicketDispenser
{
    /// <summary>Turn number sequence</summary>
    public static class TurnNumberSequence
    {
        #region 
        private static int _turnNumber = 0;

        private static object _lock = new object();
        #endregion

        /// <summary>Gets the next turn number.</summary>
        /// <returns>The next turn number</returns>
        public static int GetNextTurnNumber()
        {
            lock (_lock)
            {
                return _turnNumber++;
            }
        }
        /// <summary>Resets the sequence.</summary>
        public static void Reset()
        {
            lock (_lock)
            {
                _turnNumber = 0;
            }
        }
    }
}
