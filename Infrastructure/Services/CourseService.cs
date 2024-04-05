using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models.Views;

namespace Infrastructure.Services;

public class CourseService(HttpClient http, IConfiguration configuration)
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;

    public async Task<CourseResultModel> GetCoursesAsync(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 10)
    {
        var response = await _http.GetAsync($"{_configuration["ApiUris:Courses"]}?category={Uri.UnescapeDataString(category)}&searchQuery={Uri.UnescapeDataString(searchQuery)}&pageNumber={pageNumber}&pageSize={pageSize}");

        if (response.IsSuccessStatusCode)
        {
            var result = JsonConvert.DeserializeObject<CourseResultModel> (await response.Content.ReadAsStringAsync());
            if (result != null && result.Succeeded)
                return result;
                
        }

        return null!;

    }

    public async Task<CourseModel> GetCourseByIdAsync(int courseId)
    {
        var response = await _http.GetAsync($"{_configuration["ApiUris:Courses"]}/{courseId}");

        if (response.IsSuccessStatusCode)
        {
            var course = JsonConvert.DeserializeObject<CourseModel>(await response.Content.ReadAsStringAsync());
            return course;
        }

        return null;
    }

    public async Task<HttpResponseMessage> CreateCourseAsync(CourseModel course)
    {
        // Konvertera kursen till JSON-format
        var jsonCourse = JsonConvert.SerializeObject(course);
        // Skapa en HttpContent-objekt med JSON-data
        var content = new StringContent(jsonCourse, Encoding.UTF8, "application/json");

        // Utför en HTTP POST-förfrågan för att skapa kursen
        var response = await _http.PostAsync(_configuration["ApiUris:Courses"], content);

        // Returnera svaret från API:et
        return response;
    }

}
