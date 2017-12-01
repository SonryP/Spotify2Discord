using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Discord.Commands;

namespace Spotify2Discord {
    public partial class InterfazInicio : Form {
        public InterfazInicio() {
            InitializeComponent();
        }
        Bot robot;
        string token = "";
        private void InterfazInicio_Load(object sender, EventArgs e) {
            //Get the user token from Token.txt on App Location
            token = System.IO.File.ReadAllText(Application.StartupPath + "\\Token.txt");
        }

        //Function from https://stackoverflow.com/questions/37854194/get-current-song-name-for-a-local-application
        public string GetSpotifyTrackInfo() {
            var proc = Process.GetProcessesByName("Spotify").FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));
            if (proc == null) {
                return "Spotify - Stopped";
            }

            if (string.Equals(proc.MainWindowTitle, "Spotify", StringComparison.InvariantCultureIgnoreCase)) {
                return "Spotify - Paused";
            }
            return proc.MainWindowTitle;
        }
        

        private void Btn_Comenzar_ClickAsync(object sender, EventArgs e) {
            Btn_Comenzar.Enabled = false;
            Btn_Detener.Enabled = true;
            lbl_Cancion.Text = "Canción Actual: " + GetSpotifyTrackInfo();
            robot = new Bot(token) {
                token = token,
                song = GetSpotifyTrackInfo()
            };
            timer1.Enabled = true;
        }
        private bool ComprobarRepeticion(string sng) {
            if(sng == GetSpotifyTrackInfo()) {
                return true;
            } else {
                return false;
            }
        }

        string music;
        private void Timer1_Tick(object sender, EventArgs e) {
            if (ComprobarRepeticion(music)) {
                lbl_Cancion.Text = "Canción Actual: " + GetSpotifyTrackInfo();
                music = GetSpotifyTrackInfo();
                robot.asong = music;
                robot.MusicNew();

            } else {
                lbl_Cancion.Text = "Canción Actual: " + GetSpotifyTrackInfo();
                music = GetSpotifyTrackInfo();
                robot.asong = music;
                robot.MusicNew();
            }
        }

        private void button1_Click(object sender, EventArgs e) {

        }

        private void Btn_Detener_Click(object sender, EventArgs e) {
            Btn_Comenzar.Enabled = true;
            timer1.Enabled = false;
            robot.MusicStop();
            Btn_Detener.Enabled = false;
        }
    }
}
