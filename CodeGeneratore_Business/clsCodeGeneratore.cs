using CodeGeneratore_DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using static CodeGeneratore_Business.clsCodeGeneratore;
using System.Runtime.Remoting.Messaging;

namespace CodeGeneratore_Business
{
    public class clsCodeGeneratore
    {

        // Declaration 
        private struct stColumnInfo
        {
            public string Name;
            public string DataType;
            public bool AllowNull;
        }

        private static  List<stColumnInfo> _ListColumnsInfo = new List<stColumnInfo>();
       
        public static string ClassName { set; get; }

        private static string _SelectedTableName = "";
        public static string SelectedTableName
        {
            set
            {
                _SelectedTableName = value;
                _LoadTableInfo();
            }
            get
            {
                return _SelectedTableName;
            }
        }

        public static string ConnectionString
        {
            get
            {
                return clsDataAccessSettings.GetConnectionString();
            }
        }

        public static string IDName { get; set; }


        // Private Function
        private static void _LoadTableInfo()
        {
            DataTable dtColumns = clsCodeGeneratoreData.GetColumnsInfoForTable(_SelectedTableName);
            stColumnInfo ColumnInfo = new stColumnInfo();
            _ListColumnsInfo.Clear();

            foreach (DataRow row in dtColumns.Rows)
            {
                ColumnInfo.Name = row["COLUMN_NAME"].ToString();
                ColumnInfo.DataType = _GetCSharpDataTypeFromSqlDataType(row["DATA_TYPE"].ToString());
                ColumnInfo.AllowNull = (row["IS_NULLABLE"].ToString() == "YES");
                _ListColumnsInfo.Add(ColumnInfo);
            }
        }
        private static string _GetCSharpDataTypeFromSqlDataType(string SqlDataType)
        {
            switch (SqlDataType)
            {
                case "bigint":
                    return "long";

                case "int":
                    return "int";

                case "tinyint":
                    return "byte";

                case "smallint":
                    return "short";

                case "smalldatetime":
                case "date":
                case "datetime":
                case "datetime2":
                    return "DateTime";

                case "time":
                    return "TimeSpan";

                case "datetimeoffset":
                    return "DateTimeOffset";

                case "uniqueidentifier":
                    return "Guid";

                case "sql_variant":
                    return "object";



                case "bit":
                    return "bool";

                case "decimal":
                case "numeric":
                case "money":
                case "smallmoney":
                    return "decimal";

                case "float":
                    return "double";

                case "real":
                    return "float";

                case "char":
                case "varchar":
                case "text":
                case "nchar":
                case "nvarchar":
                case "ntext":
                case "xml":
                    return "string";

                case "binary":
                case "varbinary":
                case "image":
                    return "byte[]";

                default:
                    return "string";
            }
        }
        private static string _GetSqlDataTypeFromCSharpDataType(string CSharpDataType)
        {
            switch (CSharpDataType)
            {
                case "long":
                    return "BigInt";

                case "int":
                    return "Int";

                case "byte":
                    return "TinyInt";

                case "short":
                    return "SmallInt";

                case "DateTime":
                    return "DateTime2";

                case "TimeSpan":
                    return "Time";

                case "DateTimeOffset":
                    return "DateTimeOffset";

                case "Guid":
                    return "UniqueIdentifier";




                case "bool":
                    return "Bit";

                case "decimal":

                    return "Decimal";

                case "double":
                    return "Float";

                case "float":
                    return "Real";


                case "string":
                    return "Nvarchar";

                case "byte[]":
                    return "Binary";

                default:
                    return "Nvarchar";
            }
        }

        private static string _GetDefaultValueForDataType(string DataType, bool AllowNull)
        {
            if (AllowNull)
                return "null";

            switch (DataType)
            {
                case "string":
                    return "";

                case "bool":
                    return "false";

                case "int":
                case "long":
                    return "-1";

                case "short":
                case "byte":
                    return "0";

                case "float":
                case "double":
                case "decimal":
                    return "0";

                case "DateTime":
                    return "DateTime.Now";

                case "byte[]":
                    return "null";

                case "DateTimeOffset":
                    return "DateTimeOffset.Now";

                case "TimeSpan":
                    return "DateTime.Now-DateTime.Today";
                default:
                    return "null";
            }

        }


