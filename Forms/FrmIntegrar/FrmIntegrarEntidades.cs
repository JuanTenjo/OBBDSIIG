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
    public partial class FrmIntegrarEntidades : Form
    {
        public FrmIntegrarEntidades()
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

        private void FrmIntegrarEntidades_Load(object sender, EventArgs e)
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
                if (IntegrarEntidades.IsBusy != true) //Si el proceso esta corriendo no puede voler a iniciarse 
                {
                    

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

                    string PfiCen = TxtPrefiCenFor.Text;
                    string PfiPor = TxtPrefiPorFor.Text;

                    Utils.Titulo01 = "Control para integrar datos" + "\r";
                    Utils.Informa = "¿Usted desea iniciar el proceso de integración" + "\r";
                    Utils.Informa += "de las entidades en la instancia del" + "\r";
                    Utils.Informa += "servidor central a la instancia del portatil?" + "\r";
                    var res = MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (res == DialogResult.Yes)
                    {

                        TxtCanAmdTerFor.Text = "0";
                        TxtCanEmpForm.Text = "0";
                        TxtCanEmpTerExis.Text = "0";
                        globalCanAmdTerFor = 0;
                        globalCanEmpForm = 0;
                        globalCanEmpTerExis = 0;

                        ConectarCentral();

                        string SqlEmpTerCount = "SELECT count(*) as TotalRegis " + 
                        "FROM [ACDATOXPSQL].[dbo].[Datos empresas y terceros]";

                        int Total = 0;

                        SqlDataReader reader = Conexion.SQLDataReader(SqlEmpTerCount);

                        if (reader.HasRows)
                        {
                            reader.Read();

                            Total = Convert.ToInt32(reader["TotalRegis"]);

                            if (Total != 0)
                            {

                                LblDetener.Visible = true;
                                BtnDetener.Visible = true;

                                LblIntegrar.Visible = false;
                                BtnBuscarPacientes.Visible = false;


                                ProgressBar.Minimum = 1;
                                ProgressBar.Maximum = Total;
                                LblTotal.Text = Total.ToString();
                                IntegrarEntidades.RunWorkerAsync();

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
    

                        if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();


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

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            if (IntegrarEntidades.IsBusy == true) //Si el proceso esta corriendo no puede voler a iniciarse 
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


        int globalCanAmdTerFor = 0;
        int globalCanEmpForm = 0;
        int globalCanEmpTerExis = 0;


        private void IntegrarEntidades_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                string SqlEmpTer, SqlEmpTerCentra, CodAdm;
                int ContiPro;

                globalCanAmdTerFor = 0;


                ConectarCentral();

                SqlEmpTer = "SELECT CarAdmin, CodiMinSalud, PerEmpre, NomAdmin, ProgrAmin, TipoDocu, NumDocu, ManualTari, TarifAplica, DirecAdmin, TelePrinci, TeleFax, CiudAdmin, DptoAdmin, " +
                "PaisAdmin, Contacto, NumCel, Contrato, MontoContrato, SaldoMonto, IniciaContrato, FinContrato, CuenContaDeuda, CodRuPres, CodTipoPaciente, TipoEmpre, " +
                "CentroCuenta, RegimenAdmin, NomPlan, NaturalJuridica, HabilEmp, ActiReali, CoCoPago, PaNivel1, PaNivel2, PaNivel3, CodiRegis, FecRegis, CodiModi, FecModi, " +
                "TopeFacUs, CueContDeudaRad, AbonoSinAolica, AtenConOrdFac, SucuDoc, DigTribu, " +
                "CodPresVig01, CodPresVig02, DisContIngre, CContaDisIng, DirElectro, PagWebPro, ValNumAuto, PreForFac " +
                "FROM [ACDATOXPSQL].[dbo].[Datos empresas y terceros]";

                SqlDataReader TabEmpTer;

                using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command = new SqlCommand(SqlEmpTer, connection);
                    command.Connection.Open();
                    TabEmpTer = command.ExecuteReader();

                    if (TabEmpTer.HasRows == false)
                    {
                        Utils.Informa = "Lo siento pero no hay datos " + "\r";
                        Utils.Informa += "para exportar o modificar en , " + "\r";
                        Utils.Informa += "Datos empresas y terceros." + "\r";
                        MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        while (TabEmpTer.Read())
                        {

                            if (IntegrarEntidades.CancellationPending == true)
                            {
                                e.Cancel = true;
                                Utils.Titulo01 = "Control de ejecución";
                                Utils.Informa = "Se cancelo la operacion " + "\r";
                                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }


                            globalCanAmdTerFor += 1;

                            //Revisamos si el código interno de la entidad existe
                            CodAdm = TabEmpTer["CarAdmin"].ToString();
                            ContiPro = 0;


                            SqlEmpTerCentra = "SELECT [Datos empresas y terceros].* ";
                            SqlEmpTerCentra = SqlEmpTerCentra + "FROM [ACDATOXPSQL].[dbo].[Datos empresas y terceros] ";
                            SqlEmpTerCentra = SqlEmpTerCentra + "WHERE (CarAdmin = '" + CodAdm + "')";

                            ConectarPortatil();

                            SqlDataReader TabEmpTerCentra;

                            using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command2 = new SqlCommand(SqlEmpTerCentra, connection2);
                                command2.Connection.Open();
                                TabEmpTerCentra = command2.ExecuteReader();

                                if (TabEmpTerCentra.HasRows == false)
                                {
                                    Utils.SqlDatos = "INSERT INTO [ACDATOXPSQL].[dbo].[Datos empresas y terceros]" +
                                    "(" +
                                    "CarAdmin," +
                                    "CodiMinSalud," +
                                    "PerEmpre," +
                                    "NomAdmin," +
                                    "ProgrAmin," +
                                    "TipoDocu," +
                                    "NumDocu," +
                                    "DigTribu," +
                                    "SucuDoc," +
                                    "ManualTari," +
                                    "TarifAplica," +
                                    "DirecAdmin," +
                                    "TelePrinci," +
                                    "TeleFax," +
                                    "CiudAdmin," +
                                    "DptoAdmin," +
                                    "PaisAdmin," +
                                    "Contacto," +
                                    "NumCel," +
                                    "Contrato," +
                                    "MontoContrato," +
                                    "SaldoMonto," +
                                    "IniciaContrato," +
                                    "FinContrato," +
                                    "CuenContaDeuda," +
                                    "CodRuPres," +
                                    "CodTipoPaciente," +
                                    "TipoEmpre," +
                                    "CentroCuenta," +
                                    "RegimenAdmin," +
                                    "NomPlan," +
                                    "NaturalJuridica," +
                                    "HabilEmp," +
                                    "ActiReali," +
                                    "CoCoPago," +
                                    "PaNivel1," +
                                    "PaNivel2," +
                                    "PaNivel3," +
                                    "CodiRegis," +
                                    "FecRegis," +
                                    "CodiModi," +
                                    "FecModi," +
                                    "TopeFacUs," +
                                    "CueContDeudaRad," +
                                    "AbonoSinAolica," +
                                    "AtenConOrdFac," +
                                    "CodPresVig01," +
                                    "CodPresVig02," +
                                    "DisContIngre," +
                                    "CContaDisIng," +
                                    "DirElectro," +
                                    "PagWebPro," +
                                    "ValNumAuto," +
                                    "PreForFac" +
                                    ")" +
                                    "VALUES" +
                                    "(" +
                                     "'" + TabEmpTer["CarAdmin"].ToString() + "'," +
                                     "'" + TabEmpTer["CodiMinSalud"].ToString() + "'," +
                                     "'" + TabEmpTer["PerEmpre"].ToString() + "'," +
                                     "'" + TabEmpTer["NomAdmin"].ToString() + "'," +
                                     "'" + TabEmpTer["ProgrAmin"].ToString() + "'," +
                                     "'" + TabEmpTer["TipoDocu"].ToString() + "'," +
                                     "'" + TabEmpTer["NumDocu"].ToString() + "'," +
                                     "'" + TabEmpTer["DigTribu"].ToString() + "'," +
                                     "'" + TabEmpTer["SucuDoc"].ToString() + "'," +
                                     "'" + TabEmpTer["ManualTari"].ToString() + "'," +
                                     "'" + TabEmpTer["TarifAplica"].ToString() + "'," +
                                     "'" + TabEmpTer["DirecAdmin"].ToString() + "'," +
                                     "'" + TabEmpTer["TelePrinci"].ToString() + "'," +
                                     "'" + TabEmpTer["TeleFax"].ToString() + "'," +
                                     "'" + TabEmpTer["CiudAdmin"].ToString() + "'," +
                                     "'" + TabEmpTer["DptoAdmin"].ToString() + "'," +
                                     "'" + TabEmpTer["PaisAdmin"].ToString() + "'," +
                                     "'" + TabEmpTer["Contacto"].ToString() + "'," +
                                     "'" + TabEmpTer["NumCel"].ToString() + "'," +
                                     "'" + TabEmpTer["Contrato"].ToString() + "'," +
                                     "'" + TabEmpTer["MontoContrato"].ToString() + "'," +
                                     "'" + TabEmpTer["SaldoMonto"].ToString() + "'," +
                                     $"{Conexion.ValidarFechaNula(TabEmpTer["IniciaContrato"].ToString())}" +
                                     $"{Conexion.ValidarFechaNula(TabEmpTer["FinContrato"].ToString())}" +
                                     "'" + TabEmpTer["CuenContaDeuda"].ToString() + "'," +
                                     "'" + TabEmpTer["CodRuPres"].ToString() + "'," +
                                     "'" + TabEmpTer["CodTipoPaciente"].ToString() + "'," +
                                     "'" + TabEmpTer["TipoEmpre"].ToString() + "'," +
                                     "'" + TabEmpTer["CentroCuenta"].ToString() + "'," +
                                     "'" + TabEmpTer["RegimenAdmin"].ToString() + "'," +
                                     "'" + TabEmpTer["NomPlan"].ToString() + "'," +
                                     "'" + TabEmpTer["NaturalJuridica"].ToString() + "'," +
                                     "'" + TabEmpTer["HabilEmp"].ToString() + "'," +
                                     "'" + TabEmpTer["ActiReali"].ToString() + "'," +
                                     "'" + TabEmpTer["CoCoPago"].ToString() + "'," +
                                     "'" + TabEmpTer["PaNivel1"].ToString() + "'," +
                                     "'" + TabEmpTer["PaNivel2"].ToString() + "'," +
                                     "'" + TabEmpTer["PaNivel3"].ToString() + "'," +
                                     "'" + TabEmpTer["CodiRegis"].ToString() + "'," +
                                     $"{Conexion.ValidarFechaNula(TabEmpTer["FecRegis"].ToString())}" +
                                     "'" + TabEmpTer["CodiModi"].ToString() + "'," +
                                     $"{Conexion.ValidarFechaNula(TabEmpTer["FecModi"].ToString())}" +
                                     "'" + TabEmpTer["TopeFacUs"].ToString() + "'," +
                                     "'" + TabEmpTer["CueContDeudaRad"].ToString() + "'," +
                                     "'" + TabEmpTer["AbonoSinAolica"].ToString() + "'," +
                                     "'" + TabEmpTer["AtenConOrdFac"].ToString() + "'," +
                                     "'" + TabEmpTer["CodPresVig01"].ToString() + "'," +
                                     "'" + TabEmpTer["CodPresVig02"].ToString() + "'," +
                                     "'" + TabEmpTer["DisContIngre"].ToString() + "'," +
                                     "'" + TabEmpTer["CContaDisIng"].ToString() + "'," +
                                     "'" + TabEmpTer["DirElectro"].ToString() + "'," +
                                     "'" + TabEmpTer["PagWebPro"].ToString() + "'," +
                                     "'" + TabEmpTer["ValNumAuto"].ToString() + "'," +
                                     "'" + TabEmpTer["PreForFac"].ToString() + "' " +
                                    ")";


                                    Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos);

                                    if (Regis)
                                    {
                                        globalCanEmpForm += 1;
                                    }

                                    ContiPro = 1;


                                }
                                else
                                {

                                    //Modifique los datos
                                    Utils.SqlDatos = $"UPDATE [ACDATOXPSQL].[dbo].[Datos empresas y terceros] SET " +
                                    "CodiMinSalud = '" + TabEmpTer["CodiMinSalud"].ToString() + "', " +
                                    "PerEmpre = '" + TabEmpTer["PerEmpre"].ToString() + "', " +
                                    "NomAdmin = '" + TabEmpTer["NomAdmin"].ToString() + "', " +
                                    "ProgrAmin = '" + TabEmpTer["ProgrAmin"].ToString() + "', " +
                                    "TipoDocu = '" + TabEmpTer["TipoDocu"].ToString() + "', " +
                                    "NumDocu = '" + TabEmpTer["NumDocu"].ToString() + "', " +
                                    "DigTribu = '" + TabEmpTer["DigTribu"].ToString() + "', " +
                                    "SucuDoc = '" + TabEmpTer["SucuDoc"].ToString() + "', " +
                                    "ManualTari = '" + TabEmpTer["ManualTari"].ToString() + "', " +
                                    "TarifAplica = '" + TabEmpTer["TarifAplica"].ToString() + "', " +
                                    "DirecAdmin = '" + TabEmpTer["DirecAdmin"].ToString() + "', " +
                                    "TelePrinci = '" + TabEmpTer["TelePrinci"].ToString() + "', " +
                                    "TeleFax = '" + TabEmpTer["TeleFax"].ToString() + "', " +
                                    "CiudAdmin = '" + TabEmpTer["CiudAdmin"].ToString() + "', " +
                                    "DptoAdmin = '" + TabEmpTer["DptoAdmin"].ToString() + "', " +
                                    "PaisAdmin = '" + TabEmpTer["PaisAdmin"].ToString() + "', " +
                                    "Contacto = '" + TabEmpTer["Contacto"].ToString() + "', " +
                                    "NumCel = '" + TabEmpTer["NumCel"].ToString() + "', " +
                                    "Contrato = '" + TabEmpTer["Contrato"].ToString() + "', " +
                                    "MontoContrato = '" + TabEmpTer["MontoContrato"].ToString() + "', " +
                                    "SaldoMonto = '" + TabEmpTer["SaldoMonto"].ToString() + "', " +
                                    $"IniciaContrato = {Conexion.ValidarFechaNula(TabEmpTer["IniciaContrato"].ToString())} " +
                                    $"FinContrato = {Conexion.ValidarFechaNula(TabEmpTer["FinContrato"].ToString())} " +
                                    "CuenContaDeuda = '" + TabEmpTer["CuenContaDeuda"].ToString() + "', " +
                                    "CodRuPres = '" + TabEmpTer["CodRuPres"].ToString() + "', " +
                                    "CodTipoPaciente = '" + TabEmpTer["CodTipoPaciente"].ToString() + "', " +
                                    "TipoEmpre = '" + TabEmpTer["TipoEmpre"].ToString() + "', " +
                                    "CentroCuenta = '" + TabEmpTer["CentroCuenta"].ToString() + "', " +
                                    "RegimenAdmin = '" + TabEmpTer["RegimenAdmin"].ToString() + "', " +
                                    "NomPlan = '" + TabEmpTer["NomPlan"].ToString() + "', " +
                                    "NaturalJuridica = '" + TabEmpTer["NaturalJuridica"].ToString() + "', " +
                                    "HabilEmp = '" + TabEmpTer["HabilEmp"].ToString() + "', " +
                                    "ActiReali = '" + TabEmpTer["ActiReali"].ToString() + "', " +
                                    "CoCoPago = '" + TabEmpTer["CoCoPago"].ToString() + "', " +
                                    "PaNivel1 = '" + TabEmpTer["PaNivel1"].ToString() + "', " +
                                    "PaNivel2 = '" + TabEmpTer["PaNivel2"].ToString() + "', " +
                                    "PaNivel3 = '" + TabEmpTer["PaNivel3"].ToString() + "', " +
                                    "CodiRegis = '" + TabEmpTer["CodiRegis"].ToString() + "', " +
                                    $"FecRegis = {Conexion.ValidarFechaNula(TabEmpTer["FecRegis"].ToString())} " +
                                    "CodiModi = '" + TabEmpTer["CodiModi"].ToString() + "', " +
                                    $"FecModi = {Conexion.ValidarFechaNula(TabEmpTer["FecModi"].ToString())} " +
                                    "TopeFacUs = '" + TabEmpTer["TopeFacUs"].ToString() + "', " +
                                    "CueContDeudaRad = '" + TabEmpTer["CueContDeudaRad"].ToString() + "', " +
                                    "AbonoSinAolica = '" + TabEmpTer["AbonoSinAolica"].ToString() + "', " +
                                    "AtenConOrdFac = '" + TabEmpTer["AtenConOrdFac"].ToString() + "', " +
                                    "CodPresVig01 = '" + TabEmpTer["CodPresVig01"].ToString() + "', " +
                                    "CodPresVig02 = '" + TabEmpTer["CodPresVig02"].ToString() + "', " +
                                    "DisContIngre = '" + TabEmpTer["DisContIngre"].ToString() + "', " +
                                    "CContaDisIng = '" + TabEmpTer["CContaDisIng"].ToString() + "', " +
                                    "DirElectro = '" + TabEmpTer["DirElectro"].ToString() + "', " +
                                    "PagWebPro = '" + TabEmpTer["PagWebPro"].ToString() + "', " +
                                    "ValNumAuto = '" + TabEmpTer["ValNumAuto"].ToString() + "', " +
                                    "PreForFac = '" + TabEmpTer["PreForFac"].ToString() + "' " +
                                    "WHERE (CarAdmin = '" + CodAdm + "')";

                                    Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos);

                                    if (ActControl)
                                    {
                                        globalCanEmpTerExis += 1;
                                    }

                                    ContiPro = 1;

                                }//'Final deif (TabPlacaCen.HasRows == false)

                                TabEmpTerCentra.Close();

                            }//USing

                            IntegrarEntidades.ReportProgress(globalCanAmdTerFor);

                        }//While

                    }

                    TabEmpTer.Close();

                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "después de hacer click sobre el botón integrar" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                IntegrarEntidades.CancelAsync();
            }
        }

        private void IntegrarEntidades_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                if (IntegrarEntidades.CancellationPending == false)
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

        private void IntegrarEntidades_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

                TxtCanAmdTerFor.Text = globalCanAmdTerFor.ToString();
                TxtCanEmpForm.Text = globalCanEmpForm.ToString();
                TxtCanEmpTerExis.Text = globalCanEmpTerExis.ToString();



                LblDetener.Visible = false;
                BtnDetener.Visible = false;

                LblIntegrar.Visible = true;
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
                    IntegrarEntidades.WorkerSupportsCancellation = true;
                    IntegrarEntidades.CancelAsync();

                    ProgressBar.Minimum = 0;
                    ProgressBar.Maximum = 1;
                    ProgressBar.Value = 0;

                    LblCantidad.Text = "0";
                    LblTotal.Text = "0";

                    LblDetener.Visible = false;
                    BtnDetener.Visible = false;

                    LblIntegrar.Visible = true;
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
