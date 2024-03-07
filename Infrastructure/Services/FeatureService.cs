using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositoties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class FeatureService(FeatureReposotory featureReposotory, FeatureItemRepository featureItemRepository)
{

    private readonly FeatureReposotory _featureReposotory = featureReposotory;
    private readonly FeatureItemRepository _featureItemRepository = featureItemRepository;

    public async Task<ResponseResult> GetAllFeaturesAsync()
    {
        try
        {
            var result = await _featureReposotory.GetAllAsync();
            return result;

        }

        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }
}
