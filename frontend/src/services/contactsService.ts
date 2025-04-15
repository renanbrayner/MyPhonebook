import { api } from './api'

export async function getContacts() {
  const { data } = await api.get('/contacts')
  return data
}

export async function createContact(payload: { name: string; email: string; phone: string }) {
  const { data } = await api.post('/contacts', payload)
  return data
}
