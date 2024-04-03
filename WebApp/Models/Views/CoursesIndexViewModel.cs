namespace WebApp.Models.Views;

public class CoursesIndexViewModel
{
    public IEnumerable<CourseModel> Courses { get; set; } = [];
    public IEnumerable<CategoryModel>? Categories { get; set; }
}
