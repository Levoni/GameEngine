using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Base.Database
{
   public static class DatabaseWrapper
   {
      //static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Levon Swenson\source\repos\Base\Base\equipment.mdf;Integrated Security=True";
      static public string conString;
      static public SqlConnection sqlConnection;

      /// <summary>
      /// 
      /// </summary>
      static public void ConnectDatabaseWrapper()
      {
         conString = @"Data Source=(LocalDB)\MSSQLLocalDB;";
         conString += "AttachDbFilename=";
         DirectoryInfo QuickRelativePath = new DirectoryInfo(@"..\..\..\..\equipment.mdf");
         conString += QuickRelativePath.FullName;
         conString += ";Integrated Security=True";
         sqlConnection = new SqlConnection(conString);
         try
         {
            sqlConnection.Open();
            sqlConnection.Close();
         }
         catch (Exception ex)
         {
            Console.WriteLine("Database Failed to Connect. Exeption Description: ");
            Console.WriteLine(ex.Message);
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="fullPathToDatabase"></param>
      static public void ConnectDatabaseWrapper(string fullPathToDatabase)
      {
         conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=";
         conString += fullPathToDatabase;
         conString += ";Integrated Security=True";
         sqlConnection = new SqlConnection(conString);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sqlSelectCommand"></param>
      /// <returns></returns>
      static public List<object[]> executeSQLQueryCommand(string sqlSelectCommand)
      {
         try
         {
            sqlConnection.Open();
            try
            {
               List<object[]> rows = new List<object[]>();
               using (SqlCommand command = new SqlCommand(sqlSelectCommand, sqlConnection))
               {
                  SqlDataReader reader = command.ExecuteReader();
                  object[] objectValues = new object[reader.FieldCount];
                  while (reader.Read())
                  {
                     for (int i = 0; i < reader.FieldCount; i++)
                     {
                        objectValues[i] = reader.GetValue(i);
                     }
                     rows.Add(objectValues);
                  }
               }
               sqlConnection.Close();
               return rows;
            }
            catch (Exception e)
            {
               Console.WriteLine("Error Occured while accessing the Database");
               Console.WriteLine(e.Message);
               sqlConnection.Close();
            }
         }
         catch (Exception e)
         {
            Console.WriteLine("Database failed to connect");
            Console.WriteLine(e.Message);
         }
         return null;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sqlCommandString"></param>
      static public void executeSQLNonQueryCommand(string sqlCommandString)
      {
         try
         {
            sqlConnection.Open();
            try
            {
               using (SqlCommand command = new SqlCommand(sqlCommandString, sqlConnection))
               {
                  command.ExecuteNonQuery();
               }
               sqlConnection.Close();
            }
            catch(Exception ex)
            {
               Console.WriteLine("Error Occured while accessing the Database");
               Console.WriteLine(ex.Message);
               sqlConnection.Close();
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine("Database Failed to connect");
            Console.WriteLine(ex.Message);
         }
      }


      // TODO: Need to break this into another passthrough class

      //static public Unit GetUniqueUnit(int id)
      //{
      //   string sql = "Select * from Character WHERE Character_Id = " + id.ToString();
      //   List<object[]> unitValues = executeSQLQueryCommand(sql, 12);
      //   if (unitValues == null)
      //      return new Unit();
      //   else
      //   {
      //      Unit tempUnit = new Unit();
      //      object[] o = unitValues[0];
      //      tempUnit.level = (int)o[1];
      //      tempUnit.name = (string)o[2];
      //      tempUnit.className = (string)o[3];
      //      tempUnit.maxHealth = (int)o[4];
      //      tempUnit.Power = (int)o[5];
      //      tempUnit.Vitality = (int)o[6];
      //      tempUnit.Intelligence = (int)o[7];
      //      tempUnit.Mentality = (int)o[8];
      //      tempUnit.Agility = (int)o[9];
      //      tempUnit.Dexterity = (int)o[10];

      //      return tempUnit;
      //   }
      //}

      //static public Unit GetSoldierUnit(int id)
      //{
      //   string sql = "Select Level, Class_Name from Soldier where Soldier_Id =" + id.ToString();
      //   List<object[]> unitValues = executeSQLQueryCommand(sql, 2);
      //   if (unitValues == null)
      //      return new Unit();
      //   else
      //   {
      //      object[] o = unitValues[0];
      //      Unit tempUnit = new Unit();
      //      tempUnit.name = "Name"; //Replace with 
      //      tempUnit.level = (int)o[0];
      //      tempUnit.className = (string)o[1];
      //      return tempUnit;
      //   }
      //}

      //static public UnitUpgrade getClass(string className)
      //{
      //   string sql = "SELECT * from class WHERE Class_Name = '" + className + "'";
      //   List<object[]> classValues = executeSQLQueryCommand(sql, 10);
      //   if (classValues == null)
      //      return new UnitUpgrade();
      //   else
      //   {
      //      UnitUpgrade uu = new UnitUpgrade();
      //      object[] o = classValues[0];
      //      uu.className = (string) o[0];
      //      uu.maxHealthIncrease = (int) o[1];
      //      uu.powerIncrease = (int) o[2];
      //      uu.vitalityIncrease = (int) o[3];
      //      uu.intelligenceIncrease = (int)o[4];
      //      uu.mentalityIncrease = (int)o[5];
      //      uu.agilityIncrease = (int)o[6];
      //      uu.dexterityIncrease = (int)o[7];
      //      uu.luckIncrease = (int)o[8];
      //      return uu;
      //   }
      //}
   }
}
