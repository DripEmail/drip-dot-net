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
using Drip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DripDotNetTests
{
    public class DripClientFixture: IDisposable
    {
        public string ApiKey { get; private set; }
        public string AccountId { get; private set; }
        public DripClient Client { get; private set; }

        public DripClientFixture()
        {
            ApiKey = Environment.GetEnvironmentVariable("DRIP_API_KEY");
            AccountId = Environment.GetEnvironmentVariable("DRIP_ACCOUNT_ID");

            if (string.IsNullOrEmpty(ApiKey))
                throw new InvalidOperationException("DRIP_API_KEY environment variable must be set.");

            if (string.IsNullOrEmpty(AccountId))
                throw new InvalidOperationException("DRIP_ACCOUNT_ID environment variable must be set.");

            Client = new DripClient(ApiKey, AccountId);
        }

        public void Dispose()
        {
        }
    }
}
