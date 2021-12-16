public static class FunctionLoggingExtensions
{
	public static void LogCustom(this ILogger logger, string message)
	{
		logger.LogDebug(
			Events.Custom,
			GlobalConstants.LogMessageTemplate,
			new[] {
				message,
			}
		);
	}
}
