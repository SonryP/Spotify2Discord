using SpotifyAPI.Local;
using SpotifyAPI.Local.Models;
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

namespace Spotify2Discord {
    public partial class InterfazInicio : Form {
        public InterfazInicio() {
            InitializeComponent();
        }
        NotifyIcon notify = new NotifyIcon();
        SpotifyLocalAPI spotify;
        StatusResponse status;
        Track track;
        DiscordConnect dc;
        string song = "";

        private void InterfazInicio_Load(object sender, EventArgs e) {
            spotify = new SpotifyLocalAPI();
        }

        public bool Connect() {
            if (!SpotifyLocalAPI.IsSpotifyRunning()) {
                MessageBox.Show("Spotify isn't running!");
                return false;
            }
            if (!SpotifyLocalAPI.IsSpotifyWebHelperRunning()) {
                MessageBox.Show("SpotifyWebHelper isn't running!");
                return false;
            }

            bool successful = spotify.Connect();
            if (successful) {
                spotify.ListenForEvents = true;
                return true;
            } else {
                DialogResult res = MessageBox.Show("Couldn't connect to the spotify client. Retry?", "Spotify", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                    Connect();
                    return true;
            }
        }


        private void Btn_Comenzar_ClickAsync(object sender, EventArgs e) {
           
            if (Connect()) {
                Btn_Comenzar.Enabled = false;
                Btn_Detener.Enabled = true;
                status = spotify.GetStatus();
                track = status.Track;
                song = track.ArtistResource.Name + " - " + track.TrackResource.Name;
                lbl_Cancion.Text = "Current Song: " + song;
                songPositionBar.Maximum = track.Length;
                songPositionBar.Value = (int)status.PlayingPosition;
                dc = new DiscordConnect(track.ArtistResource.Name, track.TrackResource.Name, track.Length, (int)status.PlayingPosition);
                timer1.Enabled = true;
            }
        }

        private string SecondsToMinutes(int seconds) {
            TimeSpan t = TimeSpan.FromSeconds(seconds);

            string conversion = string.Format("{0:D2}:{1:D2}",
                t.Minutes,
                t.Seconds);
            return conversion;
        }
        
        public bool SpotTrack() {
            try{
                track = status.Track;
                return true;
            } catch{
                return false;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e) {
                status = spotify.GetStatus();
           if (SpotTrack()) {
                song = track.ArtistResource.Name + " - " + track.TrackResource.Name;
                notify.Text = "S2D: " + (song.Length >= 60 ? song.Substring(0, 50) : song);
                if (status.Playing) {
                    lbl_Cancion.Text = "Current Song: " + song + " > Playing";
                    dc.imageKey = "play";
                    dc.imageText = "Playing";
                } else {
                    lbl_Cancion.Text = "Current Song: " + song + " > Paused";
                    dc.imageKey = "pause";
                    dc.imageText = "Paused";
                }

                lblTotal.Text = SecondsToMinutes(track.Length);
                lblCurrent.Text = SecondsToMinutes((int)status.PlayingPosition);
                songPositionBar.Maximum = track.Length;
                songPositionBar.Value = (int)status.PlayingPosition;
                dc.details = track.ArtistResource.Name;
                dc.state = track.TrackResource.Name;
                dc.UpdateStatus(track.Length, (int)status.PlayingPosition);
            } else {
                    Btn_Comenzar.Enabled = true;
                    timer1.Enabled = false;
                    dc.StopStatus();
                    Btn_Detener.Enabled = false;
                    notify.BalloonTipText = "Spotify was Closed";
                    notify.BalloonTipTitle = "Spotify2Discord";
                    notify.Icon = this.Icon;
                    notify.Visible = true;
                    notify.ShowBalloonTip(100);
                }
                    
        }

        private void Btn_Detener_Click(object sender, EventArgs e) {
            Btn_Comenzar.Enabled = true;
            timer1.Enabled = false;
            dc.StopStatus();
            Btn_Detener.Enabled = false;
        }

        private void InterfazInicio_Resize(object sender, EventArgs e) {
            if (FormWindowState.Minimized == this.WindowState) {
                notify.Icon = this.Icon;
                notify.Visible = true;
                notify.MouseClick += notify_MouseClick;
                notify.BalloonTipIcon = ToolTipIcon.Info;
                this.Hide();
            } else if (FormWindowState.Normal == this.WindowState) {
                notify.Visible = false;
            }
        }

        private void notify_MouseClick(object sender, EventArgs e) {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void InterfazInicio_FormClosing(object sender, FormClosingEventArgs e) {
            if (dc != null) {
                timer1.Enabled = false;
                dc.StopStatus();
            }
        }

        private void lblCurrent_Click(object sender, EventArgs e) {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
