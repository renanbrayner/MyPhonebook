<script setup lang="ts">
import { ref, computed } from 'vue'
import BaseInput from '../../BaseInput.vue'
import Button from 'primevue/button'
import { RouterLink } from 'vue-router'
import { useContacts } from '@/composables/useContacts'
import { useToast } from 'primevue/usetoast'
import InputMask from 'primevue/inputmask'
import InputText from 'primevue/inputtext'

const { addContact } = useContacts()
const toast = useToast()

const name = ref('')
const submitting = ref(false)
const phone = ref('')
const email = ref('')

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
    <BaseInput label="Nome" icon="pi pi-user">
      <InputText v-model="name" />
    </BaseInput>
    <BaseInput label="Telefone" icon="pi pi-phone">
      <InputMask :autoClear="false" mask="(99) 99999-9999" v-model="phone" />
    </BaseInput>
    <BaseInput label="E-mail" icon="pi pi-envelope">
      <InputText v-model="email" />
    </BaseInput>
    <div class="flex gap-4">
      <RouterLink class="flex-1" to="/">
        <Button fluid outlined label="Cancelar" icon="pi pi-times" />
      </RouterLink>
      <Button type="submit" outlined class="flex-1" label="Salvar" icon="pi pi-save" />
    </div>
  </form>
</template>
