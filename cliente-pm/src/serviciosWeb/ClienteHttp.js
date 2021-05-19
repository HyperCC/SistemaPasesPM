import axios from 'axios';

axios.defaults.baseURL = 'https://localhost:5001/api';

// agregar los token una validadas las credenciales 
axios.interceptors.request.use((config) => {
    const token_seguridad = window.localStorage.getItem('token_seguridad');
    if (token_seguridad) {
        config.headers.Authorization = 'Bearer ' + token_seguridad;
        return config;
    }
    console.log('No hay token registrado para esta sesion.');
}, error => {
    return Promise.reject(error);
});

// respuestas para verbos http con axios
const requestGenerico = {
    get: (url) => axios.get(url),
    post: (url, body) => axios.post(url, body),
    put: (url, body) => axios.put(url, body),
    delete: (url) => axios.delete(url)
};

export default requestGenerico;