namespace Juan.Extensions
{
    public static partial class Extensions
    {
        public static bool IsImage(this IFormFile file)=>
            file.ContentType.Contains("image"); 
        public static bool IsCorrectSize(this IFormFile file, int size)=>
            file.Length/1024>size;
    }
}
