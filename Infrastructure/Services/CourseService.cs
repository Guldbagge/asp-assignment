﻿using Infrastructure.Models;
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
        //var response = await _http.GetAsync($"{_configuration["ApiUris:Courses"]}?category={Uri.UnescapeDataString(category)}&searchQuery={Uri.UnescapeDataString(searchQuery)}&pageNumber={pageNumber}&pageSize={pageSize}");
        var response = await _http.GetAsync($"{_configuration["ApiUris:Courses"]}?key=5e29b885-1414-4046-bb3c-33ac9c611b01&category={Uri.UnescapeDataString(category)}&searchQuery={Uri.UnescapeDataString(searchQuery)}&pageNumber={pageNumber}&pageSize={pageSize}");


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

    public async Task<HttpResponseMessage> AddCourseToSavedAsync(string userId, int courseId)
    {
        try
        {
            // Skapa en modell för att skicka data till API:et
            var userCourseModel = new UserCourseModel
            {
                UserId = userId,
                CourseId = courseId
            };

            // Konvertera modellen till JSON-format
            var jsonUserCourse = JsonConvert.SerializeObject(userCourseModel);

            // Skapa en HttpContent-objekt med JSON-data
            var content = new StringContent(jsonUserCourse, Encoding.UTF8, "application/json");

            // Utför en HTTP POST-förfrågan för att lägga till kursen i sparade kurser
            var response = await _http.PostAsync(_configuration["ApiUris:UserCourses"], content);

            // Returnera svaret från API:et
            return response;
        }
        catch (Exception ex)
        {
            // Logga felmeddelandet
            Console.WriteLine($"Error: {ex.Message}");
            throw; // Kasta vidare undantaget för att hanteras högre upp i stacken
        }
    }

    public async Task<List<CourseModel>> GetSavedCoursesAsync(string userId)
    {
        try
        {
            // Utför en HTTP GET-förfrågan för att hämta användarens sparade kurser
            var response = await _http.GetAsync($"{_configuration["ApiUris:UserCourses"]}?userId={userId}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialisera svaret till en lista med sparade kurser
                var savedCourses = JsonConvert.DeserializeObject<List<CourseModel>>(await response.Content.ReadAsStringAsync());
                return savedCourses;
            }
            else
            {
                // Om förfrågan misslyckades, returnera null eller kasta ett undantag, beroende på vad som är lämpligt för din applikation
                return null;
            }
        }
        catch (Exception ex)
        {
            // Logga felmeddelandet
            Console.WriteLine($"Error: {ex.Message}");
            throw; // Kasta vidare undantaget för att hanteras högre upp i stacken
        }
    }


}
