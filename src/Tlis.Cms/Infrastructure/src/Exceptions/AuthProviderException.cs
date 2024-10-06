using System;

namespace Tlis.Cms.Infrastructure.Exceptions;

public class AuthProviderException(string message) : Exception(message);