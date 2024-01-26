using Newtonsoft.Json;
using RestSharp;

namespace CrudConsumeApi.Repository
{
    public interface ICommonRepository
    {
        T Get<T>(string Action, object? Params = null);
        List<T> GetAll<T>(string Action, object? Params = null);
        T Post<T>(string Action, object? Params = null);
    }

    public class CommonRepository : ICommonRepository
    {
        private string Query(string action, object? parameters = null, Method method = Method.Get)
        {
            var client = new RestClient("https://localhost:44345/");
            var request = new RestRequest(action, method);

            if (parameters != null)
            {
                //request.AddJsonBody(parameters);
                if (method == Method.Post)
                {
                    request.AddJsonBody(parameters);
                }
                else // Default to adding as query parameters for other HTTP methods
                {
                    foreach (var property in parameters.GetType().GetProperties())
                    {
                        request.AddParameter(property.Name, property.GetValue(parameters)?.ToString());
                    }
                }
            }

            var response = client.Execute(request);
            return response.Content!;
        }

        public T Get<T>(string Action, object? Params = null)
        {
            return JsonConvert.DeserializeObject<T>(Query(Action, Params))!;
        }

        public List<T> GetAll<T>(string Action, object? Params = null)
        {
            return JsonConvert.DeserializeObject<List<T>>(Query(Action, Params))!;
        }

        public T Post<T>(string Action, object? Params = null)
        {
            return JsonConvert.DeserializeObject<T>(Query(Action, Params, Method.Post))!;
        }

    }
}

