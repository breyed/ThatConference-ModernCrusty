* Connects to the relay and processes device messages.
DEFINE CLASS DeviceConnector AS Session OLEPUBLIC
	HIDDEN client
	IMPLEMENTS IDeviceConnectorClientHandler IN CrustyBike.WebClients.DeviceConnectorClient

	PROCEDURE Connect(endpoint as String, apiKey as String) AS VOID
		this.client = CreateDotNetObject("CrustyBike.WebClients.DeviceConnectorClient")
		EVENTHANDLER(this.client, this)
		this.client.Connect()
	ENDPROC

	PROCEDURE IDeviceConnectorClientHandler_DeviceMessageReceived(request AS CrustyBike.Messages.MessageFromDevice) AS CrustyBike.Messages.MessageToDevice
		response = CreateDotNetObject("CrustyBike.Messages.MessageToDevice")
		
		* Store data from request.RegistrationId, request.PurchasePartIds, etc.
		
		* If the part list has changed, set the response.PartCategories to the new contents.

		RETURN response
	ENDPROC
ENDDEFINE

PROCEDURE CreateDotNetObject(name as String) AS Object
	obj = CREATEOBJECT(name)
	COMARRAY(obj, 110) && Indicates that the COM object uses arrays that are 0-based, fixed size, and passed by reference.
	RETURN obj
ENDPROC
