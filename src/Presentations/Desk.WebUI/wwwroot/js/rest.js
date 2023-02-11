let rest = {};
rest = {
    methods: {
        get: 'GET',
        post: 'POST',
        put: 'PUT',
        delete: 'DELETE',
        patch: 'PATCH'
    },
    request: function (url, callback, method = rest.methods.get, body) {
        var headers = new Headers();
        if (body) {
            headers.append('Accept', 'application/json',);
            headers.append('Content-Type', 'application/json');
        }
        var requestOptions = {
            method: method,
            headers: headers,
            body: body,
            redirect: 'follow',
            cache: 'no-cache',
            mode: 'cors'
        };
        fetch('https://localhost:7073/v1/' + url, requestOptions)
            .then(response => response.json())
            .then(result => callback(result, true))
            .catch(error => callback(error, false));
    },
    get: function (url, callback) {
        rest.request(url, callback, rest.methods.get, null);
    },
    post: function (url, body, callback) {
        rest.request(url, callback, rest.methods.post, JSON.stringify(body));
    }
}