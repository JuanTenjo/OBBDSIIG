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


namespace OBBDSIIG.Forms.FrmIntegrar
{
    public partial class FrmIExportarBiometria : Form
    {
        public FrmIExportarBiometria()
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


        private void FrmIntegrarBiometria_Load(object sender, EventArgs e)
        {
            try
            {
                CargarDatosUser();
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al cargar el formulario  FrmIntegrarBiometria " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscarPacientes_Click(object sender, EventArgs e)
        {
            try
            {

                if (ExportarBiometria.IsBusy != true) //Si el proceso esta corriendo no puede voler a iniciarse 
                {

                    SqlDataReader reader;

                    int MaximoBarra = 0;
                    int Total = 0;
                    Utils.Titulo01 = "Control para exportar datos";

                    if (string.IsNullOrWhiteSpace(TxtInstanCenFor.Text) || (TxtInstanCenFor.Text == ""))
                    {
                        Utils.Informa = "Lo siento pero mientras no exista";
                        Utils.Informa += "el nombre de la instancia central,";
                        Utils.Informa += "no se puede empezar a ejecutar el";
                        Utils.Informa += "proceso de exportación de datos.";
                        MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    if (string.IsNullOrWhiteSpace(TxtInstanPortaFor.Text) || (TxtInstanPortaFor.Text == ""))
                    {
                        Utils.Informa = "Lo siento pero mientras no exista";
                        Utils.Informa += "nombre de la instancia del porttatil,";
                        Utils.Informa += "no se puede empezar a ejecutar el";
                        Utils.Informa += "proceso de exportación de datos.";
                        MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    Utils.Informa = "¿Usted desea iniciar el proceso de exportacion de los" + "\r";
                    Utils.Informa += "datos Biometricos del portatil al servidor central?" + "\r";
                    var res = MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                    if (res == DialogResult.Yes)
                    {

                        TxtCanFirAgr.Text = "0";
                        TxtCanFirMod.Text = "0";
                        TxtCanFotAgr.Text = "0";
                        TxtCanFotMod.Text = "0";
                        TxtCanHueAgr.Text = "0";
                        TxtCanHueMod.Text = "0";
                        globalCanFirAgr = 0;
                        globalCanFirmMod = 0;
                        globalCanFotAgr = 0;
                        globalCanFotMod = 0;
                        globalCanHueAgr = 0;
                        globalCanHueMod = 0;

                        Total = 0;

                        ConectarPortatil();

                        string SqlBiometFiCount = "SELECT count(*) as TotalRegis ";
                        SqlBiometFiCount = SqlBiometFiCount + "FROM [BDBIOMETSQL].[dbo].[Datos firma digital] ";
                   
                        reader = Conexion.SQLDataReader(SqlBiometFiCount);
                        if (reader.HasRows)
                        {
                            reader.Read();

                            Total = Convert.ToInt32(reader["TotalRegis"]);

                            if (Total != 0)
                            {
                                MaximoBarra += Total;
                            }


                        }
      

                        if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();


                        TxtCanFotAgr.Text = "0";
                        TxtCanFotMod.Text = "0";

                        string SqlBiometFoCount = "SELECT count(*) as TotalRegis  ";
                        SqlBiometFoCount = SqlBiometFoCount + "FROM [BDBIOMETSQL].[dbo].[Datos foto digital] ";

                        Total = 0;

                        reader = Conexion.SQLDataReader(SqlBiometFoCount);

                        if (reader.HasRows)
                        {
                            reader.Read();

                            Total = Convert.ToInt32(reader["TotalRegis"]);

                            if (Total != 0)
                            {
                                MaximoBarra += Total;
                            }


                        }
        
                        if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();


                        TxtCanHueAgr.Text = "0";
                        TxtCanHueMod.Text = "0";


                        string SqlBiometHuCount = "SELECT count(*) as TotalRegis  ";
                        SqlBiometHuCount = SqlBiometHuCount + "FROM [BDBIOMETSQL].[dbo].[Datos huella digital] ";

                        Total = 0;


                        reader = Conexion.SQLDataReader(SqlBiometHuCount);

                        if (reader.HasRows)
                        {
                            reader.Read();

                            Total = Convert.ToInt32(reader["TotalRegis"]);

                            if (Total != 0)
                            {
                                MaximoBarra += Total;
                            }

                        }
       

                        if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();


                        if(MaximoBarra >= 1)
                        {

                            LblDetener.Visible = true;
                            BtnDetener.Visible = true;

                            LblExportar.Visible = false;
                            BtnBuscarPacientes.Visible = false;


                            ProgressBar.Minimum = 1;
                            ProgressBar.Maximum = MaximoBarra;

                            LblTotal.Text = MaximoBarra.ToString();
                            ExportarBiometria.RunWorkerAsync();

                        }
                        else
                        {
                            Utils.Titulo01 = "Control de ejecución";
                            Utils.Informa = "Lo siento pero no se han encontrado registros" + "\r";
                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            ProgressBar.Minimum = 0;
                            ProgressBar.Maximum = 1;
                            ProgressBar.Value = 0;
                            LblTotal.Text = "0";
                        }


                    }

                }
              
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "después de hacer click sobre el botón integrar" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                ProgressBar.Minimum = 0;
                ProgressBar.Maximum = 1;
                ProgressBar.Value = 0;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            if (ExportarBiometria.IsBusy == true) //Si el proceso esta corriendo no puede voler a iniciarse 
            {
                Utils.Titulo01 = "Control de ejecución";
                Utils.Informa = "Se esta corriendo un proceso, detengalo para poder salir" + "\r";
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.Dispose();
            }

        }



        int globalCanFirAgr = 0;
        int globalCanFirmMod = 0;
        int globalCanFotAgr = 0;
        int globalCanFotMod = 0;
        int globalCanHueAgr = 0;
        int globalCanHueMod = 0;

        private void ExportarBiometria_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                int contador = 0;

                string SqlBiometFi = "", CodHisFir, CodBusPlaca, SqlBiometFirCen, UsaRegis;

                byte[] bytes;
                List<SqlParameter> parameters = new List<SqlParameter>();
                string Tipo = "null";

                DateTime Fecha2 = DateTime.Now;
                string Fecha = Fecha2.ToString("yyyy-MM-dd");
                UsaRegis = lblCodigoUser.Text;

               

                ConectarPortatil();

                SqlBiometFi = "SELECT [Datos firma digital].* ";
                SqlBiometFi = SqlBiometFi + "FROM [BDBIOMETSQL].[dbo].[Datos firma digital] ";
                SqlBiometFi = SqlBiometFi + "ORDER BY HistorPaci";

                SqlDataReader TabBiometFi;

                using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command = new SqlCommand(SqlBiometFi, connection);
                    command.Connection.Open();
                    TabBiometFi = command.ExecuteReader();

                    if (TabBiometFi.HasRows == false)
                    {
                        //No hay datos para exportar o modificar
                    }
                    else
                    {


                        while (TabBiometFi.Read())
                        {

                            if (ExportarBiometria.CancellationPending == true)
                            {
                                e.Cancel = true;
                                Utils.Titulo01 = "Control de ejecución";
                                Utils.Informa = "Se cancelo la operacion " + "\r";
                                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            contador += 1;

                            Tipo = "null";
                            parameters.Clear();


                            //Revisamos si el numero de historia del paciente existe en el SERVIDOR central
                            CodHisFir = null;

                            CodHisFir = TabBiometFi["HistorPaci"].ToString();

                            SqlBiometFirCen = "SELECT [Datos firma digital].* ";
                            SqlBiometFirCen = SqlBiometFirCen + "FROM [BDBIOMETSQL].[dbo].[Datos firma digital] ";
                            SqlBiometFirCen = SqlBiometFirCen + "WHERE (HistorPaci = N'" + CodHisFir + "')";


                            ConectarCentral();

                            SqlDataReader TabBiometFiCentra;

                            using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command2 = new SqlCommand(SqlBiometFirCen, connection2);
                                command2.Connection.Open();
                                TabBiometFiCentra = command2.ExecuteReader();

                                if (TabBiometFiCentra.HasRows == false)
                                {

                                    if (string.IsNullOrWhiteSpace(TabBiometFi["Firma"].ToString()) || TabBiometFi["Firma"].ToString() == null)
                                    {
                                        bytes = (byte[])(null);
                                        Tipo = "null";
                                    }
                                    else
                                    {
                                        bytes = (byte[])(TabBiometFi["Firma"]);
                                        Tipo = "@Firma";

                                        parameters = new List<SqlParameter>
                                            {
                                                new SqlParameter("@Firma", SqlDbType.VarBinary){ Value = bytes}
                                            };

                                    }

                                    Utils.SqlDatos = "INSERT INTO [BDBIOMETSQL].[dbo].[Datos firma digital]" +
                                    "(" +
                                    "HistorPaci," +
                                    "Firma," +
                                    "CodiRegis," +
                                    "FecRegis," +
                                    "FecModi," +
                                    "CodModi" +
                                    ")" +
                                    "VALUES" +
                                    "(" +
                                    "'" + TabBiometFi["HistorPaci"].ToString() + "'," +
                                    "" + Tipo + "," +
                                    "'" + TabBiometFi["CodiRegis"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabBiometFi["FecRegis"].ToString())}" +
                                     $"{Conexion.ValidarFechaNula(TabBiometFi["FecModi"].ToString())}" +
                                    "'" + TabBiometFi["CodModi"].ToString() + "'" +
                                    ")";


                                    Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos, parameters);

                                    globalCanFirAgr += 1;


                                }
                                else
                                {
                                    TabBiometFiCentra.Read();

                                    if (Convert.ToDateTime(TabBiometFiCentra["FecRegis"]) > Convert.ToDateTime(TabBiometFi["FecRegis"]))
                                    {
                                        //'No modifique ningun dato
                                    }
                                    else
                                    {
                                        //Modifique los datos

                                        if (string.IsNullOrWhiteSpace(TabBiometFi["Firma"].ToString()) || TabBiometFi["Firma"].ToString() == null)
                                        {
                                            bytes = (byte[])(null);
                                            Tipo = "null";
                                        }
                                        else
                                        {
                                            bytes = (byte[])(TabBiometFi["Firma"]);
                                            Tipo = "@Firma";

                                            parameters = new List<SqlParameter>
                                                {
                                                new SqlParameter("@Firma", SqlDbType.VarBinary){ Value = bytes}
                                                };

                                        }


                                        Utils.SqlDatos = $"UPDATE [BDBIOMETSQL].[dbo].[Datos firma digital] SET " +
                                        "Firma = " + Tipo + ", " +
                                        $"FecModi = {Conexion.ValidarFechaNula(TabBiometFi["FecModi"].ToString())} " +
                                        "CodModi = '" + TabBiometFi["CodModi"].ToString() + "' " +
                                        "WHERE (HistorPaci = N'" + CodHisFir + "') ";

                                        //parameters.Add()

                                        Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos, parameters);

   
                                        globalCanFirmMod += 1;

                                    }


                                }//'Final deif (TabPlacaCen.HasRows == false)

                            }//USing

                            ExportarBiometria.ReportProgress(contador);

                        }//While        
                        
                    }//Using


                }//   if (TabBiometFi.HasRows == false)

                TabBiometFi.Close();


                ConectarPortatil();

                //Consultamos los datos de las firmas en el equipo BRIGADA
                string SqlBiometFotCen, CodHisFot;


                string SqlBiometFo = "SELECT [Datos foto digital].* ";
                SqlBiometFo = SqlBiometFo + "FROM [BDBIOMETSQL].[dbo].[Datos foto digital] ";
                SqlBiometFo = SqlBiometFo + "ORDER BY HistorPaci";

                SqlDataReader TabBiometFo;
                SqlDataReader TabBiometFoCentra;

                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command2 = new SqlCommand(SqlBiometFo, connection2);
                    command2.Connection.Open();
                    TabBiometFo = command2.ExecuteReader();

                    if (TabBiometFo.HasRows == false)
                    {
                        //No hay datos para exportar o modificar
                    }
                    else
                    {

                        ConectarCentral();

                        while (TabBiometFo.Read())
                        {

                            if (ExportarBiometria.CancellationPending == true)
                            {
                                e.Cancel = true;
                                Utils.Titulo01 = "Control de ejecución";
                                Utils.Informa = "Se cancelo la operacion " + "\r";
                                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }


                            Tipo = "null";
                            parameters.Clear();

                            contador += 1;

                            //Revisamos si el numero de historia del paciente existe en el SERVIDOR central
                            CodHisFot = TabBiometFo["HistorPaci"].ToString();


                            SqlBiometFotCen = "SELECT [Datos foto digital].* ";
                            SqlBiometFotCen = SqlBiometFotCen + "FROM [BDBIOMETSQL].[dbo].[Datos foto digital] ";
                            SqlBiometFotCen = SqlBiometFotCen + "WHERE (HistorPaci = N'" + CodHisFot + "')";



                            using (SqlConnection connection3 = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command3 = new SqlCommand(SqlBiometFotCen, connection3);
                                command3.Connection.Open();
                                TabBiometFoCentra = command3.ExecuteReader();

                                if (TabBiometFoCentra.HasRows == false)
                                {

                                    if (string.IsNullOrWhiteSpace(TabBiometFo["Foto"].ToString()) || TabBiometFo["Foto"].ToString() == null)
                                    {
                                        bytes = (byte[])(null);
                                        Tipo = "null";
                                    }
                                    else
                                    {
                                        bytes = (byte[])(TabBiometFo["Foto"]);
                                        Tipo = "@Foto";

                                        parameters = new List<SqlParameter>
                                            {
                                                new SqlParameter("@Foto", SqlDbType.VarBinary){ Value = bytes}
                                            };
                                    }

                                    Utils.SqlDatos = "INSERT INTO  [BDBIOMETSQL].[dbo].[Datos foto digital]" +
                                    "(" +
                                    "HistorPaci," +
                                    "Foto," +
                                    "CodiRegis," +
                                    "FecRegis," +
                                    "FecModi," +
                                    "CodModi" +
                                    ")" +
                                    "VALUES" +
                                    "(" +
                                    "'" + TabBiometFo["HistorPaci"].ToString() + "'," +
                                    "" + Tipo + "," +
                                    "'" + TabBiometFo["CodiRegis"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabBiometFo["FecRegis"].ToString())}" +
                                        $"{Conexion.ValidarFechaNula(TabBiometFo["FecModi"].ToString())}" +
                                    "'" + TabBiometFo["CodModi"].ToString() + "'" +
                                    ")";

                                    Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos, parameters);

                                    globalCanFotAgr += 1;


                                }
                                else
                                {
                                    TabBiometFoCentra.Read();

                                    if (Convert.ToDateTime(TabBiometFoCentra["FecRegis"]) > Convert.ToDateTime(TabBiometFo["FecRegis"]))
                                    {
                                        //'No modifique ningun dato
                                    }
                                    else
                                    {

                                        if (string.IsNullOrWhiteSpace(TabBiometFo["Foto"].ToString()) || TabBiometFo["Foto"].ToString() == null)
                                        {
                                            bytes = (byte[])(null);
                                            Tipo = "null";
                                        }
                                        else
                                        {
                                            bytes = (byte[])(TabBiometFo["Foto"]);
                                            Tipo = "@Foto";

                                            parameters = new List<SqlParameter>
                                                {
                                                    new SqlParameter("@Foto", SqlDbType.VarBinary){ Value = bytes}
                                                };

                                        }

                                        //Modifique los datos
                                        Utils.SqlDatos = $"UPDATE [BDBIOMETSQL].[dbo].[Datos foto digital] SET  " +
                                        "Foto = " + Tipo + ", " +
                                        $"FecModi = {Conexion.ValidarFechaNula(TabBiometFo["FecModi"].ToString())} " +
                                        "CodModi = '" + TabBiometFo["CodModi"].ToString() + "' " +
                                        "WHERE (HistorPaci = N'" + CodHisFot + "') ";

                                        Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos, parameters);

                                        globalCanFotMod += 1;

                                    }
                                }
                            }

