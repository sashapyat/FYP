using Core.Persistence.Context;
using Core.Persistence.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using VirtualLibrary.Models;

namespace VirtualLibrary.Service
{
    public class AuthorsService : IAuthorsService
    {

        private readonly AppDbContext _appDbContext;
        public AuthorsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public SelectList GetAllAuthors()
        {
            var allAuthors = (
                from a in _appDbContext.Authors
                select new
                {
                    AuthorId = a.AuthorId,
                    AuthorFullName = string.Format("{0} {1}", a.AuthorFirstName, a.AuthorLastName)
                }).ToList();


            return new SelectList(allAuthors, "AuthorId", "AuthorFullName");
        }

        public AuthorsModel GetAuthorById(int id)
        {
            var author = (from a in _appDbContext.Authors
                          where a.AuthorId== id
                          select new AuthorsModel
                          { 
                              AuthorId = a.AuthorId,
                              AuthorFirstName = a.AuthorFirstName,
                              AuthorLastName = a.AuthorLastName
                          }).FirstOrDefault();

            return author;
        }

    }
}
