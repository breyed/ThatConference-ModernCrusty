using CrustyBike.Messages;
using Microsoft.AspNet.SignalR.Client;
using PushSharp;
using PushSharp.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CrustyBike.WebClients
{
	[ComSourceInterfaces(typeof(IDeviceConnectorClientHandler))]
	public class DeviceConnectorClient
	{
		HubConnection hubConnection;
		IHubProxy hub;
		readonly PushBroker pushBroker = new PushBroker();

		public delegate MessageToDevice DeviceMessageReceivedHandler(MessageFromDevice request);
		public event DeviceMessageReceivedHandler DeviceMessageReceived;

		public void Connect()
		{
			hubConnection = new HubConnection("http://localhost:9346");
			hub = hubConnection.CreateHubProxy("OfficeHub");
			hub.On<byte[]>("OnRequest", OnRequest);
			hubConnection.Start().Wait();
		}

		public void SendPushNotification(string[] registrationIds)
		{
			pushBroker.QueueNotification(new GcmNotification { RegistrationIds = registrationIds.ToList() });
		}

		void OnRequest(byte[] request)
		{
			if (DeviceMessageReceived == null) throw new InvalidOperationException();
			byte[] response = DeviceMessageReceived(MessageFromDevice.ParseFrom(request)).ToByteArray();
			hub.Invoke("OnResponse", response);
		}
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface IDeviceConnectorClientHandler
	{
		MessageToDevice DeviceMessageReceived(MessageFromDevice request);
	}
}
