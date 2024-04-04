﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebApp.Models.Views;

namespace WebApp.Services;

public class CategoryService(HttpClient http, IConfiguration configuration)
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;

    public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync()
    {
        var response = await _http.GetAsync(_configuration["ApiUris:Categories"]);

        if(response.IsSuccessStatusCode)
        { 
        var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryModel>>(await response.Content.ReadAsStringAsync());
            return categories ??= null!;
        }

        return null!;

    }
}
