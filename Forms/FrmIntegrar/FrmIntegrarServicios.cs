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
    public partial class FrmIntegrarServicios : Form
    {
        public FrmIntegrarServicios()
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
                    this.Close();
                    return;
                }
                else
                {
                    lblCodigoUser.Text = Utils.codUsuario;
                    lblNombreUser.Text = Utils.nomUsuario;
                    LblCodEntiFac.Text = Utils.codUnicoEmpresa; // '*********************** Se agrega a partir del 13 de octubre de 2020 *********************************

                    TxtInstanCenFor.Text = Utils.InstanCenFor;
                    TxtPrefiPorFor.Text = Utils.PrefiPorFor;
                    TxtPrefiCenFor.Text = Utils.PrefiCenFor;
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

        private void FrmIntegrarServicios_Load(object sender, EventArgs e)
        {
            try
            {
                CargarDatosUser();
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

        private void BtnBuscarPacientes_Click(object sender, EventArgs e)
        {

            try
            {

                if (IntegrarServicios.IsBusy != true) //Si el proceso esta corriendo no puede voler a iniciarse 
                {

                    int maximoBarra = 0;
       

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

                    Utils.Titulo01 = "Control para integrar servicios";
                    Utils.Informa = "¿Usted desea iniciar el proceso de integración" + "\r";
                    Utils.Informa += "de los servicios en la instancia del" + "\r";
                    Utils.Informa += "servidor central a la instancia del portatil.?" + "\r";
                    var res = MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (res == DialogResult.Yes)
                    {


                        TxtCanServiFor.Text = "0";
                        TxtCanServiForm.Text = "0";
                        TxtCanServiExis.Text = "0";
                        TxtCantiProFar.Text = "0";
                        TxtProFarAgrega.Text = "0";
                        TxtCanProforVal.Text = "0";
                        globalCanServiFor = 0;
                        globalCanServiForm = 0;
                        globalCanServiExis = 0;
                        globalCantiProFar = 0;
                        globalProFarAgrega = 0;
                        globalCanProforVal = 0;


                        ConectarCentral();

                        string SqlserviCount = "SELECT count(*) as TotalRegis FROM [ACDATOXPSQL].[dbo].[Datos catalogo de servicios] ";

                        int Total = 0;

                        SqlDataReader reader = Conexion.SQLDataReader(SqlserviCount);

                        if (reader.HasRows)
                        {
                            reader.Read();

                            Total = Convert.ToInt32(reader["TotalRegis"]);

                            if (Total != 0)
                            {
                                ProgressBar.Minimum = 1;
                                maximoBarra += Total;
                            }


                        }


                        if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();

                        // Vamos a actualizar los productos de farmacia.  ******************* lo implementa HERNANDO EL 06 DE MAYO DE 2020 ****
                        //'Se require porque la formulacion medica se basa en esa tabla

                        ConectarCentral();


                        string SqlProFarCenCount = "SELECT count(*) as TotalRegis ";
                        SqlProFarCenCount = SqlProFarCenCount + "FROM [BDFARMA].[dbo].[Datos productos farmaceuticos] ";

                        Total = 0;

                        reader = Conexion.SQLDataReader(SqlProFarCenCount);

                        if (reader.HasRows)
                        {
                            reader.Read();

                            Total = Convert.ToInt32(reader["TotalRegis"]);

                            if (Total != 0)
                            {
                                ProgressBar.Minimum = 1;
                                maximoBarra += Total;
                            }


                        }


                        if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();


                        if (maximoBarra >= 1)
                        {

                            LblDetener.Visible = true;
                            BtnDetener.Visible = true;

                            LblIntegrar.Visible = false;
                            BtnBuscarPacientes.Visible = false;


                            ProgressBar.Minimum = 1;
                            ProgressBar.Maximum = maximoBarra;

                            LblTotal.Text = maximoBarra.ToString();
                            IntegrarServicios.RunWorkerAsync();

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
                Utils.Informa += "despues de hacer click en integrar " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                ProgressBar.Minimum = 0;
                ProgressBar.Maximum = 1;
                ProgressBar.Value = 0;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            if (IntegrarServicios.IsBusy == true) //Si el proceso esta corriendo no puede voler a iniciarse 
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

        int globalCantiProFar;
        int contador;
        int globalCanProforVal;
        int globalProFarAgrega;
        int globalCanServiExis;
        int globalCanServiForm;
        int globalCanServiFor;
        private void IntegrarServicios_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                string Sqlservi, SqlServiCentra, SqlProFarPor = "", SqlProFarCen = "", CodServi = "", CodCaPro = "";
                int ContiPro;

                contador = 0;

                ConectarCentral();

                Utils.SqlDatos = "SELECT * FROM [BDFARMA].[dbo].[Datos casas laboratorios]";

                SqlDataReader CasasLaboratoriosCen, CasasLaboratoriosPor;

                using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command = new SqlCommand(Utils.SqlDatos, connection);
                    command.Connection.Open();
                    CasasLaboratoriosCen = command.ExecuteReader();

                    if (CasasLaboratoriosCen.HasRows == false)
                    {
                        Utils.Informa = "Lo siento pero no hay datos" + "\r";
                        Utils.Informa += "para exportar o modificar en , " + "\r";
                        Utils.Informa += "Datos casas laboratorios " + "\r";
                        MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        ConectarPortatil();

                        while (CasasLaboratoriosCen.Read())
                        {
                            //Revisamos si el código interno de la entidad existe
                            CodCaPro = CasasLaboratoriosCen["CodCaPro"].ToString();


                            Utils.SqlDatos = "SELECT * FROM [BDFARMA].[dbo].[Datos casas laboratorios] WHERE CodCaPro = '" + CodCaPro + "'";


                            using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command2 = new SqlCommand(Utils.SqlDatos, connection2);
                                command2.Connection.Open();
                                CasasLaboratoriosPor = command2.ExecuteReader();

                                if (CasasLaboratoriosPor.HasRows == false)
                                {
                                    //'Agregue
                                    Utils.SqlDatos = "INSERT INTO [BDFARMA].[dbo].[Datos casas laboratorios] " +
                                    "(" +
                                    "CodCaPro," +
                                    "NomCaPro," +
                                    "HabilCaPro," +
                                    "CodRegis," +
                                    "FecRegis," +
                                    "FecModi," +
                                    "CodModi" +
                                    ")" +
                                    "VALUES" +
                                    "(" +
                                    "'" + CasasLaboratoriosCen["CodCaPro"].ToString() + "'," +
                                    "'" + CasasLaboratoriosCen["NomCaPro"].ToString().Replace("'", "''") + "'," +
                                    "'" + CasasLaboratoriosCen["HabilCaPro"].ToString() + "'," +
                                    "'" + CasasLaboratoriosCen["CodRegis"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(CasasLaboratoriosCen["FecRegis"].ToString())}" +
                                    $"{Conexion.ValidarFechaNula(CasasLaboratoriosCen["FecModi"].ToString())}" +
                                    "'" + CasasLaboratoriosCen["CodModi"].ToString() + "'" +
                                    ")";

                                    Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos);

                                    ContiPro = 1;

                                }
                                else
                                {

                                    Utils.SqlDatos = $"UPDATE [BDFARMA].[dbo].[Datos casas laboratorios] SET " +
                                    "NomCaPro ='" + CasasLaboratoriosCen["NomCaPro"].ToString().Replace("'", "''") + "', " +
                                    "HabilCaPro ='" + CasasLaboratoriosCen["HabilCaPro"].ToString() + "', " +
                                    "CodRegis ='" + CasasLaboratoriosCen["CodRegis"].ToString() + "', " +
                                    $"FecRegis = {Conexion.ValidarFechaNula(CasasLaboratoriosCen["FecRegis"].ToString())} " +
                                    $"FecModi = {Conexion.ValidarFechaNula(CasasLaboratoriosCen["FecModi"].ToString())} " +
                                    "CodModi ='" + CasasLaboratoriosCen["CodModi"].ToString() + "'" +
                                    "WHERE (CodCaPro = '" + CodCaPro + "') ";

                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);


                                }

                            }

                            CasasLaboratoriosPor.Close();


                        }//While

                        CasasLaboratoriosCen.Close();

                    }//Fin  if (CasasLaboratoriosCen.HasRows == false)

                }//Fin Using

                ConectarCentral();

                Utils.SqlDatos = "SELECT * FROM [BDFARMA].[dbo].[Datos forma farmaceutica]";

                SqlDataReader FormaFamaceuticaCen, FormaFamaceuticaPor;

                string CodForFar;

                using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command = new SqlCommand(Utils.SqlDatos, connection);
                    command.Connection.Open();
                    FormaFamaceuticaCen = command.ExecuteReader();

                    if (FormaFamaceuticaCen.HasRows == false)
                    {
                        Utils.Informa = "Lo siento pero no hay datos" + "\r";
                        Utils.Informa += "para exportar o modificar en , " + "\r";
                        Utils.Informa += "Datos forma farmaceutica " + "\r";
                        MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        ConectarPortatil();

                        while (FormaFamaceuticaCen.Read())
                        {
                            //Revisamos si el código interno de la entidad existe
                            CodForFar = FormaFamaceuticaCen["CodForFar"].ToString();


                            Utils.SqlDatos = "SELECT * FROM [BDFARMA].[dbo].[Datos forma farmaceutica] WHERE CodForFar = '" + CodForFar + "'";


                            using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command2 = new SqlCommand(Utils.SqlDatos, connection2);
                                command2.Connection.Open();
                                FormaFamaceuticaPor = command2.ExecuteReader();

                                if (FormaFamaceuticaPor.HasRows == false)
                                {
                                    //'Agregue
                                    Utils.SqlDatos = "INSERT INTO [BDFARMA].[dbo].[Datos forma farmaceutica] " +
                                    "(" +
                                    "CodForFar," +
                                    "NomForFar," +
                                    "ViaAdminis," +
                                    "CodRegis," +
                                    "FecRegis," +
                                    "FecModi," +
                                    "CodModi" +
                                    ")" +
                                    "VALUES" +
                                    "(" +
                                    "'" + FormaFamaceuticaCen["CodForFar"].ToString() + "'," +
                                    "'" + FormaFamaceuticaCen["NomForFar"].ToString().Replace("'", "''") + "'," +
                                    "'" + FormaFamaceuticaCen["ViaAdminis"].ToString() + "'," +
                                    "'" + FormaFamaceuticaCen["CodRegis"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(FormaFamaceuticaCen["FecRegis"].ToString())}" +
                                    $"{Conexion.ValidarFechaNula(FormaFamaceuticaCen["FecModi"].ToString())}" +
                                    "'" + FormaFamaceuticaCen["CodModi"].ToString() + "'" +
                                    ")";

                                    Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos);

                                    ContiPro = 1;

                                }
                                else
                                {

                                    Utils.SqlDatos = $"UPDATE [BDFARMA].[dbo].[Datos forma farmaceutica] SET " +
                                    "NomForFar ='" + FormaFamaceuticaCen["NomForFar"].ToString().Replace("'", "''") + "', " +
                                    "ViaAdminis ='" + FormaFamaceuticaCen["ViaAdminis"].ToString() + "', " +
                                    "CodRegis ='" + FormaFamaceuticaCen["CodRegis"].ToString() + "', " +
                                    $"FecRegis = {Conexion.ValidarFechaNula(FormaFamaceuticaCen["FecRegis"].ToString())} " +
                                    $"FecModi = {Conexion.ValidarFechaNula(FormaFamaceuticaCen["FecModi"].ToString())} " +
                                    "CodModi ='" + FormaFamaceuticaCen["CodModi"].ToString() + "'" +
                                    "WHERE (CodForFar = '" + CodForFar + "') ";

                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);


                                }

                            }

                            FormaFamaceuticaPor.Close();

                        }//While

                        FormaFamaceuticaCen.Close();

                    }//Fin  if (CasasLaboratoriosCen.HasRows == false)

                }//Fin Using



                ConectarCentral();

                Sqlservi = "SELECT * FROM [ACDATOXPSQL].[dbo].[Datos catalogo de servicios] ";
                Sqlservi = Sqlservi + "ORDER BY CodInterno ";

                SqlDataReader TabServi, TabServiCentra;

                using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command = new SqlCommand(Sqlservi, connection);
                    command.Connection.Open();
                    TabServi = command.ExecuteReader();

                    if (TabServi.HasRows == false)
                    {
                        Utils.Informa = "Lo siento pero no hay datos" + "\r";
                        Utils.Informa += "para exportar o modificar en , " + "\r";
                        Utils.Informa += "Datos catalogo de servicios. " + "\r";
                        MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        ConectarPortatil();
                        while (TabServi.Read())
                        {

                            if (IntegrarServicios.CancellationPending == true)
                            {
                                e.Cancel = true;
                                Utils.Titulo01 = "Control de ejecución";
                                Utils.Informa = "Se cancelo la operacion " + "\r";
                                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }


                            contador += 1;

                            globalCanServiFor += 1;

                            //Revisamos si el código interno de la entidad existe
                            CodServi = TabServi["CodInterno"].ToString();

                            ContiPro = 0;


                            SqlServiCentra = "SELECT [Datos catalogo de servicios].* ";
                            SqlServiCentra += "FROM [ACDATOXPSQL].[dbo].[Datos catalogo de servicios] ";
                            SqlServiCentra += "WHERE (CodInterno = '" + CodServi + "')";


                            using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command2 = new SqlCommand(SqlServiCentra, connection2);
                                command2.Connection.Open();
                                TabServiCentra = command2.ExecuteReader();

                                if (TabServiCentra.HasRows == false)
                                {
                                    //'Agregue
                                    Utils.SqlDatos = "INSERT INTO [ACDATOXPSQL].[dbo].[Datos catalogo de servicios] " +
                                    "(" +
                                    "CodInterno," +
                                    "NomServicio," +
                                    "CodiMedMin," +
                                    "GrupoServi," +
                                    "TipoServi," +
                                    "Finalidad," +
                                    "EsCirugia," +
                                    "ClasiSer," +
                                    "CenCosto," +
                                    "CodiSOAT," +
                                    "CodiISS," +
                                    "CodiCUPS," +
                                    "ValorCUPS," +
                                    "ValorParti," +
                                    "ValorSoat," +
                                    "GrupoSoat," +
                                    "ValorIss," +
                                    "UVRIss," +
                                    "ValorEspecial01," +
                                    "ValorEspecial02," +
                                    "ValorEspecial03," +
                                    "ValorEspecial04," +
                                    "ValorEspecial05," +
                                    "ValorEspecial06," +
                                    "ValorEspecial07," +
                                    "ValorEspecial08," +
                                    "ValorEspecial09," +
                                    "ValorEspecial10," +
                                    "ValorEspecial11," +
                                    "ValorEspecial12," +
                                    "HabilPro," +
                                    "Medicamento," +
                                    "PosMedi," +
                                    "SexAplica," +
                                    "Requisito," +
                                    "NivelActi," +
                                    "ActiSiste," +
                                    "PrograPyP," +
                                    "ActuaValUni," +
                                    "GrupoLabo," +
                                    "VezReal," +
                                    "CodiRegis," +
                                    "FecRegis," +
                                    "CodiModi," +
                                    "FecModi," +
                                    "DirServicio," +
                                    "CampVarios," +
                                    "UniMedi," +
                                    "NomForFar," +
                                    "ConceMedica," +
                                    "CuenConta," +
                                    "MaxPorAten," +
                                    "CodConPyP" +
                                    ")" +
                                    "VALUES" +
                                    "(" +
                                    "'" + TabServi["CodInterno"].ToString() + "'," +
                                    "'" + TabServi["NomServicio"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabServi["CodiMedMin"].ToString() + "'," +
                                    "'" + TabServi["GrupoServi"].ToString() + "'," +
                                    "'" + TabServi["TipoServi"].ToString() + "'," +
                                    "'" + TabServi["Finalidad"].ToString() + "'," +
                                    "'" + TabServi["EsCirugia"].ToString() + "'," +
                                    "'" + TabServi["ClasiSer"].ToString() + "'," +
                                    "'" + TabServi["CenCosto"].ToString() + "'," +
                                    "'" + TabServi["CodiSOAT"].ToString() + "'," +
                                    "'" + TabServi["CodiISS"].ToString() + "'," +
                                    "'" + TabServi["CodiCUPS"].ToString() + "'," +
                                    "'" + TabServi["ValorCUPS"].ToString() + "'," +
                                    "'" + TabServi["ValorParti"].ToString() + "'," +
                                    "'" + TabServi["ValorSoat"].ToString() + "'," +
                                    "'" + TabServi["GrupoSoat"].ToString() + "'," +
                                    "'" + TabServi["ValorIss"].ToString() + "'," +
                                    "'" + TabServi["UVRIss"].ToString() + "'," +
                                    "'" + TabServi["ValorEspecial01"].ToString() + "'," +
                                    "'" + TabServi["ValorEspecial02"].ToString() + "'," +
                                    "'" + TabServi["ValorEspecial03"].ToString() + "'," +
                                    "'" + TabServi["ValorEspecial04"].ToString() + "'," +
                                    "'" + TabServi["ValorEspecial05"].ToString() + "'," +
                                    "'" + TabServi["ValorEspecial06"].ToString() + "'," +
                                    "'" + TabServi["ValorEspecial07"].ToString() + "'," +
                                    "'" + TabServi["ValorEspecial08"].ToString() + "'," +
                                    "'" + TabServi["ValorEspecial09"].ToString() + "'," +
                                    "'" + TabServi["ValorEspecial10"].ToString() + "'," +
                                    "'" + TabServi["ValorEspecial11"].ToString() + "'," +
                                    "'" + TabServi["ValorEspecial12"].ToString() + "'," +
                                    "'" + TabServi["HabilPro"].ToString() + "'," +
                                    "'" + TabServi["Medicamento"].ToString() + "'," +
                                    "'" + TabServi["PosMedi"].ToString() + "'," +
                                    "'" + TabServi["SexAplica"].ToString() + "'," +
                                    "'" + TabServi["Requisito"].ToString() + "'," +
                                    "'" + TabServi["NivelActi"].ToString() + "'," +
                                    "'" + TabServi["ActiSiste"].ToString() + "'," +
                                    "'" + TabServi["PrograPyP"].ToString() + "'," +
                                    "'" + TabServi["ActuaValUni"].ToString() + "'," +
                                    "'" + TabServi["GrupoLabo"].ToString() + "'," +
                                    "'" + TabServi["VezReal"].ToString() + "'," +
                                    "'" + TabServi["CodiRegis"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabServi["FecRegis"].ToString())}" +
                                    "'" + TabServi["CodiModi"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabServi["FecModi"].ToString())}" +
                                    "'" + TabServi["DirServicio"].ToString() + "'," +
                                    "'" + TabServi["CampVarios"].ToString() + "'," +
                                    "'" + TabServi["UniMedi"].ToString() + "'," +
                                    "'" + TabServi["NomForFar"].ToString() + "'," +
                                    "'" + TabServi["ConceMedica"].ToString() + "'," +
                                    "'" + TabServi["CuenConta"].ToString() + "'," +
                                    // '**************** Los siguientes campos se crearon el 06 de octubre por HERNANDO ****************************
                                    "'" + TabServi["MaxPorAten"].ToString() + "'," +
                                    "'" + TabServi["CodConPyP"].ToString() + "' " +
                                    ")";


                                    Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos);

                                    if (Regis)
                                    {
                                        globalCanServiForm += 1;

                                    }

                                    ContiPro = 1;


                                }
                                else
                                {
                                    Utils.SqlDatos = $"UPDATE [ACDATOXPSQL].[dbo].[Datos catalogo de servicios] SET " +
                                    "NomServicio ='" + TabServi["NomServicio"].ToString().Replace("'", "''") + "', " +
                                    "CodiMedMin ='" + TabServi["CodiMedMin"].ToString() + "', " +
                                    "GrupoServi ='" + TabServi["GrupoServi"].ToString() + "', " +
                                    "TipoServi ='" + TabServi["TipoServi"].ToString() + "', " +
                                    "Finalidad ='" + TabServi["Finalidad"].ToString() + "', " +
                                    "EsCirugia ='" + TabServi["EsCirugia"].ToString() + "', " +
                                    "ClasiSer ='" + TabServi["ClasiSer"].ToString() + "', " +
                                    "CenCosto ='" + TabServi["CenCosto"].ToString() + "', " +
                                    "CodiSOAT ='" + TabServi["CodiSOAT"].ToString() + "', " +
                                    "CodiISS ='" + TabServi["CodiISS"].ToString() + "', " +
                                    "CodiCUPS ='" + TabServi["CodiCUPS"].ToString() + "', " +
                                    "ValorCUPS ='" + TabServi["ValorCUPS"].ToString() + "', " +
                                    "ValorParti ='" + TabServi["ValorParti"].ToString() + "', " +
                                    "ValorSoat ='" + TabServi["ValorSoat"].ToString() + "', " +
                                    "GrupoSoat ='" + TabServi["GrupoSoat"].ToString() + "', " +
                                    "ValorIss ='" + TabServi["ValorIss"].ToString() + "', " +
                                    "UVRIss ='" + TabServi["UVRIss"].ToString() + "', " +
                                    "ValorEspecial01 ='" + TabServi["ValorEspecial01"].ToString() + "', " +
                                    "ValorEspecial02 ='" + TabServi["ValorEspecial02"].ToString() + "', " +
                                    "ValorEspecial03 ='" + TabServi["ValorEspecial03"].ToString() + "', " +
                                    "ValorEspecial04 ='" + TabServi["ValorEspecial04"].ToString() + "', " +
                                    "ValorEspecial05 ='" + TabServi["ValorEspecial05"].ToString() + "', " +
                                    "ValorEspecial06 ='" + TabServi["ValorEspecial06"].ToString() + "', " +
                                    "ValorEspecial07 ='" + TabServi["ValorEspecial07"].ToString() + "', " +
                                    "ValorEspecial08 ='" + TabServi["ValorEspecial08"].ToString() + "', " +
                                    "ValorEspecial09 ='" + TabServi["ValorEspecial09"].ToString() + "', " +
                                    "ValorEspecial10 ='" + TabServi["ValorEspecial10"].ToString() + "', " +
                                    "ValorEspecial11 ='" + TabServi["ValorEspecial11"].ToString() + "', " +
                                    "ValorEspecial12 ='" + TabServi["ValorEspecial12"].ToString() + "', " +
                                    "HabilPro ='" + TabServi["HabilPro"].ToString() + "', " +
                                    "Medicamento ='" + TabServi["Medicamento"].ToString() + "', " +
                                    "PosMedi ='" + TabServi["PosMedi"].ToString() + "', " +
                                    "SexAplica ='" + TabServi["SexAplica"].ToString() + "', " +
                                    "Requisito ='" + TabServi["Requisito"].ToString() + "', " +
                                    "NivelActi ='" + TabServi["NivelActi"].ToString() + "', " +
                                    "ActiSiste ='" + TabServi["ActiSiste"].ToString() + "', " +
                                    "PrograPyP ='" + TabServi["PrograPyP"].ToString() + "', " +
                                    "ActuaValUni ='" + TabServi["ActuaValUni"].ToString() + "', " +
                                    "GrupoLabo ='" + TabServi["GrupoLabo"].ToString() + "', " +
                                    "VezReal ='" + TabServi["VezReal"].ToString() + "', " +
                                    "CodiRegis ='" + TabServi["CodiRegis"].ToString() + "', " +
                                    $"FecRegis = {Conexion.ValidarFechaNula(TabServi["FecRegis"].ToString())} " +
                                    "CodiModi ='" + TabServi["CodiModi"].ToString() + "', " +
                                    $"FecModi = {Conexion.ValidarFechaNula(TabServi["FecModi"].ToString())} " +
                                    "DirServicio ='" + TabServi["DirServicio"].ToString() + "', " +
                                    "CampVarios ='" + TabServi["CampVarios"].ToString() + "', " +
                                    "UniMedi ='" + TabServi["UniMedi"].ToString() + "', " +
                                    "NomForFar ='" + TabServi["NomForFar"].ToString() + "', " +
                                    "ConceMedica ='" + TabServi["ConceMedica"].ToString() + "', " +
                                    "CuenConta ='" + TabServi["CuenConta"].ToString() + "', " +
                                    "MaxPorAten ='" + TabServi["MaxPorAten"].ToString() + "', " +
                                    "CodConPyP = '" + TabServi["CodConPyP"].ToString() + "' " +
                                    "WHERE (CodInterno = '" + CodServi + "') ";

                                    Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos);

                                    if (ActControl)
                                    {
                                        globalCanServiExis += 1;

                                    }

                                    ContiPro = 1;


                                }

                            }

                            TabServiCentra.Close();

                            IntegrarServicios.ReportProgress(contador);

                        }//While

                    }
                }

                TabServi.Close();


                ConectarCentral();

                SqlProFarCen = "SELECT [Datos productos farmaceuticos].* ";
                SqlProFarCen = SqlProFarCen + "FROM [BDFARMA].[dbo].[Datos productos farmaceuticos] ";

                SqlDataReader TabProFarCen, TabProFarPor;

                int TolProFarCenCount = 0;

                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command2 = new SqlCommand(SqlProFarCen, connection2);
                    command2.Connection.Open();
                    TabProFarCen = command2.ExecuteReader();

                    if (TabProFarCen.HasRows == false)
                    {
                        //Farmacia no tiena nada
                        ContiPro = 1;
                    }
                    else
                    {
                        ConectarPortatil();
                        while (TabProFarCen.Read())
                        {

                            if (IntegrarServicios.CancellationPending == true)
                            {
                                e.Cancel = true;
                                Utils.Titulo01 = "Control de ejecución";
                                Utils.Informa = "Se cancelo la operacion " + "\r";
                                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            TolProFarCenCount += 1;

                            contador += 1;

                            //Revisamos si el código interno de la entidad existe

                            CodServi = TabProFarCen["CodigoPro"].ToString();

                            SqlProFarPor = "SELECT [Datos productos farmaceuticos].* ";
                            SqlProFarPor += "FROM [BDFARMA].[dbo].[Datos productos farmaceuticos] ";
                            SqlProFarPor += "WHERE (CodigoPro = '" + CodServi + "')";

                            using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command = new SqlCommand(SqlProFarPor, connection);
                                command.Connection.Open();
                                TabProFarPor = command.ExecuteReader();

                                if (TabProFarPor.HasRows == false)
                                {
                                    //Lo agrega normalmente

                                    Utils.SqlDatos = "INSERT INTO [BDFARMA].[dbo].[Datos productos farmaceuticos]" +
                                    "(" +
                                    "CodigoPro," +
                                    "CodAlter," +
                                    "CodiMinSa," +
                                    "CodiCUN," +
                                    "Habilitado," +
                                    "NombreGenerico," +
                                    "NomAlterno," +
                                    "PrinActivo," +
                                    "CodCaProdu," +
                                    "CentCosto," +
                                    "GrupoPertenece," +
                                    "GrupoRips," +
                                    "TipoServi," +
                                    "Decontrol," +
                                    "Medida," +
                                    "Formafarma," +
                                    "Concentra," +
                                    "SiPos," +
                                    "Material," +
                                    "NivelMin," +
                                    "NivelMax," +
                                    "CostoUnita," +
                                    "Cantactual," +
                                    "ValorInvActual," +
                                    "FecUltimEntra," +
                                    "CantUltimEntra," +
                                    "ValUltimEntra," +
                                    "PrePLM," +
                                    "PorcenIva," +
                                    "DefiClase," +
                                    "ObliATC," +
                                    "ObliCUM," +
                                    "ObliLote," +
                                    "ObliVece," +
                                    "PrecioVenta," +
                                    "ValorParti," +
                                    "ValorCUPS," +
                                    "ValorSoat," +
                                    "ValorIss," +
                                    "PreVen01," +
                                    "PreVen02," +
                                    "PreVen03," +
                                    "PreVen04," +
                                    "PreVen05," +
                                    "PreVen06," +
                                    "PreVen07," +
                                    "PreVen08," +
                                    "PreVen09," +
                                    "PreVen10," +
                                    "PreVen11," +
                                    "PreVen12," +
                                    "PreVen13," +
                                    "PreVen14," +
                                    "PreVen15," +
                                    "PreVen16," +
                                    "PreVen17," +
                                    "PreVen18," +
                                    "PreVen19," +
                                    "PreVen20," +
                                    "PreVen21," +
                                    "PreVen22," +
                                    "PreVen23," +
                                    "PreVen24," +
                                    "PreVen25," +
                                    "PreVen26," +
                                    "PreVen27," +
                                    "PreVen28," +
                                    "PreVen29," +
                                    "PreVen30," +
                                    "Reginvima," +
                                    "Integrado," +
                                    "DiferIntegra," +
                                    "ProducEspe," +
                                    "CodIngresa," +
                                    "FechIngresa," +
                                    "CodModifica," +
                                    "FechaModi," +
                                    "GrupoTerapeutico," +
                                    "MaxDispe," +
                                    "TarImpor," +
                                    "CanTraDia" +
                                    ")" +
                                    "VALUES" +
                                    "(" +
                                    "'" + TabProFarCen["CodigoPro"].ToString() + "'," +
                                    "'" + TabProFarCen["CodAlter"].ToString() + "'," +
                                    "'" + TabProFarCen["CodiMinSa"].ToString() + "'," +
                                    "'" + TabProFarCen["CodiCUN"].ToString() + "'," +
                                    "'" + TabProFarCen["Habilitado"].ToString() + "'," +
                                    "'" + TabProFarCen["NombreGenerico"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabProFarCen["NomAlterno"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabProFarCen["PrinActivo"].ToString() + "'," +
                                    "'" + TabProFarCen["CodCaProdu"].ToString() + "'," +
                                    "'" + TabProFarCen["CentCosto"].ToString() + "'," +
                                    "'" + TabProFarCen["GrupoPertenece"].ToString() + "'," +
                                    "'" + TabProFarCen["GrupoRips"].ToString() + "'," +
                                    "'" + TabProFarCen["TipoServi"].ToString() + "'," +
                                    "'" + TabProFarCen["Decontrol"].ToString() + "'," +
                                    "'" + TabProFarCen["Medida"].ToString() + "'," +
                                    "'" + TabProFarCen["Formafarma"].ToString() + "'," +
                                    "'" + TabProFarCen["Concentra"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabProFarCen["SiPos"].ToString() + "'," +
                                    "'" + TabProFarCen["Material"].ToString() + "'," +
                                    "'" + TabProFarCen["NivelMin"].ToString() + "'," +
                                    "'" + TabProFarCen["NivelMax"].ToString() + "'," +
                                    "'" + TabProFarCen["CostoUnita"].ToString() + "'," +
                                    "'" + TabProFarCen["Cantactual"].ToString() + "'," +
                                    "'" + TabProFarCen["ValorInvActual"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabProFarCen["FecUltimEntra"].ToString())}" +
                                    "'" + TabProFarCen["CantUltimEntra"].ToString() + "'," +
                                    "'" + TabProFarCen["ValUltimEntra"].ToString() + "'," +
                                    "'" + TabProFarCen["PrePLM"].ToString() + "'," +
                                    "'" + TabProFarCen["PorcenIva"].ToString() + "'," +
                                    "'" + TabProFarCen["DefiClase"].ToString() + "'," +
                                    "'" + TabProFarCen["ObliATC"].ToString() + "'," +
                                    "'" + TabProFarCen["ObliCUM"].ToString() + "'," +
                                    "'" + TabProFarCen["ObliLote"].ToString() + "'," +
                                    "'" + TabProFarCen["ObliVece"].ToString() + "'," +
                                    "'" + TabProFarCen["PrecioVenta"].ToString() + "'," +
                                    "'" + TabProFarCen["ValorParti"].ToString() + "'," +
                                    "'" + TabProFarCen["ValorCUPS"].ToString() + "'," +
                                    "'" + TabProFarCen["ValorSoat"].ToString() + "'," +
                                    "'" + TabProFarCen["ValorIss"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen01"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen02"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen03"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen04"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen05"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen06"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen07"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen08"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen09"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen10"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen11"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen12"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen13"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen14"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen15"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen16"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen17"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen18"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen19"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen20"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen21"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen22"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen23"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen24"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen25"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen26"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen27"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen28"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen29"].ToString() + "'," +
                                    "'" + TabProFarCen["PreVen30"].ToString() + "'," +
                                    "'" + TabProFarCen["Reginvima"].ToString() + "'," +
                                    "'" + TabProFarCen["Integrado"].ToString() + "'," +
                                    "'" + TabProFarCen["DiferIntegra"].ToString() + "'," +
                                    "'" + TabProFarCen["ProducEspe"].ToString() + "'," +
                                    "'" + TabProFarCen["CodIngresa"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabProFarCen["FechIngresa"].ToString())}" +
                                    "'" + TabProFarCen["CodModifica"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabProFarCen["FechaModi"].ToString())}" +
                                    "'" + TabProFarCen["GrupoTerapeutico"].ToString() + "'," +
                                    "'" + TabProFarCen["MaxDispe"].ToString() + "'," +
                                    "'" + TabProFarCen["TarImpor"].ToString() + "'," +
                                    "'" + TabProFarCen["CanTraDia"].ToString() + "' " +
                                    ")";

                                    Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos);

                                    if (Regis)
                                    {
                                        globalProFarAgrega += 1;
                                    }


                                }
                                else
                                {
                                    //Lo modifica
                                    Utils.SqlDatos = $"UPDATE [BDFARMA].[dbo].[Datos productos farmaceuticos] SET " +
                                    "CodAlter ='" + TabProFarCen["CodAlter"].ToString() + "', " +
                                    "CodiMinSa ='" + TabProFarCen["CodiMinSa"].ToString() + "', " +
                                    "CodiCUN ='" + TabProFarCen["CodiCUN"].ToString() + "', " +
                                    "Habilitado ='" + TabProFarCen["Habilitado"].ToString() + "', " +
                                    "NombreGenerico ='" + TabProFarCen["NombreGenerico"].ToString().Replace("'", "''") + "', " +
                                    "NomAlterno ='" + TabProFarCen["NomAlterno"].ToString().Replace("'", "''") + "', " +
                                    "PrinActivo ='" + TabProFarCen["PrinActivo"].ToString() + "', " +
                                    "CodCaProdu ='" + TabProFarCen["CodCaProdu"].ToString() + "', " +
                                    "CentCosto ='" + TabProFarCen["CentCosto"].ToString() + "', " +
                                    "GrupoPertenece ='" + TabProFarCen["GrupoPertenece"].ToString() + "', " +
                                    "GrupoRips ='" + TabProFarCen["GrupoRips"].ToString() + "', " +
                                    "TipoServi ='" + TabProFarCen["TipoServi"].ToString() + "', " +
                                    "Decontrol ='" + TabProFarCen["Decontrol"].ToString() + "', " +
                                    "Medida ='" + TabProFarCen["Medida"].ToString() + "', " +
                                    "Formafarma ='" + TabProFarCen["Formafarma"].ToString() + "', " +
                                    "Concentra ='" + TabProFarCen["Concentra"].ToString().Replace("'", "''") + "', " +
                                    "SiPos ='" + TabProFarCen["SiPos"].ToString() + "', " +
                                    "Material ='" + TabProFarCen["Material"].ToString() + "', " +
                                    "NivelMin ='" + TabProFarCen["NivelMin"].ToString() + "', " +
                                    "NivelMax ='" + TabProFarCen["NivelMax"].ToString() + "', " +
                                    "CostoUnita ='" + TabProFarCen["CostoUnita"].ToString() + "', " +
                                    "Cantactual ='" + TabProFarCen["Cantactual"].ToString() + "', " +
                                    "ValorInvActual ='" + TabProFarCen["ValorInvActual"].ToString() + "', " +
                                    $"FecUltimEntra = {Conexion.ValidarFechaNula(TabProFarCen["FecUltimEntra"].ToString())} " +
                                    "CantUltimEntra ='" + TabProFarCen["CantUltimEntra"].ToString() + "', " +
                                    "ValUltimEntra ='" + TabProFarCen["ValUltimEntra"].ToString() + "', " +
                                    "PrePLM ='" + TabProFarCen["PrePLM"].ToString() + "', " +
                                    "PorcenIva ='" + TabProFarCen["PorcenIva"].ToString() + "', " +
                                    "DefiClase ='" + TabProFarCen["DefiClase"].ToString() + "', " +
                                    "ObliATC ='" + TabProFarCen["ObliATC"].ToString() + "', " +
                                    "ObliCUM ='" + TabProFarCen["ObliCUM"].ToString() + "', " +
                                    "ObliLote ='" + TabProFarCen["ObliLote"].ToString() + "', " +
                                    "ObliVece ='" + TabProFarCen["ObliVece"].ToString() + "', " +
                                    "PrecioVenta ='" + TabProFarCen["PrecioVenta"].ToString() + "', " +
                                    "ValorParti ='" + TabProFarCen["ValorParti"].ToString() + "', " +
                                    "ValorCUPS ='" + TabProFarCen["ValorCUPS"].ToString() + "', " +
                                    "ValorSoat ='" + TabProFarCen["ValorSoat"].ToString() + "', " +
                                    "ValorIss ='" + TabProFarCen["ValorIss"].ToString() + "', " +
                                    "PreVen01 ='" + TabProFarCen["PreVen01"].ToString() + "', " +
                                    "PreVen02 ='" + TabProFarCen["PreVen02"].ToString() + "', " +
                                    "PreVen03 ='" + TabProFarCen["PreVen03"].ToString() + "', " +
                                    "PreVen04 ='" + TabProFarCen["PreVen04"].ToString() + "', " +
                                    "PreVen05 ='" + TabProFarCen["PreVen05"].ToString() + "', " +
                                    "PreVen06 ='" + TabProFarCen["PreVen06"].ToString() + "', " +
                                    "PreVen07 ='" + TabProFarCen["PreVen07"].ToString() + "', " +
                                    "PreVen08 ='" + TabProFarCen["PreVen08"].ToString() + "', " +
                                    "PreVen09 ='" + TabProFarCen["PreVen09"].ToString() + "', " +
                                    "PreVen10 ='" + TabProFarCen["PreVen10"].ToString() + "', " +
                                    "PreVen11 ='" + TabProFarCen["PreVen11"].ToString() + "', " +
                                    "PreVen12 ='" + TabProFarCen["PreVen12"].ToString() + "', " +
                                    "PreVen13 ='" + TabProFarCen["PreVen13"].ToString() + "', " +
                                    "PreVen14 ='" + TabProFarCen["PreVen14"].ToString() + "', " +
                                    "PreVen15 ='" + TabProFarCen["PreVen15"].ToString() + "', " +
                                    "PreVen16 ='" + TabProFarCen["PreVen16"].ToString() + "', " +
                                    "PreVen17 ='" + TabProFarCen["PreVen17"].ToString() + "', " +
                                    "PreVen18 ='" + TabProFarCen["PreVen18"].ToString() + "', " +
                                    "PreVen19 ='" + TabProFarCen["PreVen19"].ToString() + "', " +
                                    "PreVen20 ='" + TabProFarCen["PreVen20"].ToString() + "', " +
                                    "PreVen21 ='" + TabProFarCen["PreVen21"].ToString() + "', " +
                                    "PreVen22 ='" + TabProFarCen["PreVen22"].ToString() + "', " +
                                    "PreVen23 ='" + TabProFarCen["PreVen23"].ToString() + "', " +
                                    "PreVen24 ='" + TabProFarCen["PreVen24"].ToString() + "', " +
                                    "PreVen25 ='" + TabProFarCen["PreVen25"].ToString() + "', " +
                                    "PreVen26 ='" + TabProFarCen["PreVen26"].ToString() + "', " +
                                    "PreVen27 ='" + TabProFarCen["PreVen27"].ToString() + "', " +
                                    "PreVen28 ='" + TabProFarCen["PreVen28"].ToString() + "', " +
                                    "PreVen29 ='" + TabProFarCen["PreVen29"].ToString() + "', " +
                                    "PreVen30 ='" + TabProFarCen["PreVen30"].ToString() + "', " +
                                    "Reginvima ='" + TabProFarCen["Reginvima"].ToString() + "', " +
                                    "Integrado ='" + TabProFarCen["Integrado"].ToString() + "', " +
                                    "DiferIntegra ='" + TabProFarCen["DiferIntegra"].ToString() + "', " +
                                    "ProducEspe ='" + TabProFarCen["ProducEspe"].ToString() + "', " +
                                    "CodIngresa ='" + TabProFarCen["CodIngresa"].ToString() + "', " +
                                    $"FechIngresa = {Conexion.ValidarFechaNula(TabProFarCen["FechIngresa"].ToString())} " +
                                    "CodModifica ='" + TabProFarCen["CodModifica"].ToString() + "', " +
                                    $"FechaModi = {Conexion.ValidarFechaNula(TabProFarCen["FechaModi"].ToString())} " +
                                    "GrupoTerapeutico ='" + TabProFarCen["GrupoTerapeutico"].ToString() + "', " +
                                    "MaxDispe ='" + TabProFarCen["MaxDispe"].ToString() + "', " +
                                    "TarImpor ='" + TabProFarCen["TarImpor"].ToString() + "', " +
                                    "CanTraDia ='" + TabProFarCen["CanTraDia"].ToString() + "' " +
                                    "WHERE (CodigoPro = '" + CodServi + "')";

                                    Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos);

                                    if (ActControl)
                                    {
                                        globalCanProforVal += 1;
                                    }

                                }

                            }

                            TabProFarPor.Close();

                            IntegrarServicios.ReportProgress(contador);


                        }//While

                        ContiPro = 2;

                        globalCantiProFar = TolProFarCenCount;

                    }
                }

                TabProFarCen.Close();

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "después de hacer click sobre el botón integrar" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                IntegrarServicios.CancelAsync();
            }
        }

        private void IntegrarServicios_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                if (IntegrarServicios.CancellationPending == false)
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

        private void IntegrarServicios_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Utils.Titulo01 = "Control de Integrar servicios";
                Utils.Informa = "El proceso de agregar o modificar productos";
                Utils.Informa += "farmaceúticos, procedimientos y servicios,";
                Utils.Informa += "ha concluido satisfactoriamente";
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);

                ProgressBar.Minimum = 0;
                ProgressBar.Maximum = 1;
                ProgressBar.Value = 0;


                LblCantidad.Text = "0";
                LblTotal.Text = "0";

                TxtCanServiFor.Text = globalCanServiFor.ToString();
                TxtCanServiForm.Text = globalCanServiForm.ToString();
                TxtCanServiExis.Text = globalCanServiExis.ToString();
                TxtCantiProFar.Text = globalCantiProFar.ToString();
                TxtProFarAgrega.Text = globalProFarAgrega.ToString();
                TxtCanProforVal.Text = globalCanProforVal.ToString();


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
                    IntegrarServicios.WorkerSupportsCancellation = true;
                    IntegrarServicios.CancelAsync();

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
