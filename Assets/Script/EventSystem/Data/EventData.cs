namespace Gulch
{
    public class EventData
    {
        private EventType eventType;
        public EventData(EventType eventType)
        {
            this.eventType = eventType;
        }

        public override string ToString()
        {
            return eventType.ToString();
        }
    }
}
