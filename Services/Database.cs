using System.Data;
using Microsoft.Data.SqlClient;
using Online_Exam.Models;

namespace Online_Exam.Services;

public class Database
{
#if ANDROID
    private const string ServerAddress = "tcp:10.0.2.2,1433";
#else
    private const string ServerAddress = "localhost,1433";
#endif

    // Encrypt=Optional (or False) + Pooling=True keeps the socket open after the first login!
    private static readonly string _connectionString =
        $"Server={ServerAddress};Database=Online Exam System;User Id=Online_exam;Password=Sallu@31;" +
        "Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;Pooling=True;Min Pool Size=1;Max Pool Size=20;";

    public SqlConnection GetConnection() => new SqlConnection(_connectionString);

    // --------------------- AUTHENTICATION -------------------------

    public Task<AdminModel?> AdminLoginAsync(string username) => Task.Run(async () =>
    {
        using var conn = GetConnection();
        using var cmd = new SqlCommand("usp_Admin_Login", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@username", username);

        await conn.OpenAsync().ConfigureAwait(false);
        using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
        if (await reader.ReadAsync().ConfigureAwait(false))
        {
            return new AdminModel
            {
                AdminId = reader.GetInt32(reader.GetOrdinal("Admin_id")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                PasswordHash = reader.GetString(reader.GetOrdinal("Password_Hash"))
            };
        }
        return null;
    });

    public Task<TeacherModel?> TeacherLoginAsync(string username) => Task.Run(async () =>
    {
        using var conn = GetConnection();
        using var cmd = new SqlCommand("usp_Teacher_Login", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@username", username);

        await conn.OpenAsync().ConfigureAwait(false);
        using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
        if (await reader.ReadAsync().ConfigureAwait(false))
        {
            return new TeacherModel
            {
                TeacherId = reader.GetInt32(reader.GetOrdinal("Teacher_id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Dept = reader.GetString(reader.GetOrdinal("Dept")),
                PasswordHash = reader.GetString(reader.GetOrdinal("Password_hash")),
                Username = username
            };
        }
        return null;
    });

    public Task<StudentModel?> StudentLoginAsync(string regNo) => Task.Run(async () =>
    {
        using var conn = GetConnection();
        using var cmd = new SqlCommand("usp_Student_Login", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@reg_no", regNo);

        await conn.OpenAsync().ConfigureAwait(false);
        using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
        if (await reader.ReadAsync().ConfigureAwait(false))
        {
            return new StudentModel
            {
                StudentId = reader.GetInt32(reader.GetOrdinal("Student_id")),
                RegNo = reader.GetString(reader.GetOrdinal("Reg_no")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                SessionId = reader.GetInt32(reader.GetOrdinal("Session_id")),
                SessionName = reader.GetString(reader.GetOrdinal("Session_name")),
                DegreeId = reader.GetInt32(reader.GetOrdinal("Degree_id")),
                DegreeName = reader.GetString(reader.GetOrdinal("Degree_name")),
                SemesterId = reader.GetInt32(reader.GetOrdinal("Semester_id")),
                SemesterNo = reader.GetByte(reader.GetOrdinal("Semester_no"))
            };
        }
        return null;
    });
}