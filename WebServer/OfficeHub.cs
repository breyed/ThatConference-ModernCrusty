using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace CrustyBike.WebServer
{
	public class OfficeHub : Hub<IDeviceMessageExchange>
	{
		void OnResponse(byte[] request)
		{
			DeviceController.ExchangeCompletion.SetResult(request);
        }
	}
}
