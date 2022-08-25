using FluentResults;
using iot.Domain.Entities.Identity;
using iot.Domain.ValueObjects;
using RestSharp;
using System.Net;

namespace UserApi.Tests.Api.Controllers;

internal class UserApiSpec
{
    #region constructor
    private readonly RestClient _client;
    private const string UserApiAddress= "/Admin/v1/User/";

    public UserApiSpec()
    {
        _client = new RestClient("https://localhost:5001");

        ServicePointManager.ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => true;
    }
    #endregion

    public async Task<Result<Guid>> Create()
    {
        var request = new RestRequest($"{UserApiAddress}Create").AddObject(this);
        var response = await _client.GetAsync<Result<Guid>>(request);

        return response;
    }

    public async Task<Result<Guid>> Update()
    {
        var request = new RestRequest($"{UserApiAddress}Update").AddObject(this);
        var response = await _client.GetAsync<Result<Guid>>(request);

        return response;
    }


    public async Task<Result> SetPassword()
    {
        var request = new RestRequest($"{UserApiAddress}Create").AddObject(this);
        var response = await _client.GetAsync<Result>(request);

        return response;
    }

    public async Task<Result> Ban()
    {
        var request = new RestRequest($"{UserApiAddress}Ban").AddObject(this);
        var response = await _client.GetAsync<Result>(request);

        return response;
    }

    public async Task<Result> UnBan()
    {
        var request = new RestRequest($"{UserApiAddress}UnBan").AddObject(this);
        var response = await _client.GetAsync<Result>(request);

        return response;
    }

    public async Task<Result> RemoveAccount()
    {
        var request = new RestRequest($"{UserApiAddress}RemoveAccount").AddObject(this);
        var response = await _client.GetAsync<Result>(request);

        return response;
    }

    public async Task<Result> ForceDelete()
    {
        var request = new RestRequest($"{UserApiAddress}ForceDelete").AddObject(this);
        var response = await _client.GetAsync<Result>(request);

        return response;
    }
}

public class UserModel
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public PasswordHash Password { get; set; }
    public string Email { get; set; }
    public bool ConfirmedEmail { get; set; }
    public string PhoneNumber { get; set; }
    public bool ConfirmedPhoneNumber { get; set; }
    public FullName FullName { get; set; }
    public DateTime RegisteredDateTime { get; set; }
    public Concurrency ConcurrencyStamp { get; set; }
    public bool IsBaned { get; set; }
}