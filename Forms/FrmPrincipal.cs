using System;
using OBBDSIIG.Class;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.OleDb;
using System.Data.SqlClient;

using OBBDSIIG.Forms;
using OBBDSIIG.Forms.FrmExportar;
using OBBDSIIG.Forms.FrmImportar;
using OBBDSIIG.Forms.FrmIntegrar;

namespace OBBDSIIG.Forms
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                Utils.BaseDeDatosPrincipal = "ACDATOXPSQL";

                Conexion.conexionACCESS = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\SIIGHOSPLUS\LogPlus.LogSip;Jet OLEDB:Database Password=SIIGHOS33";

                Utils.SqlDatos = "SELECT * FROM [Local registro del usuario]";

                OleDbDataReader dr = Conexion.AccessDataReaderOLEDB(Utils.SqlDatos);

                if (dr.HasRows)
                {
                    dr.Read();

                    // Se procede a validar las credenciales de acceso al Servidor SQL Server
                    // Y verificar el tipo de cliente de SQL Server

                    Conexion.servidor = dr["NomServi"].ToString();
                    Conexion.servidorCen = dr["InstanCentral"].ToString();
                    Conexion.username = dr["NomUsar"].ToString();
                    Conexion.password = dr["PassWusa"].ToString();


                    //Conexion.servidor = @"HAROLD-PC\PC";
                    //Conexion.username = "sa";
                    //Conexion.password = "SIIGHOS33*";

                    Conexion.conexionSQL = "Server=" + Conexion.servidor + "; " +
                                           "Initial Catalog=" + Utils.BaseDeDatosPrincipal + ";" +
                                           "User ID= " + Conexion.username + "; " +
                                           "Password=" + Conexion.password;


                    Utils.codUsuario = dr["CodigUsar"].ToString();
                    Utils.nomUsuario = dr["NombreUsar"].ToString();
                    Utils.nivelPermiso = dr["NivelPermiso"].ToString();
                    Utils.codUnicoEmpresa = dr["CodRegEn"].ToString(); // CodEnti
                    Utils.CodAplicacion = dr["CodApli"].ToString();

                    Utils.InstanCenFor = dr["InstanCentral"].ToString(); // Se agrego este campo en LogSip para que quede general tanto para pc de mesa como para portatiles
                    Utils.PrefiCenFor = dr["PreCentral"].ToString(); // Se agrego este campo en LogSip para que quede general tanto para pc de mesa como para portatiles
                    Utils.PrefiPorFor = dr["PrePortatil"].ToString(); // Se agrego este campo en LogSip para que quede general tanto para pc de mesa como para portatiles
                    Utils.InstanPortaFor = dr["NomServi"].ToString();

                    this.lblFecha.Text = DateTime.Now.ToString("dddd dd 'de' MMMM 'de' yyyy") + "   -";
                    this.lblCodUsuario.Text = Utils.codUsuario;
                    this.lblNomUsuario.Text = Utils.nomUsuario;

                    Utils.SqlDatos = @"SELECT CodiMinSalud, NitCCEmpresa, NomEmpresa, TipoDocEmp, TelPrin " +
                                   "FROM [BDADMINSIG].[dbo].[Datos informacion de la empresa] " +
                                   "WHERE CodUnico = @codUnicoEmpresa";

                    List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@codUnicoEmpresa", SqlDbType.VarChar, 2) { Value = Utils.codUnicoEmpresa }
                    };

                    SqlDataReader Sqldr = Conexion.SQLDataReader(Utils.SqlDatos, parameters);

                    if (Sqldr.HasRows)
                    {
                        Sqldr.Read();
                        Utils.codMinSalud = Sqldr["CodiMinSalud"].ToString();
                        Utils.nitEmpresa = Sqldr["NitCCEmpresa"].ToString();
                        Utils.nomEmpresa = Sqldr["NomEmpresa"].ToString();
                        Utils.tipoDocEmp = Sqldr["TipoDocEmp"].ToString();
                        Utils.TelEmpresa = Sqldr["TelPrin"].ToString();
                    }


                    Sqldr.Close();
                }
                else
                {
                    this.Close();
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "al abrir el formulario principal" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sedeCentralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.BaseDeDatosPrincipal = "ACDATOXPSQL";

            Conexion.conexionSQL = "Server=" + Conexion.servidor + "; " +
                                   "Initial Catalog=" + Utils.BaseDeDatosPrincipal + ";" +
                                   "User ID= " + Conexion.username + "; " +
                                   "Password=" + Conexion.password;

            FrmExportSedeCentral frmExportSedeCentral = new FrmExportSedeCentral();
            frmExportSedeCentral.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void sedeCentralCitologiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.BaseDeDatosPrincipal = "ACDATOXPSQL";

            Conexion.conexionSQL = "Server=" + Conexion.servidor + "; " +
                                   "Initial Catalog=" + Utils.BaseDeDatosPrincipal + ";" +
                                   "User ID= " + Conexion.username + "; " +
                                   "Password=" + Conexion.password;
            FrmExportSedeCentralCito frmExportSedeCentralCito = new FrmExportSedeCentralCito();
            frmExportSedeCentralCito.ShowDialog();
        }

        private void sedeCenrtralHigieneOralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.BaseDeDatosPrincipal = "ACDATOXPSQL";

            Conexion.conexionSQL = "Server=" + Conexion.servidor + "; " +
                                   "Initial Catalog=" + Utils.BaseDeDatosPrincipal + ";" +
                                   "User ID= " + Conexion.username + "; " +
                                   "Password=" + Conexion.password;

            FrmExportHigieneOral frmExportHigieneOral = new FrmExportHigieneOral();

            frmExportHigieneOral.ShowDialog();
        }

        private void sedeCentralToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Utils.BaseDeDatosPrincipal = "ACDATOXPSQL";

            Conexion.conexionSQL = "Server=" + Conexion.servidor + "; " +
                                   "Initial Catalog=" + Utils.BaseDeDatosPrincipal + ";" +
                                   "User ID= " + Conexion.username + "; " +
                                   "Password=" + Conexion.password;


            FrmImportSedeCentral frmImportSedeCentral = new FrmImportSedeCentral();

            frmImportSedeCentral.ShowDialog();
        }

        private void sedeCentralHigieneOralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.BaseDeDatosPrincipal = "ACDATOXPSQL";

            Conexion.conexionSQL = "Server=" + Conexion.servidor + "; " +
                                   "Initial Catalog=" + Utils.BaseDeDatosPrincipal + ";" +
                                   "User ID= " + Conexion.username + "; " +
                                   "Password=" + Conexion.password;


            FrmImportHigieneOral frmImportHigieneOral = new FrmImportHigieneOral();

            frmImportHigieneOral.ShowDialog();

        }

        private void sedeCentralHistoriasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Utils.BaseDeDatosPrincipal = "ACDATOXPSQL";

            Conexion.conexionSQL = "Server=" + Conexion.servidor + "; " +
                                   "Initial Catalog=" + Utils.BaseDeDatosPrincipal + ";" +
                                   "User ID= " + Conexion.username + "; " +
                                   "Password=" + Conexion.password;

            FrmExportSedeCentralHistorias frmExportSedeCentralHistorias = new FrmExportSedeCentralHistorias();
            frmExportSedeCentralHistorias.ShowDialog();

        }

        private void sedeCentralHistoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.BaseDeDatosPrincipal = "ACDATOXPSQL";

            Conexion.conexionSQL = "Server=" + Conexion.servidor + "; " +
                                   "Initial Catalog=" + Utils.BaseDeDatosPrincipal + ";" +
                                   "User ID= " + Conexion.username + "; " +
                                   "Password=" + Conexion.password;

            FrmImportSedeCentralHistorias frmImportSedeCentralHistorias = new FrmImportSedeCentralHistorias();
            frmImportSedeCentralHistorias.ShowDialog();

        }

        private void biometriaSedeCentralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.BaseDeDatosPrincipal = "ACDATOXPSQL";

            Conexion.conexionSQL = "Server=" + Conexion.servidor + "; " +
                                   "Initial Catalog=" + Utils.BaseDeDatosPrincipal + ";" +
                                   "User ID= " + Conexion.username + "; " +
                                   "Password=" + Conexion.password;

            FrmIntegrarBiometria frmIntegrarBiometria = new FrmIntegrarBiometria();
            frmIntegrarBiometria.ShowDialog();

        }
    }
}
