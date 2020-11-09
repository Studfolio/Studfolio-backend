using Studfolio.FileService.Models.Dto;
using System;

namespace Studfolio.FileService.Business.Interfaces
{
    /// <summary>
    /// Represents interface for a command in command pattern.
    /// Provides method for adding a new file.
    /// </summary>
    public interface IAddNewFileCommand
    {
        /// <summary>
        /// Adds a new file. Returns id of the added file.
        /// </summary>
        /// <param name="request">File data.</param>
        /// <returns>Id of the added file.</returns>
        /// <exception cref="ValidationException">Thrown when file data is incorrect.</exception>
        Guid Execute(FileCreateRequest request);
    }
}
