<script setup lang="ts">
import { ref, onMounted, onBeforeUnmount } from 'vue'
import ContactList from '@/components/contacts/list/ContactList.vue'
import Button from 'primevue/button'
import IconField from 'primevue/iconfield'
import InputIcon from 'primevue/inputicon'
import InputText from 'primevue/inputtext'
import { RouterLink } from 'vue-router'

const search = ref('')

const headerRef = ref<HTMLElement>()
const isSticky = ref(false)

function onScroll() {
  if (!headerRef.value) return
  isSticky.value = window.scrollY > headerRef.value.offsetHeight
}

onMounted(() => {
  window.addEventListener('scroll', onScroll, { passive: true })
})
onBeforeUnmount(() => {
  window.removeEventListener('scroll', onScroll)
})
onMounted(() => {
  const obs = new IntersectionObserver(([e]) => (isSticky.value = !e.isIntersecting), {
    threshold: 0.1,
  })
  if (headerRef.value) obs.observe(headerRef.value)
})
</script>

<template>
  <Transition
    mode="out-in"
    enter-active-class="transition-all duration-300 ease-out"
    leave-active-class="transition-all duration-300 ease-in"
  >
    <!-- eslint-disable-next-line vue/require-toggle-inside-transition -->
    <div
      key="sticky-{{ isSticky }}"
      ref="headerRef"
      class="dark:bg-[#121212]"
      :class="[
        'w-full px-4 transition-all',
        isSticky
          ? 'sticky top-0 z-50 flex items-center md:justify-between justify-center pt-2 pb-2 shadow-md'
          : 'flex flex-col items-center gap-4 pt-20 pb-20',
      ]"
    >
      <h1 class="hidden md:block text-4xl font-bold">Agenda</h1>
      <div class="flex gap-4">
        <IconField>
          <InputIcon class="pi pi-search" />
          <InputText v-model="search" id="search" />
        </IconField>
        <RouterLink to="/contato">
          <Button id="add-contact" icon="pi pi-plus" outlined />
        </RouterLink>
      </div>
    </div>
  </Transition>
  <main>
    <ContactList :filter="search" />
  </main>
</template>
