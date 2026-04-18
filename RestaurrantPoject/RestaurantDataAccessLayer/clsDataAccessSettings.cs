using System.Configuration;

namespace RestaurantDataAccessLayer
{
    internal static class clsDataAccessSettings
    {
        public static string ConnectionString = "server=localhost; database = ResturantDB; user ID = sa; password = sa123456;";  // ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }
}