        private static string _GenerateProperty()
        {

            StringBuilder sb = new StringBuilder();
            string MarkIsNullableDataType = "?";

            foreach (stColumnInfo ColumnInfo in _ListColumnsInfo)
            {
                MarkIsNullableDataType = (ColumnInfo.AllowNull) ? "?" : "";

                sb.AppendLine($"public {ColumnInfo.DataType}{MarkIsNullableDataType} {ColumnInfo.Name}  {{get; set;}} ");
            }
            return sb.ToString();
        }
       
        private static string _GetListParametersWithDataType()
        {
            StringBuilder sb = new StringBuilder();



            string MarkIsAlowNull = "?";


            foreach (stColumnInfo ColumnInfo in _ListColumnsInfo)
            {
                MarkIsAlowNull = (ColumnInfo.AllowNull) ? "?" : "";
                sb.Append($"{ColumnInfo.DataType}{MarkIsAlowNull} {ColumnInfo.Name} ,");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();

        }

        private static string _GenerateConstructor()
        {


            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"public {ClassName}({ClassName}DTO {ClassName.ToLower()}DTO, enMode CreationMode=enMode.AddNew){{");

            sb.AppendLine(string.Join(";" + Environment.NewLine, _ListColumnsInfo.Select(Column => $"this.{Column.Name} = {ClassName.ToLower()}DTO.{Column.Name}")));
            sb.AppendLine(";");
            sb.AppendLine("Mode = CreationMode;");
            sb.AppendLine($"}}");
            return sb.ToString();
        }

        private static string _GenerateAddNewMethode()
        {

            stColumnInfo IdInfo = _GetIdInfo();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"private bool _AddNew{ClassName}(){{");

