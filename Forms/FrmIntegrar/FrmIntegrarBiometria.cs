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
    public partial class FrmIntegrarBiometria : Form
    {
        public FrmIntegrarBiometria()
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
           
                    TxtInstanPortaFor.Text = Utils.InstanPortaFor;
      
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

        private string ValidarFechaNula(string Fecha)
        {
            string ValidarFecha = null;

            ValidarFecha = string.IsNullOrWhiteSpace(Fecha) ? "null" + "," : "CONVERT(DATETIME,'" + Convert.ToDateTime(Fecha).ToString("yyyy-MM-dd") + "',102),";

            return ValidarFecha;

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
                string UsaRegis = "", SqlBiometFi = "", CodHisFir, CodBusPlaca, SqlBiometFirCen;



                DateTime Fecha2 = DateTime.Now;
                string Fecha = Fecha2.ToString("yyyy-MM-dd");


                UsaRegis = lblCodigoUser.Text;
                Utils.Titulo01 = "Control para exportar datos";


                if (string.IsNullOrWhiteSpace(TxtInstanCenFor.Text) || (TxtInstanCenFor.Text == ""))
                {
                    Utils.Informa = "Lo siento pero mientras no exista";
                    Utils.Informa += "el nombre de la instancia central,";
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


                Utils.Informa = "¿Usted desea iniciar el proceso de integración de los" + "\r";
                Utils.Informa += "datos Biometricos en la instancia del" + "\r";
                Utils.Informa += "portatil a la instancia del servidor central.?" + "\r";
                var res = MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (res == DialogResult.Yes)
                {

                    TxtCanFirAgr.Text = "0";
                    TxtCanFirMod.Text = "0";

                    // 'Datos para la Firma digital del paciente

                    //'Consultamos los datos de las firmas en el equipo BRIGADA

                    SqlBiometFi = "SELECT [Datos firma digital].* ";
                    SqlBiometFi = SqlBiometFi + "FROM [BDBIOMETSQL].[dbo].[Datos firma digital] ";
                    SqlBiometFi = SqlBiometFi + "ORDER BY HistorPaci";


                    ConectarPortatil();

                    SqlDataReader TabBiometFi;

                    using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                    {
                        SqlCommand command = new SqlCommand(SqlBiometFi, connection);
                        command.Connection.Open();
                        TabBiometFi = command.ExecuteReader();

                        if (TabBiometFi.HasRows == false)
                        {
                            Utils.Informa = "Lo siento pero en el rango de fecha" + "\r";
                            Utils.Informa += "digitado no existen datos para importar, " + "\r";
                            Utils.Informa += "Datos registro control placa.";
                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            while (TabBiometFi.Read())
                            {
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
                                        "'" + TabBiometFi["Firma"].ToString() + "'," +
                                        "'" + TabBiometFi["CodiRegis"].ToString() + "'," +
                                        $"{ValidarFechaNula(TabBiometFi["FecRegis"].ToString())}" +
                                         $"{ValidarFechaNula(TabBiometFi["FecModi"].ToString())}" +
                                        "'" + TabBiometFi["CodModi"].ToString() + "'" +
                                        ")";


                                        Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos);

                                        if (Regis)
                                        {
                                            int con = Convert.ToInt32(TxtCanFirAgr.Text) + 1;
                                            TxtCanFirAgr.Text = con.ToString();
                                        }


                                    }
                                    else
                                    {
                                        if (Convert.ToDateTime(TabBiometFiCentra["FecRegis"]) > Convert.ToDateTime(TabBiometFi["FecRegis"]))
                                        {
                                            //'No modifique ningun dato
                                        }
                                        else
                                        {
                                            //Modifique los datos
                                            Utils.SqlDatos = $"UPDATE [BDBIOMETSQL].[dbo].[Datos firma digital] SET " +
                                            "Firma = '" + TabBiometFi["Firma"].ToString() + "', " +
                                            $"FecModi = {ValidarFechaNula(TabBiometFi["FecModi"].ToString())} " +
                                            "CodModi = '" + TabBiometFi["CodModi"].ToString() + "' " +
                                            "WHERE (HistorPaci = N'" + CodHisFir + "') ";

                                            Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos);

                                            if (ActControl)
                                            {
                                                int con = Convert.ToInt32(TxtCanFirAgr.Text) + 1;
                                                TxtCanFirAgr.Text = con.ToString();

                                            }
                                        }


                                    }//'Final deif (TabPlacaCen.HasRows == false)

                                    TabBiometFi.Close();

                                }//USing

                            }//While

                            TabBiometFi.Close();

                            TxtCanFotAgr.Text = "0";
                            TxtCanFotMod.Text = "0";

                            //Consultamos los datos de las firmas en el equipo BRIGADA
                            string SqlBiometFotCen, CodHisFot;
                            string SqlBiometFo = "SELECT [Datos foto digital].* ";
                            SqlBiometFo = SqlBiometFo + "FROM [BDBIOMETSQL].[dbo].[Datos foto digital] ";
                            SqlBiometFo = SqlBiometFo + "ORDER BY HistorPaci";

                            ConectarPortatil();

                            SqlDataReader TabBiometFo;
                            SqlDataReader TabBiometFoCentra;

                            using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command2 = new SqlCommand(SqlBiometFo, connection2);
                                command2.Connection.Open();
                                TabBiometFo = command2.ExecuteReader();

                                if (TabBiometFo.HasRows == false)
                                {
                                    ConectarCentral();

                                    while (TabBiometFo.Read())
                                    {
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
                                                "'" + TabBiometFo["FirmaFoto"].ToString() + "'," +
                                                "'" + TabBiometFo["CodiRegis"].ToString() + "'," +
                                                $"{ValidarFechaNula(TabBiometFo["FecRegis"].ToString())}" +
                                                 $"{ValidarFechaNula(TabBiometFo["FecModi"].ToString())}" +
                                                "'" + TabBiometFo["CodModi"].ToString() + "'" +
                                                ")";


                                                Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos);

                                                if (Regis)
                                                {
                                                    int con = Convert.ToInt32(TxtCanFotAgr.Text) + 1;
                                                    TxtCanFotAgr.Text = con.ToString();
                                                }


                                            }
                                            else
                                            {
                                                if (Convert.ToDateTime(TabBiometFoCentra["FecRegis"]) > Convert.ToDateTime(TabBiometFo["FecRegis"]))
                                                {
                                                    //'No modifique ningun dato
                                                }
                                                else
                                                {
                                                    //Modifique los datos
                                                    Utils.SqlDatos = $"UPDATE [BDBIOMETSQL].[dbo].[Datos foto digital] SET  " +
                                                    "Foto = '" + TabBiometFi["Foto"].ToString() + "', " +
                                                    $"FecModi = {ValidarFechaNula(TabBiometFi["FecModi"].ToString())} " +
                                                    "CodModi = '" + TabBiometFi["CodModi"].ToString() + "' " +
                                                    "WHERE (HistorPaci = N'" + CodHisFot + "') ";

                                                    Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos);

                                                    if (ActControl)
                                                    {
                                                        int con = Convert.ToInt32(TxtCanFotMod.Text) + 1;
                                                        TxtCanFotMod.Text = con.ToString();

                                                    }
                                                }
                                            }
                                            TabBiometFoCentra.Close();
                                        }
                                    }
                                }
                            }


                                TxtCanHueAgr.Text = "0";
                                TxtCanHueMod.Text = "0";

                                //Consultamos los datos de las firmas en el equipo BRIGADA

                                string SqlBiometHu = "SELECT [Datos huella digital].* ";
                                SqlBiometHu = SqlBiometHu + "FROM [BDBIOMETSQL].[dbo].[Datos huella digital] ";
                                SqlBiometHu = SqlBiometHu + "ORDER BY HistorPaci";

                                string CodHisHue, SqlBiometHueCen;

                                ConectarPortatil();

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
                                        while (TabBiometHu.Read())
                                        {
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
                                                    "'" + TabBiometHu["huella_tpt"].ToString() + "'," +
                                                    "'" + TabBiometHu["Huella_img"].ToString() + "'," +
                                                    "'" + TabBiometHu["CodiRegis"].ToString() + "'," +
                                                    $"{ValidarFechaNula(TabBiometHu["FecRegis"].ToString())}" +
                                                        $"{ValidarFechaNula(TabBiometHu["FecModi"].ToString())}" +
                                                    "'" + TabBiometHu["CodModi"].ToString() + "'" +
                                                    ")";


                                                    Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos);

                                                    if (Regis)
                                                    {
                                                        int con = Convert.ToInt32(TxtCanHueAgr.Text) + 1;
                                                        TxtCanHueAgr.Text = con.ToString();
                                                    }
                                                }
                                                else
                                                {
                                                    if (Convert.ToDateTime(TabBiometHuCentra["FecRegis"]) > Convert.ToDateTime(TabBiometHu["FecRegis"]))
                                                    {
                                                        //'No modifique ningun dato
                                                    }
                                                    else
                                                    {
                                                        //Modifique los datos
                                                        Utils.SqlDatos = $"UPDATE [BDBIOMETSQL].[dbo].[Datos huella digital] SET  " +
                                                        "huella_tpt = '" + TabBiometHu["huella_tpt"].ToString() + "', " +
                                                        "Huella_img = '" + TabBiometHu["Huella_img"].ToString() + "', " +
                                                        $"FecModi = {ValidarFechaNula(TabBiometFi["FecModi"].ToString())} " +
                                                        "CodModi = '" + TabBiometFi["CodModi"].ToString() + "' " +
                                                        "WHERE (HistorPaci = N'" + CodHisHue + "') ";

                                                        Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos);

                                                        if (ActControl)
                                                        {
                                                            int con = Convert.ToInt32(TxtCanHueMod.Text) + 1;
                                                            TxtCanHueMod.Text = con.ToString();

                                                        }
                                                    }
                                                }

                                                TabBiometHuCentra.Close();

                                            }//USing
                                        }//Whilw

                                    }//(TabBiometHu.HasRows == false)

                                    TabBiometHu.Close();

                                }//Using

                                Utils.Informa = "El proceso ha terminado satisfactoriamente";
                                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);


                        }// if (TabBiometFi.HasRows == false)

                        TabBiometFi.Close();

                    }//Using
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "después de hacer click sobre el botón integrar" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
