using Domain.Entities.Base;

namespace Domain.Entities.Logging
{
    /// <summary>
    /// Captures unhandled exception details for auditing.
    /// </summary>
    public class ExceptionLog : AuditableEntity
    {
        public string ExceptionType { get; private set; } = null!;
        public string Message { get; private set; } = null!;
        public string StackTrace { get; private set; } = null!;
        public string? AdditionalInfo { get; private set; }

        // Parameterless ctor for EF
        private ExceptionLog() { }

        public ExceptionLog(Exception ex, string? additionalInfo = null)
        {
            ExceptionType = ex.GetType().FullName!;
            Message = ex.Message;
            StackTrace = ex.StackTrace ?? string.Empty;
            AdditionalInfo = additionalInfo;
        }
    }
}