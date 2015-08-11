using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CrustyBike.WebServer
{
	public class DeviceController : ApiController
	{
		static readonly IHubContext<IDeviceMessageExchange> officeHubContext = GlobalHost.ConnectionManager.GetHubContext<OfficeHub, IDeviceMessageExchange>();

		public static TaskCompletionSource<byte[]> ExchangeCompletion { get; private set; }

		public HttpResponseMessage Post(HttpRequestMessage request)
		{
			// Serializes the request/response sequences. Use an async approach if you need more scalability.
			lock (officeHubContext) {

				// Relays the device message to the office server.
				officeHubContext.Clients.All.OnRequest(request.Content.ReadAsByteArrayAsync().Result);

				// Waits for and relays back with the response.
				ExchangeCompletion = new TaskCompletionSource<byte[]>();
				return new HttpResponseMessage { Content = new ByteArrayContent(ExchangeCompletion.Task.Result) };
			}
        }
	}
}