using Microsoft.AspNetCore.Mvc.Rendering;
using VirtualLibrary.Models;

namespace VirtualLibrary.Service
{
    public interface IAuthorsService
    {
        SelectList GetAllAuthors();
        AuthorsModel GetAuthorById(int id);
    }
}
