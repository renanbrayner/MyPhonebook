<template>
  <main class="p-4 md:px-10 md:py-20 lg:px-[20vw]">
    <Transition name="slide-fade" mode="out-in">
      <router-view />
    </Transition>
  </main>
</template>
