﻿
namespace OBBDSIIG.Forms.FrmIntegrar
{
    partial class FrmIntegrarMedicos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIntegrarMedicos));
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtCanMediExis = new System.Windows.Forms.TextBox();
            this.TxtCanMediForm = new System.Windows.Forms.TextBox();
            this.TxtCanMediFor = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.TxtPrefiPorFor = new System.Windows.Forms.TextBox();
            this.TxtPrefiCenFor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtInstanPortaFor = new System.Windows.Forms.TextBox();
            this.TxtInstanCenFor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnBuscarPacientes = new System.Windows.Forms.Button();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.LblCodEntiFac = new System.Windows.Forms.Label();
            this.lblNivelPermitido = new System.Windows.Forms.Label();
            this.lblNombreUser = new System.Windows.Forms.Label();
            this.lblCodigoUser = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(527, 102);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 15);
            this.label15.TabIndex = 69;
            this.label15.Text = "Integrar";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.TxtCanMediExis);
            this.groupBox1.Controls.Add(this.TxtCanMediForm);
            this.groupBox1.Controls.Add(this.TxtCanMediFor);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(16, 71);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(452, 117);
            this.groupBox1.TabIndex = 67;
            this.groupBox1.TabStop = false;
            // 
            // TxtCanMediExis
            // 
            this.TxtCanMediExis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCanMediExis.Location = new System.Drawing.Point(357, 80);
            this.TxtCanMediExis.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtCanMediExis.Name = "TxtCanMediExis";
            this.TxtCanMediExis.ReadOnly = true;
            this.TxtCanMediExis.Size = new System.Drawing.Size(85, 23);
            this.TxtCanMediExis.TabIndex = 24;
            this.TxtCanMediExis.Text = "0";
            this.TxtCanMediExis.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TxtCanMediForm
            // 
            this.TxtCanMediForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCanMediForm.Location = new System.Drawing.Point(357, 50);
            this.TxtCanMediForm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtCanMediForm.Name = "TxtCanMediForm";
            this.TxtCanMediForm.ReadOnly = true;
            this.TxtCanMediForm.Size = new System.Drawing.Size(85, 23);
            this.TxtCanMediForm.TabIndex = 23;
            this.TxtCanMediForm.Text = "0";
            this.TxtCanMediForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TxtCanMediFor
            // 
            this.TxtCanMediFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCanMediFor.Location = new System.Drawing.Point(357, 22);
            this.TxtCanMediFor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtCanMediFor.Name = "TxtCanMediFor";
            this.TxtCanMediFor.ReadOnly = true;
            this.TxtCanMediFor.Size = new System.Drawing.Size(85, 23);
            this.TxtCanMediFor.TabIndex = 22;
            this.TxtCanMediFor.Text = "0";
            this.TxtCanMediFor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(0, 79);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(356, 28);
            this.label13.TabIndex = 20;
            this.label13.Text = "Medicos validados:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(0, 49);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(356, 28);
            this.label12.TabIndex = 19;
            this.label12.Text = "Medicos agregadas al equipo actual:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(0, 20);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(356, 28);
            this.label11.TabIndex = 18;
            this.label11.Text = "Cantidad de Registros de medicos:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtPrefiPorFor
            // 
            this.TxtPrefiPorFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPrefiPorFor.Location = new System.Drawing.Point(580, 39);
            this.TxtPrefiPorFor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtPrefiPorFor.Name = "TxtPrefiPorFor";
            this.TxtPrefiPorFor.ReadOnly = true;
            this.TxtPrefiPorFor.Size = new System.Drawing.Size(77, 23);
            this.TxtPrefiPorFor.TabIndex = 66;
            // 
            // TxtPrefiCenFor
            // 
            this.TxtPrefiCenFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPrefiCenFor.Location = new System.Drawing.Point(580, 12);
            this.TxtPrefiCenFor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtPrefiCenFor.Name = "TxtPrefiCenFor";
            this.TxtPrefiCenFor.ReadOnly = true;
            this.TxtPrefiCenFor.Size = new System.Drawing.Size(77, 23);
            this.TxtPrefiCenFor.TabIndex = 65;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(501, 37);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 28);
            this.label4.TabIndex = 64;
            this.label4.Text = "Prefijo:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(501, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 28);
            this.label3.TabIndex = 63;
            this.label3.Text = "Prefijo:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtInstanPortaFor
            // 
            this.TxtInstanPortaFor.Location = new System.Drawing.Point(185, 41);
            this.TxtInstanPortaFor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtInstanPortaFor.Name = "TxtInstanPortaFor";
            this.TxtInstanPortaFor.ReadOnly = true;
            this.TxtInstanPortaFor.Size = new System.Drawing.Size(312, 22);
            this.TxtInstanPortaFor.TabIndex = 62;
            // 
            // TxtInstanCenFor
            // 
            this.TxtInstanCenFor.Location = new System.Drawing.Point(185, 14);
            this.TxtInstanCenFor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtInstanCenFor.Name = "TxtInstanCenFor";
            this.TxtInstanCenFor.ReadOnly = true;
            this.TxtInstanCenFor.Size = new System.Drawing.Size(312, 22);
            this.TxtInstanCenFor.TabIndex = 61;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(16, 39);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 28);
            this.label2.TabIndex = 60;
            this.label2.Text = "Instancia portatil:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 28);
            this.label1.TabIndex = 59;
            this.label1.Text = "Instancia central:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnBuscarPacientes
            // 
            this.BtnBuscarPacientes.BackgroundImage = global::OBBDSIIG.Properties.Resources.icons8_exportar_30;
            this.BtnBuscarPacientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnBuscarPacientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBuscarPacientes.Location = new System.Drawing.Point(541, 122);
            this.BtnBuscarPacientes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnBuscarPacientes.Name = "BtnBuscarPacientes";
            this.BtnBuscarPacientes.Size = new System.Drawing.Size(57, 42);
            this.BtnBuscarPacientes.TabIndex = 68;
            this.BtnBuscarPacientes.UseVisualStyleBackColor = true;
            this.BtnBuscarPacientes.Click += new System.EventHandler(this.BtnBuscarPacientes_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(16, 196);
            this.ProgressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(643, 28);
            this.ProgressBar.TabIndex = 72;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.LblCodEntiFac);
            this.groupBox6.Controls.Add(this.lblNivelPermitido);
            this.groupBox6.Controls.Add(this.lblNombreUser);
            this.groupBox6.Controls.Add(this.lblCodigoUser);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Location = new System.Drawing.Point(16, 231);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox6.Size = new System.Drawing.Size(639, 42);
            this.groupBox6.TabIndex = 73;
            this.groupBox6.TabStop = false;
            // 
            // LblCodEntiFac
            // 
            this.LblCodEntiFac.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCodEntiFac.Location = new System.Drawing.Point(532, 17);
            this.LblCodEntiFac.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblCodEntiFac.Name = "LblCodEntiFac";
            this.LblCodEntiFac.Size = new System.Drawing.Size(51, 16);
            this.LblCodEntiFac.TabIndex = 31;
            this.LblCodEntiFac.Text = "CodEnti";
            this.LblCodEntiFac.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblCodEntiFac.Visible = false;
            // 
            // lblNivelPermitido
            // 
            this.lblNivelPermitido.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNivelPermitido.Location = new System.Drawing.Point(588, 17);
            this.lblNivelPermitido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNivelPermitido.Name = "lblNivelPermitido";
            this.lblNivelPermitido.Size = new System.Drawing.Size(51, 16);
            this.lblNivelPermitido.TabIndex = 30;
            this.lblNivelPermitido.Text = "NivelPermitido";
            this.lblNivelPermitido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNivelPermitido.Visible = false;
            // 
            // lblNombreUser
            // 
            this.lblNombreUser.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreUser.Location = new System.Drawing.Point(128, 11);
            this.lblNombreUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombreUser.Name = "lblNombreUser";
            this.lblNombreUser.Size = new System.Drawing.Size(511, 27);
            this.lblNombreUser.TabIndex = 11;
            this.lblNombreUser.Text = "NombreUser";
            this.lblNombreUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCodigoUser
            // 
            this.lblCodigoUser.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoUser.Location = new System.Drawing.Point(41, 11);
            this.lblCodigoUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodigoUser.Name = "lblCodigoUser";
            this.lblCodigoUser.Size = new System.Drawing.Size(89, 27);
            this.lblCodigoUser.TabIndex = 9;
            this.lblCodigoUser.Text = "lblcodiuser";
            this.lblCodigoUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(0, 11);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 27);
            this.label16.TabIndex = 8;
            this.label16.Text = "ID:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmIntegrarMedicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 279);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.BtnBuscarPacientes);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TxtPrefiPorFor);
            this.Controls.Add(this.TxtPrefiCenFor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtInstanPortaFor);
            this.Controls.Add(this.TxtInstanCenFor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "FrmIntegrarMedicos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INTEGRAR MEDICOS";
            this.Load += new System.EventHandler(this.FrmIntegrarMedicos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button BtnBuscarPacientes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TxtCanMediExis;
        private System.Windows.Forms.TextBox TxtCanMediForm;
        private System.Windows.Forms.TextBox TxtCanMediFor;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TxtPrefiPorFor;
        private System.Windows.Forms.TextBox TxtPrefiCenFor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtInstanPortaFor;
        private System.Windows.Forms.TextBox TxtInstanCenFor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblNivelPermitido;
        private System.Windows.Forms.Label lblNombreUser;
        private System.Windows.Forms.Label lblCodigoUser;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label LblCodEntiFac;
    }
}