namespace StudentsAPIClient.Services
{
    public interface IAccessTokenStore
    {
        string AccessToken { get; set; }
    }
}