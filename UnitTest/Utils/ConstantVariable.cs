namespace UnitTest.Utils
{
    public static class ConstantVariable
    {
        //HTTP CODE
        public const int NOT_FOUND_STATUS_CODE = 404;
        public const int BAD_REQUEST_STATUS_CODE = 400;
        public const int OK_STATUS_CODE = 200;
        public const int CREATED_STATUS_CODE = 201;

        //Url
        public const string CLIENT_NAME = "API_SERVER";
        public const string BASE_URL = "https://localhost:4546/";

        //Cache
        public const string CATEGORY_CACHE_KEY = "CATEGORY_CACHE";
    }
}