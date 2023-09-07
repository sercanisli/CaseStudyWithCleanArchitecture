using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Application.Services.Exceptions.Extensions
{
    public static class ProblemDetailExtensions
    {
        public static string AsJson<TProblemDetail>(this TProblemDetail problemDetail)
            where TProblemDetail : ProblemDetails => JsonSerializer.Serialize(problemDetail);
    }
}
