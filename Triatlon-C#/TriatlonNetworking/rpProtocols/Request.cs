using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriatlonNetworking.rpProtocols
{
    [Serializable]
    internal class Request
    {
        public RequestType type { get; private set; }

        public object data { get; private set; }

        public override string ToString()
        {
            return "Request{" +
                "type='" + type + '\'' +
                ", data='" + data + '\'' +
                '}';
        }
        public class Builder
        {
            private Request request = new Request();

            public Builder type(RequestType type)
            {
                request.type = type;
                return this;
            }

            public Builder data(object data)
            {
                request.data = data;
                return this;
            }

            public Request build()
            {
                return request;
            }
        }
    }
}
