using System.Net;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
	[BsonIgnoreExtraElements]
	public class OuterConfig: AConfigComponent
	{
		public string Host { get; set; }
		public int Port { get; set; }
        public string Host2 { get; set; }

		[BsonIgnore]
		public string Address
		{
			get
			{
				return $"{this.Host}:{this.Port}";
			}
		}

        [BsonIgnore]
        public string Address2
        {
            get
            {
                return $"{this.Host2}:{this.Port}";
            }
        }

        [BsonIgnore]
		public IPEndPoint IPEndPoint
		{
			get
			{
				return NetworkHelper.ToIPEndPoint(this.Host, this.Port);
			}
		}

        [BsonIgnore]
        public IPEndPoint IPEndPoint2
        {
            get
            {
                return NetworkHelper.ToIPEndPoint(this.Host2, this.Port);
            }
        }
    }
}