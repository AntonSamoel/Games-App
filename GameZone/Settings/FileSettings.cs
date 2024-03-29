namespace GameZone.Settings
{
    public static class FileSettings
    {
        public const string ImagesPath = "/assets/images/games";
        public const string AllowedExtensions = ".jpg,.jpeg,.png";
        public const int MaxSizeInMB = 3;
        public const int MaxSizeInBytes = MaxSizeInMB * 1024 *1024;
    }
}
