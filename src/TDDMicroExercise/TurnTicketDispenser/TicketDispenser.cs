namespace TDDMicroExercises.TurnTicketDispenser
{
    /// <summary>The ticket dispenser</summary>
    public class TicketDispenser
    {
        /// <summary>Gets the turn ticket.</summary>
        /// <returns>The turn ticket</returns>
        public TurnTicket GetTurnTicket()
        {
            int newTurnNumber = TurnNumberSequence.GetNextTurnNumber();
            var newTurnTicket = new TurnTicket(newTurnNumber);
            return newTurnTicket;
        }
    }
}
