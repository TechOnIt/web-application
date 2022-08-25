using TechTalk.SpecFlow;

namespace UserApi.Tests.Api;

[Binding]
internal class UpdateUserApi
{
    [Given(@"that a user exists in the system")]
    public void GivenThatAUserExistsInTheSystem()
    {
        throw new PendingStepException();
    }

    [When(@"i request to update the user by id and details Name: '([^']*)' Surname: '([^']*)' Email: '([^']*)' ConcurrencyStamp: '([^']*)'")]
    public void WhenIRequestToUpdateTheUserByIdAndDetailsNameSurnameEmailConcurrencyStamp(string testName, string testSurname, string p2, string p3)
    {
        throw new PendingStepException();
    }

    [Then(@"the user should be updated")]
    public void ThenTheUserShouldBeUpdated()
    {
        throw new PendingStepException();
    }

    [Then(@"the response status code is '([^']*)'")]
    public void ThenTheResponseStatusCodeIs(string p0)
    {
        throw new PendingStepException();
    }

    [Given(@"that a user dose not exists in the system")]
    public void GivenThatAUserDoseNotExistsInTheSystem()
    {
        throw new PendingStepException();
    }

    [When(@"i request to update the user without id and details Name: '([^']*)' Surname: '([^']*)' Email: '([^']*)' ConcurrencyStamp: '([^']*)' and Id:")]
    public void WhenIRequestToUpdateTheUserWithoutIdAndDetailsNameSurnameEmailConcurrencyStampAndId(string testName, string testSurname, string p2, string p3)
    {
        throw new PendingStepException();
    }

    [Given(@"that a user exists or dose not exists in the system")]
    public void GivenThatAUserExistsOrDoseNotExistsInTheSystem()
    {
        throw new PendingStepException();
    }

    //[When(@"i request to update the user by id and details Name '([^']*)' Surname '([^']*)' Email '([^']*)' ConcurrencyStamp '([^']*)'")]
    //public void WhenIRequestToUpdateTheUserByIdAndDetailsNameSurnameEmailConcurrencyStamp(string testName, string testSurname, string p2, string p3)
    //{
    //    throw new PendingStepException();
    //}

    //[Then(@"The response status code is '([^']*)'")]
    //public void ThenTheResponseStatusCodeIs(string p0)
    //{
    //    throw new PendingStepException();
    //}

}
