import { ref, onMounted } from 'vue'
import { getContacts, createContact } from '@/services/contactsService'
import { parseApiError } from '@/utils/errorHandler'

export function useContacts() {
  const contacts = ref<
    { id: number; name: string; email: string; phoneNumber: string; createdAt: string }[]
  >([]) // TODO: tipagem dos contatos
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

  return {
    contacts,
    addContact,
    loading,
    error,
    loadContacts,
  }
}
