using System;
using EventSource.Framework;
using Raven.Client;

namespace Manufacturing.Domain
{
    public class EventStore : IEventStore
    {
        private readonly IDocumentStore documentStore;

        public EventStore(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        public bool Write<T>(T aggregate)
        {
            using (var session = documentStore.OpenSession())
            {
                session.Store(aggregate);

                session.SaveChanges();
            }

            return true;
        }

        public T Get<T>(Guid id)
        {
            T result;

            using (var session = documentStore.OpenSession())
            {
                result = session.Load<T>(typeof(T).Name + "/" + id);
            }

            return result;
        }

        public TEventType AddEvent<TEventType>(Guid id, IVersionedEvent<Guid> eventItem) where TEventType : EventContainer
        {

            EventContainer result;

            using (var session = documentStore.OpenSession())
            {

                result = session.Load<TEventType>(typeof(TEventType).Name + "/" + id);

                if (result == null)
                {
                    result = (TEventType)Activator.CreateInstance(typeof(TEventType), id);
                    result.AddEvent(eventItem);
                    session.Store(result);
                }
                else
                {
                    result.AddEvent(eventItem);
                }                

                session.SaveChanges();
            }

            return (TEventType)result;
        }
    }
}