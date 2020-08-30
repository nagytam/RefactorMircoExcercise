
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDDMicroExercises.TurnTicketDispenser;

namespace TDDMicroExercise.Test.TurnTicketDispenser
{
    [TestClass]
    public class TicketDispenserTest
    {
        [TestInitialize]
        public void Initialize()
        {
            TurnNumberSequence.Reset(); // reset the number sequence for each tests
        }

        #region Simulate existing client's behaviour
        [TestMethod]
        public void TicketDispenserClient()
        {
            var response1 = new TicketDispenser().GetTurnTicket();
            Assert.AreEqual(0, response1.TurnNumber);
            var response2 = new TicketDispenser().GetTurnTicket();
            Assert.AreEqual(1, response2.TurnNumber);
            var response3 = new TicketDispenser().GetTurnTicket();
            Assert.AreEqual(2, response3.TurnNumber);
        }

        [TestMethod]
        public void TurnNumberSequenceClient()
        {
            int nextUniqueTicketNumber;
            nextUniqueTicketNumber = TurnNumberSequence.GetNextTurnNumber();
            Assert.AreEqual(0, nextUniqueTicketNumber);
            nextUniqueTicketNumber = TurnNumberSequence.GetNextTurnNumber();
            Assert.AreEqual(1, nextUniqueTicketNumber);
            nextUniqueTicketNumber = TurnNumberSequence.GetNextTurnNumber();
            Assert.AreEqual(2, nextUniqueTicketNumber);
        }

        [TestMethod]
        public void TurnTicketAndSequenceClient()
        {
            var turnTicket1 = new TurnTicket(TurnNumberSequence.GetNextTurnNumber());
            Assert.AreEqual(0, turnTicket1.TurnNumber);
            var turnTicket2 = new TurnTicket(TurnNumberSequence.GetNextTurnNumber());
            Assert.AreEqual(1, turnTicket2.TurnNumber);
            var turnTicket3 = new TurnTicket(TurnNumberSequence.GetNextTurnNumber());
            Assert.AreEqual(2, turnTicket3.TurnNumber);
        }

        [TestMethod]
        public void TurnTicketClient()
        {
            var turnTicket1 = new TurnTicket(1);
            var turnTicket2 = new TurnTicket(2);
            var turnTicket3 = new TurnTicket(3);

            var num1 = turnTicket1.TurnNumber;
            Assert.AreEqual(1, num1);
            var num2 = turnTicket2.TurnNumber;
            Assert.AreEqual(2, num2);
            var num3 = turnTicket3.TurnNumber;
            Assert.AreEqual(3, num3);
        }

        #endregion

        [TestMethod]
        public void TicketDispenserReturnsSequence()
        {
            const int maxTickets = 1000;
            var ticketDispenser = new TicketDispenser();
            for (int i = 0; i < maxTickets; i++)
            {
                var response = ticketDispenser.GetTurnTicket();
                Assert.AreEqual(i, response.TurnNumber);
            }
        }

    }
}
