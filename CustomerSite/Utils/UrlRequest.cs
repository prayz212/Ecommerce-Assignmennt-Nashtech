namespace CustomerSite.Utils
{
    public class UrlRequest
    {
        public static string GET_URL_PRODUCTS_BY_CATEGORY(string category, int page, int size) => $"client/products/?category={category}&page={page}&size={size}";
        public static string GET_URL_PRODUCT_BY_ID(int id) => $"client/products/{id}";
        public static string GET_URL_FEATURE_PRODUCTS(int page, int size) => $"client/products/features?page={page}&size={size}";
        public static string GET_URL_RELATIVE_PRODUCTS(int id, int size) => $"client/products/relative/{id}?size={size}";
        public static string GET_URL_RATING_PRODUCT() => "client/products/rating";
        public static string GET_URL_CATEGORIES() => "client/categories";
        public static string GET_URL_LOGIN() => "authenticate/login/client";
        public static string GET_URL_REGISTER() => "authenticate/register/client";
    }
}