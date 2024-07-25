namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting a client.
/// </summary>
public class GetClientByNoteOperation : IResultOperation<GetClientByNoteRequest, ApiClientModel>
{
    private readonly ILogger _Logger;
    private readonly IApiClientFactory _ApiClientFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetClientByNoteOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="apiClientFactory">The <see cref="IApiClientFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="apiClientFactory"/> cannot be null.
    /// </exception>
    public GetClientByNoteOperation(ILogger logger, IApiClientFactory apiClientFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ApiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ApiClientModel, OperationError) Execute(GetClientByNoteRequest input)
    {
        if (string.IsNullOrEmpty(input.Note)) return (null, new("{0} cannot be null or empty", nameof(input.Note)));

        _Logger.Information("GetClientByNote, Note = {0}", input.Note);

        var apiClient = _ApiClientFactory.GetByNote(input.Note);

        return (apiClient != null ? new(apiClient) : null, null);
    }
}
