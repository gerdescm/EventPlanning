using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for Class1
/// </summary>
public class DataAccess
{
    private SqlConnection con = null;

    public DataAccess()
    {
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["TylerEventsDB"].ConnectionString);
    }
    
    //Executing select command
    public DataTable ExecuteSelectCommand(string CommandName, CommandType cmdType)
    {
        SqlCommand cmd = null;
        DataTable table = new DataTable();

        cmd = con.CreateCommand();

        cmd.CommandType = cmdType;
        cmd.CommandText = CommandName;

        try
        {
            con.Open();

            SqlDataAdapter da = null;
            using (da = new SqlDataAdapter(cmd))
            {
                da.Fill(table);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            con.Close();
        }

        return table;
    }

    public DataTable ExecuteParamerizedSelectCommand(string CommandName,
                     CommandType cmdType, SqlParameter[] param)
    {
        SqlCommand cmd = null;
        DataTable table = new DataTable();

        cmd = con.CreateCommand();

        cmd.CommandType = cmdType;
        cmd.CommandText = CommandName;
        cmd.Parameters.AddRange(param);

        try
        {
            con.Open();

            SqlDataAdapter da = null;
            using (da = new SqlDataAdapter(cmd))
            {
                da.Fill(table);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            con.Close();
        }

        return table;
    }

    //Executing Update, Delete, and Insert Commands
    public bool ExecuteNonQuery(string CommandName, CommandType cmdType, SqlParameter[] pars)
    {
        SqlCommand cmd = null;
        int res = 0;

        cmd = con.CreateCommand();

        cmd.CommandType = cmdType;
        cmd.CommandText = CommandName;
        cmd.Parameters.AddRange(pars);

        try
        {
            con.Open();

            res = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd.Dispose();
            cmd = null;
            con.Close();
        }

        if (res >= 1)
        {
            return true;
        }
        return false;
    }
}