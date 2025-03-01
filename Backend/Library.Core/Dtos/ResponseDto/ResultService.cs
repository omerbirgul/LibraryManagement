using System.Net;
using System.Text.Json.Serialization;

namespace Library.Core.Dtos.ResponseDto;

public class ResultService<T> where T : class
{
    public T? Data { get; set; }
    public List<string>? ErrorMessage { get; set; }
    [JsonIgnore] public HttpStatusCode StatusCode { get; set; }
    [JsonIgnore] public bool IsSuccess => ErrorMessage is null || ErrorMessage.Count == 0;
    [JsonIgnore] public bool IsFail => !IsSuccess;

    public static ResultService<T> Succcess(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ResultService<T>()
        {
            Data = data,
            StatusCode = statusCode
        };
    }

    public static ResultService<T> Fail(List<string> errorMessages,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ResultService<T>()
        {
            ErrorMessage = errorMessages,
            StatusCode = statusCode
        };
    }

    public static ResultService<T> Fail(string errorMessage,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ResultService<T>()
        {
            ErrorMessage = new List<string>() { errorMessage },
            StatusCode = statusCode
        };
    }
}

//********************************************************************

public class ResultService
{
    public List<string>? ErrorMessage { get; set; }
    [JsonIgnore] public HttpStatusCode StatusCode { get; set; }
    [JsonIgnore] public bool IsSuccess => ErrorMessage is null || ErrorMessage.Count == 0;
    [JsonIgnore] public bool IsFail => !IsSuccess;


    public static ResultService Success(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ResultService()
        {
            StatusCode = statusCode
        };
    }

    public static ResultService Fail(List<string> errorMessage,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ResultService()
        {
            ErrorMessage = errorMessage,
            StatusCode = statusCode
        };
    }

    public static ResultService Fail(string errorMessage,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ResultService()
        {
            ErrorMessage = new List<string>() { errorMessage },
            StatusCode = statusCode
        };
    }
}