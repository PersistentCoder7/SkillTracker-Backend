using SkillTracker.Search.Domain.Models;

namespace SkillTracker.Search.Cache.Interfaces;

public interface ICacheRepository
{
    Task<List<CachedProfile>>  Read();
    Task Write(string json);
}