using SolarCoffee.Services.ModelResponse;

namespace SolarCoffee.Services.Utils
{
    public static class UtilsResponse 
    {
        public static ServiceResponse<T> GenericResponse<T>(T info, string message, bool isSucced = false)
        {
            return new ServiceResponse<T>
            {
                Data = info,
                Time = DateTime.UtcNow,
                Message = message,
                IsSuccess = isSucced
            };
        }
    }
}