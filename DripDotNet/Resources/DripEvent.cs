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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drip
{
    public class DripEvent
    {
        /// <summary>
        /// The subscriber's email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The name of the action taken. E.g. "Logged in"
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Optional. A Boolean specifiying whether we should attach a lead score
        /// to the subscriber (when lead scoring is enabled). Defaults to false. 
        /// This flag only applies to new subscribers — if the subscriber already 
        /// exists, we will silently ignore this.
        /// </summary>
        public bool? PotentialLead { get; set; }

        /// <summary>
        /// Optional. A dictionary containing custom event properties. If this event is
        /// a conversion, include the value (in cents) in the properties with a "value"
        /// key.
        /// </summary>
        public Dictionary<string, string> Properties { get; set; }

        /// <summary>
        /// Optional. Defaults to the current time.
        /// </summary>
        public DateTime? OccurredAt { get; set; }
    }
}
