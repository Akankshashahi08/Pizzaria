using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Pizzaria.Function.Api
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static async Task<T> GetObjectAsync<T>(this HttpRequest request)
            where T : class
        {
            string requestBody = await new StreamReader(request.Body).ReadToEndAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(requestBody);
        }
    }
}
