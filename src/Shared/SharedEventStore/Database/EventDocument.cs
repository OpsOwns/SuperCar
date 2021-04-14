using SuperCar.Shared.Domain.Abstraction;
using System;

namespace SuperCar.Shared.EventStore.Database
{
    public class EventDocument : Resource
    {
        public string StreamId { get; private set; }
        public int Version { get; private set; }
        public string Payload { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string AssemblyQualifiedName { get; set; }
        public EventDocument(){}
        public EventDocument(Identity streamId, int version)
        {
            StreamId = streamId.Id.ToString();
            Version = version;
            Id = $"{streamId.Id}_{version}";
        }
    }
}
