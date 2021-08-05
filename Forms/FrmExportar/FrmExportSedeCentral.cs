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
    public partial class FrmExportSedeCentral : Form
    {
        public FrmExportSedeCentral()
        {
            InitializeComponent();
        }

        #region Funciones

        private string ValidarFechaNula(string Fecha)
        {
            string ValidarFecha = null;

            ValidarFecha = string.IsNullOrWhiteSpace(Fecha) ? "null" + "," : "CONVERT(DATETIME,'" + Convert.ToDateTime(Fecha).ToString("yyyy-MM-dd") + "',102),";

            return ValidarFecha;

        }

        private string PrefijoDocumenCentral(string CodDocu)
        {
            try
            {
                string PreDoc, SqlConsecu;

                //'Cargamos primeramente los combos, abriendo las instancias que se pueden cerrar inmediatamente


                SqlConsecu = "SELECT  CodConse, PrefiConse, ConseDocu, UlltiConseDoc, CodUsaConse, ";
                SqlConsecu += "ConseCrono, FecConseDoc, CanDigConse, NomDocuConse, CodRegis, FecRegis ";
                SqlConsecu += "FROM [BDADMINSIG].[dbo].[Datos consecutivos SIIGHOSPLUS] ";
                SqlConsecu += "WHERE CodConse = '" + CodDocu + "' ";
                SqlConsecu += "ORDER BY CodConse ";

                SqlDataReader TabConsecu = Conexion.SQLDataReader(SqlConsecu);

                if (TabConsecu.HasRows == false)
                {
                    //NO se encontró el registro de consecutivo para este documento
                    PreDoc = "00";
                }
                else
                {
                    TabConsecu.Read();
                    //   'Revisamos si el documento es obligatorio que sea cronologíco

                    PreDoc = TabConsecu["PrefiConse"].ToString();


                }

                TabConsecu.Close();

                if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();


                return PreDoc;

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la función: PrefijoDocumenCentral " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "-1";
            }
        }

        private string ConseDocumElectro(string CodDocu, Boolean ActConse, string CodUsua)
        {
            try
            {
                Utils.Titulo01 = "Control para cargar formularios";

                //'Cargamos primeramente los combos, abriendo las instancias que se pueden cerrar inmediatamente
                string CodFormado = "", ConTomado = "";
                DateTime FechaUltima;
                int SigPro, TCG;
                double ConsecActual;
                string SqlConsecu = "SELECT  CodConse, PrefiConse, ConseDocu, UlltiConseDoc, CodUsaConse, ";
                SqlConsecu += "ConseCrono, FecConseDoc, CanDigConse, NomDocuConse, CodRegis, FecRegis ";
                SqlConsecu += "FROM [BDADMINSIG].[dbo].[Datos consecutivos SIIGHOSPLUS] ";
                SqlConsecu += "WHERE CodConse = '" + CodDocu + "' ";

                //string Fecha = Fecha2.ToString("dd-MM-yyyy");
                string Fecha = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime Fecha2 = Convert.ToDateTime(Fecha);
                SqlDataReader TabConsecu;

                ConectarCentral();

                using (SqlConnection connection3 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command3 = new SqlCommand(SqlConsecu, connection3);
                    command3.Connection.Open();
                    TabConsecu = command3.ExecuteReader();

                    if (TabConsecu.HasRows == false)
                    {
                        CodFormado = "0";
                    }
                    else
                    {
                        //    'Revisamos si el documento es obligatorio que sea cronologíco
                        TabConsecu.Read();


                        if (Convert.ToBoolean(TabConsecu["ConseCrono"]) == true)
                        {
                            //       'Es obligatorio la fecha cronologica
                            FechaUltima = Convert.ToDateTime(TabConsecu["FecConseDoc"]);

                            //'Revisemos que esta no sea mayor a la del sistema

                            if (FechaUltima > Fecha2)
                            {
                                CodFormado = "-2";
                                SigPro = 0;
                            }
                            else
                            {
                                SigPro = 1;
                            }//Final de FechaUltima > Date

                        }
                        else
                        {
                            SigPro = 1;
                        }//'Final de TabConsecu![ConseCrono] = True


                        if (SigPro == 1)
                        {
                            // 'Revisamos si existe un consecutivo por reasignar o perdido

                            if (Convert.ToInt32(TabConsecu["UlltiConseDoc"]) == 0)
                            {
                                // 'No existen comprobantes perdidos, debe generar el siguiente
                                ConsecActual = Convert.ToDouble(TabConsecu["ConseDocu"]);

                                ConsecActual += 1;




                                if (ActConse)
                                {
                                    Utils.SqlDatos = "UPDATE [BDADMINSIG].[dbo].[Datos consecutivos SIIGHOSPLUS] SET ConseDocu = '" + ConsecActual + "',CodUsaConse = '" + CodUsua + "', FecConseDoc = CONVERT(DATETIME, '" + Fecha + "', 102) WHERE CodConse = '" + CodDocu + "'";

                                    Boolean ActuConse = Conexion.SQLUpdate(Utils.SqlDatos);

                                }// 'Final de ActConse = True

                            }
                            else
                            {
                                // Existe un consecutivo perdido

                                ConsecActual = Convert.ToDouble(TabConsecu["UlltiConseDoc"]);

                                if (ActConse)
                                {
                                    Utils.SqlDatos = "UPDATE [BDADMINSIG].[dbo].[Datos consecutivos SIIGHOSPLUS] SET UlltiConseDoc = 0 WHERE CodConse = '" + CodDocu + "'";

                                    Boolean ActuConse = Conexion.SQLUpdate(Utils.SqlDatos);
                                }
                            }// 'Final de TabConsecu![UlltiConseDoc] = 0

                            //Tomamos el tamaño del código a generar

                            TCG = Convert.ToInt32(TabConsecu["CanDigConse"]);

                            ConTomado = Convert.ToString(ConsecActual);

                            CodFormado = ConTomado;

                        }//Fin SigPro = 1
                    }// if (TabConsecu.HasRows == false)

                    TabConsecu.Close();

                }   //Using


                return CodFormado;

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la función: ConseDocumElectro del módulo variables de la aplicación " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "-1";
            }
            finally
            {
                if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();
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
                }else
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
                        TxtFacElec.Text = "No";
                        DateFecIniFacElec.Value = DateTime.Now;
                    }
                    else
                    {
                        TabInforEmpre.Read();
                        Boolean FacturElect = Convert.ToBoolean(TabInforEmpre["FacturElec"]);

                        if (FacturElect)
                        {
                            TxtFacElec.Text = "Si";
                            DateFecIniFacElec.Value = Convert.ToDateTime(TabInforEmpre["FecFacElec"]);
                            LblDiasVenFac.Text = Convert.ToString(TabInforEmpre["DiasVenFac"]);

                        }
                        else
                        {
                            TxtFacElec.Text = "No";
                            DateFecIniFacElec.Value = DateTime.Now;
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

        #endregion

        #region Botones
        private void BtnBuscarPacientes_Click(object sender, EventArgs e)
        {
            try
            {

                string CodConseFacE = "", SqlCuenConsu = "", SqlFacCentra = "", NumResFac = "", TexResFacElec = "", UsaRegis, FacPorta = "", FunConFac = "", PrefiFacE = "", NumFacElec = "", SqlResolFactur = "";
                int SigoProcFac = 0, CantiFacElec = 0, ContiPro = 0;
                double TolFacEx = 0;
                string HiBusCue, CuenBusCue, FacBusCue, TiDGr, NumDgr, SqlPaciCentra, SqlPaciCentraCC, SqlCuenCen;
                SqlDataReader TabResolFactur;
                CodConseFacE = "51"; //'*********************** Codigo asignado para la generación de consecutivos de facturas electronicas en este sistema ************************
                SqlDataReader reader;
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

                string FecIniPro = Convert.ToString(DateInicial.Value.ToString("yyyy-MM-dd"));
                string FecFinPro = Convert.ToString(DateFinal.Value.ToString("yyyy-MM-dd"));

                string PfiCen = TxtPrefiCenFor.Text;
                string PfiPor = TxtPrefiPorFor.Text;


                Utils.Informa = "¿Usted desea iniciar el proceso de exportación" + "\r";
                Utils.Informa += "de todo lo facturado en la instancia del" + "\r";
                Utils.Informa += "portatil a la instancia del servidor central?" + "\r";
                Utils.Informa += "Fecha Inicial: " + FecIniPro + "\r";
                Utils.Informa += "Fecha Final: " + FecFinPro;
                var res = MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {

                    TxtCanFacFor.Text = "0";
                    TxtCanhisForm.Text = "0";
                    TxtCanCuenExis.Text = "0";
                    TxtCanFacForm.Text = "0";
                    TxtCanConsuForm.Text = "0";
                    //Revisamos si la entidad tiene facturación activa, para que las facturas eventos les asigne el consecutivo de la central de facturación electronica
                    //**************************** Se crea el 13 de octubre de 2020 ***********************************************}

                    string FecElec = Convert.ToString(DateFecIniFacElec.Value.ToString("yyyy-MM-dd"));

                    string FechaHoy = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));
                    

                    if (DateFecIniFacElec.Value <= DateTime.Now)
                    {
                        //'Genere los consecutivos de las facturas eventos


                        ConectarPortatil(); //S e hace para que la siguiente consulta apunte a lainstancia del portatil



                        int con = 0;
                        string SqlCuenConsuCount = "SELECT count(*) as totalRegis " +
                        "FROM [ACDATOXPSQL].[dbo].[Datos de las facturas realizadas] " +
                        "WHERE ([Datos de las facturas realizadas].PrefiFac = N'" + PfiPor + "') AND" +
                        "([Datos de las facturas realizadas].ConsoFac = 'False' ) AND " +
                        "([Datos de las facturas realizadas].FechaFac >= CONVERT(DATETIME, '" + FecIniPro + "', 102)) AND " +
                        "([Datos de las facturas realizadas].FechaFac <= CONVERT(DATETIME, '" + FecFinPro + "', 102)) AND " +
                        "([Datos de las facturas realizadas].AnuladaFac = 'False') AND ([Datos de las facturas realizadas].TipoDocTab = N'1') AND " +
                        "([Datos de las facturas realizadas].CodEstaDian = '00')";

                        reader = Conexion.SQLDataReader(SqlCuenConsuCount);

                        if (reader.HasRows)
                        {
                            reader.Read();
                            con = Convert.ToInt32(reader["totalRegis"]);

                            if(con != 0)
                            {
                                progressBar.Minimum = 1;
                                progressBar.Maximum = con;
                            }
                            else
                            {
                                progressBar.Minimum = 0;
                                progressBar.Maximum = 1;
                                progressBar.Value = 0;
                            }

                        }
                        else
                        {
                            progressBar.Minimum = 0;
                            progressBar.Maximum = 1;
                            progressBar.Value = 0;
                        }

                        if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();


                        SqlCuenConsu = "SELECT NumFactura, FechaFac, PrefiFac, TipoDocTab, NumFacAntes, PrefiFacElec, FecVenci, NumResol, CodEstaDian, " +
                        "FecCamEsta , HorCamEsta, CodCufe, NomDocValida, ObserGene, AnuladaFac, TexResol " +
                        "FROM [ACDATOXPSQL].[dbo].[Datos de las facturas realizadas] " +
                        "WHERE ([Datos de las facturas realizadas].PrefiFac = N'" + PfiPor + "') AND" +
                        "([Datos de las facturas realizadas].ConsoFac = 'False' ) AND " +
                        "([Datos de las facturas realizadas].FechaFac >= CONVERT(DATETIME, '" + FecIniPro + "', 102)) AND " +
                        "([Datos de las facturas realizadas].FechaFac <= CONVERT(DATETIME, '" + FecFinPro + "', 102)) AND " +
                        "([Datos de las facturas realizadas].AnuladaFac = 'False') AND ([Datos de las facturas realizadas].TipoDocTab = N'1') AND " +
                        "([Datos de las facturas realizadas].CodEstaDian = '00')";


                        using (SqlConnection connection3 = new SqlConnection(Conexion.conexionSQL))
                        {
                            SqlCommand command3 = new SqlCommand(SqlCuenConsu, connection3);
                            command3.Connection.Open();
                            TabCuenConsu = command3.ExecuteReader();

                            if (TabCuenConsu.HasRows == false)
                            {
                                //'No hay facturas para generar consecutivos

                                SigoProcFac = 1;

                            }
                            else
                            {

                                CantiFacElec = 0;
                                while (TabCuenConsu.Read())
                                {


                                    FacPorta = TabCuenConsu["NumFactura"].ToString();


                                    FunConFac = ConseDocumElectro(CodConseFacE, true, UsaRegis);

                                    switch (FunConFac)
                                    {
                                        case "-3":
                                            Utils.Informa = "Lo siento pero en esta base de datos no se pueden" + "\r";
                                            Utils.Informa += "registrar más facturas de ventas electrónicas," + "\r";
                                            Utils.Informa += "porque pasó la longitud permitida del código.";
                                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            SigoProcFac = 0;
                                            break;
                                        case "-2":
                                            Utils.Informa = "Lo siento pero en esta base de datos no se pueden" + "\r";
                                            Utils.Informa += "registrar más facturas de ventas electrónicas," + "\r";
                                            Utils.Informa += "la fecha del último generado es mayor a la del sistema.";
                                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            SigoProcFac = 0;
                                            break;
                                        case "-1":
                                            SigoProcFac = 0;
                                            break;
                                        case "0":
                                            //'NO se ha definido el tipo de documento para generar cuentas
                                            Utils.Informa = "Lo siento pero en esta base de datos no se pueden" + "\r";
                                            Utils.Informa += "más consecutivos de facturas de ventas electrónicas," + "\r";
                                            Utils.Informa += "porque el número de comprobante no se encuentra definido.";
                                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            SigoProcFac = 0;
                                            break;
                                        default:

                                            PrefiFacE = PrefijoDocumenCentral(CodConseFacE); //Buscamos el prefijo de factura tradicional

                                            NumFacElec = FunConFac;

                                            //'Validamos si el prefijo tiene una resolución habilitada, y si el consecutivo es valido

                                            SqlResolFactur = "SELECT PrefiFac, NumResol, FecResol, RanIni, RanFin, TexResol, FecVigen, ResolVigen, CodiModi, FecModi ";
                                            SqlResolFactur += "FROM [ACDATOXPSQL].[dbo].[Datos resoluciones de facturas] ";
                                            SqlResolFactur += " WHERE (PrefiFac = N'" + PrefiFacE + " ') AND (ResolVigen = 'True')";
                                            SqlResolFactur += " ORDER BY FecVigen ";

                                            ConectarCentral();

                                            using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                                            {
                                                SqlCommand command = new SqlCommand(SqlResolFactur, connection);
                                                command.Connection.Open();
                                                TabResolFactur = command.ExecuteReader();

                                                if (TabResolFactur.HasRows == false)
                                                {
                                                    Utils.Informa = "Lo siento pero el prefijo de consecutivos " + "\r";
                                                    Utils.Informa += "no tiene registrados los datos de la resolución de" + "\r";
                                                    Utils.Informa += "habilitacion de la DIAN para poder generar facturas electrónicas";
                                                    MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                    SigoProcFac = 0;
                                                    return;
                                                }
                                                else
                                                {

                                                    //'Revisamos si la fecha de vencimineto de la resolución no es mayor a la del sistema

                                                    while (TabResolFactur.Read())
                                                    {
                                                        DateTime FecVigen = Convert.ToDateTime(TabResolFactur["FecVigen"]);

                                                        string NumResol = TabResolFactur["NumResol"].ToString();

                                                        if (FecVigen < DateTime.Now.Date) //Revisar aqui
                                                        {
                                                            //'Deshabilitamos la resolución, haber si ya se encuentra una nueva habilitada


                                                            ConectarCentral();

                                                            Utils.SqlDatos = "UPDATE [ACDATOXPSQL].[dbo].[Datos resoluciones de facturas] SET ResolVigen = 0, CodiModi = '" + UsaRegis + "', " +
                                                                            "FecModi =  CONVERT(DATETIME, '" + Fecha + "', 102) WHERE PrefiFac = N'" + PrefiFacE + "' AND ResolVigen = 'True' AND  NumResol = '" + NumResol + "' ";


                                                            Boolean EstaAct = Conexion.SQLUpdate(Utils.SqlDatos);

                                                            SigoProcFac = 0;

                                                        }
                                                        else
                                                        {
                                                            //Si existe una habilitada, revisamos si el consecutivo generado esta dentro del rango


                                                            double NumFacElect = Convert.ToDouble(NumFacElec);

                                                            double RanIni = Convert.ToDouble(TabResolFactur["RanIni"]);

                                                            double RanFin = Convert.ToDouble(TabResolFactur["RanFin"]);




                                                            if (RanIni <= NumFacElect && NumFacElect <= RanFin)
                                                            {
                                                                //Tomamos akgunos datos, para ser grabados en la factura
                                                                NumResFac = TabResolFactur["NumResol"].ToString();
                                                                TexResFacElec = TabResolFactur["TexResol"].ToString();
                                                                SigoProcFac = 1;
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                //El consecutivo esta fuera del rango aprobado, deshabilitela
                                                                SigoProcFac = 0;
                                                            }
                                                        }
                                                    } // while (TabResolFactur.Read())

                                                    if (SigoProcFac == 0)
                                                    {
                                                        Utils.Informa = "Lo siento pero el prefijo de consecutivos " + "\r";
                                                        Utils.Informa += "tiene vencida la resolución de habilitación de" + "\r";
                                                        Utils.Informa += "la DIAN o el consecutivo generado no le permite";
                                                        Utils.Informa += "generar facturas electrónicas.";
                                                        MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                        break;
                                                    }
                                                }// if(TabResolFactur.HasRows == false)
                                            }//Usn

                                            TabResolFactur.Close();

                                            break;
                                    }//Sinal SWICH


                                    if (SigoProcFac == 1)
                                    {
                                        // 'Haga la actualización de la factura electronica


                                        DateTime FecVenci = DateTime.Now.Date;
                                        FecVenci = FecVenci.AddDays(Convert.ToInt32(LblDiasVenFac.Text));
                                        string FecVenciString = FecVenci.ToString("yyyy-MM-dd");


                                        Utils.SqlDatos = "UPDATE [ACDATOXPSQL].[dbo].[Datos de las facturas realizadas] SET " +
                                        "NumFactura = '" + FunConFac + "', " +
                                        "FechaFac = CONVERT(DATETIME, '" + Fecha + "', 102), " +
                                        //   '****************  Los siguientes campos se agregan el 13 de octubre de 2020 ***********************************
                                        "TexResol = '" + TexResFacElec + "', " +
                                        "FecVenci = CONVERT(DATETIME, '" + FecVenciString + "', 102), " +                                        //Utils.SqlDatos = "UPDATE [ACDATOXPSQL].[dbo].[Datos de las facturas realizadas] SET "+
                                        "NumResol = '" + NumResFac + "', " +
                                        "CodEstaDian = 01, " + //Preparada para enviar a la dian
                                        "NumFacAntes =  '" + FacPorta + "' ," +
                                        "PrefiFacElec =  '" + PrefiFacE + "' " +
                                        "WHERE NumFactura = '" + FacPorta + "' AND PrefiFac = N'" + PfiPor + "' "; //Preguntar

                                        ConectarPortatil();

                                        Boolean EstAct = Conexion.SQLUpdate(Utils.SqlDatos);

                                        if (EstAct) CantiFacElec += 1;



                                    }//Final de SigoProcFac = 1

                                    progressBar.Increment(1);

                                } //Fin While TabCuenConsu

                                Utils.Informa = "Se han preparado " + CantiFacElec + " facturas ";
                                Utils.Informa += "para ser enviadas a la DIAN.";
                                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                progressBar.Minimum = 0;
                                progressBar.Maximum = 1;
                                progressBar.Value = 0;

                            }//Fin if  (TabCuenConsu.HasRows == false)
                            TabCuenConsu.Close();

                        } //Fin Using

                    }
                    else
                    {

                        SigoProcFac = 1;
                    }//Fin   if(DateFecIniFacElec.Value <= DateTime.Now) 

                    if (SigoProcFac == 0)
                    {
                        return;
                    }


                    ConectarPortatil();


                    int Total = 0;

                    string SqlCuenConsuCon = "SELECT count(*) as TotalRegis " +
                    "FROM [ACDATOXPSQL].[dbo].[Datos cuentas de consumos] INNER JOIN [Datos de las facturas realizadas] ON " +
                    " [ACDATOXPSQL].[dbo].[Datos cuentas de consumos].CuenNum = [Datos de las facturas realizadas].NumCuenFac INNER JOIN " +
                    " [ACDATOXPSQL].[dbo].[Datos del Paciente] ON [Datos cuentas de consumos].HistoNum = [Datos del Paciente].HistorPaci " +
                    "WHERE ([Datos de las facturas realizadas].PrefiFac = N'" + PfiPor + "') AND" +
                    "([Datos de las facturas realizadas].ConsoFac = 'False' ) AND " +
                    "([Datos de las facturas realizadas].FechaFac >= CONVERT(DATETIME, '" + FecIniPro + "', 102)) AND " +
                    "([Datos de las facturas realizadas].FechaFac <= CONVERT(DATETIME, '" + FecFinPro + "', 102))";

                    reader = Conexion.SQLDataReader(SqlCuenConsuCon);

                    if (reader.HasRows)
                    {
                        reader.Read();

                        Total = Convert.ToInt32(reader["TotalRegis"]);

                        if (Total != 0)
                        {
                            progressBar.Minimum = 1;
                            progressBar.Maximum = Total;
                        }

                    }
                    else
                    {
                        progressBar.Minimum = 0;
                        progressBar.Maximum = 1;
                        progressBar.Value = 0;
                    }

                    if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();






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

                   "WHERE ([Datos de las facturas realizadas].PrefiFac = N'" + PfiPor + "') AND" +
                   "([Datos de las facturas realizadas].ConsoFac = 'False' ) AND " +
                   "([Datos de las facturas realizadas].FechaFac >= CONVERT(DATETIME, '" + FecIniPro + "', 102)) AND " +
                   "([Datos de las facturas realizadas].FechaFac <= CONVERT(DATETIME, '" + FecFinPro + "', 102))";

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
                            byte[] bytes;
                            byte[] bytes2;
                            List<SqlParameter> parameters = new List<SqlParameter>();
                            string Tipo = "null";
                            string Tipo2 = "null";

                            while (TabCuenConsu.Read())
                            {
                                Tipo = "null";
                                Tipo2 = "null";
                                parameters.Clear();
                                TolFacEx += 1;

                                //'Revisamos si el número de historia existe

                                HiBusCue = TabCuenConsu["HistoNum"].ToString();
                                CuenBusCue = TabCuenConsu["CuenNum"].ToString();
                                FacBusCue = TabCuenConsu["NumFactura"].ToString();
                                TiDGr = TabCuenConsu["TipoIden"].ToString();
                                NumDgr = TabCuenConsu["NumIden"].ToString();
                                ContiPro = 0;

                                SqlPaciCentra = "SELECT * FROM [ACDATOXPSQL].[dbo].[Datos del Paciente] ";
                                SqlPaciCentra += "Where HistorPaci = N'" + HiBusCue + "'";


                                SqlDataReader TabPaciCentra;

                                ConectarCentral();

                                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                                {
                                    SqlCommand command2 = new SqlCommand(SqlPaciCentra, connection2);
                                    command2.Connection.Open();
                                    TabPaciCentra = command2.ExecuteReader();

                                    if (TabPaciCentra.HasRows == false)
                                    {
                                        //Validamos si el usuario existe con otra historia y la misma cedula en la central

                                        SqlPaciCentraCC = "SELECT * FROM [ACDATOXPSQL].[dbo].[Datos del Paciente] ";
                                        SqlPaciCentraCC += "Where TipoIden = N'" + TiDGr + "'  and NumIden = N'" + NumDgr + "' ";

                                        SqlDataReader TabPaciCentraCC;


                                        ConectarCentral();
                                        using (SqlConnection connection3 = new SqlConnection(Conexion.conexionSQL))
                                        {
                                            SqlCommand command3 = new SqlCommand(SqlPaciCentraCC, connection3);
                                            command3.Connection.Open();
                                            TabPaciCentraCC = command3.ExecuteReader();

                                            if (TabPaciCentraCC.HasRows == false)
                                            {
                                                //Debe crear la historia en la base de datos central



                                                if (string.IsNullOrWhiteSpace(TabCuenConsu["Huella1"].ToString()) || TabCuenConsu["Huella1"].ToString() == null)
                                                {
                                                    bytes = (byte[])(null);
                                                    Tipo = "null";
                                                }
                                                else
                                                {
                                                    bytes = (byte[])(TabCuenConsu["Huella1"]);
                                                    Tipo = "@Huella1";

                                                    parameters.Add(new SqlParameter("@Huella1", SqlDbType.VarBinary) { Value = bytes });

                                                }

                                                if (string.IsNullOrWhiteSpace(TabCuenConsu["Huella2"].ToString()) || TabCuenConsu["Huella2"].ToString() == null)
                                                {
                                                    bytes2 = (byte[])(null);
                                                    Tipo2 = "null";
                                                }
                                                else
                                                {
                                                    bytes2 = (byte[])(TabCuenConsu["Huella2"]);
                                                    Tipo2 = "@Huella2";

                                                    parameters.Add(new SqlParameter("@Huella2", SqlDbType.VarBinary) { Value = bytes2 });

                                                }

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
                                                "TipoCuenta," +
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
                                                "CodiRegis," +
                                                "FecRegis," +
                                                "CodiModi," +
                                                "FecModi," +
                                                "NivEducUs," +
                                                "TelCelular," +
                                                "NomEmeal," +
                                                "CodSeSocial," +
                                                "DebeDere," +
                                                "UltiPeso," +
                                                "FecPeso," +
                                                "CodRgPes," +
                                                "UltiTalla," +
                                                "FecTalla," +
                                                "CodRgTal," +
                                                "Huella," +
                                                "Huella1," +
                                                "Huella2," +
                                                "CodPrefi," +
                                                "NomDepenLabo," +
                                                "CodARLLabo," +
                                                "CodAFPLabo," +
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
                                                 $"{Conexion.ValidarHoraNula(TabCuenConsu["HoraNaci"].ToString())}" +
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
                                                 "'" + TabCuenConsu["TipoCuentaFactura"].ToString() + "'," + // 'Cambio del 19 de noviembre
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
                                                 $"{Conexion.ValidarHoraNula(TabCuenConsu["HoraEntrada"].ToString())}" +
                                                 $"{ValidarFechaNula(TabCuenConsu["FecUltima"].ToString())}" +
                                                 "'" + TabCuenConsu["ArchivoViene"].ToString() + "'," +
                                                 "'" + TabCuenConsu["Muerto"].ToString() + "'," +
                                                 "'" + TabCuenConsu["PreInscripcion"].ToString() + "'," +
                                                 "'" + TabCuenConsu["CoberSalud"].ToString() + "'," +
                                                 "'" + TabCuenConsu["PacienteRegistra"].ToString() + "'," + //Llevan ese Fields
                                                 $"{ValidarFechaNula(TabCuenConsu["PaciFecRegis"].ToString())}" +
                                                 "'" + TabCuenConsu["PacienteModifica"].ToString() + "'," + //Llevan ese Fields
                                                 $"{ValidarFechaNula(TabCuenConsu["PaciFecModi"].ToString())}" +
                                                 "'" + TabCuenConsu["NivEducUs"].ToString() + "'," +
                                                 "'" + TabCuenConsu["TelCelular"].ToString() + "'," +
                                                 "'" + TabCuenConsu["NomEmeal"].ToString() + "'," +
                                                 "'" + TabCuenConsu["CodSeSocial"].ToString() + "'," +
                                                 "'" + TabCuenConsu["DebeDere"].ToString() + "'," +
                                                 "'" + TabCuenConsu["UltiPeso"].ToString() + "'," +
                                                 $"{ValidarFechaNula(TabCuenConsu["FecPeso"].ToString())}" +
                                                 "'" + TabCuenConsu["CodRgPes"].ToString() + "'," +
                                                 "'" + TabCuenConsu["UltiTalla"].ToString() + "'," +
                                                 $"{ValidarFechaNula(TabCuenConsu["FecTalla"].ToString())}" +
                                                 "'" + TabCuenConsu["CodRgTal"].ToString() + "'," +
                                                 "'" + TabCuenConsu["Huella"].ToString() + "'," +
                                                //"CONVERT(varbinary,'" + TabCuenConsu["Huella1"].ToString() + "')," +
                                                "" + Tipo + "," +
                                                "" + Tipo2 + "," +
                                                //"CONVERT(varbinary,'" + TabCuenConsu["Huella2"].ToString() + "')," +
                                                 "'" + TabCuenConsu["CodPrefi"].ToString() + "'," + //Grabe el código del portatil, mientras se agega esto en el código de creación de historias clinicas
                                                 "'" + TabCuenConsu["NomDepenLabo"].ToString() + "'," +
                                                 "'" + TabCuenConsu["CodARLLabo"].ToString() + "'," +
                                                 "'" + TabCuenConsu["CodAFPLabo"].ToString() + "'," +
                                                "'" + TabCuenConsu["Discapacidad"].ToString() + "'," +
                                                "'" + TabCuenConsu["NomCargoLabo"].ToString() + "'," +
                                                "'" + TabCuenConsu["GruSangui"].ToString() + "'," +
                                                "'" + TabCuenConsu["RhPaciente"].ToString() + "'" +
                                                ")";

                                                Boolean RegisPasciente = Conexion.SqlInsert(Utils.SqlDatos,parameters);

                                                if (RegisPasciente)
                                                {
                                                    int TolPas = Convert.ToInt32(TxtCanhisForm.Text) + 1;
                                                    TxtCanhisForm.Text = Convert.ToString(TolPas);
                                                    ContiPro = 1;
                                                }

                                  

                                            }
                                            else
                                            {
                                                TabPaciCentraCC.Read();
                                                Utils.Informa = "Por favor tome nota. El número de historia " + HiBusCue + "\r";
                                                Utils.Informa += "No existe en la central, pero existe la No." + TabPaciCentraCC["HistorPaci"].ToString() + "\r";
                                                Utils.Informa += "que tienen el mismo documento " + TiDGr + ": " + NumDgr + "\r";
                                                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                ContiPro = 0;
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

                                    ConectarCentral();
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
                                            $"{Conexion.ValidarHoraNula(TabCuenConsu["HorApertura"].ToString())}" +
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
                                            $"{Conexion.ValidarHoraNula(TabCuenConsu["HorEntrada"].ToString())}" +
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
                                            $"{Conexion.ValidarHoraNula(TabCuenConsu["HorSalida"].ToString())}" +
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

                                            ConectarCentral();
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
                                                    "CodEstaDian," +
                                                    "NumResol," +
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
                                                    $"{ValidarFechaNula(TabCuenConsu["fesale"].ToString())}" +
                                                    "'" + TabCuenConsu["codisale"].ToString() + "'," +
                                                    "'" + TabCuenConsu["Radicada"].ToString() + "'," +
                                                    $"{ValidarFechaNula(TabCuenConsu["Fecradifa"].ToString())}" +
                                                    "'" + TabCuenConsu["Codradifa"].ToString() + "'," +
                                                    "'" + TabCuenConsu["Codrecifa"].ToString() + "'," +
                                                    $"{Conexion.ValidarHoraNula(TabCuenConsu["HoraReg"].ToString())}" +
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
                                                    "'" + TabCuenConsu["CodEstaDian"].ToString() + "'," +
                                                    "'" + TabCuenConsu["NumResol"].ToString() + "',";
                                                    if (TabCuenConsu["TipoDocTab"].ToString() == "1")
                                                    {
                                                        Utils.SqlDatos += "'" + TabCuenConsu["PrefiFacElec"].ToString() + "')";
                                                    }
                                                    else
                                                    {
                                                        Utils.SqlDatos += "'" + TabCuenConsu["PrefiFac"].ToString() + "')";
                                                    }

                                                    Boolean RegisFactur = Conexion.SqlInsert(Utils.SqlDatos);

                                                    //Procedemoss a agregar los consumos, para ello buscamos los consumos en la tabla de registros del portatil

                                                    SqlConsumos = "SELECT * FROM [ACDATOXPSQL].[dbo].[Datos registros de consumos] ";
                                                    SqlConsumos = SqlConsumos + "Where CuenConsu = N'" + CuenBusCue + "'";

                                                    ConectarPortatil();

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

                                                            ConectarCentral();

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
                                                                        $"{Conexion.ValidarHoraNula(TabCuenConsu["HoraRegistro"].ToString())}" +
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
                                                                        "'" + IteNumCon + "'," +
                                                                        "'" + TabConsumos["CodiRegis"].ToString() + "'," +
                                                                        $"{ValidarFechaNula(TabConsumos["FecRegis"].ToString())}" +
                                                                        $"{Conexion.ValidarHoraNula(TabCuenConsu["Horaregis"].ToString())}" +
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


                                progressBar.Increment(1);

                            }//While

                            TxtCanFacFor.Text = Convert.ToString(TolFacEx);

                            Utils.Informa = "El proceso ha terminado satisfactoriamente" + "\r";
                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            progressBar.Minimum = 0;
                            progressBar.Maximum = 1;
                            progressBar.Value = 0;

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
                progressBar.Minimum = 0;
                progressBar.Maximum = 1;
                progressBar.Value = 0;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//Fin BOTON


        #endregion

        private void FrmExportSedeCentral_Load(object sender, EventArgs e)
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
                Utils.Informa += "al cargar el formulario FrmExportSedeCentral "+ "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
