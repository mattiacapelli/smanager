namespace client_smanager
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_action = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_auth = new System.Windows.Forms.Label();
            this.lbl_connection = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_action
            // 
            this.btn_action.Location = new System.Drawing.Point(12, 100);
            this.btn_action.Name = "btn_action";
            this.btn_action.Size = new System.Drawing.Size(253, 59);
            this.btn_action.TabIndex = 0;
            this.btn_action.Text = "btn_action";
            this.btn_action.UseVisualStyleBackColor = true;
            this.btn_action.Click += new System.EventHandler(this.btn_action_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(238, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "<>";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lbl_auth
            // 
            this.lbl_auth.AutoSize = true;
            this.lbl_auth.Location = new System.Drawing.Point(12, 173);
            this.lbl_auth.Name = "lbl_auth";
            this.lbl_auth.Size = new System.Drawing.Size(44, 13);
            this.lbl_auth.TabIndex = 2;
            this.lbl_auth.Text = "lbl_auth";
            // 
            // lbl_connection
            // 
            this.lbl_connection.AutoSize = true;
            this.lbl_connection.Location = new System.Drawing.Point(12, 193);
            this.lbl_connection.Name = "lbl_connection";
            this.lbl_connection.Size = new System.Drawing.Size(76, 13);
            this.lbl_connection.TabIndex = 3;
            this.lbl_connection.Text = "lbl_connection";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 215);
            this.Controls.Add(this.lbl_connection);
            this.Controls.Add(this.lbl_auth);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_action);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_action;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_auth;
        private System.Windows.Forms.Label lbl_connection;
    }
}

