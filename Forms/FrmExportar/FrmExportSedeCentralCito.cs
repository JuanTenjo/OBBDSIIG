﻿using System;
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




        private void BtnBuscarPacientes_Click(object sender, EventArgs e)
        {
            try
            {
                string CodConseFacE = "", SqlCito = "", SqlCitoCen = "", CodBusCito = "", SqlCuenConsu = "", SqlFacCentra = "", NumResFac = "", TexResFacElec = "", UsaRegis, FacPorta = "", FunConFac = "", PrefiFacE = "", NumFacElec = "", SqlResolFactur = "";
                int SigoProcFac = 0, CantiFacElec = 0, ContiPro = 0;
                double TolFacEx = 0;
                string HiBusCue, CuenBusCue, FacBusCue, TiDGr, NumDgr, SqlPaciCentra, SqlPaciCentraCC, SqlCuenCen;
                SqlDataReader TabResolFactur;
                CodConseFacE = "51"; //'*********************** Codigo asignado para la generación de consecutivos de facturas electronicas en este sistema ************************

                SqlDataReader TabConsumos;
                string SqlConsumos;

                double IteNumCon;
                string SqlConsuCen;

                SqlDataReader TabCuenConsu;
                DateTime Fecha2 = DateTime.Now;
                string Fecha = Fecha2.ToString("yyyy-MM-dd");


                //'Revisamos si ya empieza con la facturación electronica
                SigoProcFac = 0;
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

                string FecIniPro = Convert.ToString(DateInicial.Value.ToString("yyy-MM-dd"));
                string FecFinPro = Convert.ToString(DateFinal.Value.ToString("yyy-MM-dd"));

                string PfiCen = TxtPrefiCenFor.Text;
                string PfiPor = TxtPrefiPorFor.Text;


                Utils.Informa = "¿Usted desea iniciar el proceso de exportación" + "\r";
                Utils.Informa += "todas las citologias en la instancia del" + "\r";
                Utils.Informa += "portatil a la instancia del servidor central.?" + "\r";
                Utils.Informa += "Fecha Inicial: " + FecIniPro + "\r";
                Utils.Informa += "Fecha Final: " + FecFinPro;
                var res = MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if(res == DialogResult.Yes)
                {
                    SqlCito = "SELECT * FROM [BDSITOI].[dbo].[Datos basicos citologia] ";
                    SqlCito += "WHERE ([Datos basicos citologia].PrefiCito = N'" + PfiPor + "') AND";
                    SqlCito += "([Datos basicos citologia].CierreToma = 'True' ) AND ";
                    SqlCito += "([Datos basicos citologia].FecRadi >= CONVERT(DATETIME, '" + FecIniPro + "', 102)) AND";
                    SqlCito += "([Datos basicos citologia].FecRadi <= CONVERT(DATETIME, '" + FecFinPro + "', 102))";


                    ConectarPortatil();

                    SqlDataReader TabCitologia;

                    using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                    {
                        SqlCommand command = new SqlCommand(SqlCito, connection);
                        command.Connection.Open();
                        TabCitologia = command.ExecuteReader();

                        if(TabCitologia.HasRows == false)
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

                                    if(TabCitoCen.HasRows == false)
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
                                        $"{ValidarFechaNula(TabCitologia["FecRadi"].ToString())}" +
                                        "'" + TabCitologia["PrefiFac"].ToString() + "'," +
                                        "'" + TabCitologia["NumFac"].ToString() + "'," +
                                        "'" + TabCitologia["NumHisto"].ToString() + "'," +
                                        "'" + TabCitologia["TipUsua"].ToString() + "'," +
                                        "'" + TabCitologia["ValEdad"].ToString() + "'," +
                                        "'" + TabCitologia["UnidEdad"].ToString() + "'," +
                                        "'" + TabCitologia["CodAPB"].ToString() + "'," +
                                        "'" + TabCitologia["ContraNum"].ToString() + "'," +
                                        "'" + TabCitologia["InsToma"].ToString() + "'," +
                                        $"{ValidarFechaNula(TabCitologia["FecToma"].ToString())}" +
                                        "'" + TabCitologia["CodEsque"].ToString() + "'," +
                                        $"{ValidarFechaNula(TabCitologia["UltiRegla"].ToString())}" +
                                        "'" + TabCitologia["NumGesta"].ToString() + "'," +
                                        "'" + TabCitologia["NumPartos"].ToString() + "'," +
                                        $"{ValidarFechaNula(TabCitologia["UltiParto"].ToString())}" +
                                        "'" + TabCitologia["NumAbor"].ToString() + "'," +
                                        "'" + TabCitologia["EdadRela"].ToString() + "'," +
                                        "'" + TabCitologia["Embarazada"].ToString() + "'," +
                                       $"{ValidarFechaNula(TabCitologia["UltiCito"].ToString())}" +
                                        "'" + TabCitologia["ResCito"].ToString() + "'," +
                                        "'" + TabCitologia["MetoPlani"].ToString() + "'," +
                                        "'" + TabCitologia["CodPro"].ToString() + "'," +
                                        $"{ValidarFechaNula(TabCitologia["FecPro"].ToString())}" +
                                        "'" + TabCitologia["CodAsCue"].ToString() + "'," +
                                        "'" + TabCitologia["TipDocIden"].ToString() + "'," +
                                        "'" + TabCitologia["IdenPerso"].ToString() + "'," +
                                        "'" + TabCitologia["ObserToma"].ToString() + "'," +
                                        "'" + TabCitologia["CierreToma"].ToString() + "'," +
                                        "'" + TabCitologia["CodCierTo"].ToString() + "'," +
                                        $"{ValidarFechaNula(TabCitologia["FeCierTo"].ToString())}" +
                                        $"{ValidarFechaNula(TabCitologia["FecRecLec1"].ToString())}" +
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
                                        $"{ValidarFechaNula(TabCitologia["FecLec"].ToString())}" +
                                        "'" + TabCitologia["VezLec"].ToString() + "'," +
                                        "'" + TabCitologia["CerLec"].ToString() + "'," +
                                        "'" + TabCitologia["CodCerLec"].ToString() + "'," +
                                        $"{ValidarFechaNula(TabCitologia["FecCerLec"].ToString())}" +
                                        "'" + TabCitologia["ParaPato"].ToString() + "'," +
                                        $"{ValidarFechaNula(TabCitologia["FecRecLec2"].ToString())}" +
                                        "'" + TabCitologia["NumRadi3"].ToString() + "'," +
                                        "'" + TabCitologia["TDLec2"].ToString() + "'," +
                                        "'" + TabCitologia["IDPLec2"].ToString() + "'," +
                                        "'" + TabCitologia["LecToma2"].ToString() + "'," +
                                        $"{ValidarFechaNula(TabCitologia["FecLec2"].ToString())}" +
                                        "'" + TabCitologia["CerLec2"].ToString() + "'," +
                                        "'" + TabCitologia["CodCerLec2"].ToString() + "'," +
                                        $"{ValidarFechaNula(TabCitologia["FecCerLec2"].ToString())}" +
                                        "'" + TabCitologia["DiagDefi"].ToString() + "'," +
                                        "'" + TabCitologia["AnulToma"].ToString() + "'," +
                                        "'" + TabCitologia["RazAnulTo"].ToString() + "'," +
                                        "'" + TabCitologia["CodAnul"].ToString() + "'," +
                                        $"{ValidarFechaNula(TabCitologia["FecAnul"].ToString())}" +
                                        "'" + TabCitologia["ConCiPa"].ToString() + "'," +
                                        "'" + TabCitologia["NoConCiPa"].ToString() + "'," +
                                        "'" + TabCitologia["CodRegis"].ToString() + "'," +
                                        $"{ValidarFechaNula(TabCitologia["FecRegis"].ToString())}" + 
                                        "CONVERT(DATETIME,'" + TabCitologia["HorRegis"].ToString() + "',8)," +
                                        "'" + TabCitologia["CodIPSLec"].ToString() + "'," +
                                        "'" + TabCitologia["PrefiCito"].ToString() + "')";

                                        Boolean RegisCito = Conexion.SqlInsert(Utils.SqlDatos);


                                    }
                                    else
                                    {
                                        Utils.SqlDatos = "UPDATE [BDSITOI].[dbo].[Datos basicos citologia] SET " +
                                                        "NumRadi = '" + TabCitologia["NumRadi"].ToString() + "'," +
                                                        $"FecRadi = {ValidarFechaNula(TabCitologia["FecRadi"].ToString())} "+ // Utils.SqlDatos += ValidarFechaNula(TabCitologia["FecRadi"].ToString());
                                                        "PrefiFac = '" + TabCitologia["PrefiFac"].ToString() + "'," + 
                                                        "NumFac = '" + TabCitologia["NumFac"].ToString() + "'," + 
                                                        "NumHisto = '" + TabCitologia["NumHisto"].ToString() + "'," + 
                                                        "TipUsua = '" + TabCitologia["TipUsua"].ToString() + "'," + 
                                                        "ValEdad = '" + TabCitologia["ValEdad"].ToString() + "'," + 
                                                        "UnidEdad = '" + TabCitologia["UnidEdad"].ToString() + "'," + 
                                                        "CodAPB = '" + TabCitologia["CodAPB"].ToString() + "'," + 
                                                        "ContraNum = '" + TabCitologia["ContraNum"].ToString() + "'," + 
                                                        "InsToma = '" + TabCitologia["InsToma"].ToString() + "'," + 
                                                        $"FecToma = {ValidarFechaNula(TabCitologia["FecToma"].ToString())} " +
                                                        "CodEsque = '" + TabCitologia["CodEsque"].ToString() + "'," +
                                                        $"UltiRegla = {ValidarFechaNula(TabCitologia["UltiRegla"].ToString())} " +
                                                        "NumGesta = '" + TabCitologia["NumGesta"].ToString() + "'," + 
                                                        "NumPartos = '" + TabCitologia["NumPartos"].ToString() + "'," + 
                                                        $"UltiParto = {ValidarFechaNula(TabCitologia["UltiParto"].ToString())} " +
                                                        "NumAbor = '" + TabCitologia["NumAbor"].ToString() + "'," + 
                                                        "EdadRela = '" + TabCitologia["EdadRela"].ToString() + "'," + 
                                                        "Embarazada = '" + TabCitologia["Embarazada"].ToString() + "'," + 
                                                        $"UltiCito = {ValidarFechaNula(TabCitologia["UltiCito"].ToString())} " +
                                                        "ResCito = '" + TabCitologia["ResCito"].ToString() + "'," + 
                                                        "MetoPlani = '" + TabCitologia["MetoPlani"].ToString() + "'," + 
                                                        "CodPro = '" + TabCitologia["CodPro"].ToString() + "'," +
                                                        $"FecPro = {ValidarFechaNula(TabCitologia["FecPro"].ToString())} " +
                                                        "CodAsCue = '" + TabCitologia["CodAsCue"].ToString() + "'," + 
                                                        "TipDocIden = '" + TabCitologia["TipDocIden"].ToString() + "'," + 
                                                        "IdenPerso = '" + TabCitologia["IdenPerso"].ToString() + "'," + 
                                                        "ObserToma = '" + TabCitologia["ObserToma"].ToString() + "'," + 
                                                        "CierreToma = '" + TabCitologia["CierreToma"].ToString() + "'," + 
                                                        "CodCierTo = '" + TabCitologia["CodCierTo"].ToString() + "'," +
                                                        $"FeCierTo = {ValidarFechaNula(TabCitologia["FeCierTo"].ToString())} " +
                                                        $"FecRecLec1 = {ValidarFechaNula(TabCitologia["FecRecLec1"].ToString())} " +
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
                                                        $"FecLec = {ValidarFechaNula(TabCitologia["FecLec"].ToString())} " +
                                                        "VezLec = '" + TabCitologia["VezLec"].ToString() + "'," + 
                                                        "CerLec = '" + TabCitologia["CerLec"].ToString() + "'," + 
                                                        "CodCerLec = '" + TabCitologia["CodCerLec"].ToString() + "'," +
                                                        $"FecCerLec = {ValidarFechaNula(TabCitologia["FecCerLec"].ToString())} " +
                                                        "ParaPato = '" + TabCitologia["ParaPato"].ToString() + "'," +
                                                        $"FecRecLec2 = {ValidarFechaNula(TabCitologia["FecRecLec2"].ToString())} " +
                                                        "NumRadi3 = '" + TabCitologia["NumRadi3"].ToString() + "'," + 
                                                        "TDLec2 = '" + TabCitologia["TDLec2"].ToString() + "'," + 
                                                        "IDPLec2 = '" + TabCitologia["IDPLec2"].ToString() + "'," + 
                                                        "LecToma2 = '" + TabCitologia["LecToma2"].ToString() + "'," +
                                                        $"FecLec2 = {ValidarFechaNula(TabCitologia["FecLec2"].ToString())} " +
                                                        "CerLec2 = '" + TabCitologia["CerLec2"].ToString() + "'," + 
                                                        "CodCerLec2 = '" + TabCitologia["CodCerLec2"].ToString() + "'," +
                                                        $"FecCerLec2 = {ValidarFechaNula(TabCitologia["FecCerLec2"].ToString())} " +
                                                        "DiagDefi = '" + TabCitologia["DiagDefi"].ToString() + "'," + 
                                                        "AnulToma = '" + TabCitologia["AnulToma"].ToString() + "'," + 
                                                        "RazAnulTo = '" + TabCitologia["RazAnulTo"].ToString() + "'," + 
                                                        "CodAnul = '" + TabCitologia["CodAnul"].ToString() + "'," +
                                                        $"FecAnul = {ValidarFechaNula(TabCitologia["FecAnul"].ToString())} " +
                                                        "ConCiPa = '" + TabCitologia["ConCiPa"].ToString() + "'," + 
                                                        "NoConCiPa = '" + TabCitologia["NoConCiPa"].ToString() + "'," + 
                                                        "CodRegis = '" + TabCitologia["CodRegis"].ToString() + "'," +
                                                        $"FecRegis = {ValidarFechaNula(TabCitologia["FecRegis"].ToString())} " +
                                                        "HorRegis = '" + Convert.ToDateTime(TabCitologia["HorRegis"]).ToString("hh:ss:mm") + "'," +
                                                        "CodIPSLec = '" + TabCitologia["CodIPSLec"].ToString() + "'," + 
                                                        "PrefiCito = '" + TabCitologia["PrefiCito"].ToString() + "'" +
                                                        "WHERE (CodFiCito = '" + CodBusCito + "') ";

                                        Boolean ActCito = Conexion.SQLUpdate(Utils.SqlDatos);

                                    }//'Final de TabHistorCen.BOF
                                    TabCitoCen.Close();
                                    int con = Convert.ToInt32(TxtCanCitoFormExis.Text) + 1;

                                    TxtCanCitoFormExis.Text = con.ToString();

                                }//Using TabHi
                            }//Fin While


                            Utils.Informa = "El proceso ha terminado satisfactoriamente";
                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);


                        }//if(TabCitologia.HasRows == false)
                        TabCitologia.Close();

                    }//Using
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "después de hacer click sobre el botón exportar" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}