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

namespace OBBDSIIG.Forms.FrmImportar
{
    public partial class FrmImportHigieneOral : Form
    {
        public FrmImportHigieneOral()
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

        private void CargarRangoFechas()
        {
            try
            {
                DateTime FechaActual = DateTime.Now;

                DateTime FechaUnMesAntes = DateTime.Now.AddMonths(-1);

                DateInicial.Value = FechaUnMesAntes;

                DateFinal.Value = FechaActual;
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al abrir funcion CargarRangoFechas" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } //Carga las fechas desde la fecha actual y un mes antes. Para los filtros



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



        private void FrmImportHigieneOral_Load(object sender, EventArgs e)
        {
            try
            {
                CargarDatosUser();
                CargarRangoFechas();
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al cargar el formulario  FrmImportHigieneOral " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscarPacientes_Click(object sender, EventArgs e)
        {
            try
            {
                string UsaRegis = "", SqlPlaca = "", SqlPlacaCen = "", CodBusHistPlaca, CodBusPlaca;



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


                if (DateInicial.Value > DateFinal.Value)
                {
                    Utils.Informa = "Lo siento pero";
                    Utils.Informa += "la fecha inicial no puede ser mayor a la fecha final";
                    MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                string FecIniPro = Convert.ToString(DateInicial.Value.ToString("yyyy-MM-dd"));
                string FecFinPro = Convert.ToString(DateFinal.Value.ToString("yyyy-MM-dd"));

                string PfiCen = TxtPrefiCenFor.Text;
                string PfiPor = TxtPrefiPorFor.Text;


                Utils.Informa = "¿Usted desea iniciar el proceso de importación" + "\r";
                Utils.Informa += "todas las atenciones de higienes oral en la" + "\r";
                Utils.Informa += "sede central al portatil.?" + "\r";
                Utils.Informa += "Fecha Inicial: " + FecIniPro + "\r";
                Utils.Informa += "Fecha Final: " + FecFinPro;
                var res = MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (res == DialogResult.Yes)
                {


                    TxtCanPlacaFor.Text = "0";
                    TxtCanPlacaFormExis.Text = "0";


                    ConectarCentral();


                    string SqlPlacaCount = "SELECT count(*) as TotalRegis FROM [DACONEXTSQL].[dbo].[Datos registro control placa] " +
                    "WHERE ([Datos registro control placa].ActivoCtl = 0 ) AND " +
                    "([Datos registro control placa].FechaRealiza >= CONVERT(DATETIME, '" + FecIniPro + "', 102)) AND " +
                    "([Datos registro control placa].FechaRealiza <= CONVERT(DATETIME, '" + FecFinPro + "', 102))";

                    int Total = 0;

                    SqlDataReader reader = Conexion.SQLDataReader(SqlPlacaCount);

                    if (reader.HasRows)
                    {
                        reader.Read();

                        Total = Convert.ToInt32(reader["TotalRegis"]);

                        if (Total != 0)
                        {
                            ProgresBar.Minimum = 1;
                            ProgresBar.Maximum = Total;
                        }


                    }
                    else
                    {
                        ProgresBar.Minimum = 0;
                        ProgresBar.Maximum = 1;
                        ProgresBar.Value = 0;
                    }

                    if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();


                    SqlPlaca = "SELECT * FROM [DACONEXTSQL].[dbo].[Datos registro control placa] " +
                    "WHERE ([Datos registro control placa].ActivoCtl = 0 ) AND " +
                    "([Datos registro control placa].FechaRealiza >= CONVERT(DATETIME, '" + FecIniPro + "', 102)) AND " +
                    "([Datos registro control placa].FechaRealiza <= CONVERT(DATETIME, '" + FecFinPro + "', 102))";

                    SqlDataReader TabControlPlaca;

                    using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                    {
                        SqlCommand command = new SqlCommand(SqlPlaca, connection);
                        command.Connection.Open();
                        TabControlPlaca = command.ExecuteReader();

                        if (TabControlPlaca.HasRows == false)
                        {
                            Utils.Informa = "Lo siento pero en el rango de fecha" + "\r";
                            Utils.Informa += "digitado no existen datos para importar, " + "\r";
                            Utils.Informa += "Datos registro control placa.";
                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            while (TabControlPlaca.Read())
                            {
                                //'Revisamos si el número de codigo de atencion existe
                                CodBusHistPlaca = null;
                                CodBusPlaca = null;
                                CodBusHistPlaca = TabControlPlaca["HistoriaPaci"].ToString();
                                CodBusPlaca = TabControlPlaca["ConsecutivoCtrl"].ToString();



                                SqlPlacaCen = "SELECT [Datos registro control placa].* ";
                                SqlPlacaCen = SqlPlacaCen + "FROM [DACONEXTSQL].[dbo].[Datos registro control placa] ";
                                SqlPlacaCen = SqlPlacaCen + "WHERE (ConsecutivoCtrl = '" + CodBusPlaca + "') AND (HistoriaPaci = '" + CodBusHistPlaca + "') ";

                                ConectarPortatil();

                                SqlDataReader TabPlacaCen;

                                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                                {
                                    SqlCommand command2 = new SqlCommand(SqlPlacaCen, connection2);
                                    command2.Connection.Open();
                                    TabPlacaCen = command2.ExecuteReader();

                                    if (TabPlacaCen.HasRows == false)
                                    {
                                        Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos registro control placa]" +
                                        "(" +
                                        "ConsecutivoCtrl," +
                                        "HistoriaPaci," +
                                        "FechaRealiza," +
                                        "HoraRealiza," +
                                        "ValorEdad," +
                                        "UnidadEdad," +
                                        "Sellantes," +
                                        "NoSellates," +
                                        "AplicaFluor," +
                                        "ControlPlaca," +
                                        "Detartraje," +
                                        "Cuatrante1," +
                                        "Cuatrante2," +
                                        "Cuatrante3," +
                                        "Cuatrante4," +
                                        "Der21," +
                                        "Der22," +
                                        "Der23," +
                                        "Der24," +
                                        "Der25," +
                                        "Der26," +
                                        "Der27," +
                                        "Der28," +
                                        "Der61," +
                                        "Der62," +
                                        "Der63," +
                                        "Der64," +
                                        "Der65," +
                                        "Izq11," +
                                        "Izq12," +
                                        "Izq13," +
                                        "Izq14," +
                                        "Izq15," +
                                        "Izq16," +
                                        "Izq17," +
                                        "Izq18," +
                                        "Izq51," +
                                        "Izq52," +
                                        "Izq53," +
                                        "Izq54," +
                                        "Izq55," +
                                        "Der31," +
                                        "Der32," +
                                        "Der33," +
                                        "Der34," +
                                        "Der35," +
                                        "Der36," +
                                        "Der37," +
                                        "Der38," +
                                        "Der71," +
                                        "Der72," +
                                        "Der73," +
                                        "Der74," +
                                        "Der75," +
                                        "Izq41," +
                                        "Izq42," +
                                        "Izq43," +
                                        "Izq44," +
                                        "Izq45," +
                                        "Izq46," +
                                        "Izq47," +
                                        "Izq48," +
                                        "Izq81," +
                                        "Izq82," +
                                        "Izq83," +
                                        "Izq84," +
                                        "Izq85," +
                                        "Suptotales," +
                                        "Suptenidas," +
                                        "ObservacionesH," +
                                        "CodMedi," +
                                        "ActivoCtl," +
                                        "CodRegistra," +
                                        "FechaRegis," +
                                        "CodModify," +
                                        "FechaModify," +
                                        "PrefiPlaca" +
                                        ")" +
                                        "VALUES" +
                                        "(" +
                                        "'" + TabControlPlaca["ConsecutivoCtrl"].ToString() + "'," +
                                        "'" + TabControlPlaca["HistoriaPaci"].ToString() + "'," +
                                        $"{Conexion.ValidarFechaNula(TabControlPlaca["FechaRealiza"].ToString())}" +
                                        $"{Conexion.ValidarHoraNula(TabControlPlaca["HoraRealiza"].ToString())}" +
                                        "'" + TabControlPlaca["ValorEdad"].ToString() + "'," +
                                        "'" + TabControlPlaca["UnidadEdad"].ToString() + "'," +
                                        "'" + TabControlPlaca["Sellantes"].ToString() + "'," +
                                        "'" + TabControlPlaca["NoSellates"].ToString() + "'," +
                                        "'" + TabControlPlaca["AplicaFluor"].ToString() + "'," +
                                        "'" + TabControlPlaca["ControlPlaca"].ToString() + "'," +
                                        "'" + TabControlPlaca["Detartraje"].ToString() + "'," +
                                        "'" + TabControlPlaca["Cuatrante1"].ToString() + "'," +
                                        "'" + TabControlPlaca["Cuatrante2"].ToString() + "'," +
                                        "'" + TabControlPlaca["Cuatrante3"].ToString() + "'," +
                                        "'" + TabControlPlaca["Cuatrante4"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der21"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der22"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der23"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der24"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der25"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der26"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der27"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der28"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der61"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der62"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der63"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der64"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der65"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq11"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq12"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq13"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq14"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq15"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq16"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq17"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq18"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq51"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq52"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq53"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq54"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq55"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der31"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der32"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der33"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der34"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der35"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der36"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der37"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der38"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der71"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der72"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der73"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der74"].ToString() + "'," +
                                        "'" + TabControlPlaca["Der75"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq41"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq42"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq43"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq44"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq45"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq46"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq47"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq48"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq81"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq82"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq83"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq84"].ToString() + "'," +
                                        "'" + TabControlPlaca["Izq85"].ToString() + "'," +
                                        "'" + TabControlPlaca["Suptotales"].ToString() + "'," +
                                        "'" + TabControlPlaca["Suptenidas"].ToString() + "'," +
                                        "'" + TabControlPlaca["ObservacionesH"].ToString() + "'," +
                                        "'" + TabControlPlaca["CodMedi"].ToString() + "'," +
                                        "'" + TabControlPlaca["ActivoCtl"].ToString() + "'," +
                                        "'" + TabControlPlaca["CodRegistra"].ToString() + "'," +
                                       $"{Conexion.ValidarFechaNula(TabControlPlaca["FechaRegis"].ToString())}" +
                                        "'" + TabControlPlaca["CodModify"].ToString() + "'," +
                                       $"{Conexion.ValidarFechaNula(TabControlPlaca["FechaModify"].ToString())}" +
                                        "'" + TabControlPlaca["PrefiPlaca"].ToString() + "'" +
                                        ")";


                                        Boolean RegisControlPlaca = Conexion.SqlInsert(Utils.SqlDatos);

                                        if (RegisControlPlaca)
                                        {
                                            int con = Convert.ToInt32(TxtCanPlacaFor.Text) + 1;
                                            TxtCanPlacaFor.Text = con.ToString();

                                        }

                                    }
                                    else
                                    {
                                        //Modifique los datos
                                        Utils.SqlDatos = $"UPDATE [DACONEXTSQL].[dbo].[Datos registro control placa] SET " +
                                        $"FechaRealiza = {Conexion.ValidarFechaNula(TabControlPlaca["FechaRealiza"].ToString())} " +
                                        $"HoraRealiza = {Conexion.ValidarHoraNula(TabControlPlaca["HoraRealiza"].ToString())}" +
                                        "ValorEdad = '" + TabControlPlaca["ValorEdad"].ToString() + "', " +
                                        "UnidadEdad = '" + TabControlPlaca["UnidadEdad"].ToString() + "', " +
                                        "Sellantes = '" + TabControlPlaca["Sellantes"].ToString() + "', " +
                                        "NoSellates = '" + TabControlPlaca["NoSellates"].ToString() + "', " +
                                        "AplicaFluor = '" + TabControlPlaca["AplicaFluor"].ToString() + "', " +
                                        "ControlPlaca = '" + TabControlPlaca["ControlPlaca"].ToString() + "', " +
                                        "Detartraje = '" + TabControlPlaca["Detartraje"].ToString() + "', " +
                                        "Cuatrante1 = '" + TabControlPlaca["Cuatrante1"].ToString() + "', " +
                                        "Cuatrante2 = '" + TabControlPlaca["Cuatrante2"].ToString() + "', " +
                                        "Cuatrante3 = '" + TabControlPlaca["Cuatrante3"].ToString() + "', " +
                                        "Cuatrante4 = '" + TabControlPlaca["Cuatrante4"].ToString() + "', " +
                                        "Der21 = '" + TabControlPlaca["Der21"].ToString() + "', " +
                                        "Der22 = '" + TabControlPlaca["Der22"].ToString() + "', " +
                                        "Der23 = '" + TabControlPlaca["Der23"].ToString() + "', " +
                                        "Der24 = '" + TabControlPlaca["Der24"].ToString() + "', " +
                                        "Der25 = '" + TabControlPlaca["Der25"].ToString() + "', " +
                                        "Der26 = '" + TabControlPlaca["Der26"].ToString() + "', " +
                                        "Der27 = '" + TabControlPlaca["Der27"].ToString() + "', " +
                                        "Der28 = '" + TabControlPlaca["Der28"].ToString() + "', " +
                                        "Der61 = '" + TabControlPlaca["Der61"].ToString() + "', " +
                                        "Der62 = '" + TabControlPlaca["Der62"].ToString() + "', " +
                                        "Der63 = '" + TabControlPlaca["Der63"].ToString() + "', " +
                                        "Der64 = '" + TabControlPlaca["Der64"].ToString() + "', " +
                                        "Der65 = '" + TabControlPlaca["Der65"].ToString() + "', " +
                                        "Izq11 = '" + TabControlPlaca["Izq11"].ToString() + "', " +
                                        "Izq12 = '" + TabControlPlaca["Izq12"].ToString() + "', " +
                                        "Izq13 = '" + TabControlPlaca["Izq13"].ToString() + "', " +
                                        "Izq14 = '" + TabControlPlaca["Izq14"].ToString() + "', " +
                                        "Izq15 = '" + TabControlPlaca["Izq15"].ToString() + "', " +
                                        "Izq16 = '" + TabControlPlaca["Izq16"].ToString() + "', " +
                                        "Izq17 = '" + TabControlPlaca["Izq17"].ToString() + "', " +
                                        "Izq18 = '" + TabControlPlaca["Izq18"].ToString() + "', " +
                                        "Izq51 = '" + TabControlPlaca["Izq51"].ToString() + "', " +
                                        "Izq52 = '" + TabControlPlaca["Izq52"].ToString() + "', " +
                                        "Izq53 = '" + TabControlPlaca["Izq53"].ToString() + "', " +
                                        "Izq54 = '" + TabControlPlaca["Izq54"].ToString() + "', " +
                                        "Izq55 = '" + TabControlPlaca["Izq55"].ToString() + "', " +
                                        "Der31 = '" + TabControlPlaca["Der31"].ToString() + "', " +
                                        "Der32 = '" + TabControlPlaca["Der32"].ToString() + "', " +
                                        "Der33 = '" + TabControlPlaca["Der33"].ToString() + "', " +
                                        "Der34 = '" + TabControlPlaca["Der34"].ToString() + "', " +
                                        "Der35 = '" + TabControlPlaca["Der35"].ToString() + "', " +
                                        "Der36 = '" + TabControlPlaca["Der36"].ToString() + "', " +
                                        "Der37 = '" + TabControlPlaca["Der37"].ToString() + "', " +
                                        "Der38 = '" + TabControlPlaca["Der38"].ToString() + "', " +
                                        "Der71 = '" + TabControlPlaca["Der71"].ToString() + "', " +
                                        "Der72 = '" + TabControlPlaca["Der72"].ToString() + "', " +
                                        "Der73 = '" + TabControlPlaca["Der73"].ToString() + "', " +
                                        "Der74 = '" + TabControlPlaca["Der74"].ToString() + "', " +
                                        "Der75 = '" + TabControlPlaca["Der75"].ToString() + "', " +
                                        "Izq41 = '" + TabControlPlaca["Izq41"].ToString() + "', " +
                                        "Izq42 = '" + TabControlPlaca["Izq42"].ToString() + "', " +
                                        "Izq43 = '" + TabControlPlaca["Izq43"].ToString() + "', " +
                                        "Izq44 = '" + TabControlPlaca["Izq44"].ToString() + "', " +
                                        "Izq45 = '" + TabControlPlaca["Izq45"].ToString() + "', " +
                                        "Izq46 = '" + TabControlPlaca["Izq46"].ToString() + "', " +
                                        "Izq47 = '" + TabControlPlaca["Izq47"].ToString() + "', " +
                                        "Izq48 = '" + TabControlPlaca["Izq48"].ToString() + "', " +
                                        "Izq81 = '" + TabControlPlaca["Izq81"].ToString() + "', " +
                                        "Izq82 = '" + TabControlPlaca["Izq82"].ToString() + "', " +
                                        "Izq83 = '" + TabControlPlaca["Izq83"].ToString() + "', " +
                                        "Izq84 = '" + TabControlPlaca["Izq84"].ToString() + "', " +
                                        "Izq85 = '" + TabControlPlaca["Izq85"].ToString() + "', " +
                                        "Suptotales = '" + TabControlPlaca["Suptotales"].ToString() + "', " +
                                        "Suptenidas = '" + TabControlPlaca["Suptenidas"].ToString() + "', " +
                                        "ObservacionesH = '" + TabControlPlaca["ObservacionesH"].ToString() + "', " +
                                        "CodMedi = '" + TabControlPlaca["CodMedi"].ToString() + "', " +
                                        "ActivoCtl = '" + TabControlPlaca["ActivoCtl"].ToString() + "', " +
                                        "CodRegistra = '" + TabControlPlaca["CodRegistra"].ToString() + "', " +
                                        $"FechaRegis = {Conexion.ValidarFechaNula(TabControlPlaca["FechaRegis"].ToString())} " +
                                        "CodModify = '" + TabControlPlaca["CodModify"].ToString() + "', " +
                                        $"FechaModify = {Conexion.ValidarFechaNula(TabControlPlaca["FechaModify"].ToString())} " +
                                        "PrefiPlaca = '" + TabControlPlaca["PrefiPlaca"].ToString() + "' " +
                                        "WHERE  (ConsecutivoCtrl = '" + CodBusPlaca + "') AND (HistoriaPaci = '" + CodBusHistPlaca + "')";

                                        Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos);

                                        if (ActControl)
                                        {
                                            int con = Convert.ToInt32(TxtCanPlacaFormExis.Text) + 1;
                                            TxtCanPlacaFormExis.Text = con.ToString();

                                        }

                                    }//'Final deif (TabPlacaCen.HasRows == false)

                                    TabPlacaCen.Close();

                                }//USing

                                ProgresBar.Increment(1);

                            }//While

                            Utils.Informa = "El proceso ha terminado satisfactoriamente";
                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ProgresBar.Minimum = 0;
                            ProgresBar.Maximum = 1;
                            ProgresBar.Value = 0;

                        }// if (TabControlPlaca.HasRows == false)

                    }//Using
                }//Pregunta
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "después de hacer click sobre el botón exportar" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                ProgresBar.Minimum = 0;
                ProgresBar.Maximum = 1;
                ProgresBar.Value = 0;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
