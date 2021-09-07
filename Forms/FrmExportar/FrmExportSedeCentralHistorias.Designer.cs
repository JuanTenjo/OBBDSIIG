
namespace OBBDSIIG.Forms.FrmExportar
{
    partial class FrmExportSedeCentralHistorias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExportSedeCentralHistorias));
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.LblDiasVenFac = new System.Windows.Forms.Label();
            this.LblCodEntiFac = new System.Windows.Forms.Label();
            this.lblNivelPermitido = new System.Windows.Forms.Label();
            this.lblNombreUser = new System.Windows.Forms.Label();
            this.lblCodigoUser = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.BtnBuscarPacientes = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtCanNotAnex = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtCanhisFormExis = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.TxtCanHistFor = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DateFinal = new System.Windows.Forms.DateTimePicker();
            this.DateInicial = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtPrefiPorFor = new System.Windows.Forms.TextBox();
            this.TxtPrefiCenFor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtInstanPortaFor = new System.Windows.Forms.TextBox();
            this.TxtInstanCenFor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnRegistrarData = new System.Windows.Forms.Button();
            this.BarraExportHistorias = new System.Windows.Forms.ProgressBar();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.LblDiasVenFac);
            this.groupBox6.Controls.Add(this.LblCodEntiFac);
            this.groupBox6.Controls.Add(this.lblNivelPermitido);
            this.groupBox6.Controls.Add(this.lblNombreUser);
            this.groupBox6.Controls.Add(this.lblCodigoUser);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Location = new System.Drawing.Point(247, 334);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox6.Size = new System.Drawing.Size(364, 66);
            this.groupBox6.TabIndex = 58;
            this.groupBox6.TabStop = false;
            // 
            // LblDiasVenFac
            // 
            this.LblDiasVenFac.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDiasVenFac.Location = new System.Drawing.Point(91, 11);
            this.LblDiasVenFac.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblDiasVenFac.Name = "LblDiasVenFac";
            this.LblDiasVenFac.Size = new System.Drawing.Size(111, 22);
            this.LblDiasVenFac.TabIndex = 46;
            this.LblDiasVenFac.Text = "DiasVenFac";
            this.LblDiasVenFac.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblDiasVenFac.UseWaitCursor = true;
            this.LblDiasVenFac.Visible = false;
            // 
            // LblCodEntiFac
            // 
            this.LblCodEntiFac.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCodEntiFac.Location = new System.Drawing.Point(273, 11);
            this.LblCodEntiFac.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblCodEntiFac.Name = "LblCodEntiFac";
            this.LblCodEntiFac.Size = new System.Drawing.Size(51, 16);
            this.LblCodEntiFac.TabIndex = 31;
            this.LblCodEntiFac.Text = "CodEntiFac";
            this.LblCodEntiFac.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblCodEntiFac.Visible = false;
            // 
            // lblNivelPermitido
            // 
            this.lblNivelPermitido.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNivelPermitido.Location = new System.Drawing.Point(272, 44);
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
            this.lblNombreUser.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreUser.Location = new System.Drawing.Point(4, 34);
            this.lblNombreUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombreUser.Name = "lblNombreUser";
            this.lblNombreUser.Size = new System.Drawing.Size(291, 22);
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
            this.lblCodigoUser.Size = new System.Drawing.Size(101, 16);
            this.lblCodigoUser.TabIndex = 9;
            this.lblCodigoUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(0, 11);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 16);
            this.label16.TabIndex = 8;
            this.label16.Text = "ID:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(16, 330);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 15);
            this.label15.TabIndex = 57;
            this.label15.Text = "Exportar";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnBuscarPacientes
            // 
            this.BtnBuscarPacientes.BackgroundImage = global::OBBDSIIG.Properties.Resources.icons8_exportar_30;
            this.BtnBuscarPacientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnBuscarPacientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBuscarPacientes.Location = new System.Drawing.Point(32, 352);
            this.BtnBuscarPacientes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnBuscarPacientes.Name = "BtnBuscarPacientes";
            this.BtnBuscarPacientes.Size = new System.Drawing.Size(57, 42);
            this.BtnBuscarPacientes.TabIndex = 56;
            this.BtnBuscarPacientes.UseVisualStyleBackColor = true;
            this.BtnBuscarPacientes.Click += new System.EventHandler(this.BtnBuscarPacientes_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.TxtCanNotAnex);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.TxtCanhisFormExis);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.TxtCanHistFor);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(20, 171);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(605, 114);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            // 
            // TxtCanNotAnex
            // 
            this.TxtCanNotAnex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCanNotAnex.Location = new System.Drawing.Point(475, 79);
            this.TxtCanNotAnex.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtCanNotAnex.Name = "TxtCanNotAnex";
            this.TxtCanNotAnex.ReadOnly = true;
            this.TxtCanNotAnex.Size = new System.Drawing.Size(123, 23);
            this.TxtCanNotAnex.TabIndex = 24;
            this.TxtCanNotAnex.Text = "0";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(0, 78);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(467, 28);
            this.label8.TabIndex = 23;
            this.label8.Text = "Cantidad de notas anexas:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtCanhisFormExis
            // 
            this.TxtCanhisFormExis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCanhisFormExis.Location = new System.Drawing.Point(475, 42);
            this.TxtCanhisFormExis.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtCanhisFormExis.Name = "TxtCanhisFormExis";
            this.TxtCanhisFormExis.ReadOnly = true;
            this.TxtCanhisFormExis.Size = new System.Drawing.Size(123, 23);
            this.TxtCanhisFormExis.TabIndex = 22;
            this.TxtCanhisFormExis.Text = "0";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(0, 41);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(467, 28);
            this.label11.TabIndex = 18;
            this.label11.Text = "Atencion de consultas  no exportados porque ya existen:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtCanHistFor
            // 
            this.TxtCanHistFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCanHistFor.Location = new System.Drawing.Point(475, 9);
            this.TxtCanHistFor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtCanHistFor.Name = "TxtCanHistFor";
            this.TxtCanHistFor.ReadOnly = true;
            this.TxtCanHistFor.Size = new System.Drawing.Size(123, 23);
            this.TxtCanHistFor.TabIndex = 17;
            this.TxtCanHistFor.Text = "0";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(0, 6);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(467, 28);
            this.label10.TabIndex = 17;
            this.label10.Text = "Cantidad de atenciones de consultas:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox3.Controls.Add(this.DateFinal);
            this.groupBox3.Controls.Add(this.DateInicial);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(20, 85);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(605, 79);
            this.groupBox3.TabIndex = 55;
            this.groupBox3.TabStop = false;
            // 
            // DateFinal
            // 
            this.DateFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateFinal.Location = new System.Drawing.Point(457, 38);
            this.DateFinal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DateFinal.Name = "DateFinal";
            this.DateFinal.Size = new System.Drawing.Size(133, 22);
            this.DateFinal.TabIndex = 9;
            this.DateFinal.Value = new System.DateTime(2020, 9, 30, 8, 32, 0, 0);
            // 
            // DateInicial
            // 
            this.DateInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateInicial.Location = new System.Drawing.Point(167, 38);
            this.DateInicial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DateInicial.Name = "DateInicial";
            this.DateInicial.Size = new System.Drawing.Size(131, 22);
            this.DateInicial.TabIndex = 8;
            this.DateInicial.Value = new System.DateTime(2020, 9, 1, 8, 32, 0, 0);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(305, 36);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 28);
            this.label7.TabIndex = 6;
            this.label7.Text = "Fecha Final:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(17, 36);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(141, 28);
            this.label6.TabIndex = 5;
            this.label6.Text = "Fecha Inicial:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(605, 28);
            this.label5.TabIndex = 4;
            this.label5.Text = "Digite rango de fechas";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtPrefiPorFor
            // 
            this.TxtPrefiPorFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPrefiPorFor.Location = new System.Drawing.Point(573, 41);
            this.TxtPrefiPorFor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtPrefiPorFor.Name = "TxtPrefiPorFor";
            this.TxtPrefiPorFor.ReadOnly = true;
            this.TxtPrefiPorFor.Size = new System.Drawing.Size(51, 23);
            this.TxtPrefiPorFor.TabIndex = 53;
            // 
            // TxtPrefiCenFor
            // 
            this.TxtPrefiCenFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPrefiCenFor.Location = new System.Drawing.Point(573, 14);
            this.TxtPrefiCenFor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtPrefiCenFor.Name = "TxtPrefiCenFor";
            this.TxtPrefiCenFor.ReadOnly = true;
            this.TxtPrefiCenFor.Size = new System.Drawing.Size(51, 23);
            this.TxtPrefiCenFor.TabIndex = 52;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(495, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 28);
            this.label4.TabIndex = 51;
            this.label4.Text = "Prefijo:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(495, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 28);
            this.label3.TabIndex = 50;
            this.label3.Text = "Prefijo:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtInstanPortaFor
            // 
            this.TxtInstanPortaFor.Location = new System.Drawing.Point(163, 41);
            this.TxtInstanPortaFor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtInstanPortaFor.Name = "TxtInstanPortaFor";
            this.TxtInstanPortaFor.ReadOnly = true;
            this.TxtInstanPortaFor.Size = new System.Drawing.Size(327, 22);
            this.TxtInstanPortaFor.TabIndex = 49;
            // 
            // TxtInstanCenFor
            // 
            this.TxtInstanCenFor.Location = new System.Drawing.Point(163, 14);
            this.TxtInstanCenFor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TxtInstanCenFor.Name = "TxtInstanCenFor";
            this.TxtInstanCenFor.ReadOnly = true;
            this.TxtInstanCenFor.Size = new System.Drawing.Size(327, 22);
            this.TxtInstanCenFor.TabIndex = 48;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(16, 39);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 28);
            this.label2.TabIndex = 47;
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
            this.label1.Size = new System.Drawing.Size(145, 28);
            this.label1.TabIndex = 46;
            this.label1.Text = "Instancia central:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnRegistrarData
            // 
            this.BtnRegistrarData.Location = new System.Drawing.Point(127, 338);
            this.BtnRegistrarData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnRegistrarData.Name = "BtnRegistrarData";
            this.BtnRegistrarData.Size = new System.Drawing.Size(100, 57);
            this.BtnRegistrarData.TabIndex = 59;
            this.BtnRegistrarData.Text = "REGISTRA TRATA";
            this.BtnRegistrarData.UseVisualStyleBackColor = true;
            this.BtnRegistrarData.Visible = false;
            this.BtnRegistrarData.Click += new System.EventHandler(this.BtnRegistrarData_Click);
            // 
            // BarraExportHistorias
            // 
            this.BarraExportHistorias.Location = new System.Drawing.Point(20, 293);
            this.BarraExportHistorias.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BarraExportHistorias.Name = "BarraExportHistorias";
            this.BarraExportHistorias.Size = new System.Drawing.Size(605, 28);
            this.BarraExportHistorias.TabIndex = 60;
            // 
            // FrmExportSedeCentralHistorias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 406);
            this.Controls.Add(this.BarraExportHistorias);
            this.Controls.Add(this.BtnRegistrarData);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.BtnBuscarPacientes);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
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
            this.Name = "FrmExportSedeCentralHistorias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EXPORTAR SEDE CENTRAL HISTORIAS";
            this.Load += new System.EventHandler(this.FrmExportSedeCentralHistorias_Load);
            this.groupBox6.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label LblDiasVenFac;
        private System.Windows.Forms.Label LblCodEntiFac;
        private System.Windows.Forms.Label lblNivelPermitido;
        private System.Windows.Forms.Label lblNombreUser;
        private System.Windows.Forms.Label lblCodigoUser;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button BtnBuscarPacientes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TxtCanNotAnex;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtCanhisFormExis;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TxtCanHistFor;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker DateFinal;
        private System.Windows.Forms.DateTimePicker DateInicial;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtPrefiPorFor;
        private System.Windows.Forms.TextBox TxtPrefiCenFor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtInstanPortaFor;
        private System.Windows.Forms.TextBox TxtInstanCenFor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnRegistrarData;
        private System.Windows.Forms.ProgressBar BarraExportHistorias;
    }
}