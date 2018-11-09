using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

[ServiceFilter(typeof(LoggingActionFilter))]
public class LoggingActionFilter : IActionFilter
{
    private readonly ILogger _logger;
    public LoggingActionFilter(ILogger<LoggingActionFilter> logger) {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogDebug($"LoggingActionFilter_OnActionExecuting: {context.ActionDescriptor.DisplayName}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogDebug($"LoggingActionFilter_OnActionExecuted: {context.ActionDescriptor.DisplayName}");
    }
}