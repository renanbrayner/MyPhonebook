import axios from 'axios'

export const api = axios.create({
  baseURL: 'http://localhost:5036/api', // Em produção um arquivo .env seria necessário
})
