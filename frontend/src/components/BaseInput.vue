<script setup lang="ts">
import { defineProps } from 'vue'
import FloatLabel from 'primevue/floatlabel'
import InputText from 'primevue/inputtext'
import InputIcon from 'primevue/inputicon'
import IconField from 'primevue/iconfield'

defineProps<{
  id: string
  label: string
  icon?: string
  modelValue: string
}>()

const emit = defineEmits(['update:modelValue'])
</script>

<template>
  <FloatLabel variant="in">
    <IconField>
      <InputIcon v-if="icon" :class="`pi ${icon}`" />
      <InputText
        :id="id"
        :value="modelValue"
        @input="emit('update:modelValue', $event.target.value)"
        autocomplete="off"
        variant="filled"
      />
    </IconField>
    <label :for="id">{{ label }}</label>
  </FloatLabel>
</template>
