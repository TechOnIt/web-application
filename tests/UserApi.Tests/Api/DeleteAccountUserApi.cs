using TechTalk.SpecFlow;

namespace UserApi.Tests.Api;

[Binding]
internal class DeleteAccountUserApi
{
    [Given(@"Id of existing user")]
    public void GivenIdOfExistingUser()
    {
        throw new PendingStepException();
    }

    [When(@"i request to remove the user account by Id")]
    public void WhenIRequestToRemoveTheUserAccountById()
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

    //[When(@"i request to remove the user account by Id:")]
    //public void WhenIRequestToRemoveTheUserAccountById()
    //{
    //    throw new PendingStepException();
    //}

    [When(@"i request to ForceDelete the user account by Id")]
    public void WhenIRequestToForceDeleteTheUserAccountById()
    {
        throw new PendingStepException();
    }

    [When(@"i request to ForceDelete the user user account by Id:")]
    public void WhenIRequestToForceDeleteTheUserUserAccountById()
    {
        throw new PendingStepException();
    }

}
