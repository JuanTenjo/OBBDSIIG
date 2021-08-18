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
using System.IO;

namespace OBBDSIIG.Forms.FrmIntegrar
{
    public partial class FrmIntegrarMedicos : Form
    {
        public FrmIntegrarMedicos()
        {
            InitializeComponent();
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

                    TxtInstanCenFor.Text = Utils.InstanCenFor;
                    TxtPrefiCenFor.Text = Utils.PrefiCenFor;
                    TxtInstanPortaFor.Text = Utils.InstanPortaFor;
                    TxtPrefiPorFor.Text = Utils.PrefiPorFor;
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
                                        "User ID=" + Conexion.usernameCen + "; " +
                                        "Password=" + Conexion.passwordCen;

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

        private void FrmIntegrarMedicos_Load(object sender, EventArgs e)
        {
            try
            {
                CargarDatosUser();
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al cargar el formulario FrmIntegrarEntidades" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscarPacientes_Click(object sender, EventArgs e)
        {
            try
            {

                string SqlMedi, SqlEmpTerCentra, CodMedi, SqlMediCentra;
                int ContiPro = 0;

                if (string.IsNullOrWhiteSpace(TxtInstanCenFor.Text) || (TxtInstanCenFor.Text == ""))
                {
                    Utils.Informa = "Lo siento pero mientras no exista";
                    Utils.Informa += "el nombre de la instancia central,";
                    Utils.Informa += "no se puede empezar a ejecutar el";
                    Utils.Informa += "proceso de exportación de datos.";
                    MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(TxtPrefiCenFor.Text) || (TxtPrefiCenFor.Text == ""))
                {
                    Utils.Informa = "Lo siento pero mientras no exista";
                    Utils.Informa += "el prefijo de la instancia central,";
                    Utils.Informa += "no se puede empezar a ejecutar el";
                    Utils.Informa += "proceso de exportación de datos.";
                    MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(TxtInstanPortaFor.Text) || (TxtInstanPortaFor.Text == ""))
                {
                    Utils.Informa = "Lo siento pero mientras no exista";
                    Utils.Informa += "nombre de la instancia del porttatil,";
                    Utils.Informa += "no se puede empezar a ejecutar el";
                    Utils.Informa += "proceso de exportación de datos.";
                    MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(TxtPrefiPorFor.Text) || (TxtPrefiPorFor.Text == ""))
                {
                    Utils.Informa = "Lo siento pero mientras no exista";
                    Utils.Informa += "prefijo de la instancia del porttatil,";
                    Utils.Informa += "no se puede empezar a ejecutar el";
                    Utils.Informa += "proceso de exportación de datos.";
                    MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string PfiCen = TxtPrefiCenFor.Text;
                string PfiPor = TxtPrefiPorFor.Text;

                Utils.Titulo01 = "Control para integrar medicos";
                Utils.Informa = "¿Usted desea iniciar el proceso de integración" + "\r";
                Utils.Informa += "de los medicos en la instancia del" + "\r";
                Utils.Informa += "servidor central a la instancia del portatil.?" + "\r";
                var res = MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    TxtCanMediFor.Text = "0";
                    TxtCanMediForm.Text = "0";
                    TxtCanMediExis.Text = "0";



                    ConectarCentral();

                    string SqlMediCount = "SELECT count(*) as TotalRegis ";
                    SqlMediCount += "FROM [GEOGRAXPSQL].[dbo].[Datos de los medicos]";

                    int Total = 0;

                    SqlDataReader reader = Conexion.SQLDataReader(SqlMediCount);

                    if (reader.HasRows)
                    {
                        reader.Read();

                        Total = Convert.ToInt32(reader["TotalRegis"]);

                        if (Total != 0)
                        {
                            ProgressBar.Minimum = 1;
                            ProgressBar.Maximum = Total;
                        }


                    }
                    else
                    {
                        ProgressBar.Minimum = 0;
                        ProgressBar.Maximum = 1;
                        ProgressBar.Value = 0;
                    }

                    if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();



                    SqlMedi = "SELECT CodiMedico, CodEspecial, TipoDocum, NumDocum, NomMedico, Apellido1Medico, Apellido2Medico, FecNaciMedi, SexoMedico, ";
                    SqlMedi += "CargoMedico, DirecMedico, TeleResiden, TeleConsul, TeleCelular, RegisProfes, LicenOcupa, TrabajaPor, ActiMedico, ";
                    SqlMedi += "HaceConsul, FirmaD, FotoMedico, CodiRegis, FecRegis, CodiModi, FecModi, Nom2Medico ";
                    SqlMedi += "FROM [GEOGRAXPSQL].[dbo].[Datos de los medicos]";

                    SqlDataReader TabMedi;

                    using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                    {
                        SqlCommand command = new SqlCommand(SqlMedi, connection);
                        command.Connection.Open();
                        TabMedi = command.ExecuteReader();

                        if (TabMedi.HasRows == false)
                        {
                            Utils.Informa = "Lo siento pero no hay datos " + "\r";
                            Utils.Informa += "para exportar o modificar en , " + "\r";
                            Utils.Informa += "Datos de los medicos." + "\r";
                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            byte[] bytes;
                            byte[] bytes2;
                            List<SqlParameter> parameters = new List<SqlParameter>();
                            string Tipo = "null";
                            string Tipo2 = "null";
                            SqlDataReader TabMediCentra;
                            while (TabMedi.Read())
                            {
                                Tipo = "null";
                                Tipo2 = "null";
                                parameters.Clear();
                                //Revisamos si el código interno de la entidad existe
                                CodMedi = TabMedi["CodiMedico"].ToString();
                                ContiPro = 0;



                                SqlMediCentra = "SELECT [Datos de los medicos].* ";
                                SqlMediCentra += "FROM [GEOGRAXPSQL].[dbo].[Datos de los medicos] ";
                                SqlMediCentra += "WHERE (CodiMedico = '" + CodMedi + "')";


                                ConectarPortatil();

                                
                                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                                {
                                    SqlCommand command2 = new SqlCommand(SqlMediCentra, connection2);
                                    command2.Connection.Open();
                                    TabMediCentra = command2.ExecuteReader();

                                    if (TabMediCentra.HasRows == false)
                                    {

                                        if (string.IsNullOrWhiteSpace(TabMedi["FirmaD"].ToString()) || TabMedi["FirmaD"].ToString() == null)
                                        {
                                            bytes = (byte[])(null);
                                            Tipo = "null";
                                        }
                                        else
                                        {
                                            bytes = (byte[])(TabMedi["FirmaD"]);
                                            Tipo = "@FirmaD";

                                            parameters.Add(new SqlParameter("@FirmaD", SqlDbType.Image) { Value = bytes });

                                        }

                                        if (string.IsNullOrWhiteSpace(TabMedi["FotoMedico"].ToString()) || TabMedi["FotoMedico"].ToString() == null)
                                        {
                                            bytes2 = (byte[])(null);
                                            Tipo2 = "null";
                                        }
                                        else
                                        {
                                            bytes2 = (byte[])(TabMedi["FotoMedico"]);
                                            Tipo2 = "@FotoMedico";

                                            parameters.Add(new SqlParameter("@FotoMedico", SqlDbType.Image) { Value = bytes2 });

                                        }

                                        Utils.SqlDatos = "INSERT INTO [GEOGRAXPSQL].[dbo].[Datos de los medicos]" +
                                        "(" +
                                        "CodiMedico," + 
                                        "CodEspecial," + 
                                        "TipoDocum," + 
                                        "NumDocum," + 
                                        "NomMedico," + 
                                        "Apellido1Medico," + 
                                        "Apellido2Medico," + 
                                        "FecNaciMedi," + 
                                        "SexoMedico," + 
                                        "CargoMedico," + 
                                        "DirecMedico," + 
                                        "TeleResiden," + 
                                        "TeleConsul," + 
                                        "TeleCelular," + 
                                        "RegisProfes," + 
                                        "LicenOcupa," + 
                                        "TrabajaPor," + 
                                        "ActiMedico," + 
                                        "HaceConsul," + 
                                        "FirmaD," + 
                                        "FotoMedico," + 
                                        "CodiRegis," +
                                        "FecRegis," +
                                        "CodiModi," + 
                                        "FecModi," + 
                                        "Nom2Medico " +
                                        ")" +
                                        "VALUES" +
                                        "(" +
                                         "'" + TabMedi["CodiMedico"].ToString() + "'," +
                                         "'" + TabMedi["CodEspecial"].ToString() + "'," +
                                         "'" + TabMedi["TipoDocum"].ToString() + "'," +
                                         "'" + TabMedi["NumDocum"].ToString() + "'," +
                                         "'" + TabMedi["NomMedico"].ToString() + "'," +
                                         "'" + TabMedi["Apellido1Medico"].ToString() + "'," +
                                         "'" + TabMedi["Apellido2Medico"].ToString() + "'," +
                                         $"{Conexion.ValidarFechaNula(TabMedi["FecNaciMedi"].ToString())}" +
                                         "'" + TabMedi["SexoMedico"].ToString() + "'," +
                                         "'" + TabMedi["CargoMedico"].ToString() + "'," +
                                         "'" + TabMedi["DirecMedico"].ToString() + "'," +
                                         "'" + TabMedi["TeleResiden"].ToString() + "'," +
                                         "'" + TabMedi["TeleConsul"].ToString() + "'," +
                                         "'" + TabMedi["TeleCelular"].ToString() + "'," +
                                         "'" + TabMedi["RegisProfes"].ToString() + "'," +
                                         "'" + TabMedi["LicenOcupa"].ToString() + "'," +
                                         "'" + TabMedi["TrabajaPor"].ToString() + "'," +
                                         "'" + TabMedi["ActiMedico"].ToString() + "'," +
                                         "'" + TabMedi["HaceConsul"].ToString() + "'," +
                                         "" + Tipo + "," +
                                         "" + Tipo2 + "," +
                                         "'" + TabMedi["CodiRegis"].ToString() + "'," +
                                        $"{Conexion.ValidarFechaNula(TabMedi["FecRegis"].ToString())}" +
                                         "'" + TabMedi["CodiModi"].ToString() + "'," +
                                        $"{Conexion.ValidarFechaNula(TabMedi["FecModi"].ToString())}" +
                                         "'" + TabMedi["Nom2Medico"].ToString() + "' " +
                                        ")";


                                        Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos,parameters);

                                        if (Regis)
                                        {
                                            int con = Convert.ToInt32(TxtCanMediForm.Text) + 1;
                                            TxtCanMediForm.Text = con.ToString();
                                        }

                                        ContiPro = 1;


                                    }
                                    else
                                    {


                                        if (string.IsNullOrWhiteSpace(TabMedi["FirmaD"].ToString()) || TabMedi["FirmaD"].ToString() == null)
                                        {
                                            bytes = (byte[])(null);
                                            Tipo = "null";
                                        }
                                        else
                                        {

                                            bytes = (byte[])(TabMedi["FirmaD"]);
                                            Tipo = "@FirmaD";

                                            parameters.Add(new SqlParameter("@FirmaD", SqlDbType.Image) { Value = bytes });


                                        }

                                        if (string.IsNullOrWhiteSpace(TabMedi["FotoMedico"].ToString()) || TabMedi["FotoMedico"].ToString() == null)
                                        {
                                            bytes2 = (byte[])(null);
                                            Tipo2 = "null";
                                        }
                                        else
                                        {

                                            bytes2 = (byte[])(TabMedi["FotoMedico"]);
                                            Tipo2 = "@FotoMedico";

                                            parameters.Add(new SqlParameter("@FotoMedico", SqlDbType.Image) { Value = bytes2 });

                                        }

                                        //Modifique los datos
                                        Utils.SqlDatos = "UPDATE [GEOGRAXPSQL].[dbo].[Datos de los medicos] SET " +
                                        "CodEspecial ='" + TabMedi["CodEspecial"].ToString() + "', " +
                                        "TipoDocum ='" + TabMedi["TipoDocum"].ToString() + "', " +
                                        "NumDocum ='" + TabMedi["NumDocum"].ToString() + "'," +
                                        "NomMedico ='" + TabMedi["NomMedico"].ToString() + "'," +
                                        "Apellido1Medico ='" + TabMedi["Apellido1Medico"].ToString() + "'," +
                                        "Apellido2Medico ='" + TabMedi["Apellido2Medico"].ToString() + "'," +
                                        $"FecNaciMedi = {Conexion.ValidarFechaNula(TabMedi["FecNaciMedi"].ToString())} " +
                                        "SexoMedico ='" + TabMedi["SexoMedico"].ToString() + "'," +
                                        "CargoMedico ='" + TabMedi["CargoMedico"].ToString() + "'," +
                                        "DirecMedico ='" + TabMedi["DirecMedico"].ToString() + "'," +
                                        "TeleResiden ='" + TabMedi["TeleResiden"].ToString() + "'," +
                                        "TeleConsul ='" + TabMedi["TeleConsul"].ToString() + "'," +
                                        "TeleCelular ='" + TabMedi["TeleCelular"].ToString() + "'," +
                                        "RegisProfes ='" + TabMedi["RegisProfes"].ToString() + "'," +
                                        "LicenOcupa ='" + TabMedi["LicenOcupa"].ToString() + "'," +
                                        "TrabajaPor ='" + TabMedi["TrabajaPor"].ToString() + "'," +
                                        "ActiMedico ='" + TabMedi["ActiMedico"].ToString() + "'," +
                                        "HaceConsul ='" + TabMedi["HaceConsul"].ToString() + "'," +
                                        "FirmaD = " + Tipo + ", " +
                                        "FotoMedico =" + Tipo2 + "," +
                                        "CodiRegis ='" + TabMedi["CodiRegis"].ToString() + "'," +
                                        $"FecRegis = {Conexion.ValidarFechaNula(TabMedi["FecRegis"].ToString())} " +
                                        "CodiModi ='" + TabMedi["CodiModi"].ToString() + "'," +
                                        $"FecModi = {Conexion.ValidarFechaNula(TabMedi["FecModi"].ToString())} " +
                                        "Nom2Medico = '" + TabMedi["Nom2Medico"].ToString() + "' " +
                                        "WHERE (CodiMedico = '" + CodMedi + "')";

                                        bool ActControl = Conexion.SQLUpdate(Utils.SqlDatos,parameters);

                                        if (ActControl)
                                        {
                                            int con = Convert.ToInt32(TxtCanMediExis.Text) + 1;
                                            TxtCanMediExis.Text = con.ToString();

                                        }

                                        ContiPro = 1;

                                    }//'Final deif (TabPlacaCen.HasRows == false)

                                   

                                }//USing
                                TabMediCentra.Close();

                                ProgressBar.Increment(1);
                            }//While

                            Utils.Informa = "El proceso ha terminado satisfactoriamente";
                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ProgressBar.Minimum = 0;
                            ProgressBar.Maximum = 1;
                            ProgressBar.Value = 0;
                        }

                        TabMedi.Close();

                    }

                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "despues de dar click en integrar" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                ProgressBar.Minimum = 0;
                ProgressBar.Maximum = 1;
                ProgressBar.Value = 0;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn);
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
            catch (Exception ex)
            {
                Utils.Informa = "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }



    }
}
