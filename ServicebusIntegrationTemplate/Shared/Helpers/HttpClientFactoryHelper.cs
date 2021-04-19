using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ServiceBusIntegrationTemplate.Shared.Helpers
{
    public class HttpClientFactoryHelper
    {
        private static ConcurrentDictionary<string, HttpClient> _repositories;
        private static object lockObject = new object();

        public static HttpClient Get(string apiKey)
        {
            try
            {
                lock (lockObject)
                {
                    if (_repositories == null)
                    {
                        _repositories = new ConcurrentDictionary<string, HttpClient>();
                    }
                }

                return _repositories.GetOrAdd(apiKey, new HttpClient());
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}