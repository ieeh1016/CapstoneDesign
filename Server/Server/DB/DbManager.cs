using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using MySql.Data.MySqlClient;
using ServerCore;

namespace Server.DB
{
    public class DbManager
    {
        static private MySqlConnection conn;
        static public MySqlCommand cmd;

        //추후 정적으로 바꿈
        static private string DB_IP = "database-codingisland.c37r4fnqfff9.ap-northeast-2.rds.amazonaws.com";
        static private string DB_Port = "3306";
        static string DB_TARGET = "DB_Test";
        static string DB_UID = "";
        static string DB_PWD = "";
        static string User_DB_Table = "user_data";
        static string Challenge_DB_Table = "challenge_data";

        public static byte user_Name_Input(String name, String Uid)
        {
            byte check;
            byte reply = 1;
            string connectString = string.Format("Server={0};Port={1};Database={2};Uid ={3};Pwd={4};", DB_IP, DB_Port, DB_TARGET, DB_UID, DB_PWD);
            string sql = String.Format("select EXISTS (select Uid FROM {0} where UID = '{1}' limit 1) as success", User_DB_Table, Uid);
            using (conn = new MySqlConnection(connectString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                check = Convert.ToByte(cmd.ExecuteScalar());
                if (check == 0) // 첫방문
                {
                    sql = String.Format("insert into {0} (Uid, name) values ('{1}', '{2}')", User_DB_Table, Uid, name);
                    cmd = new MySqlCommand(sql, conn);
                }
                if (check == 1) { }
            }
            return reply;
        }


        public static Dictionary<byte, byte> Load_star(String UID)
        {
            byte check;
            string connectString = string.Format("Server={0};Port={1};Database={2};Uid ={3};Pwd={4};", DB_IP, DB_Port, DB_TARGET, DB_UID, DB_PWD);
            String sql = String.Format("select EXISTS (select Uid FROM {0} where UID = '{1}' limit 1) as success", Challenge_DB_Table, UID);
            Dictionary<byte, byte> star_Dic = new Dictionary<byte, byte>();
            using (conn = new MySqlConnection(connectString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                check = Convert.ToByte(cmd.ExecuteScalar());

                if (check == 0) // 첫방문
                {
                    sql = String.Format("insert into {0} (Uid, Stage, Star) values ('{1}', 1, 0)", Challenge_DB_Table, UID);
                    cmd = new MySqlCommand(sql, conn);
                    star_Dic.Add(1, 0);
                }
                if (check == 1)
                { // 데이터 존재
                    sql = String.Format("Select Stage, Star from {0} where Uid ='{1}' order by Stage", Challenge_DB_Table, UID);
                    cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        star_Dic.Add(Convert.ToByte(reader["Stage"]), Convert.ToByte(reader["Star"]));
                    }
                }
            }
            return star_Dic;
        }

        public static List<Data_Structure> Challenge_MyPage(String UID)
        {
            List<Data_Structure> data_set = new List<Data_Structure>();

            string connectString = string.Format("Server={0};Port={1};Database={2};Uid ={3};Pwd={4};", DB_IP, DB_Port, DB_TARGET, DB_UID, DB_PWD);
            string sql = String.Format("Select name, Rank_Table.ranking, totalStars " +
                    "from(Select {0}.Uid, NAME, SUM(Star) as totalStars, RANK() OVER(ORDER BY SUM(Star) DESC) AS ranking from {0} INNER JOIN {1} ON {0}.Uid = {1}.Uid group by {0}.Uid) Rank_Table " +
                        "WHERE Rank_Table.Uid = '{2}'", Challenge_DB_Table, User_DB_Table, UID);
            using (conn = new MySqlConnection(connectString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    data_set.Add(new Data_Structure()
                    {
                        name = Convert.ToString(reader["name"]),
                        ranking = Convert.ToInt32(reader["ranking"]),
                        TotalStars = Convert.ToByte(reader["totalStars"])
                    });
                }
            }
            return data_set;
        }


        public static List<S_Challenge_Top30Rank.Rank> Study_ChallengeTop30(List<S_Challenge_Top30Rank.Rank> list)
        {
            string connectString = string.Format("Server={0};Port={1};Database={2};Uid ={3};Pwd={4};", DB_IP, DB_Port, DB_TARGET, DB_UID, DB_PWD);
            string sql = String.Format("Select name, Rank_Table.ranking, totalStars " +
                    "from (Select {0}.Uid, NAME, SUM(Star) as totalStars, RANK() OVER(ORDER BY SUM(Star) DESC) AS ranking from {0} " +
                    "INNER JOIN {1} ON {0}.Uid = {1}.Uid group by {0}.Uid) Rank_Table ORDER BY Rank_Table.ranking Asc", Challenge_DB_Table, User_DB_Table);

            using (conn = new MySqlConnection(connectString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new S_Challenge_Top30Rank.Rank()
                    {
                        UId = Convert.ToString(reader["Uid"]),
                        ranking = Convert.ToInt32(reader["ranking"]),
                        totalStars = Convert.ToByte(reader["totalStars"])
                    });
                }
            }
            return list;
        }

        public static void challenge_UpdateStar(String UID, byte STAGE, byte Star)
        {
            string connectString = string.Format("Server={0};Port={1};Database={2};Uid ={3};Pwd={4};", DB_IP, DB_Port, DB_TARGET, DB_UID, DB_PWD);
            string sql = String.Format("Select Star from {0} where Uid = '{1}' AND STAGE = {2}", Challenge_DB_Table, UID, STAGE);
            int remaining_Star;

            using (MySqlConnection conn = new MySqlConnection(connectString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                remaining_Star = Convert.ToInt32(cmd.ExecuteScalar());
                if (remaining_Star < Star)
                {
                    sql = String.Format("Update {0} Set Star ={1} where Uid ='{2}' AND STAGE = {3}", Challenge_DB_Table, Star, UID, STAGE);
                    cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    if (remaining_Star == 0)
                    {
                        sql = String.Format("Insert Into {0}  (UID, Stage, Star) values ('{1}', {2}, {3})", Challenge_DB_Table, UID, (STAGE + 1), 0);
                        cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public class Data_Structure
        {
            public string name;
            public int ranking;
            public byte TotalStars;
        }
    }
}
