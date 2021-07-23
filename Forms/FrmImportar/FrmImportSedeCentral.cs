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
    public partial class FrmImportSedeCentral : Form
    {
        public FrmImportSedeCentral()
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

        private string ValidarFechaNula(string Fecha)
        {
            string ValidarFecha = null;

            ValidarFecha = string.IsNullOrWhiteSpace(Fecha) ? "null" + "," : "CONVERT(DATETIME,'" + Convert.ToDateTime(Fecha).ToString("yyyy-MM-dd") + "',102),";

            return ValidarFecha;

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



        private void FrmImportSedeCentral_Load(object sender, EventArgs e)
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
                Utils.Informa += "al cargar el formulario FrmImportSedeCentral" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscarPacientes_Click(object sender, EventArgs e)
        {
            try
            {


                string CodConseFacE = "", SqlCuenConsu = "", SqlFacCentra = "", NumResFac = "", TexResFacElec = "", UsaRegis, FacPorta = "", FunConFac = "", PrefiFacE = "", NumFacElec = "", SqlResolFactur = "";
                int SigoProcFac = 0, CantiFacElec = 0, ContiPro = 0;
                double TolFacEx = 0;
                string HiBusCue, CuenBusCue, FacBusCue, TipDocPacCuen, NumDocPacCuen, SqlPaciCentra, SqlPaciCentraCC, SqlCuenCen;
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


                Utils.Informa = "¿Usted desea iniciar el proceso de importación" + "\r";
                Utils.Informa += "de todo lo facturado en la instancia del" + "\r";
                Utils.Informa += "servidor central a la instnacia del portatil.?" + "\r";
                Utils.Informa += "Fecha Inicial: " + FecIniPro + "\r";
                Utils.Informa += "Fecha Final: " + FecFinPro;
                var res = MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    //Revisamos si la entidad tiene facturación activa, para que las facturas eventos les asigne el consecutivo de la central de facturación electronica
                    //**************************** Se crea el 13 de octubre de 2020 ***********************************************}

                    string FechaHoy = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));


                    SqlCuenConsu = "SELECT [Datos cuentas de consumos].*, " +
                    "[Datos de las facturas realizadas].*, [Datos del Paciente].*, " +
                    //*********************** LAS SIGUINETES LINEAS LAS AGREGA EL 03 DE SEPTIEMBRE HERNNADO PARA SOLUCIONAR EL PROBLEMA DEL CÍODIGO DE QUIEN FACTURA ************

                    "([Datos cuentas de consumos].CodiRegis) as CuentaRegistra, ([Datos cuentas de consumos].FecRegis) as CuenFecRegis, " +
                    "([Datos cuentas de consumos].CodiModi) as CuentaModifica, ([Datos cuentas de consumos].FecModi) as CuenFecModi, " +
                    "([Datos de las facturas realizadas].CodiRegis) as FacturaRegistra, ([Datos de las facturas realizadas].FecRegis) as FacFecRegis, " +
                    "([Datos de las facturas realizadas].CodiModi) as FacturaModifica, ([Datos de las facturas realizadas].FecModi) as FacFecModi, " +
                    "([Datos del Paciente].CodiRegis) as PacienteRegistra, ([Datos del Paciente].FecRegis) as PaciFecRegis, " +
                    "([Datos del Paciente].CodiModi) as PacienteModifica, ([Datos del Paciente].FecModi) as PaciFecModi, " +

                    //*********************** LAS SIGUINETES LINEAS LAS AGREGA EL 19 DE NOVIEMBRE HERNANDO PARA SOLUCIONAR EL PROBLEMA DEL TIPO DE CUENTA CON EL MISMO NOMBRE  ************

                    "([Datos cuentas de consumos].TipoCuenta) as TipoCuentaFactura " +

                   "FROM  [ACDATOXPSQL].[dbo].[Datos cuentas de consumos] INNER JOIN [Datos de las facturas realizadas] ON " +
                   " [ACDATOXPSQL].[dbo].[Datos cuentas de consumos].CuenNum = [Datos de las facturas realizadas].NumCuenFac INNER JOIN " +
                   " [ACDATOXPSQL].[dbo].[Datos del Paciente] ON [Datos cuentas de consumos].HistoNum = [Datos del Paciente].HistorPaci " +

                   "WHERE [Datos de las facturas realizadas].FechaFac >= CONVERT(DATETIME, '" + FecIniPro + "', 102) AND " +
                   "[Datos de las facturas realizadas].FechaFac <= CONVERT(DATETIME, '" + FecFinPro + "', 102)";

                    ConectarCentral();

                    using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                    {
                        SqlCommand command = new SqlCommand(SqlCuenConsu, connection);
                        command.Connection.Open();
                        TabCuenConsu = command.ExecuteReader();

                        if (TabCuenConsu.HasRows == false)
                        {
                            Utils.Informa = "Lo siento pero en el rango de fecha " + "\r";
                            Utils.Informa += "digitado no existen datos para exportar." + "\r";
                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {

                            while (TabCuenConsu.Read())
                            {
                                TolFacEx += 1;

                                //'Revisamos si el número de historia existe

                                HiBusCue = TabCuenConsu["HistoNum"].ToString();
                                CuenBusCue = TabCuenConsu["CuenNum"].ToString();
                                FacBusCue = TabCuenConsu["NumFactura"].ToString();
                                TipDocPacCuen = TabCuenConsu["TipoIden"].ToString();
                                NumDocPacCuen = TabCuenConsu["NumIden"].ToString();
                                ContiPro = 0;

                                SqlPaciCentra = "SELECT * FROM [ACDATOXPSQL].[dbo].[Datos del Paciente] ";
                                SqlPaciCentra += "Where HistorPaci = N'" + HiBusCue + "'";


                                SqlDataReader TabPaciCentra;


                                ConectarPortatil();

                                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                                {
                                    SqlCommand command2 = new SqlCommand(SqlPaciCentra, connection2);
                                    command2.Connection.Open();
                                    TabPaciCentra = command2.ExecuteReader();

                                    if (TabPaciCentra.HasRows == false)
                                    {
                                        //Revisamos si existe por el docuemnto, porque cambiaron la historia solamente, posiblemente
                                        //'Debe crear la historia en la base de datos central

                                        SqlPaciCentraCC = "SELECT * FROM [ACDATOXPSQL].[dbo].[Datos del Paciente] ";
                                        SqlPaciCentraCC += "Where TipoIden = N'" + TipDocPacCuen + "'  and NumIden = N'" + NumDocPacCuen + "' ";

                                        SqlDataReader TabPaciCentraCC;


                                        ConectarPortatil();
                                        using (SqlConnection connection3 = new SqlConnection(Conexion.conexionSQL))
                                        {
                                            SqlCommand command3 = new SqlCommand(SqlPaciCentraCC, connection3);
                                            command3.Connection.Open();
                                            TabPaciCentraCC = command3.ExecuteReader();

                                            if (TabPaciCentraCC.HasRows == false)
                                            {
                                                //gregue tranaquilamente

                                                Utils.SqlDatos = "INSERT INTO [ACDATOXPSQL].[dbo].[Datos del Paciente]" +
                                                "(" +
                                                "HistorPaci," + 
                                                "TipoIden," + 
                                                "NumIden," + 
                                                "Nombre1," + 
                                                "Nombre2," + 
                                                "Apellido1," + 
                                                "Apellido2," + 
                                                "FechaNaci," + 
                                                "HoraNaci," + 
                                                "LugarNace," + 
                                                "SemGesta," + 
                                                "CodPaisNace," + 
                                                "CodDptoNace," + 
                                                "CodCiuNace," + 
                                                "EstadoCivil," + 
                                                "CodDpto," + 
                                                "CodMuni," + 
                                                "BarrioVive," + 
                                                "DirecResi," + 
                                                "TelResi," + 
                                                "ZonaResiden," + 
                                                "FechaApertura," + 
                                                "Observaciones," + 
                                                "Sexo," + 
                                                "TipoUsar," + 
                                                "TipoAfiliado," + 
                                                "NumAfilia," + 
                                                "PolizaNum," + 
                                                "EstraNum," + 
                                                "GrupoEtni," + 
                                                "NumContra," + 
                                                "TipoCuenta," +  //Cambio el 19 de noviembre
                                                "CausaRemite," + 
                                                "NombresRespon," + 
                                                "Apellido1Respon," + 
                                                "Apellido2Respon," + 
                                                "TipoDocuRespon," + 
                                                "CedulaRespon," + 
                                                "Parentesco," + 
                                                "DireccPare," + 
                                                "TelefonoPare," + 
                                                "CiudadPare," + 
                                                "DptoRespon," + 
                                                "NombrePadre," + 
                                                "Apellido1Padre," + 
                                                "Apellido2Padre," + 
                                                "VivePadre," + 
                                                "TipoDocuPadre," + 
                                                "CedulaPadre," + 
                                                "NombreMadre," + 
                                                "Apellido1Madre," + 
                                                "Apellido2Madre," + 
                                                "ViveMadre," + 
                                                "TipoDocuMadre," + 
                                                "CedulaMadre," + 
                                                "NombreEmpresa," + 
                                                "Ocupacion," + 
                                                "CiudadEmpresa," + 
                                                "DireccTrabaja," + 
                                                "TeleEmpresa," + 
                                                "CodSeSocial," + 
                                                "CodiAdmin," + 
                                                "DptoRemite," + 
                                                "MunicipioRemite," + 
                                                "IPSRemite," + 
                                                "RemiNumero," + 
                                                "FechaRemision," + 
                                                "FechaVence," + 
                                                "CubreRemision," + 
                                                "EspecialRemite," + 
                                                "DxRemite," + 
                                                "CoMediAten," + 
                                                "MotivoConsul," + 
                                                "FechaEntrada," + 
                                                "HoraEntrada," + 
                                                "FecUltima," + 
                                                "ArchivoViene," + 
                                                "Muerto," + 
                                                "PreInscripcion," + 
                                                "CoberSalud," + 
                                                "Huella1," + 
                                                "Huella2," + 
                                                "Huella," + 
                                                "TelCelular," + 
                                                "NomEmeal," + 
                                                "NivEducUs," + 
                                                "DebeDere," + 
                                                "UltiPeso," + 
                                                "FecPeso," + 
                                                "CodRgPes," + 
                                                "UltiTalla," + 
                                                "FecTalla," + 
                                                "CodRgTal," + 
                                                "CodPrefi," +  //Grabe el código del portatil, mientras se agega esto en el código de creación de historias clinicas
                                                "CodiRegis," +
                                                "FecRegis," +
                                                "CodiModi," + 
                                                "FecModi," + 
                                                //***** CAMPOS AGREGADOS CON BASE DATOS DE ALGECIRAS Y GARZON *****
                                                "NomDepenLabo," + 
                                                "CodARLLabo," + 
                                                "CodAFPLabo," + 
                                                //***** CAMPOS AGREGADOS CON BASE DATOS DE SAN AGUSTIN *****
                                                "Discapacidad," + 
                                                "NomCargoLabo," + 
                                                "GruSangui," + 
                                                "RhPaciente" + 
                                                ")" +
                                                "VALUES" +
                                                "(" +
                                                "'" + TabCuenConsu["HistorPaci"].ToString() + "'," +
                                                "'" + TabCuenConsu["TipoIden"].ToString() + "'," +
                                                "'" + TabCuenConsu["NumIden"].ToString() + "'," +
                                                "'" + TabCuenConsu["Nombre1"].ToString() + "'," +
                                                "'" + TabCuenConsu["Nombre2"].ToString() + "'," +
                                                "'" + TabCuenConsu["Apellido1"].ToString() + "'," +
                                                "'" + TabCuenConsu["Apellido2"].ToString() + "'," +
                                                $"{ValidarFechaNula(TabCuenConsu["FechaNaci"].ToString())}" +
                                                "CONVERT(DATETIME,'" + TabCuenConsu["HoraNaci"].ToString() + "',8)," +
                                                "'" + TabCuenConsu["LugarNace"].ToString() + "'," +
                                                "'" + TabCuenConsu["SemGesta"].ToString() + "'," +
                                                "'" + TabCuenConsu["CodPaisNace"].ToString() + "'," +
                                                "'" + TabCuenConsu["CodDptoNace"].ToString() + "'," +
                                                "'" + TabCuenConsu["CodCiuNace"].ToString() + "'," +
                                                "'" + TabCuenConsu["EstadoCivil"].ToString() + "'," +
                                                "'" + TabCuenConsu["CodDpto"].ToString() + "'," +
                                                "'" + TabCuenConsu["CodMuni"].ToString() + "'," +
                                                "'" + TabCuenConsu["BarrioVive"].ToString() + "'," +
                                                "'" + TabCuenConsu["DirecResi"].ToString() + "'," +
                                                "'" + TabCuenConsu["TelResi"].ToString() + "'," +
                                                "'" + TabCuenConsu["ZonaResiden"].ToString() + "'," +
                                                $"{ValidarFechaNula(TabCuenConsu["FechaApertura"].ToString())}" +
                                                "'" + TabCuenConsu["Observaciones"].ToString() + "'," +
                                                "'" + TabCuenConsu["Sexo"].ToString() + "'," +
                                                "'" + TabCuenConsu["TipoUsar"].ToString() + "'," +
                                                "'" + TabCuenConsu["TipoAfiliado"].ToString() + "'," +
                                                "'" + TabCuenConsu["NumAfilia"].ToString() + "'," +
                                                "'" + TabCuenConsu["PolizaNum"].ToString() + "'," +
                                                "'" + TabCuenConsu["EstraNum"].ToString() + "'," +
                                                "'" + TabCuenConsu["GrupoEtni"].ToString() + "'," +
                                                "'" + TabCuenConsu["NumContra"].ToString() + "'," +
                                                "'" + TabCuenConsu["TipoCuenta "].ToString() + "'," +
                                                "'" + TabCuenConsu["CausaRemite"].ToString() + "'," +
                                                "'" + TabCuenConsu["NombresRespon"].ToString() + "'," +
                                                "'" + TabCuenConsu["Apellido1Respon"].ToString() + "'," +
                                                "'" + TabCuenConsu["Apellido2Respon"].ToString() + "'," +
                                                "'" + TabCuenConsu["TipoDocuRespon"].ToString() + "'," +
                                                "'" + TabCuenConsu["CedulaRespon"].ToString() + "'," +
                                                "'" + TabCuenConsu["Parentesco"].ToString() + "'," +
                                                "'" + TabCuenConsu["DireccPare"].ToString() + "'," +
                                                "'" + TabCuenConsu["TelefonoPare"].ToString() + "'," +
                                                "'" + TabCuenConsu["CiudadPare"].ToString() + "'," +
                                                "'" + TabCuenConsu["DptoRespon"].ToString() + "'," +
                                                "'" + TabCuenConsu["NombrePadre"].ToString() + "'," +
                                                "'" + TabCuenConsu["Apellido1Padre"].ToString() + "'," +
                                                "'" + TabCuenConsu["Apellido2Padre"].ToString() + "'," +
                                                "'" + TabCuenConsu["VivePadre"].ToString() + "'," +
                                                "'" + TabCuenConsu["TipoDocuPadre"].ToString() + "'," +
                                                "'" + TabCuenConsu["CedulaPadre"].ToString() + "'," +
                                                "'" + TabCuenConsu["NombreMadre"].ToString() + "'," +
                                                "'" + TabCuenConsu["Apellido1Madre"].ToString() + "'," +
                                                "'" + TabCuenConsu["Apellido2Madre"].ToString() + "'," +
                                                "'" + TabCuenConsu["ViveMadre"].ToString() + "'," +
                                                "'" + TabCuenConsu["TipoDocuMadre"].ToString() + "'," +
                                                "'" + TabCuenConsu["CedulaMadre"].ToString() + "'," +
                                                "'" + TabCuenConsu["NombreEmpresa"].ToString() + "'," +
                                                "'" + TabCuenConsu["Ocupacion"].ToString() + "'," +
                                                "'" + TabCuenConsu["CiudadEmpresa"].ToString() + "'," +
                                                "'" + TabCuenConsu["DireccTrabaja"].ToString() + "'," +
                                                "'" + TabCuenConsu["TeleEmpresa"].ToString() + "'," +
                                                "'" + TabCuenConsu["CodSeSocial"].ToString() + "'," +
                                                "'" + TabCuenConsu["CodiAdmin"].ToString() + "'," +
                                                "'" + TabCuenConsu["DptoRemite"].ToString() + "'," +
                                                "'" + TabCuenConsu["MunicipioRemite"].ToString() + "'," +
                                                "'" + TabCuenConsu["IPSRemite"].ToString() + "'," +
                                                "'" + TabCuenConsu["RemiNumero"].ToString() + "'," +
                                                $"{ValidarFechaNula(TabCuenConsu["FechaRemision"].ToString())}" +
                                                $"{ValidarFechaNula(TabCuenConsu["FechaVence"].ToString())}" +
                                                "'" + TabCuenConsu["CubreRemision"].ToString() + "'," +
                                                "'" + TabCuenConsu["EspecialRemite"].ToString() + "'," +
                                                "'" + TabCuenConsu["DxRemite"].ToString() + "'," +
                                                "'" + TabCuenConsu["CoMediAten"].ToString() + "'," +
                                                "'" + TabCuenConsu["MotivoConsul"].ToString() + "'," +
                                                $"{ValidarFechaNula(TabCuenConsu["FechaEntrada"].ToString())}" +
                                                "CONVERT(DATETIME,'" + TabCuenConsu["HoraEntrada"].ToString() + "',8)," +
                                                $"{ValidarFechaNula(TabCuenConsu["FecUltima"].ToString())}" +
                                                "'" + TabCuenConsu["ArchivoViene"].ToString() + "'," +
                                                "'" + TabCuenConsu["Muerto"].ToString() + "'," +
                                                "'" + TabCuenConsu["PreInscripcion"].ToString() + "'," +
                                                "'" + TabCuenConsu["CoberSalud"].ToString() + "'," +
                                                "'" + TabCuenConsu["Huella1"].ToString() + "'," +
                                                "'" + TabCuenConsu["Huella2"].ToString() + "'," +
                                                "'" + TabCuenConsu["Huella"].ToString() + "'," +
                                                "'" + TabCuenConsu["TelCelular"].ToString() + "'," +
                                                "'" + TabCuenConsu["NomEmeal"].ToString() + "'," +
                                                "'" + TabCuenConsu["NivEducUs"].ToString() + "'," +
                                                "'" + TabCuenConsu["DebeDere"].ToString() + "'," +
                                                "'" + TabCuenConsu["UltiPeso"].ToString() + "'," +
                                                $"{ValidarFechaNula(TabCuenConsu["FecPeso"].ToString())}" +
                                                "'" + TabCuenConsu["CodRgPes"].ToString() + "'," +
                                                "'" + TabCuenConsu["UltiTalla"].ToString() + "'," +
                                                $"{ValidarFechaNula(TabCuenConsu["FecTalla"].ToString())}" +
                                                "'" + TabCuenConsu["CodRgTal"].ToString() + "'," +
                                                "'" + TabCuenConsu["CodPrefi "].ToString() + "'," +
                                                "'" + TabCuenConsu["CodiRegis"].ToString() + "'," +
                                                $"{ValidarFechaNula(TabCuenConsu["FecRegis"].ToString())}" +
                                                "'" + TabCuenConsu["CodiModi"].ToString() + "'," +
                                                $"{ValidarFechaNula(TabCuenConsu["FecModi"].ToString())}" +
                                                "'" + TabCuenConsu["NomDepenLabo"].ToString() + "'," +
                                                "'" + TabCuenConsu["CodARLLabo"].ToString() + "'," +
                                                "'" + TabCuenConsu["CodAFPLabo"].ToString() + "'," +
                                                "'" + TabCuenConsu["Discapacidad"].ToString() + "'," +
                                                "'" + TabCuenConsu["NomCargoLabo"].ToString() + "'," +
                                                "'" + TabCuenConsu["GruSangui"].ToString() + "'," +
                                                "'" + TabCuenConsu["RhPaciente "].ToString() + "'" +
                                                ")";


                                                //'***** CAMPOS AGREGADOS CON BASE DATOS DE GARZON *****
                                                //TabPaciCentra!CodAlter = TabCuenConsu!CodAlter
                                                //TabPaciCentra!ConseHC = TabCuenConsu!ConseHC
                                                //'
                                                //TabPaciCentra!Cancer = TabCuenConsu!Cancer
                                                //TabPaciCentra!ViviendaPropia = TabCuenConsu!ViviendaPropia
                                                //TabPaciCentra!Instituto = TabCuenConsu!Instituto
                                                //TabPaciCentra!AnosTrabajo = TabCuenConsu!AnosTrabajo
                                                //TabPaciCentra!FecPrirAten = TabCuenConsu!FecPrirAten
                                                //TabPaciCentra!Altocosto = TabCuenConsu!Altocosto
                                                //TabPaciCentra!CausaMuerte = TabCuenConsu!CausaMuerte
                                                //TabPaciCentra!RazonMuerte = TabCuenConsu!RazonMuerte
                                                //TabPaciCentra!FechaMuerte = TabCuenConsu!FechaMuerte
                                                //TabPaciCentra!LugarMuerte = TabCuenConsu!LugarMuerte
                                                //TabPaciCentra!FecRegMuer = TabCuenConsu!FecRegMuer
                                                //TabPaciCentra!HorRegMuer = TabCuenConsu!HorRegMuer
                                                //TabPaciCentra!CodRegMuer = TabCuenConsu!CodRegMuer
                                                //TabPaciCentra!MedicoRemite = TabCuenConsu!MedicoRemite

                                                Boolean RegisPasciente = Conexion.SqlInsert(Utils.SqlDatos);

                                                if (RegisPasciente)
                                                {
                                                    int TolPas = Convert.ToInt32(TxtCanhisForm.Text) + 1;
                                                    TxtCanhisForm.Text = Convert.ToString(TolPas);
                                                    ContiPro = 1;
                                                }



                                            }
                                            else
                                            {
                                                //'Tomamo el número de historia para modificarlo en el portatil

                                                Utils.SqlDatos = "UPDATE [ACDATOXPSQL].[dbo].[Datos del Paciente] SET " +
                                                "HistorPaci = '" + HiBusCue + "'" +
                                                "Where TipoIden = N'" + TipDocPacCuen + "'  and NumIden = N'" + NumDocPacCuen + "'  ";

                                                Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);

                                                if (Act)
                                                {
                                                    ContiPro = 1;
                                                }
                                           
                                            }//'Final de TabPaciCentraCC

                                        }//Using TabPaciCentraCC

                                        TabPaciCentraCC.Close();

                                    }
                                    else
                                    {
                                        //Todo bien existe
                                        ContiPro = 1;

                                    }  //TabPaciCentra.HasRows == false)                      

                                }//Using TabPaciCentra

                                TabPaciCentra.Close();





                                if (ContiPro == 1)
                                {
                                    //Revisamos si el número cuenta existe en la base de datos del servidor central


                                    SqlCuenCen = "SELECT * FROM [ACDATOXPSQL].[dbo].[Datos cuentas de consumos] ";
                                    SqlCuenCen += "Where CuenNum = N'" + CuenBusCue + "'";


                                    SqlDataReader TabCuenCen;

                                    ConectarPortatil();
                                    using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                                    {
                                        SqlCommand command2 = new SqlCommand(SqlCuenCen, connection2);
                                        command2.Connection.Open();
                                        TabCuenCen = command2.ExecuteReader();

                                        if (TabCuenCen.HasRows == false)
                                        {
                                            //El numero de cuenta no existe, por lo tanto puede contnuar a agregarla

                                            Utils.SqlDatos = "INSERT INTO  [ACDATOXPSQL].[dbo].[Datos cuentas de consumos]" +
                                            "(" +
                                            "CuenNum ," +
                                            "HistoNum ," +
                                            "CodAdmin ," +
                                            "NumContra ," +
                                            "FecApertura ," +
                                            "HorApertura ," +
                                            "CuenActiva ," +
                                            "FechaCierre ," +
                                            "VieneRemi ," +
                                            "NumRemi ," +
                                            "FecRemi ," +
                                            "FecVenci ," +
                                            "NumAfil ," +
                                            "NumPoliza ," +
                                            "CubreRemi ," +
                                            "TipoUsuario ," +
                                            "TipoAfiliado ," +
                                            "EstratoP ," +
                                            "CodiRemIPS ," +
                                            "SeccionalDpto ," +
                                            "MunicipioRemite ," +
                                            "DptoResi ," +
                                            "CiudRes ," +
                                            "BarrioVereda ," +
                                            "ZonaC ," +
                                            "Especialidad ," +
                                            "FecEntrada ," +
                                            "HorEntrada ," +
                                            "TipoCuenta ," +
                                            "CodiMedico ," +
                                            "UnidadEdad ," +
                                            "ValorEdad ," +
                                            "EnObser ," +
                                            "HorasObser ," +
                                            "DxEntra ," +
                                            "CausaExterna ," +
                                            "DadoDeAlta ," +
                                            "DxSalida," +
                                            "TipoDxPrin," +
                                            "FecSalida," +
                                            "HorSalida," +
                                            "EstaSalida," +
                                            "CodiMediSali," +
                                            "CuenAnulada," +
                                            "FecAnulada," +
                                            "CodiAnula," +
                                            "CodiSali," +
                                            "FecSali," +
                                            "Diasincapacidad," +
                                            "HospiTal," +
                                            "DiasEstancias," +
                                            "Destino," +
                                            "DxComplica," +
                                            "DxMuerte," +
                                            "DxRelac01," +
                                            "DxRelac02," +
                                            "DxRelac03," +
                                            "ServiRips," +
                                            "Observaciones," +
                                            "CuenPrincipal," +
                                            "CuentaMadre," +
                                            "RemiteA," +
                                            "RemiNumA," +
                                            "FecRemA," +
                                            "DptoRemA," +
                                            "CiudRemA," +
                                            "IPSRemA," +
                                            "CodEspecA," +
                                            "IPSAten," +
                                            "CodiRegis," +
                                            "FecRegis," +
                                            "CodiModi," +
                                            "FecModi," +
                                            "NumCitaFac," +
                                            "SoatAcumul," +
                                            "VigenCuenta," +
                                            "CoberSalud," +
                                            "CodSeSocial," +
                                            "NumFacAten," +
                                            "DefiCuenta" +
                                            ")" +
                                            "VALUES" +
                                            "(" +
                                            "'" + TabCuenConsu["CuenNum"].ToString() + "'," +
                                            "'" + TabCuenConsu["HistoNum"].ToString() + "'," +
                                            "'" + TabCuenConsu["CodAdmin"].ToString() + "'," +
                                            "'" + TabCuenConsu["NumContra"].ToString() + "'," +
                                            $"{ValidarFechaNula(TabCuenConsu["FecApertura"].ToString())}" +
                                            "CONVERT(DATETIME,'" + Convert.ToDateTime(TabCuenConsu["HorApertura"]).ToString("hh:mm:ss") + "',102)," +
                                            "'" + TabCuenConsu["CuenActiva"].ToString() + "'," +
                                            $"{ValidarFechaNula(TabCuenConsu["FechaCierre"].ToString())}" +
                                            "'" + TabCuenConsu["VieneRemi"].ToString() + "'," +
                                            "'" + TabCuenConsu["NumRemi"].ToString() + "'," +
                                            $"{ValidarFechaNula(TabCuenConsu["FecRemi"].ToString())}" +
                                            $"{ValidarFechaNula(TabCuenConsu["FecVenci"].ToString())}" +
                                            "'" + TabCuenConsu["NumAfil"].ToString() + "'," +
                                            "'" + TabCuenConsu["NumPoliza"].ToString() + "'," +
                                            "'" + TabCuenConsu["CubreRemi"].ToString() + "'," +
                                            "'" + TabCuenConsu["TipoUsuario"].ToString() + "'," +
                                            "'" + TabCuenConsu["TipoAfiliado"].ToString() + "'," +
                                            "'" + TabCuenConsu["EstratoP"].ToString() + "'," +
                                            "'" + TabCuenConsu["CodiRemIPS"].ToString() + "'," +
                                            "'" + TabCuenConsu["SeccionalDpto"].ToString() + "'," +
                                            "'" + TabCuenConsu["MunicipioRemite"].ToString() + "'," +
                                            "'" + TabCuenConsu["DptoResi"].ToString() + "'," +
                                            "'" + TabCuenConsu["CiudRes"].ToString() + "'," +
                                            "'" + TabCuenConsu["BarrioVereda"].ToString() + "'," +
                                            "'" + TabCuenConsu["ZonaC"].ToString() + "'," +
                                            "'" + TabCuenConsu["Especialidad"].ToString() + "'," +
                                            $"{ValidarFechaNula(TabCuenConsu["FecEntrada"].ToString())}" +
                                            "CONVERT(DATETIME,'" + Convert.ToDateTime(TabCuenConsu["HorEntrada"]).ToString("hh:mm:ss") + "',102)," +
                                            "'" + TabCuenConsu["TipoCuentaFactura"].ToString() + "'," + // Cambio del 19 de noviembre
                                            "'" + TabCuenConsu["CodiMedico"].ToString() + "'," +
                                            "'" + TabCuenConsu["UnidadEdad"].ToString() + "'," +
                                            "'" + TabCuenConsu["ValorEdad"].ToString() + "'," +
                                            "'" + TabCuenConsu["EnObser"].ToString() + "'," +
                                            "'" + TabCuenConsu["HorasObser"].ToString() + "'," +
                                            "'" + TabCuenConsu["DxEntra"].ToString() + "'," +
                                            "'" + TabCuenConsu["CausaExterna"].ToString() + "'," +
                                            "'" + TabCuenConsu["DadoDeAlta"].ToString() + "'," +
                                            "'" + TabCuenConsu["DxSalida"].ToString() + "'," +
                                            "'" + TabCuenConsu["TipoDxPrin"].ToString() + "'," +
                                            $"{ValidarFechaNula(TabCuenConsu["FecSalida"].ToString())}" +
                                            "CONVERT(DATETIME,'" + Convert.ToDateTime(TabCuenConsu["HorSalida"]).ToString("hh:mm:ss") + "',102)," +
                                            "'" + TabCuenConsu["EstaSalida"].ToString() + "'," +
                                            "'" + TabCuenConsu["CodiMediSali"].ToString() + "'," +
                                            "'" + TabCuenConsu["CuenAnulada"].ToString() + "'," +
                                            $"{ValidarFechaNula(TabCuenConsu["FecAnulada"].ToString())}" +
                                            "'" + TabCuenConsu["CodiAnula"].ToString() + "'," +
                                            "'" + TabCuenConsu["CodiSali"].ToString() + "'," +
                                            $"{ValidarFechaNula(TabCuenConsu["FecSali"].ToString())}" +
                                            "'" + TabCuenConsu["Diasincapacidad"].ToString() + "'," +
                                            "'" + TabCuenConsu["HospiTal"].ToString() + "'," +
                                            "'" + TabCuenConsu["DiasEstancias"].ToString() + "'," +
                                            "'" + TabCuenConsu["Destino"].ToString() + "'," +
                                            "'" + TabCuenConsu["DxComplica"].ToString() + "'," +
                                            "'" + TabCuenConsu["DxMuerte"].ToString() + "'," +
                                            "'" + TabCuenConsu["DxRelac01"].ToString() + "'," +
                                            "'" + TabCuenConsu["DxRelac02"].ToString() + "'," +
                                            "'" + TabCuenConsu["DxRelac03"].ToString() + "'," +
                                            "'" + TabCuenConsu["ServiRips"].ToString() + "'," +
                                            "'" + TabCuenConsu["Observaciones"].ToString() + "'," +
                                            "'" + TabCuenConsu["CuenPrincipal"].ToString() + "'," +
                                            "'" + TabCuenConsu["CuentaMadre"].ToString() + "'," +
                                            "'" + TabCuenConsu["RemiteA"].ToString() + "'," +
                                            "'" + TabCuenConsu["RemiNumA"].ToString() + "'," +
                                            $"{ValidarFechaNula(TabCuenConsu["FecRemA"].ToString())}" +
                                            "'" + TabCuenConsu["DptoRemA"].ToString() + "'," +
                                            "'" + TabCuenConsu["CiudRemA"].ToString() + "'," +
                                            "'" + TabCuenConsu["IPSRemA"].ToString() + "'," +
                                            "'" + TabCuenConsu["CodEspecA"].ToString() + "'," +
                                            "'" + TabCuenConsu["IPSAten"].ToString() + "'," +
                                            "'" + TabCuenConsu["CuentaRegistra"].ToString() + "'," +
                                            $"{ValidarFechaNula(TabCuenConsu["CuenFecRegis"].ToString())}" +
                                            "'" + TabCuenConsu["CuentaModifica"].ToString() + "'," +
                                            $"{ValidarFechaNula(TabCuenConsu["CuenFecModi"].ToString())}" +
                                            "'" + TabCuenConsu["NumCitaFac"].ToString() + "'," +
                                            "'" + TabCuenConsu["SoatAcumul"].ToString() + "'," +
                                            "'" + TabCuenConsu["VigenCuenta"].ToString() + "'," +
                                            "'" + TabCuenConsu["CoberSalud"].ToString() + "'," +
                                            "'" + TabCuenConsu["CodSeSocial"].ToString() + "'," +
                                            "'" + TabCuenConsu["NumFacAten"].ToString() + "'," +
                                            "'" + TabCuenConsu["DefiCuenta"].ToString() + "')";

                                            Boolean RegisCuenConsu = Conexion.SqlInsert(Utils.SqlDatos);

                                            //Proceda a agregar la factura, revisamos si existe en la central

                                            SqlFacCentra = "SELECT * FROM  [ACDATOXPSQL].[dbo].[Datos de las facturas realizadas] ";
                                            SqlFacCentra += "Where NumFactura = N'" + FacBusCue + "'";

                                            SqlDataReader TabFacCentra;

                                            ConectarPortatil();

                                            using (SqlConnection connection3 = new SqlConnection(Conexion.conexionSQL))
                                            {
                                                SqlCommand command3 = new SqlCommand(SqlFacCentra, connection3);
                                                command3.Connection.Open();
                                                TabFacCentra = command3.ExecuteReader();

                                                if (TabFacCentra.HasRows == false)
                                                {
                                                    //Proceda a agregar la factura a la central

                                                    Utils.SqlDatos = "INSERT INTO  [ACDATOXPSQL].[dbo].[Datos de las facturas realizadas]" +
                                                    "(" +
                                                    "NumFactura," +
                                                    "FechaFac," +
                                                    "NumCuenFac," +
                                                    "Cartercero," +
                                                    "NumContra," +
                                                    "ValorTotal," +
                                                    "ValorFac," +
                                                    "ValorOtros," +
                                                    "PorcTercero," +
                                                    "Copago," +
                                                    "CanceCopago," +
                                                    "FecCanCopa," +
                                                    "PagoFac," +
                                                    "FecCanFac," +
                                                    "PagoConDepos," +
                                                    "AnuladaFac," +
                                                    "FecAnulada," +
                                                    "CodiAnul," +
                                                    "DesAnulo," +
                                                    "NotaDebito," +
                                                    "NotaCredito," +
                                                    "DesVarios," +
                                                    "Retencion," +
                                                    "DesTramite," +
                                                    "OtrosDescuentos," +
                                                    "SiCobro," +
                                                    "CuentaCobro," +
                                                    "Copiada," +
                                                    "Efectivo," +
                                                    "TexResol," +
                                                    "saliocuco," +
                                                    "Cucosale," +
                                                    "fesale," +
                                                    "codisale," +
                                                    "Radicada," +
                                                    "Fecradifa," +
                                                    "Codradifa," +
                                                    "Codrecifa," +
                                                    "HoraReg," +
                                                    "ExpoRips," +
                                                    "NumPoliza," +
                                                    "CodSele," +
                                                    "RadiEPS," +
                                                    "FecRadica," +
                                                    "CodiRegis," +
                                                    "FecRegis," +
                                                    "CodiModi," +
                                                    "FecModi," +
                                                    "ConsoFac," +
                                                    "TolValIVA," + //'***** CAMPOS AGREGADOS CON BASE DATOS DE SAN AGUSTIN *****
                                                    "ReteIVA," +
                                                    "ReteICA," +
                                                    "InterCorrien," +
                                                    "InterMora," +//'***** CAMPOS AGREGADOS CON BASE DATOS DE GARZON y SAN AGUSTIN*****
                                                    "TipoDocTab," +
                                                    "VigenFactura," +
                                                    "CodiEstado," +
                                                    "PrefiFac" +
                                                    ")" +
                                                    "VALUES" +
                                                    "(" +
                                                    "'" + TabCuenConsu["NumFactura"].ToString() + "'," +
                                                    $"{ValidarFechaNula(TabCuenConsu["FechaFac"].ToString())}" +
                                                    "'" + TabCuenConsu["NumCuenFac"].ToString() + "'," +
                                                    "'" + TabCuenConsu["Cartercero"].ToString() + "'," +
                                                    "'" + TabCuenConsu["NumContra"].ToString() + "'," +
                                                    "'" + TabCuenConsu["ValorTotal"].ToString() + "'," +
                                                    "'" + TabCuenConsu["ValorFac"].ToString() + "'," +
                                                    "'" + TabCuenConsu["ValorOtros"].ToString() + "'," +
                                                    "'" + TabCuenConsu["PorcTercero"].ToString() + "'," +
                                                    "'" + TabCuenConsu["Copago"].ToString() + "'," +
                                                    "'" + TabCuenConsu["CanceCopago"].ToString() + "'," +
                                                    $"{ValidarFechaNula(TabCuenConsu["FecCanCopa"].ToString())}" +
                                                    "'" + TabCuenConsu["PagoFac"].ToString() + "'," +
                                                    $"{ValidarFechaNula(TabCuenConsu["FecCanFac"].ToString())}" +
                                                    "'" + TabCuenConsu["PagoConDepos"].ToString() + "'," +
                                                    "'" + TabCuenConsu["AnuladaFac"].ToString() + "'," +
                                                    $"{ValidarFechaNula(TabCuenConsu["FecAnulada"].ToString())}" +
                                                    "'" + TabCuenConsu["CodiAnul"].ToString() + "'," +
                                                    "'" + TabCuenConsu["DesAnulo"].ToString() + "'," +
                                                    "'" + TabCuenConsu["NotaDebito"].ToString() + "'," +
                                                    "'" + TabCuenConsu["NotaCredito"].ToString() + "'," +
                                                    "'" + TabCuenConsu["DesVarios"].ToString() + "'," +
                                                    "'" + TabCuenConsu["Retencion"].ToString() + "'," +
                                                    "'" + TabCuenConsu["DesTramite"].ToString() + "'," +
                                                    "'" + TabCuenConsu["OtrosDescuentos"].ToString() + "'," +
                                                    "'" + TabCuenConsu["SiCobro"].ToString() + "'," +
                                                    "'" + TabCuenConsu["CuentaCobro"].ToString() + "'," +
                                                    "'" + TabCuenConsu["Copiada"].ToString() + "'," +
                                                    "'" + TabCuenConsu["Efectivo"].ToString() + "'," +
                                                    "'" + TabCuenConsu["TexResol"].ToString() + "'," +
                                                    "'" + TabCuenConsu["saliocuco"].ToString() + "'," +
                                                    "'" + TabCuenConsu["Cucosale"].ToString() + "'," +
                                                    "'" + TabCuenConsu["fesale"].ToString() + "'," +
                                                    "'" + TabCuenConsu["codisale"].ToString() + "'," +
                                                    "'" + TabCuenConsu["Radicada"].ToString() + "'," +
                                                    $"{ValidarFechaNula(TabCuenConsu["Fecradifa"].ToString())}" +
                                                    "'" + TabCuenConsu["Codradifa"].ToString() + "'," +
                                                    "'" + TabCuenConsu["Codrecifa"].ToString() + "'," +
                                                    "CONVERT(DATETIME,'" + TabCuenConsu["HoraReg"].ToString() + "',8)," +
                                                    "'" + TabCuenConsu["ExpoRips"].ToString() + "'," +
                                                    "'" + TabCuenConsu["NumPoliza"].ToString() + "'," +
                                                    "'" + TabCuenConsu["CodSele"].ToString() + "'," +
                                                    "'" + TabCuenConsu["RadiEPS"].ToString() + "'," +
                                                    $"{ValidarFechaNula(TabCuenConsu["FecRadica"].ToString())}" +
                                                    "'" + TabCuenConsu["FacturaRegistra"].ToString() + "'," +
                                                    $"{ValidarFechaNula(TabCuenConsu["FacFecRegis"].ToString())}" +
                                                    "'" + TabCuenConsu["FacturaModifica"].ToString() + "'," +
                                                    $"{ValidarFechaNula(TabCuenConsu["FacFecModi"].ToString())}" +
                                                    "'" + TabCuenConsu["ConsoFac"].ToString() + "'," +
                                                    "'" + TabCuenConsu["TolValIVA"].ToString() + "'," +
                                                    "'" + TabCuenConsu["ReteIVA"].ToString() + "'," +
                                                    "'" + TabCuenConsu["ReteICA"].ToString() + "'," +
                                                    "'" + TabCuenConsu["InterCorrien"].ToString() + "'," +
                                                    "'" + TabCuenConsu["InterMora"].ToString() + "'," +
                                                    "'" + TabCuenConsu["TipoDocTab"].ToString() + "'," +
                                                    "'" + TabCuenConsu["VigenFactura"].ToString() + "'," +
                                                    "'" + TabCuenConsu["CodiEstado"].ToString() + "'," +
                                                    "'" + TabCuenConsu["PrefiFac"].ToString() + "'" +
                                                    ")";

                                                    Boolean RegisFactur = Conexion.SqlInsert(Utils.SqlDatos);

                                                    //Procedemoss a agregar los consumos, para ello buscamos los consumos en la tabla de registrosdel servidor

                                                    SqlConsumos = "SELECT * FROM [ACDATOXPSQL].[dbo].[Datos registros de consumos] ";
                                                    SqlConsumos = SqlConsumos + "Where CuenConsu = N'" + CuenBusCue + "'";

                                                    ConectarCentral();

                                                    using (SqlConnection connection4 = new SqlConnection(Conexion.conexionSQL))
                                                    {
                                                        SqlCommand command4 = new SqlCommand(SqlConsumos, connection4);
                                                        command4.Connection.Open();
                                                        TabConsumos = command4.ExecuteReader();

                                                        if (TabConsumos.HasRows == false)
                                                        {
                                                            //'Casi imposible que existn facturas sin consumos
                                                        }
                                                        else
                                                        {

                                                            ConectarPortatil();

                                                            while (TabConsumos.Read())
                                                            {
                                                                IteNumCon = Convert.ToDouble(TabConsumos["ItemConsu"]);

                                                                SqlConsuCen = "SELECT * FROM [ACDATOXPSQL].[dbo].[Datos registros de consumos] ";
                                                                SqlConsuCen = SqlConsuCen + "Where CuenConsu = N'" + CuenBusCue + "' and ItemConsuREF = " + IteNumCon;


                                                                SqlDataReader TabConsuCen;

                                                                using (SqlConnection connection5 = new SqlConnection(Conexion.conexionSQL))
                                                                {
                                                                    SqlCommand command5 = new SqlCommand(SqlConsuCen, connection5);
                                                                    command5.Connection.Open();
                                                                    TabConsuCen = command5.ExecuteReader();

                                                                    if (TabConsuCen.HasRows == false)
                                                                    {
                                                                        Utils.SqlDatos = "INSERT INTO [ACDATOXPSQL].[dbo].[Datos registros de consumos] " +
                                                                        "(" +
                                                                        "CuenConsu," +
                                                                        "CodInter," +
                                                                        "CoCentroCon," +
                                                                        "Cantidad," +
                                                                        "SubValorUnita," +
                                                                        "ValorUnitario," +
                                                                        "Copagos," +
                                                                        "FechaCon," +
                                                                        "HoraRegistro," +
                                                                        "AutoriNum," +
                                                                        "TipoCirujia," +
                                                                        "ViaAcceso," +
                                                                        "HojaQuirur," +
                                                                        "FormaRealiza," +
                                                                        "PagaHoja," +
                                                                        "NumHoja," +
                                                                        "NumMedica," +
                                                                        "UVRIssCon," +
                                                                        "GruposCir," +
                                                                        "CodiSOAT," +
                                                                        "CodiISS," +
                                                                        "CodiCUPS," +
                                                                        "Agrupa," +
                                                                        "DepenServi," +
                                                                        "CoProDepen," +
                                                                        "RealizadoEn," +
                                                                        "FinalProce," +
                                                                        "FinalConsul," +
                                                                        "ConsulVez," +
                                                                        "PerAtien," +
                                                                        "CondiPaci," +
                                                                        "CodEspecial," +
                                                                        "CodiMedico," +
                                                                        "SeRepRips," +
                                                                        "TipoProduc," +
                                                                        "ElimRegis," +
                                                                        "NoMuestra," +
                                                                        "VigenciaML," +
                                                                        "NoOrden," +
                                                                        "MuRegistra," +
                                                                        "ExamenPEN," +
                                                                        "DescribePEN," +
                                                                        "ItemConsuREF," +
                                                                        "CodiRegis," +
                                                                        "FecRegis," +
                                                                        "Horaregis," +
                                                                        "CodModi," +
                                                                        "FecModi," +
                                                                        "VezAno," +
                                                                        "ValIVATol" +  //'***** CAMPOS AGREGADOS CON BASE DATOS DE SAN AGUSTIN Y GARZON *****
                                                                        ")" +
                                                                        "VALUES" +
                                                                        "(" +
                                                                        "'" + TabConsumos["CuenConsu"].ToString() + "'," +
                                                                        "'" + TabConsumos["CodInter"].ToString() + "'," +
                                                                        "'" + TabConsumos["CoCentroCon"].ToString() + "'," +
                                                                        "'" + TabConsumos["Cantidad"].ToString() + "'," +
                                                                        "'" + TabConsumos["SubValorUnita"].ToString() + "'," +
                                                                        "'" + TabConsumos["ValorUnitario"].ToString() + "'," +
                                                                        "'" + TabConsumos["Copagos"].ToString() + "'," +
                                                                       $"{ValidarFechaNula(TabConsumos["FechaCon"].ToString())}" +
                                                                        "CONVERT(DATETIME,'" + TabConsumos["HoraRegistro"].ToString() + "',8)," +
                                                                        "'" + TabConsumos["AutoriNum"].ToString() + "'," +
                                                                        "'" + TabConsumos["TipoCirujia"].ToString() + "'," +
                                                                        "'" + TabConsumos["ViaAcceso"].ToString() + "'," +
                                                                        "'" + TabConsumos["HojaQuirur"].ToString() + "'," +
                                                                        "'" + TabConsumos["FormaRealiza"].ToString() + "'," +
                                                                        "'" + TabConsumos["PagaHoja"].ToString() + "'," +
                                                                        "'" + TabConsumos["NumHoja"].ToString() + "'," +
                                                                        "'" + TabConsumos["NumMedica"].ToString() + "'," +
                                                                        "'" + TabConsumos["UVRIssCon"].ToString() + "'," +
                                                                        "'" + TabConsumos["GruposCir"].ToString() + "'," +
                                                                        "'" + TabConsumos["CodiSOAT"].ToString() + "'," +
                                                                        "'" + TabConsumos["CodiISS"].ToString() + "'," +
                                                                        "'" + TabConsumos["CodiCUPS"].ToString() + "'," +
                                                                        "'" + TabConsumos["Agrupa"].ToString() + "'," +
                                                                        "'" + TabConsumos["DepenServi"].ToString() + "'," +
                                                                        "'" + TabConsumos["CoProDepen"].ToString() + "'," +
                                                                        "'" + TabConsumos["RealizadoEn"].ToString() + "'," +
                                                                        "'" + TabConsumos["FinalProce"].ToString() + "'," +
                                                                        "'" + TabConsumos["FinalConsul"].ToString() + "'," +
                                                                        "'" + TabConsumos["ConsulVez"].ToString() + "'," +
                                                                        "'" + TabConsumos["PerAtien"].ToString() + "'," +
                                                                        "'" + TabConsumos["CondiPaci"].ToString() + "'," +
                                                                        "'" + TabConsumos["CodEspecial"].ToString() + "'," +
                                                                        "'" + TabConsumos["CodiMedico"].ToString() + "'," +
                                                                        "'" + TabConsumos["SeRepRips"].ToString() + "'," +
                                                                        "'" + TabConsumos["TipoProduc"].ToString() + "'," +
                                                                        "'" + TabConsumos["ElimRegis"].ToString() + "'," +
                                                                        "'" + TabConsumos["NoMuestra"].ToString() + "'," +
                                                                        "'" + TabConsumos["VigenciaML"].ToString() + "'," +
                                                                        "'" + TabConsumos["NoOrden"].ToString() + "'," +
                                                                        "'" + TabConsumos["MuRegistra"].ToString() + "'," +
                                                                        "'" + TabConsumos["ExamenPEN"].ToString() + "'," +
                                                                        "'" + TabConsumos["DescribePEN"].ToString() + "'," +
                                                                        "'" + TabConsumos["IteNumCon"].ToString() + "'," +
                                                                        "'" + TabConsumos["CodiRegis"].ToString() + "'," +
                                                                        $"{ValidarFechaNula(TabConsumos["FecRegis"].ToString())}" +
                                                                        "CONVERT(DATETIME,'" + TabConsumos["Horaregis"].ToString() + "',8)," +
                                                                        "'" + TabConsumos["CodModi"].ToString() + "'," +
                                                                        $"{ValidarFechaNula(TabConsumos["FecModi"].ToString())}" +
                                                                        "'" + TabConsumos["VezAno"].ToString() + "'," +

                                                                        "'" + TabConsumos["ValIVATol"].ToString() + "'" +  //***** CAMPOS AGREGADOS CON BASE DATOS DE SAN AGUSTIN Y GARZON *****

                                                                        ")";


                                                                        Boolean RegisConsumoCen = Conexion.SqlInsert(Utils.SqlDatos);

                                                                    }
                                                                    else
                                                                    {
                                                                        //Ya existen
                                                                        int TolPas = Convert.ToInt32(TxtCanConsuForm.Text) + 1;
                                                                        TxtCanConsuForm.Text = Convert.ToString(TolPas);

                                                                    }

                                                                    TabConsuCen.Close();

                                                                }//Using Consumos Central

                                                            }//While

                                                        }// if(TabConsumos.HasRows == false)

                                                        TabConsumos.Close();

                                                    }//Using Conexion 4

                                                }
                                                else
                                                {
                                                    //    'La factura ya existe
                                                    int TolPas = Convert.ToInt32(TxtCanFacFor.Text) + 1;
                                                    TxtCanFacFor.Text = Convert.ToString(TolPas);

                                                }//TabFacCentra

                                                TabFacCentra.Close();
                                            }//Using

                                        }
                                        else
                                        {
                                            int TolPas = Convert.ToInt32(TxtCanCuenExis.Text) + 1;
                                            TxtCanCuenExis.Text = Convert.ToString(TolPas);

                                        }//TabCuentasCen
                                    }//Using

                                    TabCuenCen.Close();

                                }//ContiPRO

                            }//While

                            TxtCanFacFor.Text = Convert.ToString(TolFacEx);

                            Utils.Informa = "El proceso ha terminado satisfactoriamente" + "\r";
                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }//  if(TabCuenConsu.HasRows == false)

                        TabCuenConsu.Close();

                    }//Fin Usn


                }
                else
                {
                    //No quiere hacer nada

                }//Fin Pregunta

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "después de hacer click sobre el botón exportar " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
