<script setup lang="ts">
import Avatar from 'primevue/avatar'
import Panel from 'primevue/panel'
import Button from 'primevue/button'
import { defineProps } from 'vue'

defineProps<{
  contact: {
    id: number
    name: string
    email: string
    phoneNumber: string
    createdAt: string
  }
  isOpen: boolean
}>()

const emit = defineEmits<{
  (e: 'toggle'): void
}>()
</script>

<template>
  <Panel :toggleable="true" :collapsed="!isOpen" @toggle="emit('toggle')">
    <template #toggleicon="{ collapsed }">
      <span :class="collapsed ? 'pi pi-chevron-down' : 'pi pi-chevron-up'"></span>
    </template>
    <template #header>
      <div @click="emit('toggle')" class="flex items-center gap-2 w-full cursor-pointer">
        <Avatar :label="contact.name[0]" shape="circle" />
        <span class="font-bold">{{ contact.name }}</span>
      </div>
    </template>
    <div>
      <div class="flex justify-between">
        <div class="flex gap-2">
          <span class="font-bold">Phone:</span>
          <span>{{ contact.phoneNumber }}</span>
        </div>
        <div class="flex gap-2">
          <span class="font-bold">Email:</span>
          <span>{{ contact.email }}</span>
        </div>
      </div>
    </div>
    <template #footer>
      <div class="flex justify-end">
        <Button
          icon="pi pi-pencil"
          severity="secondary"
          rounded
          text
          size="small"
          v-tooltip.bottom="{ value: 'Editar contato', showDelay: 1000, hideDelay: 100 }"
        />
        <Button
          icon="pi pi-trash"
          severity="secondary"
          rounded
          text
          size="small"
          v-tooltip.bottom="{ value: 'Excluir contato', showDelay: 1000, hideDelay: 100 }"
        />
      </div>
    </template>
  </Panel>
</template>
