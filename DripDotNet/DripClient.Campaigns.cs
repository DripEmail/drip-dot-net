using System;
using System.Threading;
using System.Threading.Tasks;

namespace DripDotNet
{
    public partial class DripClient
    {
        /// <summary>
        /// Subscribe a Subscriber to a campaign.
        /// See: https://www.getdrip.com/docs/rest-api#subscribe
        /// </summary>
        /// <param name="campaignId">The campaign id.</param>
        /// <param name="campaignSubscriber">A ModifyDripCampaignSubscriber containing at least an Email address.</param>
        /// <returns>A DripSubscribersResponse.</returns>
        public DripSubscribersResponse SubscribeToCampaign(string campaignId, ModifyDripCampaignSubscriber campaignSubscriber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Subscribe a Subscriber to a campaign.
        /// See: https://www.getdrip.com/docs/rest-api#subscribe
        /// </summary>
        /// <param name="campaignId">The campaign id.</param>
        /// <param name="campaignSubscriber">A ModifyDripCampaignSubscriber containing at least an Email address.</param>
        /// <param name="cancellationToken">The CancellationToken to be used to cancel the request.</param>
        /// <returns>A Task that, when completed, will contain a DripSubscribersResponse.</returns>
        public Task<DripSubscribersResponse> SubscribeToCampaignAsync(string campaignId, ModifyDripCampaignSubscriber campaignSubscriber, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Unsubscribe a subscriber globally or from one specific campaign.
        /// See: https://www.getdrip.com/docs/rest-api#unsubscribe
        /// </summary>
        /// <param name="idOrEmail">Required. The id or email address of the subscriber.</param>
        /// <param name="campaignId">Optional. The campaign from which to unsubscribe the subscriber. Defaults to all.</param>
        /// <returns>A DripSubscribersResponse.</returns>
        public DripSubscribersResponse UnsubscribeFromCampaign(string idOrEmail, string campaignId = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Unsubscribe a subscriber globally or from one specific campaign.
        /// See: https://www.getdrip.com/docs/rest-api#unsubscribe
        /// </summary>
        /// <param name="idOrEmail">Required. The id or email address of the subscriber.</param>
        /// <param name="campaignId">Optional. The campaign from which to unsubscribe the subscriber. Defaults to all.</param>
        /// <param name="cancellationToken">The CancellationToken to be used to cancel the request.</param>
        /// <returns>A Task that, when completed, will contain a DripSubscribersResponse.</returns>
        public Task<DripSubscribersResponse> UnsubscribeFromCampaignAsync(string idOrEmail, string campaignId = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
