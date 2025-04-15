<script setup lang="ts">
import { ref } from 'vue'
import BaseInput from '../../BaseInput.vue'
import Button from 'primevue/button'
import { RouterLink } from 'vue-router'
import { useContacts } from '@/composables/useContacts'
import { useToast } from 'primevue/usetoast'

const { addContact } = useContacts()
const toast = useToast()

const name = ref('')
const phone = ref('')
const email = ref('')
const submitting = ref(false)

const handleSubmit = async () => {
  submitting.value = true
  try {
    await addContact({ name: name.value, phoneNumber: phone.value, email: email.value })
    toast.add({ severity: 'success', summary: 'Contato criado!', life: 3000 })
    name.value = email.value = phone.value = ''
  } catch {
    toast.add({
      severity: 'error',
      summary: 'Erro',
      detail: 'Não foi possível criar o contato',
      life: 3000,
    })
  } finally {
    submitting.value = false
  }
}
</script>

<template>
  <form @submit.prevent="handleSubmit" class="flex flex-col gap-4">
    <BaseInput id="name" label="Nome" icon="pi pi-user" v-model="name" />
    <BaseInput id="phone" label="Telefone" icon="pi pi-phone" v-model="phone" />
    <BaseInput id="email" label="E-mail" icon="pi pi-envelope" v-model="email" />
    <div class="flex gap-4">
      <RouterLink class="flex-1" to="/">
        <Button fluid outlined label="Cancelar" icon="pi pi-times" />
      </RouterLink>
      <Button type="submit" outlined class="flex-1" label="Salvar" icon="pi pi-save" />
    </div>
  </form>
</template>
