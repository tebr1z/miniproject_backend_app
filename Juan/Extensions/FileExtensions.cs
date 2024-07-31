public static class Extensions
{
    public static bool IsImage(this IFormFile file)
    {
        if (file == null) throw new ArgumentNullException(nameof(file));

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
        var extension = Path.GetExtension(file.FileName);

        return allowedExtensions.Contains(extension.ToLower());
    }

    public static bool IsCorrectSize(this IFormFile file, int sizeInKB)
    {
        if (file == null) throw new ArgumentNullException(nameof(file));
        return file.Length / 1024 <= sizeInKB;
    }

    public static async Task<string> SaveFileAsync(this IFormFile file)
    {
        if (file == null) throw new ArgumentNullException(nameof(file));

        var filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img","product", filename);

        try
        {
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        catch (Exception ex)
        {
            throw new IOException("An error occurred while saving the file.", ex);
        }

        return filename;
    }
}
