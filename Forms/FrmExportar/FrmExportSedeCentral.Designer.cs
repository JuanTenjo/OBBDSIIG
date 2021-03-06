
namespace OBBDSIIG.Forms.FrmExportar
{
    partial class FrmExportSedeCentral
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExportSedeCentral));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DateFinal = new System.Windows.Forms.DateTimePicker();
            this.TxtFacElec = new System.Windows.Forms.TextBox();
            this.DateInicial = new System.Windows.Forms.DateTimePicker();
            this.DateFecIniFacElec = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtCanConsuForm = new System.Windows.Forms.TextBox();
            this.TxtCanFacForm = new System.Windows.Forms.TextBox();
            this.TxtCanCuenExis = new System.Windows.Forms.TextBox();
            this.TxtCanhisForm = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.TxtCanFacFor = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.LblExportar = new System.Windows.Forms.Label();
            this.BtnBuscarPacientes = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.TxtPrefiPorFor = new System.Windows.Forms.TextBox();
            this.TxtPrefiCenFor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtInstanPortaFor = new System.Windows.Forms.TextBox();
            this.TxtInstanCenFor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LblDiasVenFac = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.LblCodEntiFac = new System.Windows.Forms.Label();
            this.lblNivelPermitido = new System.Windows.Forms.Label();
            this.lblNombreUser = new System.Windows.Forms.Label();
            this.lblCodigoUser = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.BtnSalir = new System.Windows.Forms.Button();
            this.LblDetener = new System.Windows.Forms.Label();
            this.BtnDetener = new System.Windows.Forms.Button();
            this.LblTotal = new System.Windows.Forms.Label();
            this.LblEslash = new System.Windows.Forms.Label();
            this.LblCantidad = new System.Windows.Forms.Label();
            this.ExportarCentral = new System.ComponentModel.BackgroundWorker();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox3.Controls.Add(this.DateFinal);
            this.groupBox3.Controls.Add(this.TxtFacElec);
            this.groupBox3.Controls.Add(this.DateInicial);
            this.groupBox3.Controls.Add(this.DateFecIniFacElec);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(9, 66);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(235, 138);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            // 
            // DateFinal
            // 
            this.DateFinal.CustomFormat = "dd-MMM-yyyy";
            this.DateFinal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateFinal.Location = new System.Drawing.Point(93, 52);
            this.DateFinal.Name = "DateFinal";
            this.DateFinal.Size = new System.Drawing.Size(136, 22);
            this.DateFinal.TabIndex = 28;
            this.DateFinal.Value = new System.DateTime(2020, 9, 30, 8, 32, 0, 0);
            // 
            // TxtFacElec
            // 
            this.TxtFacElec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFacElec.ForeColor = System.Drawing.Color.Blue;
            this.TxtFacElec.Location = new System.Drawing.Point(93, 76);
            this.TxtFacElec.Name = "TxtFacElec";
            this.TxtFacElec.ReadOnly = true;
            this.TxtFacElec.Size = new System.Drawing.Size(136, 20);
            this.TxtFacElec.TabIndex = 26;
            this.TxtFacElec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DateInicial
            // 
            this.DateInicial.CustomFormat = "dd-MMM-yyyy";
            this.DateInicial.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateInicial.Location = new System.Drawing.Point(93, 31);
            this.DateInicial.Name = "DateInicial";
            this.DateInicial.Size = new System.Drawing.Size(136, 22);
            this.DateInicial.TabIndex = 27;
            this.DateInicial.Value = new System.DateTime(2020, 9, 1, 8, 32, 0, 0);
            // 
            // DateFecIniFacElec
            // 
            this.DateFecIniFacElec.CustomFormat = "dd-MMM-yyyy";
            this.DateFecIniFacElec.Enabled = false;
            this.DateFecIniFacElec.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateFecIniFacElec.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateFecIniFacElec.Location = new System.Drawing.Point(93, 98);
            this.DateFecIniFacElec.Name = "DateFecIniFacElec";
            this.DateFecIniFacElec.Size = new System.Drawing.Size(136, 22);
            this.DateFecIniFacElec.TabIndex = 16;
            this.DateFecIniFacElec.Value = new System.DateTime(2020, 9, 30, 8, 32, 0, 0);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(0, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 21);
            this.label9.TabIndex = 11;
            this.label9.Text = "Inicia el:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(0, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 23);
            this.label8.TabIndex = 10;
            this.label8.Text = "Factura Elect:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(0, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 23);
            this.label7.TabIndex = 6;
            this.label7.Text = "Fecha Final:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(0, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 23);
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
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(235, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "Digite rango de fechas";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.TxtCanConsuForm);
            this.groupBox1.Controls.Add(this.TxtCanFacForm);
            this.groupBox1.Controls.Add(this.TxtCanCuenExis);
            this.groupBox1.Controls.Add(this.TxtCanhisForm);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.TxtCanFacFor);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(251, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(339, 143);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // TxtCanConsuForm
            // 
            this.TxtCanConsuForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCanConsuForm.Location = new System.Drawing.Point(268, 104);
            this.TxtCanConsuForm.Name = "TxtCanConsuForm";
            this.TxtCanConsuForm.ReadOnly = true;
            this.TxtCanConsuForm.Size = new System.Drawing.Size(65, 20);
            this.TxtCanConsuForm.TabIndex = 25;
            this.TxtCanConsuForm.Text = "0";
            // 
            // TxtCanFacForm
            // 
            this.TxtCanFacForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCanFacForm.Location = new System.Drawing.Point(268, 80);
            this.TxtCanFacForm.Name = "TxtCanFacForm";
            this.TxtCanFacForm.ReadOnly = true;
            this.TxtCanFacForm.Size = new System.Drawing.Size(65, 20);
            this.TxtCanFacForm.TabIndex = 24;
            this.TxtCanFacForm.Text = "0";
            // 
            // TxtCanCuenExis
            // 
            this.TxtCanCuenExis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCanCuenExis.Location = new System.Drawing.Point(268, 56);
            this.TxtCanCuenExis.Name = "TxtCanCuenExis";
            this.TxtCanCuenExis.ReadOnly = true;
            this.TxtCanCuenExis.Size = new System.Drawing.Size(65, 20);
            this.TxtCanCuenExis.TabIndex = 23;
            this.TxtCanCuenExis.Text = "0";
            // 
            // TxtCanhisForm
            // 
            this.TxtCanhisForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCanhisForm.Location = new System.Drawing.Point(268, 32);
            this.TxtCanhisForm.Name = "TxtCanhisForm";
            this.TxtCanhisForm.ReadOnly = true;
            this.TxtCanhisForm.Size = new System.Drawing.Size(65, 20);
            this.TxtCanhisForm.TabIndex = 22;
            this.TxtCanhisForm.Text = "0";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(0, 102);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(267, 23);
            this.label14.TabIndex = 21;
            this.label14.Text = "Consumos no exportadas porque ya existen:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(0, 78);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(267, 23);
            this.label13.TabIndex = 20;
            this.label13.Text = "Facturas no exportadas porque ya existen:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(0, 54);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(267, 23);
            this.label12.TabIndex = 19;
            this.label12.Text = "Cuentas no exportadas porque ya existen:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(0, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(267, 23);
            this.label11.TabIndex = 18;
            this.label11.Text = "Historias agregadas al servidor central:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtCanFacFor
            // 
            this.TxtCanFacFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCanFacFor.Location = new System.Drawing.Point(268, 8);
            this.TxtCanFacFor.Name = "TxtCanFacFor";
            this.TxtCanFacFor.ReadOnly = true;
            this.TxtCanFacFor.Size = new System.Drawing.Size(65, 20);
            this.TxtCanFacFor.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(0, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(267, 24);
            this.label10.TabIndex = 17;
            this.label10.Text = "Cantidad Facturas";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblExportar
            // 
            this.LblExportar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblExportar.Location = new System.Drawing.Point(66, 246);
            this.LblExportar.Name = "LblExportar";
            this.LblExportar.Size = new System.Drawing.Size(56, 17);
            this.LblExportar.TabIndex = 17;
            this.LblExportar.Text = "Exportar";
            this.LblExportar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnBuscarPacientes
            // 
            this.BtnBuscarPacientes.BackgroundImage = global::OBBDSIIG.Properties.Resources.icons8_exportar_30;
            this.BtnBuscarPacientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnBuscarPacientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBuscarPacientes.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BtnBuscarPacientes.Location = new System.Drawing.Point(73, 263);
            this.BtnBuscarPacientes.Name = "BtnBuscarPacientes";
            this.BtnBuscarPacientes.Size = new System.Drawing.Size(43, 34);
            this.BtnBuscarPacientes.TabIndex = 16;
            this.BtnBuscarPacientes.UseVisualStyleBackColor = true;
            this.BtnBuscarPacientes.Click += new System.EventHandler(this.BtnBuscarPacientes_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(10, 210);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(580, 23);
            this.progressBar.TabIndex = 33;
            // 
            // TxtPrefiPorFor
            // 
            this.TxtPrefiPorFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPrefiPorFor.Location = new System.Drawing.Point(537, 29);
            this.TxtPrefiPorFor.Name = "TxtPrefiPorFor";
            this.TxtPrefiPorFor.ReadOnly = true;
            this.TxtPrefiPorFor.Size = new System.Drawing.Size(53, 20);
            this.TxtPrefiPorFor.TabIndex = 61;
            // 
            // TxtPrefiCenFor
            // 
            this.TxtPrefiCenFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPrefiCenFor.Location = new System.Drawing.Point(537, 9);
            this.TxtPrefiCenFor.Name = "TxtPrefiCenFor";
            this.TxtPrefiCenFor.ReadOnly = true;
            this.TxtPrefiCenFor.Size = new System.Drawing.Size(53, 20);
            this.TxtPrefiCenFor.TabIndex = 60;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(478, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 20);
            this.label4.TabIndex = 59;
            this.label4.Text = "Prefijo:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(478, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 20);
            this.label3.TabIndex = 58;
            this.label3.Text = "Prefijo:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtInstanPortaFor
            // 
            this.TxtInstanPortaFor.Location = new System.Drawing.Point(116, 29);
            this.TxtInstanPortaFor.Name = "TxtInstanPortaFor";
            this.TxtInstanPortaFor.ReadOnly = true;
            this.TxtInstanPortaFor.Size = new System.Drawing.Size(362, 20);
            this.TxtInstanPortaFor.TabIndex = 57;
            // 
            // TxtInstanCenFor
            // 
            this.TxtInstanCenFor.Location = new System.Drawing.Point(116, 9);
            this.TxtInstanCenFor.Name = "TxtInstanCenFor";
            this.TxtInstanCenFor.ReadOnly = true;
            this.TxtInstanCenFor.Size = new System.Drawing.Size(362, 20);
            this.TxtInstanCenFor.TabIndex = 56;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 55;
            this.label2.Text = "Instancia portatil:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 21);
            this.label1.TabIndex = 54;
            this.label1.Text = "Instancia central:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblDiasVenFac
            // 
            this.LblDiasVenFac.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDiasVenFac.Location = new System.Drawing.Point(244, 261);
            this.LblDiasVenFac.Name = "LblDiasVenFac";
            this.LblDiasVenFac.Size = new System.Drawing.Size(83, 18);
            this.LblDiasVenFac.TabIndex = 32;
            this.LblDiasVenFac.Text = "DiasVenFac";
            this.LblDiasVenFac.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblDiasVenFac.UseWaitCursor = true;
            this.LblDiasVenFac.Visible = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.LblCodEntiFac);
            this.groupBox6.Controls.Add(this.lblNivelPermitido);
            this.groupBox6.Controls.Add(this.lblNombreUser);
            this.groupBox6.Controls.Add(this.lblCodigoUser);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Location = new System.Drawing.Point(333, 252);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(257, 46);
            this.groupBox6.TabIndex = 62;
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
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(127, 246);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(43, 17);
            this.label17.TabIndex = 87;
            this.label17.Text = "Salir";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnSalir
            // 
            this.BtnSalir.BackgroundImage = global::OBBDSIIG.Properties.Resources.icons8_cerrar_ventana_35;
            this.BtnSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSalir.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BtnSalir.Location = new System.Drawing.Point(127, 264);
            this.BtnSalir.Name = "BtnSalir";
            this.BtnSalir.Size = new System.Drawing.Size(43, 34);
            this.BtnSalir.TabIndex = 86;
            this.BtnSalir.UseVisualStyleBackColor = true;
            this.BtnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // LblDetener
            // 
            this.LblDetener.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDetener.Location = new System.Drawing.Point(66, 245);
            this.LblDetener.Name = "LblDetener";
            this.LblDetener.Size = new System.Drawing.Size(54, 17);
            this.LblDetener.TabIndex = 85;
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
            this.BtnDetener.Location = new System.Drawing.Point(73, 263);
            this.BtnDetener.Name = "BtnDetener";
            this.BtnDetener.Size = new System.Drawing.Size(43, 34);
            this.BtnDetener.TabIndex = 84;
            this.BtnDetener.UseVisualStyleBackColor = true;
            this.BtnDetener.Visible = false;
            this.BtnDetener.Click += new System.EventHandler(this.BtnDetener_Click);
            // 
            // LblTotal
            // 
            this.LblTotal.BackColor = System.Drawing.Color.Transparent;
            this.LblTotal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotal.Location = new System.Drawing.Point(301, 236);
            this.LblTotal.Name = "LblTotal";
            this.LblTotal.Size = new System.Drawing.Size(64, 13);
            this.LblTotal.TabIndex = 89;
            this.LblTotal.Text = "0";
            this.LblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblEslash
            // 
            this.LblEslash.AutoSize = true;
            this.LblEslash.BackColor = System.Drawing.Color.Transparent;
            this.LblEslash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblEslash.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEslash.Location = new System.Drawing.Point(290, 236);
            this.LblEslash.Name = "LblEslash";
            this.LblEslash.Size = new System.Drawing.Size(13, 13);
            this.LblEslash.TabIndex = 90;
            this.LblEslash.Text = "/";
            // 
            // LblCantidad
            // 
            this.LblCantidad.BackColor = System.Drawing.Color.Transparent;
            this.LblCantidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblCantidad.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCantidad.Location = new System.Drawing.Point(249, 236);
            this.LblCantidad.Name = "LblCantidad";
            this.LblCantidad.Size = new System.Drawing.Size(41, 13);
            this.LblCantidad.TabIndex = 88;
            this.LblCantidad.Text = "0";
            this.LblCantidad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ExportarCentral
            // 
            this.ExportarCentral.WorkerReportsProgress = true;
            this.ExportarCentral.WorkerSupportsCancellation = true;
            this.ExportarCentral.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ExportarCentral_DoWork);
            this.ExportarCentral.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ExportarCentral_ProgressChanged);
            this.ExportarCentral.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ExportarCentral_RunWorkerCompleted);
            // 
            // FrmExportSedeCentral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(596, 309);
            this.ControlBox = false;
            this.Controls.Add(this.LblTotal);
            this.Controls.Add(this.LblEslash);
            this.Controls.Add(this.LblCantidad);
            this.Controls.Add(this.label17);
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
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.LblDiasVenFac);
            this.Controls.Add(this.LblExportar);
            this.Controls.Add(this.BtnBuscarPacientes);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.LblDetener);
            this.Controls.Add(this.BtnDetener);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmExportSedeCentral";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EXPORTAR SEDE CENTRAL";
            this.Load += new System.EventHandler(this.FrmExportSedeCentral_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TxtCanFacFor;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox TxtCanConsuForm;
        private System.Windows.Forms.TextBox TxtCanFacForm;
        private System.Windows.Forms.TextBox TxtCanCuenExis;
        private System.Windows.Forms.TextBox TxtCanhisForm;
        private System.Windows.Forms.Button BtnBuscarPacientes;
        private System.Windows.Forms.Label LblExportar;
        private System.Windows.Forms.DateTimePicker DateFecIniFacElec;
        private System.Windows.Forms.TextBox TxtFacElec;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox TxtPrefiPorFor;
        private System.Windows.Forms.TextBox TxtPrefiCenFor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtInstanPortaFor;
        private System.Windows.Forms.TextBox TxtInstanCenFor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DateFinal;
        private System.Windows.Forms.DateTimePicker DateInicial;
        private System.Windows.Forms.Label LblDiasVenFac;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label LblCodEntiFac;
        private System.Windows.Forms.Label lblNivelPermitido;
        private System.Windows.Forms.Label lblNombreUser;
        private System.Windows.Forms.Label lblCodigoUser;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button BtnSalir;
        private System.Windows.Forms.Label LblDetener;
        private System.Windows.Forms.Button BtnDetener;
        private System.Windows.Forms.Label LblTotal;
        private System.Windows.Forms.Label LblEslash;
        private System.Windows.Forms.Label LblCantidad;
        private System.ComponentModel.BackgroundWorker ExportarCentral;
    }
}