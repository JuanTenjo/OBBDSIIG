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


                    string Sqlservi, SqlServiCentra, SqlProFarPor = "", SqlProFarCen = "", CodServi = "";
                    int ContiPro;
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

                                    if(TabServiCentra.HasRows == false)
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
                                        "'" + TabServi["NomServicio"].ToString().Replace("'", "") + "'," +
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
                                        $"{ValidarFechaNula(TabServi["FecRegis"].ToString())}" +
                                        "'" + TabServi["CodiModi"].ToString() + "'," +
                                        $"{ValidarFechaNula(TabServi["FecModi"].ToString())}" +
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
                                            int con = Convert.ToInt32(TxtCanServiForm.Text) + 1;
                                            TxtCanServiForm.Text = con.ToString();

                                        }

                                        ContiPro = 1;


                                    }
                                    else
                                    {
                                        Utils.SqlDatos = $"UPDATE [ACDATOXPSQL].[dbo].[Datos catalogo de servicios] SET " +
                                        "NomServicio ='" + TabServi["NomServicio"].ToString().Replace("'","") + "', " +
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
                                        $"FecRegis = {ValidarFechaNula(TabServi["FecRegis"].ToString())} " +
                                        "CodiModi ='" + TabServi["CodiModi"].ToString() + "', " +
                                        $"FecModi = {ValidarFechaNula(TabServi["FecModi"].ToString())} " +
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
                                            int con = Convert.ToInt32(TxtCanServiExis.Text) + 1;
                                            TxtCanServiExis.Text = con.ToString();

                                        }

                                        ContiPro = 1;


                                    }

                                }

                                TabServiCentra.Close();

                            }

                        }
                    }
                    TabServi.Close();

                    // Vamos a actualizar los productos de farmacia.  ******************* lo implementa HERNANDO EL 06 DE MAYO DE 2020 ****
                    //'Se require porque la formulacion medica se basa en esa tabla

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

                        if(TabProFarCen.HasRows == false)
                        {
                            //Farmacia no tiena nada
                            ContiPro = 1;
                        }
                        else
                        {
                            ConectarPortatil();
                            while (TabProFarCen.Read())
                            {
                                TolProFarCenCount += 1;

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

                                    if(TabProFarPor.HasRows == false)
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
                                        "'" + TabProFarCen["NombreGenerico"].ToString() + "'," +
                                        "'" + TabProFarCen["NomAlterno"].ToString() + "'," +
                                        "'" + TabProFarCen["PrinActivo"].ToString() + "'," +
                                        "'" + TabProFarCen["CodCaProdu"].ToString() + "'," +
                                        "'" + TabProFarCen["CentCosto"].ToString() + "'," +
                                        "'" + TabProFarCen["GrupoPertenece"].ToString() + "'," +
                                        "'" + TabProFarCen["GrupoRips"].ToString() + "'," +
                                        "'" + TabProFarCen["TipoServi"].ToString() + "'," +
                                        "'" + TabProFarCen["Decontrol"].ToString() + "'," +
                                        "'" + TabProFarCen["Medida"].ToString() + "'," +
                                        "'" + TabProFarCen["Formafarma"].ToString() + "'," +
                                        "'" + TabProFarCen["Concentra"].ToString() + "'," +
                                        "'" + TabProFarCen["SiPos"].ToString() + "'," +
                                        "'" + TabProFarCen["Material"].ToString() + "'," +
                                        "'" + TabProFarCen["NivelMin"].ToString() + "'," +
                                        "'" + TabProFarCen["NivelMax"].ToString() + "'," +
                                        "'" + TabProFarCen["CostoUnita"].ToString() + "'," +
                                        "'" + TabProFarCen["Cantactual"].ToString() + "'," +
                                        "'" + TabProFarCen["ValorInvActual"].ToString() + "'," +
                                        $"{ValidarFechaNula(TabProFarCen["FecUltimEntra"].ToString())}" +
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
                                        $"{ValidarFechaNula(TabProFarCen["FechIngresa"].ToString())}" +
                                        "'" + TabProFarCen["CodModifica"].ToString() + "'," +
                                        $"{ValidarFechaNula(TabProFarCen["FechaModi"].ToString())}" +
                                        "'" + TabProFarCen["GrupoTerapeutico"].ToString() + "'," +
                                        "'" + TabProFarCen["MaxDispe"].ToString() + "'," +
                                        "'" + TabProFarCen["TarImpor"].ToString() + "'," +
                                        "'" + TabProFarCen["CanTraDia"].ToString() + "' " +
                                        ")";

                                        Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos);

                                        if (Regis)
                                        {
                                            int con = Convert.ToInt32(TxtProFarAgrega.Text) + 1;
                                            TxtProFarAgrega.Text = con.ToString();
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
                                        "NombreGenerico ='" + TabProFarCen["NombreGenerico"].ToString() + "', " +
                                        "NomAlterno ='" + TabProFarCen["NomAlterno"].ToString() + "', " +
                                        "PrinActivo ='" + TabProFarCen["PrinActivo"].ToString() + "', " +
                                        "CodCaProdu ='" + TabProFarCen["CodCaProdu"].ToString() + "', " +
                                        "CentCosto ='" + TabProFarCen["CentCosto"].ToString() + "', " +
                                        "GrupoPertenece ='" + TabProFarCen["GrupoPertenece"].ToString() + "', " +
                                        "GrupoRips ='" + TabProFarCen["GrupoRips"].ToString() + "', " +
                                        "TipoServi ='" + TabProFarCen["TipoServi"].ToString() + "', " +
                                        "Decontrol ='" + TabProFarCen["Decontrol"].ToString() + "', " +
                                        "Medida ='" + TabProFarCen["Medida"].ToString() + "', " +
                                        "Formafarma ='" + TabProFarCen["Formafarma"].ToString() + "', " +
                                        "Concentra ='" + TabProFarCen["Concentra"].ToString() + "', " +
                                        "SiPos ='" + TabProFarCen["SiPos"].ToString() + "', " +
                                        "Material ='" + TabProFarCen["Material"].ToString() + "', " +
                                        "NivelMin ='" + TabProFarCen["NivelMin"].ToString() + "', " +
                                        "NivelMax ='" + TabProFarCen["NivelMax"].ToString() + "', " +
                                        "CostoUnita ='" + TabProFarCen["CostoUnita"].ToString() + "', " +
                                        "Cantactual ='" + TabProFarCen["Cantactual"].ToString() + "', " +
                                        "ValorInvActual ='" + TabProFarCen["ValorInvActual"].ToString() + "', " +
                                        $"FecUltimEntra = {ValidarFechaNula(TabProFarCen["FecUltimEntra"].ToString())} " +
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
                                        $"FechIngresa = {ValidarFechaNula(TabProFarCen["FechIngresa"].ToString())} " +
                                        "CodModifica ='" + TabProFarCen["CodModifica"].ToString() + "', " +
                                        $"FechaModi = {ValidarFechaNula(TabProFarCen["FechaModi"].ToString())} " +
                                        "GrupoTerapeutico ='" + TabProFarCen["GrupoTerapeutico"].ToString() + "', " +
                                        "MaxDispe ='" + TabProFarCen["MaxDispe"].ToString() + "', " +
                                        "TarImpor ='" + TabProFarCen["TarImpor"].ToString() + "', " +
                                        "CanTraDia ='" + TabProFarCen["CanTraDia"].ToString() + "' " +
                                        "WHERE (CodigoPro = '" + CodServi + "')";

                                        Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos);

                                        if (ActControl)
                                        {
                                            int con = Convert.ToInt32(TxtCanProforVal.Text) + 1;
                                            TxtCanProforVal.Text = con.ToString();
                                        }

                                    }

                                }

                                TabProFarPor.Close();

                            }

                            ContiPro = 2;
                            TxtCantiProFar.Text = TolProFarCenCount.ToString();


                        }
                    }

                    TabProFarCen.Close();

                    if(ContiPro == 1)
                    {
                        Utils.Informa = "El proceso de agregar o modificar procedimientos y";
                        Utils.Informa += "servicios, ha concluido satisfactoriamente";
                        MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (ContiPro == 2)
                        {
                            Utils.Informa = "El proceso de agregar o modificar productos";
                            Utils.Informa += "farmaceúticos, procedimientos y servicios,";
                            Utils.Informa += "ha concluido satisfactoriamente";
                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



    }
}