                            TabBiometFoCentra.Close();

                            ExportarBiometria.ReportProgress(contador);

                        }//Whiile
                    }
                }

                TabBiometFo.Close();


                //Consultamos los datos de las firmas en el equipo BRIGADA

                ConectarPortatil();

                string SqlBiometHu = "SELECT [Datos huella digital].* ";
                SqlBiometHu = SqlBiometHu + "FROM [BDBIOMETSQL].[dbo].[Datos huella digital] ";
                SqlBiometHu = SqlBiometHu + "ORDER BY HistorPaci";

                string CodHisHue, SqlBiometHueCen;


                SqlDataReader TabBiometHu, TabBiometHuCentra;

                using (SqlConnection connection4 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command4 = new SqlCommand(SqlBiometHu, connection4);
                    command4.Connection.Open();
                    TabBiometHu = command4.ExecuteReader();


                    if (TabBiometHu.HasRows == false)
                    {
                        //No hay datos para exportar o modificar
                    }
                    else
                    {

                        ConectarCentral();

                        string Tipo2 = "null";
                        byte[] bytes2;
                        while (TabBiometHu.Read())
                        {

                            if (ExportarBiometria.CancellationPending == true)
                            {
                                e.Cancel = true;
                                Utils.Titulo01 = "Control de ejecución";
                                Utils.Informa = "Se cancelo la operacion " + "\r";
                                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }


                            contador += 1;

                            Tipo = "null";
                            parameters.Clear();
                            Tipo2 = "null";
                            //Revisamos si el numero de historia del paciente existe en el SERVIDOR central
                            CodHisHue = TabBiometHu["HistorPaci"].ToString();


                            SqlBiometHueCen = "SELECT [Datos huella digital].* ";
                            SqlBiometHueCen = SqlBiometHueCen + "FROM  [BDBIOMETSQL].[dbo].[Datos huella digital] ";
                            SqlBiometHueCen = SqlBiometHueCen + "WHERE (HistorPaci = N'" + CodHisHue + "')";



                            using (SqlConnection connection5 = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command5 = new SqlCommand(SqlBiometHueCen, connection5);
                                command5.Connection.Open();
                                TabBiometHuCentra = command5.ExecuteReader();

                                if (TabBiometHuCentra.HasRows == false)
                                {



                                    if (string.IsNullOrWhiteSpace(TabBiometHu["huella_tpt"].ToString()) || TabBiometHu["huella_tpt"].ToString() == null)
                                    {
                                        bytes = (byte[])(null);
                                        Tipo = "null";
                                    }
                                    else
                                    {
                                        bytes = (byte[])(TabBiometHu["huella_tpt"]);
                                        Tipo = "@huella_tpt";

                                        parameters.Add(new SqlParameter("@huella_tpt", SqlDbType.VarBinary) { Value = bytes });

                                    }


                                    if (string.IsNullOrWhiteSpace(TabBiometHu["Huella_img"].ToString()) || TabBiometHu["Huella_img"].ToString() == null)
                                    {
                                        bytes2 = (byte[])(null);
                                        Tipo2 = "null";
                                    }
                                    else
                                    {
                                        bytes2 = (byte[])(TabBiometHu["Huella_img"]);
                                        Tipo2 = "@Huella_img";

                                        parameters.Add(new SqlParameter("@Huella_img", SqlDbType.VarBinary) { Value = bytes2 });

                                    }


                                    Utils.SqlDatos = "INSERT INTO [BDBIOMETSQL].[dbo].[Datos huella digital] " +
                                    "(" +
                                    "HistorPaci," +
                                    "huella_tpt," +
                                    "Huella_img," +
                                    "CodiRegis," +
                                    "FecRegis," +
                                    "FecModi," +
                                    "CodModi" +
                                    ")" +
                                    "VALUES" +
                                    "(" +
                                    "'" + TabBiometHu["HistorPaci"].ToString() + "'," +
                                    "" + Tipo + "," +
                                    "" + Tipo2 + "," +
                                    "'" + TabBiometHu["CodiRegis"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabBiometHu["FecRegis"].ToString())}" +
                                        $"{Conexion.ValidarFechaNula(TabBiometHu["FecModi"].ToString())}" +
                                    "'" + TabBiometHu["CodModi"].ToString() + "'" +
                                    ")";


                                    Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos, parameters);

                                    globalCanHueAgr += 1;


                                }
                                else
                                {
                                    TabBiometHuCentra.Read();
                                    if (Convert.ToDateTime(TabBiometHuCentra["FecRegis"]) > Convert.ToDateTime(TabBiometHu["FecRegis"]))
                                    {
                                        //'No modifique ningun dato
                                    }
                                    else
                                    {

                                        if (string.IsNullOrWhiteSpace(TabBiometHu["huella_tpt"].ToString()) || TabBiometHu["huella_tpt"].ToString() == null)
                                        {
                                            bytes = (byte[])(null);
                                            Tipo = "null";
                                        }
                                        else
                                        {
                                            bytes = (byte[])(TabBiometHu["huella_tpt"]);
                                            Tipo = "@huella_tpt";

                                            parameters.Add(new SqlParameter("@huella_tpt", SqlDbType.VarBinary) { Value = bytes });

                                        }


                                        if (string.IsNullOrWhiteSpace(TabBiometHu["Huella_img"].ToString()) || TabBiometHu["Huella_img"].ToString() == null)
                                        {
                                            bytes2 = (byte[])(null);
                                            Tipo2 = "null";
                                        }
                                        else
                                        {
                                            bytes2 = (byte[])(TabBiometHu["Huella_img"]);
                                            Tipo2 = "@Huella_img";

                                            parameters.Add(new SqlParameter("@Huella_img", SqlDbType.VarBinary) { Value = bytes2 });

                                        }


                                        //Modifique los datos
                                        Utils.SqlDatos = $"UPDATE [BDBIOMETSQL].[dbo].[Datos huella digital] SET  " +
                                        "huella_tpt = " + Tipo + ", " +
                                        "Huella_img = " + Tipo2 + ", " +
                                        $"FecModi = {Conexion.ValidarFechaNula(TabBiometHu["FecModi"].ToString())} " +
                                        "CodModi = '" + TabBiometHu["CodModi"].ToString() + "' " +
                                        "WHERE (HistorPaci = N'" + CodHisHue + "') ";

                                        Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos, parameters);

                                        globalCanHueMod += 1;


                                    }

                                }

                                TabBiometHuCentra.Close();

                            }//USing

                            ExportarBiometria.ReportProgress(contador);

                        }//While

                    }//(TabBiometHu.HasRows == false)

                    TabBiometHu.Close();

                }//Using


                TabBiometFi.Close();

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "después de hacer click sobre el botón exportar" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExportarBiometria.CancelAsync();
            }
        }

        private void ExportarBiometria_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Utils.Titulo01 = "Control para importar datos";
                Utils.Informa = "El proceso ha terminado satisfactoriamente " + "\r";
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);

                ProgressBar.Minimum = 0;
                ProgressBar.Maximum = 1;
                ProgressBar.Value = 0;


                LblCantidad.Text = "0";
                LblTotal.Text = "0";

                TxtCanFirAgr.Text = globalCanFirAgr.ToString();
                TxtCanFirMod.Text = globalCanFirmMod.ToString();
                TxtCanFotAgr.Text = globalCanFotAgr.ToString();
                TxtCanFotMod.Text = globalCanFotMod.ToString();
                TxtCanHueAgr.Text = globalCanHueAgr.ToString();
                TxtCanHueMod.Text = globalCanHueMod.ToString();


                LblDetener.Visible = false;
                BtnDetener.Visible = false;

                LblExportar.Visible = true;
                BtnBuscarPacientes.Visible = true;

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "después activar el RunWorkerCompleted del Workect" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportarBiometria_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                if (ExportarBiometria.CancellationPending == false)
                {
                    ProgressBar.Value = e.ProgressPercentage;
                    LblCantidad.Text = e.ProgressPercentage.ToString();
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "después activar el progressChaned del Workect" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDetener_Click(object sender, EventArgs e)
        {
            try
            {
                Utils.Titulo01 = "Control de ejecución";
                Utils.Informa = "El proceso ya esta corriendo " + "\r";
                Utils.Informa += "¿Desea Cancelarlo? " + "\r";
                var res2 = MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (res2 == DialogResult.Yes)
                {
                    ExportarBiometria.WorkerSupportsCancellation = true;
                    ExportarBiometria.CancelAsync();

                    ProgressBar.Minimum = 0;
                    ProgressBar.Maximum = 1;
                    ProgressBar.Value = 0;

                    LblCantidad.Text = "0";
                    LblTotal.Text = "0";

                    LblDetener.Visible = false;
                    BtnDetener.Visible = false;

                    LblExportar.Visible = true;
                    BtnBuscarPacientes.Visible = true;
                }

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "después activar el BtnDetener_Click" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
