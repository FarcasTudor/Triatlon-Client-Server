using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriatlonNetworking.rpProtocols
{
    [Serializable]
    public class Response
    {
        public ResponseType type { get; private set; }

        public object data { get; private set; }

        public override string ToString()
        {
            return "Response{" +
                "type='" + type + '\'' +
                ", data='" + data + '\'' +
                '}';
        }

        public class Builder
        {
            private Response response = new Response();

            public Builder type(ResponseType type)
            {
                response.type = type;
                return this;
            }

            public Builder data(object data)
            {
                response.data = data;
                return this;
            }

            public Response build()
            {
                return response;
            }
        }
    }
}
