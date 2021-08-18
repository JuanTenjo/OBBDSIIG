using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;


namespace OBBDSIIG.Class
{
    class Conexion
    {
        #region DECLARACION PROPIEDADES

        public static string servidor { get; set; }
        public static string servidorCen { get; set; }
        public static string username { get; set; }
        public static string usernameCen { get; set; }
        public static string password { get; set; }
        public static string passwordCen { get; set; }
        public static string nativeclient { get; set; }

        public static string conexionSQL { get; set; }
        public static string conexionACCESS { get; set; }

        public static OleDbConnection conexionOleDb;
        public static SqlConnection sqlConnection;

        #endregion


        public static string AS(string texto)
        {
            return texto;
        }



        public static string ValidarHoraNula(string Hora, bool Opcion = true)
        {
            string ValidarHora = null;

            if(Opcion)
            {
                ValidarHora = string.IsNullOrWhiteSpace(Hora) ? "null" + "," : "CONVERT(DATETIME,'" + Convert.ToDateTime(Hora).ToString("hh:mm:ss") + "',8), ";
            }
            else
            {
                ValidarHora = string.IsNullOrWhiteSpace(Hora) ? "null" + "," : "CONVERT(DATETIME,'" + Convert.ToDateTime(Hora).ToString("hh:mm:ss") + "',8) ";
            } 

            return ValidarHora;

        }

        public static string ValidarFechaNula(string Fecha)
        {
            DateTime temp;
       
            string ValidarFecha = null;
            string fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
    

            if (DateTime.TryParse(Fecha, out temp)) //Valida que no venga null o con texto normal
            {

                Fecha = Convert.ToDateTime(Fecha).ToString("yyyy-MM-dd");

                string ValidacionFechas = Fecha.Substring(0,1);

                if (ValidacionFechas == "0")
                {
                    ValidarFecha = string.IsNullOrWhiteSpace(Fecha) ? "null" + "," : "CONVERT(DATETIME,'" + Convert.ToDateTime(fechaActual).ToString("yyyy-MM-dd") + "',102),";
                    return ValidarFecha;
                }
                else
                {
                    ValidarFecha = string.IsNullOrWhiteSpace(Fecha) ? "null" + "," : "CONVERT(DATETIME,'" + Convert.ToDateTime(Fecha).ToString("yyyy-MM-dd") + "',102),";
                    return ValidarFecha;
                }

            }
            else
            {
                //Si viene vacio lo deja null, si viene con un string difenrente a una fecha lo deja con la fecha actual

                ValidarFecha = string.IsNullOrWhiteSpace(Fecha) ? "null" + "," : "CONVERT(DATETIME,'" + Convert.ToDateTime(fechaActual).ToString("yyyy-MM-dd") + "',102),";
                return ValidarFecha;

            }

        }

        public static SqlDataReader SQLDataReader(string sqlString, List<SqlParameter> parameters = null)
        {
            try
            {
                sqlConnection = new SqlConnection(conexionSQL);
                sqlConnection.Open(); //aqui

                SqlCommand command = new SqlCommand(sqlString, sqlConnection);

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                command.CommandTimeout = 300;

                SqlDataReader reader = command.ExecuteReader();

                return reader;



            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion SQLDataReader" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(sqlString);
                return null;
            }
        }

        public static bool SQLUpdate(string sqlString, List<SqlParameter> parameters = null)
        {
            try
            {

                using (sqlConnection = new SqlConnection(conexionSQL))
                {
                    SqlCommand command = new SqlCommand(sqlString, sqlConnection);

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    sqlConnection.Open();

                    command.CommandTimeout = 300;

                    command.ExecuteNonQuery();

                }

                return true;

            }
            catch (Exception ex)
            {

                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion SQLUpdate" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(sqlString);
                return false;
            }
            finally
            {
                if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();
            }
        }
        public static bool SQLDelete(string sqlString, List<SqlParameter> parameters = null)
        {
            try
            {
                using (sqlConnection = new SqlConnection(conexionSQL))
                {
                    SqlCommand command = new SqlCommand(sqlString, sqlConnection);

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    command.CommandTimeout = 300;

                    sqlConnection.Open();
                    command.ExecuteNonQuery();

                }

                return true;
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion SQLUDelete" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(sqlString);
                return false;
            }
            finally
            {
                if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();
            }
        }


        public static bool SqlInsert(string sqlString, List<SqlParameter> parameters = null)
        {
            try
            {
                using (sqlConnection = new SqlConnection(conexionSQL))
                {
                    SqlCommand command = new SqlCommand(sqlString, sqlConnection);

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {   
                            command.Parameters.Add(parameter);
                        }
                    }

                    sqlConnection.Open(); //aqui

                    command.CommandTimeout = 300;

                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion SqlInsert" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(sqlString);
                return false;
            }
            finally
            {
                if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();
            }
        }

        public static int SQLStoreProcedure(string sqlString)
        {
            try
            {
                int rowAffected;

                using (sqlConnection = new SqlConnection(conexionSQL))
                {
                    SqlCommand command = new SqlCommand(sqlString, sqlConnection);
                    command.CommandType = CommandType.Text;

                    sqlConnection.Open();
                    rowAffected = command.ExecuteNonQuery();
                }

                return rowAffected;
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion SQLStoreProcedure" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();
            }
        }


        public static DataSet SQLDataSet(string sqlString, List<SqlParameter> parameters = null)
        {
            try
            {
                using (sqlConnection = new SqlConnection(conexionSQL))
                {
                    sqlConnection.Open();

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlString, sqlConnection);

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            sqlDataAdapter.SelectCommand.Parameters.Add(parameter);
                        }
                    }

                    DataSet dataSet = new DataSet();

                    sqlDataAdapter.Fill(dataSet);

                    return dataSet;
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion SQLDataSet" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();
            }
        }

        public static DataTable SQLDataTable(string sqlString, List<SqlParameter> parameters = null)
        {
            try
            {
                using (sqlConnection = new SqlConnection(conexionSQL))
                {
                    sqlConnection.Open();

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlString, sqlConnection);

                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlDataAdapter);

                    //sqlDataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                    //sqlDataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
                    //sqlDataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            sqlDataAdapter.SelectCommand.Parameters.Add(parameter);
                        }
                    }

                    DataTable dataTable = new DataTable();

                    sqlDataAdapter.Fill(dataTable);

                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion SQLDataSet" + "\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (Conexion.sqlConnection.State == ConnectionState.Open) Conexion.sqlConnection.Close();
            }
        }

        public static OleDbDataReader AccessDataReaderOLEDB(string sqlString)
        {
            try
            {
                // No se puede cerrar el objeto conexion antes de usar el Data reader
                // Se necesita abierto para que funcione el Data reader

                conexionOleDb = new OleDbConnection();
                conexionOleDb.ConnectionString = conexionACCESS;
                conexionOleDb.Open();

                OleDbCommand cmd = new OleDbCommand(sqlString, conexionOleDb);

                OleDbDataReader reader = cmd.ExecuteReader();

                return reader;
            }
            catch (Exception ex)
            {
                Utils.Titulo01 = "Control de errores de ejecución";
                Utils.Informa = "Lo siento pero se ha presentado un error" + "\r";
                Utils.Informa += "en la funcion AccessDataReaderOLEDB" + "\r\r";
                Utils.Informa += "Error: " + ex.Message + " - " + ex.StackTrace;
                MessageBox.Show(Utils.Informa, Utils.Titulo01, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
