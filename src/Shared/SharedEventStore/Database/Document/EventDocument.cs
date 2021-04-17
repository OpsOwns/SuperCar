using SuperCar.Shared.Domain.Abstraction;
using System;

namespace SuperCar.Shared.EventStore.Database.Document
{
    public sealed class EventDocument : Resource
    {
        public string StreamId { get;  set; }
        public int Version { get;  set; }
        public string Payload { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string AssemblyQualifiedName { get; set; }
        public string AssemblyName { get; set; }
        public EventDocument(){}
        public EventDocument(Identity streamId, int version)
        {
            StreamId = streamId.Value.ToString();
            Version = ++version;
            Id = $"{streamId.Value}_{version}";
        }
    }
}
