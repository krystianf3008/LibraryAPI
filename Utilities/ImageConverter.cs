namespace LibraryAPI.Utilities
{
    public static class ImageConverter
    {
        public static string ConvertToBase64(IFormFile imageFile)
        {
            if (imageFile == null) return "";
            using (var memoryStream = new MemoryStream())
            {
                imageFile.CopyTo(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}
