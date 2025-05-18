using ErrorOr;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Abstractions.Storage
{
    public interface ICloudinaryService
    {
        Task<ErrorOr<string>>  UploadUserPhotoAsync(IFormFile file, Guid userId);
        Task<ErrorOr<string>> UploadGymPhotoAsync(IFormFile file, Guid gymId);
        Task<ErrorOr<Success>> DeleteUserPhotoAsync(Guid userId);

    }
}
