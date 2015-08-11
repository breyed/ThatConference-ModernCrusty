# Tie modern mobile apps to crusty backends with Protocol Buffers

[That Conference 2015 session](https://www.thatconference.com/sessions/session/6992) sample code.

### Session abstract

You don’t need to be a construction company to stay out of the rain on the campsite. With the right materials – canvas, polls, and stakes – you can build a manageable and reliable solution. Likewise, you often don’t need dedicated teams of data access and UX gurus to build a data-rich frontend to your web app.

Learn how AngularJS’s kitchen sink approach to data binding and control logic, Breeze’s client-side entity tracking, and TypeScript’s strongly typed comfort bubble, combine to empower mere mortals to quickly and elegantly plum their business data to modern web apps.


### Solution contents

The solution stubs out the projects for an inventory management system for Crusty Bicycle Works, which an old on-premise server in the office and a brand new Android app. The solution has the following top-level folders:

#### Android

A stock Android Studio native app in Java. `SyncService` performs HTTP-based message exchange. `PartListActivity.onCreate` uses Protocol Buffers messages from office server.

#### WebServer

The ASP.NET web server that relays messages between the devices and the office server. `IDeviceMessageExchange` defines the `OnRequest` method that the hub calls to send a message from the device to the office server. The `OfficeHub` class defines the SignalR hub, which includes the `OnResponse` method that the office server calls to respond to the device.

`DeviceController` is the Web API controller that accepts HTTP requests containing the message from the device to the office server and responds with the office server's response message.

Note that in this example, the web server does not use the Protocol Buffers messages itself. It just relays them as opaque byte arrays. As you migrate functionality from the legacy office server into the cloud, you'll likely find it useful for the web server to inspect and alter the messages.

#### WebClients

The .NET helper library used by the office server. In this sample, the FoxPro application uses it via COM interop.

The message exchange protocol is defined in `DeviceMessages.proto`. The project's pre-build event runs the Protocol Buffers code generators, which output `DeviceMessages.cs` and `DeviceMessages.java`. Each message translates to a separate class. In `AssemblyInfo.cs`, `[assembly: ComVisible(true)]` causes all those classes to become COM objects if you register the DLL with this command:

    C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe /codebase /tlb WebClients\bin\Debug\CrustyBike.WebClients.dll

`DeviceConnectorClient` connects to the web server via SignalR. It calls the `DeviceMessageReceived` event when it receives a message from the device. It forwards the response message (the return value) back to the web server (which relays it back to the device).

`SendPushNotification` sends an empty push notification to a device to tell it to wake up and perform a message exchange. The office server calls this when it has something new for the device. This allows the device to keep its data fresh so that when the user opens the app, the correct data is already present.

#### FoxPro

`DeviceConnector.prg` is a stub for a FoxPro class that handles `DeviceMessageReceived` events, processes the device requests and returns device responses.