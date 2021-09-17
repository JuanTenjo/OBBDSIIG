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
namespace OBBDSIIG.Forms.FrmExportar
{
    public partial class FrmExportSedeCentralCito : Form
    {
        public FrmExportSedeCentralCito()
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

                int mes = DateTime.Now.Month;

                int ano = DateTime.Now.Year;

                int FechaUnMesAntes2 = mes - 1;

                DateTime primerDiaMesAntes = new DateTime(ano, FechaUnMesAntes2, 1);

                DateTime ultimoDiaMesAntes = primerDiaMesAntes.AddMonths(1).AddDays(-1);

                DateInicial.Value = primerDiaMesAntes;

                DateFinal.Value = ultimoDiaMesAntes;

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al abrir funcion CargarRangoFechas" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaDatosEmpresa()
        {
            try
            {
                if (LblCodEntiFac.Text != "00")
                {
                    string SqlInforEmpre;
                    string CodEniBus = LblCodEntiFac.Text;

                    SqlInforEmpre = "SELECT CodUnico, TipoDocEmp, NitCCEmpresa, DigVerEm, CodiMinSalud, NomEmpresa, NomSecEmpre, CatEmpresa, LogoEmpresa, Eslogan, ";
                    SqlInforEmpre = SqlInforEmpre + "FirmDigPaci, HueDigPaci, FotoDigPaci, FacturElec, FecFacElec, DiasVenFac  ";
                    SqlInforEmpre = SqlInforEmpre + "FROM [BDADMINSIG].[dbo].[Datos informacion de la empresa] ";
                    SqlInforEmpre = SqlInforEmpre + "WHERE (CodUnico = N'" + CodEniBus + "')";

                    ConectarCentral();
                    SqlDataReader TabInforEmpre = Conexion.SQLDataReader(SqlInforEmpre);


                    if (TabInforEmpre.HasRows == false)
                    {

                    }
                    else
                    {
                        TabInforEmpre.Read();
                        Boolean FacturElect = Convert.ToBoolean(TabInforEmpre["FacturElec"]);

                        if (FacturElect)
                        {
                            LblDiasVenFac.Text = Convert.ToString(TabInforEmpre["DiasVenFac"]);

                        }
                        else
                        {
                            LblDiasVenFac.Text = "0";
                        }
                    }

                    TabInforEmpre.Close();
                    if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();

                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al cargar la funcion CargaDatosEmpresa " + "\r";
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

        private void FrmExportSedeCentralCito_Load(object sender, EventArgs e)
        {
            try
            {
                CargarDatosUser();
                CargarRangoFechas();
                CargaDatosEmpresa();
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al cargar el formulario FrmExportSedeCentral " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            if (ExportarHistoCito.IsBusy == true) //Si el proceso esta corriendo no puede voler a iniciarse 
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

        private void BtnBuscarPacientes_Click(object sender, EventArgs e)
        {
            try
            {

                if (ExportarHistoCito.IsBusy != true) //Si el proceso esta corriendo no puede voler a iniciarse 
                {

                    globalCanCitoFor = 0;
                    globalCanCitoFormExis = 0;
    
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

                    if (string.IsNullOrWhiteSpace(TxtPrefiCenFor.Text) || (TxtPrefiCenFor.Text == ""))
                    {
                        Utils.Informa = "Lo siento pero mientras no exista";
                        Utils.Informa += "el prefijo de la instancia central,";
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

                    if (string.IsNullOrWhiteSpace(TxtPrefiPorFor.Text) || (TxtPrefiPorFor.Text == ""))
                    {
                        Utils.Informa = "Lo siento pero mientras no exista";
                        Utils.Informa += "prefijo de la instancia del porttatil,";
                        Utils.Informa += "no se puede empezar a ejecutar el";
                        Utils.Informa += "proceso de exportación de datos.";
                        MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    if (DateInicial.Value > DateFinal.Value)
                    {
                        Utils.Informa = "Lo siento pero";
                        Utils.Informa += "la fecha inicial no puede ser mayor a la fecha final";
                        MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    string FecIniPro = Convert.ToString(DateInicial.Value.ToString("yyyy-MM-dd"));
                    string FecFinPro = Convert.ToString(DateFinal.Value.ToString("yyyy-MM-dd"));

                    string PfiCen = TxtPrefiCenFor.Text;
                    string PfiPor = TxtPrefiPorFor.Text;


                    Utils.Informa = "¿Usted desea iniciar el proceso de exportación" + "\r";
                    Utils.Informa += "todas las citologias en la instancia del" + "\r";
                    Utils.Informa += "portatil a la instancia del servidor central?" + "\r";
                    Utils.Informa += "Fecha Inicial: " + FecIniPro + "\r";
                    Utils.Informa += "Fecha Final: " + FecFinPro;


                    var res = MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                    if (res == DialogResult.Yes)
                    {


                        TxtCanCitoFor.Text = "0";
                        TxtCanCitoFormExis.Text = "0";


                        ConectarPortatil();

                        string SqlCitoCount = "SELECT count(*) as TotalRegis FROM [BDSITOI].[dbo].[Datos basicos citologia] ";
                        SqlCitoCount += "WHERE ([Datos basicos citologia].PrefiCito = N'" + PfiPor + "') AND";
                        SqlCitoCount += "([Datos basicos citologia].CierreToma = 'True' ) AND ";
                        SqlCitoCount += "([Datos basicos citologia].FecRadi >= CONVERT(DATETIME, '" + FecIniPro + "', 102)) AND";
                        SqlCitoCount += "([Datos basicos citologia].FecRadi <= CONVERT(DATETIME, '" + FecFinPro + "', 102))";


                        SqlDataReader reader = Conexion.SQLDataReader(SqlCitoCount);

                        int Total = 0;

                        if (reader.HasRows)
                        {
                            reader.Read();

                            Total = Convert.ToInt32(reader["TotalRegis"]);

                            if (Total != 0)
                            {


                                LblDetener.Visible = true;
                                BtnDetener.Visible = true;
                                LblExportar.Visible = false;
                                BtnBuscarPacientes.Visible = false;


                                ProgressBar.Minimum = 1;
                                ProgressBar.Maximum = Total;
                                LblTotal.Text = Total.ToString();

                                ExportarHistoCito.RunWorkerAsync();

                            }
                            else
                            {
                                Utils.Informa = "Lo siento pero en el rango de fecha " + "\r";
                                Utils.Informa += "digitado no existen datos para exportar." + "\r";
                                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ProgressBar.Minimum = 0;
                                ProgressBar.Maximum = 1;
                                ProgressBar.Value = 0;
                                LblTotal.Text = "0";

                            }
                        }
             

                        if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();


                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "después de hacer click sobre el botón exportar" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                ProgressBar.Minimum = 0;
                ProgressBar.Maximum = 1;
                ProgressBar.Value = 0;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        int globalCanCitoFor = 0;
        int globalCanCitoFormExis = 0;

        private void ExportarHistoCito_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int contador = 0;

                string SqlCito = "", SqlCitoCen = "", CodBusCito = "";

                string FecIniPro = Convert.ToString(DateInicial.Value.ToString("yyyy-MM-dd"));
                string FecFinPro = Convert.ToString(DateFinal.Value.ToString("yyyy-MM-dd"));

                string PfiCen = TxtPrefiCenFor.Text;
                string PfiPor = TxtPrefiPorFor.Text;


                ConectarPortatil();

                SqlCito = "SELECT * FROM [BDSITOI].[dbo].[Datos basicos citologia] ";
                SqlCito += "WHERE ([Datos basicos citologia].PrefiCito = N'" + PfiPor + "') AND";
                SqlCito += "([Datos basicos citologia].CierreToma = 'True' ) AND ";
                SqlCito += "([Datos basicos citologia].FecRadi >= CONVERT(DATETIME, '" + FecIniPro + "', 102)) AND";
                SqlCito += "([Datos basicos citologia].FecRadi <= CONVERT(DATETIME, '" + FecFinPro + "', 102))";


                SqlDataReader TabCitologia;

                using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command = new SqlCommand(SqlCito, connection);
                    command.Connection.Open();
                    TabCitologia = command.ExecuteReader();

                    if (TabCitologia.HasRows == false)
                    {
                        Utils.Informa = "Lo siento pero en el rango de fecha" + "\r";
                        Utils.Informa += "registrar más facturas de ventas electrónicas," + "\r";
                        Utils.Informa += "porque pasó la longitud permitida del código.";
                        MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        while (TabCitologia.Read())
                        {
                            //Revisamos si el número de codigo de atencion existe

                            CodBusCito = TabCitologia["CodFiCito"].ToString();

                            SqlCitoCen = "SELECT [Datos basicos citologia].* ";
                            SqlCitoCen += "FROM [BDSITOI].[dbo].[Datos basicos citologia] ";
                            SqlCitoCen += "WHERE (CodFiCito = '" + CodBusCito + "')";


                            ConectarCentral();

                            SqlDataReader TabCitoCen;

                            using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command2 = new SqlCommand(SqlCitoCen, connection2);
                                command2.Connection.Open();
                                TabCitoCen = command2.ExecuteReader();

                                if (TabCitoCen.HasRows == false)
                                {
                                    //Agregue

                                    Utils.SqlDatos = "INSERT INTO [BDSITOI].[dbo].[Datos basicos citologia]" +
                                   "(" +
                                    "PrefiFicha," +
                                    "CodFiCito," +
                                    "NumRadi," +
                                    "FecRadi," +
                                    "PrefiFac," +
                                    "NumFac," +
                                    "NumHisto," +
                                    "TipUsua," +
                                    "ValEdad," +
                                    "UnidEdad," +
                                    "CodAPB," +
                                    "ContraNum," +
                                    "InsToma," +
                                    "FecToma," +
                                    "CodEsque," +
                                    "UltiRegla," +
                                    "NumGesta," +
                                    "NumPartos," +
                                    "UltiParto," +
                                    "NumAbor," +
                                    "EdadRela," +
                                    "Embarazada," +
                                    "UltiCito," +
                                    "ResCito," +
                                    "MetoPlani," +
                                    "CodPro," +
                                    "FecPro," +
                                    "CodAsCue," +
                                    "TipDocIden," +
                                    "IdenPerso," +
                                    "ObserToma," +
                                    "CierreToma," +
                                    "CodCierTo," +
                                    "FeCierTo," +
                                    "FecRecLec1," +
                                    "NumRadi2," +
                                    "SatisMues," +
                                    "DaClIna," +
                                    "Hemo," +
                                    "CelEsca," +
                                    "ExuDen," +
                                    "PoPreCel," +
                                    "CateGen," +
                                    "Tricho," +
                                    "HonMorCa," +
                                    "CaCeHerSim," +
                                    "CaFloVaBa," +
                                    "Inflama," +
                                    "BacMorAcSP," +
                                    "RepaTipica," +
                                    "Radia," +
                                    "DIU," +
                                    "Atrofia," +
                                    "PreCelEn," +
                                    "PreCelGla," +
                                    "EvaHormo," +
                                    "AscusInde," +
                                    "AscusNoHG," +
                                    "LEsInBaGra," +
                                    "LEsInAlGra," +
                                    "SosCarIn," +
                                    "CarCelEs," +
                                    "Atipicas," +
                                    "AtiEndo," +
                                    "AtiEndome," +
                                    "AtiSinEs," +
                                    "AtiFaNeo," +
                                    "AdeCarIn," +
                                    "AdeNoCar," +
                                    "OtrasNeo," +
                                    "CelGlaEs," +
                                    "ObserLec," +
                                    "TDLec," +
                                    "IDPLec," +
                                    "LecToma," +
                                    "FecLec," +
                                    "VezLec," +
                                    "CerLec," +
                                    "CodCerLec," +
                                    "FecCerLec," +
                                    "ParaPato," +
                                    "FecRecLec2," +
                                    "NumRadi3," +
                                    "TDLec2," +
                                    "IDPLec2," +
                                    "LecToma2," +
                                    "FecLec2," +
                                    "CerLec2," +
                                    "CodCerLec2," +
                                    "FecCerLec2," +
                                    "DiagDefi," +
                                    "AnulToma," +
                                    "RazAnulTo," +
                                    "CodAnul," +
                                    "FecAnul," +
                                    "ConCiPa," +
                                    "NoConCiPa," +
                                    "CodRegis," +
                                    "FecRegis," +
                                    "HorRegis," +
                                    "CodIPSLec," +
                                    "PrefiCito" +
                                   ")" +
                                    "VALUES" +
                                    "(" +
                                    "'" + TabCitologia["PrefiFicha"].ToString() + "'," +
                                    "'" + TabCitologia["CodFiCito"].ToString() + "'," +
                                    "'" + TabCitologia["NumRadi"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabCitologia["FecRadi"].ToString())}" +
                                    "'" + TabCitologia["PrefiFac"].ToString() + "'," +
                                    "'" + TabCitologia["NumFac"].ToString() + "'," +
                                    "'" + TabCitologia["NumHisto"].ToString() + "'," +
                                    "'" + TabCitologia["TipUsua"].ToString() + "'," +
                                    "'" + TabCitologia["ValEdad"].ToString() + "'," +
                                    "'" + TabCitologia["UnidEdad"].ToString() + "'," +
                                    "'" + TabCitologia["CodAPB"].ToString() + "'," +
                                    "'" + TabCitologia["ContraNum"].ToString() + "'," +
                                    "'" + TabCitologia["InsToma"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabCitologia["FecToma"].ToString())}" +
                                    "'" + TabCitologia["CodEsque"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabCitologia["UltiRegla"].ToString())}" +
                                    "'" + TabCitologia["NumGesta"].ToString() + "'," +
                                    "'" + TabCitologia["NumPartos"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabCitologia["UltiParto"].ToString())}" +
                                    "'" + TabCitologia["NumAbor"].ToString() + "'," +
                                    "'" + TabCitologia["EdadRela"].ToString() + "'," +
                                    "'" + TabCitologia["Embarazada"].ToString() + "'," +
                                   $"{Conexion.ValidarFechaNula(TabCitologia["UltiCito"].ToString())}" +
                                    "'" + TabCitologia["ResCito"].ToString() + "'," +
                                    "'" + TabCitologia["MetoPlani"].ToString() + "'," +
                                    "'" + TabCitologia["CodPro"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabCitologia["FecPro"].ToString())}" +
                                    "'" + TabCitologia["CodAsCue"].ToString() + "'," +
                                    "'" + TabCitologia["TipDocIden"].ToString() + "'," +
                                    "'" + TabCitologia["IdenPerso"].ToString() + "'," +
                                    "'" + TabCitologia["ObserToma"].ToString() + "'," +
                                    "'" + TabCitologia["CierreToma"].ToString() + "'," +
                                    "'" + TabCitologia["CodCierTo"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabCitologia["FeCierTo"].ToString())}" +
                                    $"{Conexion.ValidarFechaNula(TabCitologia["FecRecLec1"].ToString())}" +
                                    "'" + TabCitologia["NumRadi2"].ToString() + "'," +
                                    "'" + TabCitologia["SatisMues"].ToString() + "'," +
                                    "'" + TabCitologia["DaClIna"].ToString() + "'," +
                                    "'" + TabCitologia["Hemo"].ToString() + "'," +
                                    "'" + TabCitologia["CelEsca"].ToString() + "'," +
                                    "'" + TabCitologia["ExuDen"].ToString() + "'," +
                                    "'" + TabCitologia["PoPreCel"].ToString() + "'," +
                                    "'" + TabCitologia["CateGen"].ToString() + "'," +
                                    "'" + TabCitologia["Tricho"].ToString() + "'," +
                                    "'" + TabCitologia["HonMorCa"].ToString() + "'," +
                                    "'" + TabCitologia["CaCeHerSim"].ToString() + "'," +
                                    "'" + TabCitologia["CaFloVaBa"].ToString() + "'," +
                                    "'" + TabCitologia["Inflama"].ToString() + "'," +
                                    "'" + TabCitologia["BacMorAcSP"].ToString() + "'," +
                                    "'" + TabCitologia["RepaTipica"].ToString() + "'," +
                                    "'" + TabCitologia["Radia"].ToString() + "'," +
                                    "'" + TabCitologia["DIU"].ToString() + "'," +
                                    "'" + TabCitologia["Atrofia"].ToString() + "'," +
                                    "'" + TabCitologia["PreCelEn"].ToString() + "'," +
                                    "'" + TabCitologia["PreCelGla"].ToString() + "'," +
                                    "'" + TabCitologia["EvaHormo"].ToString() + "'," +
                                    "'" + TabCitologia["AscusInde"].ToString() + "'," +
                                    "'" + TabCitologia["AscusNoHG"].ToString() + "'," +
                                    "'" + TabCitologia["LEsInBaGra"].ToString() + "'," +
                                    "'" + TabCitologia["LEsInAlGra"].ToString() + "'," +
                                    "'" + TabCitologia["SosCarIn"].ToString() + "'," +
                                    "'" + TabCitologia["CarCelEs"].ToString() + "'," +
                                    "'" + TabCitologia["Atipicas"].ToString() + "'," +
                                    "'" + TabCitologia["AtiEndo"].ToString() + "'," +
                                    "'" + TabCitologia["AtiEndome"].ToString() + "'," +
                                    "'" + TabCitologia["AtiSinEs"].ToString() + "'," +
                                    "'" + TabCitologia["AtiFaNeo"].ToString() + "'," +
                                    "'" + TabCitologia["AdeCarIn"].ToString() + "'," +
                                    "'" + TabCitologia["AdeNoCar"].ToString() + "'," +
                                    "'" + TabCitologia["OtrasNeo"].ToString() + "'," +
                                    "'" + TabCitologia["CelGlaEs"].ToString() + "'," +
                                    "'" + TabCitologia["ObserLec"].ToString() + "'," +
                                    "'" + TabCitologia["TDLec"].ToString() + "'," +
                                    "'" + TabCitologia["IDPLec"].ToString() + "'," +
                                    "'" + TabCitologia["LecToma"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabCitologia["FecLec"].ToString())}" +
                                    "'" + TabCitologia["VezLec"].ToString() + "'," +
                                    "'" + TabCitologia["CerLec"].ToString() + "'," +
                                    "'" + TabCitologia["CodCerLec"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabCitologia["FecCerLec"].ToString())}" +
                                    "'" + TabCitologia["ParaPato"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabCitologia["FecRecLec2"].ToString())}" +
                                    "'" + TabCitologia["NumRadi3"].ToString() + "'," +
                                    "'" + TabCitologia["TDLec2"].ToString() + "'," +
                                    "'" + TabCitologia["IDPLec2"].ToString() + "'," +
                                    "'" + TabCitologia["LecToma2"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabCitologia["FecLec2"].ToString())}" +
                                    "'" + TabCitologia["CerLec2"].ToString() + "'," +
                                    "'" + TabCitologia["CodCerLec2"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabCitologia["FecCerLec2"].ToString())}" +
                                    "'" + TabCitologia["DiagDefi"].ToString() + "'," +
                                    "'" + TabCitologia["AnulToma"].ToString() + "'," +
                                    "'" + TabCitologia["RazAnulTo"].ToString() + "'," +
                                    "'" + TabCitologia["CodAnul"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabCitologia["FecAnul"].ToString())}" +
                                    "'" + TabCitologia["ConCiPa"].ToString() + "'," +
                                    "'" + TabCitologia["NoConCiPa"].ToString() + "'," +
                                    "'" + TabCitologia["CodRegis"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabCitologia["FecRegis"].ToString())}" +
                                    $"{Conexion.ValidarHoraNula(TabCitologia["HorRegis"].ToString())}" +
                                    "'" + TabCitologia["CodIPSLec"].ToString() + "'," +
                                    "'" + TabCitologia["PrefiCito"].ToString() + "')";

                                    Boolean RegisCito = Conexion.SqlInsert(Utils.SqlDatos);

                                    globalCanCitoFor += 1;


                                }
                                else
                                {
                                    Utils.SqlDatos = "UPDATE [BDSITOI].[dbo].[Datos basicos citologia] SET " +
                                    "NumRadi = '" + TabCitologia["NumRadi"].ToString() + "'," +
                                    $"FecRadi = {Conexion.ValidarFechaNula(TabCitologia["FecRadi"].ToString())} " + // Utils.SqlDatos += Conexion.ValidarFechaNula(TabCitologia["FecRadi"].ToString());
                                    "PrefiFac = '" + TabCitologia["PrefiFac"].ToString() + "'," +
                                    "NumFac = '" + TabCitologia["NumFac"].ToString() + "'," +
                                    "NumHisto = '" + TabCitologia["NumHisto"].ToString() + "'," +
                                    "TipUsua = '" + TabCitologia["TipUsua"].ToString() + "'," +
                                    "ValEdad = '" + TabCitologia["ValEdad"].ToString() + "'," +
                                    "UnidEdad = '" + TabCitologia["UnidEdad"].ToString() + "'," +
                                    "CodAPB = '" + TabCitologia["CodAPB"].ToString() + "'," +
                                    "ContraNum = '" + TabCitologia["ContraNum"].ToString() + "'," +
                                    "InsToma = '" + TabCitologia["InsToma"].ToString() + "'," +
                                    $"FecToma = {Conexion.ValidarFechaNula(TabCitologia["FecToma"].ToString())} " +
                                    "CodEsque = '" + TabCitologia["CodEsque"].ToString() + "'," +
                                    $"UltiRegla = {Conexion.ValidarFechaNula(TabCitologia["UltiRegla"].ToString())} " +
                                    "NumGesta = '" + TabCitologia["NumGesta"].ToString() + "'," +
                                    "NumPartos = '" + TabCitologia["NumPartos"].ToString() + "'," +
                                    $"UltiParto = {Conexion.ValidarFechaNula(TabCitologia["UltiParto"].ToString())} " +
                                    "NumAbor = '" + TabCitologia["NumAbor"].ToString() + "'," +
                                    "EdadRela = '" + TabCitologia["EdadRela"].ToString() + "'," +
                                    "Embarazada = '" + TabCitologia["Embarazada"].ToString() + "'," +
                                    $"UltiCito = {Conexion.ValidarFechaNula(TabCitologia["UltiCito"].ToString())} " +
                                    "ResCito = '" + TabCitologia["ResCito"].ToString() + "'," +
                                    "MetoPlani = '" + TabCitologia["MetoPlani"].ToString() + "'," +
                                    "CodPro = '" + TabCitologia["CodPro"].ToString() + "'," +
                                    $"FecPro = {Conexion.ValidarFechaNula(TabCitologia["FecPro"].ToString())} " +
                                    "CodAsCue = '" + TabCitologia["CodAsCue"].ToString() + "'," +
                                    "TipDocIden = '" + TabCitologia["TipDocIden"].ToString() + "'," +
                                    "IdenPerso = '" + TabCitologia["IdenPerso"].ToString() + "'," +
                                    "ObserToma = '" + TabCitologia["ObserToma"].ToString() + "'," +
                                    "CierreToma = '" + TabCitologia["CierreToma"].ToString() + "'," +
                                    "CodCierTo = '" + TabCitologia["CodCierTo"].ToString() + "'," +
                                    $"FeCierTo = {Conexion.ValidarFechaNula(TabCitologia["FeCierTo"].ToString())} " +
                                    $"FecRecLec1 = {Conexion.ValidarFechaNula(TabCitologia["FecRecLec1"].ToString())} " +
                                    "NumRadi2 = '" + TabCitologia["NumRadi2"].ToString() + "'," +
                                    "SatisMues = '" + TabCitologia["SatisMues"].ToString() + "'," +
                                    "DaClIna = '" + TabCitologia["DaClIna"].ToString() + "'," +
                                    "Hemo = '" + TabCitologia["Hemo"].ToString() + "'," +
                                    "CelEsca = '" + TabCitologia["CelEsca"].ToString() + "'," +
                                    "ExuDen = '" + TabCitologia["ExuDen"].ToString() + "'," +
                                    "PoPreCel = '" + TabCitologia["PoPreCel"].ToString() + "'," +
                                    "CateGen = '" + TabCitologia["CateGen"].ToString() + "'," +
                                    "Tricho = '" + TabCitologia["Tricho"].ToString() + "'," +
                                    "HonMorCa = '" + TabCitologia["HonMorCa"].ToString() + "'," +
                                    "CaCeHerSim = '" + TabCitologia["CaCeHerSim"].ToString() + "'," +
                                    "CaFloVaBa = '" + TabCitologia["CaFloVaBa"].ToString() + "'," +
                                    "Inflama = '" + TabCitologia["Inflama"].ToString() + "'," +
                                    "BacMorAcSP = '" + TabCitologia["BacMorAcSP"].ToString() + "'," +
                                    "RepaTipica = '" + TabCitologia["RepaTipica"].ToString() + "'," +
                                    "Radia = '" + TabCitologia["Radia"].ToString() + "'," +
                                    "DIU = '" + TabCitologia["DIU"].ToString() + "'," +
                                    "Atrofia = '" + TabCitologia["Atrofia"].ToString() + "'," +
                                    "PreCelEn = '" + TabCitologia["PreCelEn"].ToString() + "'," +
                                    "PreCelGla = '" + TabCitologia["PreCelGla"].ToString() + "'," +
                                    "EvaHormo = '" + TabCitologia["EvaHormo"].ToString() + "'," +
                                    "AscusInde = '" + TabCitologia["AscusInde"].ToString() + "'," +
                                    "AscusNoHG = '" + TabCitologia["AscusNoHG"].ToString() + "'," +
                                    "LEsInBaGra = '" + TabCitologia["LEsInBaGra"].ToString() + "'," +
                                    "LEsInAlGra = '" + TabCitologia["LEsInAlGra"].ToString() + "'," +
                                    "SosCarIn = '" + TabCitologia["SosCarIn"].ToString() + "'," +
                                    "CarCelEs = '" + TabCitologia["CarCelEs"].ToString() + "'," +
                                    "Atipicas = '" + TabCitologia["Atipicas"].ToString() + "'," +
                                    "AtiEndo = '" + TabCitologia["AtiEndo"].ToString() + "'," +
                                    "AtiEndome = '" + TabCitologia["AtiEndome"].ToString() + "'," +
                                    "AtiSinEs = '" + TabCitologia["AtiSinEs"].ToString() + "'," +
                                    "AtiFaNeo = '" + TabCitologia["AtiFaNeo"].ToString() + "'," +
                                    "AdeCarIn = '" + TabCitologia["AdeCarIn"].ToString() + "'," +
                                    "AdeNoCar = '" + TabCitologia["AdeNoCar"].ToString() + "'," +
                                    "OtrasNeo = '" + TabCitologia["OtrasNeo"].ToString() + "'," +
                                    "CelGlaEs = '" + TabCitologia["CelGlaEs"].ToString() + "'," +
                                    "ObserLec = '" + TabCitologia["ObserLec"].ToString() + "'," +
                                    "TDLec = '" + TabCitologia["TDLec"].ToString() + "'," +
                                    "IDPLec = '" + TabCitologia["IDPLec"].ToString() + "'," +
                                    "LecToma = '" + TabCitologia["LecToma"].ToString() + "'," +
                                    $"FecLec = {Conexion.ValidarFechaNula(TabCitologia["FecLec"].ToString())} " +
                                    "VezLec = '" + TabCitologia["VezLec"].ToString() + "'," +
                                    "CerLec = '" + TabCitologia["CerLec"].ToString() + "'," +
                                    "CodCerLec = '" + TabCitologia["CodCerLec"].ToString() + "'," +
                                    $"FecCerLec = {Conexion.ValidarFechaNula(TabCitologia["FecCerLec"].ToString())} " +
                                    "ParaPato = '" + TabCitologia["ParaPato"].ToString() + "'," +
                                    $"FecRecLec2 = {Conexion.ValidarFechaNula(TabCitologia["FecRecLec2"].ToString())} " +
                                    "NumRadi3 = '" + TabCitologia["NumRadi3"].ToString() + "'," +
                                    "TDLec2 = '" + TabCitologia["TDLec2"].ToString() + "'," +
                                    "IDPLec2 = '" + TabCitologia["IDPLec2"].ToString() + "'," +
                                    "LecToma2 = '" + TabCitologia["LecToma2"].ToString() + "'," +
                                    $"FecLec2 = {Conexion.ValidarFechaNula(TabCitologia["FecLec2"].ToString())} " +
                                    "CerLec2 = '" + TabCitologia["CerLec2"].ToString() + "'," +
                                    "CodCerLec2 = '" + TabCitologia["CodCerLec2"].ToString() + "'," +
                                    $"FecCerLec2 = {Conexion.ValidarFechaNula(TabCitologia["FecCerLec2"].ToString())} " +
                                    "DiagDefi = '" + TabCitologia["DiagDefi"].ToString() + "'," +
                                    "AnulToma = '" + TabCitologia["AnulToma"].ToString() + "'," +
                                    "RazAnulTo = '" + TabCitologia["RazAnulTo"].ToString() + "'," +
                                    "CodAnul = '" + TabCitologia["CodAnul"].ToString() + "'," +
                                    $"FecAnul = {Conexion.ValidarFechaNula(TabCitologia["FecAnul"].ToString())} " +
                                    "ConCiPa = '" + TabCitologia["ConCiPa"].ToString() + "'," +
                                    "NoConCiPa = '" + TabCitologia["NoConCiPa"].ToString() + "'," +
                                    "CodRegis = '" + TabCitologia["CodRegis"].ToString() + "'," +
                                    $"FecRegis = {Conexion.ValidarFechaNula(TabCitologia["FecRegis"].ToString())} " +
                                    $"HorRegis = {Conexion.ValidarHoraNula(TabCitologia["HorRegis"].ToString())}" +
                                    "CodIPSLec = '" + TabCitologia["CodIPSLec"].ToString() + "'," +
                                    "PrefiCito = '" + TabCitologia["PrefiCito"].ToString() + "'" +
                                    "WHERE (CodFiCito = '" + CodBusCito + "') ";

                                    Boolean ActCito = Conexion.SQLUpdate(Utils.SqlDatos);

                                    globalCanCitoFormExis += 1;

                                }//'Final de TabHistorCen.BOF
                                TabCitoCen.Close();


                            }//Using TabHi


