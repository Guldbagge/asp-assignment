using Infrastructure.Models;

namespace WebApp.Models.Views;

public class CoursesIndexViewModel
{
    public IEnumerable<CourseModel> Courses { get; set; } = [];
    public IEnumerable<CategoryModel>? Categories { get; set; }
    public Pagination? Pagination { get; set; }
}
