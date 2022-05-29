using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educal.Core.Models;
using Educal.Database.Repositories.BaseRepositories;

namespace Educal.Database.Repositories.LessonRepositories
{
    public interface ILessonRepository : IBaseRepository<Lesson>
    {
        Task<Lesson> GetLessonByContainName(string LessonName);
        Task<Lesson> GetLessonByName(string Name);
    }
}