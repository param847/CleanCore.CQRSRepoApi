namespace Domain.Exceptions
{
    /// <summary>
    /// Base for all domain‑level business rule violations.
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException() { }

        public DomainException(string message)
            : base(message) { }

        public DomainException(string message, Exception inner)
            : base(message, inner) { }
    }
}