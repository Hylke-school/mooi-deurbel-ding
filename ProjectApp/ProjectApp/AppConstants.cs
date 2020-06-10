using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectApp
{
    public static class AppConstants
    {
        public static string NotificationChannelName { get; set; } = "XamarinNotifyChannel";
        public static string NotificationHubName { get; set; } = "mooi-deurbel-ding";
        public static string ListenConnectionString { get; set; } = "Endpoint=sb://mooi-ding.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=Qa9Y8+xLYclZiL5WWJISK23uv7uvf5vYEaFBUf1DiBo=";
        public static string DebugTag { get; set; } = "XamarinNotify";
        public static string[] SubscriptionTags { get; set; } = { "default" };
        public static string FCMTemplateBody { get; set; } = "{\"data\":{\"message\":\"$(messageParam)\"}}";
        public static string APNTemplateBody { get; set; } = "{\"aps\":{\"alert\":\"$(messageParam)\"}}";
    }
}
