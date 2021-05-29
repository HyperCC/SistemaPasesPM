import axios from 'axios';

axios.defaults.baseURL = 'https://localhost:5001/api';

// agregar los token una validadas las credenciales 
/*
axios.interceptors.request.use((config) => {
    const token_seguridad = window.localStorage.getItem('token_seguridad');
    if (token_seguridad) {
        config.headers.Authorization = 'Bearer ' + token_seguridad;
        console.log('todo el registro de token fue bien ', config);
        return config;
    }
    console.log('no se encontro el token');
}, error =>
    Promise.reject(error.toString())
);
*/

// respuestas para verbos http con axios
const requestGenerico = {
    get: (url) => axios.get(url),
    post: (url, body) => axios.post(url, body),
    put: (url, body) => axios.put(url, body),
    delete: (url) => axios.delete(url)
};

export default requestGenerico;