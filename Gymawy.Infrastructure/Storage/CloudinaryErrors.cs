using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Storage
{
    public static class CloudinaryErrors
    {
        public static readonly Error EmptyFile = Error.Validation(code: "Cloudinary.EmptyFile", description: "File is empty");

        public static readonly Error UnsupportedPhotoType = Error.Validation(code: "Cloudinary.UnsupportedPhotoType", description: "Invalid file type. Only JPG,JPEG and PNG are allowed.");

        public static readonly Error FailedToUpload = Error.Failure(code: "Cloudinary.FailedToUpload", description: "Failed to upload photo.");
        public static readonly Error FailedToDelete = Error.Failure(code: "Cloudinary.FailedToDelete", description: "Failed to delete photo.");


    }
}
