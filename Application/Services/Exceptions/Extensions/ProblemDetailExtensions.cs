using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Application.Services.Exceptions.Extensions
{
    public static class ProblemDetailExtensions
    {
        //Gelen Problem Detail'i Serileştirerek JSON hale getiriyorum.
        public static string AsJson<TProblemDetail>(this TProblemDetail problemDetail)
            where TProblemDetail : ProblemDetails => JsonSerializer.Serialize(problemDetail);
    }
}
