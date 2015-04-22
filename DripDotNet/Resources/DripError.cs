
namespace DripDotNet
{
    /// <summary>
    /// Drip returns error codes to all requests in a standard format.
    /// You can find the errors, if there are any, on the DripResponse object.
    /// See: https://www.getdrip.com/docs/rest-api#errors
    /// </summary>
    public class DripError
    {
        /// <summary>
        /// One of the well-known Drip error codes.
        /// See: https://www.getdrip.com/docs/rest-api#errors
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// A detailed error message, useful for debugging.
        /// </summary>
        public string Message { get; set; }
    }
}
