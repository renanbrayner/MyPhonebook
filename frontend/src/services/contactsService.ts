import type { ContactListResponse, Contact } from '@/types/contact'
import { api } from './api'

export async function getContacts(): Promise<Contact[]> {
  const { data } = await api.get<ContactListResponse>('/contacts')
  return data.contacts
}

export async function getContactById(id: string): Promise<Contact> {
  const { data } = await api.get<Contact>(`/contacts/${id}`)
  return data
}

export async function createContact(payload: { name: string; email: string; phoneNumber: string }) {
  const { data } = await api.post('/contacts', payload)
  return data
}

export async function deleteContact(id: string) {
  await api.delete(`/contacts/${id}`)
}

export async function updateContact(
  id: string,
  payload: { name: string; email: string; phoneNumber: string },
) {
  const { data } = await api.put(`/contacts/${id}`, payload)
  return data
}
