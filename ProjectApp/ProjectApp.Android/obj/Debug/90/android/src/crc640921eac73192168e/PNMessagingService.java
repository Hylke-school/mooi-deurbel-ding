package crc640921eac73192168e;


public class PNMessagingService
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
		mono.android.Runtime.register ("Plugin.PushNotification.PNMessagingService, Plugin.PushNotification", PNMessagingService.class, __md_methods);
	}


	public PNMessagingService ()
	{
		super ();
		if (getClass () == PNMessagingService.class)
			mono.android.TypeManager.Activate ("Plugin.PushNotification.PNMessagingService, Plugin.PushNotification", "", this, new java.lang.Object[] {  });
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
