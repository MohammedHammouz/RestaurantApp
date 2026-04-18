using System.Configuration;

namespace RestaurantDataAccessLayer
{
    internal static class clsDataAccessSettings
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }
}
