
namespace Drip
{
    /// <summary>
    /// Some endpoints that return collections are paginated. For these endpoints, 
    /// the meta object will tell you the current page, count, total number of pages, 
    /// and total count of the collection.
    /// </summary>
    public class DripMeta
    {
        /// <summary>
        /// The page number contained in the current response.
        /// </summary>
        public long Page { get; set; }

        /// <summary>
        /// The number of items in the current page.
        /// </summary>
        public long Count { get; set; }

        /// <summary>
        /// The total number of pages in the collection.
        /// </summary>
        public long TotalPages { get; set; }

        /// <summary>
        /// The total number of items in the collection.
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// The field that was used to sort this collection.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// The direction of the sort.
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// The value of any "status" query supplied in a request.
        /// </summary>
        public string Status { get; set; }
    }
}
