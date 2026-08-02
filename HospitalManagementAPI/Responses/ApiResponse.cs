namespace HospitalManagementAPI.Response;
public class ApiResponse<T>
{
    public bool success { get; set; }
    public int statusCode { get; set; }
    public string message { get; set; } = string.Empty;

    public T? data {get; set;}

    public ApiResponse(bool success, int statusCode, string message, T?data)
    {
        this.success = success;
        this.statusCode = statusCode;
        this.message = message;
        this.data = data;
    }

}