using System;

namespace Tlis.Cms.Infrastructure.Exceptions;

public class AuthProviderUserAlreadyExistsException(string message) : Exception(message);