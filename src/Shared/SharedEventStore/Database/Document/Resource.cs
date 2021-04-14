using Newtonsoft.Json;

namespace SuperCar.Shared.EventStore.Database.Document
{
    public abstract class Resource
    {
        protected Resource() { }
        protected Resource(Resource resource){}
        [JsonProperty(PropertyName = "id")] 
        public virtual string Id { get; set; }
        [JsonProperty(PropertyName = "_rid")]
        public virtual string ResourceId { get; set; }
        [JsonProperty(PropertyName = "_self")]
        public string SelfLink { get; }
        [JsonIgnore] 
        public string AltLink { get; set; }
        [JsonProperty(PropertyName = "_etag")]
        public string Etag { get; }
    }
}
