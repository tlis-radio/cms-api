using System;

namespace Tlis.Cms.Infrastructure.Exceptions;

public class EntityNotFoundException(string? message = null) : Exception(message);