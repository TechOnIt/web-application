import { IHttpService } from '../interfaces/IHttpService';

class HttpService implements IHttpService {

    async get(url: string): Promise<any> {
        const response = await fetch(url);
        const data = await response.json();
        return data;
    }

    async post(url: string, data: any): Promise<any> {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });
        const responseData = await response.json();
        return responseData;
    }

    async put(url: string, data: any): Promise<any> {
        const response = await fetch(url, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });
        const responseData = await response.json();
        return responseData;
    }

    async delete(url: string): Promise<any> {
        const response = await fetch(url, {
            method: 'DELETE',
        });
        const responseData = await response.json();
        return responseData;
    }
}

export default HttpService;
