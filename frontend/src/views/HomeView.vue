<script setup lang="ts">
import ContactList from '@/components/contacts/list/ContactList.vue'
import Button from 'primevue/button'
import IconField from 'primevue/iconfield'
import InputIcon from 'primevue/inputicon'
import InputText from 'primevue/inputtext'
import { RouterLink } from 'vue-router'
</script>

<template>
  <header class="flex flex-col items-center justify-center gap-4">
    <h1 class="text-4xl">Agenda</h1>
    <div class="flex gap-4">
      <IconField>
        <InputIcon class="pi pi-search" />
        <InputText />
      </IconField>
      <RouterLink to="/novo-contato">
        <Button
          icon="pi pi-plus"
          aria-label="adicionar contato"
          outlined
          severity="secondary"
          v-tooltip="{ value: 'Adicionar contato', showDelay: 1000, hideDelay: 100 }"
        />
      </RouterLink>
    </div>
  </header>
  <main>
    <ContactList />
  </main>
</template>
