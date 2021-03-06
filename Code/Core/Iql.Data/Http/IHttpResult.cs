﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace Iql.Data.Http
{
    public interface IHttpResult
    {
        bool IsOffline { get; set; }
        string ContentType { get; set; }
        HttpStatusCode StatusCode { get; set; }
        Func<Task<string>> GetResponseTextAsync { get; set; }
        Func<Task<byte[]>> GetResponseBytesAsync { get; set; }
        Func<Task<Stream>> GetResponseStreamAsync { get; set; }
        bool Success { get; set; }
    }
}