<script setup lang="ts">
import { Form } from '@primevue/forms'
import type { FormSubmitEvent } from '@primevue/forms'
import { ref } from 'vue'
import BaseInput from '../../BaseInput.vue'
import Button from 'primevue/button'
import { RouterLink } from 'vue-router'
import { useContacts } from '@/composables/useContacts'
import { useToast } from 'primevue/usetoast'
import InputMask from 'primevue/inputmask'
import InputText from 'primevue/inputtext'
import { zodResolver } from '@primevue/forms/resolvers/zod'
import { z } from 'zod'
import Toast from 'primevue/toast'

const { addContact } = useContacts()
const toast = useToast()

const contactSchema = z.object({
  name: z.string().nonempty({ message: 'Nenhum nome informado' }),
  phone: z
    .string()
    .transform((val) => val.replace(/\D/g, ''))
    .refine((val) => val.length === 10 || val.length === 11, {
      message: 'Número de telefone incompleto',
    }),
  email: z.string().email({ message: 'E-mail inválido' }),
})

const resolver = zodResolver(contactSchema)

const submitting = ref(false)

const handleSubmit = async (event: FormSubmitEvent) => {
  console.log(event)
  if (!event.valid) {
    toast.add({
      severity: 'error',
      summary: 'Erro',
      detail: 'Corrija os campos com erros antes de enviar',
      life: 3000,
    })
    console.log('not valid')
    return
  }

  const { name, phone, email } = Object.fromEntries(
    Object.entries(event.states).map(([key, val]) => [key, val.value]),
  )

  submitting.value = true
  try {
    await addContact({ name, phoneNumber: phone, email })
    toast.add({ severity: 'success', summary: 'Contato criado!', life: 3000 })
  } catch (error) {
    console.log(error)
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
  <Toast />
  <Form
    v-slot="$form"
    :resolver="resolver"
    @submit="handleSubmit"
    class="flex flex-col gap-1"
    :initialValues="{ name: '', phone: '', email: '' }"
  >
    <BaseInput label="Nome" icon="pi pi-user">
      <InputText name="name" />
      <template #message>
        <small
          :data-hide="!$form.name?.invalid"
          class="min-h-[1.75rem] data-[hide=true]:opacity-0 data-[hide=true]:-translate-y-1 block transition-all opacity-100 duration-150 translate-y-0 text-sm text-red-400 py-1"
          severity="error"
          size="small"
          variant="simple"
        >
          {{ $form.name?.error?.message }}
        </small>
      </template>
    </BaseInput>
    <BaseInput label="Telefone" icon="pi pi-phone">
      <InputMask name="phone" :autoClear="false" mask="(99) 99999-9999" />
      <template #message>
        <small
          :data-hide="!$form.phone?.invalid"
          class="min-h-[1.75rem] data-[hide=true]:opacity-0 data-[hide=true]:-translate-y-1 block transition-all opacity-100 duration-150 translate-y-0 text-sm text-red-400 py-1"
          severity="error"
          size="small"
          variant="simple"
        >
          {{ $form.phone?.error?.message }}
        </small>
      </template>
    </BaseInput>
    <BaseInput label="E-mail" icon="pi pi-envelope">
      <InputText name="email" />
      <template #message>
        <small
          :data-hide="!$form.email?.invalid"
          class="min-h-[1.75rem] data-[hide=true]:opacity-0 data-[hide=true]:-translate-y-1 block transition-all opacity-100 duration-150 translate-y-0 text-sm text-red-400 py-1"
          severity="error"
          size="small"
          variant="simple"
        >
          {{ $form.email?.error?.message }}
        </small>
      </template>
    </BaseInput>
    <div class="flex gap-4">
      <RouterLink class="flex-1" to="/">
        <Button fluid outlined label="Cancelar" icon="pi pi-times" />
      </RouterLink>
      <Button type="submit" outlined class="flex-1" label="Salvar" icon="pi pi-save" />
    </div>
  </Form>
</template>
