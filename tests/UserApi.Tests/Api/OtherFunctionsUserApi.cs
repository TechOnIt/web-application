using TechTalk.SpecFlow;

namespace UserApi.Tests.Api;

[Binding]
internal class OtherFunctionsUserApi
{
    [Given(@"Id of existing user")]
    public void GivenIdOfExistingUser()
    {
        throw new PendingStepException();
    }

    [When(@"i request to update the user baned status by Id")]
    public void WhenIRequestToUpdateTheUserBanedStatusById()
    {
        throw new PendingStepException();
    }

    [Then(@"the response status code is '([^']*)'")]
    public void ThenTheResponseStatusCodeIs(string p0)
    {
        throw new PendingStepException();
    }

    [Given(@"Id of non-existing user")]
    public void GivenIdOfNon_ExistingUser()
    {
        throw new PendingStepException();
    }

    //[When(@"i request to update the user baned status by Id:")]
    //public void WhenIRequestToUpdateTheUserBanedStatusById()
    //{
    //    throw new PendingStepException();
    //}

}
