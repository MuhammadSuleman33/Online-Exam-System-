namespace Online_Exam.Models;

// 1. Users & Authentication
public class AdminModel
{
    public int AdminId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}

public class TeacherModel
{
    public int TeacherId { get; set; }
    public string EmployeeNo { get; set; } = string.Empty;
    public string Dept { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}

public class StudentModel
{
    public int StudentId { get; set; }
    public string RegNo { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int SessionId { get; set; }
    public string SessionName { get; set; } = string.Empty;
    public int DegreeId { get; set; }
    public string DegreeName { get; set; } = string.Empty;
    public int SemesterId { get; set; }
    public byte SemesterNo { get; set; }
}

// 2. Lookup Entities
public class SessionModel
{
    public int SessionId { get; set; }
    public string SessionName { get; set; } = string.Empty;
}

public class ExamModel
{
    public int ExamId { get; set; }
    public string ExamCode { get; set; } = string.Empty;
    public string? ExamName { get; set; }
}

public class DegreeModel
{
    public int DegreeId { get; set; }
    public string DegreeName { get; set; } = string.Empty;
}

public class SemesterModel
{
    public int SemesterId { get; set; }
    public byte SemesterNo { get; set; }
    public byte? YearNo { get; set; }
}

public class SubjectModel
{
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public int DegreeId { get; set; }
    public string DegreeName { get; set; } = string.Empty;
    public int SemesterId { get; set; }
    public byte SemesterNo { get; set; }
}

// 3. Papers & Questions
public class PaperModel
{
    public int PaperId { get; set; }
    public string SessionName { get; set; } = string.Empty;
    public string ExamCode { get; set; } = string.Empty;
    public string DegreeName { get; set; } = string.Empty;
    public byte SemesterNo { get; set; }
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public string CreatedByName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int QuestionCount { get; set; }
}

public class QuestionModel
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string OptionA { get; set; } = string.Empty;
    public string OptionB { get; set; } = string.Empty;
    public string OptionC { get; set; } = string.Empty;
    public string OptionD { get; set; } = string.Empty;
    public string? CorrectOption { get; set; } // Null when viewed by a Student
    public string? SelectedOption { get; set; } // Used in UI when student attempts exam
}