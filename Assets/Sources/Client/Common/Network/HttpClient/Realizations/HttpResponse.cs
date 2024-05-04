using System.Net;

namespace Client.Common.Network.HttpClient
{
    public struct HttpResponse<T>
    {
        public T Data { get; set; }
        public HttpStatusCode Status => (HttpStatusCode) Code;
        public long Code { get; set; }
        public bool IsSuccess => Code < 400;
        public string Message { get; set; }
        public string RawData { get; set; }

        public HttpResponse(object data, long code, string message, string rawData)
        {
            Data = (T) data;
            Code = code;
            Message = message;
            RawData = rawData;
        }

        public static implicit operator T(HttpResponse<T> other)
        {
            return other.Data;
        }

        public static implicit operator HttpResponse<T>(HttpResponse other)
        {
            return new HttpResponse<T>(other.Data, other.Code, other.Message, other.RawData);
        }

        public static implicit operator bool(HttpResponse<T> other)
        {
            return other.IsSuccess;
        }
    }
    
    public struct HttpResponse
    {
        public object Data { get; set; }
        public HttpStatusCode Status => (HttpStatusCode) Code;
        public long Code { get; set; }
        public bool IsSuccess => Code < 400;
        public string Message { get; set; }
        public string RawData { get; set; }

        public HttpResponse(object data, long code, string message, string rawData)
        {
            Data = data;
            Code = code;
            Message = message;
            RawData = rawData;
        }
        
        public static implicit operator bool(HttpResponse other)
        {
            return other.IsSuccess;
        }
    }
}