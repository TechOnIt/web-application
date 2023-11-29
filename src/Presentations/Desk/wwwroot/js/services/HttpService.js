var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
class HttpService {
    get(url) {
        return __awaiter(this, void 0, void 0, function* () {
            const response = yield fetch(url);
            const data = yield response.json();
            return data;
        });
    }
    post(url, data) {
        return __awaiter(this, void 0, void 0, function* () {
            const response = yield fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(data),
            });
            const responseData = yield response.json();
            return responseData;
        });
    }
    put(url, data) {
        return __awaiter(this, void 0, void 0, function* () {
            const response = yield fetch(url, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(data),
            });
            const responseData = yield response.json();
            return responseData;
        });
    }
    delete(url) {
        return __awaiter(this, void 0, void 0, function* () {
            const response = yield fetch(url, {
                method: 'DELETE',
            });
            const responseData = yield response.json();
            return responseData;
        });
    }
}
export default HttpService;
//# sourceMappingURL=HttpService.js.map