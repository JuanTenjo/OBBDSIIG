
namespace OBBDSIIG.Forms.FrmIntegrar
{
    partial class FrmIExportarBiometria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIExportarBiometria));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtCanHueMod = new System.Windows.Forms.TextBox();
            this.TxtCanHueAgr = new System.Windows.Forms.TextBox();
            this.TxtCanFotMod = new System.Windows.Forms.TextBox();
            this.TxtCanFotAgr = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtCanFirMod = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.TxtCanFirAgr = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.LblExportar = new System.Windows.Forms.Label();
            this.BtnBuscarPacientes = new System.Windows.Forms.Button();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.TxtPrefiPorFor = new System.Windows.Forms.TextBox();
            this.TxtPrefiCenFor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtInstanPortaFor = new System.Windows.Forms.TextBox();
            this.TxtInstanCenFor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.LblCodEntiFac = new System.Windows.Forms.Label();
            this.lblNivelPermitido = new System.Windows.Forms.Label();
            this.lblNombreUser = new System.Windows.Forms.Label();
            this.lblCodigoUser = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.BtnSalir = new System.Windows.Forms.Button();
            this.LblDetener = new System.Windows.Forms.Label();
            this.BtnDetener = new System.Windows.Forms.Button();
            this.LblTotal = new System.Windows.Forms.Label();
            this.LblEslash = new System.Windows.Forms.Label();
            this.LblCantidad = new System.Windows.Forms.Label();
            this.ExportarBiometria = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.TxtCanHueMod);
            this.groupBox1.Controls.Add(this.TxtCanHueAgr);
            this.groupBox1.Controls.Add(this.TxtCanFotMod);
            this.groupBox1.Controls.Add(this.TxtCanFotAgr);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.TxtCanFirMod);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.TxtCanFirAgr);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(4, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(468, 182);
            this.groupBox1.TabIndex = 76;
            this.groupBox1.TabStop = false;
            // 
            // TxtCanHueMod
            // 
            this.TxtCanHueMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCanHueMod.Location = new System.Drawing.Point(418, 150);
            this.TxtCanHueMod.Name = "TxtCanHueMod";
            this.TxtCanHueMod.ReadOnly = true;
            this.TxtCanHueMod.Size = new System.Drawing.Size(47, 20);
            this.TxtCanHueMod.TabIndex = 30;
            this.TxtCanHueMod.Text = "0";
            // 
            // TxtCanHueAgr
            // 
            this.TxtCanHueAgr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCanHueAgr.Location = new System.Drawing.Point(418, 121);
            this.TxtCanHueAgr.Name = "TxtCanHueAgr";
            this.TxtCanHueAgr.ReadOnly = true;
            this.TxtCanHueAgr.Size = new System.Drawing.Size(47, 20);
            this.TxtCanHueAgr.TabIndex = 29;
            this.TxtCanHueAgr.Text = "0";
            // 
            // TxtCanFotMod
            // 
            this.TxtCanFotMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCanFotMod.Location = new System.Drawing.Point(418, 92);
            this.TxtCanFotMod.Name = "TxtCanFotMod";
            this.TxtCanFotMod.ReadOnly = true;
            this.TxtCanFotMod.Size = new System.Drawing.Size(47, 20);
            this.TxtCanFotMod.TabIndex = 28;
            this.TxtCanFotMod.Text = "0";
            // 
            // TxtCanFotAgr
            // 
            this.TxtCanFotAgr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCanFotAgr.Location = new System.Drawing.Point(418, 64);
            this.TxtCanFotAgr.Name = "TxtCanFotAgr";
            this.TxtCanFotAgr.ReadOnly = true;
            this.TxtCanFotAgr.Size = new System.Drawing.Size(47, 20);
            this.TxtCanFotAgr.TabIndex = 27;
            this.TxtCanFotAgr.Text = "0";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(0, 147);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(415, 23);
            this.label8.TabIndex = 26;
            this.label8.Text = "Cantidad de Registros de Huella modificadas:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(0, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(415, 23);
            this.label7.TabIndex = 25;
            this.label7.Text = "Cantidad de Registros de Huella agregadas:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(0, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(415, 23);
            this.label6.TabIndex = 24;
            this.label6.Text = "Cantidad de Registros de foto modificadas:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(415, 23);
            this.label5.TabIndex = 23;
            this.label5.Text = "Cantidad de Registros de foto agregadas:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtCanFirMod
            // 
            this.TxtCanFirMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCanFirMod.Location = new System.Drawing.Point(418, 35);
            this.TxtCanFirMod.Name = "TxtCanFirMod";
            this.TxtCanFirMod.ReadOnly = true;
            this.TxtCanFirMod.Size = new System.Drawing.Size(47, 20);
            this.TxtCanFirMod.TabIndex = 22;
            this.TxtCanFirMod.Text = "0";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(0, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(415, 23);
            this.label11.TabIndex = 18;
            this.label11.Text = "Cantidad de Registros de firmas modificadas:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtCanFirAgr
            // 
            this.TxtCanFirAgr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCanFirAgr.Location = new System.Drawing.Point(418, 6);
            this.TxtCanFirAgr.Name = "TxtCanFirAgr";
            this.TxtCanFirAgr.ReadOnly = true;
            this.TxtCanFirAgr.Size = new System.Drawing.Size(47, 20);
            this.TxtCanFirAgr.TabIndex = 17;
            this.TxtCanFirAgr.Text = "0";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(0, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(415, 23);
            this.label10.TabIndex = 17;
            this.label10.Text = "Cantidad de Registros de firmas agregadas:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblExportar
            // 
            this.LblExportar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblExportar.Location = new System.Drawing.Point(54, 280);
            this.LblExportar.Name = "LblExportar";
            this.LblExportar.Size = new System.Drawing.Size(57, 20);
            this.LblExportar.TabIndex = 73;
            this.LblExportar.Text = "Exportar";
            this.LblExportar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnBuscarPacientes
            // 
            this.BtnBuscarPacientes.BackgroundImage = global::OBBDSIIG.Properties.Resources.icons8_exportar_30;
            this.BtnBuscarPacientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnBuscarPacientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBuscarPacientes.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BtnBuscarPacientes.Location = new System.Drawing.Point(58, 301);
            this.BtnBuscarPacientes.Name = "BtnBuscarPacientes";
            this.BtnBuscarPacientes.Size = new System.Drawing.Size(43, 34);
            this.BtnBuscarPacientes.TabIndex = 72;
            this.BtnBuscarPacientes.UseVisualStyleBackColor = true;
            this.BtnBuscarPacientes.Click += new System.EventHandler(this.BtnBuscarPacientes_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(4, 243);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(465, 23);
            this.ProgressBar.TabIndex = 78;
            // 
            // TxtPrefiPorFor
            // 
            this.TxtPrefiPorFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPrefiPorFor.Location = new System.Drawing.Point(422, 29);
            this.TxtPrefiPorFor.Name = "TxtPrefiPorFor";
            this.TxtPrefiPorFor.ReadOnly = true;
            this.TxtPrefiPorFor.Size = new System.Drawing.Size(45, 20);
            this.TxtPrefiPorFor.TabIndex = 86;
            // 
            // TxtPrefiCenFor
            // 
            this.TxtPrefiCenFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPrefiCenFor.Location = new System.Drawing.Point(422, 9);
            this.TxtPrefiCenFor.Name = "TxtPrefiCenFor";
            this.TxtPrefiCenFor.ReadOnly = true;
            this.TxtPrefiCenFor.Size = new System.Drawing.Size(45, 20);
            this.TxtPrefiCenFor.TabIndex = 85;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(363, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 20);
            this.label4.TabIndex = 84;
            this.label4.Text = "Prefijo:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(363, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 20);
            this.label3.TabIndex = 83;
            this.label3.Text = "Prefijo:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtInstanPortaFor
            // 
            this.TxtInstanPortaFor.Location = new System.Drawing.Point(113, 29);
            this.TxtInstanPortaFor.Name = "TxtInstanPortaFor";
            this.TxtInstanPortaFor.ReadOnly = true;
            this.TxtInstanPortaFor.Size = new System.Drawing.Size(252, 20);
            this.TxtInstanPortaFor.TabIndex = 82;
            // 
            // TxtInstanCenFor
            // 
            this.TxtInstanCenFor.Location = new System.Drawing.Point(113, 9);
            this.TxtInstanCenFor.Name = "TxtInstanCenFor";
            this.TxtInstanCenFor.ReadOnly = true;
            this.TxtInstanCenFor.Size = new System.Drawing.Size(252, 20);
            this.TxtInstanCenFor.TabIndex = 81;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(4, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 80;
            this.label2.Text = "Instancia portatil:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 21);
            this.label1.TabIndex = 79;
            this.label1.Text = "Instancia central:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.LblCodEntiFac);
            this.groupBox6.Controls.Add(this.lblNivelPermitido);
            this.groupBox6.Controls.Add(this.lblNombreUser);
            this.groupBox6.Controls.Add(this.lblCodigoUser);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Location = new System.Drawing.Point(210, 289);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(259, 46);
            this.groupBox6.TabIndex = 88;
            this.groupBox6.TabStop = false;
            // 
            // LblCodEntiFac
            // 
            this.LblCodEntiFac.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCodEntiFac.Location = new System.Drawing.Point(222, 9);
            this.LblCodEntiFac.Name = "LblCodEntiFac";
            this.LblCodEntiFac.Size = new System.Drawing.Size(34, 13);
            this.LblCodEntiFac.TabIndex = 31;
            this.LblCodEntiFac.Text = "CodEntiFac";
            this.LblCodEntiFac.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblCodEntiFac.Visible = false;
            // 
            // lblNivelPermitido
            // 
            this.lblNivelPermitido.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNivelPermitido.Location = new System.Drawing.Point(228, 28);
            this.lblNivelPermitido.Name = "lblNivelPermitido";
            this.lblNivelPermitido.Size = new System.Drawing.Size(25, 13);
            this.lblNivelPermitido.TabIndex = 30;
            this.lblNivelPermitido.Text = "NivelPermitido";
            this.lblNivelPermitido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNivelPermitido.Visible = false;
            // 
            // lblNombreUser
            // 
            this.lblNombreUser.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreUser.Location = new System.Drawing.Point(3, 24);
            this.lblNombreUser.Name = "lblNombreUser";
            this.lblNombreUser.Size = new System.Drawing.Size(251, 18);
            this.lblNombreUser.TabIndex = 11;
            this.lblNombreUser.Text = "NombreUser";
            this.lblNombreUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCodigoUser
            // 
            this.lblCodigoUser.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoUser.Location = new System.Drawing.Point(31, 9);
            this.lblCodigoUser.Name = "lblCodigoUser";
            this.lblCodigoUser.Size = new System.Drawing.Size(76, 13);
            this.lblCodigoUser.TabIndex = 9;
            this.lblCodigoUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(1, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 13);
            this.label16.TabIndex = 8;
            this.label16.Text = "ID:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(113, 283);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 17);
            this.label12.TabIndex = 92;
            this.label12.Text = "Salir";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnSalir
            // 
            this.BtnSalir.BackgroundImage = global::OBBDSIIG.Properties.Resources.icons8_cerrar_ventana_35;
            this.BtnSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSalir.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BtnSalir.Location = new System.Drawing.Point(113, 301);
            this.BtnSalir.Name = "BtnSalir";
            this.BtnSalir.Size = new System.Drawing.Size(43, 34);
            this.BtnSalir.TabIndex = 91;
            this.BtnSalir.UseVisualStyleBackColor = true;
            this.BtnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // LblDetener
            // 
            this.LblDetener.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDetener.Location = new System.Drawing.Point(54, 280);
            this.LblDetener.Name = "LblDetener";
            this.LblDetener.Size = new System.Drawing.Size(54, 20);
            this.LblDetener.TabIndex = 90;
            this.LblDetener.Text = "Detener";
            this.LblDetener.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblDetener.Visible = false;
            // 
            // BtnDetener
            // 
            this.BtnDetener.BackgroundImage = global::OBBDSIIG.Properties.Resources.detener;
            this.BtnDetener.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnDetener.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDetener.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BtnDetener.Location = new System.Drawing.Point(58, 301);
            this.BtnDetener.Name = "BtnDetener";
            this.BtnDetener.Size = new System.Drawing.Size(43, 34);
            this.BtnDetener.TabIndex = 89;
            this.BtnDetener.UseVisualStyleBackColor = true;
            this.BtnDetener.Visible = false;
            this.BtnDetener.Click += new System.EventHandler(this.BtnDetener_Click);
            // 
            // LblTotal
            // 
            this.LblTotal.BackColor = System.Drawing.Color.Transparent;
            this.LblTotal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotal.Location = new System.Drawing.Point(243, 269);
            this.LblTotal.Name = "LblTotal";
            this.LblTotal.Size = new System.Drawing.Size(35, 13);
            this.LblTotal.TabIndex = 94;
            this.LblTotal.Text = "0";
            this.LblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblEslash
            // 
            this.LblEslash.AutoSize = true;
            this.LblEslash.BackColor = System.Drawing.Color.Transparent;
            this.LblEslash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblEslash.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEslash.Location = new System.Drawing.Point(232, 269);
            this.LblEslash.Name = "LblEslash";
            this.LblEslash.Size = new System.Drawing.Size(13, 13);
            this.LblEslash.TabIndex = 95;
            this.LblEslash.Text = "/";
            // 
            // LblCantidad
            // 
            this.LblCantidad.BackColor = System.Drawing.Color.Transparent;
            this.LblCantidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblCantidad.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCantidad.Location = new System.Drawing.Point(191, 269);
            this.LblCantidad.Name = "LblCantidad";
            this.LblCantidad.Size = new System.Drawing.Size(41, 13);
            this.LblCantidad.TabIndex = 93;
            this.LblCantidad.Text = "0";
            this.LblCantidad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ExportarBiometria
            // 
            this.ExportarBiometria.WorkerReportsProgress = true;
            this.ExportarBiometria.WorkerSupportsCancellation = true;
            this.ExportarBiometria.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ExportarBiometria_DoWork);
            this.ExportarBiometria.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ExportarBiometria_ProgressChanged);
            this.ExportarBiometria.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ExportarBiometria_RunWorkerCompleted);
            // 
            // FrmIExportarBiometria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(476, 346);
            this.ControlBox = false;
            this.Controls.Add(this.LblTotal);
            this.Controls.Add(this.LblEslash);
            this.Controls.Add(this.LblCantidad);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.BtnSalir);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.TxtPrefiPorFor);
            this.Controls.Add(this.TxtPrefiCenFor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtInstanPortaFor);
            this.Controls.Add(this.TxtInstanCenFor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.LblExportar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnBuscarPacientes);
            this.Controls.Add(this.LblDetener);
            this.Controls.Add(this.BtnDetener);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmIExportarBiometria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EXPORTAR BIOMETRIA";
            this.Load += new System.EventHandler(this.FrmIntegrarBiometria_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TxtCanHueMod;
        private System.Windows.Forms.TextBox TxtCanHueAgr;
        private System.Windows.Forms.TextBox TxtCanFotMod;
        private System.Windows.Forms.TextBox TxtCanFotAgr;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtCanFirMod;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TxtCanFirAgr;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label LblExportar;
        private System.Windows.Forms.Button BtnBuscarPacientes;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.TextBox TxtPrefiPorFor;
        private System.Windows.Forms.TextBox TxtPrefiCenFor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtInstanPortaFor;
        private System.Windows.Forms.TextBox TxtInstanCenFor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label LblCodEntiFac;
        private System.Windows.Forms.Label lblNivelPermitido;
        private System.Windows.Forms.Label lblNombreUser;
        private System.Windows.Forms.Label lblCodigoUser;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button BtnSalir;
        private System.Windows.Forms.Label LblDetener;
        private System.Windows.Forms.Button BtnDetener;
        private System.Windows.Forms.Label LblTotal;
        private System.Windows.Forms.Label LblEslash;
        private System.Windows.Forms.Label LblCantidad;
        private System.ComponentModel.BackgroundWorker ExportarBiometria;
    }
}