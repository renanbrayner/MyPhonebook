<script setup lang="ts">
import ContactForm from '@/components/contacts/form/ContactForm.vue'
import Skeleton from 'primevue/skeleton'
import { ref, onMounted } from 'vue'
import { useContacts } from '@/composables/useContacts'
import { useToast } from 'primevue/usetoast'
import { useRoute, useRouter } from 'vue-router'
import axios from 'axios'

const { editContact, loadContactById, addContact } = useContacts()

const route = useRoute()
const router = useRouter()
const toast = useToast()

const initialValues = ref<{ name: string; phoneNumber: string; email: string }>({
  name: '',
  phoneNumber: '',
  email: '',
})

const submitting = ref(false)

const initialLoading = ref(!!route.params.id)

onMounted(async () => {
  if (!route.params.id) return

  try {
    initialLoading.value = true
    const data = await loadContactById(route.params.id as string)
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
  } finally {
    initialLoading.value = false
  }
})

const handleSubmit = async (formData: { name: string; phoneNumber: string; email: string }) => {
  try {
    submitting.value = true
    if (!!route.params.id) {
      await editContact(route.params.id! as string, formData)
      toast.add({ severity: 'success', summary: 'Contato atualizado!', life: 3000 })
    } else {
      const res = await addContact(formData)
      toast.add({ severity: 'success', summary: 'Contato criado!', life: 3000 })
      router.push(`/contato/${res.id}`)
    }
  } catch (err) {
    // Se não for um erro de requisição Axios ou não tiver response → fallback genérico
    if (!axios.isAxiosError(err) || !err.response) {
      toast.add({
        severity: 'error',
        summary: 'Erro',
        detail: 'Não foi possível salvar o contato. Tente novamente mais tarde.',
        life: 5000,
      })
      return
    }

    // Se não for status 400 ou não trouxer objeto errors → fallback genérico
    const { status, data } = err.response
    if (status !== 400 || typeof data?.errors !== 'object') {
      toast.add({
        severity: 'error',
        summary: 'Erro',
        detail: 'Não foi possível salvar o contato. Tente novamente mais tarde.',
        life: 5000,
      })
      return
    }

    // Agora temos um 400 com data.errors como objeto
    const apiErrors = data.errors as Record<string, string[]>
    const messages = Object.values(apiErrors).filter(Array.isArray).flat()

    if (messages.length === 0) {
      toast.add({
        severity: 'error',
        summary: 'Erro de validação',
        detail: 'O servidor retornou erros de validação, mas sem mensagens específicas.',
        life: 5000,
      })
      return
    }

    // Exibe todas as mensagens vindas do backend
    toast.add({
      severity: 'error',
      summary: 'Erro de validação',
      detail: messages.join('\n'),
      life: 5000,
    })
  } finally {
    submitting.value = false
  }
}
</script>

<template>
  <div class="flex flex-col gap-4 items-center justify-center md:pt-20">
    <Toast />
    <h1 class="text-2xl">{{ !!route.params.id ? 'Editar contato' : 'Novo contato' }}</h1>
    <ContactForm
      :submitting="submitting"
      v-if="!initialLoading"
      :initialValues="initialValues"
      @submit="handleSubmit"
    />
    <div class="flex flex-col gap-8 w-72 pt-10" v-else>
      <Skeleton class="p-3" />
      <Skeleton class="p-6" />
      <Skeleton class="p-6" />
      <Skeleton class="p-6" />

      <div class="flex gap-4">
        <Skeleton class="p-6 flex-1" />
        <Skeleton class="p-6 flex-1" />
      </div>
    </div>
  </div>
</template>
