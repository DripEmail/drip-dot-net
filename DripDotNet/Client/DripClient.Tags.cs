using RestSharp;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DripDotNet
{
    public partial class DripClient
    {
        protected const string ApplyTagToSubscriberResource = "/{accountId}/tags";
        protected const string TagsRequestBodyKey = "tags";
        protected const string RemoveTagFromSubscriberResource = "/{accountId}/subscribers/{subscriberId}/tags/{tag}";
        protected const string TagUrlSegmentKey = "tag";

        /// <summary>
        /// Apply a tag to a subscriber.
        /// See: https://www.getdrip.com/docs/rest-api#apply_tag
        /// </summary>
        /// <param name="email">The subscriber's email address.</param>
        /// <param name="tag">The tag to apply. E.g. "Customer"</param>
        /// <returns>On success, a DripResponse with StatusCode of Created.</returns>
        public DripResponse ApplyTagToSubscriber(string email, string tag)
        {
            return PostResource<DripResponse>(ApplyTagToSubscriberResource, TagsRequestBodyKey, new DripTag[] { new DripTag { Email = email, Tag = tag } });
        }

        /// <summary>
        /// Apply a tag to a subscriber.
        /// See: https://www.getdrip.com/docs/rest-api#apply_tag
        /// </summary>
        /// <param name="email">The subscriber's email address.</param>
        /// <param name="tag">The tag to apply. E.g. "Customer"</param>
        /// <param name="cancellationToken">The CancellationToken to be used to cancel the request.</param>
        /// <returns>A Task that, when completed successfully, will contain a StatusCode of NoContent.</returns>
        public Task<DripResponse> ApplyTagToSubscriberAsync(string email, string tag, CancellationToken cancellationToken = default(CancellationToken))
        {
            return PostResourceAsync<DripResponse>(ApplyTagToSubscriberResource, TagsRequestBodyKey, new DripTag[] { new DripTag { Email = email, Tag = tag } }, cancellationToken);
        }

        /// <summary>
        /// Remove a tag from a subscriber.
        /// See: https://www.getdrip.com/docs/rest-api#remove_tag
        /// </summary>
        /// <param name="email">The subscriber's email address.</param>
        /// <param name="tag">The tag to remove. E.g. "Customer"</param>
        /// <returns>On success, a DripResponse with a StatusCode of NoContent.</returns>
        public DripResponse RemoveTagFromSubscriber(string email, string tag)
        {
            return Execute<DripResponse>(CreateRemoveTagFromSubscriberRequest(email, tag));
        }

        /// <summary>
        /// Remove a tag from a subscriber.
        /// See: https://www.getdrip.com/docs/rest-api#remove_tag
        /// </summary>
        /// <param name="email">The subscriber's email address.</param>
        /// <param name="tag">The tag to remove. E.g. "Customer"</param>
        /// <param name="cancellationToken">The CancellationToken to be used to cancel the request.</param>
        /// <returns>A Task that, when completed successfully, will contain a DripResponse with a StatusCode of NoContent.</returns>
        public Task<DripResponse> RemoveTagFromSubscriberAsync(string email, string tag, CancellationToken cancellationToken = default(CancellationToken))
        {
            return ExecuteAsync<DripResponse>(CreateRemoveTagFromSubscriberRequest(email, tag), cancellationToken);
        }

        protected virtual IRestRequest CreateRemoveTagFromSubscriberRequest(string email, string tag)
        {
            var req = CreatePostRequest(RemoveTagFromSubscriberResource, null, null, SubscriberIdUrlSegmentKey, email);
            if (tag != null)
                req.AddUrlSegment(TagUrlSegmentKey, tag);
            return req;
        }
    }
}
