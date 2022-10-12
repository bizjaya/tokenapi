using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data;
using MySqlConnector;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System.Data.SqlClient;
using TOKENAPI.Common;
//using DevSql = Devart.Data.MySql;
//using Z.BulkOperations;

namespace TOKENAPI.EF
{

    public interface IDbCon
    {

    }

    public class DbCon : IDbCon
    {

        string sqlconn;
        string sqlconn2;
        private IConfiguration _config;

        public DbCon(IConfiguration config)
        {
            // string envir = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //  string jfile = (envir == "Development") ? "appsettings.json" : "appsettings." + envir + ".json";
            //   AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(jfile).Build();

            //AppSetting["ConnectionStrings:PosConn"];
            //string conString = Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(this.Configuration, "DefaultConnection");
            _config = config;
            //AppSetting.GetConnectionString("ExConn");
            sqlconn = _config.GetConnectionString("devcon");
            sqlconn2 = _config.GetConnectionString("devart");


        }


        public async Task Insert<T>(T obj) where T : class
        {

            using (IDbConnection con = new MySqlConnection(sqlconn))
            {
                await con.InsertAsync<T>(obj);
            }

        }




        public async Task<FBQueRes<T>> GetList<T>(string sql, FBPageQuery @p = null)
        {
            @p.SortBy = (@p.SortBy == "Id") ? "t1.Id" : @p.SortBy;
            @p.PageSize = (@p.PageSize == 0) ? 10 : @p.PageSize;

            Regex rgx = new Regex(@"\#([^\#]*)\#");

            @p.SearchBy = @p.SearchBy?.Replace("_", ".");
            if ((@p.SearchBy ?? "").Length > 0)
            {
                sql += $" AND {@p.SearchBy} LIKE '%{@p.SearchTxt}%' ";
            }
            //  string count = sql+";";

            string count = rgx.Replace(sql, " count(*) as cnt ") + ";";
            sql = sql.Replace("#", "");
            @p.SortBy = @p.SortBy.Replace("_", ".");
            sql += $" ORDER BY {@p.SortBy} {@p.SortDir} LIMIT {@p.PageOff},{@p.PageSize}; ";

            //List<T> list = new List<T>();

            using (IDbConnection con = new MySqlConnection(sqlconn))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var multi = await con.QueryMultipleAsync(sql + count);

                var list = multi.Read<T>().ToList();
                int cnt = (list.Count() > 0) ? multi.Read<int>().First() : 0;
                @p.ResCount = cnt;

                var ret = new FBQueRes<T>(list, cnt);
                return ret;

            }
        }


