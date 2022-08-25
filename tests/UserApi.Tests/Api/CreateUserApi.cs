using FluentResults;
using iot.Domain.ValueObjects;
using Shouldly;
using System.Net;
using TechTalk.SpecFlow;
using UserApi.Tests.Api.Controllers;

namespace UserApi.Tests.Api;

[Binding]
internal class CreateUserApi
{

    #region constructor
    private readonly UserApiSpec _userApiSpec;
    private Result<Guid> _responseWithId;
    private UserModel _userModel;

    public CreateUserApi()
    {
        _userApiSpec = new UserApiSpec();
        _userModel = new UserModel();
    }
    #endregion

    [Given(@"new user information by details Name: '([^']*)' Surname: '([^']*)' Email: '([^']*)' PhoneNumber: '([^']*)' and Password: '([^']*)'")]
    public void GivenNewUserInformationByDetailsNameSurnameEmailPhoneNumberAndPassword(string Name, string Surname, string Email, string PhoneNumber, string Password)
    {
        _userModel.FullName = new FullName(name: Name, surname: Surname);
        _userModel.Email = Email;
        _userModel.PhoneNumber = PhoneNumber;
        _userModel.Password = new PasswordHash(Password);
    }

    [When(@"I request to create a new user by details")]
    public async void WhenIRequestToCreateANewUserByDetails()
    {
        _userModel.Id = new Guid();
        _responseWithId = await _userApiSpec.Create();
    }

    [Then(@"the response should be user Id return as '([^']*)'")]
    public void ThenTheResponseShouldBeUserIdReturnAs(string response)
    {
        _responseWithId.ShouldNotBeNull();
        _responseWithId.Value.ShouldBeOfType<Guid>();
    }

    [Given(@"new user information and details Name: '([^']*)' Surname '([^']*)' Email '([^']*)' Password '([^']*)' PhoneNumber '([^']*)'")]
    public void GivenNewUserInformationAndDetailsNameSurnameEmailPasswordPhoneNumber(string Name, string Surname, string Email, string PhoneNumber, string Password)
    {
        _userModel.FullName = new FullName(name: Name, surname: Surname);
        _userModel.Email = Email;
        _userModel.PhoneNumber = PhoneNumber;
        _userModel.Password = new PasswordHash(Password);
    }

    [When(@"I request to create a new user and details")]
    public async void WhenIRequestToCreateANewUserAndDetails()
    {
        _userModel.Id = new Guid();
        _responseWithId = await _userApiSpec.Create();
    }

    [Then(@"The response status code is '([^']*)'")]
    public void ThenTheResponseStatusCodeIs(HttpStatusCode response)
    {
        _responseWithId.ToResult().ShouldBeSameAs(response);
    }

}
