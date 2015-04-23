# Drip API Wrapper - .NET

An object-oriented .NET wrapper for [Drip's](https://www.getdrip.com/) [REST API v2.0](https://www.getdrip.com/docs/rest-api).
This implementation supports both synchronous and Task-based asynchronous calling semantics.

## Installation

### NuGet

File a request if you're interested in this. For now see below.

### From Binaries

Reference the bin/Debug or bin/Release dll's in your project.

### From Source

Clone the repo.

Open "DripDotNet-vs2013.sln" in Visual Studio 2013.

Build the DripDotNet-vs2013 project in Release or Debug mode.

Reference the DripDotNet.dll and RestSharp.dll in your .NET 4.5 project.

## Usage

Your account ID can be found [here](https://www.getdrip.com/settings/site).

For private integrations, you may use your personal API key (found
[here](https://www.getdrip.com/user/edit)) via the `apiKey` constructor parameter
or via the public properties on the DripClient:

```c#
var client = new Drip.DripClient("YOUR_API_KEY", "YOUR_ACCOUNT_ID");
```

or

```c#
var client = new Drip.DripClient {
	ApiKey = "YOUR_API_KEY",
	AccountId = "YOUR_ACCOUNT_ID"
};
```

Just make sure the API Key and Account ID are set prior to using the API.

For public integrations, pass in the user's OAuth token via the `AccessToken`
property:

```c#
var client = new Drip.DripClient {
	AccessToken = "YOUR_ACCESS_TOKEN",
	AccountId = "YOUR_ACCOUNT_ID"
};
```

Since the Drip client is a flat API client, most API actions are available
as methods on the client object. The following methods are currently available with
both synchronous and asynchronous signatures (only synchronous shown here):

| Action                     | Method                                                  |
| :------------------------- | :------------------------------------------------------ |
| Create/update a subscriber | `CreateOrUpdateSubscriber(subscriber)`                  |
| Create/update a batch of subscribers | `CreateOrUpdateSubscribers(subscribers)`      |
| Fetch a subscriber         | `GetSubscriber(idOrEmail)`                              |
| Subscribe to a campaign    | `SubscribeToCampaign(campaignId, campaignSubscriber)`   |
| Unsubscribe                | `UnsubscribeFromCampaign(idOrEmail, campaignId = null)` |
| Apply a tag                | `ApplyTagToSubscriber(email, tag)`                      |
| Remove a tag               | `RemoveTagFromSubscriber(email, tag)`                   |
| Track an event             | `TrackEvent(dripEvent)`                                 |
| Track a batch of events    | `TrackEvents(dripEvents)`                               |

Actions that require complex arguments take them in the form of a strongly typed 
`Request` object.

**Note:** We do not have complete API coverage yet. If we are missing an API method
that you need to use in your application, please file an issue and/or open a
pull request. [See the official REST API docs](https://www.getdrip.com/docs/rest-api)
for a complete API reference.

## Examples

```c#
var client = new Drip.DripClient("YOUR_API_KEY", "YOUR_ACCOUNT_ID");

// Fetch a subscriber
var resp = client.GetSubscriber("foo@example.com")

Debug.WriteLine("Did it work? " + resp.HasSuccessStatusCode());
//# Did it work? true

var subscriber = resp.Subscribers.First()
Debug.WriteLine("Subscriber Email: " + subscriber.Email);
//# Subscriber Email: foo@example.com
```

## Contributing

1. Fork it ( https://github.com/DripEmail/drip-dot-net/fork )
2. Create your feature branch (`git checkout -b my-new-feature`)
3. Commit your changes (`git commit -am 'Add some feature'`)
4. Push to the branch (`git push origin my-new-feature`)
5. Create a new Pull Request

## Testing

In order to run the tests you need to set two environment variables or you 
will get exceptions.

	DRIP_API_KEY="YOUR_API_KEY"
	DRIP_ACCOUNT_ID="YOUR_ACCOUNT_ID"

XUnit is used for testing. Install the xunit test runner to easily run from 
within the Visual Studio solution.

Since we do not yet support creating campaigns via API there is a TestCampaignId
constant in the CampaignTests.

## Known Issues

	1. Async operations that return empty results do not report the correct StatusCode on the DripResponse. This causes the associated integration tests to fail.
		a. ApplyTagToSubscriberAsync
		b. RemoveTagFromSubscriberAsync
		c. CreateOrUpdateSubscribersAsync
		d. TrackEventsAsync
