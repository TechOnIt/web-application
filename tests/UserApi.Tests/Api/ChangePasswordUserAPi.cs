using TechTalk.SpecFlow;

namespace UserApi.Tests.Api;

[Binding]
internal class ChangePasswordUserAPi
{
    [Given(@"new password and existing user data as SetUserPasswordCommand")]
    public void GivenNewPasswordAndExistingUserDataAsSetUserPasswordCommand()
    {
        throw new PendingStepException();
    }

    [When(@"i request to update the user password by Id and details Password: '([^']*)' and RepeatPassword: '([^']*)'")]
    public void WhenIRequestToUpdateTheUserPasswordByIdAndDetailsPasswordAndRepeatPassword(string p0, string p1)
    {
        throw new PendingStepException();
    }

    [Then(@"the response status code is '([^']*)'")]
    public void ThenTheResponseStatusCodeIs(string p0)
    {
        throw new PendingStepException();
    }

    [When(@"i request to update the user password without Id and details Password: '([^']*)' and RepeatPassword: '([^']*)' and Id:")]
    public void WhenIRequestToUpdateTheUserPasswordWithoutIdAndDetailsPasswordAndRepeatPasswordAndId(string p0, string p1)
    {
        throw new PendingStepException();
    }

    [Given(@"that a user exists or dose not exists in the system")]
    public void GivenThatAUserExistsOrDoseNotExistsInTheSystem()
    {
        throw new PendingStepException();
    }

    [When(@"i request to set new password for user by Id and details Password: '([^']*)' RepeatPassword '([^']*)'")]
    public void WhenIRequestToSetNewPasswordForUserByIdAndDetailsPasswordRepeatPassword(string p0, string repeatpassword)
    {
        throw new PendingStepException();
    }

    //[Then(@"The response status code is '([^']*)'")]
    //public void ThenTheResponseStatusCodeIs(string p0)
    //{
    //    throw new PendingStepException();
    //}

}
