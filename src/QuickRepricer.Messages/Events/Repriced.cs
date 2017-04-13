using System.Collections.Generic;

namespace QuickRepricer.Messages.Events
{
    public class InvetoryChangeEvent
    {
        public string ASIM { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }

        public InvetoryChangeEvent(string asim, string name, double price)
        {
            ASIM = asim;
            Name = name;
            Price = price;
        }
    }


    public class ChangeEventsCollection<T>
        where T : class
    {
        public Dictionary<string, T> Changes { get; private set; }

        public ChangeEventsCollection(Dictionary<string, T> changes)
        {
            Changes = changes;
        }
    }

}
