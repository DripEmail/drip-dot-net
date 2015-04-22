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
        /// Optional. A Object containing custom event properties. If this event is
        /// a conversion, include the value (in cents) in the properties with a "value"
        /// key.
        /// </summary>
        public object Properties { get; set; }

        /// <summary>
        /// Optional. Defaults to the current time.
        /// </summary>
        public DateTime? OccurredAt { get; set; }
    }
}
