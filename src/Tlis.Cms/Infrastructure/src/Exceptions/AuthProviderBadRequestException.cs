using System;

namespace Tlis.Cms.Infrastructure.Exceptions;

public class AuthProviderBadRequestException(string message) : Exception(message);