namespace CustomerSite.Utils
{
    public class UrlRequest
    {
        public static string GET_URL_PRODUCTS_BY_CATEGORY(string category, int page, int size) => $"products/?category={category}&page={page}&size={size}";
        public static string GET_URL_PRODUCT_BY_ID(int id) => $"products/{id}";
        public static string GET_URL_FEATURE_PRODUCTS(int page, int size) => $"products/features?page={page}&size={size}";
        public static string GET_URL_RELATIVE_PRODUCTS(int id, int size) => $"products/relative/{id}?size={size}";
        public static string GET_URL_RATING_PRODUCT() => "products/rating";
        public static string GET_URL_CATEGORIES() => "categories";
    }
}