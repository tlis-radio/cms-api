using System;

namespace Tlis.Cms.Infrastructure.Exceptions;

public class EntityAlreadyExistsException(string? message = null) : Exception(message);