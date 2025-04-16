<script setup lang="ts">
import { defineProps } from 'vue'
import FloatLabel from 'primevue/floatlabel'
import InputText from 'primevue/inputtext'
import InputMask from 'primevue/inputmask'
import InputIcon from 'primevue/inputicon'
import IconField from 'primevue/iconfield'

defineProps<{
  id: string
  label: string
  icon?: string
  modelValue: string
  mask?: string
}>()

const emit = defineEmits(['update:modelValue'])
</script>

<template>
  <FloatLabel variant="in">
    <IconField>
      <InputIcon v-if="icon" :class="`pi ${icon}`" />
      <component
        :is="mask ? InputMask : InputText"
        :mask="mask"
        :autoClear="false"
        :id="id"
        :modelValue="modelValue"
        @update:modelValue="emit('update:modelValue', $event)"
        autocomplete="off"
        variant="filled"
      />
    </IconField>
    <label :for="id">{{ label }}</label>
  </FloatLabel>
</template>
