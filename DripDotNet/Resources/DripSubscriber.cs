using Drip.Protocol;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Drip
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
        /// A dictionary containing custom field data. E.g. new Dictionary<string,string> { { "name" , "John Doe" } }
        /// </summary>
        [JsonConverter(typeof(UntouchableKeysConverter))]
        public Dictionary<string, string> CustomFields { get; set; }

        /// <summary>
        /// A list containing one or more tags. E.g. ["Customer", "SEO"].
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// The subscriber's Unique ID. This can be used in place of an email address on some API calls.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// The subscriber's status (i.e. "active")
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The subscriber's local timezone utc offset as reported by their browser.
        /// </summary>
        public int? UtcOffset { get; set; }

        /// <summary>
        /// The Uuid of the visitor.
        /// </summary>
        public string VisitorUuid { get; set; }

        /// <summary>
        /// The IP Address used by the subscriber to access your site.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// The user agent of the subscriber's web browser.
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// The Url that originally referred the subscriber to your site.
        /// </summary>
        public string OriginalReferrer { get; set; }

        /// <summary>
        /// When this subscriber was created in Drip.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// The REST endpoint that can be used to retrieve the subscriber.
        /// </summary>
        public string Href { get; set; }
    }

    public class ModifyDripSubscriberRequest
    {
        /// <summary>
        /// The subscriber's email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The subscriber's time zone (in Olsen format). Defaults to Etc/UTC
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        /// A dictionary containing custom field data. E.g. new Dictionary<string,string> { { "name" , "John Doe" } }
        /// </summary>
        [JsonConverter(typeof(UntouchableKeysConverter))]
        public Dictionary<string, string> CustomFields { get; set; }

        /// <summary>
        /// A list containing one or more tags. E.g. ["Customer", "SEO"].
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Optional. A new email address for the subscriber. If provided and a subscriber
        /// with the email does not exist, this address will be used to create a new subscriber.
        /// </summary>
        public string NewEmail { get; set; }

        /// <summary>
        /// A Boolean specifiying whether we should attach a lead score to the
        /// subscriber (when lead scoring is enabled). Defaults to false. This flag only
        /// applies to new subscribers — if the subscriber already exists, we will silently
        /// ignore this.
        /// </summary>
        public bool? PotentialLead { get; set; }
    }

    public class ModifyDripCampaignSubscriberRequest : ModifyDripSubscriberRequest
    {
        /// <summary>
        /// Optional. If true, the double opt-in confirmation email is sent; if false, 
        /// the confirmation email is skipped. Defaults to the value set on the campaign.
        /// </summary>
        public bool? DoubleOptin { get; set; }

        /// <summary>
        /// Optional. The index (zero-based) of the email to send first. Defaults to 0.
        /// </summary>
        public int? StartingEmailIndex { get; set; }

        /// <summary>
        /// Optional. If true, re-subscribe the subscriber to the campaign if there is 
        /// a removed subscriber in Drip with the same email address; otherwise, respond
        /// with 422 Unprocessable Entity. Defaults to true.
        /// </summary>
        public bool? ReactivateIfRemoved { get; set; }
    }

    /// <summary>
    /// A response that contains DripSubscriber items.
    /// </summary>
    public class DripSubscribersResponse : DripResponse
    {
        /// <summary>
        /// The Subscribers that were returned by the API endpoint.
        /// </summary>
        public List<DripSubscriber> Subscribers { get; set; }
    }
}
