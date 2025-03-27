using System.Text.Json;
using YarpGateWay.Constants;

namespace YarpGateWay
{
    public class AuthValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthValidationMiddleware(RequestDelegate next, IHttpClientFactory httpClientFactory)
        {
            _next = next;
            _httpClientFactory = httpClientFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var httpClient = _httpClientFactory.CreateClient();
            bool isTokenExist=true;
            // Extract the Authorization header
            if (!context.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                isTokenExist = false;
            }

            // Extract the Bearer token
            var bearerToken = authorizationHeader.ToString();
            var token = string.Empty;
            if (string.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                isTokenExist = false;
            }
            else
            {
                token = bearerToken.Substring("Bearer ".Length).Trim();
            }


            if (string.IsNullOrEmpty(token)||token.Length<20)
                isTokenExist = false;


            var fullRoute = $"{context.Request.PathBase}{context.Request.Path}{context.Request.QueryString}";

            if (context.Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {

            }

            if (!isTokenExist&&NoAuthRoutes.Routes.Any(x=>fullRoute.Contains(x)))
            {
                await _next(context);
            }
            else
            {

                //var authServiceUrl = "http://localhost:7070/api/auth/validate-token";
                var authServiceUrl = AuthServiceConstants.AuthHostPath+AuthServiceConstants.ValidateLoginPath;

                var authRequest = new HttpRequestMessage(HttpMethod.Get, authServiceUrl);

                foreach (var header in context.Request.Headers)
                {
                    authRequest.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                }

                if (context.Request.ContentLength > 0)
                {
                    authRequest.Content = new StreamContent(context.Request.Body);
                    authRequest.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(context.Request.ContentType);
                }

                var authResponse = await httpClient.SendAsync(authRequest, context.RequestAborted);

                if (!authResponse.IsSuccessStatusCode)
                {

                    context.Response.StatusCode = (int)authResponse.StatusCode;
                    await authResponse.Content.CopyToAsync(context.Response.Body);
                    return;
                }

                var responseContent = await authResponse.Content.ReadAsStringAsync();
                /* No need in userid?
                int UserId = JsonSerializer.Deserialize<int>(responseContent);
                context.Items["UserId"] = UserId;*/
                // If validation succeeds, proceed to the next middleware (YARP)
                await _next(context);
            }

            
        }
    }
}