            sb.AppendLine($"this.{IdInfo.Name} ={ClassName}Data.AddNew{ClassName}(this.{ClassName.ToLower()}DTO);");
            sb.AppendLine($"return (this.{IdInfo.Name} != {_GetDefaultValueForDataType(IdInfo.DataType, IdInfo.AllowNull)});");
            sb.AppendLine($"}}");
            return sb.ToString();
        }
        private static string _GenerateUpdateMethode()
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"private bool _Update{ClassName}(){{");

            sb.AppendLine($"return {ClassName}Data.Update{ClassName}(this.{ClassName.ToLower()}DTO);");
            sb.AppendLine($"}}");
            return sb.ToString();
        }
        private static string _GenerateSaveMethode()
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"	         public bool Save()
                           {
                                switch (Mode)
                                {
                                    case enMode.AddNew:
                                        if (_AddNew" + $"{ClassName}()" + @")
                                        {
                                             Mode = enMode.Update;
                                             return true;
                                         }
                                         else
                                              return false;
                                    case enMode.Update:
                                          return _Update" + $"{ClassName}()" + @";
                                  }
                                  return false;
                             }");
            return sb.ToString();
        }

        private static string _GenerateDeleteMethode()
        {
            stColumnInfo IdInfo = _GetIdInfo();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"	         public static bool Delete" + $"{ClassName}({IdInfo.DataType} {IdInfo.Name}" + @")
                          {
                                                return " + $"{ClassName}Data.Delete{ClassName}({IdInfo.Name})" + @";
                          }");
            return sb.ToString();
        }
        private static string _GenerateGetAllMethode()
        {

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"	         public static List<" + $"{ClassName}DTO> GetAll{SelectedTableName}(" + @")
                           {
                                                return " + $"{ClassName}Data.GetAll{SelectedTableName}()" + @";
                           }");
            return sb.ToString();
        }

        private static string _GenerateISExistMethode()
        {
            stColumnInfo IdInfo = _GetIdInfo();
            StringBuilder sb = new StringBuilder();


            sb.AppendLine(@"	         public static bool Is" + $"{ClassName}Exist({IdInfo.DataType} {IdInfo.Name}" + @")
                          {
                                                return " + $"{ClassName}Data.Is{ClassName}Exist({IdInfo.Name})" + @";
                          }");
            return sb.ToString();
        }

        private static string _GenerateFindMethode()
        {
            stColumnInfo IdInfo = _GetIdInfo();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"public static " + $"{ClassName} Find({IdInfo.DataType} {IdInfo.Name})" + @"
                          {

                             " + $"{ClassName}DTO {ClassName.ToLower()}DTO = {ClassName}Data.Get{ClassName}InfoByID({IdInfo.Name});" + @"

                             " + $"if ({ClassName.ToLower()}DTO != null)" + @"
                             {
                                     return new " + $"{ClassName}({ClassName.ToLower()}DTO , enMode.Update)" + @";
                             }
                            return null;

                         }");
            return sb.ToString();
        }

        private static string _GenerateproprietyClassDTO()
        {
            return $"public {ClassName}DTO {ClassName.ToLower()}DTO" + @"
        {
            get =>  new " + $"{ClassName}DTO({string.Join(",", _ListColumnsInfo.Select(Column => "this." + Column.Name))})" + @";
        }";
        }

        private static string _GetCommandParameters(bool WithID = true)
        {
            StringBuilder sb = new StringBuilder();

            foreach (stColumnInfo ColumnInfo in _ListColumnsInfo)
            {

                if (!WithID && ColumnInfo.Name == IDName)
                    continue;

                if (!ColumnInfo.AllowNull)
                {
                    sb.AppendLine($"Command.Parameters.AddWithValue(\"@{ColumnInfo.Name}\", {ClassName.ToLower()}DTO.{ColumnInfo.Name});");
                }
                else
                {
                    if (ColumnInfo.DataType != "string")
                    {
                        sb.AppendLine(@"if (" + $"{ClassName.ToLower()}DTO.{ColumnInfo.Name}!= null)" + @"
                                          Command.Parameters.AddWithValue(" + $"\"@{ColumnInfo.Name}\", {ClassName.ToLower()}DTO.{ColumnInfo.Name});" + @"
                                  else
                                          Command.Parameters.AddWithValue(" + $"\"@{ColumnInfo.Name}\"" + ", DBNull.Value);");
                    }
                    else
                    {
                        sb.AppendLine(@"if (" + $"{ClassName.ToLower()}DTO.{ColumnInfo.Name}!= null && {ClassName.ToLower()}DTO.{ColumnInfo.Name}!= \"\")" + @"
                                          Command.Parameters.AddWithValue(" + $"\"@{ColumnInfo.Name}\", {ClassName.ToLower()}DTO.{ColumnInfo.Name});" + @"
                                  else
                                          Command.Parameters.AddWithValue(" + $"\"@{ColumnInfo.Name}\"" + ", DBNull.Value);");
                    }

                }

            }

            return sb.ToString();
        }
        

        private static stColumnInfo _GetIdInfo()
        {
            return _ListColumnsInfo.FirstOrDefault(Column => Column.Name == IDName);
        }


        private static string _GenerateAddNewDataMethode()
        {
            stColumnInfo IdInfo = _GetIdInfo();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($@"public static int AddNew" + $"{ClassName}({ClassName}DTO  {ClassName.ToLower()}DTO)" + @"
                          {
                                                  " + $"{IdInfo.DataType} {IdInfo.Name} = {_GetDefaultValueForDataType(IdInfo.DataType, IdInfo.AllowNull)};" + @"
                                                  try
                                                  {
                                                         using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                                                         {
                                                                 Connection.Open();
                                                                 using (SqlCommand Command = new SqlCommand(""SP_AddNew" + $"{ClassName}\"" + @", Connection))
                                                                 {
                                                                         Command.CommandType = CommandType.StoredProcedure;
                                                                         " + $"{_GetCommandParameters(false)}" + @"

                                                                         SqlParameter outputIdParam = new SqlParameter("""+$"{IdInfo.Name}\"" + @", SqlDbType." + $"{_GetSqlDataTypeFromCSharpDataType(IdInfo.DataType)})" + @"
                                                                         {
                                                                              Direction = ParameterDirection.Output
                                                                          };
                                                                           Command.Parameters.Add(outputIdParam);


                                                                           Command.ExecuteNonQuery();

                                                                           " + $"{IdInfo.Name} = ({IdInfo.DataType})outputIdParam.Value;" + @"

                                                                    }
                                                              }
                                                           
                                                        }
                                                       catch (Exception Ex)
                                                       {
                                                               clsEventLogData.WriteEvent($"" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}"", EventLogEntryType.Error);
                                                                 " + $"{IdInfo.Name} = {_GetDefaultValueForDataType(IdInfo.DataType, IdInfo.AllowNull)} ;" + @"
                                                        }
                                                        return " + $"{IdInfo.Name}" + @";
                          }");
            return sb.ToString();
        }

        private static string _GenerateUpdateDataMethode()
        {

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"public static bool Update" + $"{ClassName}({ClassName}DTO {ClassName.ToLower()}DTO)" + @"
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand(""SP_Update" + $"{ClassName}\"" + @", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        " + $"{_GetCommandParameters(true)}" + @"
        
                        SqlParameter RowsEffectedParam = new SqlParameter(""@RowsEffected"", SqlDbType.Int" + @")
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        Command.Parameters.Add(RowsEffectedParam);

                        Command.ExecuteNonQuery();
                        RowsEffected = ((int)RowsEffectedParam.Value);

                      
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($"" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}"", EventLogEntryType.Error);
                RowsEffected = 0;
            }

            return (RowsEffected == 1);
        }");
            return sb.ToString();
        }
        private static string _GenerateDeleteDataMethode()
        {
            stColumnInfo IdInfo = _GetIdInfo();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"public static bool Delete" + $"{ClassName}({IdInfo.DataType} {IdInfo.Name})" + @"
        {
            int RowsEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand(""SP_Delete" + $"{ClassName}\"" + @", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue(" + $"\"@{IdInfo.Name}\", {IdInfo.Name});" + @"


                       SqlParameter RowsEffectedParam = new SqlParameter(""@RowsEffected"", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        Command.Parameters.Add(RowsEffectedParam);

                        Command.ExecuteNonQuery();
                        RowsEffected = ((int)RowsEffectedParam.Value);

                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($"" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}"", EventLogEntryType.Error);
                RowsEffected = 0;
            }

            return RowsEffected == 1;
        }");
            return sb.ToString();
        }


        private static string _GenerateGetAllDataMethode()
        {

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"public static List<" + $"{ClassName}DTO>" + @" GetAll" + $"{SelectedTableName}" + @"()
        {
             List<" + $"{ClassName}DTO> List{SelectedTableName} = new List<{ClassName}DTO>();" + @"
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand(""SP_GetAll" + $"{SelectedTableName}\"" + @", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                           while (Reader.Read())
                            {
                                 " + $"List{SelectedTableName}.Add" + @"
                                    (
                                       new " + $"{ClassName}DTO" + @"
                                       (
                                          " + $"{string.Join("," + Environment.NewLine, _ListColumnsInfo.Select(Column => _ReadProprityFromReader(Column)))}" + @"
                                       )
                                    );
                            }
                        }


                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($"" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}"", EventLogEntryType.Error);
            
            }

            return  " + $"List{SelectedTableName}" + @";
        }");
            return sb.ToString();
        }
        private static string _GenerateIsExistByIDDataMethode()
        {

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"public static bool Is" + $"{ClassName}ExistByID(int " + $"{IDName})" + @"
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand(""SP_Is" + $"{ClassName}ExistByID\"" + @", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue(" + $"\"@{IDName}\", {IDName});" + @"
                        SqlParameter IsFoundParam = new SqlParameter(""@IsFound"", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        Command.Parameters.Add(IsFoundParam);

                        Command.ExecuteNonQuery();
                        IsFound = ((int)IsFoundParam.Value == 1);

                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($"" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}"", EventLogEntryType.Error);
                IsFound = false;
            }
            return IsFound;
        }");
            return sb.ToString();
        }

        private static string _ReadProprityFromReader(stColumnInfo ColumnInfo)
        {

            switch (ColumnInfo.DataType)
            {
                case "int":
                    return (ColumnInfo.AllowNull) ? $"Reader.IsDBNull(Reader.GetOrdinal(\"{ColumnInfo.Name}\")) ? null : Reader.GetInt32(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))" : $"" +
                        $"Reader.GetInt32(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))";

                case "short":
                    return (ColumnInfo.AllowNull) ? $"Reader.IsDBNull(Reader.GetOrdinal(\"{ColumnInfo.Name}\")) ? null : Reader.GetInt16(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))" : $"" +
                        $"Reader.GetInt16(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))";

                case "long":
                    return (ColumnInfo.AllowNull) ? $"Reader.IsDBNull(Reader.GetOrdinal(\"{ColumnInfo.Name}\")) ? null : Reader.GetInt64(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))" : $"" +
                        $"Reader.GetInt64(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))";

                case "byte":
                    return (ColumnInfo.AllowNull) ? $"Reader.IsDBNull(Reader.GetOrdinal(\"{ColumnInfo.Name}\")) ? null : Reader.GetByte(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))" : $"" +
                         $"Reader.GetByte(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))";

                case "bool":
                    return (ColumnInfo.AllowNull) ? $"Reader.IsDBNull(Reader.GetOrdinal(\"{ColumnInfo.Name}\")) ? null : Reader.GetBoolean(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))" : $"" +
                          $"Reader.GetBoolean(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))";

                case "string":
                    return (ColumnInfo.AllowNull) ? $"Reader.IsDBNull(Reader.GetOrdinal(\"{ColumnInfo.Name}\")) ? null : Reader.GetString(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))" : $"" +
                         $"Reader.GetString(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))";

                case "decimal":
                    return (ColumnInfo.AllowNull) ? $"Reader.IsDBNull(Reader.GetOrdinal(\"{ColumnInfo.Name}\")) ? null : Reader.GetDecimal(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))" : $"" +
                         $"Reader.GetDecimal(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))";

                case "float":
                    return (ColumnInfo.AllowNull) ? $"Reader.IsDBNull(Reader.GetOrdinal(\"{ColumnInfo.Name}\")) ? null : Reader.GetFloat(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))" : $"" +
                         $"Reader.GetFloat(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))";

                case "double":
                    return (ColumnInfo.AllowNull) ? $"Reader.IsDBNull(Reader.GetOrdinal(\"{ColumnInfo.Name}\")) ? null : Reader.GetDouble(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))" : $"" +
                         $"Reader.GetDouble(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))";

                case "DateTime":
                    return (ColumnInfo.AllowNull) ? $"Reader.IsDBNull(Reader.GetOrdinal(\"{ColumnInfo.Name}\")) ? null : Reader.GetDateTime(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))" : $"" +
                         $"Reader.GetDateTime(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))";

                case "Guid":
                    return (ColumnInfo.AllowNull) ? $"Reader.IsDBNull(Reader.GetOrdinal(\"{ColumnInfo.Name}\")) ? null : Reader.GetGuid(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))" : $"" +
                         $"Reader.GetGuid(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))";

                case "[]byte":
                    return (ColumnInfo.AllowNull) ? $"Reader.IsDBNull(Reader.GetOrdinal(\"{ColumnInfo.Name}\")) ? null : Reader.GetBytes(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))" : $"" +
                         $"Reader.GetBytes(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))";

                case "char":
                    return (ColumnInfo.AllowNull) ? $"Reader.IsDBNull(Reader.GetOrdinal(\"{ColumnInfo.Name}\")) ? null : Reader.GetChar(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))" : $"" +
                         $"Reader.GetChar(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))";

                case "TimeSpan":
                    return (ColumnInfo.AllowNull) ? $"Reader.IsDBNull(Reader.GetOrdinal(\"{ColumnInfo.Name}\")) ? null : Reader.GetTimeSpan(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))" : $"" +
                         $"Reader.GetTimeSpan(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))";

                case "DateTimeOffset":
                    return (ColumnInfo.AllowNull) ? $"Reader.IsDBNull(Reader.GetOrdinal(\"{ColumnInfo.Name}\")) ? null : Reader.GetDateTimeOffset(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))" : $"" +
                          $"Reader.GetDateTimeOffset(Reader.GetOrdinal(\"{ColumnInfo.Name}\"))";

                default:
                    return "";

            }


        }

        private static string _GenerateGetInfoByIDDataMethode()
        {
            stColumnInfo IdInfo = _GetIdInfo();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"public static " + $"{ClassName}DTO  Get{ClassName}InfoByID({IdInfo.DataType} {IdInfo.Name})" + @"
        {
             " + $"{ClassName}DTO {ClassName.ToLower()}DTO = null;" + @"
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand(""SP_Get" + $"{ClassName}InfoByID\", Connection))" + @"
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue(" + $"\"@{IdInfo.Name}\", {IdInfo.Name});" + @"
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                 
                                 " + $"{ClassName.ToLower()}DTO = new {ClassName}DTO(" +
                                  $" {string.Join("," + Environment.NewLine, _ListColumnsInfo.Select(Column => _ReadProprityFromReader(Column)))}" + @"
                                  );
                               
                            }
                            else
                                " + $"{ClassName.ToLower()}DTO = null;" + @"
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                clsEventLogData.WriteEvent($"" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}"", EventLogEntryType.Error);
                " + $"{ClassName.ToLower()}DTO = null;" + @"
            }
            return " + $"{ClassName.ToLower()}DTO;" + @"
            
        }
");
            return sb.ToString();
        }

        private static string _GenearteConstructorForClassDTO()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"public {ClassName}DTO({_GetListParametersWithDataType()}){{");

            foreach (stColumnInfo ColumnInfo in _ListColumnsInfo)
            {
                sb.AppendLine($"this.{ColumnInfo.Name} = {ColumnInfo.Name};");
            }
            sb.AppendLine($"}}");
            return sb.ToString();
        }
        private static string _GenerateClassDTO()
        {
            StringBuilder sbClassDTO = new StringBuilder();
            sbClassDTO.AppendLine($@"public class {ClassName}DTO
            {{
                  {_GenerateProperty()}
                  {_GenearteConstructorForClassDTO()}
            }}");

            return sbClassDTO.ToString();
        }

        private static string _GenerateSP_AddNewMethode()
        {
            stColumnInfo IdInfo = _GetIdInfo();

            return @"CREATE procedure SP_AddNew" + $"{ClassName}" + @"
                     " + $"@{IdInfo.Name} {_GetSqlDataTypeFromCSharpDataType(IdInfo.DataType)} output," + @"
                    " + $"{string.Join("," + Environment.NewLine, _ListColumnsInfo.Where(Column => Column.Name != IdInfo.Name).Select(Column => "@" + Column.Name + $" {_GetSqlDataTypeFromCSharpDataType(Column.DataType)}"))}" + @"
                    as
                    begin
                    insert into " + $"{SelectedTableName}({string.Join(",", _ListColumnsInfo.Where(Column => Column.Name != IdInfo.Name).Select(Column => Column.Name))}){Environment.NewLine}" + @"
                    values" + $"({string.Join(",", _ListColumnsInfo.Where(Column => Column.Name != IdInfo.Name).Select(Column => "@" + Column.Name))}){Environment.NewLine}" + @"

                    set " + $"@{IdInfo.Name}=SCOPE_IDENTITY(); " + @"
                    end";
        }

        private static string _GenerateSP_UpdateMethode()
        {


            return @"CREATE procedure SP_Update" + $"{ClassName}" + @"
                    " + $"{string.Join("," + Environment.NewLine, _ListColumnsInfo.Select(Column => "@" + Column.Name + $" {_GetSqlDataTypeFromCSharpDataType(Column.DataType)}"))}" + @"
                    as
                    begin
                    update " + $"{SelectedTableName} set {string.Join(",", _ListColumnsInfo.Where(Column => Column.Name != IDName).Select(Column => Column.Name + $" =@{Column.Name} "))} {Environment.NewLine}" + @"
                   

                     " + $"where {IDName}=@{IDName}" + @"

                      return @@ROWCOUNT;
                    end";
        }

        private static string _GenerateSP_DeleteMethode()
        {
            stColumnInfo IdInfo = _GetIdInfo();

            return @"CREATE procedure SP_Delete" + $"{ClassName}" + @"
                    " + $"@{IdInfo.Name} {_GetSqlDataTypeFromCSharpDataType(IdInfo.DataType)}" + @"
                    as
                    begin
                    delete from " + $"{SelectedTableName} where {IdInfo.Name}=@{IdInfo.Name}" + @"
                    return @@ROWCOUNT;
                    end";
        }

        private static string _GenerateSP_GetInfoByIDMethode()
        {
            stColumnInfo IdInfo = _GetIdInfo();

            return @"CREATE procedure SP_Get" + $"{ClassName}InfoByID" + @"
                    " + $"@{IdInfo.Name} {_GetSqlDataTypeFromCSharpDataType(IdInfo.DataType)}" + @"
                    as
                    begin
                    select * from " + $"{SelectedTableName} where {IdInfo.Name}=@{IdInfo.Name}" + @"
                    end";
        }

        private static string _GenerateSP_IsExistByIDMethode()
        {
            stColumnInfo IdInfo = _GetIdInfo();

            return @"CREATE procedure SP_Is" + $"{ClassName}ExistByID" + @"
                    " + $"@{IdInfo.Name} {_GetSqlDataTypeFromCSharpDataType(IdInfo.DataType)}" + @"
                    as
                    begin
                    " + $"if(exists( select isfound=1 from {SelectedTableName} where {IDName}=@{IDName}))" + @"
                    return 1;
                    else
                    return 0;
                    end";
        }

        private static string _GenerateSP_GetAllMethode()
        {
            stColumnInfo IdInfo = _GetIdInfo();

            return @"CREATE procedure SP_GetAll" + $"{SelectedTableName}" + @"
                    as
                    begin
                    select * from " + $"{SelectedTableName}" + @"
                    end";
        }

        // Public Function

        public static DataTable GetColumnsInfo(string SelectedTableName)
        {
            if (!IsConnectionValide())
                return null;
            return clsCodeGeneratoreData.GetColumnsInfoForTable(SelectedTableName);
        }

        public static  bool SetConnection(string Server,string DataBase,string UserId,string Password)
        {
            clsDataAccessSettings.SetConnectionString(Server, DataBase, UserId, Password);
            return clsCodeGeneratoreData.IsConnectionValide();
        }
        public static bool IsConnectionValide()
        {
            return clsCodeGeneratoreData.IsConnectionValide();
        }

        public static  DataTable GetAllTables()
        {
            if (!IsConnectionValide())
                return null;
            return clsCodeGeneratoreData.GetAllTables();
        }

        public static string GenerateBusinessLayer()
        {
            if (!IsConnectionValide())
                return "";

            StringBuilder sbBusinessLayer = new StringBuilder();
            
            
            sbBusinessLayer.AppendLine($"public enum enMode {{ AddNew = 0, Update = 1 }};\r\nprivate enMode Mode = enMode.AddNew;");

            sbBusinessLayer.AppendLine(_GenerateproprietyClassDTO());

            sbBusinessLayer.AppendLine(_GenerateProperty());


            sbBusinessLayer.AppendLine(_GenerateConstructor());

            sbBusinessLayer.AppendLine(_GenerateFindMethode());

            sbBusinessLayer.AppendLine(_GenerateAddNewMethode());

            sbBusinessLayer.AppendLine(_GenerateUpdateMethode());

            sbBusinessLayer.AppendLine(_GenerateSaveMethode());

            sbBusinessLayer.AppendLine(_GenerateDeleteMethode());

            sbBusinessLayer.AppendLine(_GenerateGetAllMethode());

            sbBusinessLayer.AppendLine(_GenerateISExistMethode());


            return sbBusinessLayer.ToString();
        }

        public static string GenerateDataAccessLayer()
        {
            if (!IsConnectionValide())
                return "";

            StringBuilder sbDataAccessLayer = new StringBuilder();

            sbDataAccessLayer.AppendLine(_GenerateClassDTO());
            sbDataAccessLayer.AppendLine(_GenerateAddNewDataMethode());
            sbDataAccessLayer.AppendLine(_GenerateUpdateDataMethode());
            sbDataAccessLayer.AppendLine(_GenerateDeleteDataMethode());
            sbDataAccessLayer.AppendLine(_GenerateGetAllDataMethode());
            sbDataAccessLayer.AppendLine(_GenerateIsExistByIDDataMethode());
            sbDataAccessLayer.AppendLine(_GenerateGetInfoByIDDataMethode());
            return sbDataAccessLayer.ToString();
        }

        public static string GenerateEventLog()
        {
            return @"using System;
                     using System.Diagnostics;
                     using Microsoft.Extensions.Logging.EventLog;
                     public class clsEventLogData
    {
        public static void WriteEvent(string Message, EventLogEntryType eventLogEntryType)
        {

            string SourceName = ""SourceName"";

            try
            {
                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, ""Application"");
                }

                EventLog.WriteEntry(SourceName, Message, eventLogEntryType);
            }
            catch (Exception Ex)
            {

            }
            
        }
                    ";
        }

        public static string GenerateDataAccessSettings()
        {
            if (!IsConnectionValide())
                return "";

            return @"
             static public class clsDataAccessSettings
             {
                     public static string ConnectionString = """+$"{ConnectionString}"+ @" "";
             }";
        }

        public static string GenerateStoredProcedure()
        {
            if (!IsConnectionValide())
                return "";

            StringBuilder sbStoredProcedure = new StringBuilder();

            sbStoredProcedure.AppendLine(_GenerateSP_AddNewMethode());
            sbStoredProcedure.AppendLine(_GenerateSP_UpdateMethode());
            sbStoredProcedure.AppendLine(_GenerateSP_DeleteMethode());
            sbStoredProcedure.AppendLine(_GenerateSP_GetInfoByIDMethode());
            sbStoredProcedure.AppendLine(_GenerateSP_IsExistByIDMethode());
            sbStoredProcedure.AppendLine(_GenerateSP_GetAllMethode());
            return sbStoredProcedure.ToString();
        }
    }
}
