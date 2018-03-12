namespace Iql.Queryable.Data.Http
{
    public enum HttpStatusCode
    {
        Continue = 100, // 0x00000064
        SwitchingProtocols = 101, // 0x00000065
        Ok = 200, // 0x000000C8
        Created = 201, // 0x000000C9
        Accepted = 202, // 0x000000CA
        NonAuthoritativeInformation = 203, // 0x000000CB
        NoContent = 204, // 0x000000CC
        ResetContent = 205, // 0x000000CD
        PartialContent = 206, // 0x000000CE
        Ambiguous = 300, // 0x0000012C
        MultipleChoices = 300, // 0x0000012C
        Moved = 301, // 0x0000012D
        MovedPermanently = 301, // 0x0000012D
        Found = 302, // 0x0000012E
        Redirect = 302, // 0x0000012E
        RedirectMethod = 303, // 0x0000012F
        SeeOther = 303, // 0x0000012F
        NotModified = 304, // 0x00000130
        UseProxy = 305, // 0x00000131
        Unused = 306, // 0x00000132
        RedirectKeepVerb = 307, // 0x00000133
        TemporaryRedirect = 307, // 0x00000133
        BadRequest = 400, // 0x00000190
        Unauthorized = 401, // 0x00000191
        PaymentRequired = 402, // 0x00000192
        Forbidden = 403, // 0x00000193
        NotFound = 404, // 0x00000194
        MethodNotAllowed = 405, // 0x00000195
        NotAcceptable = 406, // 0x00000196
        ProxyAuthenticationRequired = 407, // 0x00000197
        RequestTimeout = 408, // 0x00000198
        Conflict = 409, // 0x00000199
        Gone = 410, // 0x0000019A
        LengthRequired = 411, // 0x0000019B
        PreconditionFailed = 412, // 0x0000019C
        RequestEntityTooLarge = 413, // 0x0000019D
        RequestUriTooLong = 414, // 0x0000019E
        UnsupportedMediaType = 415, // 0x0000019F
        RequestedRangeNotSatisfiable = 416, // 0x000001A0
        ExpectationFailed = 417, // 0x000001A1
        UpgradeRequired = 426, // 0x000001AA
        InternalServerError = 500, // 0x000001F4
        NotImplemented = 501, // 0x000001F5
        BadGateway = 502, // 0x000001F6
        ServiceUnavailable = 503, // 0x000001F7
        GatewayTimeout = 504, // 0x000001F8
        HttpVersionNotSupported = 505, // 0x000001F9
    }
}