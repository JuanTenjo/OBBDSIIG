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
    public partial class FrmImportSedeCentralHistorias : Form
    {
        public FrmImportSedeCentralHistorias()
        {
            InitializeComponent();
        }


        #region Funciones
        private int RemisionesIMP(string CodHistRE)
        {
            try
            {
                string CodAteRem, CodRem, TipConReg, SqlRemis, SqlRemisCen;
                int FunDetTratam = 0, FunRegControles = 0, FunDetVacuApl = 0;

                SqlDataReader TabRemis, TabRemisCen;

                SqlRemis = "SELECT [Datos de las remisiones].* ";
                SqlRemis += "FROM [DACONEXTSQL].[dbo].[Datos de las remisiones] ";
                SqlRemis += "WHERE ([Datos de las remisiones].NumeroAten = N'" + CodHistRE + "')";


                ConectarCentral();

                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command2 = new SqlCommand(SqlRemis, connection2);
                    command2.Connection.Open();
                    TabRemis = command2.ExecuteReader();


                    if (TabRemis.HasRows == false)
                    {
                        //'No hay registros en la tabla Datos registro  la tabla Datos registro de procedimientos
                        return 0;
                    }
                    else
                    {
                    
                        while (TabRemis.Read())
                        {
                            ConectarPortatil();
                            //Revisamos si el número de codigo de atencion existe
                            CodAteRem = TabRemis["NumeroAten"].ToString();
                            CodRem = TabRemis["RemisionNum"].ToString();

                            SqlRemisCen = "SELECT * FROM [DACONEXTSQL].[dbo].[Datos de las remisiones] ";
                            SqlRemisCen += "WHERE NumeroAten = N'" + CodAteRem + "' AND RemisionNum = N'" + CodRem + "'";


                            using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command = new SqlCommand(SqlRemisCen, connection);
                                command.Connection.Open();
                                TabRemisCen = command.ExecuteReader();

                                if (TabRemisCen.HasRows == false)
                                {
                                    Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos de las remisiones]  " +
                                    "(" +
                                    "RemisionNum," +
                                    "HistoriaPaci," +
                                    "NumeroAten," +
                                    "FechaEgreso," +
                                    "HoraEgreso," +
                                    "RegimenRemis," +
                                    "CardinalEmp," +
                                    "ServiRemite," +
                                    "Esperefere," +
                                    "Modalidad," +
                                    "MotivoRemi," +
                                    "NivelRemite," +
                                    "NivelRefere," +
                                    "HoraSolicita," +
                                    "HoraConfirma," +
                                    "Qconfirma,";
                                    if (string.IsNullOrWhiteSpace(TabRemis["Conductor"].ToString()) == false)
                                    {
                                        Utils.SqlDatos += "Conductor,";
                                    }
                                    if (string.IsNullOrWhiteSpace(TabRemis["PlacaAmbu"].ToString()) == false)
                                    {
                                        Utils.SqlDatos += "PlacaAmbu,";
                                    }
                                    Utils.SqlDatos += "ServiRecibe," +
                                    "InstituRefer," +
                                    "CodigoDpto," +
                                    "CodigoCiudad," +
                                    "EspeciaAlrefere," +
                                    "ResultadoExamen," +
                                    "JustificaREF," +
                                    "PrograPyP," +
                                    "CodAnul," +
                                    "FechAnul," +
                                    "Anulado," +
                                    "RazonesAnula," +
                                    "EfectoTerapeutico," +
                                    "AlternativaPOS," +
                                    "CopPOSAlternativo," +
                                    "PorquenoSerealiza," +
                                    "Prioridad," +
                                    "TipoRem," +
                                    "Control," +
                                    "MedicoRemite," +
                                    "Frecuencia," +
                                    "ConResponRe," +
                                    "TipDocRespo," +
                                    "NumDocRespo," +
                                    "NomRespon," +
                                    "Apel1ResRem," +
                                    "Apel2ResRem," +
                                    "CodDptoRes," +
                                    "CodMuniRes," +
                                    "DirecResRe," +
                                    "TelResRe," +
                                    "DxRemite" +
                                    ")" +
                                    "VALUES (" +
                                    "'" + TabRemis["RemisionNum"].ToString() + "'," +
                                    "'" + TabRemis["HistoriaPaci"].ToString() + "'," +
                                    "'" + TabRemis["NumeroAten"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabRemis["FechaEgreso"].ToString())}" +
                                    $"{Conexion.ValidarHoraNula(TabRemis["HoraEgreso"].ToString())}" +
                                    "'" + TabRemis["RegimenRemis"].ToString() + "'," +
                                    "'" + TabRemis["CardinalEmp"].ToString() + "'," +
                                    "'" + TabRemis["ServiRemite"].ToString() + "'," +
                                    "'" + TabRemis["Esperefere"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabRemis["Modalidad"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabRemis["MotivoRemi"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabRemis["NivelRemite"].ToString() + "'," +
                                    "'" + TabRemis["NivelRefere"].ToString() + "'," +
                                    $"{Conexion.ValidarHoraNula(TabRemis["HoraSolicita"].ToString())}" +
                                    $"{Conexion.ValidarHoraNula(TabRemis["HoraConfirma"].ToString())}" +
                                    "'" + TabRemis["Qconfirma"].ToString() + "',";

                                    if (string.IsNullOrWhiteSpace(TabRemis["Conductor"].ToString()) == false)
                                    {
                                        Utils.SqlDatos += "'" + TabRemis["Conductor"].ToString() + "',";
                                    }
                                    if (string.IsNullOrWhiteSpace(TabRemis["PlacaAmbu"].ToString()) == false)
                                    {
                                        Utils.SqlDatos += "'" + TabRemis["PlacaAmbu"].ToString() + "',";
                                    }

                                    Utils.SqlDatos += "'" + TabRemis["ServiRecibe"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabRemis["InstituRefer"].ToString() + "'," +
                                    "'" + TabRemis["CodigoDpto"].ToString() + "'," +
                                    "'" + TabRemis["CodigoCiudad"].ToString() + "'," +
                                    "'" + TabRemis["EspeciaAlrefere"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabRemis["ResultadoExamen"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabRemis["JustificaREF"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabRemis["PrograPyP"].ToString() + "'," +
                                    "'" + TabRemis["CodAnul"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabRemis["FechAnul"].ToString())}" +
                                    "'" + TabRemis["Anulado"].ToString() + "'," +
                                    "'" + TabRemis["RazonesAnula"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabRemis["EfectoTerapeutico"].ToString() + "'," +
                                    "'" + TabRemis["AlternativaPOS"].ToString() + "'," +
                                    "'" + TabRemis["CopPOSAlternativo"].ToString() + "'," +
                                    "'" + TabRemis["PorquenoSerealiza"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabRemis["Prioridad"].ToString() + "'," +
                                    "'" + TabRemis["TipoRem"].ToString() + "'," +
                                    "'" + TabRemis["Control"].ToString() + "'," +
                                    "'" + TabRemis["MedicoRemite"].ToString() + "'," +
                                    //'***** CAMPOS AGREGADOS CON BASE DATOS DE SAN AGUSTIN *****
                                    "'" + TabRemis["Frecuencia"].ToString() + "'," +
                                    "'" + TabRemis["ConResponRe"].ToString() + "'," +
                                    "'" + TabRemis["TipDocRespo"].ToString() + "'," +
                                    "'" + TabRemis["NumDocRespo"].ToString() + "'," +
                                    "'" + TabRemis["NomRespon"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabRemis["Apel1ResRem"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabRemis["Apel2ResRem"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabRemis["CodDptoRes"].ToString() + "'," +
                                    "'" + TabRemis["CodMuniRes"].ToString() + "'," +
                                    "'" + TabRemis["DirecResRe"].ToString() + "'," +
                                    "'" + TabRemis["TelResRe"].ToString() + "'," +
                                    "'" + TabRemis["DxRemite"].ToString() + "'" +
                                    ")";

                                    Boolean Insert = Conexion.SqlInsert(Utils.SqlDatos);


                                }
                                else
                                {
                                    //Modifique los datos

                                    Utils.SqlDatos = "UPDATE [DACONEXTSQL].[dbo].[Datos de las remisiones] SET " +
                                    "HistoriaPaci ='" + TabRemis["HistoriaPaci"].ToString() + "'," +
                                    "RegimenRemis ='" + TabRemis["RegimenRemis"].ToString() + "'," +
                                    "CardinalEmp ='" + TabRemis["CardinalEmp"].ToString() + "'," +
                                    "ServiRemite ='" + TabRemis["ServiRemite"].ToString() + "'," +
                                    "Esperefere ='" + TabRemis["Esperefere"].ToString() + "'," +
                                    "Modalidad ='" + TabRemis["Modalidad"].ToString().Replace("'", "''") + "'," +
                                    "MotivoRemi ='" + TabRemis["MotivoRemi"].ToString().Replace("'", "''") + "'," +
                                    "NivelRemite ='" + TabRemis["NivelRemite"].ToString() + "'," +
                                    "NivelRefere ='" + TabRemis["NivelRefere"].ToString() + "'," +
                                    $"HoraSolicita = {Conexion.ValidarHoraNula(TabRemis["HoraSolicita"].ToString())}" +
                                    $"HoraConfirma = {Conexion.ValidarHoraNula(TabRemis["HoraConfirma"].ToString())}" +
                                    "Qconfirma ='" + TabRemis["Qconfirma"].ToString() + "',";

                                    if (string.IsNullOrWhiteSpace(TabRemis["Conductor"].ToString()) == false)
                                    {
                                        Utils.SqlDatos += "Conductor ='" + TabRemis["Conductor"].ToString() + "',";
                                    }
                                    if (string.IsNullOrWhiteSpace(TabRemis["PlacaAmbu"].ToString()) == false)
                                    {
                                        Utils.SqlDatos += "PlacaAmbu ='" + TabRemis["PlacaAmbu"].ToString() + "',";
                                    }

                                    Utils.SqlDatos += "ServiRecibe ='" + TabRemis["ServiRecibe"].ToString().Replace("'", "''") + "'," +
                                    "InstituRefer ='" + TabRemis["InstituRefer"].ToString() + "'," +
                                    "CodigoDpto ='" + TabRemis["CodigoDpto"].ToString() + "'," +
                                    "CodigoCiudad ='" + TabRemis["CodigoCiudad"].ToString() + "'," +
                                    "EspeciaAlrefere ='" + TabRemis["EspeciaAlrefere"].ToString() + "'," +
                                    "ResultadoExamen ='" + TabRemis["ResultadoExamen"].ToString().Replace("'", "''") + "'," +
                                    "JustificaREF ='" + TabRemis["JustificaREF"].ToString().Replace("'", "''") + "'," +
                                    "PrograPyP ='" + TabRemis["PrograPyP"].ToString() + "'," +
                                    "CodAnul ='" + TabRemis["CodAnul"].ToString() + "'," +
                                    $"FechAnul = {Conexion.ValidarFechaNula(TabRemis["FechAnul"].ToString())}" +
                                    "Anulado ='" + TabRemis["Anulado"].ToString() + "'," +
                                    "RazonesAnula ='" + TabRemis["RazonesAnula"].ToString() + "'," +
                                    "EfectoTerapeutico ='" + TabRemis["EfectoTerapeutico"].ToString() + "'," +
                                    "AlternativaPOS ='" + TabRemis["AlternativaPOS"].ToString() + "'," +
                                    "CopPOSAlternativo ='" + TabRemis["CopPOSAlternativo"].ToString() + "'," +
                                    "PorquenoSerealiza ='" + TabRemis["PorquenoSerealiza"].ToString() + "'," +
                                    "Prioridad ='" + TabRemis["Prioridad"].ToString() + "'," +
                                    "TipoRem ='" + TabRemis["TipoRem"].ToString() + "'," +
                                    "Control ='" + TabRemis["Control"].ToString() + "'," +
                                    "MedicoRemite ='" + TabRemis["MedicoRemite"].ToString() + "'," +
                                    //'***** CAMPOS AGREGADOS CON BASE DATOS DE SAN AGUSTIN *****
                                    "Frecuencia ='" + TabRemis["Frecuencia"].ToString() + "'," +
                                    "ConResponRe ='" + TabRemis["ConResponRe"].ToString() + "'," +
                                    "TipDocRespo ='" + TabRemis["TipDocRespo"].ToString() + "'," +
                                    "NumDocRespo = '" + TabRemis["NumDocRespo"].ToString().Replace("'", "''") + "'," +
                                    "NomRespon = '" + TabRemis["NomRespon"].ToString().Replace("'", "''") + "'," +
                                    "Apel1ResRem = '" + TabRemis["Apel1ResRem"].ToString().Replace("'", "''") + "'," +
                                    "Apel2ResRem = '" + TabRemis["Apel2ResRem"].ToString().Replace("'", "''") + "'," +
                                    "CodDptoRes = '" + TabRemis["CodDptoRes"].ToString() + "'," +
                                    "CodMuniRes = '" + TabRemis["CodMuniRes"].ToString() + "'," +
                                    "DirecResRe = '" + TabRemis["DirecResRe"].ToString() + "'," +
                                    "TelResRe = '" + TabRemis["TelResRe"].ToString() + "'," +
                                    "DxRemite = '" + TabRemis["DxRemite"].ToString() + "' " +
                                    "WHERE NumeroAten = N'" + CodAteRem + "' AND RemisionNum = N'" + CodRem + "'";

                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);


                                } //if (TabSegControlCen.HasRows == false)




                                TabRemisCen.Close();

                            }//Using
                        }//While

                        TabRemis.Close();
                        return 1;

                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion RemisionesIMP  " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private int SeguimientodecontrolesIMP(string NumAtenSC)
        {
            try
            {
                string CodSegControl, NumHisRegis, TipConReg, SqlSegControl, SqlSegControlCen;
                int FunDetTratam = 0, FunRegControles = 0, FunDetVacuApl = 0;

                SqlDataReader TabSegControl, TabSegControlCen;

                SqlSegControl = "SELECT [Datos seguimiento de controles].* ";
                SqlSegControl += "FROM [DACONEXTSQL].[dbo].[Datos seguimiento de controles] ";
                SqlSegControl += "WHERE ([Datos seguimiento de controles].CodConExt = N'" + NumAtenSC + "')";


                ConectarCentral();

                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command2 = new SqlCommand(SqlSegControl, connection2);
                    command2.Connection.Open();
                    TabSegControl = command2.ExecuteReader();


                    if (TabSegControl.HasRows == false)
                    {
                        //'No hay registros en la tabla Datos registro  la tabla Datos registro de procedimientos
                        return 0;
                    }
                    else
                    {
                        ConectarPortatil();
                        while (TabSegControl.Read())
                        {
                            //Revisamos si el número de codigo de atencion existe
                            CodSegControl = TabSegControl["CodControl"].ToString();
                            NumHisRegis = TabSegControl["HistoriaNum"].ToString();
                            TipConReg = TabSegControl["TipoControl"].ToString();

                            SqlSegControlCen = "SELECT * FROM [DACONEXTSQL].[dbo].[Datos seguimiento de controles] ";
                            SqlSegControlCen += "WHERE CodControl = N'" + CodSegControl + "' AND  HistoriaNum = N'" + NumHisRegis + "' and ";
                            SqlSegControlCen += "TipoControl = N'" + TipConReg + "' ";


                            using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command = new SqlCommand(SqlSegControlCen, connection);
                                command.Connection.Open();
                                TabSegControlCen = command.ExecuteReader();

                                if (TabSegControlCen.HasRows == false)
                                {
                                    Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos seguimiento de controles]  " +
                                    "(" +
                                    "CodControl," +
                                    "HistoriaNum," +
                                    "TipoControl," +
                                    "CodConExt," +
                                    "FechaUltimaGesta," +
                                    "UltimoPrevio," +
                                    "EmbaProg," +
                                    "FracMetodPF," +
                                    "PesoAnterior," +
                                    "Talla," +
                                    "PorECO," +
                                    "EGConfiable," +
                                    "FUM," +
                                    "FPP," +
                                    "EdadGIni," +
                                    "Trimestre," +
                                    "ExaMamas," +
                                    "ExaOdonto," +
                                    "ExaPAP," +
                                    "ExaInsPV," +
                                    "ExaColpos," +
                                    "GrSangui," +
                                    "FactorRH," +
                                    "Inmunizado," +
                                    "TOXOIGgMN20," +
                                    "TOXOIGgMY20," +
                                    "TOXOIGM," +
                                    "VDRLMN20," +
                                    "VDRLMY20," +
                                    "FTA," +
                                    "TTOSifilis," +
                                    "HBMN20," +
                                    "HBMY20," +
                                    "BacteriuriaMN20," +
                                    "BacteriuriaMY20," +
                                    "VIHMN20," +
                                    "VIHMY20," +
                                    "FE," +
                                    "Folatos," +
                                    "Chagas," +
                                    "Paludismo," +
                                    "Estreptococo," +
                                    "Observaciones," +
                                    "ActivoControl," +
                                    "RazonCierre," +
                                    "FechaCierre," +
                                    "QuienCierra," +
                                    "RiesgoO," +
                                    "TipoDeman," +
                                    "GLMN20," +
                                    "GLMY20," +
                                    "PreparaParto," +
                                    "LactaMenor," +
                                    "LactaMaterna," +
                                    "FecConLacMa," +
                                    "AntiRubeola," +
                                    "AntiTetanos," +
                                    "CMotriGruesa," +
                                    "CodCalificaMG," +
                                    "ColorMG," +
                                    "CMatrfinoAdap," +
                                    "CodCalificaMFA," +
                                    "ColorMFA," +
                                    "CAudicionLengua," +
                                    "CodCalificaAL," +
                                    "ColorAL," +
                                    "CPersonalSocial," +
                                    "CodCalificaPS," +
                                    "ColorPS," +
                                    "TotalEAD," +
                                    "CodTotalEAD," +
                                    "ColorTEAD," +
                                    "CodRangoS," +
                                    "ProyectoVida," +
                                    "ImaCorpo," +
                                    "Frustacion," +
                                    "ConQvive," +
                                    "QueETS," +
                                    "CondonPorque," +
                                    "ContagiaPorque," +
                                    "QueMetodos," +
                                    "CualesUtili," +
                                    "TBCP," +
                                    "TBCF," +
                                    "BDTF," +
                                    "BDTP," +
                                    "HTAP," +
                                    "HTAF," +
                                    "PRECLAMP," +
                                    "PRECLAMF," +
                                    "ECLAMP," +
                                    "ECLAMF," +
                                    "OTRAP," +
                                    "OTRAF," +
                                    "QXURINA," +
                                    "INFERTILIDAD," +
                                    "CARDIOP," +
                                    "NEFROPATIA," +
                                    "VIOLENCIA," +
                                    "GestasPrevias," +
                                    "PartosV," +
                                    "Cesareas," +
                                    "Abortos," +
                                    "EspConse," +
                                    "Vivos," +
                                    "Viven," +
                                    "Muertos," +
                                    "Muertos1Sem," +
                                    "Muertos1SemDes," +
                                    "Menos1Año," +
                                    "Gemelares," +
                                    "Alfabeta," +
                                    "Estudios," +
                                    "EstadoCivil," +
                                    "ViveSola," +
                                    "EmbEctopi," +
                                    "EmbaMol," +
                                    "TestSuli," +
                                    "TuvoConLacMa," +
                                    "FecProxiControl," +
                                    "FechaReg," +
                                    "CodReg," +
                                    "FechaMod," +
                                    "CodMod" +
                                    ")" +
                                    "VALUES (" +
                                    "'" + TabSegControl["CodControl"].ToString() + "'," +
                                    "'" + TabSegControl["HistoriaNum"].ToString() + "'," +
                                    "'" + TabSegControl["TipoControl"].ToString() + "'," +
                                    "'" + TabSegControl["CodConExt"].ToString() + "'," +
                                   $"{Conexion.ValidarFechaNula(TabSegControl["FechaUltimaGesta"].ToString())}" +
                                    "'" + TabSegControl["UltimoPrevio"].ToString() + "'," +
                                    "'" + TabSegControl["EmbaProg"].ToString() + "'," +
                                    "'" + TabSegControl["FracMetodPF"].ToString() + "'," +
                                    "'" + TabSegControl["PesoAnterior"].ToString() + "'," +
                                    "'" + TabSegControl["Talla"].ToString() + "'," +
                                    "'" + TabSegControl["PorECO"].ToString() + "'," +
                                    "'" + TabSegControl["EGConfiable"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabSegControl["FUM"].ToString())}" +
                                    $"{Conexion.ValidarFechaNula(TabSegControl["FPP"].ToString())}" +
                                    "'" + TabSegControl["EdadGIni"].ToString() + "'," +
                                    "'" + TabSegControl["Trimestre"].ToString() + "'," +
                                    "'" + TabSegControl["ExaMamas"].ToString() + "'," +
                                    "'" + TabSegControl["ExaOdonto"].ToString() + "'," +
                                    "'" + TabSegControl["ExaPAP"].ToString() + "'," +
                                    "'" + TabSegControl["ExaInsPV"].ToString() + "'," +
                                    "'" + TabSegControl["ExaColpos"].ToString() + "'," +
                                    "'" + TabSegControl["GrSangui"].ToString() + "'," +
                                    "'" + TabSegControl["FactorRH"].ToString() + "'," +
                                    "'" + TabSegControl["Inmunizado"].ToString() + "'," +
                                    "'" + TabSegControl["TOXOIGgMN20"].ToString() + "'," +
                                    "'" + TabSegControl["TOXOIGgMY20"].ToString() + "'," +
                                    "'" + TabSegControl["TOXOIGM"].ToString() + "'," +
                                    "'" + TabSegControl["VDRLMN20"].ToString() + "'," +
                                    "'" + TabSegControl["VDRLMY20"].ToString() + "'," +
                                    "'" + TabSegControl["FTA"].ToString() + "'," +
                                    "'" + TabSegControl["TTOSifilis"].ToString() + "'," +
                                    "'" + TabSegControl["HBMN20"].ToString() + "'," +
                                    "'" + TabSegControl["HBMY20"].ToString() + "'," +
                                    "'" + TabSegControl["BacteriuriaMN20"].ToString() + "'," +
                                    "'" + TabSegControl["BacteriuriaMY20"].ToString() + "'," +
                                    "'" + TabSegControl["VIHMN20"].ToString() + "'," +
                                    "'" + TabSegControl["VIHMY20"].ToString() + "'," +
                                    "'" + TabSegControl["FE"].ToString() + "'," +
                                    "'" + TabSegControl["Folatos"].ToString() + "'," +
                                    "'" + TabSegControl["Chagas"].ToString() + "'," +
                                    "'" + TabSegControl["Paludismo"].ToString() + "'," +
                                    "'" + TabSegControl["Estreptococo"].ToString() + "'," +
                                    "'" + TabSegControl["Observaciones"].ToString() + "'," +
                                    "'" + TabSegControl["ActivoControl"].ToString() + "'," +
                                    "'" + TabSegControl["RazonCierre"].ToString() + "'," +
                                   $"{Conexion.ValidarFechaNula(TabSegControl["FechaCierre"].ToString())}" +
                                    "'" + TabSegControl["QuienCierra"].ToString() + "'," +
                                    "'" + TabSegControl["RiesgoO"].ToString() + "'," +
                                    "'" + TabSegControl["TipoDeman"].ToString() + "'," +
                                    "'" + TabSegControl["GLMN20"].ToString() + "'," +
                                    "'" + TabSegControl["GLMY20"].ToString() + "'," +
                                    "'" + TabSegControl["PreparaParto"].ToString() + "'," +
                                    "'" + TabSegControl["LactaMenor"].ToString() + "'," +
                                    "'" + TabSegControl["LactaMaterna"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabSegControl["FecConLacMa"].ToString())}" +
                                    "'" + TabSegControl["AntiRubeola"].ToString() + "'," +
                                    "'" + TabSegControl["AntiTetanos"].ToString() + "'," +
                                    "'" + TabSegControl["CMotriGruesa"].ToString() + "'," +
                                    "'" + TabSegControl["CodCalificaMG"].ToString() + "'," +
                                    "'" + TabSegControl["ColorMG"].ToString() + "'," +
                                    "'" + TabSegControl["CMatrfinoAdap"].ToString() + "'," +
                                    "'" + TabSegControl["CodCalificaMFA"].ToString() + "'," +
                                    "'" + TabSegControl["ColorMFA"].ToString() + "'," +
                                    "'" + TabSegControl["CAudicionLengua"].ToString() + "'," +
                                    "'" + TabSegControl["CodCalificaAL"].ToString() + "'," +
                                    "'" + TabSegControl["ColorAL"].ToString() + "'," +
                                    "'" + TabSegControl["CPersonalSocial"].ToString() + "'," +
                                    "'" + TabSegControl["CodCalificaPS"].ToString() + "'," +
                                    "'" + TabSegControl["ColorPS"].ToString() + "'," +
                                    "'" + TabSegControl["TotalEAD"].ToString() + "'," +
                                    "'" + TabSegControl["CodTotalEAD"].ToString() + "'," +
                                    "'" + TabSegControl["ColorTEAD"].ToString() + "'," +
                                    "'" + TabSegControl["CodRangoS"].ToString() + "'," +
                                    "'" + TabSegControl["ProyectoVida"].ToString() + "'," +
                                    "'" + TabSegControl["ImaCorpo"].ToString() + "'," +
                                    "'" + TabSegControl["Frustacion"].ToString() + "'," +
                                    "'" + TabSegControl["ConQvive"].ToString() + "'," +
                                    "'" + TabSegControl["QueETS"].ToString() + "'," +
                                    "'" + TabSegControl["CondonPorque"].ToString() + "'," +
                                    "'" + TabSegControl["ContagiaPorque"].ToString() + "'," +
                                    "'" + TabSegControl["QueMetodos"].ToString() + "'," +
                                    "'" + TabSegControl["CualesUtili"].ToString() + "'," +
                                    "'" + TabSegControl["TBCP"].ToString() + "'," +
                                    "'" + TabSegControl["TBCF"].ToString() + "'," +
                                    "'" + TabSegControl["BDTF"].ToString() + "'," +
                                    "'" + TabSegControl["BDTP"].ToString() + "'," +
                                    "'" + TabSegControl["HTAP"].ToString() + "'," +
                                    "'" + TabSegControl["HTAF"].ToString() + "'," +
                                    "'" + TabSegControl["PRECLAMP"].ToString() + "'," +
                                    "'" + TabSegControl["PRECLAMF"].ToString() + "'," +
                                    "'" + TabSegControl["ECLAMP"].ToString() + "'," +
                                    "'" + TabSegControl["ECLAMF"].ToString() + "'," +
                                    "'" + TabSegControl["OTRAP"].ToString() + "'," +
                                    "'" + TabSegControl["OTRAF"].ToString() + "'," +
                                    "'" + TabSegControl["QXURINA"].ToString() + "'," +
                                    "'" + TabSegControl["INFERTILIDAD"].ToString() + "'," +
                                    "'" + TabSegControl["CARDIOP"].ToString() + "'," +
                                    "'" + TabSegControl["NEFROPATIA"].ToString() + "'," +
                                    "'" + TabSegControl["VIOLENCIA"].ToString() + "'," +
                                    "'" + TabSegControl["GestasPrevias"].ToString() + "'," +
                                    "'" + TabSegControl["PartosV"].ToString() + "'," +
                                    "'" + TabSegControl["Cesareas"].ToString() + "'," +
                                    "'" + TabSegControl["Abortos"].ToString() + "'," +
                                    "'" + TabSegControl["EspConse"].ToString() + "'," +
                                    "'" + TabSegControl["Vivos"].ToString() + "'," +
                                    "'" + TabSegControl["Viven"].ToString() + "'," +
                                    "'" + TabSegControl["Muertos"].ToString() + "'," +
                                    "'" + TabSegControl["Muertos1Sem"].ToString() + "'," +
                                    "'" + TabSegControl["Muertos1SemDes"].ToString() + "'," +
                                    "'" + TabSegControl["Menos1Año"].ToString() + "'," +
                                    "'" + TabSegControl["Gemelares"].ToString() + "'," +
                                    "'" + TabSegControl["Alfabeta"].ToString() + "'," +
                                    "'" + TabSegControl["Estudios"].ToString() + "'," +
                                    "'" + TabSegControl["EstadoCivil"].ToString() + "'," +
                                    "'" + TabSegControl["ViveSola"].ToString() + "'," +
                                    "'" + TabSegControl["EmbEctopi"].ToString() + "'," +
                                    "'" + TabSegControl["EmbaMol"].ToString() + "'," +
                                    "'" + TabSegControl["TestSuli"].ToString() + "'," +
                                    "'" + TabSegControl["TuvoConLacMa"].ToString() + "'," +
                                   $"{Conexion.ValidarFechaNula(TabSegControl["FecProxiControl"].ToString())}" +
                                   $"{Conexion.ValidarFechaNula(TabSegControl["FechaReg"].ToString())}" +
                                    "'" + TabSegControl["CodReg"].ToString() + "'," +
                                   $"{Conexion.ValidarFechaNula(TabSegControl["FechaMod"].ToString())}" +
                                    "'" + TabSegControl["CodMod"].ToString() + "'" +
                                    ")";

                                    Boolean Insert = Conexion.SqlInsert(Utils.SqlDatos);

                                    FunRegControles = RegistrodecontrolesEXP(CodSegControl, NumAtenSC);
                                    FunDetVacuApl = DetallevacunasaplicadasEXP(CodSegControl, NumHisRegis);


                                }
                                else
                                {
                                    //Modifique los datos

                                    Utils.SqlDatos = "UPDATE [DACONEXTSQL].[dbo].[Datos seguimiento de controles]  SET " +
                                    "CodConExt = '" + TabSegControl["CodConExt"].ToString() + "'," +
                                    $"FechaUltimaGesta = {Conexion.ValidarFechaNula(TabSegControl["FechaUltimaGesta"].ToString())}" +
                                    "UltimoPrevio = '" + TabSegControl["UltimoPrevio"].ToString() + "'," +
                                    "EmbaProg = '" + TabSegControl["EmbaProg"].ToString() + "'," +
                                    "FracMetodPF = '" + TabSegControl["FracMetodPF"].ToString() + "'," +
                                    "PesoAnterior = '" + TabSegControl["PesoAnterior"].ToString() + "'," +
                                    "Talla = '" + TabSegControl["Talla"].ToString() + "'," +
                                    "PorECO = '" + TabSegControl["PorECO"].ToString() + "'," +
                                    "EGConfiable = '" + TabSegControl["EGConfiable"].ToString() + "'," +
                                    $"FUM = {Conexion.ValidarFechaNula(TabSegControl["FUM"].ToString())}" +
                                    $"FPP = {Conexion.ValidarFechaNula(TabSegControl["FPP"].ToString())}" +
                                    "EdadGIni = '" + TabSegControl["EdadGIni"].ToString() + "'," +
                                    "Trimestre = '" + TabSegControl["Trimestre"].ToString() + "'," +
                                    "ExaMamas = '" + TabSegControl["ExaMamas"].ToString() + "'," +
                                    "ExaOdonto = '" + TabSegControl["ExaOdonto"].ToString() + "'," +
                                    "ExaPAP = '" + TabSegControl["ExaPAP"].ToString() + "'," +
                                    "ExaInsPV = '" + TabSegControl["ExaInsPV"].ToString() + "'," +
                                    "ExaColpos = '" + TabSegControl["ExaColpos"].ToString() + "'," +
                                    "GrSangui = '" + TabSegControl["GrSangui"].ToString() + "'," +
                                    "FactorRH = '" + TabSegControl["FactorRH"].ToString() + "'," +
                                    "Inmunizado = '" + TabSegControl["Inmunizado"].ToString() + "'," +
                                    "TOXOIGgMN20 = '" + TabSegControl["TOXOIGgMN20"].ToString() + "'," +
                                    "TOXOIGgMY20 = '" + TabSegControl["TOXOIGgMY20"].ToString() + "'," +
                                    "TOXOIGM = '" + TabSegControl["TOXOIGM"].ToString() + "'," +
                                    "VDRLMN20 = '" + TabSegControl["VDRLMN20"].ToString() + "'," +
                                    "VDRLMY20 = '" + TabSegControl["VDRLMY20"].ToString() + "'," +
                                    "FTA = '" + TabSegControl["FTA"].ToString() + "'," +
                                    "TTOSifilis = '" + TabSegControl["TTOSifilis"].ToString() + "'," +
                                    "HBMN20 = '" + TabSegControl["HBMN20"].ToString() + "'," +
                                    "HBMY20 = '" + TabSegControl["HBMY20"].ToString() + "'," +
                                    "BacteriuriaMN20 = '" + TabSegControl["BacteriuriaMN20"].ToString() + "'," +
                                    "BacteriuriaMY20 = '" + TabSegControl["BacteriuriaMY20"].ToString() + "'," +
                                    "VIHMN20 = '" + TabSegControl["VIHMN20"].ToString() + "'," +
                                    "VIHMY20 = '" + TabSegControl["VIHMY20"].ToString() + "'," +
                                    "FE = '" + TabSegControl["FE"].ToString() + "'," +
                                    "Folatos = '" + TabSegControl["Folatos"].ToString() + "'," +
                                    "Chagas = '" + TabSegControl["Chagas"].ToString() + "'," +
                                    "Paludismo = '" + TabSegControl["Paludismo"].ToString() + "'," +
                                    "Estreptococo = '" + TabSegControl["Estreptococo"].ToString() + "'," +
                                    "Observaciones = '" + TabSegControl["Observaciones"].ToString() + "'," +
                                    "ActivoControl = '" + TabSegControl["ActivoControl"].ToString() + "'," +
                                    "RazonCierre = '" + TabSegControl["RazonCierre"].ToString() + "'," +
                                    $"FechaCierre = {Conexion.ValidarFechaNula(TabSegControl["FechaCierre"].ToString())}" +
                                    "QuienCierra = '" + TabSegControl["QuienCierra"].ToString() + "'," +
                                    "RiesgoO = '" + TabSegControl["RiesgoO"].ToString() + "'," +
                                    "TipoDeman = '" + TabSegControl["TipoDeman"].ToString() + "'," +
                                    "GLMN20 = '" + TabSegControl["GLMN20"].ToString() + "'," +
                                    "GLMY20 = '" + TabSegControl["GLMY20"].ToString() + "'," +
                                    "PreparaParto = '" + TabSegControl["PreparaParto"].ToString() + "'," +
                                    "LactaMenor = '" + TabSegControl["LactaMenor"].ToString() + "'," +
                                    "LactaMaterna = '" + TabSegControl["LactaMaterna"].ToString() + "'," +
                                    $"FecConLacMa = {Conexion.ValidarFechaNula(TabSegControl["FecConLacMa"].ToString())}" +
                                    "AntiRubeola = '" + TabSegControl["AntiRubeola"].ToString() + "'," +
                                    "AntiTetanos = '" + TabSegControl["AntiTetanos"].ToString() + "'," +
                                    "CMotriGruesa = '" + TabSegControl["CMotriGruesa"].ToString() + "'," +
                                    "CodCalificaMG = '" + TabSegControl["CodCalificaMG"].ToString() + "'," +
                                    "ColorMG = '" + TabSegControl["ColorMG"].ToString() + "'," +
                                    "CMatrfinoAdap = '" + TabSegControl["CMatrfinoAdap"].ToString() + "'," +
                                    "CodCalificaMFA = '" + TabSegControl["CodCalificaMFA"].ToString() + "'," +
                                    "ColorMFA = '" + TabSegControl["ColorMFA"].ToString() + "'," +
                                    "CAudicionLengua = '" + TabSegControl["CAudicionLengua"].ToString() + "'," +
                                    "CodCalificaAL = '" + TabSegControl["CodCalificaAL"].ToString() + "'," +
                                    "ColorAL = '" + TabSegControl["ColorAL"].ToString() + "'," +
                                    "CPersonalSocial = '" + TabSegControl["CPersonalSocial"].ToString() + "'," +
                                    "CodCalificaPS = '" + TabSegControl["CodCalificaPS"].ToString() + "'," +
                                    "ColorPS = '" + TabSegControl["ColorPS"].ToString() + "'," +
                                    "TotalEAD = '" + TabSegControl["TotalEAD"].ToString() + "'," +
                                    "CodTotalEAD = '" + TabSegControl["CodTotalEAD"].ToString() + "'," +
                                    "ColorTEAD = '" + TabSegControl["ColorTEAD"].ToString() + "'," +
                                    "CodRangoS = '" + TabSegControl["CodRangoS"].ToString() + "'," +
                                    "ProyectoVida = '" + TabSegControl["ProyectoVida"].ToString() + "'," +
                                    "ImaCorpo = '" + TabSegControl["ImaCorpo"].ToString() + "'," +
                                    "Frustacion = '" + TabSegControl["Frustacion"].ToString() + "'," +
                                    "ConQvive = '" + TabSegControl["ConQvive"].ToString() + "'," +
                                    "QueETS = '" + TabSegControl["QueETS"].ToString() + "'," +
                                    "CondonPorque = '" + TabSegControl["CondonPorque"].ToString() + "'," +
                                    "ContagiaPorque = '" + TabSegControl["ContagiaPorque"].ToString() + "'," +
                                    "QueMetodos = '" + TabSegControl["QueMetodos"].ToString() + "'," +
                                    "CualesUtili = '" + TabSegControl["CualesUtili"].ToString() + "'," +
                                    "TBCP = '" + TabSegControl["TBCP"].ToString() + "'," +
                                    "TBCF = '" + TabSegControl["TBCF"].ToString() + "'," +
                                    "BDTF = '" + TabSegControl["BDTF"].ToString() + "'," +
                                    "BDTP = '" + TabSegControl["BDTP"].ToString() + "'," +
                                    "HTAP = '" + TabSegControl["HTAP"].ToString() + "'," +
                                    "HTAF = '" + TabSegControl["HTAF"].ToString() + "'," +
                                    "PRECLAMP = '" + TabSegControl["PRECLAMP"].ToString() + "'," +
                                    "PRECLAMF = '" + TabSegControl["PRECLAMF"].ToString() + "'," +
                                    "ECLAMP = '" + TabSegControl["ECLAMP"].ToString() + "'," +
                                    "ECLAMF = '" + TabSegControl["ECLAMF"].ToString() + "'," +
                                    "OTRAP = '" + TabSegControl["OTRAP"].ToString() + "'," +
                                    "OTRAF = '" + TabSegControl["OTRAF"].ToString() + "'," +
                                    "QXURINA = '" + TabSegControl["QXURINA"].ToString() + "'," +
                                    "INFERTILIDAD = '" + TabSegControl["INFERTILIDAD"].ToString() + "'," +
                                    "CARDIOP = '" + TabSegControl["CARDIOP"].ToString() + "'," +
                                    "NEFROPATIA = '" + TabSegControl["NEFROPATIA"].ToString() + "'," +
                                    "VIOLENCIA = '" + TabSegControl["VIOLENCIA"].ToString() + "'," +
                                    "GestasPrevias = '" + TabSegControl["GestasPrevias"].ToString() + "'," +
                                    "PartosV = '" + TabSegControl["PartosV"].ToString() + "'," +
                                    "Cesareas = '" + TabSegControl["Cesareas"].ToString() + "'," +
                                    "Abortos = '" + TabSegControl["Abortos"].ToString() + "'," +
                                    "EspConse = '" + TabSegControl["EspConse"].ToString() + "'," +
                                    "Vivos = '" + TabSegControl["Vivos"].ToString() + "'," +
                                    "Viven = '" + TabSegControl["Viven"].ToString() + "'," +
                                    "Muertos = '" + TabSegControl["Muertos"].ToString() + "'," +
                                    "Muertos1Sem = '" + TabSegControl["Muertos1Sem"].ToString() + "'," +
                                    "Muertos1SemDes = '" + TabSegControl["Muertos1SemDes"].ToString() + "'," +
                                    "Menos1Año = '" + TabSegControl["Menos1Año"].ToString() + "'," +
                                    "Gemelares = '" + TabSegControl["Gemelares"].ToString() + "'," +
                                    "Alfabeta = '" + TabSegControl["Alfabeta"].ToString() + "'," +
                                    "Estudios = '" + TabSegControl["Estudios"].ToString() + "'," +
                                    "EstadoCivil = '" + TabSegControl["EstadoCivil"].ToString() + "'," +
                                    "ViveSola = '" + TabSegControl["ViveSola"].ToString() + "'," +
                                    "EmbEctopi = '" + TabSegControl["EmbEctopi"].ToString() + "'," +
                                    "EmbaMol = '" + TabSegControl["EmbaMol"].ToString() + "'," +
                                    "TestSuli = '" + TabSegControl["TestSuli"].ToString() + "'," +
                                    "TuvoConLacMa = '" + TabSegControl["TuvoConLacMa"].ToString() + "'," +
                                    $"FecProxiControl = {Conexion.ValidarFechaNula(TabSegControl["FecProxiControl"].ToString())}" +
                                    $"FechaReg = {Conexion.ValidarFechaNula(TabSegControl["FechaReg"].ToString())}" +
                                    "CodReg = '" + TabSegControl["CodReg"].ToString() + "'," +
                                    $"FechaMod = {Conexion.ValidarFechaNula(TabSegControl["FechaMod"].ToString())}" +
                                    "CodMod = '" + TabSegControl["CodMod"].ToString() + "' " +
                                    "WHERE CodControl = N'" + CodSegControl + "' AND  HistoriaNum = N'" + NumHisRegis + "' and TipoControl = N'" + TipConReg + "'  ";

                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);

                                    FunRegControles = RegistrodecontrolesEXP(CodSegControl, NumAtenSC);
                                    FunDetVacuApl = DetallevacunasaplicadasEXP(CodSegControl, NumHisRegis);


                                } //if (TabSegControlCen.HasRows == false)




                                TabSegControlCen.Close();

                            }//Using
                        }//While

                        TabSegControl.Close();
                        return 1;

                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion SeguimientodecontrolesIMP  " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private int DetallevacunasaplicadasEXP(string CodControlDeta, string HisBus)
        {
            try
            {
                string SqlDetVacuApl, SqlDetVacuAplCen, CodVac;
                SqlDataReader TabDetVacuApl, TabDetVacuAplCen;

                SqlDetVacuApl = "SELECT [Datos detalle vacunas aplicadas].* ";
                SqlDetVacuApl += "FROM [DACONEXTSQL].[dbo].[Datos detalle vacunas aplicadas] ";
                SqlDetVacuApl += "WHERE ([Datos detalle vacunas aplicadas].CodControl = N'" + CodControlDeta + "') AND ";
                SqlDetVacuApl += "([Datos detalle vacunas aplicadas].HistoriaVac = N'" + HisBus + "')";

                ConectarCentral();

                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command2 = new SqlCommand(SqlDetVacuApl, connection2);
                    command2.Connection.Open();
                    TabDetVacuApl = command2.ExecuteReader();


                    if (TabDetVacuApl.HasRows == false)
                    {
                        //'No hay registros en la tabla Datos registro  la tabla Datos registro de procedimientos
                        return 0;
                    }
                    else
                    {
                        ConectarPortatil();
                        while (TabDetVacuApl.Read())
                        {

                            CodVac = TabDetVacuApl["CodVacuna"].ToString();

                            //Revisamos si el número de codigo de atencion existe
                            SqlDetVacuAplCen = "SELECT * FROM [DACONEXTSQL].[dbo].[Datos detalle vacunas aplicadas] ";
                            SqlDetVacuAplCen += "WHERE CodControl = N'" + CodControlDeta + "' AND HistoriaVac = N'" + HisBus + "' AND ";
                            SqlDetVacuAplCen += "CodVacuna = '" + CodVac + "'";



                            using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command = new SqlCommand(SqlDetVacuAplCen, connection);
                                command.Connection.Open();
                                TabDetVacuAplCen = command.ExecuteReader();

                                if (TabDetVacuAplCen.HasRows == false)
                                {
                                    Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos detalle vacunas aplicadas] " +
                                    "(" +
                                    "CodControl," +
                                    "HistoriaVac," +
                                    "CodVacuna," +
                                    "AplicadDet," +
                                    "FechaAplica," +
                                    "FechaReg," +
                                    "MedReg" +
                                    ")" +
                                    "VALUES (" +
                                    "'" + TabDetVacuApl["CodControl"].ToString() + "'," +
                                    "'" + TabDetVacuApl["HistoriaVac"].ToString() + "'," +
                                    "'" + TabDetVacuApl["CodVacuna"].ToString() + "'," +
                                    "'" + TabDetVacuApl["AplicadDet"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabDetVacuApl["FechaAplica"].ToString())}" +
                                    $"{Conexion.ValidarFechaNula(TabDetVacuApl["FechaReg"].ToString())}" +
                                    "'" + TabDetVacuApl["MedReg"].ToString() + "'" +
                                    ")";

                                    Boolean Insert = Conexion.SqlInsert(Utils.SqlDatos);

                                }
                                else
                                {
                                    Utils.SqlDatos = "UPDATE [DACONEXTSQL].[dbo].[Datos detalle vacunas aplicadas] SET " +
                                    "CodControl ='" + TabDetVacuApl["CodControl"].ToString() + "'," +
                                    "HistoriaVac ='" + TabDetVacuApl["HistoriaVac"].ToString() + "'," +
                                    "CodVacuna ='" + TabDetVacuApl["CodVacuna"].ToString() + "'," +
                                    "AplicadDet ='" + TabDetVacuApl["AplicadDet"].ToString() + "'," +
                                    $"FechaAplica = {Conexion.ValidarFechaNula(TabDetVacuApl["FechaAplica"].ToString())}" +
                                    $"FechaReg = {Conexion.ValidarFechaNula(TabDetVacuApl["FechaReg"].ToString())}" +
                                    "MedReg ='" + TabDetVacuApl["MedReg"].ToString() + "'  " +
                                    "WHERE CodControl = N'" + CodControlDeta + "' AND HistoriaVac = N'" + HisBus + "' AND CodVacuna = '" + CodVac + "'";

                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);

                                }
                            }
                            TabDetVacuAplCen.Close();
                        }
                        TabDetVacuApl.Close();
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion DetallevacunasaplicadasEXP  " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private int RegistrodecontrolesEXP(string CodControlRC, string NumAtenRC)
        {
            try
            {
                string SqlRegControles, SqlRegControlesCen;
                SqlDataReader TabRegControles, TabRegControlesCen;

                SqlRegControles = "SELECT [Datos registro de controles].* ";
                SqlRegControles += "FROM [DACONEXTSQL].[dbo].[Datos registro de controles] ";
                SqlRegControles += "WHERE ([Datos registro de controles].CodControl = N'" + CodControlRC + "') AND ";
                SqlRegControles += "([Datos registro de controles].NumAtencion = N'" + NumAtenRC + "')";

                ConectarCentral();

                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command2 = new SqlCommand(SqlRegControles, connection2);
                    command2.Connection.Open();
                    TabRegControles = command2.ExecuteReader();


                    if (TabRegControles.HasRows == false)
                    {
                        //'No hay registros en la tabla Datos registro  la tabla Datos registro de procedimientos
                        return 0;
                    }
                    else
                    {
                        ConectarPortatil();
                        while (TabRegControles.Read())
                        {
                            //Revisamos si el número de codigo de atencion existe
                            SqlRegControlesCen = "SELECT * FROM [DACONEXTSQL].[dbo].[Datos registro de controles] ";
                            SqlRegControlesCen += "WHERE CodControl = N'" + CodControlRC + "' AND NumAtencion = N'" + NumAtenRC + "'";



                            using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command = new SqlCommand(SqlRegControlesCen, connection);
                                command.Connection.Open();
                                TabRegControlesCen = command.ExecuteReader();

                                if (TabRegControlesCen.HasRows == false)
                                {
                                    Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos registro de controles]  " +
                                    "(" +
                                    "CodControl," +
                                    "NumAtencion," +
                                    "FechaConsulta," +
                                    "SemAmeno," +
                                    "Peso," +
                                    "Trimestre," +
                                    "Sistolica," +
                                    "Diastolica," +
                                    "AlturaUte," +
                                    "Presentacion," +
                                    "FCF," +
                                    "MovFetal," +
                                    "Proteinuria," +
                                    "Observaciones," +
                                    "TipoDeman," +
                                    "FechaCita," +
                                    "IndiceGrafica," +
                                    "TiopoEDGrafica" +
                                    ")" +
                                    "VALUES (" +
                                    "'" + TabRegControles["CodControl"].ToString() + "'," +
                                    "'" + TabRegControles["NumAtencion"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabRegControles["FechaConsulta"].ToString())}" +
                                    "'" + TabRegControles["SemAmeno"].ToString() + "'," +
                                    "'" + TabRegControles["Peso"].ToString().Replace(".", "") + "'," +
                                    "'" + TabRegControles["Trimestre"].ToString() + "'," +
                                    "'" + TabRegControles["Sistolica"].ToString() + "'," +
                                    "'" + TabRegControles["Diastolica"].ToString() + "'," +
                                    "'" + TabRegControles["AlturaUte"].ToString() + "'," +
                                    "'" + TabRegControles["Presentacion"].ToString() + "'," +
                                    "'" + TabRegControles["FCF"].ToString() + "'," +
                                    "'" + TabRegControles["MovFetal"].ToString() + "'," +
                                    "'" + TabRegControles["Proteinuria"].ToString() + "'," +
                                    "'" + TabRegControles["Observaciones"].ToString() + "'," +
                                    "'" + TabRegControles["TipoDeman"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabRegControles["FechaCita"].ToString())}" +
                                    "'" + TabRegControles["IndiceGrafica"].ToString() + "'," +
                                    "'" + TabRegControles["TiopoEDGrafica"].ToString() + "'" +
                                    ")";

                                    Boolean Insert = Conexion.SqlInsert(Utils.SqlDatos);

                                }
                                else
                                {
                                    Utils.SqlDatos = "UPDATE [DACONEXTSQL].[dbo].[Datos registro de controles] SET " +
                                    $"FechaConsulta = {Conexion.ValidarFechaNula(TabRegControles["FechaConsulta"].ToString())}" +
                                    "SemAmeno ='" + TabRegControles["SemAmeno"].ToString() + "'," +
                                    "Peso ='" + TabRegControles["Peso"].ToString().Replace(".", "") + "'," +
                                    "Trimestre ='" + TabRegControles["Trimestre"].ToString() + "'," +
                                    "Sistolica ='" + TabRegControles["Sistolica"].ToString() + "'," +
                                    "Diastolica ='" + TabRegControles["Diastolica"].ToString() + "'," +
                                    "AlturaUte ='" + TabRegControles["AlturaUte"].ToString() + "'," +
                                    "Presentacion ='" + TabRegControles["Presentacion"].ToString() + "'," +
                                    "FCF ='" + TabRegControles["FCF"].ToString() + "'," +
                                    "MovFetal ='" + TabRegControles["MovFetal"].ToString() + "'," +
                                    "Proteinuria ='" + TabRegControles["Proteinuria"].ToString() + "'," +
                                    "Observaciones ='" + TabRegControles["Observaciones"].ToString() + "'," +
                                    "TipoDeman ='" + TabRegControles["TipoDeman"].ToString() + "'," +
                                    $"FechaCita = {Conexion.ValidarFechaNula(TabRegControles["FechaCita"].ToString())}" +
                                    "IndiceGrafica ='" + TabRegControles["IndiceGrafica"].ToString() + "'," +
                                    "TiopoEDGrafica ='" + TabRegControles["TiopoEDGrafica"].ToString() + "' " +
                                    "WHERE CodControl = N'" + CodControlRC + "' AND NumAtencion = N'" + NumAtenRC + "'";

                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);

                                }
                            }
                            TabRegControlesCen.Close();
                        }
                        TabRegControles.Close();
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion RegistrodecontrolesEXP  " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private int TratamientosIMP(string CodHistT, string AtenReal)
        {
            try
            {
                string CodAtenLoc, CodTratamiento, CodItemEscREF, SqlTratamiento, SqlTratamientoCen;
                int FunDetTratam = 0;

                SqlDataReader TabTratamiento, TabTratamientoCen;

                SqlTratamiento = "SELECT [Datos de los tratamientos].NumTrataM1, [Datos de los tratamientos].HistorTrata, [Datos de los tratamientos].CodigoAten, [Datos de los tratamientos].CuentaCon, " +
                "[Datos de los tratamientos].FechaAten, [Datos de los tratamientos].HoraAten, [Datos de los tratamientos].Der21, [Datos de los tratamientos].Der22, " +
                "[Datos de los tratamientos].Der23, [Datos de los tratamientos].Der24, [Datos de los tratamientos].Der25, [Datos de los tratamientos].Der26, " +
                "[Datos de los tratamientos].Der27, [Datos de los tratamientos].Der28, [Datos de los tratamientos].Der61, [Datos de los tratamientos].Der62, " +
                "[Datos de los tratamientos].Der63, [Datos de los tratamientos].Der64, [Datos de los tratamientos].Der65, [Datos de los tratamientos].Izq11, " +
                "[Datos de los tratamientos].Izq12, [Datos de los tratamientos].Izq13, [Datos de los tratamientos].Izq14, [Datos de los tratamientos].Izq15, " +
                "[Datos de los tratamientos].Izq16, [Datos de los tratamientos].Izq17, [Datos de los tratamientos].Izq18, [Datos de los tratamientos].Izq51, " +
                "[Datos de los tratamientos].Izq52, [Datos de los tratamientos].Izq53, [Datos de los tratamientos].Izq54, [Datos de los tratamientos].Izq55, " +
                "[Datos de los tratamientos].Der31, [Datos de los tratamientos].Der32, [Datos de los tratamientos].Der33, [Datos de los tratamientos].Der34, " +
                "[Datos de los tratamientos].Der35, [Datos de los tratamientos].Der36, [Datos de los tratamientos].Der37, [Datos de los tratamientos].Der38, " +
                "[Datos de los tratamientos].Der71, [Datos de los tratamientos].Der72, [Datos de los tratamientos].Der73, [Datos de los tratamientos].Der74, " +
                "[Datos de los tratamientos].Der75, [Datos de los tratamientos].Izq41, [Datos de los tratamientos].Izq42, [Datos de los tratamientos].Izq43, " +
                "[Datos de los tratamientos].Izq44, [Datos de los tratamientos].Izq45, [Datos de los tratamientos].Izq46, [Datos de los tratamientos].Izq47, " +
                "[Datos de los tratamientos].Izq48, [Datos de los tratamientos].Izq81, [Datos de los tratamientos].Izq82, [Datos de los tratamientos].Izq83, " +
                "[Datos de los tratamientos].Izq84, [Datos de los tratamientos].Izq85, [Datos de los tratamientos].Oclusion1, [Datos de los tratamientos].Oclusion2, " +
                "[Datos de los tratamientos].ObservaODONTO, [Datos de los tratamientos].CodRegistra, [Datos de los tratamientos].FechaRegis, " +
                "[Datos de los tratamientos].CodModify, [Datos de los tratamientos].FechaModify, [Datos de los tratamientos].Inicial, [Datos de los tratamientos].ActivoT " +
                "FROM  [DACONEXTSQL].[dbo].[Datos de los tratamientos] INNER JOIN " +
                " [DACONEXTSQL].[dbo].[Datos atencion de la consulta] ON [Datos de los tratamientos].CodigoAten = [Datos atencion de la consulta].CodConExt " +
                "WHERE ([Datos de los tratamientos].HistorTrata = N'" + CodHistT + "')";

                ConectarCentral();

                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command2 = new SqlCommand(SqlTratamiento, connection2);
                    command2.Connection.Open();
                    TabTratamiento = command2.ExecuteReader();


                    if (TabTratamiento.HasRows == false)
                    {
                        //'No hay registros en la tabla Datos registro  la tabla Datos registro de procedimientos
                        return 0;
                    }
                    else
                    {
                        ConectarPortatil();
                        while (TabTratamiento.Read())
                        {
                            //Revisamos si el número de codigo de atencion existe
                            CodAtenLoc = TabTratamiento["CodigoAten"].ToString();
                            CodTratamiento = TabTratamiento["NumTrataM1"].ToString();


                            //



                            SqlTratamientoCen = "SELECT * FROM [DACONEXTSQL].[dbo].[Datos de los tratamientos] ";
                            SqlTratamientoCen += "WHERE NumTrataM1 = N'" + CodTratamiento + "' AND CodigoAten = N'" + CodAtenLoc + "' AND HistorTrata = N'" + CodHistT + "'";


                            using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command = new SqlCommand(SqlTratamientoCen, connection);
                                command.Connection.Open();
                                TabTratamientoCen = command.ExecuteReader();

                                if (TabTratamientoCen.HasRows == false)
                                {
                                    Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos de los tratamientos]  " +
                                    "(" +
                                    "NumTrataM1," +
                                    "HistorTrata," +
                                    "CodigoAten," +
                                    "CuentaCon," +
                                    "FechaAten," +
                                    "HoraAten," +
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
                                    "Oclusion1," +
                                    "Oclusion2," +
                                    "ObservaODONTO," +
                                    "CodRegistra," +
                                    "FechaRegis," +
                                    "CodModify," +
                                    "FechaModify," +
                                    "Inicial," +
                                    "ActivoT" +
                                    ")" +
                                    "VALUES (" +
                                     "'" + TabTratamiento["NumTrataM1"].ToString() + "'," +
                                     "'" + TabTratamiento["HistorTrata"].ToString() + "'," +
                                     "'" + TabTratamiento["CodigoAten"].ToString() + "'," +
                                     "'" + TabTratamiento["CuentaCon"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabTratamiento["FechaAten"].ToString())}" +
                                     $"{Conexion.ValidarHoraNula(TabTratamiento["HoraAten"].ToString())}" +
                                     "'" + TabTratamiento["Der21"].ToString() + "'," +
                                     "'" + TabTratamiento["Der22"].ToString() + "'," +
                                     "'" + TabTratamiento["Der23"].ToString() + "'," +
                                     "'" + TabTratamiento["Der24"].ToString() + "'," +
                                     "'" + TabTratamiento["Der25"].ToString() + "'," +
                                     "'" + TabTratamiento["Der26"].ToString() + "'," +
                                     "'" + TabTratamiento["Der27"].ToString() + "'," +
                                     "'" + TabTratamiento["Der28"].ToString() + "'," +
                                     "'" + TabTratamiento["Der61"].ToString() + "'," +
                                     "'" + TabTratamiento["Der62"].ToString() + "'," +
                                     "'" + TabTratamiento["Der63"].ToString() + "'," +
                                     "'" + TabTratamiento["Der64"].ToString() + "'," +
                                     "'" + TabTratamiento["Der65"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq11"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq12"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq13"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq14"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq15"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq16"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq17"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq18"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq51"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq52"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq53"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq54"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq55"].ToString() + "'," +
                                     "'" + TabTratamiento["Der31"].ToString() + "'," +
                                     "'" + TabTratamiento["Der32"].ToString() + "'," +
                                     "'" + TabTratamiento["Der33"].ToString() + "'," +
                                     "'" + TabTratamiento["Der34"].ToString() + "'," +
                                     "'" + TabTratamiento["Der35"].ToString() + "'," +
                                     "'" + TabTratamiento["Der36"].ToString() + "'," +
                                     "'" + TabTratamiento["Der37"].ToString() + "'," +
                                     "'" + TabTratamiento["Der38"].ToString() + "'," +
                                     "'" + TabTratamiento["Der71"].ToString() + "'," +
                                     "'" + TabTratamiento["Der72"].ToString() + "'," +
                                     "'" + TabTratamiento["Der73"].ToString() + "'," +
                                     "'" + TabTratamiento["Der74"].ToString() + "'," +
                                     "'" + TabTratamiento["Der75"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq41"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq42"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq43"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq44"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq45"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq46"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq47"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq48"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq81"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq82"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq83"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq84"].ToString() + "'," +
                                     "'" + TabTratamiento["Izq85"].ToString() + "'," +
                                     "'" + TabTratamiento["Oclusion1"].ToString() + "'," +
                                     "'" + TabTratamiento["Oclusion2"].ToString() + "'," +
                                     "'" + TabTratamiento["ObservaODONTO"].ToString() + "'," +
                                     "'" + TabTratamiento["CodRegistra"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabTratamiento["FechaRegis"].ToString())}" +
                                     "'" + TabTratamiento["CodModify"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabTratamiento["FechaModify"].ToString())}" +
                                     "'" + TabTratamiento["Inicial"].ToString() + "'," +
                                     "'" + TabTratamiento["ActivoT"].ToString() + "'" +
                                    ")";

                                    Boolean Insert = Conexion.SqlInsert(Utils.SqlDatos);

                                    if (Insert)
                                    {
                                        FunDetTratam = DetallesdetratamientosEXP(CodTratamiento, AtenReal);
                                    }
                                }
                                else
                                {
                                    Utils.SqlDatos = "UPDATE [DACONEXTSQL].[dbo].[Datos de los tratamientos] SET " +
                                   "NumTrataM1 ='" + TabTratamiento["NumTrataM1"].ToString() + "'," +
                                    "HistorTrata ='" + TabTratamiento["HistorTrata"].ToString() + "'," +
                                    "CuentaCon ='" + TabTratamiento["CuentaCon"].ToString() + "'," +
                                    $"FechaAten = {Conexion.ValidarFechaNula(TabTratamiento["FechaAten"].ToString())}" +
                                    $"HoraAten = {Conexion.ValidarHoraNula(TabTratamiento["HoraAten"].ToString())}" +
                                    "Der21 ='" + TabTratamiento["Der21"].ToString() + "'," +
                                    "Der22 ='" + TabTratamiento["Der22"].ToString() + "'," +
                                    "Der23 ='" + TabTratamiento["Der23"].ToString() + "'," +
                                    "Der24 ='" + TabTratamiento["Der24"].ToString() + "'," +
                                    "Der25 ='" + TabTratamiento["Der25"].ToString() + "'," +
                                    "Der26 ='" + TabTratamiento["Der26"].ToString() + "'," +
                                    "Der27 ='" + TabTratamiento["Der27"].ToString() + "'," +
                                    "Der28 ='" + TabTratamiento["Der28"].ToString() + "'," +
                                    "Der61 ='" + TabTratamiento["Der61"].ToString() + "'," +
                                    "Der62 ='" + TabTratamiento["Der62"].ToString() + "'," +
                                    "Der63 ='" + TabTratamiento["Der63"].ToString() + "'," +
                                    "Der64 ='" + TabTratamiento["Der64"].ToString() + "'," +
                                    "Der65 ='" + TabTratamiento["Der65"].ToString() + "'," +
                                    "Izq11 ='" + TabTratamiento["Izq11"].ToString() + "'," +
                                    "Izq12 ='" + TabTratamiento["Izq12"].ToString() + "'," +
                                    "Izq13 ='" + TabTratamiento["Izq13"].ToString() + "'," +
                                    "Izq14 ='" + TabTratamiento["Izq14"].ToString() + "'," +
                                    "Izq15 ='" + TabTratamiento["Izq15"].ToString() + "'," +
                                    "Izq16 ='" + TabTratamiento["Izq16"].ToString() + "'," +
                                    "Izq17 ='" + TabTratamiento["Izq17"].ToString() + "'," +
                                    "Izq18 ='" + TabTratamiento["Izq18"].ToString() + "'," +
                                    "Izq51 ='" + TabTratamiento["Izq51"].ToString() + "'," +
                                    "Izq52 ='" + TabTratamiento["Izq52"].ToString() + "'," +
                                    "Izq53 ='" + TabTratamiento["Izq53"].ToString() + "'," +
                                    "Izq54 ='" + TabTratamiento["Izq54"].ToString() + "'," +
                                    "Izq55 ='" + TabTratamiento["Izq55"].ToString() + "'," +
                                    "Der31 ='" + TabTratamiento["Der31"].ToString() + "'," +
                                    "Der32 ='" + TabTratamiento["Der32"].ToString() + "'," +
                                    "Der33 ='" + TabTratamiento["Der33"].ToString() + "'," +
                                    "Der34 ='" + TabTratamiento["Der34"].ToString() + "'," +
                                    "Der35 ='" + TabTratamiento["Der35"].ToString() + "'," +
                                    "Der36 ='" + TabTratamiento["Der36"].ToString() + "'," +
                                    "Der37 ='" + TabTratamiento["Der37"].ToString() + "'," +
                                    "Der38 ='" + TabTratamiento["Der38"].ToString() + "'," +
                                    "Der71 ='" + TabTratamiento["Der71"].ToString() + "'," +
                                    "Der72 ='" + TabTratamiento["Der72"].ToString() + "'," +
                                    "Der73 ='" + TabTratamiento["Der73"].ToString() + "'," +
                                    "Der74 ='" + TabTratamiento["Der74"].ToString() + "'," +
                                    "Der75 ='" + TabTratamiento["Der75"].ToString() + "'," +
                                    "Izq41 ='" + TabTratamiento["Izq41"].ToString() + "'," +
                                    "Izq42 ='" + TabTratamiento["Izq42"].ToString() + "'," +
                                    "Izq43 ='" + TabTratamiento["Izq43"].ToString() + "'," +
                                    "Izq44 ='" + TabTratamiento["Izq44"].ToString() + "'," +
                                    "Izq45 ='" + TabTratamiento["Izq45"].ToString() + "'," +
                                    "Izq46 ='" + TabTratamiento["Izq46"].ToString() + "'," +
                                    "Izq47 ='" + TabTratamiento["Izq47"].ToString() + "'," +
                                    "Izq48 ='" + TabTratamiento["Izq48"].ToString() + "'," +
                                    "Izq81 ='" + TabTratamiento["Izq81"].ToString() + "'," +
                                    "Izq82 ='" + TabTratamiento["Izq82"].ToString() + "'," +
                                    "Izq83 ='" + TabTratamiento["Izq83"].ToString() + "'," +
                                    "Izq84 ='" + TabTratamiento["Izq84"].ToString() + "'," +
                                    "Izq85 ='" + TabTratamiento["Izq85"].ToString() + "'," +
                                    "Oclusion1 ='" + TabTratamiento["Oclusion1"].ToString() + "'," +
                                    "Oclusion2 ='" + TabTratamiento["Oclusion2"].ToString() + "'," +
                                    "ObservaODONTO ='" + TabTratamiento["ObservaODONTO"].ToString() + "'," +
                                    "CodRegistra ='" + TabTratamiento["CodRegistra"].ToString() + "'," +
                                    $"FechaRegis = {Conexion.ValidarFechaNula(TabTratamiento["FechaRegis"].ToString())}" +
                                    "CodModify ='" + TabTratamiento["CodModify"].ToString() + "'," +
                                    $"FechaModify = {Conexion.ValidarFechaNula(TabTratamiento["FechaModify"].ToString())}" +
                                    "Inicial ='" + TabTratamiento["Inicial"].ToString() + "'," +
                                    "ActivoT ='" + TabTratamiento["ActivoT"].ToString() + "' " +
                                    "WHERE NumTrataM1 = N'" + CodTratamiento + "' AND CodigoAten = N'" + CodAtenLoc + "' AND HistorTrata = N'" + CodHistT + "'";

                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);

                                    if (Act)
                                    {
                                        FunDetTratam = DetallesdetratamientosEXP(CodTratamiento, AtenReal);
                                    }


                                }//if (TabRegMedicCen.HasRows == false)

                                TabTratamientoCen.Close();

                            }//Using
                        }//While

                        TabTratamiento.Close();
                        return 1;

                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion TratamientosIMP  " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private int DetallesdetratamientosEXP(string CodTra, string AteNcopy)
        {
            try
            {
                string SqlDetTratam, NumTraRegis, ConSeRegis, CodDetTratam, SqlDetTratamCen;

                SqlDataReader TabDetTratam, TabDetTratamCen;

                SqlDetTratam = "SELECT [Datos detalles de tratamientos].NumTrata, [Datos detalles de tratamientos].Consecutivo, [Datos detalles de tratamientos].NumeroAtencion1, " +
                "[Datos detalles de tratamientos].CuentaOdontologia, [Datos detalles de tratamientos].Der21, [Datos detalles de tratamientos].Der22, " +
                "[Datos detalles de tratamientos].Der23, [Datos detalles de tratamientos].Der24, [Datos detalles de tratamientos].Der25, [Datos detalles de tratamientos].Der26, " +
                "[Datos detalles de tratamientos].Der27, [Datos detalles de tratamientos].Der28, [Datos detalles de tratamientos].Der61, [Datos detalles de tratamientos].Der62, " +
                "[Datos detalles de tratamientos].Der63, [Datos detalles de tratamientos].Der64, [Datos detalles de tratamientos].Der65, [Datos detalles de tratamientos].Izq11, " +
                "[Datos detalles de tratamientos].Izq12, [Datos detalles de tratamientos].Izq13, [Datos detalles de tratamientos].Izq14, [Datos detalles de tratamientos].Izq15, " +
                "[Datos detalles de tratamientos].Izq16, [Datos detalles de tratamientos].Izq17, [Datos detalles de tratamientos].Izq18, [Datos detalles de tratamientos].Izq51, " +
                "[Datos detalles de tratamientos].Izq52, [Datos detalles de tratamientos].Izq53, [Datos detalles de tratamientos].Izq54, [Datos detalles de tratamientos].Izq55, " +
                "[Datos detalles de tratamientos].Der31, [Datos detalles de tratamientos].Der32, [Datos detalles de tratamientos].Der33, [Datos detalles de tratamientos].Der34, " +
                "[Datos detalles de tratamientos].Der35, [Datos detalles de tratamientos].Der36, [Datos detalles de tratamientos].Der37, [Datos detalles de tratamientos].Der38, " +
                "[Datos detalles de tratamientos].Der71, [Datos detalles de tratamientos].Der72, [Datos detalles de tratamientos].Der73, [Datos detalles de tratamientos].Der74, " +
                "[Datos detalles de tratamientos].Der75, [Datos detalles de tratamientos].Izq41, [Datos detalles de tratamientos].Izq42, [Datos detalles de tratamientos].Izq43, " +
                "[Datos detalles de tratamientos].Izq44, [Datos detalles de tratamientos].Izq45, [Datos detalles de tratamientos].Izq46, [Datos detalles de tratamientos].Izq47, " +
                "[Datos detalles de tratamientos].Izq48, [Datos detalles de tratamientos].Izq81, [Datos detalles de tratamientos].Izq82, [Datos detalles de tratamientos].Izq83, " +
                "[Datos detalles de tratamientos].Izq84, [Datos detalles de tratamientos].Izq85, [Datos detalles de tratamientos].ActivoDET, " +
                "[Datos detalles de tratamientos].CodRegistra, [Datos detalles de tratamientos].FechaRegis, [Datos detalles de tratamientos].CodModify, " +
                "[Datos detalles de tratamientos].FechaModify " +
                "FROM [DACONEXTSQL].[dbo].[Datos detalles de tratamientos] " +
                "WHERE ([Datos detalles de tratamientos].NumTrata = N'" + CodTra + "' and [Datos detalles de tratamientos].NumeroAtencion1 = N'" + AteNcopy + "')";


                ConectarCentral();

                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command2 = new SqlCommand(SqlDetTratam, connection2);
                    command2.Connection.Open();
                    TabDetTratam = command2.ExecuteReader();


                    if (TabDetTratam.HasRows == false)
                    {
                        //No hay registros en la tabla Datos detalles de tratamientos
                        return 0;
                    }
                    else
                    {
                        ConectarPortatil();
                        while (TabDetTratam.Read())
                        {
                            //Revisamos si el número de codigo de atencion existe
                            NumTraRegis = TabDetTratam["NumTrata"].ToString();
                            ConSeRegis = TabDetTratam["Consecutivo"].ToString();
                            CodDetTratam = TabDetTratam["NumeroAtencion1"].ToString();

                            SqlDetTratamCen = "SELECT * FROM [DACONEXTSQL].[dbo].[Datos detalles de tratamientos] ";
                            SqlDetTratamCen += "WHERE NumTrata = N'" + NumTraRegis + "' AND ";
                            SqlDetTratamCen += "Consecutivo = " + ConSeRegis + " AND ";
                            SqlDetTratamCen += "NumeroAtencion1 = N'" + CodDetTratam + "' ";

                            using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command = new SqlCommand(SqlDetTratamCen, connection);
                                command.Connection.Open();
                                TabDetTratamCen = command.ExecuteReader();

                                if (TabDetTratamCen.HasRows == false)
                                {
                                    //El numero de codigo atencion no existe, por lo tanto puede continuar a agregarla

                                    Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos detalles de tratamientos]  " +
                                    "(" +
                                    "NumTrata," +
                                    "Consecutivo," +
                                    "NumeroAtencion1," +
                                    "CuentaOdontologia," +
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
                                    "ActivoDET," +
                                    "CodRegistra," +
                                    "FechaRegis," +
                                    "FechaModify," +
                                    "CodModify" +
                                    ")" +
                                    "VALUES (" +
                                    "'" + TabDetTratam["NumTrata"].ToString() + "'," +
                                    "'" + TabDetTratam["Consecutivo"].ToString() + "'," +
                                    "'" + TabDetTratam["NumeroAtencion1"].ToString() + "'," +
                                    "'" + TabDetTratam["CuentaOdontologia"].ToString() + "'," +
                                    "'" + TabDetTratam["Der21"].ToString() + "'," +
                                    "'" + TabDetTratam["Der22"].ToString() + "'," +
                                    "'" + TabDetTratam["Der23"].ToString() + "'," +
                                    "'" + TabDetTratam["Der24"].ToString() + "'," +
                                    "'" + TabDetTratam["Der25"].ToString() + "'," +
                                    "'" + TabDetTratam["Der26"].ToString() + "'," +
                                    "'" + TabDetTratam["Der27"].ToString() + "'," +
                                    "'" + TabDetTratam["Der28"].ToString() + "'," +
                                    "'" + TabDetTratam["Der61"].ToString() + "'," +
                                    "'" + TabDetTratam["Der62"].ToString() + "'," +
                                    "'" + TabDetTratam["Der63"].ToString() + "'," +
                                    "'" + TabDetTratam["Der64"].ToString() + "'," +
                                    "'" + TabDetTratam["Der65"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq11"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq12"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq13"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq14"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq15"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq16"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq17"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq18"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq51"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq52"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq53"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq54"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq55"].ToString() + "'," +
                                    "'" + TabDetTratam["Der31"].ToString() + "'," +
                                    "'" + TabDetTratam["Der32"].ToString() + "'," +
                                    "'" + TabDetTratam["Der33"].ToString() + "'," +
                                    "'" + TabDetTratam["Der34"].ToString() + "'," +
                                    "'" + TabDetTratam["Der35"].ToString() + "'," +
                                    "'" + TabDetTratam["Der36"].ToString() + "'," +
                                    "'" + TabDetTratam["Der37"].ToString() + "'," +
                                    "'" + TabDetTratam["Der38"].ToString() + "'," +
                                    "'" + TabDetTratam["Der71"].ToString() + "'," +
                                    "'" + TabDetTratam["Der72"].ToString() + "'," +
                                    "'" + TabDetTratam["Der73"].ToString() + "'," +
                                    "'" + TabDetTratam["Der74"].ToString() + "'," +
                                    "'" + TabDetTratam["Der75"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq41"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq42"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq43"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq44"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq45"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq46"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq47"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq48"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq81"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq82"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq83"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq84"].ToString() + "'," +
                                    "'" + TabDetTratam["Izq85"].ToString() + "'," +
                                    "'" + TabDetTratam["ActivoDET"].ToString() + "'," +
                                    "'" + TabDetTratam["CodRegistra"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabDetTratam["FechaRegis"].ToString())}" +
                                    $"{Conexion.ValidarFechaNula(TabDetTratam["FechaModify"].ToString())}" +
                                    "'" + TabDetTratam["CodModify"].ToString() + "'" +
                                    ")";

                                    Boolean Insert = Conexion.SqlInsert(Utils.SqlDatos);

                                }
                                else
                                {
                                    Utils.SqlDatos = "UPDATE [DACONEXTSQL].[dbo].[Datos detalles de tratamientos] SET " +
                                    "NumTrata='" + TabDetTratam["NumTrata"].ToString() + "'," +
                                    "Consecutivo='" + TabDetTratam["Consecutivo"].ToString() + "'," +
                                    "CuentaOdontologia='" + TabDetTratam["CuentaOdontologia"].ToString() + "'," +
                                    "Der21='" + TabDetTratam["Der21"].ToString() + "'," +
                                    "Der22='" + TabDetTratam["Der22"].ToString() + "'," +
                                    "Der23='" + TabDetTratam["Der23"].ToString() + "'," +
                                    "Der24='" + TabDetTratam["Der24"].ToString() + "'," +
                                    "Der25='" + TabDetTratam["Der25"].ToString() + "'," +
                                    "Der26='" + TabDetTratam["Der26"].ToString() + "'," +
                                    "Der27='" + TabDetTratam["Der27"].ToString() + "'," +
                                    "Der28='" + TabDetTratam["Der28"].ToString() + "'," +
                                    "Der61='" + TabDetTratam["Der61"].ToString() + "'," +
                                    "Der62='" + TabDetTratam["Der62"].ToString() + "'," +
                                    "Der63='" + TabDetTratam["Der63"].ToString() + "'," +
                                    "Der64='" + TabDetTratam["Der64"].ToString() + "'," +
                                    "Der65='" + TabDetTratam["Der65"].ToString() + "'," +
                                    "Izq11='" + TabDetTratam["Izq11"].ToString() + "'," +
                                    "Izq12='" + TabDetTratam["Izq12"].ToString() + "'," +
                                    "Izq13='" + TabDetTratam["Izq13"].ToString() + "'," +
                                    "Izq14='" + TabDetTratam["Izq14"].ToString() + "'," +
                                    "Izq15='" + TabDetTratam["Izq15"].ToString() + "'," +
                                    "Izq16='" + TabDetTratam["Izq16"].ToString() + "'," +
                                    "Izq17='" + TabDetTratam["Izq17"].ToString() + "'," +
                                    "Izq18='" + TabDetTratam["Izq18"].ToString() + "'," +
                                    "Izq51='" + TabDetTratam["Izq51"].ToString() + "'," +
                                    "Izq52='" + TabDetTratam["Izq52"].ToString() + "'," +
                                    "Izq53='" + TabDetTratam["Izq53"].ToString() + "'," +
                                    "Izq54='" + TabDetTratam["Izq54"].ToString() + "'," +
                                    "Izq55='" + TabDetTratam["Izq55"].ToString() + "'," +
                                    "Der31='" + TabDetTratam["Der31"].ToString() + "'," +
                                    "Der32='" + TabDetTratam["Der32"].ToString() + "'," +
                                    "Der33='" + TabDetTratam["Der33"].ToString() + "'," +
                                    "Der34='" + TabDetTratam["Der34"].ToString() + "'," +
                                    "Der35='" + TabDetTratam["Der35"].ToString() + "'," +
                                    "Der36='" + TabDetTratam["Der36"].ToString() + "'," +
                                    "Der37='" + TabDetTratam["Der37"].ToString() + "'," +
                                    "Der38='" + TabDetTratam["Der38"].ToString() + "'," +
                                    "Der71='" + TabDetTratam["Der71"].ToString() + "'," +
                                    "Der72='" + TabDetTratam["Der72"].ToString() + "'," +
                                    "Der73='" + TabDetTratam["Der73"].ToString() + "'," +
                                    "Der74='" + TabDetTratam["Der74"].ToString() + "'," +
                                    "Der75='" + TabDetTratam["Der75"].ToString() + "'," +
                                    "Izq41='" + TabDetTratam["Izq41"].ToString() + "'," +
                                    "Izq42='" + TabDetTratam["Izq42"].ToString() + "'," +
                                    "Izq43='" + TabDetTratam["Izq43"].ToString() + "'," +
                                    "Izq44='" + TabDetTratam["Izq44"].ToString() + "'," +
                                    "Izq45='" + TabDetTratam["Izq45"].ToString() + "'," +
                                    "Izq46='" + TabDetTratam["Izq46"].ToString() + "'," +
                                    "Izq47='" + TabDetTratam["Izq47"].ToString() + "'," +
                                    "Izq48='" + TabDetTratam["Izq48"].ToString() + "'," +
                                    "Izq81='" + TabDetTratam["Izq81"].ToString() + "'," +
                                    "Izq82='" + TabDetTratam["Izq82"].ToString() + "'," +
                                    "Izq83='" + TabDetTratam["Izq83"].ToString() + "'," +
                                    "Izq84='" + TabDetTratam["Izq84"].ToString() + "'," +
                                    "Izq85='" + TabDetTratam["Izq85"].ToString() + "'," +
                                    "ActivoDET='" + TabDetTratam["ActivoDET"].ToString() + "'," +
                                    "CodRegistra='" + TabDetTratam["CodRegistra"].ToString() + "'," +
                                   $"FechaRegis = {Conexion.ValidarFechaNula(TabDetTratam["FechaRegis"].ToString())}" +
                                   $"FechaModify = {Conexion.ValidarFechaNula(TabDetTratam["FechaModify"].ToString())}" +
                                    "CodModify='" + TabDetTratam["CodModify"].ToString() + "' " +
                                    "WHERE NumTrata = N'" + NumTraRegis + "' AND Consecutivo = " + ConSeRegis + " AND NumeroAtencion1 = N'" + CodDetTratam + "' ";

                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);
                                }

                                TabDetTratamCen.Close();
                            }
                        }
                        TabDetTratam.Close();
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion DetallesdetratamientosEXP  " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private int RegistroHtaDiabeticosIMP(string CodHistRHD)
        {
            try
            {
                string CodRegHtaDiabe, CodDetEscAbre, CodItemEscREF, SqlRegHtaDiabe, SqlRegHtaDiabeCen;

                SqlDataReader TabRegHtaDiabe, TabRegHtaDiabeCen;

                SqlRegHtaDiabe = "SELECT [Datos registro HtaDiabeticos].* ";
                SqlRegHtaDiabe += "FROM [DACONEXTSQL].[dbo].[Datos registro HtaDiabeticos] ";
                SqlRegHtaDiabe += "WHERE ([Datos registro HtaDiabeticos].CodAten = N'" + CodHistRHD + "')";



                ConectarCentral();

                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command2 = new SqlCommand(SqlRegHtaDiabe, connection2);
                    command2.Connection.Open();
                    TabRegHtaDiabe = command2.ExecuteReader();


                    if (TabRegHtaDiabe.HasRows == false)
                    {
                        //'No hay registros en la tabla Datos registro  la tabla Datos registro de procedimientos
                        return 0;
                    }
                    else
                    {
                        ConectarPortatil();
                        while (TabRegHtaDiabe.Read())
                        {
                            //Revisamos si el número de codigo de atencion existe
                            CodRegHtaDiabe = TabRegHtaDiabe["CodAten"].ToString();

                            SqlRegHtaDiabeCen = "SELECT * FROM [DACONEXTSQL].[dbo].[Datos registro HtaDiabeticos] ";
                            SqlRegHtaDiabeCen = SqlRegHtaDiabeCen + "Where CodAten = N'" + CodRegHtaDiabe + "'";



                            using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command = new SqlCommand(SqlRegHtaDiabeCen, connection);
                                command.Connection.Open();
                                TabRegHtaDiabeCen = command.ExecuteReader();

                                if (TabRegHtaDiabeCen.HasRows == false)
                                {
                                    Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos registro HtaDiabeticos]  " +
                                    "(" +
                                    "CodAten," +
                                    "CodEnfer," +
                                    "ClasiHTA," +
                                    "ReingNS," +
                                    "FumaNS," +
                                    "LesBlaNS," +
                                    "ControNS," +
                                    "ValGlice," +
                                    "ColeTotal," +
                                    "ColeHDL," +
                                    "Creatinina," +
                                    "ColeLDL," +
                                    "Cintura," +
                                    "SuperCorp," +
                                    "TasFilGlo," +
                                    "EstERC," +
                                    "AnormUroan," +
                                    "FecExamenes," +
                                    "CodRegis," +
                                    "FecRegis," +
                                    "CodModi," +
                                    "FecModi," +
                                    "CondClinAsoc," +
                                    "RiesCardio," +
                                    "TomaEkg," +
                                    "EKG," +
                                    "FecTomEKG," +
                                    "HabiNuti," +
                                    "ObsNutri," +
                                    "ActFisica," +
                                    "REmiMedInt," +
                                    "RemiNutr," +
                                    "RemiPsic," +
                                    "RemiOft," +
                                    "Trigli," +
                                    "HemoGlico," +
                                    "MicroAlbumi," +
                                    "OlvTomaMedHTA," +
                                    "DesHoraMed," +
                                    "BnNoTomaMed," +
                                    "CaeMalMediDejaToma," +
                                    "AdheTrata," +
                                    "Observacion," +
                                    "TomaGlice," +
                                    "FecGli," +
                                    "TomaColeste," +
                                    "FecCol," +
                                    "TomaCreati," +
                                    "TomaTrigli," +
                                    "FecTri," +
                                    "FecMInterna," +
                                    "FecOftal," +
                                    "FecPsic," +
                                    "FecNutri," +
                                    "TomaGlicosi," +
                                    "FecHGli," +
                                    "TomaMicro," +
                                    "FecMicro," +
                                    "TomaUroAnal," +
                                    "FecUroAnal," +
                                    "UroAnal" +
                                    ")" +
                                    "VALUES (" +
                                    "'" + TabRegHtaDiabe["CodAten"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["CodEnfer"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["ClasiHTA"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["ReingNS"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["FumaNS"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["LesBlaNS"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["ControNS"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["ValGlice"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["ColeTotal"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["ColeHDL"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["Creatinina"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["ColeLDL"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["Cintura"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["SuperCorp"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["TasFilGlo"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["EstERC"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["AnormUroan"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabRegHtaDiabe["FecExamenes"].ToString())}" +
                                    "'" + TabRegHtaDiabe["CodRegis"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabRegHtaDiabe["FecRegis"].ToString())}" +
                                    "'" + TabRegHtaDiabe["CodModi"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabRegHtaDiabe["FecModi"].ToString())}" +
                                    "'" + TabRegHtaDiabe["CondClinAsoc"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["RiesCardio"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["TomaEkg"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["EKG"].ToString().Replace("'","''") + "'," +
                                    $"{Conexion.ValidarFechaNula(TabRegHtaDiabe["FecTomEKG"].ToString())}" +
                                    "'" + TabRegHtaDiabe["HabiNuti"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["ObsNutri"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabRegHtaDiabe["ActFisica"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["REmiMedInt"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["RemiNutr"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["RemiPsic"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["RemiOft"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["Trigli"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["HemoGlico"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["MicroAlbumi"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["OlvTomaMedHTA"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["DesHoraMed"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["BnNoTomaMed"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["CaeMalMediDejaToma"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["AdheTrata"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["Observacion"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabRegHtaDiabe["TomaGlice"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabRegHtaDiabe["FecGli"].ToString())}" +
                                    "'" + TabRegHtaDiabe["TomaColeste"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabRegHtaDiabe["FecCol"].ToString())}" +
                                    "'" + TabRegHtaDiabe["TomaCreati"].ToString() + "'," +
                                    "'" + TabRegHtaDiabe["TomaTrigli"].ToString() + "'," +
                                     $"{Conexion.ValidarFechaNula(TabRegHtaDiabe["FecTri"].ToString())}" +
                                     $"{Conexion.ValidarFechaNula(TabRegHtaDiabe["FecMInterna"].ToString())}" +
                                     $"{Conexion.ValidarFechaNula(TabRegHtaDiabe["FecOftal"].ToString())}" +
                                     $"{Conexion.ValidarFechaNula(TabRegHtaDiabe["FecPsic"].ToString())}" +
                                     $"{Conexion.ValidarFechaNula(TabRegHtaDiabe["FecNutri"].ToString())}" +
                                    "'" + TabRegHtaDiabe["TomaGlicosi"].ToString() + "'," +
                                     $"{Conexion.ValidarFechaNula(TabRegHtaDiabe["FecHGli"].ToString())}" +
                                    "'" + TabRegHtaDiabe["TomaMicro"].ToString() + "'," +
                                     $"{Conexion.ValidarFechaNula(TabRegHtaDiabe["FecMicro"].ToString())}" +
                                    "'" + TabRegHtaDiabe["TomaUroAnal"].ToString() + "'," +
                                     $"{Conexion.ValidarFechaNula(TabRegHtaDiabe["FecUroAnal"].ToString())}" +
                                    "'" + TabRegHtaDiabe["UroAnal"].ToString() + "'" +
                                    ")";

                                    Boolean Insert = Conexion.SqlInsert(Utils.SqlDatos);

                                }
                                else
                                {
                                    Utils.SqlDatos = "UPDATE [DACONEXTSQL].[dbo].[Datos registro HtaDiabeticos] SET " +
                                    "CodEnfer= '" + TabRegHtaDiabe["CodEnfer"].ToString() + "'," +
                                    "ClasiHTA= '" + TabRegHtaDiabe["ClasiHTA"].ToString() + "'," +
                                    "ReingNS= '" + TabRegHtaDiabe["ReingNS"].ToString() + "'," +
                                    "FumaNS= '" + TabRegHtaDiabe["FumaNS"].ToString() + "'," +
                                    "LesBlaNS= '" + TabRegHtaDiabe["LesBlaNS"].ToString() + "'," +
                                    "ControNS= '" + TabRegHtaDiabe["ControNS"].ToString() + "'," +
                                    "ValGlice= '" + TabRegHtaDiabe["ValGlice"].ToString() + "'," +
                                    "ColeTotal= '" + TabRegHtaDiabe["ColeTotal"].ToString() + "'," +
                                    "ColeHDL= '" + TabRegHtaDiabe["ColeHDL"].ToString() + "'," +
                                    "Creatinina= '" + TabRegHtaDiabe["Creatinina"].ToString() + "'," +
                                    "ColeLDL= '" + TabRegHtaDiabe["ColeLDL"].ToString() + "'," +
                                    "Cintura= '" + TabRegHtaDiabe["Cintura"].ToString() + "'," +
                                    "SuperCorp= '" + TabRegHtaDiabe["SuperCorp"].ToString() + "'," +
                                    "TasFilGlo= '" + TabRegHtaDiabe["TasFilGlo"].ToString() + "'," +
                                    "EstERC= '" + TabRegHtaDiabe["EstERC"].ToString() + "'," +
                                    "AnormUroan= '" + TabRegHtaDiabe["AnormUroan"].ToString() + "'," +
                                    $"FecExamenes = {Conexion.ValidarFechaNula(TabRegHtaDiabe["FecExamenes"].ToString())}" +
                                    "CodRegis= '" + TabRegHtaDiabe["CodRegis"].ToString() + "'," +
                                    $"FecRegis = {Conexion.ValidarFechaNula(TabRegHtaDiabe["FecRegis"].ToString())}" +
                                    "CodModi= '" + TabRegHtaDiabe["CodModi"].ToString() + "'," +
                                    $"FecModi = {Conexion.ValidarFechaNula(TabRegHtaDiabe["FecModi"].ToString())}" +
                                    "CondClinAsoc= '" + TabRegHtaDiabe["CondClinAsoc"].ToString() + "'," +
                                    "RiesCardio= '" + TabRegHtaDiabe["RiesCardio"].ToString() + "'," +
                                    "TomaEkg= '" + TabRegHtaDiabe["TomaEkg"].ToString() + "'," +
                                    "EKG= '" + TabRegHtaDiabe["EKG"].ToString().Replace("'", "''") + "'," +
                                    $"FecTomEKG = {Conexion.ValidarFechaNula(TabRegHtaDiabe["FecTomEKG"].ToString())}" +
                                    "HabiNuti= '" + TabRegHtaDiabe["HabiNuti"].ToString() + "'," +
                                    "ObsNutri= '" + TabRegHtaDiabe["ObsNutri"].ToString().Replace("'", "''") + "'," +
                                    "ActFisica= '" + TabRegHtaDiabe["ActFisica"].ToString() + "'," +
                                    "REmiMedInt= '" + TabRegHtaDiabe["REmiMedInt"].ToString() + "'," +
                                    "RemiNutr= '" + TabRegHtaDiabe["RemiNutr"].ToString() + "'," +
                                    "RemiPsic= '" + TabRegHtaDiabe["RemiPsic"].ToString() + "'," +
                                    "RemiOft= '" + TabRegHtaDiabe["RemiOft"].ToString() + "'," +
                                    "Trigli= '" + TabRegHtaDiabe["Trigli"].ToString() + "'," +
                                    "HemoGlico= '" + TabRegHtaDiabe["HemoGlico"].ToString() + "'," +
                                    "MicroAlbumi= '" + TabRegHtaDiabe["MicroAlbumi"].ToString() + "'," +
                                    "OlvTomaMedHTA= '" + TabRegHtaDiabe["OlvTomaMedHTA"].ToString() + "'," +
                                    "DesHoraMed= '" + TabRegHtaDiabe["DesHoraMed"].ToString() + "'," +
                                    "BnNoTomaMed= '" + TabRegHtaDiabe["BnNoTomaMed"].ToString() + "'," +
                                    "CaeMalMediDejaToma= '" + TabRegHtaDiabe["CaeMalMediDejaToma"].ToString() + "'," +
                                    "AdheTrata= '" + TabRegHtaDiabe["AdheTrata"].ToString() + "'," +
                                    "Observacion= '" + TabRegHtaDiabe["Observacion"].ToString().Replace("'", "''") + "'," +
                                    "TomaGlice= '" + TabRegHtaDiabe["TomaGlice"].ToString() + "'," +
                                    $"FecGli = {Conexion.ValidarFechaNula(TabRegHtaDiabe["FecGli"].ToString())}" +
                                    "TomaColeste= '" + TabRegHtaDiabe["TomaColeste"].ToString() + "'," +
                                    $"FecCol = {Conexion.ValidarFechaNula(TabRegHtaDiabe["FecCol"].ToString())}" +
                                    "TomaCreati= '" + TabRegHtaDiabe["TomaCreati"].ToString() + "'," +
                                    "TomaTrigli= '" + TabRegHtaDiabe["TomaTrigli"].ToString() + "'," +
                                    $"FecTri = {Conexion.ValidarFechaNula(TabRegHtaDiabe["FecTri"].ToString())}" +
                                    $"FecMInterna = {Conexion.ValidarFechaNula(TabRegHtaDiabe["FecMInterna"].ToString())}" +
                                    $"FecOftal = {Conexion.ValidarFechaNula(TabRegHtaDiabe["FecOftal"].ToString())}" +
                                    $"FecPsic = {Conexion.ValidarFechaNula(TabRegHtaDiabe["FecPsic"].ToString())}" +
                                    $"FecNutri = {Conexion.ValidarFechaNula(TabRegHtaDiabe["FecNutri"].ToString())}" +
                                    "TomaGlicosi= '" + TabRegHtaDiabe["TomaGlicosi"].ToString() + "'," +
                                    $"FecHGli = {Conexion.ValidarFechaNula(TabRegHtaDiabe["FecHGli"].ToString())}" +
                                    "TomaMicro= '" + TabRegHtaDiabe["TomaMicro"].ToString() + "'," +
                                    $"FecMicro = {Conexion.ValidarFechaNula(TabRegHtaDiabe["FecMicro"].ToString())}" +
                                    "TomaUroAnal= '" + TabRegHtaDiabe["TomaUroAnal"].ToString() + "'," +
                                    $"FecUroAnal = {Conexion.ValidarFechaNula(TabRegHtaDiabe["FecUroAnal"].ToString())}" +
                                    "UroAnal= '" + TabRegHtaDiabe["UroAnal"].ToString() + "' " +
                                    "where CodAten = N'" + CodRegHtaDiabe + "'";

                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);

                                }//if (TabRegMedicCen.HasRows == false)

                                TabRegHtaDiabeCen.Close();

                            }//Using
                        }//While

                        TabRegHtaDiabe.Close();
                        return 1;

                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion RegistroHtaDiabeticosIMP  " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }
        private int DetalleescalaabreviadaIMP(string CodHistDEA)
        {
            try
            {

                string CodConReg, CodDetEscAbre, CodItemEscREF, SqlDetEscAbre, SqlDetEscAbreCen, CodEscDtt, EscaDesa, CodRan;
                int NuDeta = 0;

                SqlDataReader TabDetEscAbre, TabDetEscAbreCen;

                SqlDetEscAbre = "SELECT [Datos detalle escala abreviada].* ";
                SqlDetEscAbre = SqlDetEscAbre + "FROM [DACONEXTSQL].[dbo].[Datos detalle escala abreviada]  ";
                SqlDetEscAbre = SqlDetEscAbre + "WHERE ([Datos detalle escala abreviada].CodAtencion = N'" + CodHistDEA + "')";



                ConectarCentral();

                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command2 = new SqlCommand(SqlDetEscAbre, connection2);
                    command2.Connection.Open();
                    TabDetEscAbre = command2.ExecuteReader();


                    if (TabDetEscAbre.HasRows == false)
                    {
                        //'No hay registros en la tabla Datos detalle de escala abreviada
                        return 0;
                    }
                    else
                    {
                        ConectarPortatil();
                        while (TabDetEscAbre.Read())
                        {
                            //Revisamos si el número de codigo de atencion existe
                            CodConReg = TabDetEscAbre["CodControl"].ToString();
                            CodDetEscAbre = TabDetEscAbre["CodAtencion"].ToString();
                            CodEscDtt = TabDetEscAbre["CodEscalaDet"].ToString();

                            NuDeta = Convert.ToInt32(TabDetEscAbre["NumeralDet"]);
                            EscaDesa = TabDetEscAbre["EADNumDet"].ToString();
                            CodRan = TabDetEscAbre["CodigoRango"].ToString();

                            CodItemEscREF = TabDetEscAbre["Item"].ToString();

                            //SqlDetEscAbreCen = "SELECT [Datos detalle escala abreviada].* ";
                            //SqlDetEscAbreCen = SqlDetEscAbreCen + "FROM [DACONEXTSQL].[dbo].[Datos detalle escala abreviada] ";
                            //SqlDetEscAbreCen = SqlDetEscAbreCen + "WHERE (CodAtencion = '" + CodDetEscAbre + "') AND ";
                            //SqlDetEscAbreCen = SqlDetEscAbreCen + "(ItemEscREF = '" + CodItemEscREF + "') AND (CodControl = '" + CodConReg + "')";


                            SqlDetEscAbreCen = "SELECT [Datos detalle escala abreviada].* ";
                            SqlDetEscAbreCen = SqlDetEscAbreCen + "FROM [DACONEXTSQL].[dbo].[Datos detalle escala abreviada] ";
                            SqlDetEscAbreCen = SqlDetEscAbreCen + "WHERE (CodControl = '" + CodConReg + "') AND (CodAtencion = '" + CodDetEscAbre + "') and ";
                            SqlDetEscAbreCen = SqlDetEscAbreCen + "(CodEscalaDet = N'" + CodEscDtt + "') AND (NumeralDet = " + NuDeta + ") and ";
                            SqlDetEscAbreCen = SqlDetEscAbreCen + "(EADNumDet = N'" + EscaDesa + "') AND (CodigoRango = N'" + CodRan + "')";


                            using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command = new SqlCommand(SqlDetEscAbreCen, connection);
                                command.Connection.Open();
                                TabDetEscAbreCen = command.ExecuteReader();

                                if (TabDetEscAbreCen.HasRows == false)
                                {

                                    Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos detalle escala abreviada]  " +
                                    "(" +
                                    "CodControl," +
                                    "CodAtencion," +
                                    "CodEscalaDet," +
                                    "NumeralDet," +
                                    "EADNumDet," +
                                    "CodigoRango," +
                                    "Estados," +
                                    "UnidadEdadES," +
                                    "ValorEdadES," +
                                    "NoEvaluar," +
                                    "ItemEscREF" +
                                    ")" +
                                    "VALUES (" +
                                    "'" + CodConReg + "'," +
                                    "'" + CodDetEscAbre + "'," +
                                    "'" + CodEscDtt + "'," +
                                    "" + NuDeta + "," +
                                    "'" + EscaDesa + "'," +
                                    "'" + CodRan + "'," +
                                    "'" + TabDetEscAbre["Estados"].ToString() + "'," +
                                    "'" + TabDetEscAbre["UnidadEdadES"].ToString() + "'," +
                                    "'" + TabDetEscAbre["ValorEdadES"].ToString() + "'," +
                                    "'" + TabDetEscAbre["NoEvaluar"].ToString() + "'," +
                                    "'" + CodItemEscREF + "'" +
                                    ")";

                                    Boolean Insert = Conexion.SqlInsert(Utils.SqlDatos);

                                }
                                else
                                {

                                    Utils.SqlDatos = "UPDATE  [DACONEXTSQL].[dbo].[Datos detalle escala abreviada] SET " +
                                    "Estados = '" + TabDetEscAbre["Estados"].ToString() + "'," +
                                    "UnidadEdadES = '" + TabDetEscAbre["UnidadEdadES"].ToString() + "'," +
                                    "ValorEdadES = '" + TabDetEscAbre["ValorEdadES"].ToString() + "'," +
                                    "NoEvaluar = '" + TabDetEscAbre["NoEvaluar"].ToString() + "'," +
                                    "ItemEscREF = '" + CodItemEscREF + "' " +
                                    "WHERE (CodControl = '" + CodConReg + "') AND (CodAtencion = '" + CodDetEscAbre + "') and " +
                                    "(CodEscalaDet = N'" + CodEscDtt + "') AND (NumeralDet = " + NuDeta + ") and " +
                                    "(EADNumDet = N'" + EscaDesa + "') AND (CodigoRango = N'" + CodRan + "')";

                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);

                                }//if (TabRegMedicCen.HasRows == false)

                                TabDetEscAbreCen.Close();

                            }//Using
                        }//While

                        TabDetEscAbre.Close();
                        return 1;

                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion DetalleescalaabreviadaIMP  " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private int DetallesdeobservacionespordocumentoIMP(string CodHistDOD)
        {
            try
            {
                string SqlDetObsDoc, SqlDetObsDocCen, CodDetObsDoc = "", CodNumeroDocumento, TipoDocRegis;
                SqlDataReader TabDetObsDoc, TabDetObsDocCen;
                int FunRegImag = 0, FunRegLabo = 0, FumRegMedic = 0, FumRegProced = 0;



                SqlDetObsDoc = "SELECT [Datos detalles de observaciones por documento].* ";
                SqlDetObsDoc += "FROM [DACONEXTSQL].[dbo].[Datos detalles de observaciones por documento] ";
                SqlDetObsDoc += "WHERE ([Datos detalles de observaciones por documento].CodigoAtencion = N'" + CodHistDOD + "')";

                ConectarCentral();
                using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command = new SqlCommand(SqlDetObsDoc, connection);
                    command.Connection.Open();
                    TabDetObsDoc = command.ExecuteReader();

                    if (TabDetObsDoc.HasRows == false)
                    {
                        //'No hay registros en la tabla Datos detalles de observaciones por documento
                        return 0;
                    }
                    else
                    {
                        ConectarPortatil();
                        while (TabDetObsDoc.Read())
                        {
                            //Revisamos si el número de codigo de atencion existe

                            CodDetObsDoc = "";
                            CodNumeroDocumento = "";
                            TipoDocRegis = "";

                            //Revisamos si el número de codigo de atencion existe

                            CodDetObsDoc = TabDetObsDoc["CodigoAtencion"].ToString();
                            CodNumeroDocumento = TabDetObsDoc["NumeroDocumento"].ToString();
                            TipoDocRegis = TabDetObsDoc["TipoDocumento"].ToString();


                            SqlDetObsDocCen = "SELECT [Datos detalles de observaciones por documento].* ";
                            SqlDetObsDocCen += "FROM [DACONEXTSQL].[dbo].[Datos detalles de observaciones por documento] ";
                            SqlDetObsDocCen += "WHERE (CodigoAtencion = N'" + CodDetObsDoc + "') AND ";
                            SqlDetObsDocCen += "(NumeroDocumento = N'" + CodNumeroDocumento + "') AND ";
                            SqlDetObsDocCen += "(TipoDocumento = N'" + TipoDocRegis + "') ";

                            using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command2 = new SqlCommand(SqlDetObsDocCen, connection2);
                                command2.Connection.Open();
                                TabDetObsDocCen = command2.ExecuteReader();

                                if (TabDetObsDocCen.HasRows == false)
                                {
                                    //Agregue
                                    Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos detalles de observaciones por documento]   " +
                                    "(" +
                                    "CodigoAtencion," +
                                    "NumeroDocumento," +
                                    "TipoDocumento," +
                                    "Observaciones," +
                                    "CodiMedico," +
                                    "LugarRealiza," +
                                    "TipoFormula," +
                                    "TipoSerSoli," +
                                    "PrioridadAten," +
                                    "Consecutivo3047," +
                                    "CodiOrden," +
                                    "CodRegis," +
                                    "FechaRegis," +
                                    "Horaregis" +
                                    ") " + "Values(" +
                                    "'" + TabDetObsDoc["CodigoAtencion"].ToString() + "'," +
                                    "'" + TabDetObsDoc["NumeroDocumento"].ToString() + "'," +
                                    "'" + TabDetObsDoc["TipoDocumento"].ToString() + "'," +
                                    "'" + TabDetObsDoc["Observaciones"].ToString() + "'," +
                                    "'" + TabDetObsDoc["CodiMedico"].ToString() + "'," +
                                    "'" + TabDetObsDoc["LugarRealiza"].ToString() + "'," +
                                    "'" + TabDetObsDoc["TipoFormula"].ToString() + "'," +
                                    "'" + TabDetObsDoc["TipoSerSoli"].ToString() + "'," +
                                    "'" + TabDetObsDoc["PrioridadAten"].ToString() + "'," +
                                    "'" + TabDetObsDoc["Consecutivo3047"].ToString() + "'," +
                                    "'" + TabDetObsDoc["CodiOrden"].ToString() + "'," +
                                    "'" + TabDetObsDoc["CodRegis"].ToString() + "'," +
                                   $"{Conexion.ValidarFechaNula(TabDetObsDoc["FechaRegis"].ToString())}" +
                                   $"{Conexion.ValidarHoraNula(TabDetObsDoc["Horaregis"].ToString(), false)}" +
                                   ")";

                                    Boolean Insert = Conexion.SqlInsert(Utils.SqlDatos);

                                }
                                else
                                {
                                    //Modifique los datos

                                    Utils.SqlDatos = "UPDATE [DACONEXTSQL].[dbo].[Datos detalles de observaciones por documento]  SET " +
                                    "TipoDocumento = '" + TabDetObsDoc["TipoDocumento"].ToString() + "'," +
                                    "Observaciones = '" + TabDetObsDoc["Observaciones"].ToString() + "'," +
                                    "CodiMedico = '" + TabDetObsDoc["CodiMedico"].ToString() + "'," +
                                    "LugarRealiza = '" + TabDetObsDoc["LugarRealiza"].ToString() + "'," +
                                    "TipoFormula = '" + TabDetObsDoc["TipoFormula"].ToString() + "'," +
                                    "TipoSerSoli = '" + TabDetObsDoc["TipoSerSoli"].ToString() + "'," +
                                    "PrioridadAten = '" + TabDetObsDoc["PrioridadAten"].ToString() + "'," +
                                    "Consecutivo3047 = '" + TabDetObsDoc["Consecutivo3047"].ToString() + "'," +
                                    "CodiOrden = '" + TabDetObsDoc["CodiOrden"].ToString() + "'," +
                                    "CodRegis = '" + TabDetObsDoc["CodRegis"].ToString() + "'," +
                                    $"FechaRegis = {Conexion.ValidarFechaNula(TabDetObsDoc["FechaRegis"].ToString())}" +
                                    $"Horaregis = {Conexion.ValidarHoraNula(TabDetObsDoc["Horaregis"].ToString(), false)}" +
                                    "WHERE  (CodigoAtencion = N'" + CodDetObsDoc + "') AND (NumeroDocumento = N'" + CodNumeroDocumento + "') AND (TipoDocumento = N'" + TipoDocRegis + "')  ";

                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);
                                }

                                TabDetObsDocCen.Close();

                            }//Using
                        }//While

                        FunRegImag = RegistrodeimagenologiaIMP(CodDetObsDoc);
                        FunRegLabo = RegistrolaboratoriosIMP(CodDetObsDoc);
                        FumRegMedic = RegistrosmedicamentosIMP(CodDetObsDoc);
                        FumRegProced = RegistrodeprocedimientosIMP(CodDetObsDoc);



                        TabDetObsDoc.Close();
                        return 1;


                    }//    if (TabDetObsDoc.HasRows == false)
                }//Using
              

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion DetallesdeobservacionespordocumentoEXP  " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private int RegistrodeprocedimientosIMP(string CodHistRP)
        {
            try
            {
                string CodRegProced, ProcedCodProce, ProcedNumPro, SqlRegProced, SqlRegProcedCen;

                SqlDataReader TabRegProced, TabRegProcedCen;

                SqlRegProced = "SELECT [Datos registro de procedimientos].* ";
                SqlRegProced = SqlRegProced + "FROM [DACONEXTSQL].[dbo].[Datos registro de procedimientos] ";
                SqlRegProced = SqlRegProced + "WHERE ([Datos registro de procedimientos].CodAten = N'" + CodHistRP + "')";

                ConectarCentral();

                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command2 = new SqlCommand(SqlRegProced, connection2);
                    command2.Connection.Open();
                    TabRegProced = command2.ExecuteReader();


                    if (TabRegProced.HasRows == false)
                    {
                        //'No hay registros en la tabla Datos registro  la tabla Datos registro de procedimientos
                        return 0;
                    }
                    else
                    {
                        ConectarPortatil();
                        while (TabRegProced.Read())
                        {
                            //Revisamos si el número de codigo de atencion existe
                            CodRegProced = TabRegProced["CodAten"].ToString();
                            ProcedCodProce = TabRegProced["CodProce"].ToString();
                            ProcedNumPro = TabRegProced["NumPro"].ToString();

                            SqlRegProcedCen = "SELECT * FROM [DACONEXTSQL].[dbo].[Datos registro de procedimientos] ";
                            SqlRegProcedCen = SqlRegProcedCen + "WHERE CodAten = N'" + CodRegProced + "' AND CodProce = N'" + ProcedCodProce + "' AND NumPro = N'" + ProcedNumPro + "'";


                            using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command = new SqlCommand(SqlRegProcedCen, connection);
                                command.Connection.Open();
                                TabRegProcedCen = command.ExecuteReader();

                                if (TabRegProcedCen.HasRows == false)
                                {


                                    Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos registro de procedimientos] " +
                                    "(" +
                                    "CodAten," +
                                    "NumPro," +
                                    "NombreActividad," +
                                    "TipoOrdenPro," +
                                    "CodProce," +
                                    "CanPro," +
                                    "Obser," +
                                    "CodMed," +
                                    "Leido," +
                                    "LugarAten," +
                                    "POS," +
                                    "Frecuencia," +
                                    "Control," +
                                    "EfectoTerapeutico," +
                                    "AternativaPOS," +
                                    "CopPOSAlternativo," +
                                    "PorquenoSerealiza," +
                                    "FechaReg," +
                                    "HoraReg," +
                                    "TipoFormula" +
                                    ")" +
                                    "VALUES (" +
                                    "'" + TabRegProced["CodAten"].ToString() + "'," +
                                    "'" + TabRegProced["NumPro"].ToString() + "'," +
                                    "'" + TabRegProced["NombreActividad"].ToString() + "'," +
                                    "'" + TabRegProced["TipoOrdenPro"].ToString() + "'," +
                                    "'" + TabRegProced["CodProce"].ToString() + "'," +
                                    "'" + TabRegProced["CanPro"].ToString() + "'," +
                                    "'" + TabRegProced["Obser"].ToString() + "'," +
                                    "'" + TabRegProced["CodMed"].ToString() + "'," +
                                    "'" + TabRegProced["Leido"].ToString() + "'," +
                                    "'" + TabRegProced["LugarAten"].ToString() + "'," +
                                    "'" + TabRegProced["POS"].ToString() + "'," +
                                    "'" + TabRegProced["Frecuencia"].ToString() + "'," +
                                    "'" + TabRegProced["Control"].ToString() + "'," +
                                    "'" + TabRegProced["EfectoTerapeutico"].ToString() + "'," +
                                    "'" + TabRegProced["AternativaPOS"].ToString() + "'," +
                                    "'" + TabRegProced["CopPOSAlternativo"].ToString() + "'," +
                                    "'" + TabRegProced["PorquenoSerealiza"].ToString() + "'," +
                                   $"{Conexion.ValidarFechaNula(TabRegProced["FechaReg"].ToString())}" +
                                   $"{Conexion.ValidarHoraNula(TabRegProced["HoraReg"].ToString())}" +
                                    "'" + TabRegProced["TipoFormula"].ToString() + "'" +
                                    ")";

                                    Boolean Insert = Conexion.SqlInsert(Utils.SqlDatos);

                                }
                                else
                                {
                                    Utils.SqlDatos = "UPDATE [DACONEXTSQL].[dbo].[Datos registro de procedimientos] SET " +
                                    "NumPro = '" + TabRegProced["NumPro"].ToString() + "'," +
                                    "NombreActividad = '" + TabRegProced["NombreActividad"].ToString() + "'," +
                                    "TipoOrdenPro = '" + TabRegProced["TipoOrdenPro"].ToString() + "'," +
                                    "CodProce = '" + TabRegProced["CodProce"].ToString() + "'," +
                                    "CanPro = '" + TabRegProced["CanPro"].ToString() + "'," +
                                    "Obser = '" + TabRegProced["Obser"].ToString() + "'," +
                                    "CodMed = '" + TabRegProced["CodMed"].ToString() + "'," +
                                    "Leido = '" + TabRegProced["Leido"].ToString() + "'," +
                                    "LugarAten = '" + TabRegProced["LugarAten"].ToString() + "'," +
                                    "POS = '" + TabRegProced["POS"].ToString() + "'," +
                                    "Frecuencia = '" + TabRegProced["Frecuencia"].ToString() + "'," +
                                    "Control = '" + TabRegProced["Control"].ToString() + "'," +
                                    "EfectoTerapeutico = '" + TabRegProced["EfectoTerapeutico"].ToString() + "'," +
                                    "AternativaPOS = '" + TabRegProced["AternativaPOS"].ToString() + "'," +
                                    "CopPOSAlternativo = '" + TabRegProced["CopPOSAlternativo"].ToString() + "'," +
                                    "PorquenoSerealiza = '" + TabRegProced["PorquenoSerealiza"].ToString() + "'," +
                                    $"FechaReg = {Conexion.ValidarFechaNula(TabRegProced["FechaReg"].ToString())} " +
                                    $"HoraReg = {Conexion.ValidarHoraNula(TabRegProced["HoraReg"].ToString())}" +
                                    "TipoFormula = '" + TabRegProced["TipoFormula"].ToString() + "'" +
                                    "WHERE CodAten = N'" + CodRegProced + "' AND CodProce = N'" + ProcedCodProce + "' AND NumPro = N'" + ProcedNumPro + "'";

                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);

                                }//if (TabRegMedicCen.HasRows == false)

                                TabRegProcedCen.Close();

                            }//Using
                        }//While

                        TabRegProced.Close();
                        return 1;

                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion RegistrodeprocedimientosIMP  " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private int RegistrosmedicamentosIMP(string CodHistRM)
        {
            try
            {
                string CodRegMedic, MedicCodMedic, MedicNumForm, SqlRegMedic, SqlRegMedicCen, TipoDocuMed;

                SqlDataReader TabRegMedic, TabRegMedicCen;

                SqlRegMedic = "SELECT [Datos registros medicamentos].* ";
                SqlRegMedic = SqlRegMedic + "FROM [DACONEXTSQL].[dbo].[Datos registros medicamentos] ";
                SqlRegMedic = SqlRegMedic + "WHERE ([Datos registros medicamentos].CodAten = N'" + CodHistRM + "')";

                ConectarCentral();

                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command2 = new SqlCommand(SqlRegMedic, connection2);
                    command2.Connection.Open();
                    TabRegMedic = command2.ExecuteReader();


                    if (TabRegMedic.HasRows == false)
                    {
                        //'No hay registros en la tabla Datos registro laboratorios
                        return 0;
                    }
                    else
                    {
                        ConectarPortatil();
                        while (TabRegMedic.Read())
                        {
                            
                            CodRegMedic = TabRegMedic["CodAten"].ToString();
                            MedicCodMedic = TabRegMedic["CodMedic"].ToString();
                            MedicNumForm = TabRegMedic["NumForm"].ToString();


                            SqlRegMedicCen = "SELECT [Datos registros medicamentos].* ";
                            SqlRegMedicCen = SqlRegMedicCen + "FROM  [DACONEXTSQL].[dbo].[Datos registros medicamentos] ";
                            SqlRegMedicCen = SqlRegMedicCen + "WHERE (CodAten = N'" + CodRegMedic + "') AND (CodMedic = N'" + MedicCodMedic + "') AND (NumForm = N'" + MedicNumForm + "')";




                            using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command = new SqlCommand(SqlRegMedicCen, connection);
                                command.Connection.Open();
                                TabRegMedicCen = command.ExecuteReader();


                                if (string.IsNullOrWhiteSpace(TabRegMedic["TipoDocuMed"].ToString()))
                                {
                                    TipoDocuMed = "01";
                                }
                                else
                                {
                                    TipoDocuMed = TabRegMedic["TipoDocuMed"].ToString();
                                }

                                if (TabRegMedicCen.HasRows == false)
                                {
                                    Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos registros medicamentos] " +
                                    "(" +
                                    "NumForm," +
                                    "CodMedic," +
                                    "CodAten," +
                                    "Formafarma," +
                                    "CantMedi," +
                                    "Posologia," +
                                    "Obser," +
                                    "TipoFormula," +
                                    "CodMed," +
                                    "FechaReg," +
                                    "LugarAten," +
                                    "TipoServ," +
                                    "DosisDia," +
                                    "TiemTrata," +
                                    "AyuDiagnos," +
                                    "Justifica1," +
                                    "Justifica2," +
                                    "Justifica3," +
                                    "Justifica4," +
                                    "Justifica5," +
                                    "Justifica6," +
                                    "Justifica7," +
                                    "Justifica8," +
                                    "AlterPrevia," +
                                    "Riesgo," +
                                    "Progresion," +
                                    "AltoRiesgo," +
                                    "Secuelas," +
                                    "OtroRies," +
                                    "CualOtra," +
                                    "TipoDocuMed" +
                                    ")" +
                                    "VALUES (" +
                                    "'" + TabRegMedic["NumForm"].ToString() + "'," +
                                    "'" + TabRegMedic["CodMedic"].ToString() + "'," +
                                    "'" + TabRegMedic["CodAten"].ToString() + "'," +
                                    "'" + TabRegMedic["Formafarma"].ToString() + "'," +
                                    "'" + TabRegMedic["CantMedi"].ToString() + "'," +
                                    "'" + TabRegMedic["Posologia"].ToString() + "'," +
                                    "'" + TabRegMedic["Obser"].ToString() + "'," +
                                    "'" + TabRegMedic["TipoFormula"].ToString() + "'," +
                                    "'" + TabRegMedic["CodMed"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabRegMedic["FechaReg"].ToString())}" +
                                    "'" + TabRegMedic["LugarAten"].ToString() + "'," +
                                    "'" + TabRegMedic["TipoServ"].ToString() + "'," +
                                    "'" + TabRegMedic["DosisDia"].ToString() + "'," +
                                    "'" + TabRegMedic["TiemTrata"].ToString() + "'," +
                                    "'" + TabRegMedic["AyuDiagnos"].ToString() + "'," +
                                    "'" + TabRegMedic["Justifica1"].ToString() + "'," +
                                    "'" + TabRegMedic["Justifica2"].ToString() + "'," +
                                    "'" + TabRegMedic["Justifica3"].ToString() + "'," +
                                    "'" + TabRegMedic["Justifica4"].ToString() + "'," +
                                    "'" + TabRegMedic["Justifica5"].ToString() + "'," +
                                    "'" + TabRegMedic["Justifica6"].ToString() + "'," +
                                    "'" + TabRegMedic["Justifica7"].ToString() + "'," +
                                    "'" + TabRegMedic["Justifica8"].ToString() + "'," +
                                    "'" + TabRegMedic["AlterPrevia"].ToString() + "'," +
                                    "'" + TabRegMedic["Riesgo"].ToString() + "'," +
                                    "'" + TabRegMedic["Progresion"].ToString() + "'," +
                                    "'" + TabRegMedic["AltoRiesgo"].ToString() + "'," +
                                    "'" + TabRegMedic["Secuelas"].ToString() + "'," +
                                    "'" + TabRegMedic["OtroRies"].ToString() + "'," +
                                    "'" + TabRegMedic["CualOtra"].ToString() + "'," +
                                    "'" + TipoDocuMed + "'" +
                                    ")";

                                    Boolean Insert = Conexion.SqlInsert(Utils.SqlDatos);

                                }
                                else
                                {

                                    Utils.SqlDatos = "UPDATE [DACONEXTSQL].[dbo].[Datos registros medicamentos] SET " +
                                    "CodMedic = '" + TabRegMedic["CodMedic"].ToString() + "'," +
                                    "Formafarma = '" + TabRegMedic["Formafarma"].ToString() + "'," +
                                    "CantMedi = '" + TabRegMedic["CantMedi"].ToString() + "'," +
                                    "Posologia = '" + TabRegMedic["Posologia"].ToString() + "'," +
                                    "Obser = '" + TabRegMedic["Obser"].ToString() + "'," +
                                    "TipoFormula = '" + TabRegMedic["TipoFormula"].ToString() + "'," +
                                    "CodMed = '" + TabRegMedic["CodMed"].ToString() + "'," +
                                    $"FechaReg = {Conexion.ValidarFechaNula(TabRegMedic["FechaReg"].ToString())} " +
                                    "LugarAten = '" + TabRegMedic["LugarAten"].ToString() + "'," +
                                    "TipoServ = '" + TabRegMedic["TipoServ"].ToString() + "'," +
                                    "DosisDia = '" + TabRegMedic["DosisDia"].ToString() + "'," +
                                    "TiemTrata = '" + TabRegMedic["TiemTrata"].ToString() + "'," +
                                    "AyuDiagnos = '" + TabRegMedic["AyuDiagnos"].ToString() + "'," +
                                    "Justifica1 = '" + TabRegMedic["Justifica1"].ToString() + "'," +
                                    "Justifica2 = '" + TabRegMedic["Justifica2"].ToString() + "'," +
                                    "Justifica3 = '" + TabRegMedic["Justifica3"].ToString() + "'," +
                                    "Justifica4 = '" + TabRegMedic["Justifica4"].ToString() + "'," +
                                    "Justifica5 = '" + TabRegMedic["Justifica5"].ToString() + "'," +
                                    "Justifica6 = '" + TabRegMedic["Justifica6"].ToString() + "'," +
                                    "Justifica7 = '" + TabRegMedic["Justifica7"].ToString() + "'," +
                                    "Justifica8 = '" + TabRegMedic["Justifica8"].ToString() + "'," +
                                    "AlterPrevia = '" + TabRegMedic["AlterPrevia"].ToString() + "'," +
                                    "Riesgo = '" + TabRegMedic["Riesgo"].ToString() + "'," +
                                    "Progresion = '" + TabRegMedic["Progresion"].ToString() + "'," +
                                    "AltoRiesgo = '" + TabRegMedic["AltoRiesgo"].ToString() + "'," +
                                    "Secuelas = '" + TabRegMedic["Secuelas"].ToString() + "'," +
                                    "OtroRies = '" + TabRegMedic["OtroRies"].ToString() + "'," +
                                    "CualOtra = '" + TabRegMedic["CualOtra"].ToString() + "'," +
                                    "TipoDocuMed = '" + TipoDocuMed + "' " +
                                    "WHERE (CodAten = N'" + CodRegMedic + "') AND (CodMedic = N'" + MedicCodMedic + "') AND (NumForm = N'" + MedicNumForm + "')";
                                    // TabRegMedicCen!TipoDocuMed = TabRegMedic!TipoDocuMed
                                    //' TabRegMedicCen!ItemConsecMEDREF = TabRegMedic!ItemConsecMED

                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);


                                }//if (TabRegMedicCen.HasRows == false)

                                TabRegMedicCen.Close();

                            }//Using
                        }//While

                        TabRegMedic.Close();
                        return 1;

                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion RegistrosmedicamentosIMP  " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }
        private int RegistrolaboratoriosIMP(string CodHistRL)
        {
            try
            {
                string SqlRegLabo, CodRegLabo, LabCodProce, LabNumLab, SqlRegLaboCen;
                SqlDataReader TabRegLabo, TabRegLaboCen;
                ConectarCentral();


                SqlRegLabo = "SELECT [Datos registro laboratorios].* ";
                SqlRegLabo = SqlRegLabo + "FROM [DACONEXTSQL].[dbo].[Datos registro laboratorios] ";
                SqlRegLabo = SqlRegLabo + "WHERE ([Datos registro laboratorios].CodAten = N'" + CodHistRL + "')";

                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command2 = new SqlCommand(SqlRegLabo, connection2);
                    command2.Connection.Open();
                    TabRegLabo = command2.ExecuteReader();


                    if (TabRegLabo.HasRows == false)
                    {
                        //'No hay registros en la tabla Datos registro laboratorios
                        return 0;
                    }
                    else
                    {
                        ConectarPortatil();
                        while (TabRegLabo.Read())
                        {
                            //Revisamos si el número de codigo de atencion existe
                            CodRegLabo = TabRegLabo["CodAten"].ToString();
                            LabCodProce = TabRegLabo["CodProce"].ToString();
                            LabNumLab = TabRegLabo["NumLab"].ToString();

                            SqlRegLaboCen = "SELECT * FROM [DACONEXTSQL].[dbo].[Datos registro laboratorios] ";
                            SqlRegLaboCen = SqlRegLaboCen + "WHERE CodAten = N'" + CodRegLabo + "' AND CodProce = N'" + LabCodProce + "' AND NumLab = N'" + LabNumLab + "'";



                            using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command = new SqlCommand(SqlRegLaboCen, connection);
                                command.Connection.Open();
                                TabRegLaboCen = command.ExecuteReader();

                                if (TabRegLaboCen.HasRows == false)
                                {
                                    Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos registro laboratorios] " +
                                    "(" +
                                    "NumLab," +
                                    "CodProce," +
                                    "CodAten," +
                                    "CanPro," +
                                    "Obser," +
                                    "Leido," +
                                    "LugarAten," +
                                    "CodBac," +
                                    "CodMed," +
                                    "FechaReg," +
                                    "Frecuencia," +
                                    "Control," +
                                    "EfectoTerapeutico," +
                                    "AternativaPOS," +
                                    "CopPOSAlternativo," +
                                    "PorquenoSerealiza," +
                                    "POS," +
                                    "TipoFormula" +
                                    ") " + "Values(" +
                                    "'" + TabRegLabo["NumLab"].ToString() + "'," +
                                    "'" + TabRegLabo["CodProce"].ToString() + "'," +
                                    "'" + TabRegLabo["CodAten"].ToString() + "'," +
                                    "'" + TabRegLabo["CanPro"].ToString() + "'," +
                                    "'" + TabRegLabo["Obser"].ToString() + "'," +
                                    "'" + TabRegLabo["Leido"].ToString() + "'," +
                                    //'********** Campos que no aparecen en la E.S.E de san agustin
                                    "'" + TabRegLabo["TipoOrdenLAB"].ToString() + "'," +
                                    "'" + TabRegLabo["LugarAten"].ToString() + "'," +
                                    "'" + TabRegLabo["CodBac"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabRegLabo["FechaReg"].ToString())}" +
                                    "'" + TabRegLabo["Frecuencia"].ToString() + "'," +
                                    "'" + TabRegLabo["Control"].ToString() + "'," +
                                    "'" + TabRegLabo["EfectoTerapeutico"].ToString() + "'," +
                                    "'" + TabRegLabo["AternativaPOS"].ToString() + "'," +
                                    "'" + TabRegLabo["CopPOSAlternativo"].ToString() + "'," +
                                    "'" + TabRegLabo["PorquenoSerealiza"].ToString() + "'," +
                                    "'" + TabRegLabo["POS"].ToString() + "'," +
                                    "'" + TabRegLabo["TipoFormula"].ToString() + "'" +
                                    ")";

                                    Boolean Insert = Conexion.SqlInsert(Utils.SqlDatos);

                                }
                                else
                                {
                                    Utils.SqlDatos = "UPDATE [DACONEXTSQL].[dbo].[Datos registro laboratorios] SET " +
                                    "NumLab = '" + TabRegLabo["NumLab"].ToString() + "'," +
                                    "CodProce = '" + TabRegLabo["CodProce"].ToString() + "'," +
                                    "CanPro = '" + TabRegLabo["CanPro"].ToString() + "'," +
                                    "Obser = '" + TabRegLabo["Obser"].ToString() + "'," +
                                    "Leido = '" + TabRegLabo["Leido"].ToString() + "'," +
                                    "TipoOrdenLAB = '" + TabRegLabo["TipoOrdenLAB"].ToString() + "'," +
                                    "LugarAten = '" + TabRegLabo["LugarAten"].ToString() + "'," +
                                    "CodBac = '" + TabRegLabo["CodBac"].ToString() + "'," +
                                    "CodMed = '" + TabRegLabo["CodMed"].ToString() + "'," +
                                    $"FechaReg = {Conexion.ValidarFechaNula(TabRegLabo["FechaReg"].ToString())} " +
                                    "Frecuencia = '" + TabRegLabo["Frecuencia"].ToString() + "'," +
                                    "Control = '" + TabRegLabo["Control"].ToString() + "'," +
                                    "EfectoTerapeutico = '" + TabRegLabo["EfectoTerapeutico"].ToString() + "'," +
                                    "AternativaPOS = '" + TabRegLabo["AternativaPOS"].ToString() + "'," +
                                    "CopPOSAlternativo = '" + TabRegLabo["CopPOSAlternativo"].ToString() + "'," +
                                    "PorquenoSerealiza = '" + TabRegLabo["PorquenoSerealiza"].ToString() + "'," +
                                    "POS = '" + TabRegLabo["POS"].ToString() + "'," +
                                    "TipoFormula = '" + TabRegLabo["TipoFormula"].ToString() + "' " +
                                    "WHERE CodAten = N'" + CodRegLabo + "' AND CodProce = N'" + LabCodProce + "' AND NumLab = N'" + LabNumLab + "'";


                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);

                                }
                                TabRegLaboCen.Close();
                            }//Using
                        }//While

                        TabRegLabo.Close();
                        return 1;

                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion RegistrolaboratoriosIMP  " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private int RegistrodeimagenologiaIMP(string CodHistRI)
        {
            try
            {
                string CodRegImag, ImagCodProce, ProceNumIma, SqlRegImag, SqlRegImagCen;

                ConectarCentral();

                SqlRegImag = "SELECT [Datos registro de imagenologia].* ";
                SqlRegImag = SqlRegImag + "FROM [DACONEXTSQL].[dbo].[Datos registro de imagenologia] ";
                SqlRegImag = SqlRegImag + "WHERE ([Datos registro de imagenologia].CodAten = N'" + CodHistRI + "')";

                SqlDataReader TabRegImag, TabRegImagCen;

                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command2 = new SqlCommand(SqlRegImag, connection2);
                    command2.Connection.Open();
                    TabRegImag = command2.ExecuteReader();

                    if (TabRegImag.HasRows == false)
                    {
                        //Revisamos si el número de codigo de atencion existe
                        return 0;
                    }
                    else
                    {
                        ConectarPortatil();
                        while (TabRegImag.Read())
                        {
                            CodRegImag = TabRegImag["CodAten"].ToString();
                            ImagCodProce = TabRegImag["CodProce"].ToString();
                            ProceNumIma = TabRegImag["NumIma"].ToString();

                            SqlRegImagCen = "SELECT * FROM [DACONEXTSQL].[dbo].[Datos registro de imagenologia] ";
                            SqlRegImagCen = SqlRegImagCen + "WHERE CodAten = N'" + CodRegImag + "' AND CodProce = N'" + ImagCodProce + "' AND NumIma = N'" + ProceNumIma + "'";


                            using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command = new SqlCommand(SqlRegImagCen, connection);
                                command.Connection.Open();
                                TabRegImagCen = command.ExecuteReader();


                                if (TabRegImagCen.HasRows == false)
                                {
                                    Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos registro de imagenologia]  " +
                                    "(" +
                                    "NumIma," +
                                    "CodProce," +
                                    "CodAten," +
                                    "CanPro," +
                                    "Obser," +
                                    "FechaReg," +
                                    "CodMed," +
                                    "Leido," +
                                    "LugarAten," +
                                    "Frecuencia," +
                                    "Control," +
                                    "EfectoTerapeutico," +
                                    "AlternativaPOS," +
                                    "CopPOSAlternativo," +
                                    "PorquenoSerealiza," +
                                    "POS," +
                                    "TipoFormula" +
                                    ")" +
                                    "VALUES" +
                                    "(" +
                                    "'" + TabRegImag["NumIma"].ToString() + "'," +
                                    "'" + TabRegImag["CodProce"].ToString() + "'," +
                                    "'" + TabRegImag["CodAten"].ToString() + "'," +
                                    "'" + TabRegImag["CanPro"].ToString() + "'," +
                                    "'" + TabRegImag["Obser"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabRegImag["FechaReg"].ToString())}" +
                                    "'" + TabRegImag["CodMed"].ToString() + "'," +
                                    "'" + TabRegImag["Leido"].ToString() + "'," +
                                    "'" + TabRegImag["LugarAten"].ToString() + "'," +
                                    "'" + TabRegImag["Frecuencia"].ToString() + "'," +
                                    "'" + TabRegImag["Control"].ToString() + "'," +
                                    "'" + TabRegImag["EfectoTerapeutico"].ToString() + "'," +
                                    "'" + TabRegImag["AlternativaPOS"].ToString() + "'," +
                                    "'" + TabRegImag["CopPOSAlternativo"].ToString() + "'," +
                                    "'" + TabRegImag["PorquenoSerealiza"].ToString() + "'," +
                                    "'" + TabRegImag["POS"].ToString() + "'," +
                                    "'" + TabRegImag["TipoFormula"].ToString() + "'" +
                                    ")";

                                    Boolean Insert = Conexion.SqlInsert(Utils.SqlDatos);

                                }
                                else
                                {
                                    Utils.SqlDatos = "UPDATE [DACONEXTSQL].[dbo].[Datos registro de imagenologia] SET " +
                                    "NumIma = '" + TabRegImag["NumIma"].ToString() + "'," +
                                    "CodProce = '" + TabRegImag["CodProce"].ToString() + "'," +
                                    "CanPro = '" + TabRegImag["CanPro"].ToString() + "'," +
                                    "Obser = '" + TabRegImag["Obser"].ToString() + "'," +
                                   $"FechaReg =  {Conexion.ValidarFechaNula(TabRegImag["FechaReg"].ToString())}" +
                                    "CodMed = '" + TabRegImag["CodMed"].ToString() + "'," +
                                    "Leido = '" + TabRegImag["Leido"].ToString() + "'," +
                                    // ********** Campos que no aparecen en la E.S.E de san agustin
                                    //'TabRegImagCen!TipoOrdenIMA = TabRegImag!TipoOrdenIMA
                                    //'**********
                                    "LugarAten = '" + TabRegImag["LugarAten"].ToString() + "'," +
                                    "Frecuencia = '" + TabRegImag["Frecuencia"].ToString() + "'," +
                                    "Control = '" + TabRegImag["Control"].ToString() + "'," +
                                    "EfectoTerapeutico = '" + TabRegImag["EfectoTerapeutico"].ToString() + "'," +
                                    "AlternativaPOS = '" + TabRegImag["AlternativaPOS"].ToString() + "'," +
                                    "CopPOSAlternativo = '" + TabRegImag["CopPOSAlternativo"].ToString() + "'," +
                                    "PorquenoSerealiza = '" + TabRegImag["PorquenoSerealiza"].ToString() + "'," +
                                    "POS = '" + TabRegImag["POS"].ToString() + "'," +
                                    "TipoFormula = '" + TabRegImag["TipoFormula"].ToString() + "' " +
                                    "WHERE CodAten = N'" + CodRegImag + "' AND CodProce = N'" + ImagCodProce + "' AND NumIma = N'" + ProceNumIma + "' ";

                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);


                                }

                                TabRegImagCen.Close();

                            }//Using
                        }//While

                        TabRegImag.Close();
                        return 1;

                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion RegistrodeimagenologiaEXP  " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private int RegistrodeevolucionesIMP(string CodHistRE)
        {
            try
            {
                string CodRegiEvol, ItemEvoCen, SqlRegEvo, SqlRegEvoCen;

                SqlRegEvo = "SELECT [Datos registro de evoluciones].*  ";
                SqlRegEvo = SqlRegEvo + "FROM [DACONEXTSQL].[dbo].[Datos registro de evoluciones] ";
                SqlRegEvo = SqlRegEvo + "WHERE ([Datos registro de evoluciones].CodAtencion = N'" + CodHistRE + "')";

                ConectarCentral();

                SqlDataReader TabRegEvo, TabRegEvoCen;

                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command2 = new SqlCommand(SqlRegEvo, connection2);
                    command2.Connection.Open();
                    TabRegEvo = command2.ExecuteReader();

                    if (TabRegEvo.HasRows == false)
                    {
                        return 0;
                    }
                    else
                    {

                        ConectarPortatil();

                        while (TabRegEvo.Read())
                        {
                            //Revisamos si el número de codigo de atencion existe
                            CodRegiEvol = "";
                            ItemEvoCen = "";
                            CodRegiEvol = TabRegEvo["CodAtencion"].ToString();
                            ItemEvoCen = TabRegEvo["Item"].ToString();

                            SqlRegEvoCen = "SELECT * FROM [DACONEXTSQL].[dbo].[Datos registro de evoluciones] ";
                            SqlRegEvoCen = SqlRegEvoCen + " WHERE CodAtencion = N'" + CodRegiEvol + "' AND ItemREF = N'" + ItemEvoCen + "'";

                            using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command = new SqlCommand(SqlRegEvoCen, connection);
                                command.Connection.Open();
                                TabRegEvoCen = command.ExecuteReader();

                                if (TabRegEvoCen.HasRows == false)
                                {
                                    //AGREGUE

                                    Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos registro de evoluciones] " +
                                    "(" +
                                    "TipodeIngreso," +
                                    "CodAtencion," +
                                    "FechaEvolucion," +
                                    "HoraEvolucion," +
                                    "NotaEvolucion," +
                                    "Subjetivo," +
                                    "Objetivo," +
                                    "Analisis," +
                                    "PlanN," +
                                    "TensionSisto," +
                                    "TensionDiasto," +
                                    "FrecuCardi," +
                                    "FrecuRespi," +
                                    "temperatura," +
                                    "DxEvolucion," +
                                    "DxEvolucion1," +
                                    "DxEvolucion2," +
                                    "DiaHospi," +
                                    "NumeroHoras," +
                                    "Pabellon," +
                                    "Cama," +
                                    "FechaIngresoCama," +
                                    "CuentaNUM," +
                                    "MediReg," +
                                    "Activa," +
                                    "CodRegis," +
                                    "FechaRegis," +
                                    "Horaregis," +
                                    "ItemREF " +
                                    ")" +
                                    "VALUES" +
                                    "(" +
                                    "'" + TabRegEvo["TipodeIngreso"].ToString() + "'," +
                                    "'" + TabRegEvo["CodAtencion"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabRegEvo["FechaEvolucion"].ToString())}" +
                                    $"{Conexion.ValidarHoraNula(TabRegEvo["HoraEvolucion"].ToString())}" +
                                    "'" + TabRegEvo["NotaEvolucion"].ToString().Replace("'","") + "'," +
                                    "'" + TabRegEvo["Subjetivo"].ToString().Replace("'", "") + "'," +
                                    "'" + TabRegEvo["Objetivo"].ToString().Replace("'", "") + "'," +
                                    "'" + TabRegEvo["Analisis"].ToString().Replace("'", "") + "'," +
                                    "'" + TabRegEvo["PlanN"].ToString().Replace("'", "") + "'," +
                                    "'" + TabRegEvo["TensionSisto"].ToString() + "'," +
                                    "'" + TabRegEvo["TensionDiasto"].ToString() + "'," +
                                    "'" + TabRegEvo["FrecuCardi"].ToString() + "'," +
                                    "'" + TabRegEvo["FrecuRespi"].ToString() + "'," +
                                    "'" + TabRegEvo["temperatura"].ToString() + "'," +
                                    "'" + TabRegEvo["DxEvolucion"].ToString() + "'," +
                                    "'" + TabRegEvo["DxEvolucion1"].ToString() + "'," +
                                    "'" + TabRegEvo["DxEvolucion2"].ToString() + "'," +
                                    "'" + TabRegEvo["DiaHospi"].ToString() + "'," +
                                    "'" + TabRegEvo["NumeroHoras"].ToString() + "'," +
                                    "'" + TabRegEvo["Pabellon"].ToString() + "'," +
                                    "'" + TabRegEvo["Cama"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabRegEvo["FechaIngresoCama"].ToString())}" +
                                    "'" + TabRegEvo["CuentaNUM"].ToString() + "'," +
                                    "'" + TabRegEvo["MediReg"].ToString() + "'," +
                                    "'" + TabRegEvo["Activa"].ToString() + "'," +
                                    "'" + TabRegEvo["CodRegis"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabRegEvo["FechaRegis"].ToString())}" +
                                    $"{Conexion.ValidarHoraNula(TabRegEvo["Horaregis"].ToString())}" +

                                    "'" + TabRegEvo["ItemREF"].ToString() + "'" +
                                    ")";

                                    Boolean Insert = Conexion.SqlInsert(Utils.SqlDatos);
                                }
                                else
                                {
                                    //Modifique los datos
                                    Utils.SqlDatos = "UPDATE [DACONEXTSQL].[dbo].[Datos registro de evoluciones]  SET " +
                                    "TipodeIngreso = '" + TabRegEvo["TipodeIngreso"].ToString() + "'," +
                                   $"FechaEvolucion = {Conexion.ValidarFechaNula(TabRegEvo["FechaEvolucion"].ToString())} " +
                                    $"HoraEvolucion = {Conexion.ValidarHoraNula(TabRegEvo["HoraEvolucion"].ToString())}" +
                                    "NotaEvolucion = '" + TabRegEvo["NotaEvolucion"].ToString().Replace("'", "") + "'," +
                                    "Subjetivo = '" + TabRegEvo["Subjetivo"].ToString().Replace("'", "") + "'," +
                                    "Objetivo = '" + TabRegEvo["Objetivo"].ToString().Replace("'", "") + "'," +
                                    "Analisis = '" + TabRegEvo["Analisis"].ToString().Replace("'", "") + "'," +
                                    "PlanN = '" + TabRegEvo["PlanN"].ToString().Replace("'", "") + "'," +
                                    "TensionSisto = '" + TabRegEvo["TensionSisto"].ToString() + "'," +
                                    "TensionDiasto = '" + TabRegEvo["TensionDiasto"].ToString() + "'," +
                                    "FrecuCardi = '" + TabRegEvo["FrecuCardi"].ToString() + "'," +
                                    "FrecuRespi = '" + TabRegEvo["FrecuRespi"].ToString() + "'," +
                                    "temperatura = '" + TabRegEvo["temperatura"].ToString() + "'," +
                                    "DxEvolucion = '" + TabRegEvo["DxEvolucion"].ToString() + "'," +
                                    "DxEvolucion1 = '" + TabRegEvo["DxEvolucion1"].ToString() + "'," +
                                    "DxEvolucion2 = '" + TabRegEvo["DxEvolucion2"].ToString() + "'," +
                                    "DiaHospi = '" + TabRegEvo["DiaHospi"].ToString() + "'," +
                                    "NumeroHoras = '" + TabRegEvo["NumeroHoras"].ToString() + "'," +
                                    "Pabellon = '" + TabRegEvo["Pabellon"].ToString() + "'," +
                                    "Cama = '" + TabRegEvo["Cama"].ToString() + "'," +
                                   $"FechaIngresoCama = {Conexion.ValidarFechaNula(TabRegEvo["FechaIngresoCama"].ToString())} " +
                                    "CuentaNUM = '" + TabRegEvo["CuentaNUM"].ToString() + "'," +
                                    "MediReg = '" + TabRegEvo["MediReg"].ToString() + "'," +
                                    "Activa = '" + TabRegEvo["Activa"].ToString() + "'," +
                                    "CodRegis = '" + TabRegEvo["CodRegis"].ToString() + "'," +
                                    $"FechaRegis = {Conexion.ValidarFechaNula(TabRegEvo["FechaRegis"].ToString())} " +          
                                    $"HorRegis = {Conexion.ValidarHoraNula(TabRegEvo["HorRegis"].ToString(), false)} " +
                                    "WHERE CodAtencion = N'" + CodRegiEvol + "' AND ItemREF = N'" + ItemEvoCen + "'";

                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);

                                }

                                TabRegEvoCen.Close();

                            }//USing

                        }//While
                        TabRegEvo.Close();
                        return 1;

                    }//if(TabRegEvo.HasRows == false)

              

                }//USing
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al cargar la funcion  RegistrodeevolucionesIMP " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private int AntecedentespacientesIMP(string CodHistAP)
        {
            try
            {
                string CodAntePaci, CodAntPacPort;
                string HisRegis, TipAnteRegis, SqlAntPac, SqlAntPacCen;

                SqlAntPac = "SELECT [Datos antecedentes pacientes].* ";
                SqlAntPac += "FROM [DACONEXTSQL].[dbo].[Datos antecedentes pacientes] ";
                SqlAntPac += "WHERE ([Datos antecedentes pacientes].CodAten = N'" + CodHistAP + "')";

                ConectarCentral();


                SqlDataReader TabAntPac, TabAntPacCen;

                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command2 = new SqlCommand(SqlAntPac, connection2);
                    command2.Connection.Open();
                    TabAntPac = command2.ExecuteReader();


                    if (TabAntPac.HasRows == false)
                    {
                        //'No hay registros en la tabla Datos antecedentes pacientes
                        return 0;
                    }
                    else
                    {
                        ConectarPortatil();

                        while (TabAntPac.Read())
                        {
                            //Revisamos si el número de codigo de atencion existe

                            HisRegis = "";
                            CodAntPacPort = "";
                            TipAnteRegis = "";
                            CodAntePaci = "";

                            HisRegis = TabAntPac["HistoNumero"].ToString();
                            CodAntPacPort = TabAntPac["CodigoAnteced"].ToString();
                            TipAnteRegis = TabAntPac["TipoAnteced"].ToString();
                            CodAntePaci = TabAntPac["CodAten"].ToString();

                            SqlAntPacCen = "SELECT * FROM [DACONEXTSQL].[dbo].[Datos antecedentes pacientes] ";
                            SqlAntPacCen += "WHERE (HistoNumero = N'" + HisRegis + "') AND (CodigoAnteced = N'" + CodAntPacPort + "') AND ";
                            SqlAntPacCen += "(TipoAnteced = N'" + TipAnteRegis + "') AND (CodAten = N'" + CodAntePaci + "')";


                            using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command = new SqlCommand(SqlAntPacCen, connection);
                                command.Connection.Open();
                                TabAntPacCen = command.ExecuteReader();

                                if (TabAntPacCen.HasRows == false)
                                {
                                    Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos antecedentes pacientes]  " +
                                    "(" +
                                    "HistoNumero," +
                                    "CodigoAnteced," +
                                    "TipoAnteced," +
                                    "CodAten," +
                                    "Observaciones," +
                                    "Cantidad," +
                                    "FechasAntecede," +
                                    "Incluido," +
                                    "CodRegis," +
                                    "FechaRegis," +
                                    "Horaregis" +
                                    ")" +
                                    "VALUES" +
                                    "(" +
                                    "'" + TabAntPac["HistoNumero"].ToString() + "'," +
                                    "'" + TabAntPac["CodigoAnteced"].ToString() + "'," +
                                    "'" + TabAntPac["TipoAnteced"].ToString() + "'," +
                                    "'" + TabAntPac["CodAten"].ToString() + "'," +
                                    "'" + TabAntPac["Observaciones"].ToString().Replace("'","") + "'," +
                                    "'" + TabAntPac["Cantidad"].ToString() + "'," +
                                     $"{Conexion.ValidarFechaNula(TabAntPac["FechasAntecede"].ToString())}" +
                                    "'" + TabAntPac["Incluido"].ToString() + "'," +
                                    "'" + TabAntPac["CodRegis"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabAntPac["FechaRegis"].ToString())}" +
                                    $"{Conexion.ValidarHoraNula(TabAntPac["Horaregis"].ToString(), false)}" +
                                    ")";

                                    Boolean Insert = Conexion.SqlInsert(Utils.SqlDatos);

                                }
                                else
                                {
                                    //Modifique los datos
                                    Utils.SqlDatos = "UPDATE [DACONEXTSQL].[dbo].[Datos antecedentes pacientes]  SET " +
                                    "CodigoAnteced = '" + TabAntPac["CodigoAnteced"].ToString() + "'," +
                                    "TipoAnteced = '" + TabAntPac["TipoAnteced"].ToString() + "'," +
                                    "Observaciones = '" + TabAntPac["Observaciones"].ToString().Replace("'", "") + "'," +
                                    "Cantidad = '" + TabAntPac["Cantidad"].ToString() + "'," +
                                    $"FechasAntecede = {Conexion.ValidarFechaNula(TabAntPac["FechasAntecede"].ToString())} " +
                                    "Incluido = '" + TabAntPac["Incluido"].ToString() + "'," +
                                    "CodRegis = '" + TabAntPac["CodRegis"].ToString() + "'," +
                                    $"FechaRegis = {Conexion.ValidarFechaNula(TabAntPac["FechaRegis"].ToString())} " +                              
                                    $"Horaregis = {Conexion.ValidarHoraNula(TabAntPac["Horaregis"].ToString(), false)}" +
                                    "WHERE (TipoAnteced = N'" + TipAnteRegis + "') AND (CodAten = '" + CodAntePaci + "')  AND  (HistoNumero = N'" + HisRegis + "') AND (CodigoAnteced = N'" + CodAntPacPort + "') ";

                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);

                                }

                            }//Using

                            TabAntPacCen.Close();

                        }//While

                        return 0;

                    }//if(TabAntPac.HasRows == false)

                }//Using

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al cargar la funcion AntecedentespacientesIMP " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private int DetalleatencionconsultaIMP(string CodHistAC)
        {
            try
            {
                string SqlDetAteCon, SqlDetAteConCen, CodDetAteCon, CodDetAteConPort;

                ConectarCentral();

                SqlDetAteCon = "SELECT [Datos detalle atencion de consulta].* ";
                SqlDetAteCon = SqlDetAteCon + "FROM [DACONEXTSQL].[dbo].[Datos detalle atencion de consulta] ";
                SqlDetAteCon = SqlDetAteCon + "WHERE ([Datos detalle atencion de consulta].CodigoAten = N'" + CodHistAC + "')";


                SqlDataReader TabDetAteCon, TabDetAteConCen;

                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command2 = new SqlCommand(SqlDetAteCon, connection2);
                    command2.Connection.Open();
                    TabDetAteCon = command2.ExecuteReader();

                    if (TabDetAteCon.HasRows == false)
                    {
                        //No hay registros en la tabla Datos atencion de la consulta
                        return 0;
                    }
                    else
                    {
                        ConectarPortatil();
                        while (TabDetAteCon.Read())
                        {
                            //'Revisamos si el número de codigo de atencion existe
                            CodDetAteCon = "";
                            CodDetAteConPort = "";
                            CodDetAteCon = TabDetAteCon["CodigoAten"].ToString();
                            CodDetAteConPort = TabDetAteCon["CodigoNodo"].ToString();



                            SqlDetAteConCen = "SELECT * FROM  [DACONEXTSQL].[dbo].[Datos detalle atencion de consulta] ";
                            SqlDetAteConCen = SqlDetAteConCen + "WHERE (CodigoAten = N'" + CodDetAteCon + "') AND (CodigoNodo = '" + CodDetAteConPort + "')";

                            using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command = new SqlCommand(SqlDetAteConCen, connection);
                                command.Connection.Open();
                                TabDetAteConCen = command.ExecuteReader();

                                if (TabDetAteConCen.HasRows == false)
                                {
                                    //Agregue
                                    Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos detalle atencion de consulta] " +
                                    "(" +
                                    "CodigoAten," +
                                    "CodigoNodo," +
                                    "DescripcionSIS," +
                                    "CodigoTExam," +
                                    "CodiMedi," +
                                    "FecRegis," +
                                    "Horaregis" +
                                    ")" +
                                    "VALUES" +
                                    "(" +
                                    "'" + TabDetAteCon["CodigoAten"].ToString() + "'," +
                                    "'" + TabDetAteCon["CodigoNodo"].ToString() + "'," +
                                    "'" + TabDetAteCon["DescripcionSIS"].ToString() + "'," +
                                    "'" + TabDetAteCon["CodigoTExam"].ToString() + "'," +
                                    "'" + TabDetAteCon["CodiMedi"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabDetAteCon["FecRegis"].ToString())}" +
                                    $"{Conexion.ValidarHoraNula(TabDetAteCon["Horaregis"].ToString(), false)}" +
                                    ")";

                                    Boolean Insert = Conexion.SqlInsert(Utils.SqlDatos);

                                }
                                else
                                {
                                    //Modifique los datos
                                    Utils.SqlDatos = "UPDATE [DACONEXTSQL].[dbo].[Datos detalle atencion de consulta] SET " +
                                    "DescripcionSIS = '" + TabDetAteCon["DescripcionSIS"].ToString() + "'," +
                                    "CodigoTExam = '" + TabDetAteCon["CodigoTExam"].ToString() + "'," +
                                    "CodiMedi = '" + TabDetAteCon["CodiMedi"].ToString() + "'," +
                                    $"FecRegis = {Conexion.ValidarFechaNula(TabDetAteCon["FecRegis"].ToString())} " +              
                                    $"Horaregis = {Conexion.ValidarHoraNula(TabDetAteCon["Horaregis"].ToString(), false)}" +
                                    "WHERE (CodigoAten = N'" + CodDetAteCon + "') AND (CodigoNodo = '" + CodDetAteConPort + "') ";

                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);
                                }

                            }

                            TabDetAteConCen.Close();
                        }

                        TabDetAteCon.Close();
                        return 1;
                    }
                }

            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al cargar la funcion DetalleatencionconsultaIMP " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }
        #endregion

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
        } //Carga las fechas desde del el 01 del mes anterioir hasta que se acabe

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

        int GlobalCantiIngre = 0, GlobalCantiAct = 0;
        private void FrmImportSedeCentralHistorias_Load(object sender, EventArgs e)
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
                Utils.Informa += "al cargar el formulario  FrmImportSedeCentralHistorias " + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscarPacientes_Click(object sender, EventArgs e)
        {
            try
            {

                if (ImportarHistorias.IsBusy != true) //Si el proceso esta corriendo no puede voler a iniciarse 
                {

                    DateTime Fecha2 = DateTime.Now;
                    string Fecha = Fecha2.ToString("yyyy-MM-dd");

    
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
                    Utils.Informa += "todas las historias clinicas en la instancia del" + "\r";
                    Utils.Informa += "servidor central a la instancia del portatil.?" + "\r";
                    Utils.Informa += "Fecha Inicial: " + FecIniPro + "\r";
                    Utils.Informa += "Fecha Final: " + FecFinPro;

                    var res = MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                    if (res == DialogResult.Yes)
                    {
                        TxtCanHistFor.Text = "0";
                        TxtCanhisFormExis.Text = "0";
                        GlobalCantiAct = 0;
                        GlobalCantiIngre = 0;

                        ConectarCentral();

                        //SqlHistoCli = "SELECT * FROM [DACONEXTSQL].[dbo].[Datos atencion de la consulta] ";
                        //SqlHistoCli += "WHERE ([Datos atencion de la consulta].Activa = 'False' ) AND ";
                        //SqlHistoCli += "([Datos atencion de la consulta].FecAtenc >= CONVERT(DATETIME, '" + FecIniPro + "', 102)) AND";
                        //SqlHistoCli += "([Datos atencion de la consulta].FecAtenc <= CONVERT(DATETIME, '" + FecFinPro + "', 102))";

                        string SqlHistoCliCount = "SELECT count(*) as totalAten FROM [DACONEXTSQL].[dbo].[Datos atencion de la consulta] ";
                        SqlHistoCliCount += "WHERE ([Datos atencion de la consulta].PrefiHis = N'" + PfiCen + "') AND ([Datos atencion de la consulta].Activa = 'False' ) AND ";
                        SqlHistoCliCount += "([Datos atencion de la consulta].FecAtenc >= CONVERT(DATETIME, '" + FecIniPro + "', 102)) AND";
                        SqlHistoCliCount += "([Datos atencion de la consulta].FecAtenc <= CONVERT(DATETIME, '" + FecFinPro + "', 102))";


                        SqlDataReader reader = Conexion.SQLDataReader(SqlHistoCliCount);

                        int totalAten = 0;

                        if (reader.HasRows)
                        {
                            reader.Read();
                            totalAten = Convert.ToInt32(reader["totalAten"]);

                            if (totalAten != 0)
                            {

                                GlobalCantiAct = 0;
                                GlobalCantiIngre = 0;

                                LblDetener.Visible = true;
                                BtnDetener.Visible = true;

                                LblImportar.Visible = false;
                                BtnBuscarPacientes.Visible = false;

                                BarraExportHistorias.Minimum = 1;
                                BarraExportHistorias.Maximum = totalAten;
                                LblTotal.Text = totalAten.ToString();
                                ImportarHistorias.RunWorkerAsync();
                            }
                            else
                            {
                                Utils.Informa = "Lo siento pero en el rango de fecha" + "\r";
                                Utils.Informa += "digitado no existen datos para exportar, " + "\r";
                                Utils.Informa += "atenciones de consultas.";
                                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                BarraExportHistorias.Minimum = 0;
                                BarraExportHistorias.Maximum = 1;
                                BarraExportHistorias.Value = 0;
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
                Utils.Informa += "después de hacer click sobre el botón importar" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                BarraExportHistorias.Minimum = 0;
                BarraExportHistorias.Maximum = 1;
                BarraExportHistorias.Value = 0;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImportarHistorias_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                GlobalCantiIngre = 0;
                GlobalCantiAct = 0;


                string UsaRegis = "", SqlHistoCli = "", SqlHistCen = "", CodBusAten, SqlAnexPor = "", NumUniAnexa = "", HistoPaci = "", SqlAnexCen = "";

                DateTime Fecha2 = DateTime.Now;
                string Fecha = Fecha2.ToString("yyyy-MM-dd");
                int FunDetAntCon = 0, FunAntDePac = 0, FunAntPac = 0, FunRegEvo = 0, FunDetObsDoc = 0, FunDetEscAbre = 0, FunRegHtaDiabe = 0, FunTratamiento = 0, FunSegControl = 0, FunRemision = 0;

                string FecIniPro = Convert.ToString(DateInicial.Value.ToString("yyyy-MM-dd"));
                string FecFinPro = Convert.ToString(DateFinal.Value.ToString("yyyy-MM-dd"));

                string PfiCen = TxtPrefiCenFor.Text;
                string PfiPor = TxtPrefiPorFor.Text;


                UsaRegis = lblCodigoUser.Text;
                Utils.Titulo01 = "Control para exportar datos";

                ConectarCentral();

                SqlHistoCli = "SELECT * FROM [DACONEXTSQL].[dbo].[Datos atencion de la consulta] ";
                SqlHistoCli += "WHERE ([Datos atencion de la consulta].PrefiHis = N'" + PfiCen + "') AND";
                SqlHistoCli += "([Datos atencion de la consulta].Activa = 'False' ) AND ";
                SqlHistoCli += "([Datos atencion de la consulta].FecAtenc >= CONVERT(DATETIME, '" + FecIniPro + "', 102)) AND";
                SqlHistoCli += "([Datos atencion de la consulta].FecAtenc <= CONVERT(DATETIME, '" + FecFinPro + "', 102))";

                SqlDataReader TabHistoCli;

                using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                {
                    SqlCommand command = new SqlCommand(SqlHistoCli, connection);
                    command.Connection.Open();
                    TabHistoCli = command.ExecuteReader();


                    if (TabHistoCli.HasRows == false)
                    {
                        Utils.Informa = "Lo siento pero en el rango de fecha" + "\r";
                        Utils.Informa += "digitado no existen datos para exportar, " + "\r";
                        Utils.Informa += "atenciones de consultas.";
                        MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        int contador = 0;

                        while (TabHistoCli.Read())
                        {
                            

                            if (ImportarHistorias.CancellationPending == true)
                            {
                                e.Cancel = true;
                                Utils.Titulo01 = "Control de ejecución";
                                Utils.Informa = "Se cancelo la operacion " + "\r";
                                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }


                            contador += 1;
                            // 'Revisamos si el número de codigo de atencion existe

                            CodBusAten = TabHistoCli["CodConExt"].ToString();
                            HistoPaci = TabHistoCli["HistoriaNum"].ToString();

                            SqlHistCen = "SELECT * FROM [DACONEXTSQL].[dbo].[Datos atencion de la consulta] ";
                            SqlHistCen += "Where CodConExt = '" + CodBusAten + "'";

                            ConectarPortatil();


                            SqlDataReader TabHistorCen;

                            using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                            {
                                SqlCommand command2 = new SqlCommand(SqlHistCen, connection2);
                                command2.Connection.Open();
                                TabHistorCen = command2.ExecuteReader();


                                if (TabHistorCen.HasRows == false)
                                {
                                    //Agregue

                                    Utils.SqlDatos = "INSERT INTO [DACONEXTSQL].[dbo].[Datos atencion de la consulta] " +
                                    "(" +
                                    "CodConExt," +
                                    "TipoAten," +
                                    "NumCuenta," +
                                    "HistoriaNum," +
                                    "FecAtenc," +
                                    "HoraInicio," +
                                    "Dxprinc," +
                                    "DxEntra," +
                                    "DxMuer," +
                                    "DxRelac01," +
                                    "DxRelac02," +
                                    "DxRelac03," +
                                    "DxRelac04," +
                                    "ImpeDX," +
                                    "TipoDxPrin," +
                                    "UnidadEdad," +
                                    "ValorEdad," +
                                    "EdadMeses," +
                                    "CodMediIngresa," +
                                    "CodiMedi," +
                                    "CodEspeci," +
                                    "CodiMediSalida," +
                                    "CodEspeciSalida," +
                                    "FormAten," +
                                    "ImagAten," +
                                    "LaboAten," +
                                    "ProAten," +
                                    "TuvoControl," +
                                    "TipoControl," +
                                    "TuvoProcedimiento," +
                                    "Fechallega," +
                                    "HoraLlega," +
                                    "CodEstado," +
                                    "MedioLlegada," +
                                    "CualMedio," +
                                    "NomAcomp," +
                                    "CodParen," +
                                    "DirParen," +
                                    "TelParen," +
                                    "FechaOcurre," +
                                    "HoraOcurre," +
                                    "CausaBase," +
                                    "SitOcurre," +
                                    "MotConsul," +
                                    "HistEnfActual," +
                                    "TensionSisto," +
                                    "TensionDiasto," +
                                    "FrecuCardi," +
                                    "FrecuRespi," +
                                    "temperatura," +
                                    "PesoAMB," +
                                    "TallaAMB," +
                                    "IMC," +
                                    "CatIMC," +
                                    "PerimetroCefalico," +
                                    "SPO2," +
                                    "CabezaCuello," +
                                    "Endocrino," +
                                    "CardioPulmonar," +
                                    "Cardiovascular," +
                                    "Abdomen," +
                                    "GenitoUrinario," +
                                    "Neurologico," +
                                    "Extremidades," +
                                    "PielyFaneras," +
                                    "OsteoMuscu," +
                                    "GlasGow," +
                                    "Glaswog01," +
                                    "Glaswog02," +
                                    "Glaswog03," +
                                    "EstadoGral," +
                                    "CabezaCuelloFisi," +
                                    "OcularFisi," +
                                    "ORLFisi," +
                                    "DorsalLumbarFisi," +
                                    "AbdomenFisi," +
                                    "ExtremidadesFisi," +
                                    "ToraxFisico," +
                                    "CardioVascuFisi," +
                                    "PielSub," +
                                    "ExaGineco," +
                                    "TactoRectal," +
                                    "ExaMamas," +
                                    "AparLocomo," +
                                    "AparGanglio," +
                                    "NeuroFisico," +
                                    "pqsiquiatrico," +
                                    "Conducta," +
                                    "CodiColumna," +
                                    "PosTPlan," +
                                    "Remision," +
                                    "RemiNum," +
                                    "DestinoUsuario," +
                                    "Activa," +
                                    "ActivaInicialUrgencias," +
                                    "ExamenesAuxiliares," +
                                    "EpicrisisActiva," +
                                    "EpicrisisCerr," +
                                    "ValoraH," +
                                    "CodiMediValor," +
                                    "FechaValora," +
                                    "HoraLlegaValora," +
                                    "DetalleValora," +
                                    "Soporte," +
                                    "OtAnteceCE," +
                                    "IngresoEPI," +
                                    "EvolucionEPI," +
                                    "EgresoEPI," +
                                    "CodiMediEpi," +
                                    "FechaEpi," +
                                    "HoraEpi," +
                                    "CodColor," +
                                    "NumeroCitas," +
                                    "Horaregis," +
                                    "HoraAtencion," +
                                    "CodiRegis," +
                                    "FecRegis," +
                                    "CodiModi," +
                                    "FecModi," +
                                    "ResumenEvoEPI," +
                                    "PrefiHis" +
                                    ")" +
                                    "VALUES" +
                                    "(" +
                                    "'" + TabHistoCli["CodConExt"].ToString() + "'," +
                                    "'" + TabHistoCli["TipoAten"].ToString() + "'," +
                                    "'" + TabHistoCli["NumCuenta"].ToString() + "'," +
                                    "'" + TabHistoCli["HistoriaNum"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabHistoCli["FecAtenc"].ToString())}" +
                                    $"{Conexion.ValidarHoraNula(TabHistoCli["HoraInicio"].ToString())}" +
                                    "'" + TabHistoCli["Dxprinc"].ToString() + "'," +
                                    "'" + TabHistoCli["DxEntra"].ToString() + "'," +
                                    "'" + TabHistoCli["DxMuer"].ToString() + "'," +
                                    "'" + TabHistoCli["DxRelac01"].ToString() + "'," +
                                    "'" + TabHistoCli["DxRelac02"].ToString() + "'," +
                                    "'" + TabHistoCli["DxRelac03"].ToString() + "'," +
                                    "'" + TabHistoCli["DxRelac04"].ToString() + "'," +
                                    "'" + TabHistoCli["ImpeDX"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["TipoDxPrin"].ToString() + "'," +
                                    "'" + TabHistoCli["UnidadEdad"].ToString() + "'," +
                                    "'" + TabHistoCli["ValorEdad"].ToString() + "'," +
                                    "'" + TabHistoCli["EdadMeses"].ToString() + "'," +
                                    "'" + TabHistoCli["CodMediIngresa"].ToString() + "'," +
                                    "'" + TabHistoCli["CodiMedi"].ToString() + "'," +
                                    "'" + TabHistoCli["CodEspeci"].ToString() + "'," +
                                    "'" + TabHistoCli["CodiMediSalida"].ToString() + "'," +
                                    "'" + TabHistoCli["CodEspeciSalida"].ToString() + "'," +
                                    "'" + TabHistoCli["FormAten"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["ImagAten"].ToString() + "'," +
                                    "'" + TabHistoCli["LaboAten"].ToString() + "'," +
                                    "'" + TabHistoCli["ProAten"].ToString() + "'," +
                                    "'" + TabHistoCli["TuvoControl"].ToString() + "'," +
                                    "'" + TabHistoCli["TipoControl"].ToString() + "'," +
                                    "'" + TabHistoCli["TuvoProcedimiento"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabHistoCli["Fechallega"].ToString())}" +
                                    $"{Conexion.ValidarHoraNula(TabHistoCli["HoraLlega"].ToString())}" +
                                    "'" + TabHistoCli["CodEstado"].ToString() + "'," +
                                    "'" + TabHistoCli["MedioLlegada"].ToString() + "'," +
                                    "'" + TabHistoCli["CualMedio"].ToString() + "'," +
                                    "'" + TabHistoCli["NomAcomp"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["CodParen"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["DirParen"].ToString() + "'," +
                                    "'" + TabHistoCli["TelParen"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabHistoCli["FechaOcurre"].ToString())}" +
                                    $"{Conexion.ValidarHoraNula(TabHistoCli["HoraOcurre"].ToString())}" +
                                    "'" + TabHistoCli["CausaBase"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["SitOcurre"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["MotConsul"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["HistEnfActual"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["TensionSisto"].ToString() + "'," +
                                    "'" + TabHistoCli["TensionDiasto"].ToString() + "'," +
                                    "'" + TabHistoCli["FrecuCardi"].ToString() + "'," +
                                    "'" + TabHistoCli["FrecuRespi"].ToString() + "'," +
                                    "'" + TabHistoCli["temperatura"].ToString() + "'," +
                                    "'" + TabHistoCli["PesoAMB"].ToString() + "'," +
                                    "'" + TabHistoCli["TallaAMB"].ToString() + "'," +
                                    "'" + TabHistoCli["IMC"].ToString() + "'," +
                                    "'" + TabHistoCli["CatIMC"].ToString() + "'," +
                                    "'" + TabHistoCli["PerimetroCefalico"].ToString() + "'," +
                                    "'" + TabHistoCli["SPO2"].ToString() + "'," +
                                    "'" + TabHistoCli["CabezaCuello"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["Endocrino"].ToString() + "'," +
                                    "'" + TabHistoCli["CardioPulmonar"].ToString() + "'," +
                                    "'" + TabHistoCli["Cardiovascular"].ToString() + "'," +
                                    "'" + TabHistoCli["Abdomen"].ToString() + "'," +
                                    "'" + TabHistoCli["GenitoUrinario"].ToString() + "'," +
                                    "'" + TabHistoCli["Neurologico"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["Extremidades"].ToString() + "'," +
                                    "'" + TabHistoCli["PielyFaneras"].ToString() + "'," +
                                    "'" + TabHistoCli["OsteoMuscu"].ToString() + "'," +
                                    "'" + TabHistoCli["GlasGow"].ToString() + "'," +
                                    "'" + TabHistoCli["Glaswog01"].ToString() + "'," +
                                    "'" + TabHistoCli["Glaswog02"].ToString() + "'," +
                                    "'" + TabHistoCli["Glaswog03"].ToString() + "'," +
                                    "'" + TabHistoCli["EstadoGral"].ToString() + "'," +
                                    "'" + TabHistoCli["CabezaCuelloFisi"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["OcularFisi"].ToString() + "'," +
                                    "'" + TabHistoCli["ORLFisi"].ToString() + "'," +
                                    "'" + TabHistoCli["DorsalLumbarFisi"].ToString() + "'," +
                                    "'" + TabHistoCli["AbdomenFisi"].ToString() + "'," +
                                    "'" + TabHistoCli["ExtremidadesFisi"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["ToraxFisico"].ToString() + "'," +
                                    "'" + TabHistoCli["CardioVascuFisi"].ToString() + "'," +
                                    "'" + TabHistoCli["PielSub"].ToString() + "'," +
                                    "'" + TabHistoCli["ExaGineco"].ToString() + "'," +
                                    "'" + TabHistoCli["TactoRectal"].ToString() + "'," +
                                    "'" + TabHistoCli["ExaMamas"].ToString() + "'," +
                                    "'" + TabHistoCli["AparLocomo"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["AparGanglio"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["NeuroFisico"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["pqsiquiatrico"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["Conducta"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["CodiColumna"].ToString() + "'," +
                                    "'" + TabHistoCli["PosTPlan"].ToString() + "'," +
                                    "'" + TabHistoCli["Remision"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["RemiNum"].ToString() + "'," +
                                    "'" + TabHistoCli["DestinoUsuario"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["Activa"].ToString() + "'," +
                                    "'" + TabHistoCli["ActivaInicialUrgencias"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["ExamenesAuxiliares"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["EpicrisisActiva"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["EpicrisisCerr"].ToString() + "'," +
                                    "'" + TabHistoCli["ValoraH"].ToString() + "'," +
                                    "'" + TabHistoCli["CodiMediValor"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabHistoCli["FechaValora"].ToString())}" +
                                    $"{Conexion.ValidarHoraNula(TabHistoCli["HoraLlegaValora"].ToString())}" +
                                    "'" + TabHistoCli["DetalleValora"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["Soporte"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["OtAnteceCE"].ToString().Replace("'", "''") + "'," +

                                    //********** Campos que no aparecen en la E.S.E de san agustin
                                    //TabHistorCen!OservaMedica = TabHistoCli!OservaMedica
                                    //TabHistorCen!ObsevaLabora = TabHistoCli!ObsevaLabora
                                    //TabHistorCen!ObservaImagen = TabHistoCli!ObservaImagen
                                    //'**********
                                    "'" + TabHistoCli["IngresoEPI"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["EvolucionEPI"].ToString().Replace("'", "''") + "'," +
                                    "'" + TabHistoCli["EgresoEPI"].ToString() + "'," +
                                    "'" + TabHistoCli["CodiMediEpi"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabHistoCli["FechaEpi"].ToString())}" +
                                    $"{Conexion.ValidarHoraNula(TabHistoCli["HoraEpi"].ToString())}" +
                                    "'" + TabHistoCli["CodColor"].ToString() + "'," +
                                    "'" + TabHistoCli["NumeroCitas"].ToString() + "'," +
                                    $"{Conexion.ValidarHoraNula(TabHistoCli["Horaregis"].ToString())}" +
                                    $"{Conexion.ValidarHoraNula(TabHistoCli["HoraAtencion"].ToString())}" +
                                    "'" + TabHistoCli["CodiRegis"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabHistoCli["FecRegis"].ToString())}" +
                                    "'" + TabHistoCli["CodiModi"].ToString() + "'," +
                                    $"{Conexion.ValidarFechaNula(TabHistoCli["FecModi"].ToString())}" +

                                    //TabHistorCen!SintRespiratorioNO = TabHistoCli!SintRespiratorioNO
                                    //TabHistorCen!SintPielNO = TabHistoCli!SintPielNO
                                    //TabHistorCen!SintSNNO = TabHistoCli!SintSNNO
                                    //'********** Campos que no aparecen en la E.S.E de san agustin
                                    //TabHistorCen!ProcediQxOxEPI = TabHistoCli!ProcediQxOxEPI
                                    //TabHistorCen!TratamientosEPI = TabHistoCli!TratamientosEPI
                                    //TabHistorCen!ResumenEPI = TabHistoCli!ResumenEPI
                                    //'**********
                                    "'" + TabHistoCli["ResumenEvoEPI"].ToString().Replace("'", "''") + "'," +

                                    //'********** Campos que no aparecen en la E.S.E de san agustin
                                    //TabHistorCen!ComplicacionesEPI = TabHistoCli!ComplicacionesEPI
                                    //TabHistorCen!CondicionesEPI = TabHistoCli!CondicionesEPI
                                    //TabHistorCen!PronosticoEPI = TabHistoCli!PronosticoEPI
                                    //TabHistorCen!RecomendacionesEPI = TabHistoCli!RecomendacionesEPI
                                    //'**********

                                    "'" + TabHistoCli["PrefiHis"].ToString() + "'" +
                                    ")";

                                    Boolean Insert = Conexion.SqlInsert(Utils.SqlDatos);

                                    FunDetAntCon = DetalleatencionconsultaIMP(CodBusAten);
                                    FunAntPac = AntecedentespacientesIMP(CodBusAten);
                                    FunRegEvo = RegistrodeevolucionesIMP(CodBusAten);
                                    FunDetObsDoc = DetallesdeobservacionespordocumentoIMP(CodBusAten);
                                    FunDetEscAbre = DetalleescalaabreviadaIMP(CodBusAten);
                                    FunRegHtaDiabe = RegistroHtaDiabeticosIMP(CodBusAten);
                                    FunTratamiento = TratamientosIMP(HistoPaci, CodBusAten);
                                    FunSegControl = SeguimientodecontrolesIMP(CodBusAten);//AQUI  
                                    FunRemision = RemisionesIMP(CodBusAten);

                                    GlobalCantiIngre += 1;

                                }
                                else
                                {
                                    //Modifique los datos
                                    Utils.SqlDatos = "UPDATE [DACONEXTSQL].[dbo].[Datos atencion de la consulta] SET " +
                                    "Dxprinc = '" + TabHistoCli["Dxprinc"].ToString() + "'," +
                                    "DxEntra = '" + TabHistoCli["DxEntra"].ToString() + "'," +
                                    "DxMuer = '" + TabHistoCli["DxMuer"].ToString() + "'," +
                                    "DxRelac01 = '" + TabHistoCli["DxRelac01"].ToString() + "'," +
                                    "DxRelac02 = '" + TabHistoCli["DxRelac02"].ToString() + "'," +
                                    "DxRelac03 = '" + TabHistoCli["DxRelac03"].ToString() + "'," +
                                    "DxRelac04 = '" + TabHistoCli["DxRelac04"].ToString() + "'," +
                                    "ImpeDX = '" + TabHistoCli["ImpeDX"].ToString().Replace("'", "''") + "'," +
                                    "TipoDxPrin = '" + TabHistoCli["TipoDxPrin"].ToString() + "'," +
                                    "UnidadEdad = '" + TabHistoCli["UnidadEdad"].ToString() + "'," +
                                    "ValorEdad = '" + TabHistoCli["ValorEdad"].ToString() + "'," +
                                    "EdadMeses = '" + TabHistoCli["EdadMeses"].ToString() + "'," +
                                    "CodMediIngresa = '" + TabHistoCli["CodMediIngresa"].ToString() + "'," +
                                    "CodiMedi = '" + TabHistoCli["CodiMedi"].ToString() + "'," +
                                    "CodEspeci = '" + TabHistoCli["CodEspeci"].ToString() + "'," +
                                    "CodiMediSalida = '" + TabHistoCli["CodiMediSalida"].ToString() + "'," +
                                    "CodEspeciSalida = '" + TabHistoCli["CodEspeciSalida"].ToString() + "'," +
                                    "FormAten = '" + TabHistoCli["FormAten"].ToString().Replace("'", "''") + "'," +
                                    "ImagAten = '" + TabHistoCli["ImagAten"].ToString() + "'," +
                                    "LaboAten = '" + TabHistoCli["LaboAten"].ToString() + "'," +
                                    "ProAten = '" + TabHistoCli["ProAten"].ToString() + "'," +
                                    "TuvoControl = '" + TabHistoCli["TuvoControl"].ToString() + "'," +
                                    "TipoControl = '" + TabHistoCli["TipoControl"].ToString() + "'," +
                                    "TuvoProcedimiento = '" + TabHistoCli["TuvoProcedimiento"].ToString() + "'," +
                                    $"Fechallega = {Conexion.ValidarFechaNula(TabHistoCli["Fechallega"].ToString())}" +
                                    $"HoraLlega = {Conexion.ValidarHoraNula(TabHistoCli["HoraLlega"].ToString())}" +
                                    "CodEstado = '" + TabHistoCli["CodEstado"].ToString() + "'," +
                                    "MedioLlegada = '" + TabHistoCli["MedioLlegada"].ToString() + "'," +
                                    "CualMedio = '" + TabHistoCli["CualMedio"].ToString() + "'," +
                                    "NomAcomp = '" + TabHistoCli["NomAcomp"].ToString().Replace("'", "''") + "'," +
                                    "CodParen = '" + TabHistoCli["CodParen"].ToString().Replace("'", "''") + "'," +
                                    "DirParen = '" + TabHistoCli["DirParen"].ToString() + "'," +
                                    "TelParen = '" + TabHistoCli["TelParen"].ToString() + "'," +
                                    $"FechaOcurre = {Conexion.ValidarFechaNula(TabHistoCli["FechaOcurre"].ToString())}" +
                                    $"HoraOcurre = {Conexion.ValidarHoraNula(TabHistoCli["HoraOcurre"].ToString())}" +
                                    "CausaBase = '" + TabHistoCli["CausaBase"].ToString().Replace("'", "''") + "'," +
                                    "SitOcurre = '" + TabHistoCli["SitOcurre"].ToString().Replace("'", "''") + "'," +
                                    "MotConsul = '" + TabHistoCli["MotConsul"].ToString().Replace("'", "") + "'," +
                                    "HistEnfActual = '" + TabHistoCli["HistEnfActual"].ToString().Replace("'", "''") + "'," +
                                    "TensionSisto = '" + TabHistoCli["TensionSisto"].ToString() + "'," +
                                    "TensionDiasto = '" + TabHistoCli["TensionDiasto"].ToString() + "'," +
                                    "FrecuCardi = '" + TabHistoCli["FrecuCardi"].ToString() + "'," +
                                    "FrecuRespi = '" + TabHistoCli["FrecuRespi"].ToString() + "'," +
                                    "temperatura = '" + TabHistoCli["temperatura"].ToString() + "'," +
                                    "PesoAMB = '" + TabHistoCli["PesoAMB"].ToString() + "'," +
                                    "TallaAMB = '" + TabHistoCli["TallaAMB"].ToString() + "'," +
                                    "IMC = '" + TabHistoCli["IMC"].ToString() + "'," +
                                    "CatIMC = '" + TabHistoCli["CatIMC"].ToString() + "'," +
                                    "PerimetroCefalico = '" + TabHistoCli["PerimetroCefalico"].ToString() + "'," +
                                    "SPO2 = '" + TabHistoCli["SPO2"].ToString() + "'," +
                                    "CabezaCuello = '" + TabHistoCli["CabezaCuello"].ToString().Replace("'", "''") + "'," +
                                    "Endocrino = '" + TabHistoCli["Endocrino"].ToString() + "'," +
                                    "CardioPulmonar = '" + TabHistoCli["CardioPulmonar"].ToString() + "'," +
                                    "Cardiovascular = '" + TabHistoCli["Cardiovascular"].ToString() + "'," +
                                    "Abdomen = '" + TabHistoCli["Abdomen"].ToString().Replace("'", "''") + "'," +
                                    "GenitoUrinario = '" + TabHistoCli["GenitoUrinario"].ToString() + "'," +
                                    "Neurologico = '" + TabHistoCli["Neurologico"].ToString() + "'," +
                                    "Extremidades = '" + TabHistoCli["Extremidades"].ToString() + "'," +
                                    "PielyFaneras = '" + TabHistoCli["PielyFaneras"].ToString() + "'," +
                                    "OsteoMuscu = '" + TabHistoCli["OsteoMuscu"].ToString() + "'," +
                                    "GlasGow = '" + TabHistoCli["GlasGow"].ToString() + "'," +
                                    "Glaswog01 = '" + TabHistoCli["Glaswog01"].ToString() + "'," +
                                    "Glaswog02 = '" + TabHistoCli["Glaswog02"].ToString() + "'," +
                                    "Glaswog03 = '" + TabHistoCli["Glaswog03"].ToString() + "'," +
                                    "EstadoGral = '" + TabHistoCli["EstadoGral"].ToString() + "'," +
                                    "CabezaCuelloFisi = '" + TabHistoCli["CabezaCuelloFisi"].ToString() + "'," +
                                    "OcularFisi = '" + TabHistoCli["OcularFisi"].ToString() + "'," +
                                    "ORLFisi = '" + TabHistoCli["ORLFisi"].ToString() + "'," +
                                    "DorsalLumbarFisi = '" + TabHistoCli["DorsalLumbarFisi"].ToString() + "'," +
                                    "AbdomenFisi = '" + TabHistoCli["AbdomenFisi"].ToString() + "'," +
                                    "ExtremidadesFisi = '" + TabHistoCli["ExtremidadesFisi"].ToString() + "'," +
                                    "ToraxFisico = '" + TabHistoCli["ToraxFisico"].ToString() + "'," +
                                    "CardioVascuFisi = '" + TabHistoCli["CardioVascuFisi"].ToString() + "'," +
                                    "PielSub = '" + TabHistoCli["PielSub"].ToString() + "'," +
                                    "ExaGineco = '" + TabHistoCli["ExaGineco"].ToString() + "'," +
                                    "TactoRectal = '" + TabHistoCli["TactoRectal"].ToString() + "'," +
                                    "ExaMamas = '" + TabHistoCli["ExaMamas"].ToString() + "'," +
                                    "AparLocomo = '" + TabHistoCli["AparLocomo"].ToString() + "'," +
                                    "AparGanglio = '" + TabHistoCli["AparGanglio"].ToString() + "'," +
                                    "NeuroFisico = '" + TabHistoCli["NeuroFisico"].ToString().Replace("'", "''") + "'," +
                                    "pqsiquiatrico = '" + TabHistoCli["pqsiquiatrico"].ToString().Replace("'", "''") + "'," +
                                    "Conducta = '" + TabHistoCli["Conducta"].ToString().Replace("'", "''") + "'," +
                                    "CodiColumna = '" + TabHistoCli["CodiColumna"].ToString() + "'," +
                                    "PosTPlan = '" + TabHistoCli["PosTPlan"].ToString() + "'," +
                                    "Remision = '" + TabHistoCli["Remision"].ToString() + "'," +
                                    "RemiNum = '" + TabHistoCli["RemiNum"].ToString() + "'," +
                                    "DestinoUsuario = '" + TabHistoCli["DestinoUsuario"].ToString() + "'," +
                                    "Activa = '" + TabHistoCli["Activa"].ToString() + "'," +
                                    "ActivaInicialUrgencias = '" + TabHistoCli["ActivaInicialUrgencias"].ToString().Replace("'", "''") + "'," +
                                    "ExamenesAuxiliares = '" + TabHistoCli["ExamenesAuxiliares"].ToString().Replace("'", "''") + "'," +
                                    "EpicrisisActiva = '" + TabHistoCli["EpicrisisActiva"].ToString() + "'," +
                                    "EpicrisisCerr = '" + TabHistoCli["EpicrisisCerr"].ToString() + "'," +
                                    "ValoraH = '" + TabHistoCli["ValoraH"].ToString().Replace("'", "''") + "'," +
                                    "CodiMediValor = '" + TabHistoCli["CodiMediValor"].ToString() + "'," +
                                    $"FechaValora = {Conexion.ValidarFechaNula(TabHistoCli["FechaValora"].ToString())}" +
                                    $"HoraLlegaValora = {Conexion.ValidarHoraNula(TabHistoCli["HoraLlegaValora"].ToString())}" +
                                    "DetalleValora = '" + TabHistoCli["DetalleValora"].ToString().Replace("'", "''") + "'," +
                                    "Soporte = '" + TabHistoCli["Soporte"].ToString().Replace("'", "''") + "'," +
                                    "OtAnteceCE = '" + TabHistoCli["OtAnteceCE"].ToString().Replace("'", "''") + "'," +
                                    "IngresoEPI = '" + TabHistoCli["IngresoEPI"].ToString().Replace("'", "''") + "'," +
                                    "EvolucionEPI = '" + TabHistoCli["EvolucionEPI"].ToString().Replace("'", "''") + "'," +
                                    "EgresoEPI = '" + TabHistoCli["EgresoEPI"].ToString() + "'," +
                                    "CodiMediEpi = '" + TabHistoCli["CodiMediEpi"].ToString() + "'," +
                                    $"FechaEpi = {Conexion.ValidarFechaNula(TabHistoCli["FechaEpi"].ToString())}" +
                                    $"HoraEpi = {Conexion.ValidarHoraNula(TabHistoCli["HoraEpi"].ToString())}" +
                                    "CodColor = '" + TabHistoCli["CodColor"].ToString() + "'," +
                                    "NumeroCitas = '" + TabHistoCli["NumeroCitas"].ToString() + "'," +
                                    $"Horaregis = {Conexion.ValidarHoraNula(TabHistoCli["Horaregis"].ToString())}" +
                                    $"HoraAtencion = {Conexion.ValidarHoraNula(TabHistoCli["HoraAtencion"].ToString())}" +
                                    "CodiRegis = '" + TabHistoCli["CodiRegis"].ToString() + "'," +
                                    $"FecRegis = {Conexion.ValidarFechaNula(TabHistoCli["FecRegis"].ToString())}" +
                                    "CodiModi = '" + TabHistoCli["CodiModi"].ToString() + "'," +
                                    $"FecModi = {Conexion.ValidarFechaNula(TabHistoCli["FecModi"].ToString())}" +
                                    //TabHistorCen!SintRespiratorioNO = TabHistoCli!SintRespiratorioNO
                                    //TabHistorCen!SintPielNO = TabHistoCli!SintPielNO
                                    //TabHistorCen!SintSNNO = TabHistoCli!SintSNNO


                                    //'********** Campos que no aparecen en la E.S.E de san agustin
                                    //TabHistorCen!ProcediQxOxEPI = TabHistoCli!ProcediQxOxEPI
                                    //TabHistorCen!TratamientosEPI = TabHistoCli!TratamientosEPI
                                    //TabHistorCen!ResumenEPI = TabHistoCli!ResumenEPI
                                    //'**********
                                    "ResumenEvoEPI = '" + TabHistoCli["ResumenEvoEPI"].ToString().Replace("'", "''") + "'," +
                                    "PrefiHis = '" + TabHistoCli["PrefiHis"].ToString() + "' " +
                                    "WHERE CodConExt = '" + CodBusAten + "'";


                                    Boolean Act = Conexion.SQLUpdate(Utils.SqlDatos);

                               

                                    FunDetAntCon = DetalleatencionconsultaIMP(CodBusAten);
                                    FunAntPac = AntecedentespacientesIMP(CodBusAten);
                                    FunRegEvo = RegistrodeevolucionesIMP(CodBusAten);
                                    FunDetObsDoc = DetallesdeobservacionespordocumentoIMP(CodBusAten);
                                    FunDetEscAbre = DetalleescalaabreviadaIMP(CodBusAten);
                                    FunRegHtaDiabe = RegistroHtaDiabeticosIMP(CodBusAten);
                                    FunTratamiento = TratamientosIMP(HistoPaci, CodBusAten);
                                    FunSegControl = SeguimientodecontrolesIMP(CodBusAten);
                                    FunRemision = RemisionesIMP(CodBusAten);

                                    GlobalCantiAct += 1;

                                }//Final de TabHistorCen.BOF

                                TabHistorCen.Close();

                            }//using


                            //BarraExportHistorias.Increment(1);
                            ImportarHistorias.ReportProgress(contador);

                        }//While
                    }// if(TabHistoCli.HasRows == false)

                }//uSING


            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "después de hacer click sobre el botón importar" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ImportarHistorias.CancelAsync();
            }
        }


        private void ImportarHistorias_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                if (ImportarHistorias.CancellationPending == false)
                {
                    BarraExportHistorias.Value = e.ProgressPercentage;
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

        private void ImportarHistorias_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Utils.Titulo01 = "Control para importar datos";
                Utils.Informa = "El proceso ha terminado satisfactoriamente " + "\r";
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);

                BarraExportHistorias.Minimum = 0;
                BarraExportHistorias.Maximum = 1;
                BarraExportHistorias.Value = 0;


                LblCantidad.Text = "0";
                LblTotal.Text = "0";

                TxtCanHistFor.Text = GlobalCantiIngre.ToString();
                TxtCanhisFormExis.Text = GlobalCantiAct.ToString();

                LblDetener.Visible = false;
                BtnDetener.Visible = false;

                LblImportar.Visible = true;
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

                
                    ImportarHistorias.WorkerSupportsCancellation = true;
                    ImportarHistorias.CancelAsync();


                    BarraExportHistorias.Minimum = 0;
                    BarraExportHistorias.Maximum = 1;
                    BarraExportHistorias.Value = 0;

                    LblCantidad.Text = "0";
                    LblTotal.Text = "0";

                    LblDetener.Visible = false;
                    BtnDetener.Visible = false;
                    LblImportar.Visible = true;
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

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            if (ImportarHistorias.IsBusy == true) //Si el proceso esta corriendo no puede voler a iniciarse 
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
    }
}
