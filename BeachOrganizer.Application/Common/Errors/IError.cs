﻿using System.Net;

namespace BeachOrganizer.Application.Common.Errors;

public interface IError
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }
}