<script setup lang="ts">
import { ref } from 'vue'
import ContactListItem from './ContactListItem.vue'
import { useContacts } from '@/composables/useContacts'
import Skeleton from 'primevue/skeleton'
import { onMounted } from 'vue'

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
</script>

<template>
  <div class="flex flex-col gap-4">
    <div v-if="loading" class="flex flex-col gap-4">
      <Skeleton class="w-full pt-[52px]" v-for="i in 10" :key="i" />
    </div>
    <div v-else-if="error" class="flex justify-center">
      <p>Erro ao carregar contatos: {{ error }}</p>
    </div>
    <div v-else class="flex flex-col gap-4">
      <ContactListItem
        :isOpen="expandedContact === contact.id"
        v-for="contact in contacts"
        :key="contact.id"
        :contact="contact"
        @toggle="openContact(contact.id)"
      />
    </div>
  </div>
</template>
