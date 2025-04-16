<script setup lang="ts">
import { defineProps } from 'vue'
import FloatLabel from 'primevue/floatlabel'
import InputIcon from 'primevue/inputicon'
import IconField from 'primevue/iconfield'

defineProps<{
  label: string
  icon?: string
  mask?: string
}>()
</script>

<template>
  <FloatLabel variant="in">
    <IconField>
      <InputIcon v-if="icon" :class="icon" />
      <slot />
    </IconField>
    <label>{{ label }}</label>
  </FloatLabel>
</template>
