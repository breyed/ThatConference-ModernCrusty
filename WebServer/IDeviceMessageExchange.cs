using CrustyBike.Messages;

namespace CrustyBike.WebServer
{
	public interface IDeviceMessageExchange
	{
		void OnRequest(byte[] request);
	}
}