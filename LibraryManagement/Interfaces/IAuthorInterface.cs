﻿using LibraryManagement.Models.Domain;
using LibraryManagement.Models.Domain.Dto;

namespace LibraryManagement.Interfaces
{
    public interface IAuthorInterface
    {
        Task<List<Author>> GetallAsync();
        Task<Author> GetByIdAsync(int id);
        Task<Author> AddAuthorAsync(CreateAuthorDto author);
        Task<Author> UpdateAuthorAsync(UpdateAuthorDto author);
        Task<Author> DeleteAuthorAsync(int id);
    }
}
