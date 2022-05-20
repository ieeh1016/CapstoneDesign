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

        static string RR_UID = "Using_Select";
        static string RR_PWD = "vorlzjarhk";

        static string Init_UID = "Using_init";
        static string Init_PWD = "vorlzjarhk123";

        static string User_DB_Table = "user_data";
        static string Challenge_DB_Table = "challenge_data";

        public class Data_Structure
        {
            public string name;
            public int ranking;
            public byte TotalStars;
        }

        public static Dictionary<byte, byte> user_Name_Input(String Uid, String name)
        {
            byte check;
            Dictionary<byte, byte> star_Dic = new Dictionary<byte, byte>();

            string connectString = string.Format("Server={0};Port={1};Database={2};Uid ={3};Pwd={4};", DB_IP, DB_Port, DB_TARGET, RR_UID, RR_PWD);
            string sql = String.Format("select EXISTS (select Uid FROM {0} where UID = '{1}' limit 1) as success", User_DB_Table, Uid);
            using (conn = new MySqlConnection(connectString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                check = Convert.ToByte(cmd.ExecuteScalar());
            }

            if (check == 0) // 초기방문 세팅
            {
                connectString = string.Format("Server={0};Port={1};Database={2};Uid ={3};Pwd={4};", DB_IP, DB_Port, DB_TARGET, Init_UID, Init_PWD);
                using (conn = new MySqlConnection(connectString))
                {
                    conn.Open();

                    sql = String.Format("insert into {0} (Uid, name) values ('{1}', '{2}')", User_DB_Table, Uid, name);
                    cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }

                using (conn = new MySqlConnection(connectString))
                {
                    conn.Open();

                    sql = String.Format("insert into {0} (Uid, challenge_Stage, Star) values ('{1}', 1, 0)", Challenge_DB_Table, Uid);
                    cmd = new MySqlCommand(sql, conn);
                    star_Dic.Add(1, 0);
                    cmd.ExecuteNonQuery();
                }
            }

            else if (check == 1) // 기존 데이터가 있을때
            {
                connectString = string.Format("Server={0};Port={1};Database={2};Uid ={3};Pwd={4};", DB_IP, DB_Port, DB_TARGET, RR_UID, RR_PWD);
                using (conn = new MySqlConnection(connectString))
                {
                    conn.Open();

                    sql = String.Format("Select challenge_Stage, Star from {0} where Uid ='{1}' order by challenge_Stage", Challenge_DB_Table, Uid);
                    cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        star_Dic.Add(Convert.ToByte(reader["challenge_Stage"]), Convert.ToByte(reader["Star"]));
                    }
                }
            }
            return star_Dic;
        }

        public static Data_Structure Challenge_MyPage(String UID)
        {
            Data_Structure data_set = new Data_Structure();

            string connectString = string.Format("Server={0};Port={1};Database={2};Uid ={3};Pwd={4};", DB_IP, DB_Port, DB_TARGET, RR_UID, RR_PWD);
            string sql = String.Format("Select name, Rank_Table.ranking, totalStars " +
                    "from(Select {0}.Uid, NAME, SUM(Star) as totalStars, ROW_NUMBER() OVER (ORDER BY SUM(Star) DESC) AS ranking from {0} INNER JOIN {1} ON {0}.Uid = {1}.Uid group by {0}.Uid) Rank_Table " +
                        "WHERE Rank_Table.Uid = '{2}'", Challenge_DB_Table, User_DB_Table, UID);
            using (conn = new MySqlConnection(connectString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    data_set.name = Convert.ToString(reader["name"]);
                    data_set.ranking = Convert.ToInt32(reader["ranking"]);
                    data_set.TotalStars = Convert.ToByte(reader["totalStars"]);

                }
            }
            return data_set;
        }

        public static List<S_Challenge_Top30Rank.Rank> Study_ChallengeTop30(List<S_Challenge_Top30Rank.Rank> list)
        {
            string connectString = string.Format("Server={0};Port={1};Database={2};Uid ={3};Pwd={4};", DB_IP, DB_Port, DB_TARGET, RR_UID, RR_PWD);
            string sql = String.Format("Select name, Rank_Table.ranking, totalStars " +
                    "from (Select {0}.Uid, NAME, SUM(Star) as totalStars, ROW_NUMBER() OVER (ORDER BY SUM(Star) DESC) AS ranking from {0} " +
                    "INNER JOIN {1} ON {0}.Uid = {1}.Uid group by {0}.Uid) Rank_Table ORDER BY Rank_Table.ranking Asc limit 30", Challenge_DB_Table, User_DB_Table);

            using (conn = new MySqlConnection(connectString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new S_Challenge_Top30Rank.Rank()
                    {
                        name = Convert.ToString(reader["name"]),
                        ranking = Convert.ToInt32(reader["ranking"]),
                        totalStars = Convert.ToByte(reader["totalStars"])
                    });

                }
                Console.WriteLine(list.Count);

                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine($"{i}번째 유저 정보");
                    Console.WriteLine(list[i].name);
                    Console.WriteLine(list[i].totalStars);

                }
            }
            return list;
        }

        public static void challenge_UpdateStar(String UID, byte STAGE, byte Star)
        {
            string connectString = string.Format("Server={0};Port={1};Database={2};Uid ={3};Pwd={4};", DB_IP, DB_Port, DB_TARGET, RR_UID, RR_PWD);
            string sql = String.Format("Select Star from {0} where Uid = '{1}' AND challenge_Stage = {2}", Challenge_DB_Table, UID, STAGE);
            int remaining_Star;
            using (MySqlConnection conn = new MySqlConnection(connectString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                remaining_Star = Convert.ToInt32(cmd.ExecuteScalar());
            }
            connectString = string.Format("Server={0};Port={1};Database={2};Uid ={3};Pwd={4};", DB_IP, DB_Port, DB_TARGET, Init_UID, Init_PWD);

            if (remaining_Star < Star)
            {
                using (MySqlConnection conn = new MySqlConnection(connectString))
                {
                    conn.Open();

                    sql = String.Format("Update {0} Set Star ={1} where Uid ='{2}' AND challenge_Stage = {3}", Challenge_DB_Table, Star, UID, STAGE);
                    cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }

            if (remaining_Star < Star)
            {
                using (MySqlConnection conn = new MySqlConnection(connectString))
                {
                    conn.Open();

                    sql = String.Format("Update {0} Set Star ={1} where Uid ='{2}' AND challenge_Stage = {3}", Challenge_DB_Table, Star, UID, STAGE);
                    cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }

            if (remaining_Star == 0 && (STAGE <10 ))
            {
                using (MySqlConnection conn = new MySqlConnection(connectString))
                {
                    conn.Open();

                    sql = String.Format("Insert Into {0}  (UID, challenge_Stage, Star) values ('{1}', {2}, {3})", Challenge_DB_Table, UID, (STAGE + 1), 0);
                    cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}