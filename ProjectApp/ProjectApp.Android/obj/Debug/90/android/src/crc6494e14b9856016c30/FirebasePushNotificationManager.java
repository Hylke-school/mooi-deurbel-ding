package crc6494e14b9856016c30;


public class FirebasePushNotificationManager
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.tasks.OnCompleteListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onComplete:(Lcom/google/android/gms/tasks/Task;)V:GetOnComplete_Lcom_google_android_gms_tasks_Task_Handler:Android.Gms.Tasks.IOnCompleteListenerInvoker, Xamarin.GooglePlayServices.Tasks\n" +
			"";
		mono.android.Runtime.register ("Plugin.FirebasePushNotification.FirebasePushNotificationManager, Plugin.FirebasePushNotification", FirebasePushNotificationManager.class, __md_methods);
	}


	public FirebasePushNotificationManager ()
	{
		super ();
		if (getClass () == FirebasePushNotificationManager.class)
			mono.android.TypeManager.Activate ("Plugin.FirebasePushNotification.FirebasePushNotificationManager, Plugin.FirebasePushNotification", "", this, new java.lang.Object[] {  });
	}


	public void onComplete (com.google.android.gms.tasks.Task p0)
	{
		n_onComplete (p0);
	}

	private native void n_onComplete (com.google.android.gms.tasks.Task p0);

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
