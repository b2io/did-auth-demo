using DidAuthDemo.Core.Models;
using Refit;

namespace DidAuthDemo.Maui.Clients;

public record CaptureResponse(bool successful);

public interface IAuthClient
{
    [Post("/api/capture-auth-response")]
    Task<ApiResponse<CaptureResponse>> SendAsync([Body] AuthResponse response);
}
