using System.ComponentModel;

namespace Triatlon
{
    partial class MainController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewParticipanti = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.labelArbitru = new System.Windows.Forms.Label();
            this.textBoxArbitru = new System.Windows.Forms.TextBox();
            this.buttonAdaugaPunctaj = new System.Windows.Forms.Button();
            this.logOutButton = new System.Windows.Forms.Button();
            this.labelProba = new System.Windows.Forms.Label();
            this.textBoxProba = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAdaugaPunctaj = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParticipanti)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewParticipanti
            // 
            this.dataGridViewParticipanti.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewParticipanti.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewParticipanti.Location = new System.Drawing.Point(9, 102);
            this.dataGridViewParticipanti.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewParticipanti.Name = "dataGridViewParticipanti";
            this.dataGridViewParticipanti.RowHeadersWidth = 51;
            this.dataGridViewParticipanti.RowTemplate.Height = 24;
            this.dataGridViewParticipanti.Size = new System.Drawing.Size(280, 197);
            this.dataGridViewParticipanti.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(316, 102);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(275, 197);
            this.dataGridView1.TabIndex = 1;
            // 
            // labelArbitru
            // 
            this.labelArbitru.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelArbitru.Location = new System.Drawing.Point(6, 7);
            this.labelArbitru.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelArbitru.Name = "labelArbitru";
            this.labelArbitru.Size = new System.Drawing.Size(56, 24);
            this.labelArbitru.TabIndex = 2;
            this.labelArbitru.Text = "Arbitru:";
            this.labelArbitru.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxArbitru
            // 
            this.textBoxArbitru.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxArbitru.Location = new System.Drawing.Point(66, 11);
            this.textBoxArbitru.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxArbitru.Name = "textBoxArbitru";
            this.textBoxArbitru.ReadOnly = true;
            this.textBoxArbitru.Size = new System.Drawing.Size(98, 23);
            this.textBoxArbitru.TabIndex = 3;
            this.textBoxArbitru.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonAdaugaPunctaj
            // 
            this.buttonAdaugaPunctaj.Location = new System.Drawing.Point(229, 315);
            this.buttonAdaugaPunctaj.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAdaugaPunctaj.Name = "buttonAdaugaPunctaj";
            this.buttonAdaugaPunctaj.Size = new System.Drawing.Size(59, 28);
            this.buttonAdaugaPunctaj.TabIndex = 4;
            this.buttonAdaugaPunctaj.Text = "Adauga";
            this.buttonAdaugaPunctaj.UseVisualStyleBackColor = true;
            this.buttonAdaugaPunctaj.Click += new System.EventHandler(this.buttonAdaugaPunctaj_Click);
            // 
            // logOutButton
            // 
            this.logOutButton.Location = new System.Drawing.Point(499, 6);
            this.logOutButton.Margin = new System.Windows.Forms.Padding(2);
            this.logOutButton.Name = "logOutButton";
            this.logOutButton.Size = new System.Drawing.Size(92, 28);
            this.logOutButton.TabIndex = 5;
            this.logOutButton.Text = "Log Out";
            this.logOutButton.UseVisualStyleBackColor = true;
            this.logOutButton.Click += new System.EventHandler(this.logOutButton_Click);
            // 
            // labelProba
            // 
            this.labelProba.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProba.Location = new System.Drawing.Point(6, 47);
            this.labelProba.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelProba.Name = "labelProba";
            this.labelProba.Size = new System.Drawing.Size(56, 24);
            this.labelProba.TabIndex = 6;
            this.labelProba.Text = "Proba:";
            this.labelProba.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxProba
            // 
            this.textBoxProba.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProba.Location = new System.Drawing.Point(66, 48);
            this.textBoxProba.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxProba.Name = "textBoxProba";
            this.textBoxProba.ReadOnly = true;
            this.textBoxProba.Size = new System.Drawing.Size(98, 23);
            this.textBoxProba.TabIndex = 7;
            this.textBoxProba.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 321);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Adauga punctaj:";
            // 
            // textBoxAdaugaPunctaj
            // 
            this.textBoxAdaugaPunctaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAdaugaPunctaj.Location = new System.Drawing.Point(123, 318);
            this.textBoxAdaugaPunctaj.Name = "textBoxAdaugaPunctaj";
            this.textBoxAdaugaPunctaj.Size = new System.Drawing.Size(41, 23);
            this.textBoxAdaugaPunctaj.TabIndex = 9;
            // 
            // MainController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 391);
            this.Controls.Add(this.textBoxAdaugaPunctaj);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxProba);
            this.Controls.Add(this.labelProba);
            this.Controls.Add(this.logOutButton);
            this.Controls.Add(this.buttonAdaugaPunctaj);
            this.Controls.Add(this.textBoxArbitru);
            this.Controls.Add(this.labelArbitru);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dataGridViewParticipanti);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainController";
            this.Text = "MainController";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParticipanti)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewParticipanti;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labelArbitru;
        private System.Windows.Forms.TextBox textBoxArbitru;
        private System.Windows.Forms.Button buttonAdaugaPunctaj;
        private System.Windows.Forms.Button logOutButton;
        private System.Windows.Forms.Label labelProba;
        private System.Windows.Forms.TextBox textBoxProba;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAdaugaPunctaj;
    }
}