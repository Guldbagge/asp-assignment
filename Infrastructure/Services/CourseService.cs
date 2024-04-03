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

    public async Task<IEnumerable<CourseModel>> GetCoursesAsync()
    {
        var response = await _http.GetAsync(_configuration["ApiUris:Courses"]);

        if (response.IsSuccessStatusCode)
        {
            var result = JsonConvert.DeserializeObject<CourseResultModel> (await response.Content.ReadAsStringAsync());
            if (result != null && result.Succeeded)

                return result.Courses ??= null!;
        }

        return null!;

    }
}
