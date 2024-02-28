﻿using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositoties;

public class FeatureItemRepository(DataContext context) : Repo<FeatureItemEntity>(context)
{
    private readonly DataContext _context = context;
}
