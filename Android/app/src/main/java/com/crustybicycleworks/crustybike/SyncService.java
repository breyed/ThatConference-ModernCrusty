package com.crustybicycleworks.crustybike;

import android.app.IntentService;
import android.content.Intent;
import android.support.v4.content.LocalBroadcastManager;

import java.io.BufferedInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;

public class SyncService extends IntentService {
    public static final String SYNC_COMPLETE_KEY = "SyncComplete";

    static DeviceMessages.MessageFromDevice messageFromDevice;
    static DeviceMessages.MessageToDevice messageToDevice;

    public SyncService() {
        super("SyncService");
    }

    @Override
    protected void onHandleIntent(Intent intent) {
        try {
            // Opens connection.
            HttpURLConnection connection = (HttpURLConnection) new URL("http://localhost:9346/api/device").openConnection();
            connection.setDoOutput(true);
            OutputStream output = connection.getOutputStream(); // Blocks waiting for the connection.

            // Sends request.
            messageFromDevice.writeTo(output);

            // Reads response.
            InputStream input = new BufferedInputStream(connection.getInputStream());
            messageToDevice = DeviceMessages.MessageToDevice.parseFrom(input);

            // Notify UI that sync is complete.
            LocalBroadcastManager.getInstance(getBaseContext()).sendBroadcast(new Intent(SYNC_COMPLETE_KEY));
        } catch (IOException e) {
            // Retry later. Consider using ConnectivityRetryManager. http://stackoverflow.com/q/18923663/145173
        }
    }
}
