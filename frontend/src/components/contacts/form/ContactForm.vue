<script setup lang="ts">
import { Form } from '@primevue/forms'
import type { FormSubmitEvent } from '@primevue/forms'
import { ref, watch, computed } from 'vue'
import BaseInput from '../../BaseInput.vue'
import Button from 'primevue/button'
import { RouterLink } from 'vue-router'
import { useToast } from 'primevue/usetoast'
import ToggleSwitch from 'primevue/toggleswitch'
import InputMask from 'primevue/inputmask'
import InputText from 'primevue/inputtext'
import { zodResolver } from '@primevue/forms/resolvers/zod'
import { z } from 'zod'
import Toast from 'primevue/toast'

const props = defineProps<{
  initialValues?: {
    name: string
    phoneNumber: string
    email: string
  }
  onSubmit: (formData: { name: string; phoneNumber: string; email: string }) => Promise<void>
}>()

// const formRef = ref<InstanceType<typeof Form>>()
// watch(
//   // Isso é necessário pois o <Form> do PrimeVue só “puxa” o initialValues uma única vez na montagem, e depois não sincroniza mais automaticamente com essa prop
//   () => props.initialValues,
//   (vals) => {
//     if (formRef.value && vals) {
//       formRef.value.setValues(vals)
//     }
//   },
//   { immediate: true },
// )

// gera uma key única toda vez que initialValues mudar
const initialKey = computed(() => JSON.stringify(props.initialValues))

const toast = useToast()
const disableValidation = ref(false)

const contactSchema = z.object({
  name: z.string().nonempty({ message: 'Nenhum nome informado' }),
  phoneNumber: z
    .string()
    .transform((val) => val.replace(/\D/g, ''))
    .refine((val) => val.length === 10 || val.length === 11, {
      message: 'Número de telefone incompleto',
    }),
  email: z.string().email({ message: 'E-mail inválido' }),
})

const resolver = zodResolver(contactSchema)

function noopResolver({ values }: { values: Record<string, any> }) {
  return { values, errors: {} }
}

const activeResolver = computed(() => (disableValidation.value ? noopResolver : resolver))

const submitting = ref(false)

const handleSubmit = async (event: FormSubmitEvent) => {
  if (!disableValidation.value && !event.valid) {
    toast.add({
      severity: 'error',
      summary: 'Erro',
      detail: 'Corrija os campos com erros antes de enviar',
      life: 3000,
    })
    return
  }

  type FieldKey = 'name' | 'phoneNumber' | 'email'

  const formData = Object.fromEntries(
    Object.entries(event.states).map(([key, val]) => {
      const k = key as FieldKey
      return [k, val.value || props.initialValues?.[k] || '']
    }),
  ) as Record<FieldKey, string>

  submitting.value = true
  try {
    await props.onSubmit({
      name: formData.name,
      phoneNumber: formData.phoneNumber,
      email: formData.email,
    })
  } finally {
    submitting.value = false
  }
}
</script>

<template>
  <Toast />
  <Form
    :key="initialKey"
    v-slot="$form"
    :resolver="activeResolver"
    @submit="handleSubmit"
    class="flex flex-col gap-1"
    :initialValues="props.initialValues ?? { name: '', phoneNumber: '', email: '' }"
  >
    <div class="flex items-center gap-2 pb-4">
      <ToggleSwitch inputId="disableValidation" v-model="disableValidation" />
      <label for="disableValidation" class="text-sm">Desabilitar validações do front-end</label>
    </div>
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
      <InputMask name="phoneNumber" :autoClear="false" mask="(99) 99999-9999" />
      <template #message>
        <small
          :data-hide="!$form.phoneNumber?.invalid"
          class="min-h-[1.75rem] data-[hide=true]:opacity-0 data-[hide=true]:-translate-y-1 block transition-all opacity-100 duration-150 translate-y-0 text-sm text-red-400 py-1"
          severity="error"
          size="small"
          variant="simple"
        >
          {{ $form.phoneNumber?.error?.message }}
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
        <Button fluid outlined label="Voltar" icon="pi pi-chevron-left" />
      </RouterLink>
      <div class="flex-1">
        <Button fluid type="submit" outlined label="Salvar" icon="pi pi-save" />
      </div>
    </div>
  </Form>
</template>
