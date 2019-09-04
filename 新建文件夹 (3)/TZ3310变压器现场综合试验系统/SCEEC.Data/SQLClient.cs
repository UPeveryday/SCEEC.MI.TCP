using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SCEEC.Data
{
    public class SQLClient : IDisposable
    {
        MySqlConnection sConn;
        bool connected = false;
        string errorText = string.Empty;

        private void __SQLClient(string connectionString)
        {
            sConn = new MySqlConnection(connectionString);
            try
            {
                sConn.Open();
                connected = true;
                errorText = string.Empty;
            }
            catch (Exception ex)
            {
                sConn = null;
                connected = false;
                errorText = ex.Message;
            }
        }

        public SQLClient(string connectionString)
        {
            __SQLClient(connectionString);
        }

        public SQLClient(string server, string port, string username, string password, string database)
        {
            string connString = "server=" + server + ";port=" + port + ";database=" + database + ";user=" + username + ";password=" + password + ";";
            __SQLClient(connString);
        }

        public DataSet getDataSet(string database, string table)
        {
            DataSet ds = new DataSet();
            if (this.Connected)
            {
                string cmd = "select * from " + database + "." + table;
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd, sConn);
                    sda.Fill(ds);
                }
                catch (Exception ex)
                {
                    errorText = ex.Message;
                    connected = false;
                }
            }
            return ds;
        }

        public DataSet getDataSetView(string database, string table)
        {
            DataSet ds = new DataSet();
            if (this.Connected)
            {
                string cmd = "select * from " + database + "." + table;
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd, sConn);
                    sda.Fill(ds, "table");
                }
                catch (Exception ex)
                {
                    errorText = ex.Message;
                    connected = false;
                }
            }
            return ds;
        }

        public DataTable getDataTable(string database, string table)
        {
            DataTable ds = new DataTable();
            if (this.Connected)
            {
                string cmd = "select * from " + database + "." + table;
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd, sConn);
                    sda.Fill(ds);
                }
                catch (Exception ex)
                {
                    errorText = ex.Message;
                    connected = false;
                }
            }
            return ds;
        }

        public DataTable updateDataTable(string database, string table, DataTable ds)
        {
            if (this.Connected)
            {
                string cmd = "select * from " + database + "." + table;
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd, sConn);
                    MySqlCommandBuilder scb = new MySqlCommandBuilder(sda);
                    sda.UpdateCommand = scb.GetUpdateCommand();
                    sda.InsertCommand = scb.GetInsertCommand();
                    sda.DeleteCommand = scb.GetDeleteCommand();
                    sda.Update(ds);
                }
                
                catch (Exception ex)
                {
                    errorText = ex.Message;
                    connected = false;
                }
            }
            return ds;
        }

        public bool insertDataRow(string database, string table, string[] names , string[] values, int id = -1)
        {
            if (this.Connected)
            {
                if (values.Length < 1) return false;
                if (names.Length != values.Length) return false;
                StringBuilder sb_names = new StringBuilder();
                StringBuilder sb_values = new StringBuilder();
                sb_names.Append("(");
                sb_values.Append("(");
                if (id > -1)
                {
                    sb_names.Append("id, ");
                    sb_values.Append(id.ToString() + ", ");
                }
                for (int i = 0; i < values.Length; i++)
                {
                    sb_values.Append("'");
                    sb_names.Append(names[i]);
                    sb_values.Append(values[i]);
                    sb_values.Append("'");
                    if (i < (values.Length - 1))
                    {
                        sb_names.Append(",");
                        sb_values.Append(",");
                    }
                }
                sb_names.Append(")");
                sb_values.Append(")");
                string cmd = "insert into " + database + "." + table + " " + sb_names.ToString() + " values " + sb_values.ToString();
                try
                {
                    MySqlCommand scmd = new MySqlCommand(cmd, sConn);
                    scmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    errorText = ex.Message;
                    connected = false;
                }
            }
            return false;
        }

        public bool deleteTable(string database, string table)
        {
            if (this.Connected)
            {
                string cmd = "delete from " + database + "." + table;
                try
                {
                    MySqlCommand scmd = new MySqlCommand(cmd, sConn);
                    scmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    errorText = ex.Message;
                    connected = false;
                }
            }
            return false;
        }
        public bool deleteDataRow(string database, string table, string columnName, string name)
        {
            if (this.Connected)
            {
                string cmd = "delete from " + database + "." + table + " where " + columnName + " = '" + name + "'";
                try
                {
                    MySqlCommand scmd = new MySqlCommand(cmd, sConn);
                    scmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    errorText = ex.Message;
                    connected = false;
                }
            }
            return false;
        }

        public bool updateData(string database, string table, string uniqueName, string uniqueValue, string columnName, string Value)
        {
            if (this.Connected)
            {
                string cmd = "update " + database + "." + table + " set " + columnName + " = '" + Value + "' , where " + uniqueName + " = '" + uniqueValue + "'";
                try
                {
                    MySqlCommand scmd = new MySqlCommand(cmd, sConn);
                    scmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    errorText = ex.Message;
                    connected = false;
                }
            }
            return false;
        }

        public string ErrorText
        {
            get
            {
                return errorText;
            }
        }

        public bool Connected
        {
            get
            {
                if (__disposed) return false;
                if (sConn == null) return false;
                return connected;
            }
        }

        bool __disposed = false;

        public bool Disposed
        {
            get
            {
                return __disposed;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (__disposed) return;
            if (disposing)
            {
                if (sConn != null) sConn.Close();
                sConn = null;
                connected = false;
                GC.SuppressFinalize(this);
            }
            __disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        ~SQLClient()
        {
            Dispose(false);
        }


    }
}
