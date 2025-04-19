<script setup lang="ts">
import { RouterView } from 'vue-router'
import Toast from 'primevue/toast'
import ConfirmDialog from 'primevue/confirmdialog'
import ConfirmPopup from 'primevue/confirmpopup'
</script>

<template>
  <ConfirmPopup appendTo="body" id="confirm-delete-contact-dialog" />
  <ConfirmDialog group="delete-contact-dialog" />
  <Toast />
  <RouterView />
</template>
