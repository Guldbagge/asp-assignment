using Infrastructure.Models;

namespace WebApp.Models.Views;

public class SavedCourseViewModel
{
    public IEnumerable<UserSavedCourseModel> Courses { get; set; } = [];
    public UserCourseModel UserCourse { get; set; } = new UserCourseModel();
}
