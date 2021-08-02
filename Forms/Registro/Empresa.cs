using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OBBDSIIG.Class;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Drawing.Imaging;

namespace OBBDSIIG.Forms.Registro
{
    public partial class Empresa : Form
    {

        public Empresa()
        {
            InitializeComponent();
        }


        #region Texbox
        #endregion

        #region Botones
        #endregion

        #region Funciones

        private void CargaDatosEmpresa()
        {
            try
            {
                Conexion.conexionACCESS = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\SIIGHOSPLUS\LogPlus.LogSip;Jet OLEDB:Database Password=SIIGHOS33";

                Utils.SqlDatos = "SELECT * FROM [Local registro del usuario]";

                OleDbDataReader dr = Conexion.AccessDataReaderOLEDB(Utils.SqlDatos);

                if (dr.HasRows)
                {
                    dr.Read();

                    CboTipDocEm.SelectedValue = dr["TipoDocumEmp"].ToString();
                    TxtNumDocEm.Text = dr["NitCCEmpresa"].ToString();
                    TxtDVDian.Text = dr["DigVerEm"].ToString();

                    TxtNumMinSal.Text = dr["CodiMinSalud"].ToString();
                    TxtNomPrinEm.Text = dr["NombreEmpresa"].ToString();
                    TxtNomSecEm.Text = dr["NombreSecundario"].ToString();
                    TxtCateEmp.Text = dr["CategoriaEmpresa"].ToString();

                    //Dim mybytearray() As Byte = dr["LogoEmpresa"];
                    //MemoryStream ms = new MemoryStream();
                    //byte[] aByte = ms.ToArray();

                    //byte mybytearray = Convert.ToByte(dr["LogoEmpresa"]);

                    //MemoryStream ms = new MemoryStream(Convert.ToInt32(dr["LogoEmpresa"]));

                    //BpLogo.Image = System.Drawing.Image.FromStream(ms);

                    //byte[] datos = Convert.ToBase64String((byte[])dr["LogoEmpresa"].ToString());

                    //Image i = Image.FromFile("osito.jpg");

                    //MessageBox.Show(dr["LogoEmpresa"].ToString());

                    //Image i = Image.FromStream((Stream)dr["LogoEmpresa"]);

                    //MemoryStream m = new MemoryStream();

                    //i.Save(m, ImageFormat.Bmp);
                    ////byte[] imagenDatos = m.ToArray();

                    //byte[] asaa = Convert.ToBase64String(dr["LogoEmpresa"].T);
                    //MemoryStream ms = new MemoryStream(asaa);
                    //BpLogo.Image = Image.FromStream(ms);


                    CboCodPais.SelectedValue = dr["PaisDomi"].ToString();

         
                    CboDptoIPS.SelectedValue = dr["DptoDomi"].ToString();

                   
                    CboCodCiuIps.SelectedValue = dr["MuniDomi"].ToString();

                    TxtBarIPS.Text = dr["BarIPS"].ToString();
                    TxtDirecIPS.Text = dr["DirecIPS"].ToString();
                    TxtTeleIPS1.Text = dr["TelPrin"].ToString();
                    TxtTeleIPS2.Text = dr["TelePie"].ToString();

                    TxtNomSerCentral.Text = dr["InstanCentral"].ToString();
                    TxtNomUsuaSC.Text = dr["NomUsaCen"].ToString();
                    TxtContraseSC.Text = dr["PasswordCen"].ToString();
                    TxtPrefiCentral.Text = dr["PreCentral"].ToString();
                    TxtPrefiPortatil.Text = dr["PrePortatil"].ToString();
                    TxtNomRepLegal.Text = dr["NomReLe"].ToString(); //
                    TxtCorrElectro.Text = dr["Email1"].ToString();

                    TxtEsloganIPS.Text = dr["Eslogan"].ToString();

                    lblCodigoUser.Text = dr["CodigUsar"].ToString(); //
                    lblNombreUser.Text = dr["NombreUsar"].ToString();
                    lblNivelPermitido.Text = dr["NivelPermiso"].ToString();

                }
                else
                {
                    lblCodigoUser.Text = "000";
                    lblNombreUser.Text = "SOFTWARE PIRATA";
                    lblNivelPermitido.Text = "0";
                }

                dr.Close();

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "En la funcion CargaDatosEmpresa " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




            private void ConectarPortatil()
        {
            try
            {

                Conexion.conexionSQL = "Server=" + Conexion.servidor + "; " +
                                        "Initial Catalog=" + Utils.BaseDeDatosPrincipal + ";" +
                                        "User ID= " + Conexion.username + "; " +
                                        "Password=" + Conexion.password;

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "configurar la instancia para Portatil " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConectarCentral()
        {
            try
            {

                Conexion.conexionSQL = "Server=" + Conexion.servidorCen + "; " +
                                        "Initial Catalog=" + Utils.BaseDeDatosPrincipal + ";" +
                                        "User ID= " + Conexion.username + "; " +
                                        "Password=" + Conexion.password;

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "configurar la instancia para Portatil " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarComboBox()
        {
            try
            {

                ConectarPortatil();

                this.CboTipDocEm.DataSource = null;
                this.CboTipDocEm.Items.Clear();

                Utils.SqlDatos = "SELECT [Datos documentos empresas].CodIdenti, [Datos documentos empresas].NomIdenti " +
                "FROM [GEOGRAXPSQL].[dbo].[Datos documentos empresas] " +
                "ORDER BY [Datos documentos empresas].CodIdenti;";

                DataSet dataSet = Conexion.SQLDataSet(Utils.SqlDatos);

                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    this.CboTipDocEm.DataSource = dataSet.Tables[0];
                    this.CboTipDocEm.ValueMember = "CodIdenti";
                    this.CboTipDocEm.DisplayMember = "NomIdenti";
                }


                this.CboCodPais.DataSource = null;
                this.CboCodPais.Items.Clear();

                string SqlPais = "SELECT [Datos de paises].CodiPais, [Datos de paises].NomPais, [Datos de paises].HabilPais ";
                SqlPais += "FROM [GEOGRAXPSQL].[dbo].[Datos de paises] ";
                SqlPais += "WHERE [Datos de paises].HabilPais = 'True' ";
                SqlPais += "ORDER BY [Datos de paises].NomPais;";

                DataSet dataSetPaises = Conexion.SQLDataSet(SqlPais);

                if (dataSetPaises != null && dataSetPaises.Tables.Count > 0)
                {
                    this.CboCodPais.DataSource = dataSetPaises.Tables[0];
                    this.CboCodPais.ValueMember = "CodiPais";
                    this.CboCodPais.DisplayMember = "NomPais";
                }



            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al cargar los combobox" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatosUser()
        {
            try
            {

                if (Utils.codUsuario == "0000" || Utils.codUsuario == "001")
                {

                    Utils.Titulo01 = "Control de errores de ejecución";
                    Utils.Informa = "Lo siento pero este formulario no se puede" + "\r";
                    Utils.Informa += "abrir en este instante, porque el código del" + "\r";
                    Utils.Informa += "usuario no es válido para realizar operaciones" + "\r";
                    Utils.Informa += "con las historias clinicas de los usuarios." + "\r";
                    MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    lblCodigoUser.Text = Utils.codUsuario;
                    lblNombreUser.Text = Utils.nomUsuario;
                    LblCodEntiFac.Text = Utils.codUnicoEmpresa; // '*********************** Se agrega a partir del 13 de octubre de 2020 *********************************

                }

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al cargar la informacion del usario" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } //Carga datos de los usuairos

        #endregion

        #region ComboBox

        private void CboDptoIPS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CboDptoIPS.DisplayMember) || CboDptoIPS.SelectedIndex == -1)
            {
                this.CboCodCiuIps.DataSource = null;
                return;
            }
            else
            {
                this.CboCodCiuIps.DataSource = null;
                this.CboCodCiuIps.Items.Clear();


                string SqlCiudades = "SELECT [Datos ciudades del dpto].CodDptoCity, [Datos ciudades del dpto].NombreCiudad, [Datos ciudades del dpto].CodCiudad, [Datos ciudades del dpto].HabilCiud ";
                SqlCiudades += "FROM [GEOGRAXPSQL].[dbo].[Datos ciudades del dpto] ";
                SqlCiudades += "WHERE ((([Datos ciudades del dpto].CodigoDpto)= '" + CboDptoIPS.SelectedValue + "' )) ";
                SqlCiudades += "ORDER BY [Datos ciudades del dpto].NombreCiudad;";


                DataSet dataSetCuidades = Conexion.SQLDataSet(SqlCiudades);

                if (dataSetCuidades.Tables[0].Rows.Count > 0)
                {
                    this.CboCodCiuIps.DataSource = dataSetCuidades.Tables[0];
                    this.CboCodCiuIps.ValueMember = "CodCiudad";
                    this.CboCodCiuIps.DisplayMember = "NombreCiudad";
                }
                else
                {
                    this.CboCodCiuIps.DataSource = null;
                    this.CboCodCiuIps.Text = "No tiene Ciudades registradas";
                }

            }
        }

        private void CboCodPais_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (string.IsNullOrWhiteSpace(CboCodPais.DisplayMember) || CboCodPais.SelectedIndex == -1)
            {
                return;
            }
            else
            {
                this.CboDptoIPS.DataSource = null;
                this.CboDptoIPS.Items.Clear();


                string SqlDpto = "SELECT [Datos de los Dpto o Estados].CodigoDpto, [Datos de los Dpto o Estados].NombreDpto, [Datos de los Dpto o Estados].CodigoPais ";
                SqlDpto += "FROM [GEOGRAXPSQL].[dbo].[Datos de los Dpto o Estados] ";
                SqlDpto += "WHERE ((([Datos de los Dpto o Estados].CodigoPais)= '" + CboCodPais.SelectedValue + "' )) ";
                SqlDpto += "ORDER BY [Datos de los Dpto o Estados].NombreDpto;";


                DataSet dataSetDpto = Conexion.SQLDataSet(SqlDpto);

                if (dataSetDpto.Tables[0].Rows.Count > 0)
                {
                    this.CboDptoIPS.DataSource = dataSetDpto.Tables[0];
                    this.CboDptoIPS.ValueMember = "CodigoDpto";
                    this.CboDptoIPS.DisplayMember = "NombreDpto";
                }
                else
                {
                    this.CboDptoIPS.Text = "No tiene DPTO registrados";
                }

            }

        }
        #endregion


        private void Empresa_Load(object sender, EventArgs e)
        {
            try
            {
                CargarDatosUser();
                CargarComboBox();

                CargaDatosEmpresa();

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al cargar el formulario  FrmIntegrarServicios " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSubirLogo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OdfSeleccionar = new OpenFileDialog();

                OdfSeleccionar.Filter = "Imagenes|*.jpg; *.png";
                OdfSeleccionar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                OdfSeleccionar.Title = "Seleccionar Imagen";

                if(OdfSeleccionar.ShowDialog() == DialogResult.OK)
                {
                    BpLogo.Image = Image.FromFile(OdfSeleccionar.FileName);
                }


            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al subir el logo " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRegisEmpre_Click(object sender, EventArgs e)
        {

        }


        //    private void button1_Click(object sender, EventArgs e)
        //    {
        //        MemoryStream ms = new MemoryStream();
        //        BpLogo.Image.Save(ms, ImageFormat.Jpeg);
        //        byte[] aByte = ms.ToArray();




        //    }
    }
}
