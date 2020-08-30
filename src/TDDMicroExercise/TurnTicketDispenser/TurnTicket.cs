namespace TDDMicroExercises.TurnTicketDispenser
{
    /// <summary>Turn ticket</summary>
    public class TurnTicket
    {
        /// <summary>Gets the turn number.</summary>
        /// <value>The turn number.</value>
        public int TurnNumber { get; }

        /// <summary>Initializes a new instance of the <a onclick="return false;" href="TurnTicket" originaltag="see">TurnTicket</a> class.</summary>
        /// <param name="turnNumber">The turn number.</param>
        public TurnTicket(int turnNumber)
        {
            TurnNumber = turnNumber;
        }


    }
}