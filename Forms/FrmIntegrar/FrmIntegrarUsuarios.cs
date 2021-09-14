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
    public partial class FrmIntegrarUsuarios : Form
    {
        public FrmIntegrarUsuarios()
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

        private void FrmIntegrarUsuarios_Load(object sender, EventArgs e)
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

                string PfiCen = TxtPrefiCenFor.Text;
                string PfiPor = TxtPrefiPorFor.Text;


                Utils.Informa = "¿Usted desea iniciar el proceso de integración" + "\r";
                Utils.Informa += "de losusuarios de SIIGHOSPLUS en la instancia del" + "\r";
                Utils.Informa += "servidor central a la instancia del portatil.?" + "\r";
                var res = MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.Yes) {


                    TxtCanUsuFor.Text = "0";
                    TxtCanUsuForm.Text = "0";
                    TxtCanUsuExis.Text = "0";



                    // partir del 02 de marzo de 2021, HERNANDO adiciona que primero se deben validar los aplicativos

                    string SqlApliDisCen, CodApliDis, SqlApliDisPor;
                    int ContiPro = 0,ApliDis;
                    ConectarCentral();



                    string SqlApliDisCenCount = "SELECT count(*) as TotalRegis  ";
                    SqlApliDisCenCount = SqlApliDisCenCount + "FROM [DATUSIIGXPSQL].[dbo].[Datos aplicativos disponibles]";

                    int Total = 0;

                    SqlDataReader reader = Conexion.SQLDataReader(SqlApliDisCenCount);

                    if (reader.HasRows)
                    {
                        reader.Read();

                        Total = Convert.ToInt32(reader["TotalRegis"]);

                        if (Total != 0)
                        {
                            ProgressBar.Minimum = 1;
                            ProgressBar.Maximum = Total;
                        }


                    }
                    else
                    {
                        ProgressBar.Minimum = 0;
                        ProgressBar.Maximum = 1;
                        ProgressBar.Value = 0;
                    }

                    if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();


                    SqlApliDisCen = "SELECT  CodApli, NomAplica, NomObje, RutaApli, NumRefe, Disponible, PuntoNet, ";
                    SqlApliDisCen = SqlApliDisCen + "Grupo, Version, CodRegis, FecRegis, CodModi, FecModi ";
                    SqlApliDisCen = SqlApliDisCen + "FROM [DATUSIIGXPSQL].[dbo].[Datos aplicativos disponibles]";



                    SqlDataReader TabApliDisCen, TabApliDisPor;

                    using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                    {
                        SqlCommand command = new SqlCommand(SqlApliDisCen, connection);
                        command.Connection.Open();
                        TabApliDisCen = command.ExecuteReader();

                        if(TabApliDisCen.HasRows == false)
                        {
                            Utils.Informa = "Lo siento pero en la sede central no" + "\r";
                            Utils.Informa += "existen aplicativos disponibles para " + "\r";
                            Utils.Informa += "sincronicar con esta instancia local." + "\r";
                            MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ContiPro = 0;
                        }
                        else
                        {
                            ApliDis = 0;
                            ConectarPortatil();
                            while (TabApliDisCen.Read())
                            {
                                CodApliDis = TabApliDisCen["CodApli"].ToString();


                                SqlApliDisPor = "SELECT CodApli, NomAplica, NomObje, RutaApli, NumRefe, Disponible, PuntoNet, ";
                                SqlApliDisPor = SqlApliDisPor + "Grupo, Version, CodRegis, FecRegis, CodModi, FecModi ";
                                SqlApliDisPor = SqlApliDisPor + "FROM [DATUSIIGXPSQL].[dbo].[Datos aplicativos disponibles] ";
                                SqlApliDisPor = SqlApliDisPor + "WHERE  (CodApli = N'" + CodApliDis + "')";

                                using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                                {
                                    SqlCommand command2 = new SqlCommand(SqlApliDisPor, connection2);
                                    command2.Connection.Open();
                                    TabApliDisPor = command2.ExecuteReader();

                                    if(TabApliDisPor.HasRows == false)
                                    {
                                        //Adicionelo porque no existe en la sede local
                                        Utils.SqlDatos = "INSERT INTO [DATUSIIGXPSQL].[dbo].[Datos aplicativos disponibles]" +
                                        "(" +
                                        "CodApli," +
                                        "NomAplica," +
                                        "NomObje," +
                                        "RutaApli," +
                                        "NumRefe," +
                                        "Disponible," +
                                        "PuntoNet," +
                                        "Grupo," +
                                        "version," +
                                        "CodRegis," +
                                        "FecRegis," +
                                        "FecModi, " +
                                        "CodModi" +
                                        ")" +
                                        "VALUES" +
                                        "(" +
                                        "'" + TabApliDisCen["CodApli"].ToString() + "'," +
                                        "'" + TabApliDisCen["NomAplica"].ToString() + "'," +
                                        "'" + TabApliDisCen["NomObje"].ToString() + "'," +
                                        "'" + TabApliDisCen["RutaApli"].ToString() + "'," +
                                        "'" + TabApliDisCen["NumRefe"].ToString() + "'," +
                                        "'" + TabApliDisCen["Disponible"].ToString() + "'," +
                                        "'" + TabApliDisCen["PuntoNet"].ToString() + "'," +
                                        "'" + TabApliDisCen["Grupo"].ToString() + "'," +
                                        "'" + TabApliDisCen["version"].ToString() + "'," +
                                        "'" + TabApliDisCen["CodRegis"].ToString() + "'," +
                                        $"{Conexion.ValidarFechaNula(TabApliDisCen["FecRegis"].ToString())}" +
                                        $"{Conexion.ValidarFechaNula(TabApliDisCen["FecModi"].ToString())}" +
                                        "'" + TabApliDisCen["CodModi"].ToString() + "' " +
                                        ")";

                                        Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos);
                                    }
                                    else
                                    {
                                        Utils.SqlDatos = $"UPDATE [DATUSIIGXPSQL].[dbo].[Datos aplicativos disponibles] SET " +
                                        "NomAplica = '" + TabApliDisCen["NomAplica"].ToString() + "', " +
                                        "NomObje = '" + TabApliDisCen["NomObje"].ToString() + "', " +
                                        "RutaApli = '" + TabApliDisCen["RutaApli"].ToString() + "', " +
                                        "NumRefe = '" + TabApliDisCen["NumRefe"].ToString() + "', " +
                                        "Disponible = '" + TabApliDisCen["Disponible"].ToString() + "', " +
                                        "PuntoNet = '" + TabApliDisCen["PuntoNet"].ToString() + "', " +
                                        "Grupo = '" + TabApliDisCen["Grupo"].ToString() + "', " +
                                        "version = '" + TabApliDisCen["version"].ToString() + "', " +
                                        "CodRegis = '" + TabApliDisCen["CodRegis"].ToString() + "', " +
                                       $"FecRegis = {Conexion.ValidarFechaNula(TabApliDisCen["FecRegis"].ToString())} " +
                                       $"FecModi = {Conexion.ValidarFechaNula(TabApliDisCen["FecModi"].ToString())} " +
                                        "CodModi = '" + TabApliDisCen["CodModi"].ToString() + "' " +
                                        "WHERE (CodApli = N'" + CodApliDis + "') ";

                                        Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos);

                                    }
                                }
                                TabApliDisPor.Close();
                                ApliDis = ApliDis + 1;

                                ProgressBar.Increment(1);
                            }//While

                            if(ApliDis > 0)
                            {
                                ContiPro = 1;
                            }
                            else
                            {
                                ContiPro = 0;
                            }


                        }

                    }

                    TabApliDisCen.Close();

                    ProgressBar.Minimum = 0;
                    ProgressBar.Maximum = 1;
                    ProgressBar.Value = 0;

                    ApliDis = 0;
                    string SqlUsu, CodUsu = "", SqlUsuCentra = "";
                    int CanUsuFor = 0;
                    SqlDataReader TabUsu, TabUsuCentra;

                    if (ContiPro == 1)
                    {
                        //Primero copiamos todos los usuarios nuevos y modifcamos loa existentes

                        ConectarCentral();

                        string SqlUsuCont = "SELECT count(*) as TotalRegis ";
                        SqlUsuCont = SqlUsuCont + "FROM  [DATUSIIGXPSQL].[dbo].[Datos usuarios de los aplicativos] ";

                        Total = 0;

                        reader = Conexion.SQLDataReader(SqlUsuCont);

                        if (reader.HasRows)
                        {
                            reader.Read();

                            Total = Convert.ToInt32(reader["TotalRegis"]);

                            if (Total != 0)
                            {
                                ProgressBar.Minimum = 1;
                                ProgressBar.Maximum = Total;
                            }


                        }
                        else
                        {
                            ProgressBar.Minimum = 0;
                            ProgressBar.Maximum = 1;
                            ProgressBar.Value = 0;
                        }

                        if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();

                        SqlUsu = "SELECT [Datos usuarios de los aplicativos].* ";
                        SqlUsu = SqlUsu + "FROM  [DATUSIIGXPSQL].[dbo].[Datos usuarios de los aplicativos] ";
                        

                        using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                        {
                            SqlCommand command2 = new SqlCommand(SqlUsu, connection2);
                            command2.Connection.Open();
                            TabUsu = command2.ExecuteReader();

                            if(TabUsu.HasRows == false)
                            {
                                Utils.Informa = "Lo siento pero en la sede central no hay" + "\r";
                                Utils.Informa += "usuarios del aplicatovo SIIGHOS PLUS, para" + "\r";
                                Utils.Informa += "ser sincronizados con la sede local." + "\r";
                                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ContiPro = 0;
                            }
                            else
                            {
                                TxtCanUsuFor.Text = "0";
                                TxtCanUsuExis.Text = "0";

                                ConectarPortatil();

                                while (TabUsu.Read())
                                {
                                    CanUsuFor += 1;

                                    CodUsu = TabUsu["CodigoUsa"].ToString();

                                    SqlUsuCentra = "SELECT * FROM [DATUSIIGXPSQL].[dbo].[Datos usuarios de los aplicativos] ";
                                    SqlUsuCentra = SqlUsuCentra + "WHERE ([Datos usuarios de los aplicativos].CodigoUsa = '" + CodUsu + "')";

                                    using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                                    {
                                        SqlCommand command = new SqlCommand(SqlUsuCentra, connection);
                                        command.Connection.Open();
                                        TabUsuCentra = command.ExecuteReader();

                                        if(TabUsuCentra.HasRows == false)
                                        {
                                            Utils.SqlDatos = "INSERT INTO [DATUSIIGXPSQL].[dbo].[Datos usuarios de los aplicativos]" +
                                            "(" +
                                            "SecretoUsa," + 
                                            "NombreEntrada," + 
                                            "CodAnte," + 
                                            "CodigoUsa," + 
                                            "IdentificUsa," + 
                                            "NombreUsa," + 
                                            "Apellido1Usa," + 
                                            "Apellido2Usa," + 
                                            "CargoUsar," + 
                                            "CodiDepen," + 
                                            "DepenUsar," + 
                                            "NivelPermiso," + 
                                            "Vigente," + 
                                            "CodiRegis," + 
                                            "FecRegis," +
                                            "FecModi," +
                                            "CodiModi" +                                
                                            ")" +
                                            "VALUES" +
                                            "(" +
                                            "'" + TabUsu["SecretoUsa"].ToString() + "'," +
                                            "'" + TabUsu["NombreEntrada"].ToString() + "'," +
                                            "'" + TabUsu["CodAnte"].ToString() + "'," +
                                            "'" + TabUsu["CodigoUsa"].ToString() + "'," +
                                            "'" + TabUsu["IdentificUsa"].ToString() + "'," +
                                            "'" + TabUsu["NombreUsa"].ToString() + "'," +
                                            "'" + TabUsu["Apellido1Usa"].ToString() + "'," +
                                            "'" + TabUsu["Apellido2Usa"].ToString() + "'," +
                                            "'" + TabUsu["CargoUsar"].ToString() + "'," +
                                            "'" + TabUsu["CodiDepen"].ToString() + "'," +
                                            "'" + TabUsu["DepenUsar"].ToString() + "'," +
                                            "'" + TabUsu["NivelPermiso"].ToString() + "'," +
                                            "'" + TabUsu["Vigente"].ToString() + "'," +
                                            "'" + TabUsu["CodiRegis"].ToString() + "'," +
                                            $"{Conexion.ValidarFechaNula(TabUsu["FecRegis"].ToString())}" +
                                            $"{Conexion.ValidarFechaNula(TabUsu["FecModi"].ToString())}" +
                                            "'" + TabUsu["CodiModi"].ToString() + "' " +
                                            ")";

                                            Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos);

                                            if (Regis)
                                            {
                                                int con = Convert.ToInt32(TxtCanUsuForm.Text) + 1;
                                                TxtCanUsuForm.Text = con.ToString();
                                            }

                                        }
                                        else
                                        {
                                            //Modifique

                                            Utils.SqlDatos = $"UPDATE [DATUSIIGXPSQL].[dbo].[Datos usuarios de los aplicativos] SET " +
                                            "SecretoUsa = '" + TabUsu["SecretoUsa"].ToString() + "', " +
                                            "NombreEntrada = '" + TabUsu["NombreEntrada"].ToString() + "', " +
                                            "CodAnte = '" + TabUsu["CodAnte"].ToString() + "', " +
                                            "CodigoUsa = '" + TabUsu["CodigoUsa"].ToString() + "', " +
                                            "IdentificUsa = '" + TabUsu["IdentificUsa"].ToString() + "', " +
                                            "NombreUsa = '" + TabUsu["NombreUsa"].ToString() + "', " +
                                            "Apellido1Usa = '" + TabUsu["Apellido1Usa"].ToString() + "', " +
                                            "Apellido2Usa = '" + TabUsu["Apellido2Usa"].ToString() + "', " +
                                            "CargoUsar = '" + TabUsu["CargoUsar"].ToString() + "', " +
                                            "CodiDepen = '" + TabUsu["CodiDepen"].ToString() + "', " +
                                            "DepenUsar = '" + TabUsu["DepenUsar"].ToString() + "', " +
                                            "NivelPermiso = '" + TabUsu["NivelPermiso"].ToString() + "', " +
                                            "Vigente = '" + TabUsu["Vigente"].ToString() + "', " +
                                            "CodiRegis = '" + TabUsu["CodiRegis"].ToString() + "', " +
                                            $"FecRegis = {Conexion.ValidarFechaNula(TabUsu["FecRegis"].ToString())} " +
                                            $"FecModi = {Conexion.ValidarFechaNula(TabUsu["FecModi"].ToString())} " +
                                            "CodiModi = '" + TabUsu["CodiModi"].ToString() + "' " +
                                            "WHERE ([Datos usuarios de los aplicativos].CodigoUsa = '" + CodUsu + "') ";

                                            Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos);

                                            if (ActControl)
                                            {
                                                int con = Convert.ToInt32(TxtCanUsuExis.Text) + 1;
                                                TxtCanUsuExis.Text = con.ToString();
                                            }

                                        }
                                    }
                                    TabUsuCentra.Close();

                                    ApliDis = ApliDis + 1;

                                    ProgressBar.Increment(1);

                                }//While

                                TxtCanUsuFor.Text = CanUsuFor.ToString();


                                if (ApliDis > 0)
                                {
                                    ContiPro = 1;
                                }
                                else
                                {
                                    ContiPro = 0;
                                }

                            }
                            
                        }

                        TabUsu.Close();

                    }//'Final ContiPro = 1

                    ProgressBar.Minimum = 0;
                    ProgressBar.Maximum = 1;
                    ProgressBar.Value = 0;

                    string SqlAplNueCentra, SqlAplNueCentra1;
                    SqlDataReader TabAplNueCentra, TabAplNueCentra1;

                    if (ContiPro == 1)
                    {
                        //Proceda a copiar los aplicativos por usuarios
                        ConectarCentral();

                        string SqlAplNueCentraCon = "SELECT count(*) as TotalRegis  ";
                        SqlAplNueCentraCon += "FROM [DATUSIIGXPSQL].[dbo].[Datos aplicativos por usuario]";

                        Total = 0;

                        reader = Conexion.SQLDataReader(SqlAplNueCentraCon);

                        if (reader.HasRows)
                        {
                            reader.Read();

                            Total = Convert.ToInt32(reader["TotalRegis"]);

                            if (Total != 0)
                            {
                                ProgressBar.Minimum = 1;
                                ProgressBar.Maximum = Total;
                            }


                        }
                        else
                        {
                            ProgressBar.Minimum = 0;
                            ProgressBar.Maximum = 1;
                            ProgressBar.Value = 0;
                        }

                        if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();


                        SqlAplNueCentra = "SELECT  CodAplica, CodUsua, CodRegis, FecRegis, CodModi, FecModi ";
                        SqlAplNueCentra += "FROM [DATUSIIGXPSQL].[dbo].[Datos aplicativos por usuario]";


                        using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                        {
                            SqlCommand command = new SqlCommand(SqlAplNueCentra, connection);
                            command.Connection.Open();
                            TabAplNueCentra = command.ExecuteReader();

                            if(TabAplNueCentra.HasRows == false)
                            {
                                //No se tienen asignados aplicativos a ningùn usuario
                            }
                            else
                            {
                                ApliDis = 0;

                                ConectarPortatil();

                                while (TabAplNueCentra.Read())
                                {
                                    CodApliDis = TabAplNueCentra["CodAplica"].ToString();
                                    CodUsu = TabAplNueCentra["CodUsua"].ToString();

                                    SqlAplNueCentra1 = "SELECT * FROM [DATUSIIGXPSQL].[dbo].[Datos aplicativos por usuario] ";
                                    SqlAplNueCentra1 = SqlAplNueCentra1 + "WHERE CodUsua = '" + CodUsu + "' AND CodAplica = '" + CodApliDis + "'";


                                    using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                                    {
                                        SqlCommand command2 = new SqlCommand(SqlAplNueCentra1, connection2);
                                        command2.Connection.Open();
                                        TabAplNueCentra1 = command2.ExecuteReader();

                                        if(TabAplNueCentra1.HasRows == false)
                                        {
                                            //Agreguelo 
                                            Utils.SqlDatos = "INSERT INTO [DATUSIIGXPSQL].[dbo].[Datos aplicativos por usuario]" +
                                            "(" +
                                            "CodAplica," +
                                            "CodUsua," +
                                            "CodRegis," +
                                            "FecRegis," +
                                            "FecModi," +
                                            "CodModi " +
                                            ")" +
                                            "VALUES" +
                                            "(" +
                                            "'" + TabAplNueCentra["CodAplica"].ToString() + "'," +
                                            "'" + TabAplNueCentra["CodUsua"].ToString() + "'," +
                                            "'" + TabAplNueCentra["CodRegis"].ToString() + "'," +
                                            $"{Conexion.ValidarFechaNula(TabAplNueCentra["FecRegis"].ToString())}" +
                                             $"{Conexion.ValidarFechaNula(TabAplNueCentra["FecModi"].ToString())}" +
                                            "'" + TabAplNueCentra["CodModi"].ToString() + "'" +
                                            ")";

                                            Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos);

                                        }
                                        else
                                        {
                                            //Modifiquelo

                                            Utils.SqlDatos = $"UPDATE [DATUSIIGXPSQL].[dbo].[Datos aplicativos por usuario] SET " +
                                            "CodRegis = '" + TabAplNueCentra["CodRegis"].ToString() + "', " +
                                            $"FecRegis = {Conexion.ValidarFechaNula(TabAplNueCentra["FecRegis"].ToString())} " +
                                            $"FecModi = {Conexion.ValidarFechaNula(TabAplNueCentra["FecModi"].ToString())} " +
                                            "CodModi = '" + TabAplNueCentra["CodModi"].ToString() + "' " +
                                            "WHERE CodUsua = '" + CodUsu + "' AND CodAplica = '" + CodApliDis + "' ";

                                            Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos);

                                        }
                                    }

                                    TabAplNueCentra1.Close();
                                    ApliDis = ApliDis + 1;

                                    ProgressBar.Increment(1);

                                }

                                if (ApliDis > 0)
                                {
                                    ContiPro = 1;
                                }
                                else
                                {
                                    ContiPro = 0;
                                }

                            }//'Final de TabAplNueCentra.BOF

                        }

                        TabAplNueCentra.Close();

                    }//'Final de ContiPro = 1

                    ProgressBar.Minimum = 0;
                    ProgressBar.Maximum = 1;
                    ProgressBar.Value = 0;


                    SqlDataReader TabPerUsuaCen, TabPerUsuaPor;

                    string SqlPerUsuaCen, MenUsua, SqlPerUsuaPor;
                    if (ContiPro == 1)
                    {
                        //Proceda a copiar los permisos por usuarios

                        ConectarCentral();


                        string SqlPerUsuaCenConunt = "SELECT count(*) as TotalRegis ";
                        SqlPerUsuaCenConunt = SqlPerUsuaCenConunt + "FROM [DATUSIIGXPSQL].[dbo].[Datos permisos usuarios]";

                        Total = 0;

                        reader = Conexion.SQLDataReader(SqlPerUsuaCenConunt);

                        if (reader.HasRows)
                        {
                            reader.Read();

                            Total = Convert.ToInt32(reader["TotalRegis"]);

                            if (Total != 0)
                            {
                                ProgressBar.Minimum = 1;
                                ProgressBar.Maximum = Total;
                            }


                        }
                        else
                        {
                            ProgressBar.Minimum = 0;
                            ProgressBar.Maximum = 1;
                            ProgressBar.Value = 0;
                        }

                        if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();


                        SqlPerUsuaCen = "SELECT CodUsua, CodAplicati, CodMenu, PerEspe, ObsPerEspe, CodRegis, FecRegis, CodModi, FecModi ";
                        SqlPerUsuaCen = SqlPerUsuaCen + "FROM [DATUSIIGXPSQL].[dbo].[Datos permisos usuarios]";

                        using (SqlConnection connection2 = new SqlConnection(Conexion.conexionSQL))
                        {
                            SqlCommand command2 = new SqlCommand(SqlPerUsuaCen, connection2);
                            command2.Connection.Open();
                            TabPerUsuaCen = command2.ExecuteReader();

                            if(TabPerUsuaCen.HasRows == false)
                            {
                                //NO se tiene definido los permisos por usuarios en la sede central
                            }
                            else
                            {
                                ConectarPortatil();
                                while (TabPerUsuaCen.Read())
                                {
                                    CodApliDis = TabPerUsuaCen["CodAplicati"].ToString();
                                    CodUsu = TabPerUsuaCen["CodUsua"].ToString();
                                    MenUsua = TabPerUsuaCen["CodMenu"].ToString();

                                    SqlPerUsuaPor = "SELECT CodUsua, CodAplicati, CodMenu, PerEspe, ObsPerEspe, CodRegis, FecRegis, CodModi, FecModi ";
                                    SqlPerUsuaPor = SqlPerUsuaPor + "FROM [DATUSIIGXPSQL].[dbo].[Datos permisos usuarios] ";
                                    SqlPerUsuaPor = SqlPerUsuaPor + "WHERE  (CodUsua = N'" + CodUsu + "') AND ";
                                    SqlPerUsuaPor = SqlPerUsuaPor + "(CodAplicati = N'" + CodApliDis + "') AND ";
                                    SqlPerUsuaPor = SqlPerUsuaPor + "(CodMenu = N'" + MenUsua + "')";

                                    using (SqlConnection connection = new SqlConnection(Conexion.conexionSQL))
                                    {
                                        SqlCommand command = new SqlCommand(SqlPerUsuaCen, connection);
                                        command.Connection.Open();
                                        TabPerUsuaPor = command.ExecuteReader();

                                        if(TabPerUsuaPor.HasRows == false)
                                        {
                                            //Agrega el permiso del usuarios
                                            Utils.SqlDatos = "INSERT INTO [DATUSIIGXPSQL].[dbo].[Datos permisos usuarios]" +
                                            "(" +
                                            "CodUsua," +
                                            "CodAplicati," +
                                            "CodMenu," +
                                            "PerEspe," +
                                            "ObsPerEspe," +
                                            "CodRegis," +
                                            "FecRegis," +
                                            "FecModi," +
                                            "CodModi" +
                                            ")" +
                                            "VALUES" +
                                            "(" +
                                            "'" + TabPerUsuaCen["CodUsua"].ToString() + "'," +
                                            "'" + TabPerUsuaCen["CodAplicati"].ToString() + "'," +
                                            "'" + TabPerUsuaCen["CodMenu"].ToString() + "'," +
                                            "'" + TabPerUsuaCen["PerEspe"].ToString() + "'," +
                                            "'" + TabPerUsuaCen["ObsPerEspe"].ToString() + "'," +
                                            "'" + TabPerUsuaCen["CodRegis"].ToString() + "'," +
                                            $"{Conexion.ValidarFechaNula(TabPerUsuaCen["FecRegis"].ToString())}" +
                                            $"{Conexion.ValidarFechaNula(TabPerUsuaCen["FecModi"].ToString())}" +
                                            "'" + TabPerUsuaCen["CodModi"].ToString() + "'" +
                                            ")";

                                            Boolean Regis = Conexion.SqlInsert(Utils.SqlDatos);
                                        }
                                        else
                                        {
                                            //Modifica el permiso

                                            Utils.SqlDatos = $"UPDATE [DATUSIIGXPSQL].[dbo].[Datos permisos usuarios] SET " +
                                            "PerEspe = '" + TabPerUsuaCen["PerEspe"].ToString() + "', " +
                                            "ObsPerEspe = '" + TabPerUsuaCen["ObsPerEspe"].ToString() + "', " +
                                            "CodRegis = '" + TabPerUsuaCen["CodRegis"].ToString() + "', " +
                                            $"FecRegis = {Conexion.ValidarFechaNula(TabPerUsuaCen["FecRegis"].ToString())} " +
                                            $"FecModi = {Conexion.ValidarFechaNula(TabPerUsuaCen["FecModi"].ToString())} " +
                                            "CodModi = '" + TabPerUsuaCen["CodModi"].ToString() + "' " +
                                            "WHERE (CodUsua = N'" + CodUsu + "') and (CodAplicati = N'" + CodApliDis + "') AND (CodMenu = N'" + MenUsua + "')  ";

                                            Boolean ActControl = Conexion.SQLUpdate(Utils.SqlDatos);

                                        }
                                    }

                                    TabPerUsuaPor.Close();

                                    ProgressBar.Increment(1);

                                }//While

                            }
                        }

                        TabPerUsuaCen.Close();
                    }


                    if(ContiPro == 1)
                    {
                        Utils.Titulo01 = "Control de integracion de Usuarios";
                        Utils.Informa = "El proceso ha terminado satisfactoriamente";
                        MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    ProgressBar.Minimum = 0;
                    ProgressBar.Maximum = 1;
                    ProgressBar.Value = 0;

                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "despues de hacer click en integrar" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
