using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spotify2Discord {
    class Bot {
        public string song;
        public string asong;
        public string token;


        public Bot(string tok) {
            token = tok;
            Task.Run(MainAsync);
        }   
 
        private DiscordSocketClient _client;

        public async Task MainAsync() {
            song = "";
            _client = new DiscordSocketClient();
            await _client.LoginAsync(TokenType.User, token);
            await _client.StartAsync();
            await _client.SetStatusAsync(UserStatus.Invisible);
            await MusicNew();
        }

        public async Task MusicNew() {
            if (asong != song) {
                if(asong == "Spotify - Stopped") {
                    await _client.SetGameAsync("");
                    song = asong;
                } else {
                    await _client.SetGameAsync(asong,null,(Discord.StreamType) 2);
                    song = asong;
                }
            }
            
        }

        public async Task MusicStop() {
            await _client.SetGameAsync("");
            await _client.StopAsync();
            await _client.LogoutAsync();
        }

        private Task _client_Ready() {
            throw new NotImplementedException();
        }
    }
}
