package crc6494e14b9856016c30;


public class PNFirebaseMessagingService
	extends com.google.firebase.messaging.FirebaseMessagingService
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMessageReceived:(Lcom/google/firebase/messaging/RemoteMessage;)V:GetOnMessageReceived_Lcom_google_firebase_messaging_RemoteMessage_Handler\n" +
			"n_onNewToken:(Ljava/lang/String;)V:GetOnNewToken_Ljava_lang_String_Handler\n" +
			"";
		mono.android.Runtime.register ("Plugin.FirebasePushNotification.PNFirebaseMessagingService, Plugin.FirebasePushNotification", PNFirebaseMessagingService.class, __md_methods);
	}


	public PNFirebaseMessagingService ()
	{
		super ();
		if (getClass () == PNFirebaseMessagingService.class)
			mono.android.TypeManager.Activate ("Plugin.FirebasePushNotification.PNFirebaseMessagingService, Plugin.FirebasePushNotification", "", this, new java.lang.Object[] {  });
	}


	public void onMessageReceived (com.google.firebase.messaging.RemoteMessage p0)
	{
		n_onMessageReceived (p0);
	}

	private native void n_onMessageReceived (com.google.firebase.messaging.RemoteMessage p0);


	public void onNewToken (java.lang.String p0)
	{
		n_onNewToken (p0);
	}

	private native void n_onNewToken (java.lang.String p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
