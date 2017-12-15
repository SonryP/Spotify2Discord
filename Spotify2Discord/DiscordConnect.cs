using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Spotify2Discord {
    class DiscordConnect {
        DiscordRpc.RichPresence presence = new DiscordRpc.RichPresence();
        DiscordRpc.JoinRequest joinRequest;
        DiscordRpc.EventHandlers handlers;
        Int32 timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        public string details;
        public string state;
        public string imageKey;
        public string imageText;

        public DiscordConnect(string details, string state, int duration, int current) {
            this.details = details;
            this.state = state;
            handlers = new DiscordRpc.EventHandlers();
            DiscordRpc.Initialize("APPCODEHERE", ref handlers, true, "");
            presence.details = Utf8String(details);
            presence.state = Utf8String(state);
            timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            presence.startTimestamp = timeStamp - current;
            presence.endTimestamp =  timeStamp + duration;
            presence.largeImageText = "Now Playing";
            presence.largeImageKey = "test";
            presence.smallImageKey = imageKey;
            presence.smallImageText = imageText;
            presence.instance = false;
            DiscordRpc.UpdatePresence(ref presence);
            DiscordRpc.RunCallbacks();
        }

        private static UTF8Encoding UTF8 { get; } = new UTF8Encoding(false);

        public IntPtr Utf8String(string str) {
            var bts = UTF8.GetBytes(str);

            var ptr = Marshal.AllocHGlobal(bts.Length + 1);
            Marshal.Copy(bts, 0, ptr, bts.Length);
            bts[0] = 0;
            Marshal.Copy(bts, 0, ptr + bts.Length, 1);

            return ptr;
        }
        

        public void UpdateStatus(int duration, int current) {
            presence.details = Utf8String("👤 " + details);
            presence.state = Utf8String("🎵 " + state);
            timeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            int start = timeStamp;
            presence.startTimestamp = start - current;
            presence.endTimestamp = (start - current) + duration;
            presence.largeImageText = "Now Playing";
            presence.largeImageKey = "test";
            presence.smallImageKey = imageKey;
            presence.smallImageText = imageText;
            presence.instance = false;
            DiscordRpc.UpdatePresence(ref presence);
            DiscordRpc.RunCallbacks();
        }

        public void StopStatus() {
            DiscordRpc.Shutdown();
        }
    }
}
