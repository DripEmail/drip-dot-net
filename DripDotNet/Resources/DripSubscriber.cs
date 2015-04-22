using System.Collections.Generic;

namespace DripDotNet
{
    /// <summary>
    /// Represents a Subscriber in a Drip account.
    /// </summary>
    public class DripSubscriber
    {
        /// <summary>
        /// The subscriber's email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The subscriber's time zone (in Olsen format). Defaults to Etc/UTC
        /// </summary>
        public string Timezone { get; set; }

        /// <summary>
        /// An Object containing custom field data. E.g. new { name = "John Doe" }
        /// </summary>
        public object CustomFields { get; set; }

        /// <summary>
        /// A list containing one or more tags. E.g. ["Customer", "SEO"].
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// A Boolean specifiying whether we should attach a lead score to the
        /// subscriber (when lead scoring is enabled). Defaults to false. This flag only
        /// applies to new subscribers — if the subscriber already exists, we will silently
        /// ignore this.
        /// </summary>
        public bool PotentialLead { get; set; }
    }

    public class ModifyDripSubscriber : DripSubscriber
    {
        /// <summary>
        /// Optional. A new email address for the subscriber. If provided and a subscriber
        /// with the email does not exist, this address will be used to create a new subscriber.
        /// </summary>
        public string NewEmail { get; set; }
    }

    public class ModifyDripCampaignSubscriber : DripSubscriber
    {
        /// <summary>
        /// Optional. If true, the double opt-in confirmation email is sent; if false, 
        /// the confirmation email is skipped. Defaults to the value set on the campaign.
        /// </summary>
        public bool DoubleOptIn { get; set; }

        /// <summary>
        /// Optional. The index (zero-based) of the email to send first. Defaults to 0.
        /// </summary>
        public int StartingEmailIndex { get; set; }

        /// <summary>
        /// Optional. If true, re-subscribe the subscriber to the campaign if there is 
        /// a removed subscriber in Drip with the same email address; otherwise, respond
        /// with 422 Unprocessable Entity. Defaults to true.
        /// </summary>
        public bool ReactivateIfRemoved { get; set; }
    }
}
