using System;
using System.Collections.Generic;
using WrkOrdr.TestObjects;

namespace WrkOrdr.Framework
{
    public class BaseEntity<TId> : IAggregate<TId>
    {
        protected readonly Dictionary<Type, Action<IVersionedEvent<TId>>> _handlers = new Dictionary<Type, Action<IVersionedEvent<TId>>>();

        protected void Handles<TEvent>(Action<TEvent> handler) where TEvent : IEvent<TId>
        {
            _handlers.Add(typeof(TEvent), @event => handler((TEvent)@event));
        }

        protected BaseEntity(TId id)
        {
            Id = id;
        }

        public TId Id { get; }
    }
}