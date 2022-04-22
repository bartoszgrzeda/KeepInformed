namespace KeepInformed.Web.Api.Models;

public class Response
{
    public object? ResponseModel { get; set; }
    public string? Message { get; set; }

    public Response(object? responseModel)
    {
        ResponseModel = responseModel;
    }

    public Response(string message)
    {
        Message = message;
    }

    public Response(object responseModel, string message)
    {
        ResponseModel = responseModel;
        Message = message;
    }
}
