using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace DripDotNet
{
    public partial class DripClient
    {
        protected const string SubscribeToCampaignResource = "{accountId}/campaigns/{campaignId}/subscribers";
        protected const string CampaignIdUrlSegmentKey = "campaignId";
        protected const string CampaignIdQueryKey = CampaignIdUrlSegmentKey;
        protected const string UnsubscribeFromCampaignResource = "/{accountId}/subscribers/{subscriberId}/unsubscribe";
        protected const string SubscribersRequestBodyKey = "subscribers";

        /// <summary>
        /// Subscribe a Subscriber to a campaign.
        /// See: https://www.getdrip.com/docs/rest-api#subscribe
        /// </summary>
        /// <param name="campaignId">The campaign id.</param>
        /// <param name="campaignSubscriber">A ModifyDripCampaignSubscriberRequest containing at least an Email address.</param>
        /// <returns>A DripSubscribersResponse.</returns>
        public DripSubscribersResponse SubscribeToCampaign(string campaignId, ModifyDripCampaignSubscriberRequest campaignSubscriber)
        {
            return PostResource<DripSubscribersResponse>(SubscribeToCampaignResource, SubscribersRequestBodyKey, new ModifyDripCampaignSubscriberRequest[] { campaignSubscriber }, CampaignIdUrlSegmentKey, campaignId);
        }

        /// <summary>
        /// Subscribe a Subscriber to a campaign.
        /// See: https://www.getdrip.com/docs/rest-api#subscribe
        /// </summary>
        /// <param name="campaignId">The campaign id.</param>
        /// <param name="campaignSubscriber">A ModifyDripCampaignSubscriberRequest containing at least an Email address.</param>
        /// <param name="cancellationToken">The CancellationToken to be used to cancel the request.</param>
        /// <returns>A Task that, when completed, will contain a DripSubscribersResponse.</returns>
        public Task<DripSubscribersResponse> SubscribeToCampaignAsync(string campaignId, ModifyDripCampaignSubscriberRequest campaignSubscriber, CancellationToken cancellationToken = default(CancellationToken))
        {
            return PostResourceAsync<DripSubscribersResponse>(SubscribeToCampaignResource, SubscribersRequestBodyKey, new ModifyDripCampaignSubscriberRequest[] { campaignSubscriber }, cancellationToken, CampaignIdUrlSegmentKey, campaignId);
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
            return Execute<DripSubscribersResponse>(CreateUnsubscribeCampaignRequest(idOrEmail, campaignId));
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
            return ExecuteAsync<DripSubscribersResponse>(CreateUnsubscribeCampaignRequest(idOrEmail, campaignId), cancellationToken);
        }

        protected virtual IRestRequest CreateUnsubscribeCampaignRequest(string idOrEmail, string campaignId)
        {
            var req = CreatePostRequest(UnsubscribeFromCampaignResource, null, null, SubscriberIdUrlSegmentKey, idOrEmail);
            if (campaignId != null)
                req.AddQueryParameter(CampaignIdQueryKey, campaignId);
            return req;
        }
    }
}
