namespace Loans.Frontend.Common
{
    public class ApiEndpoints
    {
        public const string API_URL = "http://localhost:5225";

        public const string FUNCTIONS_URL = "http://localhost:7071/api";
        
        public const string LOAN_ADD_ENDPOINT = $"{API_URL}/api/loans/add";

        public const string LOAN_GET_ENDPOINT = $"{API_URL}/api/loans/get";

        public const string LOANS_LIST_GET_ENDPOINT = $"{API_URL}/api/loans/list";

        public const string LOG_IN_ENDPOINT = $"{API_URL}/api/user/logIn";

        public const string LOG_OUT_ENDPOINT = $"{API_URL}/api/user/logOut";

        public const string SIGN_UP_ENDPOINT = $"{API_URL}/api/user/add";
    }
}