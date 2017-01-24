/*
 The MIT License (MIT)
 
 Copyright (c) 2015 - 2017 Avenue 81 Inc. d/b/a Leadpages, All Rights Reserved
 
 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to deal
 in the Software without restriction, including without limitation the rights
 to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:
 
 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
 
 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 SOFTWARE.
*/

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
