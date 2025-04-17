<script setup lang="ts">
import ContactForm from '@/components/contacts/form/ContactForm.vue'
import { ref, onMounted } from 'vue'
import { useContacts } from '@/composables/useContacts'
import { useToast } from 'primevue/usetoast'
import { useRoute, useRouter } from 'vue-router'
import Toast from 'primevue/toast'

const { editContact, loadContactById, addContact } = useContacts()

const route = useRoute()
const router = useRouter()
const toast = useToast()

const initialValues = ref<{ name: string; phoneNumber: string; email: string }>({
  name: '',
  phoneNumber: '',
  email: '',
})

const id = route.params.id as string | undefined
const isEdit = !!id

onMounted(async () => {
  if (!isEdit) return

  try {
    const data = await loadContactById(id)
    initialValues.value = {
      name: data.name,
      phoneNumber: data.phoneNumber,
      email: data.email,
    }
  } catch {
    toast.add({
      severity: 'error',
      summary: 'Erro',
      detail: 'Não foi possível carregar o contato.',
      life: 3000,
    })
    router.push('/')
  }
})

const handleSubmit = async (formData: { name: string; phoneNumber: string; email: string }) => {
  try {
    if (isEdit) {
      await editContact(id!, formData)
      toast.add({ severity: 'success', summary: 'Contato atualizado!', life: 3000 })
    } else {
      await addContact(formData)
      toast.add({ severity: 'success', summary: 'Contato criado!', life: 3000 })
    }

    router.push('/')
  } catch {
    toast.add({
      severity: 'error',
      summary: 'Erro',
      detail: 'Não foi possível salvar o contato.',
      life: 3000,
    })
  }
}
</script>

<template>
  <div class="flex flex-col gap-4 items-center justify-center">
    <Toast />
    <h1 class="text-2xl">{{ isEdit ? 'Editar contato' : 'Novo contato' }}</h1>
    <ContactForm :initialValues="initialValues" @submit="handleSubmit" />
  </div>
</template>