                            contador += 1;

                            ExportarHistoCito.ReportProgress(contador);


                        }//Fin While


                    }//if(TabCitologia.HasRows == false)

                    TabCitologia.Close();

                }//Using
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "después de hacer click sobre el botón exportar" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                ProgressBar.Minimum = 0;
                ProgressBar.Maximum = 1;
                ProgressBar.Value = 0;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExportarHistoCito.CancelAsync();
            }
        }

        private void ExportarHistoCito_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {

                if (ExportarHistoCito.CancellationPending == false)
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

        private void ExportarHistoCito_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Utils.Titulo01 = "Control para exportar datos";
                Utils.Informa = "El proceso ha terminado satisfactoriamente " + "\r";
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);

                ProgressBar.Minimum = 0;
                ProgressBar.Maximum = 1;
                ProgressBar.Value = 0;

                LblCantidad.Text = "0";
                LblTotal.Text = "0";

                TxtCanCitoFor.Text = globalCanCitoFor.ToString();
                TxtCanCitoFormExis.Text = globalCanCitoFormExis.ToString();


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


                    ExportarHistoCito.WorkerSupportsCancellation = true;
                    ExportarHistoCito.CancelAsync();


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

        private void LblExportar_Click(object sender, EventArgs e)
        {

        }
    }
}