        public async Task<List<T>> SqlToList<T>(string sql, object param)
        {
            using (IDbConnection con = new MySqlConnection(sqlconn))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var k = await con.QueryAsync<T>(sql, param);

                List<T> list = k.ToList();
                return list;

            }


        }


        public async Task<T> SqlToEnt<T>(string sql, object param=null) where T : new()
        {
            T obj = new T();
            if (sql == "")
                return obj;

            using (IDbConnection con = new MySqlConnection(sqlconn))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                //DynamicParameters parameter = new DynamicParameters();
                //parameter.Add("@Id", id);
                //groupMeeting = con.Query<GroupMeeting>("GetGroupMeetingByID", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();

                obj = await con.QueryFirstOrDefaultAsync<T>(sql, param);

            }

            return obj;
        }

        public async Task<SPReturn> SPROC(string sql, object param)
        {


            using (IDbConnection con = new MySqlConnection(sqlconn))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var k = await con.QueryFirstOrDefaultAsync<SPReturn>(sql, param, commandType: CommandType.StoredProcedure);
                ;
                return k;

            }
        }

        public async Task<T> SPROC<T>(string sql, object param = null) where T : new()
        {

            T obj = new T();
            if (sql == "")
                return obj;


            using (IDbConnection con = new MySqlConnection(sqlconn))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var k = await con.QueryFirstOrDefaultAsync<T>(sql, param, commandType: CommandType.StoredProcedure);

                return k;

            }
        }

        public async Task<List<T>> SPROCLIST<T>(string sql, object param = null) where T : new()
        {

            using (IDbConnection con = new MySqlConnection(sqlconn))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var k = await con.QueryAsync<T>(sql, param, commandType: CommandType.StoredProcedure);

                List<T> list = k.ToList();
                return list;
            }
        }

        public async Task<(List<T1>, List<T2>)> SPROCLIST<T1, T2>(string sql) where T1 : new() where T2 : new()
        {

            using (IDbConnection con = new MySqlConnection(sqlconn))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var res = await con.QueryMultipleAsync(sql, commandType: CommandType.StoredProcedure);

                var table1 = await res.ReadAsync<T1>();
                var table2 = await res.ReadAsync<T2>();

                return (table1.ToList(), table2.ToList());
            }
        }

        public async Task<(List<T1>, List<T2>)> SPROCLIST<T1, T2>(string sql, object param = null) where T1 : new() where T2 : new()
        {

            using (IDbConnection con = new MySqlConnection(sqlconn))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var res = await con.QueryMultipleAsync(sql, param, commandType: CommandType.StoredProcedure);

                var table1 = await res.ReadAsync<T1>();
                var table2 = await res.ReadAsync<T2>();

                return (table1.ToList(), table2.ToList());
            }
        }

        public async Task<(List<T1>, List<T2>, List<T3>)> SPROCLIST<T1, T2, T3>(string sql) where T1 : new() where T2 : new() where T3 : new()
        {

            using (IDbConnection con = new MySqlConnection(sqlconn))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var res = await con.QueryMultipleAsync(sql, commandType: CommandType.StoredProcedure);

                var table1 = await res.ReadAsync<T1>();
                var table2 = await res.ReadAsync<T2>();
                var table3 = await res.ReadAsync<T3>();



                return (table1.ToList(), table2.ToList(), table3.ToList());
            }
        }

        public async Task<(List<T1>, List<T2>, List<T3>, List<T4>)> SPROCLIST<T1, T2, T3, T4>(string sql) where T1 : new() where T2 : new() where T3 : new() where T4 : new()
        {

            using (IDbConnection con = new MySqlConnection(sqlconn))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var res = await con.QueryMultipleAsync(sql, commandType: CommandType.StoredProcedure);

                var table1 = await res.ReadAsync<T1>();
                var table2 = await res.ReadAsync<T2>();
                var table3 = await res.ReadAsync<T3>();
                var table4 = await res.ReadAsync<T4>();



                return (table1.ToList(), table2.ToList(), table3.ToList(), table4.ToList());
            }
        }

        public async Task<(List<T1>, List<T2>, List<T3>, List<T4>, List<T5>, List<T6>)> SPROCLIST<T1, T2, T3, T4, T5, T6>(string sql, object param = null) where T1 : new() where T2 : new() where T3 : new() where T4 : new() where T5 : new() where T6 : new()
        {

            using (IDbConnection con = new MySqlConnection(sqlconn))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var res = await con.QueryMultipleAsync(sql, param, commandType: CommandType.StoredProcedure);

                var table1 = await res.ReadAsync<T1>();
                var table2 = await res.ReadAsync<T2>();
                var table3 = await res.ReadAsync<T3>();
                var table4 = await res.ReadAsync<T4>();
                var table5 = await res.ReadAsync<T5>();
                var table6 = await res.ReadAsync<T6>();



                return (table1.ToList(), table2.ToList(), table3.ToList(), table4.ToList(), table5.ToList(), table6.ToList());
            }
        }


        /*
        public async Task<(List<T1>,List<T2>)> SPROCLIST<T1,T2>(string sql, params object[] args) where T1 : new() where T2 : new()
        {

            using (IDbConnection con = new MySqlConnection(sqlconn))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var res = await con.QueryMultipleAsync(sql, commandType: CommandType.StoredProcedure);

                var table1 = await res.ReadAsync<T>();
                var table2 = await res.ReadAsync<Q>();

                return (table1.ToList(), table2.ToList());
            }
        }

    */



        public async Task<int> Execute(string sql, object param = null)
        {
            int affectedRows = 0;

            using (IDbConnection con = new MySqlConnection(sqlconn))
            {
                affectedRows = await con.ExecuteAsync(sql, param);


            }

            return affectedRows;
        }

        public async Task<int> ExecMulti(string sql, object param = null)
        {
            int affectedRows = 0;

            using (IDbConnection con = new MySqlConnection(sqlconn))
            {
                affectedRows = await con.ExecuteAsync(sql, param);


            }

            return affectedRows;
        }

        //public async Task BulkUpdate<T>(List<T> obj)
        //{
        //    using (var connection = new MySqlConnection(sqlconn))
        //    {
        //        connection.Open();

        //        // BulkMerge 
        //        using (var bulk = new BulkOperation<List<T>>(connection))
        //        {

        //            await bulk.BulkUpdateAsync(obj);

        //        }
        //    }

        //}


        public async Task<int> Count(string sql, object param = null)
        {
            int cntRows = 0;

            using (IDbConnection con = new MySqlConnection(sqlconn))
            {
                cntRows = await con.ExecuteScalarAsync<int>(sql);
            }

            return cntRows;
        }



    }
}

