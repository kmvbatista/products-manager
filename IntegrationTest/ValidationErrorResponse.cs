namespace IntegrationTest
{
  public class ValidationErrorResponse
  {
    public string _Key { get; set; }
    public string _Message { get; set; }
    public ValidationErrorResponse(string Key, string Message)
    {
      _Key = Key;
      _Message = Message;
    }
  }
}
