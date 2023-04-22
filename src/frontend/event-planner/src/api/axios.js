import axios from 'axios';
import * as endpoints from "../shared/endpoints";

const BASE_URL = endpoints.baseUrl;

export default axios.create({
    baseURL: BASE_URL
});

export const axiosPrivate = axios.create({
    baseURL: BASE_URL,
    headers: { 'Content-Type': 'application/json' },
    withCredentials: true
});