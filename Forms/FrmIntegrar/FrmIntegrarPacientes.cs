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

namespace OBBDSIIG.Forms.FrmIntegrar
{
    public partial class FrmIntegrarPacientes : Form
    {
        public FrmIntegrarPacientes()
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

        private void FrmIntegrarPacientes_Load(object sender, EventArgs e)
        {
            try
            {
                CargarDatosUser();
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al cargar el formulario FrmIntegrarPacientes" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscarPacientes_Click(object sender, EventArgs e)
        {
            try
            {

                string SqlPacien, SqlEmpTerCentra, CodPacien, SqlPacienCentra, TDCen, NumDCen, CodPacienPort = "", SqlCueCons, SqlAtenCon, SqlAnoAneHis, SqlAntPac, SqlRemi, SqlTrat, SqlTRIAGE, SqlDetVacApl, SqlInfQui, SqlRegContPlac, SqlRegEco, SqlBasCito, SqlBasBioFir, SqlBasBioFirmod;
                string SqlBasBioFot, SqlBasBioFotmod, SqlBasBioHue, SqlBasBioHuemod, SqlSegCont, SqlPacienCentra1;
                int ContiPro = 0;

                Boolean ActDatos;

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


                Utils.Informa = "¿Usted desea iniciar el proceso de integración" + "\r";
                Utils.Informa += "de los pacientes en la instancia del" + "\r";
                Utils.Informa += "servidor central a la instancia del portatil.?" + "\r";
                var res = MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    SqlPacien = "SELECT * FROM [ACDATOXPSQL].[dbo].[Datos del Paciente] ";



                    ConectarCentral();

                    SqlDataReader TabPacien;

                    using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                    {
                        SqlCommand command = new SqlCommand(SqlPacien, connection);
                        command.Connection.Open();
                        TabPacien = command.ExecuteReader();

                        if (TabPacien.HasRows == false)
                        {
                            Utils.Informa = "Lo siento pero no hay datos " + "\r";
                            Utils.Informa += "para exportar o modificar en , " + "\r";
                            Utils.Informa += "Datos de los paciente." + "\r";
                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            while (TabPacien.Read())
                            {
                                //Tomamos datos del SERVIDOR central
                                CodPacien = TabPacien["HistorPaci"].ToString();
                                TDCen = TabPacien["TipoIden"].ToString();
                                NumDCen = TabPacien["NumIden"].ToString();

                                ContiPro = 0;

                                //Revisamos si el numero de historia del paciente existe en el equipo de BRIGADA
                                SqlPacienCentra = "SELECT [Datos del Paciente].* ";
                                SqlPacienCentra += "FROM [ACDATOXPSQL].[dbo].[Datos del Paciente] ";
                                SqlPacienCentra += "WHERE (HistorPaci = '" + CodPacien + "')";


                                ConectarPortatil();

                                SqlDataReader TabPacienCentra, TabPacienCentra1;

                                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                                {
                                    SqlCommand command2 = new SqlCommand(SqlPacienCentra, connection2);
                                    command2.Connection.Open();
                                    TabPacienCentra = command2.ExecuteReader();

                                    if (TabPacienCentra.HasRows == false)
                                    {

                                        //'Revisamos si el tipo documento y el numero de identificacion del paciente existe en el equipo de BRIGADA
                                        SqlPacienCentra1 = "SELECT [Datos del Paciente].* ";
                                        SqlPacienCentra1 += "FROM  [ACDATOXPSQL].[dbo].[Datos del Paciente] ";
                                        SqlPacienCentra1 += "WHERE (TipoIden = '" + TDCen + "') and (NumIden = '" + NumDCen + "')";


                                        using (SqlConnection connection3 = new SqlConnection(Conexion.conexionSQL))
                                        {
                                            SqlCommand command3 = new SqlCommand(SqlPacienCentra1, connection3);
                                            command3.Connection.Open();
                                            TabPacienCentra1 = command3.ExecuteReader();


                                            if(TabPacienCentra1.HasRows == false)
                                            {
                                                //Agregue los datos del paciente NUEVO en el equipo de BRIGADA
                                                Utils.SqlDatos = "INSERT INTO [ACDATOXPSQL].[dbo].[Datos del Paciente] " +
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
                                                "TelCelular," +
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
                                                "NomEmeal," +
                                                "NivEducUs," +
                                                "DebeDere," +
                                                "UltiPeso," +
                                                "FecPeso," +
                                                "CodRgPes," +
                                                "UltiTalla," +
                                                "FecTalla," +
                                                "CodRgTal," +
                                                "CodPrefi," +
                                                "NomDepenLabo," +
                                                "CodARLLabo," +
                                                "CodAFPLabo," +
                                                "CodiRegis," +
                                                "FecRegis," +
                                                "CodiModi," +
                                                "FecModi," +
                                                //'***** CAMPOS AGREGADOS CON BASE DATOS DE SAN AGUSTIN *****
                                                "Discapacidad," +
                                                "NomCargoLabo," +
                                                "GruSangui," +
                                                "RhPaciente," +
                                                "PaciSolUrgen" +
                                                ")" +
                                                "VALUES" +
                                                "(" +
                                                "'" + TabPacien["HistorPaci"].ToString() + "'," +
                                                "'" + TabPacien["TipoIden"].ToString() + "'," +
                                                "'" + TabPacien["NumIden"].ToString() + "'," +
                                                "'" + TabPacien["Nombre1"].ToString() + "'," +
                                                "'" + TabPacien["Nombre2"].ToString() + "'," +
                                                "'" + TabPacien["Apellido1"].ToString() + "'," +
                                                "'" + TabPacien["Apellido2"].ToString() + "'," +
                                                $"{ValidarFechaNula(TabPacien["FechaNaci"].ToString())}" +
                                                "CONVERT(DATETIME,'" + TabPacien["HoraNaci"].ToString() + "',8)," +
                                                "'" + TabPacien["LugarNace"].ToString() + "'," +
                                                "'" + TabPacien["SemGesta"].ToString() + "'," +
                                                "'" + TabPacien["CodPaisNace"].ToString() + "'," +
                                                "'" + TabPacien["CodDptoNace"].ToString() + "'," +
                                                "'" + TabPacien["CodCiuNace"].ToString() + "'," +
                                                "'" + TabPacien["EstadoCivil"].ToString() + "'," +
                                                "'" + TabPacien["CodDpto"].ToString() + "'," +
                                                "'" + TabPacien["CodMuni"].ToString() + "'," +
                                                "'" + TabPacien["BarrioVive"].ToString() + "'," +
                                                "'" + TabPacien["DirecResi"].ToString() + "'," +
                                                "'" + TabPacien["TelResi"].ToString() + "'," +
                                                "'" + TabPacien["TelCelular"].ToString() + "'," +
                                                "'" + TabPacien["ZonaResiden"].ToString() + "'," +
                                                    $"{ValidarFechaNula(TabPacien["FechaApertura"].ToString())}" +
                                                "'" + TabPacien["Observaciones"].ToString() + "'," +
                                                "'" + TabPacien["Sexo"].ToString() + "'," +
                                                "'" + TabPacien["TipoUsar"].ToString() + "'," +
                                                "'" + TabPacien["TipoAfiliado"].ToString() + "'," +
                                                "'" + TabPacien["NumAfilia"].ToString() + "'," +
                                                "'" + TabPacien["PolizaNum"].ToString() + "'," +
                                                "'" + TabPacien["EstraNum"].ToString() + "'," +
                                                "'" + TabPacien["GrupoEtni"].ToString() + "'," +
                                                "'" + TabPacien["NumContra"].ToString() + "'," +
                                                "'" + TabPacien["TipoCuenta"].ToString() + "'," +
                                                "'" + TabPacien["CausaRemite"].ToString() + "'," +
                                                "'" + TabPacien["NombresRespon"].ToString() + "'," +
                                                "'" + TabPacien["Apellido1Respon"].ToString() + "'," +
                                                "'" + TabPacien["Apellido2Respon"].ToString() + "'," +
                                                "'" + TabPacien["TipoDocuRespon"].ToString() + "'," +
                                                "'" + TabPacien["CedulaRespon"].ToString() + "'," +
                                                "'" + TabPacien["Parentesco"].ToString() + "'," +
                                                "'" + TabPacien["DireccPare"].ToString() + "'," +
                                                "'" + TabPacien["TelefonoPare"].ToString() + "'," +
                                                "'" + TabPacien["CiudadPare"].ToString() + "'," +
                                                "'" + TabPacien["DptoRespon"].ToString() + "'," +
                                                "'" + TabPacien["NombrePadre"].ToString() + "'," +
                                                "'" + TabPacien["Apellido1Padre"].ToString() + "'," +
                                                "'" + TabPacien["Apellido2Padre"].ToString() + "'," +
                                                "'" + TabPacien["VivePadre"].ToString() + "'," +
                                                "'" + TabPacien["TipoDocuPadre"].ToString() + "'," +
                                                "'" + TabPacien["CedulaPadre"].ToString() + "'," +
                                                "'" + TabPacien["NombreMadre"].ToString() + "'," +
                                                "'" + TabPacien["Apellido1Madre"].ToString() + "'," +
                                                "'" + TabPacien["Apellido2Madre"].ToString() + "'," +
                                                "'" + TabPacien["ViveMadre"].ToString() + "'," +
                                                "'" + TabPacien["TipoDocuMadre"].ToString() + "'," +
                                                "'" + TabPacien["CedulaMadre"].ToString() + "'," +
                                                "'" + TabPacien["NombreEmpresa"].ToString() + "'," +
                                                "'" + TabPacien["Ocupacion"].ToString() + "'," +
                                                "'" + TabPacien["CiudadEmpresa"].ToString() + "'," +
                                                "'" + TabPacien["DireccTrabaja"].ToString() + "'," +
                                                "'" + TabPacien["TeleEmpresa"].ToString() + "'," +
                                                "'" + TabPacien["CodSeSocial"].ToString() + "'," +
                                                "'" + TabPacien["CodiAdmin"].ToString() + "'," +
                                                "'" + TabPacien["DptoRemite"].ToString() + "'," +
                                                "'" + TabPacien["MunicipioRemite"].ToString() + "'," +
                                                "'" + TabPacien["IPSRemite"].ToString() + "'," +
                                                "'" + TabPacien["RemiNumero"].ToString() + "'," +
                                                    $"{ValidarFechaNula(TabPacien["FechaRemision"].ToString())}" +
                                                    $"{ValidarFechaNula(TabPacien["FechaVence"].ToString())}" +
                                                "'" + TabPacien["CubreRemision"].ToString() + "'," +
                                                "'" + TabPacien["EspecialRemite"].ToString() + "'," +
                                                "'" + TabPacien["DxRemite"].ToString() + "'," +
                                                "'" + TabPacien["CoMediAten"].ToString() + "'," +
                                                "'" + TabPacien["MotivoConsul"].ToString() + "'," +
                                                    $"{ValidarFechaNula(TabPacien["FechaEntrada"].ToString())}" +
                                                "CONVERT(DATETIME,'" + TabPacien["HoraEntrada"].ToString() + "',8)," +
                                                $"{ValidarFechaNula(TabPacien["FecUltima"].ToString())}" +
                                                "'" + TabPacien["ArchivoViene"].ToString() + "'," +
                                                "'" + TabPacien["Muerto"].ToString() + "'," +
                                                "'" + TabPacien["PreInscripcion"].ToString() + "'," +
                                                "'" + TabPacien["CoberSalud"].ToString() + "'," +
                                                "'" + TabPacien["Huella1"].ToString() + "'," +
                                                "'" + TabPacien["Huella2"].ToString() + "'," +
                                                "'" + TabPacien["Huella"].ToString() + "'," +
                                                "'" + TabPacien["NomEmeal"].ToString() + "'," +
                                                "'" + TabPacien["NivEducUs"].ToString() + "'," +
                                                "'" + TabPacien["DebeDere"].ToString() + "'," +
                                                "'" + TabPacien["UltiPeso"].ToString() + "'," +
                                                $"{ValidarFechaNula(TabPacien["FecPeso"].ToString())}" +
                                                "'" + TabPacien["CodRgPes"].ToString() + "'," +
                                                "'" + TabPacien["UltiTalla"].ToString() + "'," +
                                                $"{ValidarFechaNula(TabPacien["FecTalla"].ToString())}" +
                                                "'" + TabPacien["CodRgTal"].ToString() + "'," +
                                                "'" + TabPacien["CodPrefi"].ToString() + "'," +
                                                "'" + TabPacien["NomDepenLabo"].ToString() + "'," +
                                                "'" + TabPacien["CodARLLabo"].ToString() + "'," +
                                                "'" + TabPacien["CodAFPLabo"].ToString() + "'," +
                                                "'" + TabPacien["CodiRegis"].ToString() + "'," +
                                                $"{ValidarFechaNula(TabPacien["FecRegis"].ToString())}" +
                                                "'" + TabPacien["CodiModi"].ToString() + "'," +
                                                $"{ValidarFechaNula(TabPacien["FecModi"].ToString())}" +
                                                "'" + TabPacien["Discapacidad"].ToString() + "'," +
                                                "'" + TabPacien["NomCargoLabo"].ToString() + "'," +
                                                "'" + TabPacien["GruSangui"].ToString() + "'," +
                                                "'" + TabPacien["RhPaciente"].ToString() + "'," +
                                                "'" + TabPacien["PaciSolUrgen"].ToString() + "'" +
                                                ")";


                                                Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos);

                                                if (Regis)
                                                {
                                                    int con = Convert.ToInt32(TxtCanPaciForm.Text) + 1;
                                                    TxtCanPaciForm.Text = con.ToString();
                                                }

                                                ContiPro = 1;
                                            }
                                            else
                                            {
                                                TabPacienCentra.Read();

                                                if (TabPacienCentra["Nombre1"].ToString() == TabPacien["Nombre1"].ToString())
                                                {
                                                    if (TabPacienCentra["Apellido1"].ToString() == TabPacien["Apellido1"].ToString())
                                                    {
                                                        if (TabPacienCentra["Sexo"].ToString() == TabPacien["Sexo"].ToString())
                                                        {
                                                            //Modifique los datos
                                                            Utils.SqlDatos = $"UPDATE [ACDATOXPSQL].[dbo].[Datos del Paciente] SET " +
                                                            "HistorPaci  = '" + TabPacien["HistorPaci"].ToString() + "', " +
                                                            "TipoIden  = '" + TabPacien["TipoIden"].ToString() + "', " +
                                                            "NumIden  = '" + TabPacien["NumIden"].ToString() + "', " +
                                                            "Nombre1  = '" + TabPacien["Nombre1"].ToString() + "', " +
                                                            "Nombre2  = '" + TabPacien["Nombre2"].ToString() + "', " +
                                                            "Apellido1  = '" + TabPacien["Apellido1"].ToString() + "', " +
                                                            "Apellido2  = '" + TabPacien["Apellido2"].ToString() + "', " +
                                                            $"FechaNaci = {ValidarFechaNula(TabPacien["FechaNaci"].ToString())}" +
                                                            "HoraNaci = CONVERT(DATETIME,'" + TabPacien["HoraNaci"].ToString() + "',8)," +
                                                            "LugarNace  = '" + TabPacien["LugarNace"].ToString() + "', " +
                                                            "SemGesta  = '" + TabPacien["SemGesta"].ToString() + "', " +
                                                            "CodPaisNace  = '" + TabPacien["CodPaisNace"].ToString() + "', " +
                                                            "CodDptoNace  = '" + TabPacien["CodDptoNace"].ToString() + "', " +
                                                            "CodCiuNace  = '" + TabPacien["CodCiuNace"].ToString() + "', " +
                                                            "EstadoCivil  = '" + TabPacien["EstadoCivil"].ToString() + "', " +
                                                            "CodDpto  = '" + TabPacien["CodDpto"].ToString() + "', " +
                                                            "CodMuni  = '" + TabPacien["CodMuni"].ToString() + "', " +
                                                            "BarrioVive  = '" + TabPacien["BarrioVive"].ToString() + "', " +
                                                            "DirecResi  = '" + TabPacien["DirecResi"].ToString() + "', " +
                                                            "TelResi  = '" + TabPacien["TelResi"].ToString() + "', " +
                                                            "TelCelular  = '" + TabPacien["TelCelular"].ToString() + "', " +
                                                            "ZonaResiden  = '" + TabPacien["ZonaResiden"].ToString() + "', " +
                                                            $"FechaApertura = {ValidarFechaNula(TabPacien["FechaApertura"].ToString())}" +
                                                            "Observaciones  = '" + TabPacien["Observaciones"].ToString() + "', " +
                                                            "Sexo  = '" + TabPacien["Sexo"].ToString() + "', " +
                                                            "TipoUsar  = '" + TabPacien["TipoUsar"].ToString() + "', " +
                                                            "TipoAfiliado  = '" + TabPacien["TipoAfiliado"].ToString() + "', " +
                                                            "NumAfilia  = '" + TabPacien["NumAfilia"].ToString() + "', " +
                                                            "PolizaNum  = '" + TabPacien["PolizaNum"].ToString() + "', " +
                                                            "EstraNum  = '" + TabPacien["EstraNum"].ToString() + "', " +
                                                            "GrupoEtni  = '" + TabPacien["GrupoEtni"].ToString() + "', " +
                                                            "NumContra  = '" + TabPacien["NumContra"].ToString() + "', " +
                                                            "TipoCuenta  = '" + TabPacien["TipoCuenta"].ToString() + "', " +
                                                            "CausaRemite  = '" + TabPacien["CausaRemite"].ToString() + "', " +
                                                            "NombresRespon  = '" + TabPacien["NombresRespon"].ToString() + "', " +
                                                            "Apellido1Respon  = '" + TabPacien["Apellido1Respon"].ToString() + "', " +
                                                            "Apellido2Respon  = '" + TabPacien["Apellido2Respon"].ToString() + "', " +
                                                            "TipoDocuRespon  = '" + TabPacien["TipoDocuRespon"].ToString() + "', " +
                                                            "CedulaRespon  = '" + TabPacien["CedulaRespon"].ToString() + "', " +
                                                            "Parentesco  = '" + TabPacien["Parentesco"].ToString() + "', " +
                                                            "DireccPare  = '" + TabPacien["DireccPare"].ToString() + "', " +
                                                            "TelefonoPare  = '" + TabPacien["TelefonoPare"].ToString() + "', " +
                                                            "CiudadPare  = '" + TabPacien["CiudadPare"].ToString() + "', " +
                                                            "DptoRespon  = '" + TabPacien["DptoRespon"].ToString() + "', " +
                                                            "NombrePadre  = '" + TabPacien["NombrePadre"].ToString() + "', " +
                                                            "Apellido1Padre  = '" + TabPacien["Apellido1Padre"].ToString() + "', " +
                                                            "Apellido2Padre  = '" + TabPacien["Apellido2Padre"].ToString() + "', " +
                                                            "VivePadre  = '" + TabPacien["VivePadre"].ToString() + "', " +
                                                            "TipoDocuPadre  = '" + TabPacien["TipoDocuPadre"].ToString() + "', " +
                                                            "CedulaPadre  = '" + TabPacien["CedulaPadre"].ToString() + "', " +
                                                            "NombreMadre  = '" + TabPacien["NombreMadre"].ToString() + "', " +
                                                            "Apellido1Madre  = '" + TabPacien["Apellido1Madre"].ToString() + "', " +
                                                            "Apellido2Madre  = '" + TabPacien["Apellido2Madre"].ToString() + "', " +
                                                            "ViveMadre  = '" + TabPacien["ViveMadre"].ToString() + "', " +
                                                            "TipoDocuMadre  = '" + TabPacien["TipoDocuMadre"].ToString() + "', " +
                                                            "CedulaMadre  = '" + TabPacien["CedulaMadre"].ToString() + "', " +
                                                            "NombreEmpresa  = '" + TabPacien["NombreEmpresa"].ToString() + "', " +
                                                            "Ocupacion  = '" + TabPacien["Ocupacion"].ToString() + "', " +
                                                            "CiudadEmpresa  = '" + TabPacien["CiudadEmpresa"].ToString() + "', " +
                                                            "DireccTrabaja  = '" + TabPacien["DireccTrabaja"].ToString() + "', " +
                                                            "TeleEmpresa  = '" + TabPacien["TeleEmpresa"].ToString() + "', " +
                                                            "CodSeSocial  = '" + TabPacien["CodSeSocial"].ToString() + "', " +
                                                            "CodiAdmin  = '" + TabPacien["CodiAdmin"].ToString() + "', " +
                                                            "DptoRemite  = '" + TabPacien["DptoRemite"].ToString() + "', " +
                                                            "MunicipioRemite  = '" + TabPacien["MunicipioRemite"].ToString() + "', " +
                                                            "IPSRemite  = '" + TabPacien["IPSRemite"].ToString() + "', " +
                                                            "RemiNumero  = '" + TabPacien["RemiNumero"].ToString() + "', " +
                                                            $"FechaRemision = {ValidarFechaNula(TabPacien["FechaRemision"].ToString())}" +
                                                            $"FechaVence = {ValidarFechaNula(TabPacien["FechaVence"].ToString())}" +
                                                            "CubreRemision  = '" + TabPacien["CubreRemision"].ToString() + "', " +
                                                            "EspecialRemite  = '" + TabPacien["EspecialRemite"].ToString() + "', " +
                                                            "DxRemite  = '" + TabPacien["DxRemite"].ToString() + "', " +
                                                            "CoMediAten  = '" + TabPacien["CoMediAten"].ToString() + "', " +
                                                            "MotivoConsul  = '" + TabPacien["MotivoConsul"].ToString() + "', " +
                                                            $"FechaEntrada = {ValidarFechaNula(TabPacien["FechaEntrada"].ToString())}" +
                                                            "HoraEntrada = CONVERT(DATETIME,'" + TabPacien["HoraEntrada"].ToString() + "',8)," +
                                                            $"FecUltima = {ValidarFechaNula(TabPacien["FecUltima"].ToString())}" +
                                                            "ArchivoViene  = '" + TabPacien["ArchivoViene"].ToString() + "', " +
                                                            "Muerto  = '" + TabPacien["Muerto"].ToString() + "', " +
                                                            "PreInscripcion  = '" + TabPacien["PreInscripcion"].ToString() + "', " +
                                                            "CoberSalud  = '" + TabPacien["CoberSalud"].ToString() + "', " +
                                                            "Huella1  = '" + TabPacien["Huella1"].ToString() + "', " +
                                                            "Huella2  = '" + TabPacien["Huella2"].ToString() + "', " +
                                                            "Huella  = '" + TabPacien["Huella"].ToString() + "', " +
                                                            "NomEmeal  = '" + TabPacien["NomEmeal"].ToString() + "', " +
                                                            "NivEducUs  = '" + TabPacien["NivEducUs"].ToString() + "', " +
                                                            "DebeDere  = '" + TabPacien["DebeDere"].ToString() + "', " +
                                                            "UltiPeso  = '" + TabPacien["UltiPeso"].ToString() + "', " +
                                                            $"FecPeso = {ValidarFechaNula(TabPacien["FecPeso"].ToString())}" +
                                                            "CodRgPes  = '" + TabPacien["CodRgPes"].ToString() + "', " +
                                                            "UltiTalla  = '" + TabPacien["UltiTalla"].ToString() + "', " +
                                                            $"FecTalla = {ValidarFechaNula(TabPacien["FecTalla"].ToString())}" +
                                                            "CodRgTal  = '" + TabPacien["CodRgTal"].ToString() + "', " +
                                                            "CodPrefi  = '" + TabPacien["CodPrefi"].ToString() + "', " +
                                                            "NomDepenLabo  = '" + TabPacien["NomDepenLabo"].ToString() + "', " +
                                                            "CodARLLabo  = '" + TabPacien["CodARLLabo"].ToString() + "', " +
                                                            "CodAFPLabo  = '" + TabPacien["CodAFPLabo"].ToString() + "', " +
                                                            "CodiRegis  = '" + TabPacien["CodiRegis"].ToString() + "', " +
                                                            $"FecRegis = {ValidarFechaNula(TabPacien["FecRegis"].ToString())}" +
                                                            "CodiModi  = '" + TabPacien["CodiModi"].ToString() + "', " +
                                                            $"FecModi = {ValidarFechaNula(TabPacien["FecModi"].ToString())}" +
                                                            "Discapacidad  = '" + TabPacien["Discapacidad"].ToString() + "', " +
                                                            "NomCargoLabo  = '" + TabPacien["NomCargoLabo"].ToString() + "', " +
                                                            "GruSangui  = '" + TabPacien["GruSangui"].ToString() + "', " +
                                                            "RhPaciente  = '" + TabPacien["RhPaciente"].ToString() + "', " +
                                                            "PaciSolUrgen = '" + TabPacien["PaciSolUrgen"].ToString() + "' " +
                                                            "WHERE (HistorPaci = '" + CodPacien + "') ";

                                                            Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos);

                                                            if (ActControl)
                                                            {
                                                                int con = Convert.ToInt32(TxtCanPaciForm.Text) + 1;
                                                                TxtCanPaciForm.Text = con.ToString();

                                                            }

                                                            ContiPro = 1;

                                                            CodPacienPort = TabPacienCentra["HistorPaci"].ToString();

                                                            // SE DEBE EJECUTAR EL PROCESO DE MODIFICAION HISTORIAS CLINICAS PARA QUE ACTUALIZE TODAS LAS TABLAS QUE TIENE EL NUMERO ANTERIOR **********
                                                            //'*****  ACDATOXPSQL  *****
                                                            //'Actualizamos cuentas de consumos

                                                            ConectarPortatil();

                                                            SqlCueCons = "UPDATE [ACDATOXPSQL].[dbo].[Datos cuentas de consumos] ";
                                                            SqlCueCons = SqlCueCons + "SET HistoNum  = '" + CodPacien + "' ";
                                                            SqlCueCons = SqlCueCons + "WHERE HistoNum = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlCueCons);

                                                            //'*****  DACONEXTSQL  *****
                                                            //'Actualizamos atencion de la consulta
                                                            SqlAtenCon = "UPDATE [DACONEXTSQL].[dbo].[Datos atencion de la consulta] ";
                                                            SqlAtenCon = SqlAtenCon + "SET HistoriaNum  = '" + CodPacien + "' ";
                                                            SqlAtenCon = SqlAtenCon + "WHERE HistoriaNum = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlAtenCon);

                                                            //Actualizamos anotaciones anexas historias

                                                            SqlAnoAneHis = "UPDATE [DACONEXTSQL].[dbo].[Datos anotaciones anexas historias] ";
                                                            SqlAnoAneHis += "SET HistoNumero  = '" + CodPacien + "' ";
                                                            SqlAnoAneHis += "WHERE HistoNumero = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlAnoAneHis);

                                                            //Actualizamos antecedentes pacientes

                                                            SqlAntPac = "UPDATE [DACONEXTSQL].[dbo].[Datos antecedentes pacientes] ";
                                                            SqlAntPac += "SET HistoNumero  = '" + CodPacien + "' ";
                                                            SqlAntPac += "WHERE HistoNumero = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlAntPac);

                                                            //Actualizamos remisiones

                                                            SqlRemi = "UPDATE [DACONEXTSQL].[dbo].[Datos de las remisiones] ";
                                                            SqlRemi = SqlRemi + "SET HistoriaPaci  = '" + CodPacien + "' ";
                                                            SqlRemi = SqlRemi + "WHERE HistoriaPaci = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlRemi);

                                                            //'Actualizamos tratamientos


                                                            SqlTrat = "UPDATE [DACONEXTSQL].[dbo].[Datos de los tratamientos] ";
                                                            SqlTrat += "SET HistorTrata  = '" + CodPacien + "' ";
                                                            SqlTrat += "WHERE HistorTrata = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlTrat);

                                                            //Actualizamos TRIAGE

                                                            SqlTRIAGE = "UPDATE [DACONEXTSQL].[dbo].[Datos de los TRIAGE realizados] ";
                                                            SqlTRIAGE += "SET NumeroHistoria  = '" + CodPacien + "' ";
                                                            SqlTRIAGE += "WHERE NumeroHistoria = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlTRIAGE);

                                                            //Actualizamos detalle vacunas aplicadas

                                                            SqlDetVacApl = "UPDATE [DACONEXTSQL].[dbo].[Datos detalle vacunas aplicadas] ";
                                                            SqlDetVacApl = SqlDetVacApl + "SET HistoriaVac  = '" + CodPacien + "' ";
                                                            SqlDetVacApl = SqlDetVacApl + "WHERE HistoriaVac = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlDetVacApl);

                                                            //Actualizamos informe quirurgico

                                                            SqlInfQui = "UPDATE [DACONEXTSQL].[dbo].[Datos informe quirurgico] ";
                                                            SqlInfQui += "SET HistoriaPac  = '" + CodPacien + "' ";
                                                            SqlInfQui += "WHERE HistoriaPac = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlDetVacApl);

                                                            //Actualizamos registro control placa

                                                            SqlRegContPlac = "UPDATE [DACONEXTSQL].[dbo].[Datos registro control placa] ";
                                                            SqlRegContPlac += "SET HistoriaPaci  = '" + CodPacien + "' ";
                                                            SqlRegContPlac += "WHERE HistoriaPaci = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlRegContPlac);

                                                            //Actualizamos registros de ecografias

                                                            SqlRegEco = "UPDATE [DACONEXTSQL].[dbo].[Datos registros de ecografias] ";
                                                            SqlRegEco += "SET NumHisEco  = '" + CodPacien + "' ";
                                                            SqlRegEco += "WHERE NumHisEco = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlRegEco);

                                                            //Actualizamos seguimiento de controles

                                                            SqlSegCont = "UPDATE [DACONEXTSQL].[dbo].[Datos seguimiento de controles] ";
                                                            SqlSegCont += "SET HistoriaNum  = '" + CodPacien + "' ";
                                                            SqlSegCont += "WHERE HistoriaNum = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlSegCont);

                                                            //*****  BDSITOI  *****
                                                            //'Actualizamos basicos citologia

                                                            SqlBasCito = "UPDATE [BDSITOI].[dbo].[Datos basicos citologia] ";
                                                            SqlBasCito += "SET NumHisto  = '" + CodPacien + "' ";
                                                            SqlBasCito += "WHERE NumHisto = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlBasCito);

                                                            //'*****  BDBIOMETSQL  *****
                                                            //Actualizamos digitalizacion de firma

                                                            SqlBasBioFir = "UPDATE [BDBIOMETSQL].[dbo].[Datos firma digital] ";
                                                            SqlBasBioFir = SqlBasBioFir + "SET HistorPaci  = '" + CodPacien + "' ";
                                                            SqlBasBioFir = SqlBasBioFir + "WHERE HistorPaci = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlBasBioFir);

                                                            //Actualizamos digitalizacion de firma modificadas

                                                            SqlBasBioFirmod = "UPDATE  [BDBIOMETSQL].[dbo].[Datos modificados firma] ";
                                                            SqlBasBioFirmod += "SET HistorPaci  = '" + CodPacien + "' ";
                                                            SqlBasBioFirmod += "WHERE HistorPaci = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlBasBioFirmod);


                                                            //Actualizamos digitalizacion de foto

                                                            SqlBasBioFot = "UPDATE [BDBIOMETSQL].[dbo].[Datos foto digital] ";
                                                            SqlBasBioFot = SqlBasBioFot + "SET HistorPaci  = '" + CodPacien + "' ";
                                                            SqlBasBioFot = SqlBasBioFot + "WHERE HistorPaci = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlBasBioFot);

                                                            //Actualizamos digitalizacion de foto modificadas

                                                            SqlBasBioFotmod = "UPDATE [BDBIOMETSQL].[Datos modificados foto] ";
                                                            SqlBasBioFotmod = SqlBasBioFotmod + "SET HistorPaci  = '" + CodPacien + "' ";
                                                            SqlBasBioFotmod = SqlBasBioFotmod + "WHERE HistorPaci = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlBasBioFotmod);

                                                            //Actualizamos digitalizacion de huella

                                                            SqlBasBioHue = "UPDATE [BDBIOMETSQL].[Datos huella digital] ";
                                                            SqlBasBioHue = SqlBasBioHue + "SET HistorPaci  = '" + CodPacien + "' ";
                                                            SqlBasBioHue = SqlBasBioHue + "WHERE HistorPaci = N'" + CodPacienPort + "' ";

                                                            ActDatos = Conexion.SQLUpdate(SqlBasBioHue);


                                                        }
                                                        else
                                                        {
                                                            Utils.Informa = "lo siento pero el numero de historia " + CodPacien + "\r";
                                                            Utils.Informa += "No se encuentra registrado en el portatil, pero la misma no se " + "\r";
                                                            Utils.Informa += "puede adicionar por que existe la historia  " + "\r";
                                                            Utils.Informa += "No. " + TabPacienCentra["HistorPaci"].ToString() + " la cual tiene el mismo documento  " + "\r";
                                                            Utils.Informa += "de identificacion con diferente Sexo." + "\r";
                                                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Utils.Informa = "lo siento pero el numero de historia " + CodPacien + "\r";
                                                        Utils.Informa += "No se encuentra registrado en el portatil, pero la misma no se " + "\r";
                                                        Utils.Informa += "puede adicionar por que existe la historia  " + "\r";
                                                        Utils.Informa += "No. " + TabPacienCentra["HistorPaci"].ToString() + " la cual tiene el mismo documento  " + "\r";
                                                        Utils.Informa += "de identificacion con diferentes nombre." + "\r";
                                                        MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                    }
                                                }
                                            }////if(TabPacienCentra1.HasRows == false)
                                        }
                                        TabPacienCentra1.Close();
                                    }
                                    else
                                    {
                                        //Modifique los datos del paciente en el equipo de BRIGADA

                                        Utils.SqlDatos = $"UPDATE [ACDATOXPSQL].[dbo].[Datos del Paciente] SET " +                                                    
                                                        "TipoIden  = '" + TabPacien["TipoIden"].ToString() + "', " +
                                                        "NumIden  = '" + TabPacien["NumIden"].ToString() + "', " +
                                                        "Nombre1  = '" + TabPacien["Nombre1"].ToString() + "', " +
                                                        "Nombre2  = '" + TabPacien["Nombre2"].ToString() + "', " +
                                                        "Apellido1  = '" + TabPacien["Apellido1"].ToString() + "', " +
                                                        "Apellido2  = '" + TabPacien["Apellido2"].ToString() + "', " +
                                                        $"FechaNaci = {ValidarFechaNula(TabPacien["FechaNaci"].ToString())}" +
                                                        "HoraNaci = CONVERT(DATETIME,'" + TabPacien["HoraNaci"].ToString() + "',8)," +
                                                        "LugarNace  = '" + TabPacien["LugarNace"].ToString() + "', " +
                                                        "SemGesta  = '" + TabPacien["SemGesta"].ToString() + "', " +
                                                        "CodPaisNace  = '" + TabPacien["CodPaisNace"].ToString() + "', " +
                                                        "CodDptoNace  = '" + TabPacien["CodDptoNace"].ToString() + "', " +
                                                        "CodCiuNace  = '" + TabPacien["CodCiuNace"].ToString() + "', " +
                                                        "EstadoCivil  = '" + TabPacien["EstadoCivil"].ToString() + "', " +
                                                        "CodDpto  = '" + TabPacien["CodDpto"].ToString() + "', " +
                                                        "CodMuni  = '" + TabPacien["CodMuni"].ToString() + "', " +
                                                        "BarrioVive  = '" + TabPacien["BarrioVive"].ToString() + "', " +
                                                        "DirecResi  = '" + TabPacien["DirecResi"].ToString() + "', " +
                                                        "TelResi  = '" + TabPacien["TelResi"].ToString() + "', " +
                                                        "TelCelular  = '" + TabPacien["TelCelular"].ToString() + "', " +
                                                        "ZonaResiden  = '" + TabPacien["ZonaResiden"].ToString() + "', " +
                                                        $"FechaApertura = {ValidarFechaNula(TabPacien["FechaApertura"].ToString())}" +
                                                        "Observaciones  = '" + TabPacien["Observaciones"].ToString() + "', " +
                                                        "Sexo  = '" + TabPacien["Sexo"].ToString() + "', " +
                                                        "TipoUsar  = '" + TabPacien["TipoUsar"].ToString() + "', " +
                                                        "TipoAfiliado  = '" + TabPacien["TipoAfiliado"].ToString() + "', " +
                                                        "NumAfilia  = '" + TabPacien["NumAfilia"].ToString() + "', " +
                                                        "PolizaNum  = '" + TabPacien["PolizaNum"].ToString() + "', " +
                                                        "EstraNum  = '" + TabPacien["EstraNum"].ToString() + "', " +
                                                        "GrupoEtni  = '" + TabPacien["GrupoEtni"].ToString() + "', " +
                                                        "NumContra  = '" + TabPacien["NumContra"].ToString() + "', " +
                                                        "TipoCuenta  = '" + TabPacien["TipoCuenta"].ToString() + "', " +
                                                        "CausaRemite  = '" + TabPacien["CausaRemite"].ToString() + "', " +
                                                        "NombresRespon  = '" + TabPacien["NombresRespon"].ToString() + "', " +
                                                        "Apellido1Respon  = '" + TabPacien["Apellido1Respon"].ToString() + "', " +
                                                        "Apellido2Respon  = '" + TabPacien["Apellido2Respon"].ToString() + "', " +
                                                        "TipoDocuRespon  = '" + TabPacien["TipoDocuRespon"].ToString() + "', " +
                                                        "CedulaRespon  = '" + TabPacien["CedulaRespon"].ToString() + "', " +
                                                        "Parentesco  = '" + TabPacien["Parentesco"].ToString() + "', " +
                                                        "DireccPare  = '" + TabPacien["DireccPare"].ToString() + "', " +
                                                        "TelefonoPare  = '" + TabPacien["TelefonoPare"].ToString() + "', " +
                                                        "CiudadPare  = '" + TabPacien["CiudadPare"].ToString() + "', " +
                                                        "DptoRespon  = '" + TabPacien["DptoRespon"].ToString() + "', " +
                                                        "NombrePadre  = '" + TabPacien["NombrePadre"].ToString() + "', " +
                                                        "Apellido1Padre  = '" + TabPacien["Apellido1Padre"].ToString() + "', " +
                                                        "Apellido2Padre  = '" + TabPacien["Apellido2Padre"].ToString() + "', " +
                                                        "VivePadre  = '" + TabPacien["VivePadre"].ToString() + "', " +
                                                        "TipoDocuPadre  = '" + TabPacien["TipoDocuPadre"].ToString() + "', " +
                                                        "CedulaPadre  = '" + TabPacien["CedulaPadre"].ToString() + "', " +
                                                        "NombreMadre  = '" + TabPacien["NombreMadre"].ToString() + "', " +
                                                        "Apellido1Madre  = '" + TabPacien["Apellido1Madre"].ToString() + "', " +
                                                        "Apellido2Madre  = '" + TabPacien["Apellido2Madre"].ToString() + "', " +
                                                        "ViveMadre  = '" + TabPacien["ViveMadre"].ToString() + "', " +
                                                        "TipoDocuMadre  = '" + TabPacien["TipoDocuMadre"].ToString() + "', " +
                                                        "CedulaMadre  = '" + TabPacien["CedulaMadre"].ToString() + "', " +
                                                        "NombreEmpresa  = '" + TabPacien["NombreEmpresa"].ToString() + "', " +
                                                        "Ocupacion  = '" + TabPacien["Ocupacion"].ToString() + "', " +
                                                        "CiudadEmpresa  = '" + TabPacien["CiudadEmpresa"].ToString() + "', " +
                                                        "DireccTrabaja  = '" + TabPacien["DireccTrabaja"].ToString() + "', " +
                                                        "TeleEmpresa  = '" + TabPacien["TeleEmpresa"].ToString() + "', " +
                                                        "CodSeSocial  = '" + TabPacien["CodSeSocial"].ToString() + "', " +
                                                        "CodiAdmin  = '" + TabPacien["CodiAdmin"].ToString() + "', " +
                                                        "DptoRemite  = '" + TabPacien["DptoRemite"].ToString() + "', " +
                                                        "MunicipioRemite  = '" + TabPacien["MunicipioRemite"].ToString() + "', " +
                                                        "IPSRemite  = '" + TabPacien["IPSRemite"].ToString() + "', " +
                                                        "RemiNumero  = '" + TabPacien["RemiNumero"].ToString() + "', " +
                                                        $"FechaRemision = {ValidarFechaNula(TabPacien["FechaRemision"].ToString())}" +
                                                        $"FechaVence = {ValidarFechaNula(TabPacien["FechaVence"].ToString())}" +
                                                        "CubreRemision  = '" + TabPacien["CubreRemision"].ToString() + "', " +
                                                        "EspecialRemite  = '" + TabPacien["EspecialRemite"].ToString() + "', " +
                                                        "DxRemite  = '" + TabPacien["DxRemite"].ToString() + "', " +
                                                        "CoMediAten  = '" + TabPacien["CoMediAten"].ToString() + "', " +
                                                        "MotivoConsul  = '" + TabPacien["MotivoConsul"].ToString() + "', " +
                                                        $"FechaEntrada = {ValidarFechaNula(TabPacien["FechaEntrada"].ToString())}" +
                                                        "HoraEntrada = CONVERT(DATETIME,'" + TabPacien["HoraEntrada"].ToString() + "',8)," +
                                                        $"FecUltima = {ValidarFechaNula(TabPacien["FecUltima"].ToString())}" +
                                                        "ArchivoViene  = '" + TabPacien["ArchivoViene"].ToString() + "', " +
                                                        "Muerto  = '" + TabPacien["Muerto"].ToString() + "', " +
                                                        "PreInscripcion  = '" + TabPacien["PreInscripcion"].ToString() + "', " +
                                                        "CoberSalud  = '" + TabPacien["CoberSalud"].ToString() + "', " +
                                                        "Huella1  = '" + TabPacien["Huella1"].ToString() + "', " +
                                                        "Huella2  = '" + TabPacien["Huella2"].ToString() + "', " +
                                                        "Huella  = '" + TabPacien["Huella"].ToString() + "', " +
                                                        "NomEmeal  = '" + TabPacien["NomEmeal"].ToString() + "', " +
                                                        "NivEducUs  = '" + TabPacien["NivEducUs"].ToString() + "', " +
                                                        "DebeDere  = '" + TabPacien["DebeDere"].ToString() + "', " +
                                                        "UltiPeso  = '" + TabPacien["UltiPeso"].ToString() + "', " +
                                                        $"FecPeso = {ValidarFechaNula(TabPacien["FecPeso"].ToString())}" +
                                                        "CodRgPes  = '" + TabPacien["CodRgPes"].ToString() + "', " +
                                                        "UltiTalla  = '" + TabPacien["UltiTalla"].ToString() + "', " +
                                                        $"FecTalla = {ValidarFechaNula(TabPacien["FecTalla"].ToString())}" +
                                                        "CodRgTal  = '" + TabPacien["CodRgTal"].ToString() + "', " +
                                                        "CodPrefi  = '" + TabPacien["CodPrefi"].ToString() + "', " +
                                                        "NomDepenLabo  = '" + TabPacien["NomDepenLabo"].ToString() + "', " +
                                                        "CodARLLabo  = '" + TabPacien["CodARLLabo"].ToString() + "', " +
                                                        "CodAFPLabo  = '" + TabPacien["CodAFPLabo"].ToString() + "', " +
                                                        "CodiRegis  = '" + TabPacien["CodiRegis"].ToString() + "', " +
                                                        $"FecRegis = {ValidarFechaNula(TabPacien["FecRegis"].ToString())}" +
                                                        "CodiModi  = '" + TabPacien["CodiModi"].ToString() + "', " +
                                                        $"FecModi = {ValidarFechaNula(TabPacien["FecModi"].ToString())}" +
                                                        "Discapacidad  = '" + TabPacien["Discapacidad"].ToString() + "', " +
                                                        "NomCargoLabo  = '" + TabPacien["NomCargoLabo"].ToString() + "', " +
                                                        "GruSangui  = '" + TabPacien["GruSangui"].ToString() + "', " +
                                                        "RhPaciente  = '" + TabPacien["RhPaciente"].ToString() + "', " +
                                                        "PaciSolUrgen = '" + TabPacien["PaciSolUrgen"].ToString() + "' " +
                                                        "WHERE (HistorPaci = '" + CodPacien + "')";

                                        Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos);

                                        if (ActControl)
                                        {
                                            int con = Convert.ToInt32(TxtPaciExis.Text) + 1;
                                            TxtPaciExis.Text = con.ToString();
                                        }

                                        ContiPro = 1;

                                    }//'Final deif (TabPlacaCen.HasRows == false)

                                    int FunBiometFir = BiometFirEXP(CodPacien);
                                    int FunBiometFot = BiometFotEXP(CodPacien);
                                    int FunBiometHue = BiometHueEXP(CodPacien);

                                    TabPacienCentra.Close();

                                }//USing
                            }//While

                            Utils.Informa = "El proceso ha terminado satisfactoriamente";
                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        TabPacien.Close();

                    }

                }

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "despues de dar click en integrar" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int BiometHueEXP(string CodHisPacHue)
        {
            try
            {
                ConectarCentral();
                string SqlBiometHueCen, SqlBiometHue;
                //Revisamos si el numero de historia del paciente existe en el equipo de BRIGADA

                SqlBiometHueCen = "SELECT [Datos huella digital].* ";
                SqlBiometHueCen += "FROM [BDBIOMETSQL].[dbo].[Datos huella digital] ";
                SqlBiometHueCen += "WHERE (HistorPaci = N'" + CodHisPacHue + "')";

                SqlDataReader TabBiometHueCen, TabBiometHue;

                using (SqlConnection connection3 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command3 = new SqlCommand(SqlBiometHueCen, connection3);
                    command3.Connection.Open();
                    TabBiometHueCen = command3.ExecuteReader();


                    if (TabBiometHueCen.HasRows == false)
                    {
                        //No hay datos del paciente a consultar
                    }
                    else
                    {
                        ConectarPortatil();
                        //Revisamos si el numero de historia del paciente existe en el equipo de BRIGADA
                        SqlBiometHue = "SELECT [Datos huella digital].* ";
                        SqlBiometHue += "FROM [BDBIOMETSQL].[dbo].[Datos huella digital] ";
                        SqlBiometHue += "WHERE (HistorPaci = N'" + CodHisPacHue + "')";


                        using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                        {
                            SqlCommand command = new SqlCommand(SqlBiometHue, connection);
                            command.Connection.Open();
                            TabBiometHue = command.ExecuteReader();

                            if (TabBiometHue.HasRows == false)
                            {
                                Utils.SqlDatos = "INSERT INTO [BDBIOMETSQL].[dbo].[Datos huella digital]" +
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
                                "'" + TabBiometHue["HistorPaci"].ToString() + "'," +
                                "'" + TabBiometHue["huella_tpt"].ToString() + "'," +
                                "'" + TabBiometHue["Huella_img"].ToString() + "'," +
                                "'" + TabBiometHue["CodiRegis"].ToString() + "'," +
                                $"{ValidarFechaNula(TabBiometHue["FecRegis"].ToString())}" +
                                 $"{ValidarFechaNula(TabBiometHue["FecModi"].ToString())}" +
                                "'" + TabBiometHue["CodModi"].ToString() + "'" +
                                ")";

                                Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos);
                            }
                            else
                            {
                                TabBiometHue.Read();
                                if (Convert.ToDateTime(TabBiometHueCen["FecRegis"]) > Convert.ToDateTime(TabBiometHue["FecRegis"]))
                                {

                                    //Modifique los datos
                                    Utils.SqlDatos = $"UPDATE [BDBIOMETSQL].[dbo].[Datos huella digital] SET " +
                                    "huella_tpt = '" + TabBiometHueCen["huella_tpt"].ToString() + "', " +
                                    "Huella_img = '" + TabBiometHueCen["Huella_img"].ToString() + "', " +
                                    $"FecModi = {ValidarFechaNula(TabBiometHueCen["FecModi"].ToString())} " +
                                    "CodModi = '" + TabBiometHueCen["CodModi"].ToString() + "' " +
                                    "WHERE (HistorPaci = N'" + CodHisPacHue + "') ";

                                    Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos);
                                }
                            }

                        }

                        TabBiometHue.Close();

                    }
                }

                TabBiometHueCen.Close();
                return 1;


            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion BiometHueEXP" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private int BiometFotEXP(string CodHisPacFot)
        {
            try
            {
                ConectarCentral();
                //Tomamos el numero de identificacion del paciente del SERVIDOR centrar

                string SqlBiometFotCen, SqlBiometFot;
                SqlDataReader TabBiometFotCen, TabBiometFot;

                SqlBiometFotCen = "SELECT [Datos foto digital].* ";
                SqlBiometFotCen += "FROM [BDBIOMETSQL].[dbo].[Datos foto digital] ";
                SqlBiometFotCen += "WHERE (HistorPaci = N'" + CodHisPacFot + "')";

                using (SqlConnection connection3 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command3 = new SqlCommand(SqlBiometFotCen, connection3);
                    command3.Connection.Open();
                    TabBiometFotCen = command3.ExecuteReader();


                    if(TabBiometFotCen.HasRows == false)
                    {
                        //No hay datos del paciente a consultar
                    }
                    else
                    {
                        ConectarPortatil();
                        //Revisamos si el numero de historia del paciente existe en el equipo de BRIGADA
                        SqlBiometFot = "SELECT [Datos foto digital].* ";
                        SqlBiometFot += "FROM [BDBIOMETSQL].[dbo].[Datos foto digital] ";
                        SqlBiometFot += "WHERE (HistorPaci = N'" + CodHisPacFot + "')";


                        using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                        {
                            SqlCommand command = new SqlCommand(SqlBiometFot, connection);
                            command.Connection.Open();
                            TabBiometFot = command.ExecuteReader();

                            if(TabBiometFot.HasRows == false)
                            {
                                Utils.SqlDatos = "INSERT INTO [BDBIOMETSQL].[dbo].[Datos foto digital]" +
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
                                "'" + TabBiometFotCen["HistorPaci"].ToString() + "'," +
                                "'" + TabBiometFotCen["Foto"].ToString() + "'," +
                                "'" + TabBiometFotCen["CodiRegis"].ToString() + "'," +
                                $"{ValidarFechaNula(TabBiometFotCen["FecRegis"].ToString())}" +
                                 $"{ValidarFechaNula(TabBiometFotCen["FecModi"].ToString())}" +
                                "'" + TabBiometFotCen["CodModi"].ToString() + "'" +
                                ")";

                                Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos);
                            }
                            else
                            {
                                TabBiometFot.Read();
                                if (Convert.ToDateTime(TabBiometFotCen["FecRegis"]) > Convert.ToDateTime(TabBiometFot["FecRegis"]))
                                {

                                    //Modifique los datos
                                    Utils.SqlDatos = $"UPDATE [BDBIOMETSQL].[dbo].[Datos foto digital] SET " +
                                    "Foto = '" + TabBiometFotCen["Firma"].ToString() + "', " +
                                    $"FecModi = {ValidarFechaNula(TabBiometFotCen["FecModi"].ToString())} " +
                                    "CodModi = '" + TabBiometFotCen["CodModi"].ToString() + "' " +
                                    "WHERE (HistorPaci = N'" + CodHisPacFot + "') ";

                                    Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos);
                                }
                            }

                        }
                        TabBiometFot.Close();

                    }
                }

                TabBiometFotCen.Close();
                return 1;
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion BiometFotEXP" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private int BiometFirEXP(string CodHisPac)
        {
            try
            {
                string SqlBiometFirCen, SqlBiometFir;

                ConectarCentral();

                SqlBiometFirCen = "SELECT [Datos firma digital].* ";
                SqlBiometFirCen = SqlBiometFirCen + "FROM  [BDBIOMETSQL].[dbo].[Datos firma digital] ";
                SqlBiometFirCen = SqlBiometFirCen + "WHERE (HistorPaci = N'" + CodHisPac + "')";

                SqlDataReader TabBiometFirCen, TabBiometFir;

                using (SqlConnection connection3 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command3 = new SqlCommand(SqlBiometFirCen, connection3);
                    command3.Connection.Open();
                    TabBiometFirCen = command3.ExecuteReader();

                    if (TabBiometFirCen.HasRows == false)
                    {
                        // No hay datos del paciente a consultar
                    }
                    else
                    {
                        TabBiometFirCen.Read();
                        //Revisamos si el numero de historia del paciente existe en el equipo de BRIGADA
                        ConectarPortatil();
                        SqlBiometFir = "SELECT [Datos firma digital].* ";
                        SqlBiometFir = SqlBiometFir + "FROM [BDBIOMETSQL].[dbo].[Datos firma digital] ";
                        SqlBiometFir = SqlBiometFir + "WHERE (HistorPaci = N'" + CodHisPac + "')";

                        using (SqlConnection connection4 = new SqlConnection(Conexion.conexionSQL))
                        {
                            SqlCommand command4 = new SqlCommand(SqlBiometFir, connection4);
                            command4.Connection.Open();
                            TabBiometFir = command4.ExecuteReader();

                            if (TabBiometFir.HasRows == false)
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
                                    "'" + TabBiometFirCen["HistorPaci"].ToString() + "'," +
                                    "'" + TabBiometFirCen["Firma"].ToString() + "'," +
                                    "'" + TabBiometFirCen["CodiRegis"].ToString() + "'," +
                                    $"{ValidarFechaNula(TabBiometFirCen["FecRegis"].ToString())}" +
                                     $"{ValidarFechaNula(TabBiometFirCen["FecModi"].ToString())}" +
                                    "'" + TabBiometFirCen["CodModi"].ToString() + "'" +
                                    ")";

                                    Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos);

                            }
                            else
                            {
                                TabBiometFir.Read();
                                if (Convert.ToDateTime(TabBiometFirCen["FecRegis"]) > Convert.ToDateTime(TabBiometFir["FecRegis"]))
                                {

                                    //Modifique los datos
                                    Utils.SqlDatos = $"UPDATE [BDBIOMETSQL].[dbo].[Datos firma digital] SET " +
                                    "Firma = '" + TabBiometFirCen["Firma"].ToString() + "', " +
                                    $"FecModi = {ValidarFechaNula(TabBiometFirCen["FecModi"].ToString())} " +
                                    "CodModi = '" + TabBiometFirCen["CodModi"].ToString() + "' " +
                                    "WHERE (HistorPaci = N'" + CodHisPac + "') ";

                                    Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos);
                                }

                            }
                        }
                        TabBiometFir.Close();
                    }
                }
                TabBiometFirCen.Close();
                return 1;
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion BiometFirEXP" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

    }
}