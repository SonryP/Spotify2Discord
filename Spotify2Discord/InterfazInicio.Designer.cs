namespace Spotify2Discord {
    partial class InterfazInicio {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.lbl_Cancion = new System.Windows.Forms.Label();
            this.Btn_Comenzar = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Btn_Detener = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_Cancion
            // 
            this.lbl_Cancion.AutoSize = true;
            this.lbl_Cancion.Location = new System.Drawing.Point(12, 9);
            this.lbl_Cancion.Name = "lbl_Cancion";
            this.lbl_Cancion.Size = new System.Drawing.Size(85, 13);
            this.lbl_Cancion.TabIndex = 0;
            this.lbl_Cancion.Text = "Canción Actual: ";
            // 
            // Btn_Comenzar
            // 
            this.Btn_Comenzar.Location = new System.Drawing.Point(12, 38);
            this.Btn_Comenzar.Name = "Btn_Comenzar";
            this.Btn_Comenzar.Size = new System.Drawing.Size(214, 32);
            this.Btn_Comenzar.TabIndex = 1;
            this.Btn_Comenzar.Text = "Comenzar";
            this.Btn_Comenzar.UseVisualStyleBackColor = true;
            this.Btn_Comenzar.Click += new System.EventHandler(this.Btn_Comenzar_ClickAsync);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Btn_Detener
            // 
            this.Btn_Detener.Enabled = false;
            this.Btn_Detener.Location = new System.Drawing.Point(239, 38);
            this.Btn_Detener.Name = "Btn_Detener";
            this.Btn_Detener.Size = new System.Drawing.Size(214, 32);
            this.Btn_Detener.TabIndex = 2;
            this.Btn_Detener.Text = "Detener";
            this.Btn_Detener.UseVisualStyleBackColor = true;
            this.Btn_Detener.Click += new System.EventHandler(this.Btn_Detener_Click);
            // 
            // InterfazInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 75);
            this.Controls.Add(this.Btn_Detener);
            this.Controls.Add(this.Btn_Comenzar);
            this.Controls.Add(this.lbl_Cancion);
            this.Name = "InterfazInicio";
            this.Text = "Spotify2Discord";
            this.Load += new System.EventHandler(this.InterfazInicio_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Cancion;
        private System.Windows.Forms.Button Btn_Comenzar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button Btn_Detener;
    }
}

