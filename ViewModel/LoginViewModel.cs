namespace Online_Exam.ViewModel;

public partial class LoginViewModel : ObservableObject
{
    private readonly Database _db;

    public LoginViewModel(Database db)
    {
        _db = db;
    }

    public List<string> Roles { get; } = new() { "Admin", "Teacher", "Student" };

    [ObservableProperty]
    private string selectedRole = "Admin";

    [ObservableProperty]
    private string usernameOrRegNo = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private bool isPasswordVisible = true;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    partial void OnSelectedRoleChanged(string value)
    {
        // Students log in using only Reg_no in usp_Student_Login
        IsPasswordVisible = value != "Student";
        ErrorMessage = string.Empty;
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (IsBusy) return;

        if (string.IsNullOrWhiteSpace(UsernameOrRegNo))
        {
            ErrorMessage = SelectedRole == "Student"
                ? "Please enter your Registration Number."
                : "Please enter your Username.";
            return;
        }

        if (SelectedRole != "Student" && string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Please enter your Password.";
            return;
        }

        try
        {
            IsBusy = true;
            ErrorMessage = string.Empty;

            if (SelectedRole == "Admin")
            {
                var admin = await _db.AdminLoginAsync(UsernameOrRegNo.Trim());
                if (admin != null && admin.PasswordHash == Password)
                {
                    await Shell.Current.DisplayAlert("Success", $"Welcome Admin: {admin.Username}", "OK");
                    // Next step: Navigate to AdminDashboardPage
                }
                else
                {
                    ErrorMessage = "Invalid Admin username or password.";
                }
            }
            else if (SelectedRole == "Teacher")
            {
                var teacher = await _db.TeacherLoginAsync(UsernameOrRegNo.Trim());
                if (teacher != null && teacher.PasswordHash == Password)
                {
                    await Shell.Current.DisplayAlert("Success", $"Welcome {teacher.Name} ({teacher.Dept})", "OK");
                    // Next step: Navigate to TeacherDashboardPage
                }
                else
                {
                    ErrorMessage = "Invalid Teacher username or password.";
                }
            }
            else if (SelectedRole == "Student")
            {
                var student = await _db.StudentLoginAsync(UsernameOrRegNo.Trim());
                if (student != null)
                {
                    await Shell.Current.DisplayAlert("Success", $"Welcome {student.Name} ({student.DegreeName})", "OK");
                    // Next step: Navigate to StudentDashboardPage
                }
                else
                {
                    ErrorMessage = "Registration number not found.";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Database Error: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }
}