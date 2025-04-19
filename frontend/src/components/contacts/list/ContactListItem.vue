<script setup lang="ts">
import Avatar from 'primevue/avatar'
import Panel from 'primevue/panel'
import Button from 'primevue/button'
import { useConfirm } from 'primevue/useconfirm'
import { useToast } from 'primevue/usetoast'
import { defineProps } from 'vue'
import { useContacts } from '@/composables/useContacts'
import ConfirmPopup from 'primevue/confirmpopup'
import Toast from 'primevue/toast'

const props = defineProps<{
  contact: {
    id: string
    name: string
    email: string
    phoneNumber: string
  }
  isOpen: boolean
}>()

const emit = defineEmits<{
  (e: 'toggle'): void
}>()

const { removeContact } = useContacts()

const confirm = useConfirm()
const toast = useToast()

const acceptDeleteContact = async () => {
  try {
    await removeContact(props.contact.id)
    toast.add({
      severity: 'success',
      detail: 'O contato foi removido com sucesso.',
      life: 3000,
    })
  } catch (err) {
    toast.add({
      severity: 'error',
      detail: err instanceof Error ? err.message : 'Erro desconhecido',
      life: 3000,
    })
  }
}

const confirmDeleteContactDialog = () => {
  confirm.require({
    group: 'delete-contact-dialog',
    message: 'Tem certeza que deseja excluir este contato?',
    icon: 'pi pi-exclamation-triangle',
    rejectProps: {
      label: 'Cancelar',
      outlined: true,
    },
    acceptProps: {
      label: 'Deletar',
      severity: 'danger',
    },
    accept: acceptDeleteContact,
  })
}

const confirmDeleteContact = (event: MouseEvent) => {
  const target = event.currentTarget as HTMLElement

  confirm.require({
    target,
    message: 'Tem certeza que deseja excluir este contato?',
    icon: 'pi pi-exclamation-triangle',
    rejectProps: {
      label: 'Cancelar',
      outlined: true,
    },
    acceptProps: {
      label: 'Deletar',
      severity: 'danger',
    },
    accept: acceptDeleteContact,
  })
}
</script>

<template>
  <Panel class="p-1" :toggleable="true" :collapsed="!isOpen" @toggle="emit('toggle')">
    <Toast />
    <ConfirmPopup></ConfirmPopup>
    <template #toggleicon="{ collapsed }">
      <span :class="collapsed ? 'pi pi-chevron-down' : 'pi pi-chevron-up'"></span>
    </template>
    <template #header>
      <div @click="emit('toggle')" class="flex items-center gap-2 w-full cursor-pointer">
        <Avatar :label="contact.name?.[0]" shape="circle" />
        <span class="font-bold text-xl">{{ contact.name }}</span>
      </div>
    </template>
    <div>
      <div class="flex flex-col gap-2 text-lg">
        <div class="flex gap-2">
          <span class="font-bold">Número de telefone:</span>
          <a class="text-blue-500 underline" :href="`tel:${contact.phoneNumber}`">{{
            contact.phoneNumber
          }}</a>
        </div>
        <div class="flex gap-2">
          <span class="font-bold">Email:</span>
          <a class="text-blue-500 underline" :href="`mailto:${contact.email}`">{{
            contact.email
          }}</a>
        </div>
      </div>
    </div>
    <template #footer>
      <div class="justify-end hidden md:flex">
        <RouterLink :to="`/contato/${contact.id}`">
          <Button
            icon="pi pi-pencil"
            severity="secondary"
            rounded
            text
            size="small"
            v-tooltip.bottom="{ value: 'Editar contato', showDelay: 500, hideDelay: 100 }"
          />
        </RouterLink>
        <Button
          icon="pi pi-trash"
          severity="secondary"
          rounded
          text
          size="small"
          @click="confirmDeleteContact($event)"
          v-tooltip.bottom="{ value: 'Excluir contato', showDelay: 500, hideDelay: 100 }"
        />
      </div>
      <div class="justify-end md:hidden flex">
        <RouterLink :to="`/contato/${contact.id}`">
          <Button
            icon="pi pi-pencil"
            severity="secondary"
            rounded
            text
            size="small"
            v-tooltip.bottom="{ value: 'Editar contato', showDelay: 500, hideDelay: 100 }"
          />
        </RouterLink>
        <Button
          icon="pi pi-trash"
          severity="secondary"
          rounded
          text
          size="small"
          @click="confirmDeleteContactDialog()"
          v-tooltip.bottom="{ value: 'Excluir contato', showDelay: 500, hideDelay: 100 }"
        />
      </div>
    </template>
  </Panel>
</template>
