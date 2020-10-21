namespace SerialReader
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
            this.buttonConnect = new System.Windows.Forms.Button();
            this.comboBoxSerialPorts = new System.Windows.Forms.ComboBox();
            this.buttonRefreshSerialPorts = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.maskedTextBoxDNFUCode = new System.Windows.Forms.MaskedTextBox();
            this.buttonSendDNFUCode = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(207, 12);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(74, 60);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // comboBoxSerialPorts
            // 
            this.comboBoxSerialPorts.FormattingEnabled = true;
            this.comboBoxSerialPorts.Location = new System.Drawing.Point(80, 33);
            this.comboBoxSerialPorts.Name = "comboBoxSerialPorts";
            this.comboBoxSerialPorts.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSerialPorts.TabIndex = 1;
            // 
            // buttonRefreshSerialPorts
            // 
            this.buttonRefreshSerialPorts.BackgroundImage = global::SerialReader.Properties.Resources.res;
            this.buttonRefreshSerialPorts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonRefreshSerialPorts.Location = new System.Drawing.Point(12, 12);
            this.buttonRefreshSerialPorts.Name = "buttonRefreshSerialPorts";
            this.buttonRefreshSerialPorts.Size = new System.Drawing.Size(62, 60);
            this.buttonRefreshSerialPorts.TabIndex = 2;
            this.buttonRefreshSerialPorts.UseVisualStyleBackColor = true;
            this.buttonRefreshSerialPorts.Click += new System.EventHandler(this.buttonRefreshSerialPorts_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(287, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 60);
            this.button1.TabIndex = 3;
            this.button1.Text = "Disconnect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // maskedTextBoxDNFUCode
            // 
            this.maskedTextBoxDNFUCode.Location = new System.Drawing.Point(12, 123);
            this.maskedTextBoxDNFUCode.Mask = "00000000";
            this.maskedTextBoxDNFUCode.Name = "maskedTextBoxDNFUCode";
            this.maskedTextBoxDNFUCode.Size = new System.Drawing.Size(62, 20);
            this.maskedTextBoxDNFUCode.TabIndex = 5;
            // 
            // buttonSendDNFUCode
            // 
            this.buttonSendDNFUCode.Location = new System.Drawing.Point(80, 111);
            this.buttonSendDNFUCode.Name = "buttonSendDNFUCode";
            this.buttonSendDNFUCode.Size = new System.Drawing.Size(134, 47);
            this.buttonSendDNFUCode.TabIndex = 6;
            this.buttonSendDNFUCode.Text = "buttonSendDNFUCode";
            this.buttonSendDNFUCode.UseVisualStyleBackColor = true;
            this.buttonSendDNFUCode.Click += new System.EventHandler(this.buttonSendDNFUCode_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "GET_HW_VERSION",
            "GET_ERASE"});
            this.comboBox1.Location = new System.Drawing.Point(12, 203);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(147, 189);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(134, 47);
            this.button2.TabIndex = 8;
            this.button2.Text = "buttonSendCmd";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 422);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.buttonSendDNFUCode);
            this.Controls.Add(this.maskedTextBoxDNFUCode);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonRefreshSerialPorts);
            this.Controls.Add(this.comboBoxSerialPorts);
            this.Controls.Add(this.buttonConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.ComboBox comboBoxSerialPorts;
        private System.Windows.Forms.Button buttonRefreshSerialPorts;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxDNFUCode;
        private System.Windows.Forms.Button buttonSendDNFUCode;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button2;
    }
}

