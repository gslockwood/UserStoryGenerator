namespace UserStoryGenerator.Utilities
{

    public class ImageLoader
    {
        /// <summary>
        /// Loads an Image from a specified file path.
        /// </summary>
        /// <param name="filePath">The full path to the image file.</param>
        /// <returns>An Image object if the file exists and is a valid image; otherwise, null.</returns>
        public static Image? GetImageFromFilePath(string filePath)
        {
            // Check if the file path is null or empty
            if( string.IsNullOrEmpty(filePath) )
            {
                Console.WriteLine("Error: File path cannot be null or empty.");
                return null;
            }

            // Check if the file actually exists at the given path
            if( !File.Exists(filePath) )
            {
                Console.WriteLine($"Error: File not found at '{filePath}'");
                return null;
            }

            try
            {
                // Create an Image object from the file path.
                // Using Image.FromFile can lock the file, preventing further modifications or deletions.
                // If you need to release the file lock immediately, consider loading it into a MemoryStream first.
                return Image.FromFile(filePath);
            }
            catch( OutOfMemoryException )
            {
                // This can happen if the file is not a valid image format or is corrupted.
                Console.WriteLine($"Error: '{filePath}' is not a valid image or is corrupted.");
                return null;
            }
            catch( FileNotFoundException )
            {
                // This catch block is technically redundant due to the File.Exists check,
                // but good for robustness in case of race conditions.
                Console.WriteLine($"Error: File not found at '{filePath}' (during loading).");
                return null;
            }
            catch( Exception ex )
            {
                // Catch any other unexpected errors during image loading
                Console.WriteLine($"An unexpected error occurred while loading image from '{filePath}': {ex.Message}");
                return null;
            }
        }

    }

}
