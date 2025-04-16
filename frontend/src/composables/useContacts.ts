import { ref } from 'vue'
import { getContacts, createContact, deleteContact } from '@/services/contactsService'
import { parseApiError } from '@/utils/errorHandler'
import type { Contact } from '@/types/contact'

const contacts = ref<Contact[]>([]) // TODO: tipagem dos contatos

export function useContacts() {
  const loading = ref<boolean>(false)
  const error = ref<string | null>(null)

  const loadContacts = async () => {
    loading.value = true
    error.value = null
    try {
      contacts.value = await getContacts()
    } catch (err) {
      error.value = parseApiError(err)
    } finally {
      loading.value = false
    }
  }

  const addContact = async (payload: { name: string; phoneNumber: string; email: string }) => {
    error.value = null
    try {
      const newContact = await createContact(payload)
      contacts.value.unshift(newContact)
    } catch (err) {
      error.value = parseApiError(err)
      throw err
    }
  }

  const removeContact = async (id: string) => {
    error.value = null
    const oldContacts = contacts.value
    contacts.value = [...contacts.value.filter((contact) => contact.id !== id)] // optimistic update
    try {
      await deleteContact(id)
    } catch (err) {
      error.value = parseApiError(err)
      contacts.value = oldContacts
      throw err
    }
  }

  return {
    contacts,
    addContact,
    loading,
    error,
    loadContacts,
    removeContact,
  }
}
