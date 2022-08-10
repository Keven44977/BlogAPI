namespace Database
{
    public static class DatabaseConnexion
    {
        public static string GetConnexionString()
        {
            return "Host=localhost;Database=blog;Username=postgres;password=admin";
        }
    }
}
