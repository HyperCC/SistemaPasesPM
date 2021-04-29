import axios from 'axios';

axios.defaults.baseURL = 'https://localhost:5001/api';

// respuestas para verbos http con axios
const requestGenerico = {
    get: (url) => axios.get(url),
    post: (url, body) => axios.post(url, body),
    put: (url, body) => axios.put(url, body),
    delete: (url) => axios.delete(url)
};

export default requestGenerico;