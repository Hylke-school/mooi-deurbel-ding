using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;

using Plugin.PushNotification;
using Plugin.LocalNotifications;

using ProjectApp.Database;
using System.Linq;

namespace ProjectApp.Droid
{
    [Activity(Label = "ProjectApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        /// <summary>
        /// This runs on creation of the Android app
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            #region startup code
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            #endregion

            //Setting notificaton channel, which is used to target the app
            PushNotificationManager.DefaultNotificationChannelId = "notifications";
            PushNotificationManager.DefaultNotificationChannelName = "notifications";

            //initializes the notification manager plugin
            PushNotificationManager.Initialize(this, false);
            //Triggers when a notification is received, runs in a service, works even when app isn't running
            CrossPushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                //gets data from notification
                var data = p.Data;
                //declares variables
                object title; object body;
                string titlestring = ""; string bodystring = "";
                //gets title and body from Dictionary, puts them into string
                if (data.TryGetValue("title", out title)) titlestring = title.ToString();
                if (data.TryGetValue("body", out body)) bodystring = body.ToString();
                //sets notification icon
                LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.bell;
                //shows notification with correct title and string, uses localnotifications plugin
                CrossLocalNotifications.Current.Show(titlestring, bodystring);
                //updates database with n ew notification, only works when app is running
                DatabaseManager.StoreCurrentDate();
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            PushNotificationManager.ProcessIntent(this, intent);
        }
    }
}