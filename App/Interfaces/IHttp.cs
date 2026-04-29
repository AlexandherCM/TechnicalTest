using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Interfaces
{
    public enum HttpContentType { Json, Xml }

    public interface IHttp
    {
        Task<string> GetAsync(string endPoint, HttpContentType httpMethod = HttpContentType.Json, string jwt = "");
    }
}   
