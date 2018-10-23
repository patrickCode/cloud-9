using System.Collections.Generic;

namespace Contracts
{
    public class AggregateRoot
    {
        private List<Event> _unsavedEvents = new List<Event>();
        protected void AddEvent(Event @event) 
        {
            _unsavedEvents.Add(@event);
        }

        public void SaveEvents()
        {
            _unsavedEvents = new List<Event>();
        }

        public IList<Event> GetUnsavedEvents()
        {
            return _unsavedEvents;
        }
    }
}