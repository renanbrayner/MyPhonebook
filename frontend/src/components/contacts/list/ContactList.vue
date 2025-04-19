<script setup lang="ts">
import { ref, computed } from 'vue'
import ContactListItem from './ContactListItem.vue'
import { useContacts } from '@/composables/useContacts'
import Skeleton from 'primevue/skeleton'
import { onMounted } from 'vue'
import type { Contact } from '@/types/contact'

const props = defineProps<{
  filter: string
}>()

const { contacts, loading, error, loadContacts } = useContacts()

onMounted(() => {
  loadContacts()
})

const expandedContact = ref<string | null>(null)

const openContact = (id: string) => {
  if (expandedContact.value === id) {
    // não muda o comportamento, mas torna o estado igual a interface
    expandedContact.value = null
    return
  }
  expandedContact.value = id
}

const filteredContacts = computed(() => {
  const term = props.filter.trim().toLowerCase()
  if (!term) return contacts.value

  return contacts.value.filter((c: Contact) => {
    return (
      c.name.toLowerCase().includes(term) ||
      c.email.toLowerCase().includes(term) ||
      c.phoneNumber.toLowerCase().includes(term)
    )
  })
})
</script>

<template>
  <div class="flex flex-col gap-4">
    <div v-if="loading" class="flex flex-col gap-4">
      <Skeleton class="w-full pt-[52px]" v-for="i in 10" :key="i" />
    </div>
    <div v-else-if="error" class="flex justify-center">
      <p>Erro ao carregar contatos: {{ error }}</p>
    </div>
    <transition-group v-else tag="div" name="stagger" class="flex flex-col gap-3">
      <ContactListItem
        v-for="(contact, i) in filteredContacts"
        class="stagger-item"
        :style="{ '--delay': `${i * 15}ms` }"
        :isOpen="expandedContact === contact.id"
        :key="contact.id"
        :id="contact.id"
        :contact="contact"
        @toggle="openContact(contact.id)"
      />
    </transition-group>
    <p v-if="!filteredContacts.length && filter !== ''" class="text-center text-gray-500">
      Nenhum contato encontrado para “{{ props.filter }}”
    </p>
  </div>
</template>
