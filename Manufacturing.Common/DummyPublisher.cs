using EventSource.Framework;

namespace Manufacturing.Common
{
    public class DummyPublisher : IEventPublisher
    {
        public void Publish<TMessage>(TMessage message)
        {
        }
    }
}