using System;

namespace Contracts
{
    public class Event
    {
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EventName { get; set; }
    }
}