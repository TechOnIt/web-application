


(function () {
    var signinFrm = {
        username: "RezaAmd",
        password: "123456"
    };
    rest.post("Authentication/Signin", signinFrm, function (result, isSuccess) {
        if (isSuccess) {
            console.log(result.data.token);
        }
    });
})();