﻿using FoP_IMT.Domain.Entities.Shared.Resources;
using FoP_IMT.Shared.Data.Enums.Shared.Resources;

namespace FoP_IMT.Domain.Repositories.Shared.Resources
{
    public interface IResourceRepository : IRepository
    {
        Task<Resource?> Load(LanguageCode language, string resourceCode);
        Task<IList<Resource>> LoadAll(LanguageCode language);
    }
}
